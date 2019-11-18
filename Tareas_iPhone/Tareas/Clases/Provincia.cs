using System.Threading;
using Foundation;
using Tareas.Servidor;
using System;

namespace Tareas.Clases
{
    public class Provincia : Pais
    {
        // Variables
        public int ID_Provincia { set; get; }
        public string Nombre_Provincia { set; get; }

        // Conexion servidor
        private static readonly object l = new object();

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Provincia()
        {
        }

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="id">ID provincia.</param>
        /// <param name="nombre">Nombre provincia.</param>
        public Provincia(int id, string nombre)
        {
            ID_Provincia = id;
            Nombre_Provincia = nombre;
        }

        /// <summary>
        /// Obtiene datos de la provincia.
        /// </summary>
        public void GetDataProvincia()
        {
            // Provincia
            Provincia p;

            // Obtengo provincia
            p = SQLiteManager.Connection().GetProvincia(ID_Provincia);

            // Compruebo datos
            if (p == null)
            {
                // Obtengo provincia del servidor
                RestManager.Connection().GetData((int)URIS.GetProvincia, new string[] { ID_Provincia.ToString() }, null, (arg) =>
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
                                case "id_provincia":
                                    ID_Provincia = (int)(NSNumber)data.ValueForKey(key);
                                    break;
                                case "id_pais":
                                    ID_Pais = (int)(NSNumber)data.ValueForKey(key);
                                    break;
                                case "nombre":
                                    Nombre_Provincia = data.ValueForKey(key).ToString();
                                    break;
                            }
                        }

                        // Guardo en SQLite
                        SQLiteManager.Connection().SetProvincia(this);
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
                Nombre_Provincia = p.Nombre_Provincia;

                // Cargo ID pais
                ID_Pais = p.ID_Pais;
            }

            // Obtengo datos pais
            GetDataPais();
        }
    }
}