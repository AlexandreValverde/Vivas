using System;
using CoreLocation;
using MapKit;
using Tareas.Clases;
using UIKit;

namespace Tareas.Tareas.Datos
{
    public partial class Datos_Tarea_TableViewCell : UITableViewCell
    {
        public Datos_Tarea_TableViewCell(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Carga celda.
        /// </summary>
        /// <param name="t">Tarea a mostrar.</param>
        public void UpdateCell(Tarea t)
        {
            // Cargo datos
            lblNombre.Text = t.Cliente.Nombre + " " + t.Cliente.Apellidos;
            lblTelefono.Text = t.Cliente.Telefono;
            lblOp.Text = t.Urgente ? "Urgente " : "";
            lblOp.Text += t.OP2 ? "Sin agua" : "";
            lblNotas.Text = t.Notas;
            lblCiudad.Text = t.Direccion.Nombre_Ciudad + ", " + t.Direccion.Nombre_Provincia;
            lblCalle.Text = (string.IsNullOrWhiteSpace(t.Direccion.Nombre_Direccion) ? "" : t.Direccion.Nombre_Direccion + ", ") + t.Direccion.Calle;

            // Mapa
            CargoMapa(t.Direccion.Coords);
        }

        /// <summary>
        /// Muestro region del mapa y añado punto.
        /// </summary>
        /// <param name="coords">Coordenadas.</param>
        private void CargoMapa(Coordenadas coords)
        {
            // Coordenadas
            CLLocationCoordinate2D mapCoordinate = new CLLocationCoordinate2D(coords.Latitud, coords.Longitud);

            // Region que se muestra
            map.SetRegion(MKCoordinateRegion.FromDistance(mapCoordinate, 1000, 1000), true);

            // Creo y añado punto
            map.AddAnnotation(new MKPointAnnotation { Coordinate = mapCoordinate });
        }
    }
}
