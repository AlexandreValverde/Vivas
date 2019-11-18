using System;
using Foundation;
using UIKit;

namespace Tareas.Tareas.Lista
{
    public class Lista_Tareas_TableSource : UITableViewSource
    {
        // Controller
        private readonly Lista_Tareas_ViewController Controller;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller"></param>
        public Lista_Tareas_TableSource(Lista_Tareas_ViewController controller)
        {
            Controller = controller;
        }

        /// <summary>
        /// Numero de filas en la seccion.
        /// </summary>
        /// <param name="tableview"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (Controller.tareas == null)
            {
                return 0;
            }
            return Controller.tareas.Length;
        }

        /// <summary>
        /// Obtiene celda.
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="indexPath"></param>
        /// <returns></returns>
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Creo la celda
            Lista_Tareas_TableViewCell cell = (Lista_Tareas_TableViewCell)tableView.DequeueReusableCell("Lista_Tareas_Cell", indexPath);
            cell.UpdateCell(Controller.tareas[indexPath.Row]);
            return cell;
        }

        /// <summary>
        /// Muestra celda seleccionada.
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="indexPath"></param>
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            // Muestra tarea
            Controller.PerformSegue("datosSegue", (NSNumber)indexPath.Row);
        }
    }
}