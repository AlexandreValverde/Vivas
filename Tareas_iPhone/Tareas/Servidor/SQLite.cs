using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UIKit;

namespace Tareas.Servidor
{
    public static class SQLite
    {
        public static SQLiteManager Connection()
        {
            return (UIApplication.SharedApplication.Delegate as AppDelegate).scon;
        }
    }

    public class SQLiteManager
    {
        // Variables
        SqliteConnection connection;

        // Object
        private static readonly object l = new object();

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        public SQLiteManager()
        {
            string db = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tareas.db3");

            if (File.Exists(db))
            {
                connection = new SqliteConnection("Data Source=" + db);
            }
        }

        /// <summary>
        /// Creo base de datos SQLite.
        /// </summary>
        /// <param name="cod">Codigo de creacion de la base de datos.</param>
        public void CreateDB(string cod)
        {
            // Path
            string db = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tareas.db3");

            // Elimino base de datos (en caso de que exista)
            File.Delete(db);

            // Creo base de datos y conecto
            SqliteConnection.CreateFile(db);
            connection = new SqliteConnection("Data Source=" + db);

            lock (l)
            {
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

            // Compruebo que se creo correctamente
            if (GetPreference(0) != 1)
            {
                Console.WriteLine("La base de datos SQLite no se ha podido crear");
                File.Delete(db);
            }
            else
            {
                Console.WriteLine("Base de datos SQLite creada");
            }
        }

        /// <summary>
        /// Obtengo version de la base de datos SQLite.
        /// </summary>
        /// <returns>La version de la base de datos o 0 si no existe.</returns>
        public int GetVersion()
        {
            // Path
            string db = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tareas.db3");

            // Compruebo si existe
            if (!File.Exists(db))
            {
                return 0;
            }

            // Obtengo version local
            int v = 0;
            lock (l)
            {
                using (SqliteCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    cmd.CommandText = "SELECT URI FROM uris WHERE ID = 0";

                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        // Lee fila
                        if (reader.Read())
                        {
                            v = int.Parse((string)reader[0]);
                        }
                    }
                }

                // Cierro conexion
                connection.Close();
            }

            // Devuelvo version
            return v;
        }

        /// <summary>
        /// Actualizo la base de datos SQLite.
        /// </summary>
        /// <param name="cod">Codigo de actualizacion.</param>
        public void UpdateDB(string cod)
        {
            lock (l)
            {
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

            // Compruebo que se creo correctamente
            if (GetPreference(0) != 1)
            {
                throw new Exception();
            }
        }
        #endregion

        #region Preferences
        /// <summary>
        /// Actualiza el estado de la preferencia.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="active">0 desactivada, 1 activa</param>
        public void SetPreference(int id, int active)
        {
            try
            {
                lock (l)
                {
                    // Abro conexion
                    connection.Open();

                    // Actualizo datos
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE preferences SET ACTIVE = @P1 WHERE ID = @P2";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", active);
                        cmd.Parameters.AddWithValue("@P2", id);
                        cmd.ExecuteNonQuery();
                    }

                    // Cierro conexion
                    connection.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No se ha podido guardar la preferencia");
            }
        }

        /// <summary>
        /// Comprueba el estado de la preferencia.
        /// </summary>
        /// <returns>Codigo preferencia.</returns>
        /// <param name="id">Identifier.</param>
        public int GetPreference(int id)
        {
            // Preferencia
            int pref = 0;

            // Compruebo
            try
            {
                lock (l)
                {
                    using (SqliteCommand cmd = connection.CreateCommand())
                    {
                        connection.Open();
                        cmd.CommandText = "SELECT ACTIVE FROM preferences WHERE ID = @P1";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@P1", id);
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            // Lee fila
                            if (reader.Read())
                            {
                                pref = (byte)reader[0];
                            }
                        }
                    }

                    // Cierro conexion
                    connection.Close();
                }
            }
            catch (Exception)
            {
                return pref;
            }

            // Devuelvo url
            return pref;
        }
        #endregion
    }
}
