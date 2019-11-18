namespace Tareas.Clases
{
    public class Coordenadas
    {
        public double Latitud { set; get; }
        public double Longitud { set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lat">Latitud.</param>
        /// <param name="lng">Longitud.</param>
        public Coordenadas(double lat, double lng)
        {
            Latitud = lat;
            Longitud = lng;
        }
    }
}

