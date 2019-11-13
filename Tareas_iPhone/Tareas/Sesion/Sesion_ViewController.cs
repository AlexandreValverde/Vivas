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
            RestManager.Connection().GetData((int)URIS.Login, new string[] { txtUser.Text.Trim(), txtPass.Text.Trim() }, null, (arg1) =>
            {
                // Compruebo datos
                if (!string.IsNullOrWhiteSpace(arg1)) // Login correcto
                {
                    InvokeOnMainThread(() =>
                    {
                        // Guardo ID usuario
                        UserDataDefaults.SetUserID(arg1);

                        // Obtiene token
                        RestManager.Connection().GetTokenFromServer();

                        // Login OK
                        OnLoginSuccess?.Invoke(true, new EventArgs());
                    });
                }
                else // Login incorrecto
                {
                    InvokeOnMainThread(() =>
                    {
                        // Paro carga
                        ((ButtonLoad)btnLogin).HideLoading();

                        // Muestro aviso
                        ShowAlert();

                        // Activo boton
                        btnLogin.Enabled = true;
                    });
                }
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