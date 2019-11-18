using System;
using UIKit;

namespace Tareas.Nueva.Crear
{
    public partial class Crear_Tarea_TableViewCell3 : UITableViewCell
    {
        // Controller
        private Crear_Tarea_ViewController controller;

        public Crear_Tarea_TableViewCell3(IntPtr handle) : base(handle)
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

            // Notas cambiado
            txtDescrip.Changed += TxtDescrip_Changed;
        }

        /// <summary>
        /// Descripcion tarea cambiada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtDescrip_Changed(object sender, EventArgs e)
        {
            // Actualizo tarea
            controller.t.Notas = ((UITextView)sender).Text.Trim();
        }
    }
}
