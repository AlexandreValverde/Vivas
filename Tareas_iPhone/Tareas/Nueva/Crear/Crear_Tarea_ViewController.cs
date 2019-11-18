using System;
using System.Threading;
using Tareas.Clases;
using Tareas.Servidor;
using UIKit;

namespace Tareas.Nueva.Crear
{
    public partial class Crear_Tarea_ViewController : UIViewController
    {
        // Tarea
        public Tarea t;

        // Flag carga
        private bool FlagCarga;

        // Hilo
        private static readonly object l = new object();

        public Crear_Tarea_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Inicializo tarea
            t = new Tarea
            {
                Tipo = (UIApplication.SharedApplication.Delegate as AppDelegate).tipoNueva
            };

            // Cargo tabla
            tabla.Source = new Crear_Tarea_TableSource(this);
            tabla.ReloadData();
        }

        /// <summary>
        /// Boton añadir.
        /// </summary>
        /// <param name="sender"></param>
        partial void Add_Touch(UIBarButtonItem sender)
        {
            // Compruebo datos
            if (CheckData())
            {
                // Desactivo tabla
                tabla.UserInteractionEnabled = false;

                // Desactivo botones
                btnAdd.Enabled = false;
                btnCancel.Enabled = false;

                // Activo carga
                InitCarga();

                // Guardo direccion
                RestManager.Connection().GetData((int)URIS.AddLugar, null, t.Cliente.Direccion.GetJSON(), (arg) =>
                {
                    // Compruebo datos
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        // Guardo ID direccion
                        t.Cliente.Direccion.ID_Direccion = int.Parse(arg);
                    }

                    // Continuo
                    lock (l)
                    {
                        Monitor.Pulse(l);
                    }
                });

                // Espero a obtener datos direccion
                lock (l)
                {
                    Monitor.Wait(l);
                }

                // Guardo cliente
                RestManager.Connection().GetData((int)URIS.AddCliente, null, t.Cliente.GetJSON(), (arg) =>
                {
                    // Depuracion
                    Console.WriteLine("ID cliente -> " + arg ?? "null");

                    // Compruebo datos
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        // Guardo ID cliente
                        t.Cliente.ID_Cliente = int.Parse(arg);
                    }

                    // Continuo
                    lock (l)
                    {
                        Monitor.Pulse(l);
                    }
                });

                // Espero a obtener datos cliente
                lock (l)
                {
                    Monitor.Wait(l);
                }

                // Guardo tarea
                RestManager.Connection().GetData((int)URIS.AddTarea, null, t.GetJSON(), (arg) =>
                {
                    // Depuracion
                    Console.WriteLine("Tarea guardada");

                    // Continuo
                    lock (l)
                    {
                        Monitor.Pulse(l);
                    }
                });

                // Espero a obtener datos cliente
                lock (l)
                {
                    Monitor.Wait(l);
                }

                // Creo alerta
                UIAlertController alert = UIAlertController.Create("Tarea guardada", "La tarea ha sido guardada con exito!", UIAlertControllerStyle.Alert);

                // Añado accion
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (arg) =>
                {
                    // Vuelve a la view anterior
                    DismissModalViewController(true);
                }));

                // Muestro alerta
                PresentViewController(alert, true, null);
            }
            else
            {
                // Creo alerta
                UIAlertController alert = UIAlertController.Create("Error", "Rellene todos los datos para guardar la tarea", UIAlertControllerStyle.Alert);

                // Añado accion
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Muestro alerta
                PresentViewController(alert, true, null);
            }
        }

        /// <summary>
        /// Compruebo los datos de la tarea.
        /// </summary>
        /// <returns>True tarea valida, false no.</returns>
        private bool CheckData()
        {
            // Cliente
            if (string.IsNullOrWhiteSpace(t.Cliente.Nombre)) return false;
            if (string.IsNullOrWhiteSpace(t.Cliente.Apellidos)) return false;
            if (string.IsNullOrWhiteSpace(t.Cliente.Telefono)) return false;

            // Direccion
            if (string.IsNullOrWhiteSpace(t.Cliente.Direccion.Nombre_Pais)) return false;
            if (string.IsNullOrWhiteSpace(t.Cliente.Direccion.Nombre_Provincia)) return false;
            if (string.IsNullOrWhiteSpace(t.Cliente.Direccion.Nombre_Ciudad)) return false;
            if (string.IsNullOrWhiteSpace(t.Cliente.Direccion.Nombre_Direccion)) return false;

            // Devuelvo OK
            return true;
        }

        /// <summary>
        /// Muestra en el titulo cargando.
        /// </summary>
        private void InitCarga()
        {
            // Activo flag
            FlagCarga = true;

            // Count carga
            int CountCarga = 0;

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
                                Title = "Guardando";
                                break;
                            case 1:
                                Title = " Guardando.";
                                break;
                            case 2:
                                Title = "  Guardando..";
                                break;
                            case 3:
                                Title = "  Guardando...";
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
                    Title = "Nueva";

                    // Activo tabla
                    tabla.UserInteractionEnabled = true;

                    // Activo botones
                    btnAdd.Enabled = true;
                    btnCancel.Enabled = true;
                });
            }).Start();
        }

        /// <summary>
        /// Touch boton cancelar.
        /// </summary>
        /// <param name="sender"></param>
        partial void BtnCancel_Touch(UIBarButtonItem sender)
        {
            // Vuelvo a la view anterior
            DismissModalViewController(true);
        }
    }
}

