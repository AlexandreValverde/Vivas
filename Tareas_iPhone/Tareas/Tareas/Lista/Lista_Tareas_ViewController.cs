using System;
using UIKit;
using Tareas.Servidor;
using Tareas.Clases;
using System.Threading;
using Foundation;

namespace Tareas.Tareas.Lista
{
    public partial class Lista_Tareas_ViewController : UIViewController
    {
        // Titulo carga
        private bool FlagCarga;
        private int CountCarga;

        // Tareas
        public Tarea[] tareas;

        public Lista_Tareas_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Recargo datos
            ObtengoTareas();
        }

        /// <summary>
        /// Obtengo tareas.
        /// </summary>
        private void ObtengoTareas()
        {
            // Inicio carga de datos
            InitCarga();

            // Lista tareas
            tareas = null;

            // Elimino tareas SQLite
            SQLiteManager.Connection().DeleteTareas();

            // Obtengo datos servidor (Obtengo fecha de creacion de la ultima tarea para descargar solo las nuevas, las demas las obtengo de SQLite)
            RestManager.Connection().GetData((int)URIS.TareasNuevas, new string[] { "1972-01-01 00:00:01" }, null, (arg) =>
            {
                // Compruebo datos
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    // JSON
                    if (!arg.Equals("[]"))
                    {
                        // Leo JSON
                        tareas = JSONManager.DeserializeTareas(arg);

                        // Recorro tareas
                        foreach (Tarea t in tareas)
                        {
                            // Obtengo datos
                            InvokeOnMainThread(() =>
                            {
                                // Datos direccion
                                t.GetDireccionData();

                                // Guardo tarea
                                SQLiteManager.Connection().SetTarea(t);
                            });
                        }
                    }
                }

                InvokeOnMainThread(() =>
                {
                    // Obtengo tareas SQLite
                    tareas = SQLiteManager.Connection().GetTareas();

                    // Obtengo datos
                    if (tareas != null)
                    {
                        // Recorro tareas
                        foreach (Tarea t in tareas)
                        {
                            // Obtiene direccion
                            t.GetDireccionData();

                            // Obtiene usuario
                            t.GetUserCompletada();
                        }
                    }

                    // Cargo tabla
                    table.Source = new Lista_Tareas_TableSource(this);
                    table.ReloadData();

                    // Compruebo si no hay tareas
                    if (tareas == null)
                    {
                        // Creo alerta
                        UIAlertController alert = UIAlertController.Create("Sin datos", "No existen tareas", UIAlertControllerStyle.Alert);

                        // Añado accion
                        alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        // Muestro alerta
                        PresentViewController(alert, true, null);
                    }

                    // Detengo titulo carga
                    FlagCarga = false;
                });
            });
        }

        /// <summary>
        /// Muestra en el titulo cargando.
        /// </summary>
        private void InitCarga()
        {
            // Activo flag
            FlagCarga = true;

            // Count carga
            CountCarga = 0;

            // Creo hilo
            new Thread(() =>
            {
                // Compruebo flag
                while (FlagCarga)
                {
                    InvokeOnMainThread(() =>
                    {
                        // Cambio titulo
                        switch (CountCarga)
                        {
                            case 0:
                                Title = "Obteniendo datos";
                                break;
                            case 1:
                                Title = " Obteniendo datos.";
                                break;
                            case 2:
                                Title = "  Obteniendo datos..";
                                break;
                            case 3:
                                Title = "  Obteniendo datos...";
                                break;
                        }

                        // Sumo contador
                        CountCarga++;

                        // Restauro contador
                        if (CountCarga > 3) CountCarga = 0;
                    });

                    // Espero
                    Thread.Sleep(400);
                }

                // Restauro
                InvokeOnMainThread(() =>
                {
                    // Titulo
                    Title = "Lista";

                    // Activo tabla
                    table.UserInteractionEnabled = true;

                    // Activo boton filtrar
                    btnFiltrar.Enabled = true;
                });
            }).Start();
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if (segue.DestinationViewController is Filtrar.Filtrar_Tareas_ViewController filtrarSegue)
            {
                filtrarSegue.controllerLista = this;
            }

            if (segue.DestinationViewController is Datos.Datos_Tarea_ViewController datosSegue)
            {
                datosSegue.controller = this;
                datosSegue.t = tareas[(int)(NSNumber)sender];
            }
        }
    }
}
