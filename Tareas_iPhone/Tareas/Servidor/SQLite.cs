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
            string cod = "CREATE TABLE pais (ID INT NOT NULL, N VARCHAR(255) NOT NULL, PRIMARY KEY(ID));"
                + "CREATE TABLE provincia (ID INT NOT NULL, IDP INT NOT NULL, N VARCHAR(255) NOT NULL, PRIMARY KEY (ID), FOREIGN KEY (IDP) REFERENCES pais (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE ciudad (ID INT NOT NULL, IDP INT NOT NULL, N VARCHAR(255) NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (IDP) REFERENCES provincia (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE direccion (ID INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, DIRECCION VARCHAR(255) NOT NULL, NUMERO VARCHAR(20), CP VARCHAR(20), IDC INT NOT NULL, LAT DOUBLE, LNG DOUBLE, PRIMARY KEY(ID), FOREIGN KEY (IDC) REFERENCES ciudad (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE cliente (ID INT NOT NULL, NOMBRE VARCHAR(255) NOT NULL, APELLIDOS VARCHAR(255), TELEFONO VARCHAR(255) NOT NULL, DIRECCION INT NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (DIRECCION) REFERENCES direccion (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE tarea (ID INT NOT NULL, IDC INT NOT NULL, TIPO INT NOT NULL, DIRECCION INT, URGENTE INT NOT NULL, OP2 INT NOT NULL, NOTAS VARCHAR(4000), CREACION TIMESTAMP NOT NULL, PRIMARY KEY(ID), FOREIGN KEY (IDC) REFERENCES cliente (ID) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY (DIRECCION) REFERENCES direccion (ID) ON DELETE CASCADE ON UPDATE CASCADE);"
                + "CREATE TABLE tarea_realizada (ID INT NOT NULL, IDU INT NOT NULL, FECHA TIMESTAMP NOT NULL, FOREIGN KEY (ID) REFERENCES tarea (ID) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY (IDU) REFERENCES usuario (ID) ON DELETE CASCADE ON UPDATE CASCADE)";

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
