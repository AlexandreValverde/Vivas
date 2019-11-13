namespace Tareas.Servidor
{
    public static class CrearJSON
    {
        /// <summary>
        /// Creo JSON.
        /// </summary>
        /// <param name="data">Datos.</param>
        /// <returns>JSON.</returns>
        public static string crear(string[] data)
        {
            // Compruebo si es null
            if (data == null)
            {
                return null;
            }

            // JSON
            string j = "";

            // Corchetes
            if (data.Length > 1)
            {
                j = "[";
            }

            // Añado datos
            foreach (string d in data)
            {
                j += d + ",";
            }

            // Elimino ultima coma
            j = j.Substring(0, j.Length - 1);

            // Corchetes
            if (data.Length > 1)
            {
                j += "]";
            }

            // Devuelvo JSON creado
            return j;
        }
    }
}