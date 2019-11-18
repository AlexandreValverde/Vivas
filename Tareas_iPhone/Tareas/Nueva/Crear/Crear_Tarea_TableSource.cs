using System;
using Foundation;
using UIKit;

namespace Tareas.Nueva.Crear
{
    public class Crear_Tarea_TableSource : UITableViewSource
    {
        // Controller
        private Crear_Tarea_ViewController controller;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="controller">Controller.</param>
        public Crear_Tarea_TableSource(Crear_Tarea_ViewController controller)
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
            return 4;
        }

        /// <summary>
        /// Crea y obtiene celda.
        /// </summary>
        /// <param name="tableView"></param>
        /// <param name="indexPath"></param>
        /// <returns></returns>
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Crea y obtiene celdas
            switch (indexPath.Row)
            {
                case 0: // Datos cliente
                    Crear_Tarea_TableViewCell1 cell1 = (Crear_Tarea_TableViewCell1)tableView.DequeueReusableCell("Crear_Tarea_Cell1", indexPath);
                    cell1.UpdateCell(controller);
                    return cell1;
                case 1: // Opcion tarea
                    Crear_Tarea_TableViewCell2 cell2 = (Crear_Tarea_TableViewCell2)tableView.DequeueReusableCell("Crear_Tarea_Cell2", indexPath);
                    cell2.UpdateCell(controller);
                    return cell2;
                case 2: // Descripcion tarea
                    Crear_Tarea_TableViewCell3 cell3 = (Crear_Tarea_TableViewCell3)tableView.DequeueReusableCell("Crear_Tarea_Cell3", indexPath);
                    cell3.UpdateCell(controller);
                    return cell3;
                case 3: // Ubicacion
                    Crear_Tarea_TableViewCell4 cell4 = (Crear_Tarea_TableViewCell4)tableView.DequeueReusableCell("Crear_Tarea_Cell4", indexPath);
                    cell4.UpdateCell(controller);
                    return cell4;
                default: return null;
            }
        }
    }
}
