using System;
using Tareas.Componentes;
using Tareas.Servidor;
using UIKit;

namespace Tareas.Sesion
{
    public partial class Sesion_ViewController : UIViewController
    {
        // Evento login
        public event EventHandler OnLoginSuccess;

        public Sesion_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Inicializo conexion servidor
            RestManager.Init();
        }

        /// <summary>
        /// Value changed txt.
        /// </summary>
        /// <param name="sender"></param>
        partial void ValueChanged_txt(UITextField sender)
        {
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUser.Text) && !string.IsNullOrWhiteSpace(txtPass.Text);
        }

        /// <summary>
        /// Touch boton login.
        /// </summary>
        /// <param name="sender"></param>
        partial void Touch_BtnLogin(ButtonLoad sender)
        {
            // Desactivo boton
            btnLogin.Enabled = false;

            // Muestro carga
            sender.ShowLoading();

            // Conexion con el servidor
            RestManager.Connection().GetData((int)URIS.Login, new string[] { txtUser.Text.Trim(), txtPass.Text.Trim() }, null, (arg) =>
            {
                // Compruebo datos
                if (!string.IsNullOrWhiteSpace(arg)) // Login correcto
                {
                    // Compruebo id
                    if (!arg.Equals("-1"))
                    {
                        LoginOk(arg);
                    }
                    else
                    {
                        LoginError();
                    }
                }
                else // Login incorrecto
                {
                    LoginError();
                }
            });
        }

        /// <summary>
        /// Login correcto.
        /// </summary>
        /// <param name="id">ID usuario.</param>
        private void LoginOk(string id)
        {
            InvokeOnMainThread(() =>
            {
                // Guardo login data
                UserDataDefaults.SetLoginData(txtUser.Text.Trim(), txtPass.Text.Trim());

                // Guardo ID usuario
                UserDataDefaults.SetUserID(id);

                // Inicializo preferencias
                UserDataDefaults.SetPreferences(0, 0, 0);

                // Obtiene token
                RestManager.Connection().GetTokenFromServer();

                // Login OK
                OnLoginSuccess(true, new EventArgs());
            });
        }

        /// <summary>
        /// Login erroneo.
        /// </summary>
        private void LoginError()
        {
            InvokeOnMainThread(() =>
            {
                // Paro carga
                btnLogin.HideLoading();

                // Muestro aviso
                ShowAlert();

                // Activo boton
                btnLogin.Enabled = true;
            });
        }

        /// <summary>
        /// Muestra alerta de login incorrecto.
        /// </summary>
        private void ShowAlert()
        {
            // Creo alerta
            UIAlertController alert = UIAlertController.Create("Login incorrecto", "El email y contraseña no coinciden", UIAlertControllerStyle.Alert);

            // Añado accion
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            // Muestro alerta
            PresentViewController(alert, true, null);
        }
    }
}