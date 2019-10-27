using System;
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
        private string token;
        private string urlHead;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor. Inicializa conexion con el servidor y comprueba que la base de datos SQLite esta creada y actualizada.
        /// </summary>
        /// <param name="complete">True conexion realizada correctamente, false no se ha podido conectar con el servidor.</param>
        public ServerConnection(Action<bool> complete)
        {
            // Realizo conexiones
            Thread hilo = new Thread(() =>
            {
                lock (l)
                {
                    bool connect = true;
                    while (connect)
                    {
                        try
                        {
                            // TODO: Obtengo URIs

                            // Obtengo token
                            token = "0";
                            GetToken();

                            // SQLite
                            int sqlv = SQLite.Connection().GetVersion();
                            if (sqlv == 0)
                            {
                                // Creo base de datos
                                CreateDB();
                            }
                            else
                            {
                                // Compruebo si esta actualizada
                                UpdateDB(sqlv.ToString());
                            }

                            // Compruebo conexion
                            connect &= token == "0";
                        }
                        catch (Exception)
                        {
                            connect = true;
                        }
                    }
                    // Conexion completada
                    complete?.Invoke(true);
                }
            })
            {
                Priority = ThreadPriority.Highest
            };
            hilo.Start();
        }

        /// <summary>
        /// Obtiene el codigo de creacion y crea la base de datos local SQLite.
        /// </summary>
        private void CreateDB()
        {
            // URI
            URIData u = new URIData
            {
                Url = "v/c",
                Method = 1,
                Accept = 1,
                Authu = 2
            };

            // Realizo conexion
            Connection(u, (arg1, arg2) =>
            {
                if (arg1)
                {
                    SQLite.Connection().CreateDB(arg2);
                }
                else
                {
                    if ((arg2 ?? "null").Equals("401"))
                    {
                        GetTokenFromServer();
                        token = null;
                    }
                }
                lock (d)
                {
                    Monitor.Pulse(d);
                }
            });
            lock (d)
            {
                Monitor.Wait(d);
            }
        }

        /// <summary>
        /// Comprueba si la base de datos SQLite esta actualizada y la actualiza si es necesario.
        /// </summary>
        /// <param name="versionSQLite">Version base de datos SQLite.</param>
        private void UpdateDB(string versionSQLite)
        {
            // Obtengo version app
            string versionApp = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

            // Conexion
            GetData(4, new string[] { versionSQLite, versionApp }, null, (arg1) =>
            {
                if (arg1 != null)
                {
                    // Actualizo base de datos
                    SQLite.Connection().UpdateDB(arg1);
                }
                lock (d)
                {
                    Monitor.Pulse(d);
                }
            });

            // Espero a que finalice
            lock (d)
            {
                Monitor.Wait(d);
            }
        }
        #endregion

        #region Token
        public void GetToken()
        {
            
        }

        /// <summary>
        /// Obtiene el token del servidor.
        /// </summary>
        /// <returns>El token.</returns>
        private void GetTokenFromServer()
        {
            // Variables
            string[] userData = null;
            URIData uriData = null;

            // Action
            Action<bool, string> complete = null;

            // Inicializo
            token = "0";

            // Obtengo datos usuario
            userData = UserDataDefaults.GetLoginData();

            // Preparo conexion
            if (userData != null)
            {
                // URI
                //TODO: Obtengo URI
                uriData.AddData(userData);

                // Action
                complete += (arg1, arg2) =>
                {
                    if (arg1)
                    {
                        if (arg2 != null)
                        {
                            if (!arg2.Equals("f") && !arg2.Equals("t") && !arg2.Equals("b"))
                            {
                                token = arg2;
                                // TODO: Guardar token
                            }
                        }
                    }
                    lock (d)
                    {
                        Monitor.Pulse(d);
                    }
                };
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
            string converted = ((NSString)(urlHead + uri.Url)).CreateStringByAddingPercentEscapes(NSStringEncoding.UTF8);

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
            if (uri.Authu != 0)
            {
                request["Authorization"] = (token) ?? "";
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
            // Obtener uri

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
                        // TODO: Añadir body
                    }
                }

                // Action
                Action<bool, string> actionConnection = null;
                actionConnection += (arg1, arg2) =>
                {
                    if (arg1)
                    {
                        complete(arg2);
                    }
                    else
                    {
                        if ((arg2 ?? "null").Equals("401") && flag)
                        {
                            GetTokenFromServer();
                            flag = false;
                            Connection(uri, actionConnection);
                        }
                        else
                        {
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
            complete(false, status_code.ToString());
        }
        else
        {
            complete(true, responseBuilder.ToString());
        }
    }
}

public static class Manager
{
    /// <summary>
    /// Inicializa la conexion con el servidor.
    /// </summary>
    /// <param name="complete">Complete server connection.</param>
    public static void Init(Action<bool> complete)
    {
        (UIApplication.SharedApplication.Delegate as AppDelegate).con = new ServerConnection(complete);
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