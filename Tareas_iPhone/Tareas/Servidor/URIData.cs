using Foundation;

namespace Tareas.Servidor
{
    public class URIData
    {
        // Propiedades
        public string Url { set; get; }
        public byte Method, Accept, ContentType;
        public byte Auth { set; get; }
        public NSData Body { set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="url">URL uri.</param>
        /// <param name="method">Method.</param>
        /// <param name="accept">Accept.</param>
        /// <param name="type">Content type.</param>
        /// <param name="auth">Autentificacion.</param>
        public URIData(string url, byte method, byte accept, byte type, byte auth)
        {
            Url = url;
            Method = method;
            Accept = accept;
            ContentType = type;
            Auth = auth;
        }

        /// <summary>
        /// Obtiene Method.
        /// </summary>
        /// <returns>Method string.</returns>
        public string GetMethod()
        {
            switch (Method)
            {
                case 1: return "GET";
                case 2: return "POST";
                case 3: return "PUT";
                case 4: return "DELETE";
                default: return null;
            }
        }

        /// <summary>
        /// Obtiene tipo de dato aceptado.
        /// </summary>
        /// <returns>Accept string.</returns>
        public string GetAccept()
        {
            switch (Accept)
            {
                case 1: return "text/plain";
                case 2: return "application/json";
                case 3: return "text/html";
                default: return null;
            }
        }

        /// <summary>
        /// Obtiene tipo de dato enviado en el body.
        /// </summary>
        /// <returns>ContentType string.</returns>
        public string GetContentType()
        {
            switch (ContentType)
            {
                case 1: return "text/plain";
                case 2: return "application/json";
                default: return null;
            }
        }

        /// <summary>
        /// Añado datos a la url.
        /// </summary>
        /// <returns>URI con datos añadidos.</returns>
        /// <param name="data">Datos.</param>
        public void AddData(string[] data)
        {
            // Variables
            string u = Url;

            // Compruebo url y datos
            if (data != null)
            {
                // Añado datos a la uri
                foreach (string d in data)
                {
                    if (u.Contains('#'))
                    {
                        u = u.Substring(0, u.IndexOf('#')) + (string.IsNullOrEmpty(d) ? "" : d) + u.Substring(u.IndexOf('#') + 1);
                    }
                }
            }

            // Elimino restantes
            Url = u.Replace("#", "");
        }
    }
}
