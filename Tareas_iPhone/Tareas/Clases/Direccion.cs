using Foundation;

namespace Tareas.Clases
{
    public class Direccion : Ciudad
    {
        // Variables
        public int ID_Direccion { set; get; }
        public string Nombre_Direccion { set; get; } // Nombre del lugar (Colegio Vivas)
        public string Calle { set; get; }
        public string Numero { set; get; }
        public string Codigo_Postal { set; get; }
        public Coordenadas Coords { set; get; }

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Direccion()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Datos JSON.</param>
        public Direccion(NSDictionary data)
        {
            // Coordenadas
            double lat = 0;
            double lng = 0;

            // Leo datos
            foreach (NSString key in data.Keys)
            {
                switch (key.ToString().ToLower())
                {
                    case "id_direccion":
                        ID_Direccion = (int)(NSNumber)data.ValueForKey(key);
                        break;
                    case "nombre":
                        Nombre_Direccion = data.ValueForKey(key).ToString();
                        break;
                    case "direccion":
                        Calle = data.ValueForKey(key).ToString();
                        break;
                    case "numero":
                        Numero = data.ValueForKey(key).ToString();
                        break;
                    case "codigo_postal":
                        Codigo_Postal = data.ValueForKey(key).ToString();
                        break;
                    case "id_ciudad":
                        ID_Ciudad = (int)(NSNumber)data.ValueForKey(key);
                        break;
                    case "lat":
                        lat = (double)(NSNumber)data.ValueForKey(key);
                        break;
                    case "lng":
                        lng = (double)(NSNumber)data.ValueForKey(key);
                        break;
                }
            }

            // Guardo coordenadas
            Coords = new Coordenadas(lat, lng);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id_pais">ID pais.</param>
        /// <param name="nombre_pais">Nombre pais.</param>
        /// <param name="id_provincia">ID provincia.</param>
        /// <param name="nombre_provincia">Nombre provincia.</param>
        /// <param name="id_ciudad">ID ciudad.</param>
        /// <param name="nombre_ciudad">Nombre ciudad.</param>
        /// <param name="id_direccion">ID direccion.</param>
        /// <param name="nombre_direccion">Nombre direccion.</param>
        /// <param name="calle">Calle (nombre).</param>
        /// <param name="num">Numero (calle).</param>
        /// <param name="cod">Codigo postal.</param>
        /// <param name="lat">Latitud.</param>
        /// <param name="lng">Longitud.</param>
        public Direccion(int id_pais, string nombre_pais, int id_provincia, string nombre_provincia, int id_ciudad, string nombre_ciudad, int id_direccion, string nombre_direccion, string calle, string num, string cod, double lat, double lng)
        {
            ID_Pais = id_pais;
            Nombre_Pais = nombre_pais;
            ID_Provincia = id_provincia;
            Nombre_Provincia = nombre_provincia;
            ID_Ciudad = id_ciudad;
            Nombre_Ciudad = nombre_ciudad;
            ID_Direccion = id_direccion;
            Nombre_Direccion = nombre_direccion;
            Calle = calle;
            Numero = num;
            Codigo_Postal = cod;
            Coords = new Coordenadas(lat, lng);
        }

        /// <summary>
        /// Obtiene datos en formato JSON.
        /// </summary>
        /// <returns>JSON.</returns>
        public string GetJSON()
        {
            // JSON
            string j = "{";

            // Añado datos
            j += JSONManager.GetFormat("nombre_pais", Nombre_Pais) + ",";
            j += JSONManager.GetFormat("nombre_provincia", Nombre_Provincia) + ",";
            j += JSONManager.GetFormat("nombre_ciudad", Nombre_Ciudad) + ",";
            j += JSONManager.GetFormat("nombre", Nombre_Direccion) + ",";
            j += JSONManager.GetFormat("direccion", Calle) + ",";
            j += JSONManager.GetFormat("numero", Numero) + ",";
            j += JSONManager.GetFormat("lat", Coords.Latitud) + ",";
            j += JSONManager.GetFormat("lng", Coords.Longitud);

            // Cierro y devuelvo JSON
            return j += "}";
        }
    }
}