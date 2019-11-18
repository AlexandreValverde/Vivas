using System;
using Tareas.Clases;
using UIKit;

namespace Tareas.Tareas.Lista
{
    public partial class Lista_Tareas_TableViewCell : UITableViewCell
    {
        public Lista_Tareas_TableViewCell(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Carga la celda.
        /// </summary>
        /// <param name="t"></param>
        public void UpdateCell(Tarea t)
        {
            // Obtengo direccion
            t.GetDireccionData();

            // Direccion
            lblUbic.Text = t.Direccion.Nombre_Direccion == null ? "" : t.Direccion.Nombre_Direccion + ", ";
            lblUbic.Text += t.Direccion.Calle + ", " + t.Direccion.Nombre_Ciudad;

            // Fecha
            lblFecha.Text = t.Fecha_creacion;

            // Opcion
            lblInfo.Text = t.Urgente ? "Urgente" : "Normal";
            lblInfo.Text = t.OP2 ? "Sin agua" : lblInfo.Text;

            // Distancia
            lblKms.Text = string.Format("{0:N1} kms", t.GetDistancia());

            // Color celda
            BackgroundColor = t.Tipo ? UIColor.FromRGB(163, 228, 215) : UIColor.FromRGB(249, 231, 159);
        }
    }
}
