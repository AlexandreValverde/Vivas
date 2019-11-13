﻿using System;
using System.Text;
using System.Threading;
using Foundation;
using Tareas;
using Tareas.Servidor;
using UIKit;

namespace Tareas.Servidor
{
    public class ServerConnection
    {
        #region Variables
        private static readonly object l = new object();
        private static readonly object d = new object();

        private URIData[] uris;
        public string token;
        private readonly string url = "http://192.168.0.35:31416/tareasvivas/app/";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor. Inicializa conexion con el servidor y comprueba que la base de datos SQLite esta creada y actualizada.
        /// </summary>
        public ServerConnection()
        {
            // Realizo conexiones
            Thread hilo = new Thread(() =>
            {
                lock (l)
                {
                    // Cargo URIS
                    uris = new URIData[]
                    {
                        new URIData("token/login?id=#&pass=#", 1, 1, 0, 0),
                        new URIData("users/login?email=#&pass=#", 1, 1, 0, 0)
                    };

                    // Obtengo token
                    GetToken();
                }
            })
            {
                Priority = ThreadPriority.Highest
            };
            hilo.Start();
        }
        #endregion

        #region Token
        /// <summary>
        /// Obtiene el token del usuario.
        /// </summary>
        public void GetToken()
        {
            // Obtiene token guardado en local
            token = UserDataDefaults.GetToken();

            // Comprueba
            if (token == null)
            {
                GetTokenFromServer();
            }
        }

        /// <summary>
        /// Obtiene el token del servidor.
        /// </summary>
        public void GetTokenFromServer()
        {
            // Variables
            string[] userData = null;
            URIData uri = null;

            // Action
            Action<bool, string> complete = null;

            // Inicializo
            token = "";

            // Obtengo datos usuario
            userData = UserDataDefaults.GetLoginData();

            // Preparo conexion
            if (userData != null)
            {
                // URI
                uri = uris[(int)URIS.GetToken];

                // Añado datos
                uri.AddData(userData);

                // Action
                complete += (arg1, arg2) =>
                {
                    // Compruebo conexion
                    if (arg1)
                    {
                        // Compruebo datos
                        if (!string.IsNullOrWhiteSpace(arg2))
                        {
                            // Guardo token
                            token = arg2;

                            // Guardo token en local
                            UserDataDefaults.SetToken(arg2);
                        }
                    }
                    lock (d)
                    {
                        Monitor.Pulse(d);
                    }
                };

                // Obtengo datos del servidor
                Connection(uri, complete);

                // Espero a que finalice
                lock (d)
                {
                    Monitor.Wait(d);
                }
            }
        }
        #endregion

        #region Conexion
        /// <summary>
        /// Crea consulta rest.
        /// </summary>
        /// <returns>Request.</returns>
        /// <param name="uri">URI data.</param>
        private NSMutableUrlRequest CreateRequest(URIData uri)
        {
            // Convierto a UTF8
            string converted = ((NSString)(url + uri.Url)).CreateStringByAddingPercentEscapes(NSStringEncoding.UTF8);

            // Method
            NSMutableUrlRequest request = new NSMutableUrlRequest(NSUrl.FromString(converted))
            {
                HttpMethod = uri.GetMethod()
            };

            // Accept
            if (uri.GetAccept() != null)
            {
                request["Accept"] = uri.GetAccept();
            }

            // Content-Type
            if (uri.GetContentType() != null)
            {
                request["Content-Type"] = uri.GetContentType();
            }

            // Authorization
            if (uri.Auth != 0)
            {
                request["Authorization"] = token;
            }

            // Body
            if (uri.Body != null)
            {
                request.Body = uri.Body;
            }

            // Devuelvo request
            return request;
        }

        /// <summary>
        /// Establece conexion con el servidor para obtener los datos.
        /// </summary>
        /// <param name="uri">URI.</param>
        /// <param name="complete">Action conexion finalizada.</param>
        private void Connection(URIData uri, Action<bool, string> complete)
        {
            using (NSUrlSession session = NSUrlSession.FromConfiguration(NSUrlSessionConfiguration.DefaultSessionConfiguration, (INSUrlSessionDelegate)new DataDelegate(complete), null))
            {
                NSUrlSessionDataTask task = session.CreateDataTask(CreateRequest(uri));
                task.Resume();
            }
        }

