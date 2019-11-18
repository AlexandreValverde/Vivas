using System.Collections.Generic;
using Foundation;

namespace Tareas.Clases
{
    public static class JSONManager
    {
        /// <summary>
        /// Deserializa tareas.
        /// </summary>
        /// <param name="json">JSON con datos.</param>
        /// <returns>Tareas.</returns>
        public static Tarea[] DeserializeTareas(string json)
        {
            // Tareas
            List<Tarea> tareas = null;

            // Deserializo JSON
            NSArray data = (NSArray)NSJsonSerialization.Deserialize(json, 0, out NSError e);

            // Compruebo datos
            if (data != null)
            {
                // Inicializo lista
                tareas = new List<Tarea>();

                // Recorro tareas
                for (uint i = 0; i < data.Count; i++)
                {
                    // Creo y añado
                    tareas.Add(new Tarea(data.GetItem<NSDictionary>(i)));
                }
            }

            // Devuelvo datos
            return tareas.ToArray();
        }

        /// <summary>
        /// Obtiene datos en formato "key":"value".
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Datos en formato JSON.</returns>
        public static string GetFormat(string key, string value)
        {
            return string.Format("\"{0}\":\"{1}\"", key, value);
        }

        /// <summary>
        /// Obtiene datos en formato "key":value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Datos en formato JSON.</returns>
        public static string GetFormat(string key, int value)
        {
            return string.Format("\"{0}\":{1}", key, value);
        }

        /// <summary>
        /// Obtiene datos en formato "key":value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Datos en formato JSON.</returns>
        public static string GetFormat(string key, bool value)
        {
            return string.Format("\"{0}\":{1}", key, value.ToString().ToLower());
        }

        /// <summary>
        /// Obtiene datos en formato "key":value.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <returns>Datos en formato JSON.</returns>
        public static string GetFormat(string key, double value)
        {
            return string.Format("\"{0}\":{1}", key, value);
        }
    }
}