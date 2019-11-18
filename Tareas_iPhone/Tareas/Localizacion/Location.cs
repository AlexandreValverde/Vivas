using CoreLocation;
using UIKit;
using Tareas.Clases;

namespace Tareas.Localizacion
{
    public class Location
    {
        // Localizador
        private CLLocationManager Localizacion;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Location()
        {
            // Objeto localizador
            Localizacion = new CLLocationManager();

            // Compruebo permisos
            switch (CLLocationManager.Status)
            {
                // Pregunta por autorizacion
                case CLAuthorizationStatus.NotDetermined:
                    Localizacion.RequestWhenInUseAuthorization();
                    break;
                case CLAuthorizationStatus.Restricted:
                case CLAuthorizationStatus.Denied:
                    Localizacion.RequestWhenInUseAuthorization();
                    break;
                case CLAuthorizationStatus.AuthorizedWhenInUse:
                    CompruebaPermisos();
                    break;
                case CLAuthorizationStatus.AuthorizedAlways:
                    CompruebaPermisos();
                    break;
            }
        }

        /// <summary>
        /// Comprueba los permisos de localizacion en el dispositivo.
        /// </summary>
        private void CompruebaPermisos()
        {
            // Comprueba si tiene permiso de localizacion
            if (CLLocationManager.LocationServicesEnabled)
            {
                Localizacion.StartUpdatingLocation();
            }

            // Comprueba si tiene permiso para mostrar la localización
            if (CLLocationManager.HeadingAvailable)
            {
                Localizacion.StartUpdatingHeading();
            }
        }

        /// <summary>
        /// Obtiene las coordenadas de la localización actual.
        /// </summary>
        /// <returns>Las coordenadas.</returns>
        public Coordenadas GetCoords()
        {
            return new Coordenadas(Localizacion.Location.Coordinate.Latitude, Localizacion.Location.Coordinate.Longitude);
        }

        /// <summary>
        /// Obtiene coordenadas localizacion usuario.
        /// </summary>
        /// <returns>Coordenadas.</returns>
        public CLLocationCoordinate2D GetCoordsMap()
        {
            return Localizacion.Location.Coordinate;
        }
    }

    public static class LocationManager
    {
        /// <summary>
        /// Obtiene las coordenadas del dispositivo.
        /// </summary>
        /// <returns>Coordenadas.</returns>
        public static Coordenadas GetCoords()
        {
            return (UIApplication.SharedApplication.Delegate as AppDelegate).loc.GetCoords();
        }

        /// <summary>
        /// Obtiene coordenadas para mapa.
        /// </summary>
        /// <returns>Coordenadas.</returns>
        public static CLLocationCoordinate2D GetCoordsMap()
        {
            return (UIApplication.SharedApplication.Delegate as AppDelegate).loc.GetCoordsMap();
        }
    }
}