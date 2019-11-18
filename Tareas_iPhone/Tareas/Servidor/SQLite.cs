using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using Tareas.Clases;
using UIKit;

namespace Tareas.Servidor
{
    public class SQLite
    {
        // Variables
        SqliteConnection connection;

        // Bloqueo
        private static readonly object l = new object();

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public SQLite()
        {
            string db = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tareas.db3");

            if (File.Exists(db))
            {
                connection = new SqliteConnection("Data Source=" + db);
            }
            else
            {
                CreateDB();
            }
        }

        /// <summary>
        /// Creo base de datos SQLite.
        /// </summary>
        private void CreateDB()
        {
            // Path
            string db = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tareas.db3");

            // Elimino base de datos (en caso de que exista)
            File.Delete(db);

            // Creo base de datos y conecto
            SqliteConnection.CreateFile(db);
            connection = new SqliteConnection("Data Source=" + db);

            // Codigo creacion base de datos
            string cod = "CREATE TABLE pais (ID INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, PRIMARY KEY(ID));"
                + "CREATE TABLE provincia (ID INT NOT NULL, IDP INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, PRIMARY KEY (ID), FOREIGN KEY (IDP) REFERENCES pais (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE ciudad (ID INT NOT NULL, IDP INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (IDP) REFERENCES provincia (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE direccion (ID INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, DIRECCION VARCHAR(255) NOT NULL, NUMERO VARCHAR(20), CP VARCHAR(20), IDC INT NOT NULL, LAT DOUBLE, LNG DOUBLE, PRIMARY KEY(ID), FOREIGN KEY (IDC) REFERENCES ciudad (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE cliente (ID INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, APELLIDOS VARCHAR(255), TELEFONO VARCHAR(255) NOT NULL, DIRECCION INT NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (DIRECCION) REFERENCES direccion (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE tarea (ID INT NOT NULL, IDC INT NOT NULL, TIPO INT NOT NULL, DIRECCION INT, URGENTE INT NOT NULL, OP2 INT NOT NULL, NOTAS VARCHAR(4000), CREACION TIMESTAMP NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (IDC) REFERENCES cliente (ID) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY (DIRECCION) REFERENCES direccion (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE tarea_realizada (ID_TAREA INT NOT NULL, ID_USUARIO INT NOT NULL, FOREIGN KEY (ID_TAREA) REFERENCES tarea (ID) ON DELETE CASCADE ON UPDATE CASCADE)";

            // Abro conexion
            connection.Open();

            // Añado tablas y datos
            foreach (string cmd in cod.Split(';'))
            {
                using (SqliteCommand c = connection.CreateCommand())
                {
                    c.CommandText = cmd;
                    c.CommandType = CommandType.Text;
                    c.ExecuteNonQuery();
                }
            }

            // Cierro conexion
            connection.Close();
        }
        #endregion

