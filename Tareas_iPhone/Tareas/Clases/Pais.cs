using System.Threading;
using Foundation;
using Tareas.Servidor;

namespace Tareas.Clases
{
    public class Pais
    {
        // Variables
        public int ID_Pais { get; set; }
        public string Nombre_Pais { set; get; }

        // Conexion servidor
        private static readonly object l = new object();

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Pais()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID del pais.</param>
        /// <param name="nombre">Nombre del pais.</param>
        public Pais(int id, string nombre)
        {
            ID_Pais = id;
            Nombre_Pais = nombre;
        }

        /// <summary>
        /// Obtiene datos del pais.
        /// </summary>
        public void GetDataPais()
        {
            // Pais
            Pais p;

            // Obtengo pais
            p = SQLiteManager.Connection().GetProvincia(ID_Pais);

            // Compruebo datos
            if (p == null)
            {
                // Obtengo pais del servidor
                RestManager.Connection().GetData((int)URIS.GetPais, new string[] { ID_Pais.ToString() }, null, (arg) =>
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
                                case "id_pais":
                                    ID_Pais = (int)(NSNumber)data.ValueForKey(key);
                                    break;
                                case "nombre":
                                    Nombre_Pais = data.ValueForKey(key).ToString();
                                    break;
                            }
                        }

                        // Guardo en SQLite
                        SQLiteManager.Connection().SetPais(this);
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
                Nombre_Pais = p.Nombre_Pais;
            }
        }
    }
}