        /// <summary>
        /// Obtiene datos del servidor.
        /// </summary>
        /// <param name="id">ID de la URI.</param>
        /// <param name="data">Datos para crear la consulta.</param>
        /// <param name="complete">Conexion completada.</param>
        public void GetData(int id, string[] data, string[] bodyData, Action<string> complete)
        {
            // Reintento token
            bool flag = true;

            // URI
            URIData uri = null;

            // Obtengo uri
            uri = uris[id];

            // Compruebo que existe la URI
            if (uri != null)
            {
                // Request
                uri.AddData(data);

                // Body
                if (bodyData != null)
                {
                    if (bodyData.Length > 0)
                    {
                        uri.Body = CrearJSON.crear(bodyData);
                    }
                }

                // Depuracion
                Console.WriteLine("URI -> " + uri.Url);

                // Action
                Action<bool, string> actionConnection = null;
                actionConnection += (arg1, arg2) =>
                {
                    // Compruebo resultado conexion
                    if (arg1)
                    {
                        // Devuelvo datos obtenidos del servidor
                        complete(arg2);
                    }
                    else
                    {
                        // Compruebo si el token esta caducado
                        if ((arg2 ?? "null").Equals("401") && flag)
                        {
                            // Renuevo token
                            GetTokenFromServer();
                            flag = false;
                            Connection(uri, actionConnection);
                        }
                        else
                        {
                            // Conexion erronea
                            complete(null);
                        }
                    }
                };

                // Realizo conexion
                Connection(uri, actionConnection);
            }
        }
        #endregion
    }
}

public class DataDelegate : NSUrlSessionDataDelegate
{
    // Variables
    private readonly Action<bool, string> complete;
    private StringBuilder responseBuilder;
    private nint status_code;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="complete">Complete connection.</param>
    public DataDelegate(Action<bool, string> complete)
    {
        this.complete = complete;
        responseBuilder = new StringBuilder();
    }

    /// <summary>
    /// Obtengo codigo response.
    /// </summary>
    /// <param name="session">Session.</param>
    /// <param name="dataTask">Data task.</param>
    /// <param name="response">Response.</param>
    /// <param name="completionHandler">Completion handler.</param>
    public override void DidReceiveResponse(NSUrlSession session, NSUrlSessionDataTask dataTask, NSUrlResponse response, Action<NSUrlSessionResponseDisposition> completionHandler)
    {
        // Compruebo si es null
        if (!(response is NSHttpUrlResponse http_response))
        {
            status_code = -1;
            return;
        }

        // Obtengo codigo
        status_code = http_response.StatusCode;

        // Conexion
        completionHandler(status_code == 200 || status_code == 201 ? NSUrlSessionResponseDisposition.Allow : NSUrlSessionResponseDisposition.Cancel);
    }

    /// <summary>
    /// Obtengo datos del servidor.
    /// </summary>
    /// <param name="session">Session.</param>
    /// <param name="dataTask">Data task.</param>
    /// <param name="data">Data.</param>
    public override void DidReceiveData(NSUrlSession session, NSUrlSessionDataTask dataTask, NSData data)
    {
        if (data != null)
        {
            responseBuilder.Append(data.ToString());
        }
    }

    /// <summary>
    /// Conexion finalizada.
    /// </summary>
    /// <param name="session">Session.</param>
    /// <param name="task">Task.</param>
    /// <param name="error">Error.</param>
    public override void DidCompleteWithError(NSUrlSession session, NSUrlSessionTask task, NSError error)
    {
        if (error != null)
        {
            // Depuracion
            Console.WriteLine("Conexion con error -> " + status_code.ToString());

            complete(false, status_code.ToString());
        }
        else
        {
            // Depuracion
            Console.WriteLine("Conexion con exito -> " + responseBuilder);

            complete(true, responseBuilder.ToString());
        }
    }
}

public static class RestManager
{
    /// <summary>
    /// Inicializa la conexion con el servidor.
    /// </summary>
    public static void Init()
    {
        (UIApplication.SharedApplication.Delegate as AppDelegate).con = new ServerConnection();
    }

    /// <summary>
    /// Obtiene la conexion.
    /// </summary>
    /// <returns>The con.</returns>
    public static ServerConnection Connection()
    {
        return (UIApplication.SharedApplication.Delegate as AppDelegate).con;
    }
}