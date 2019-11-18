using System;
using System.Threading;
using Foundation;
using Tareas.Localizacion;
using Tareas.Servidor;

namespace Tareas.Clases
{
    public class Tarea
    {
        // Variables
        public int ID_Tarea { set; get; }
        public Cliente Cliente { set; get; }
        public bool Tipo { set; get; }
        public Direccion Direccion { set; get; }
        public bool Urgente { set; get; }
        public bool OP2 { set; get; }
        public string Notas { set; get; }
        public string Fecha_creacion { set; get; }
        public int ID_Usuario { set; get; }
        public string Fecha_realizada { set; get; }

        // Conexion servidor
        private static readonly object l = new object();

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Tarea()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Datos JSON.</param>
        public Tarea(NSDictionary data)
        {
            // Leo datos
            foreach (NSString key in data.Keys)
            {
                switch (key.ToString().ToLower())
                {
                    case "id_tarea":
                        ID_Tarea = (int)(NSNumber)data.ValueForKey(key);
                        break;
                    case "id_cliente":
                        Cliente = new Cliente((int)(NSNumber)data.ValueForKey(key));
                        break;
                    case "tipo":
                        Tipo = (bool)(NSNumber)data.ValueForKey(key);
                        break;
                    case "id_direccion":
                        Direccion = new Direccion { ID_Direccion = (int)(NSNumber)data.ValueForKey(key) };
                        break;
                    case "urgente":
                        Tipo = (bool)(NSNumber)data.ValueForKey(key);
                        break;
                    case "op2":
                        OP2 = (bool)(NSNumber)data.ValueForKey(key);
                        break;
                    case "notas":
                        Notas = data.ValueForKey(key).ToString();
                        break;
                    case "fecha_creacion":
                        Fecha_creacion = data.ValueForKey(key).ToString();
                        break;
                    case "id_usuario":
                        ID_Usuario = (int)(NSNumber)data.ValueForKey(key);
                        break;
                    case "fecha_realizada":
                        Fecha_realizada = data.ValueForKey(key).ToString();
                        break;
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">ID tarea.</param>
        /// <param name="cliente">Cliente.</param>
        /// <param name="tipo">Tipo de tarea.</param>
        /// <param name="direccion">Direccion tarea.</param>
        /// <param name="urgente">Urgente.</param>
        /// <param name="op2">Segunda opcion.</param>
        /// <param name="notas">Notas.</param>
        /// <param name="fecha_creacion">Fecha de creacion de la tarea.</param>
        public Tarea(int id, Cliente cliente, bool tipo, Direccion direccion, bool urgente, bool op2, string notas, string fecha_creacion)
        {
            ID_Tarea = id;
            Cliente = cliente;
            Tipo = tipo;
            Direccion = direccion;
            Urgente = urgente;
            OP2 = op2;
            Notas = notas;
            Fecha_creacion = fecha_creacion;
        }

        /// <summary>
        /// Obtiene los datos de la direccion de la tarea.
        /// </summary>
        public void GetDireccionData()
        {
            // Direccion
            Direccion d;

            // Direccion tarea
            if (Direccion.ID_Direccion != 0)
            {
                // Obtengo direccion de SQLite
                d = SQLiteManager.Connection().GetDireccion(Direccion.ID_Direccion);

                // Compruebo datos
                if (d == null)
                {
                    // Obtengo direccion del servidor
                    RestManager.Connection().GetData((int)URIS.GetDireccion, new string[] { Direccion.ID_Direccion.ToString() }, null, (arg) =>
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
            }
            // Direccion cliente
            else
            {
                // Obtengo direccion
                d = SQLiteManager.Connection().GetDireccionCliente(Cliente.ID_Cliente);

                // Compruebo
                if (d == null)
                {
                    // Obtengo datos del servidor
                    RestManager.Connection().GetData((int)URIS.GetDireccionCliente, new string[] { Cliente.ID_Cliente.ToString() }, null, (arg) =>
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
            }

            // Obtengo datos ciudad
            d.GetDataCiudad();

            // Cargo direccion
            Direccion = d;
        }

        /// <summary>
        /// Obtiene datos cliente.
        /// </summary>
        public void GetClienteData()
        {
            // Cliente
            Cliente c;

            // Obtengo datos servidor
            RestManager.Connection().GetData((int)URIS.GetCliente, new string[] { Cliente.ID_Cliente.ToString() }, null, (arg) =>
            {
                // Depuracion
                Console.WriteLine(arg ?? "null");

                // Compruebo datos
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    // Deserializo JSON
                    NSDictionary data = (NSDictionary)NSJsonSerialization.Deserialize(arg, 0, out NSError e);

                    // Cargo datos
                    c = new Cliente(data);
                    Cliente = c;
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

        /// <summary>
        /// Obtiene el nombre de usuario que creo la tarea.
        /// </summary>
        public void GetUserCompletada()
        {
            // Obtiene ID usuario
            ID_Usuario = SQLiteManager.Connection().GetUserCompletada(ID_Tarea);
        }

        /// <summary>
        /// Obtiene la distancia entre la tarea y el usuario actual.
        /// </summary>
        /// <returns>Distancia.</returns>
        public double GetDistancia()
        {
            // Coordenadas
            Coordenadas coordsUser, coordsTarea;

            // Obtiene coordenadas usuario
            coordsUser = LocationManager.GetCoords();
            if (coordsUser == null) return -1;

            // Coordenadas tarea
            coordsTarea = Direccion.Coords ?? Cliente.Direccion.Coords;
            if (coordsTarea == null) return -1;

            // Obtengo distancia
            double lats = coordsUser.Latitud - coordsTarea.Latitud;
            double lngs = coordsUser.Longitud - coordsTarea.Longitud;
            double latm = lats * 60 * 1852;
            double lngm = (lngs * Math.Cos(coordsUser.Latitud * Math.PI / 180)) * 60 * 1852;
            double distInMeters = Math.Sqrt(Math.Pow(latm, 2) + Math.Pow(lngm, 2));

            // Devuelvo distancia en Kms
            return distInMeters / 1000;
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
            j += JSONManager.GetFormat("id_cliente", Cliente.ID_Cliente) + ",";
            j += JSONManager.GetFormat("tipo", Tipo) + ",";
            j += JSONManager.GetFormat("id_direccion", Cliente.Direccion.ID_Direccion) + ",";
            j += JSONManager.GetFormat("urgente", Urgente) + ",";
            j += JSONManager.GetFormat("op2", OP2) + ",";
            j += JSONManager.GetFormat("notas", Notas);

            // Cierro y devuelvo JSON
            return j += "}";
        }
    }
}
