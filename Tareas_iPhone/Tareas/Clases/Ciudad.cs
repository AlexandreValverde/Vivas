using System.Threading;
using Foundation;
using Tareas.Servidor;

namespace Tareas.Clases
{
    public class Ciudad : Provincia
    {
        // Variables
        public int ID_Ciudad { set; get; }
        public string Nombre_Ciudad { set; get; }

        // Conexion servidor
        private static readonly object l = new object();

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Ciudad()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID ciudad.</param>
        /// <param name="nombre">Nombre ciudad.</param>
        public Ciudad(int id, string nombre)
        {
            ID_Ciudad = id;
            Nombre_Ciudad = nombre;
        }

        /// <summary>
        /// Obtengo ciudad de SQLite o del servidor.
        /// </summary>
        public void GetDataCiudad()
        {
            // Ciudad
            Ciudad c;

            // Obtengo ciudad
            c = SQLiteManager.Connection().GetCiudad(ID_Ciudad);

            // Compruebo datos
            if (c == null)
            {
                // Obtengo ciudad del servidor
                RestManager.Connection().GetData((int)URIS.GetCiudad, new string[] { ID_Ciudad.ToString() }, null, (arg) =>
                {
                    // Compruebo datos
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        // Deserializo JSON
                        NSDictionary data = (NSDictionary)NSJsonSerialization.Deserialize(arg, 0, out NSError e);

                        // Leo datos
                        foreach (NSString key in data.Keys)
                        {
                            switch (key.ToString().ToLower())
                            {
                                case "id_ciudad":
                                    ID_Ciudad = (int)(NSNumber)data.ValueForKey(key);
                                    break;
                                case "id_provincia":
                                    ID_Provincia = (int)(NSNumber)data.ValueForKey(key);
                                    break;
                                case "nombre":
                                    Nombre_Ciudad = data.ValueForKey(key).ToString();
                                    break;
                            }
                        }

                        // Guardo en SQLite
                        SQLiteManager.Connection().SetCiudad(this);
                    }

                    // Continuo
                    lock (l)
                    {
                        Monitor.Pulse(l);
                    }
                });

                // Espero
                lock (l)
                {
                    Monitor.Wait(l);
                }
            }
            else
            {
                // Cargo nombre
                Nombre_Ciudad = c.Nombre_Ciudad;

                // Cargo ID provincia
                ID_Provincia = c.ID_Provincia;
            }

            // Obtengo datos provincia
            GetDataProvincia();
        }
    }
}