using System;
using UIKit;
using Tareas.Clases;
using Tareas.Localizacion;
using MapKit;

namespace Tareas.Nueva.Crear
{
    public partial class Crear_Tarea_TableViewCell4 : UITableViewCell
    {
        // Controller
        private Crear_Tarea_ViewController controller;

        public Crear_Tarea_TableViewCell4(IntPtr handle) : base(handle)
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

            // Inicializo direccion tarea
            controller.t.Cliente.Direccion = new Direccion();

            // Cargo mapa
            mapa.SetRegion(new MKCoordinateRegion(LocationManager.GetCoordsMap(), new MKCoordinateSpan(0.05, 0.05)), true);
            mapa.RegionChanged += Mapa_RegionChanged;

            // Inicializo coordenadas
            controller.t.Cliente.Direccion.Coords = LocationManager.GetCoords();
        }

        /// <summary>
        /// Cambio valor pais.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtPais_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Nombre_Pais = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Cambio valor provincia.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtProvincia_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Nombre_Provincia = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Cambio valor ciudad.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtCiudad_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Nombre_Ciudad = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Cambio valor direccion.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtDireccion_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Calle = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Cambio valor numero.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtNumero_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Numero = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Cambio valor nombre lugar.
        /// </summary>
        /// <param name="sender"></param>
        partial void TxtLugar_Changed(UITextField sender)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Nombre_Direccion = string.IsNullOrWhiteSpace(sender.Text) ? "" : sender.Text.Trim();
        }

        /// <summary>
        /// Cambio region mapa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mapa_RegionChanged(object sender, MKMapViewChangeEventArgs e)
        {
            // Actualizo tarea
            controller.t.Cliente.Direccion.Coords = new Coordenadas(mapa.CenterCoordinate.Latitude, mapa.CenterCoordinate.Longitude);
        }
    }
}