        #region Tareas
        /// <summary>
        /// Guardo tarea.
        /// </summary>
        /// <param name="t">Tarea.</param>
        public void SetTarea(Tarea t)
        {
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Guardo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO tarea VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8);";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", t.ID_Tarea);
                        cmd.Parameters.AddWithValue("@P2", t.Cliente.ID_Cliente);
                        cmd.Parameters.AddWithValue("@P3", t.Tipo);
                        cmd.Parameters.AddWithValue("@P4", t.Direccion.ID_Direccion);
                        cmd.Parameters.AddWithValue("@P5", t.Urgente);
                        cmd.Parameters.AddWithValue("@P6", t.OP2);
                        cmd.Parameters.AddWithValue("@P7", t.Notas);
                        cmd.Parameters.AddWithValue("@P8", t.Fecha_creacion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();

            // Tarea realizada
            if (t.ID_Usuario != 0)
            {
                try
                {
                    // Bloqueo
                    lock (l)
                    {
                        // Abro conexion
                        connection.Open();

                        // Guardo datos
                        using (SqliteCommand cmd = connection.CreateCommand())
                        {
                            cmd.CommandText = "INSERT INTO tarea_realizada VALUES (@P1, @P2);";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@P1", t.ID_Tarea);
                            cmd.Parameters.AddWithValue("@P2", t.ID_Usuario);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                }

                // Cierro conexion
                connection.Close();
            }
        }

        /// <summary>
        /// Obtiene las tareas de la base de datos.
        /// </summary>
        /// <returns>Tareas.</returns>
        public Tarea[] GetTareas()
        {
            // Tareas
            List<Tarea> tareas = new List<Tarea>();

            // Obtengo preferencias
            Preferencias p = UserDataDefaults.GetPreferences();

            // Consulta
            string c = "SELECT * FROM tarea";
            bool flagWhere = false;

            // Mostrar
            switch (p.Mostrar)
            {
                case 1: // Realizadas
                    c += " WHERE EXISTS(SELECT NULL FROM tarea_realizada WHERE ID_TAREA = tarea.ID)";
                    flagWhere = true;
                    break;
                case 2: // Sin realizar
                    c += " WHERE NOT EXISTS(SELECT NULL FROM tarea_realizada WHERE ID_TAREA = tarea.ID)";
                    flagWhere = true;
                    break;
            }

            // Tipo
            switch (p.Tipo)
            {
                case 1: // Urgente
                    if (flagWhere)
                    {
                        c += " AND EXISTS(SELECT TRUE FROM tarea WHERE URGENTE = 1 OR OP2 = 1)";
                    }
                    else
                    {
                        c += " WHERE EXISTS(SELECT TRUE FROM tarea WHERE URGENTE = 1 OR OP2 = 1)";
                    }
                    break;
                case 2: // Normal
                    if (flagWhere)
                    {
                        c += " AND EXISTS(SELECT TRUE FROM tarea WHERE URGENTE = 0 AND OP2 = 0)";
                    }
                    else
                    {
                        c += " WHERE EXISTS(SELECT TRUE FROM tarea WHERE URGENTE = 0 AND OP2 = 0)";
                    }
                    break;
            }

            // Orden
            c += " ORDER BY CREACION " + (p.Orden == 0 ? "ASC;" : "DESC;");

            // Conexion
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = c;
                        cmd.CommandType = CommandType.Text;

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            while (reader.Read())
                            {
                                tareas.Add(new Tarea
                                {
                                    ID_Tarea = (int)reader[0],
                                    Cliente = new Cliente { ID_Cliente = (int)reader[1] },
                                    Tipo = (int)reader[2] == 1,
                                    Direccion = new Direccion { ID_Direccion = (int)reader[3] },
                                    Urgente = (int)reader[4] == 1,
                                    OP2 = (int)reader[5] == 1,
                                    Notas = (string)reader[6],
                                    Fecha_creacion = DatetimeToTimestamp((DateTime)reader[7])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo tareas
            return tareas.Count == 0 ? null : tareas.ToArray();
        }

        /// <summary>
        /// Obtiene el ID del usuario que completo la tarea.
        /// </summary>
        /// <param name="idTarea">ID de la tarea.</param>
        /// <returns>ID usuario.</returns>
        public int GetUserCompletada(int idTarea)
        {
            // ID usuario
            int idUsuario = 0;

            // Conexion
            try
            {
                // Bloqueo
                lock (l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT ID_USUARIO FROM tarea_realizada WHERE ID_TAREA = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", idTarea);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            if (reader.Read())
                            {
                                idUsuario = (int)reader[0];
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo
            return idUsuario;
        }

        /// <summary>
        /// Elimina todas las tareas.
        /// </summary>
        public void DeleteTareas()
        {
            try
            {
                // Bloqueo
                lock (l)
                {
                    // Abro conexion
                    connection.Open();

                    // Guardo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM tarea";
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();
        }
        #endregion

        #region Clientes
        /// <summary>
        /// Guardo el cliente.
        /// </summary>
        /// <param name="c">Cliente.</param>
        public void SetCliente(Cliente c)
        {
            try
            {
                // Bloqueo
                lock (l)
                {
                    // Abro conexion
                    connection.Open();

                    // Guardo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        //+"CREATE TABLE cliente (ID INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, APELLIDOS VARCHAR(255), TELEFONO VARCHAR(255) NOT NULL, DIRECCION INT NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (DIRECCION) REFERENCES direccion (ID) ON DELETE CASCADE ON UPDATE CASCADE);"

                        cmd.CommandText = "INSERT INTO cliente VALUES (@P1, @P2, @P3, @P4, @P5);";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", c.ID_Cliente);
                        cmd.Parameters.AddWithValue("@P2", c.Nombre);
                        cmd.Parameters.AddWithValue("@P3", c.Apellidos);
                        cmd.Parameters.AddWithValue("@P4", c.Telefono);
                        cmd.Parameters.AddWithValue("@P5", c.Direccion.ID_Direccion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();
        }

        /// <summary>
        /// Obtengo datos cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Cliente GetCliente(int id)
        {
            // Cliente
            Cliente c = null;

            // Conexion
            try
            {
                // Bloqueo
                lock (l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM cliente WHERE ID = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            while (reader.Read())
                            {
                                c = new Cliente
                                {
                                    ID_Cliente = (int)reader[0],
                                    Nombre = (string)reader[1],
                                    Apellidos = (string)reader[2],
                                    Telefono = (string)reader[3],
                                    Direccion = new Direccion { ID_Direccion = (int)reader[4] }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo cliente
            return c;
        }

        /// <summary>
        /// Obtengo la direccion del cliente.
        /// </summary>
        /// <param name="id">ID direccion.</param>
        /// <returns>Direccion.</returns>
        public Direccion GetDireccionCliente(int id)
        {
            // ID direccion
            int idDireccion;

            // Conexion
            try
            {
                // Bloqueo
                lock (l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT DIRECCION FROM cliente WHERE ID = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            if (reader.Read())
                            {
                                idDireccion = (int)reader[0];
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo direccion
            return id != 0 ? GetDireccion(id) : null;
        }
        #endregion

        #region Lugares
        /// <summary>
        /// Guarda direccion.
        /// </summary>
        /// <param name="d">Direccion.</param>
        public void SetDireccion(Direccion d)
        {
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO direccion VALUES (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8);";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", d.ID_Direccion);
                        cmd.Parameters.AddWithValue("@P2", d.Nombre_Direccion);
                        cmd.Parameters.AddWithValue("@P3", d.Calle);
                        cmd.Parameters.AddWithValue("@P4", d.Numero);
                        cmd.Parameters.AddWithValue("@P5", d.Codigo_Postal);
                        cmd.Parameters.AddWithValue("@P6", d.ID_Ciudad);
                        cmd.Parameters.AddWithValue("@P7", d.Coords.Latitud);
                        cmd.Parameters.AddWithValue("@P8", d.Coords.Longitud);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();
        }

        /// <summary>
        /// Guardo ciudad.
        /// </summary>
        /// <param name="c">Ciudad.</param>
        public void SetCiudad(Ciudad c)
        {
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Guardo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO ciudad VALUES (@P1, @P2, @P3);";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", c.ID_Ciudad);
                        cmd.Parameters.AddWithValue("@P2", c.ID_Provincia);
                        cmd.Parameters.AddWithValue("@P3", c.Nombre_Ciudad);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();
        }

        /// <summary>
        /// Guardo provincia.
        /// </summary>
        /// <param name="p">Provincia.</param>
        public void SetProvincia(Provincia p)
        {
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Guardo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO provincia VALUES (@P1, @P2, @P3);";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", p.ID_Provincia);
                        cmd.Parameters.AddWithValue("@P2", p.ID_Pais);
                        cmd.Parameters.AddWithValue("@P3", p.Nombre_Provincia);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();
        }

        /// <summary>
        /// Guardo pais.
        /// </summary>
        /// <param name="p">Pais.</param>
        public void SetPais(Pais p)
        {
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Guardo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO pais VALUES (@P1, @P2);";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", p.ID_Pais);
                        cmd.Parameters.AddWithValue("@P2", p.Nombre_Pais);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }

            // Cierro conexion
            connection.Close();
        }

        /// <summary>
        /// Obtiene la direccion seleccionada.
        /// </summary>
        /// <param name="id">ID direccion.</param>
        /// <returns>Direccion.</returns>
        public Direccion GetDireccion(int id)
        {
            // Direccion
            Direccion d = null;

            // Conexion
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Creo consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM direccion WHERE ID = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            if (reader.Read())
                            {
                                d = new Direccion
                                {
                                    ID_Direccion = (int)reader[0],
                                    Nombre_Direccion = (string)reader[1],
                                    Calle = (string)reader[2],
                                    Numero = (string)reader[3],
                                    Codigo_Postal = (string)reader[4],
                                    ID_Ciudad = (int)reader[5],
                                    Coords = new Coordenadas((double)reader[6], (double)reader[7])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo direccion
            return d;
        }

        /// <summary>
        /// Obtiene la ciuadad seleccionada.
        /// </summary>
        /// <param name="id">ID ciudad.</param>
        /// <returns>Ciudad.</returns>
        public Ciudad GetCiudad(int id)
        {
            // Ciudad
            Ciudad c = null;

            // Conexion
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Creo consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT IDP, NOMBRE FROM ciudad WHERE ID = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            if (reader.Read())
                            {
                                c = new Ciudad
                                {
                                    ID_Ciudad = id,
                                    ID_Provincia = (int)reader[0],
                                    Nombre_Ciudad = (string)reader[1]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo ciudad
            return c;
        }

        /// <summary>
        /// Obtiene provincia seleccionada.
        /// </summary>
        /// <param name="id">ID provincia.</param>
        /// <returns>Provincia.</returns>
        public Provincia GetProvincia(int id)
        {
            // Provincia
            Provincia p = null;

            // Conexion
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT IDP, NOMBRE FROM provincia WHERE ID = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            if (reader.Read())
                            {
                                p = new Provincia
                                {
                                    ID_Provincia = id,
                                    ID_Pais = (int)reader[0],
                                    Nombre_Provincia = (string)reader[1]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo provincia
            return p;
        }

        /// <summary>
        /// Obtiene el pais seleccionado.
        /// </summary>
        /// <param name="id">ID pais.</param>
        /// <returns>Pais.</returns>
        public Pais GetPais(int id)
        {
            // Pais
            Pais p = null;

            // Conexion
            try
            {
                // Bloqueo
                lock(l)
                {
                    // Abro conexion
                    connection.Open();

                    // Crea consulta
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT NOMBRE FROM pais WHERE ID = @P1;";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);

                        // Realiza consulta
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee datos
                            if (reader.Read())
                            {
                                p = new Pais
                                {
                                    ID_Pais = id,
                                    Nombre_Pais = (string)reader[0],
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            // Cierro conexion
            connection.Close();

            // Devuelvo pais
            return p;
        }
        #endregion

        #region Logout
        /// <summary>
        /// Elimina la base de datos.
        /// </summary>
        public void Eliminar()
        {
            // Path
            string db = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tareas.db3");

            // Elimino
            File.Delete(db);
        }
        #endregion

        #region Utilidades
        /// <summary>
        /// Convierte datetime a string.
        /// </summary>
        /// <param name="date">String.</param>
        /// <returns></returns>
        private string DatetimeToTimestamp(DateTime date)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }
        #endregion
    }

    public static class SQLiteManager
    {
        /// <summary>
        /// Obtengo conexion SQLite.
        /// </summary>
        /// <returns></returns>
        public static SQLite Connection()
        {
            return (UIApplication.SharedApplication.Delegate as AppDelegate).scon;
        }
    }
}