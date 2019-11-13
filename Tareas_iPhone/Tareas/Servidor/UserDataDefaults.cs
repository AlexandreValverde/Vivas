using Foundation;

namespace Tareas.Servidor
{
    public static class UserDataDefaults
    {
        #region Login
        /// <summary>
        /// Guarda los datos para loguearse del usuario actual.
        /// </summary>
        /// <param name="user">Email.</param>
        /// <param name="pass">Password.</param>
        public static void SetLoginData(string user, string pass)
        {
            NSUserDefaults.StandardUserDefaults.SetString(user, "UserEmail");
            NSUserDefaults.StandardUserDefaults.SetString(pass, "UserSerial");
        }

        /// <summary>
        /// Guarda el ID del usuario.
        /// </summary>
        /// <param name="id">ID del usuario.</param>
        public static void SetUserID(string id)
        {
            NSUserDefaults.StandardUserDefaults.SetString(id, "UserID");
        }

        /// <summary>
        /// Obtiene los datos para loguearse el usuario actual.
        /// </summary>
        /// <returns>Email y password.</returns>
        public static string[] GetLoginData()
        {
            // Datos
            string user, pass;

            // Obtengo usuario
            user = NSUserDefaults.StandardUserDefaults.StringForKey("UserEmail");

            // Obtengo password
            pass = NSUserDefaults.StandardUserDefaults.StringForKey("UserSerial");

            // Devuelvo datos
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                return new string[] { user, pass };
            }
            return null;
        }

        /// <summary>
        /// Obtiene el ID del usuario.
        /// </summary>
        /// <returns>ID usuario.</returns>
        public static int GetUserID()
        {
            return int.Parse(NSUserDefaults.StandardUserDefaults.StringForKey("UserID"));
        }
        #endregion

        #region Token
        /// <summary>
        /// Guarda el token del usuario.
        /// </summary>
        /// <param name="token">Token del usuario.</param>
        public static void SetToken(string token)
        {
            NSUserDefaults.StandardUserDefaults.SetString(token, "Token");
        }

        /// <summary>
        /// Obtiene token del usuario.
        /// </summary>
        /// <returns>Token del usuario.</returns>
        public static string GetToken()
        {
            return NSUserDefaults.StandardUserDefaults.StringForKey("Token");
        }
        #endregion

        #region Preferencias
        /// <summary>
        /// Guarda preferencias usuario.
        /// </summary>
        /// <param name="order">Orden de tareas.</param>
        /// <param name="mostrar">Mostrar (completadas).</param>
        /// <param name="tipo">Tipo de tareas.</param>
        public static void SetPreferences(int order, int mostrar, int tipo)
        {
            NSUserDefaults.StandardUserDefaults.SetString(order.ToString(), "Order");
            NSUserDefaults.StandardUserDefaults.SetString(mostrar.ToString(), "Mostrar");
            NSUserDefaults.StandardUserDefaults.SetString(tipo.ToString(), "Tipo");
        }

        /// <summary>
        /// Obtengo preferencias usuario.
        /// </summary>
        /// <returns>Preferencias.</returns>
        public static int[] GetPreferences()
        {
            // Preferencias
            int[] p = new int[3];

            // Obtengo preferencias
            p[0] = int.Parse(NSUserDefaults.StandardUserDefaults.StringForKey("Order"));
            p[1] = int.Parse(NSUserDefaults.StandardUserDefaults.StringForKey("Mostrar"));
            p[2] = int.Parse(NSUserDefaults.StandardUserDefaults.StringForKey("Tipo"));

            // Devuelvo
            return p;
        }
        #endregion
    }
}
