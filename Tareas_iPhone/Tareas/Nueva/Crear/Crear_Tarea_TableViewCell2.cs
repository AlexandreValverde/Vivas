using System;
using UIKit;

namespace Tareas.Nueva.Crear
{
    public partial class Crear_Tarea_TableViewCell2 : UITableViewCell
    {
        // Controller
        private Crear_Tarea_ViewController controller;

        public Crear_Tarea_TableViewCell2(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Carga la celda.
        /// </summary>
        /// <param name="controller">Controller.</param>
        public void UpdateCell(Crear_Tarea_ViewController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Valor urgente cambiado.
        /// </summary>
        /// <param name="sender"></param>
        partial void SwitchUrgente_Changed(UISwitch sender)
        {
            // Actualizo tarea
            controller.t.Urgente = sender.On;
        }

        /// <summary>
        /// Valor opcion 2 cambiado.
        /// </summary>
        /// <param name="sender"></param>
        partial void SwitchOp2_Changed(UISwitch sender)
        {
            // Actualizo tarea
            controller.t.OP2 = sender.On;
        }
    }
}
