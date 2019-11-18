using System;
using System.Threading;
using Tareas.Clases;
using Tareas.Servidor;
using UIKit;

namespace Tareas.Tareas.Datos
{
    public partial class Datos_Tarea_ViewController : UIViewController
    {
        // Controller
        public Lista.Lista_Tareas_ViewController controller;

        // Tarea
        public Tarea t;

        // Hilo
        private static readonly object l = new object();

        public Datos_Tarea_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Direccion
            Direccion d = null;
            if (t.Direccion.ID_Direccion == 0)
            {
                d = t.Cliente.Direccion;
            }

            // Obtengo datos cliente
            t.GetClienteData();

            // Direccion cliente
            if (d != null)
            {
                t.Direccion = d;
            }

            // Cargo tabla
            tabla.Source = new Datos_Tarea_TableSource(this);
            tabla.ReloadData();

            // Completo tarea
            if (t.ID_Usuario != 0)
            {
                Completar();
            }
        }

        /// <summary>
        /// Muestra boton como tarea completada.
        /// </summary>
        private void Completar()
        {
            // Tarea completada
            btnComplete.Enabled = false;

            // Color
            btnComplete.BackgroundColor = UIColor.Red;

            // Titulo
            btnComplete.SetTitle("COMPLETADA", UIControlState.Normal);
        }

        /// <summary>
        /// Touch boton completada.
        /// </summary>
        /// <param name="sender"></param>
        partial void Touch_Completar(UIButton sender)
        {
            // Actualizo boton
            Completar();

            // Completo
            RestManager.Connection().GetData((int)URIS.CompletaTarea, new string[] { t.ID_Tarea.ToString() }, null, (arg) =>
            {
                // Continuo
                lock (l)
                {
                    Monitor.Pulse(l);
                }
            });

            // Espero conexion
            lock (l)
            {
                Monitor.Wait(l);
            }
        }
    }
}

