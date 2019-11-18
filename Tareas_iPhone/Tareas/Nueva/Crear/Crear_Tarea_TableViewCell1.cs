using System;
using UIKit;
using Tareas.Clases;

namespace Tareas.Nueva.Crear
{
    public partial class Crear_Tarea_TableViewCell1 : UITableViewCell
    {
        // Controller
        private Crear_Tarea_ViewController controller;

        public Crear_Tarea_TableViewCell1(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Carga la celda.
        /// </summary>
        /// <param name="controller">Controller.</param>
        public void UpdateCell(Crear_Tarea_ViewController controller)
        {
            // Controller
            this.controller = controller;

            // Inicializo cliente
            controller.t.Cliente = new Cliente();
        }

        /// <summary>
        /// Nombre cambiado.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtNombre_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Nombre = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Apellidos cambiado.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtApellidos_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Apellidos = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Telefono cambiado.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtTelefono_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Telefono = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }
    }
}
