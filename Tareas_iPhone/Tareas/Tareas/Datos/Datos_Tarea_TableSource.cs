using System;
using Foundation;
using UIKit;

namespace Tareas.Tareas.Datos
{
    public class Datos_Tarea_TableSource : UITableViewSource
    {
        // Controller
        private Datos_Tarea_ViewController controller;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller">Controller.</param>
        public Datos_Tarea_TableSource(Datos_Tarea_ViewController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Numero de filas.
        /// </summary>
        /// <param name="tableview"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        /// <summary>
        /// Crea y obtiene celda.
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="indexPath"></param>
        /// <returns></returns>
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Creo la celda
            Datos_Tarea_TableViewCell cell = (Datos_Tarea_TableViewCell)tableView.DequeueReusableCell("Datos_Tarea_Cell", indexPath);
            cell.UpdateCell(controller.t);
            return cell;
        }
    }
}
