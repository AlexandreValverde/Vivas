using System.Threading;
using Foundation;
using Tareas.Servidor;

namespace Tareas.Clases
{
    public class Cliente
    {
        // Variables
        public int ID_Cliente { set; get; }
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public string Telefono { set; get; }
        public Direccion Direccion { set; get; }

        // Conexion servidor
        private static readonly object l = new object();

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Cliente()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID cliente.</param>
        public Cliente(int id)
        {
            // ID cliente
            ID_Cliente = id;

            // Obtengo datos direccion
            GetDireccionData();
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="data">Datos JSON.</param>
        public Cliente(NSDictionary data)
        {
            // Leo datos
            foreach (NSString key in data.Keys)
            {
                switch (key.ToString().ToLower())
                {
                    case "id_cliente":
                        ID_Cliente = (int)(NSNumber)data.ValueForKey(key);
                        break;
                    case "nombre":
                        Nombre = data.ValueForKey(key).ToString();
                        break;
                    case "apellidos":
                        Apellidos = data.ValueForKey(key).ToString();
                        break;
                    case "telefono":
                        Telefono = data.ValueForKey(key).ToString();
                        break;
                    case "id_direccion":
                        Direccion = new Direccion { ID_Direccion = (int)(NSNumber)data.ValueForKey(key) };
                        break;
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID cliente.</param>
        /// <param name="nombre">Nombre cliente.</param>
        /// <param name="apellidos">Apellidos cliente.</param>
        /// <param name="tel">Telefono cliente.</param>
        /// <param name="direccion">Direccion cliente.</param>
        public Cliente(int id, string nombre, string apellidos, string tel, Direccion direccion)
        {
            ID_Cliente = id;
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = tel;
            Direccion = direccion;
        }

        /// <summary>
        /// Obtengo direccion cliente.
        /// </summary>
        public void GetDireccionData()
        {
            // Obtengo direccion de SQLite
            Direccion d = null;

            // Compruebo datos
            if (d == null)
            {
                // Obtengo direccion del servidor
                RestManager.Connection().GetData((int)URIS.GetDireccionCliente, new string[] { ID_Cliente.ToString() }, null, (arg) =>
                {
                    // Compruebo datos
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        // Deserializo JSON
                        NSDictionary data = (NSDictionary)NSJsonSerialization.Deserialize(arg, 0, out NSError e);

                        // Cargo datos
                        d = Direccion = new Direccion(data);

                        // Guardo en SQLite
                        SQLiteManager.Connection().SetDireccion(d);
                    }

                    // Continuo
                    lock (l)
                    {
                        Monitor.Pulse(l);
                    }
                });

                // Espero a obtener datos
                lock (l)
                {
                    Monitor.Wait(l);
                }
            }

            // Obtengo datos ciudad
            d.GetDataCiudad();

            // Cargo direccion
            Direccion = d;
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
            j += JSONManager.GetFormat("nombre", Nombre) + ",";
            j += JSONManager.GetFormat("apellidos", Apellidos) + ",";
            j += JSONManager.GetFormat("telefono", Telefono) + ",";
            j += JSONManager.GetFormat("id_direccion", Direccion.ID_Direccion);

            // Cierro y devuelvo JSON
            return j += "}";
        }
    }
}