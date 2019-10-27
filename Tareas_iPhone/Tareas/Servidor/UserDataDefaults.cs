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
        /// Obtiene los datos para loguearse el usuario actual.
        /// </summary>
        /// <returns>Email y password.</returns>
        public static string[] GetLoginData()
        {
            // Datos
            string user, pass;

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
    }
    #endregion
}
