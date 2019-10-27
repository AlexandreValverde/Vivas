using System;
using UIKit;

namespace Tareas.Sesion
{
    public partial class Sesion_ViewController : UIViewController
    {
        public Sesion_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        /// <summary>
        /// Value changed. Activa o desactiva boton de iniciar sesion.
        /// </summary>
        /// <param name="sender"></param>
        partial void Txt_ValueChanged(UITextField sender)
        {
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUser.Text) && !string.IsNullOrWhiteSpace(txtPass.Text);
        }

        /// <summary>
        /// Touch login. Comprueba datos contra el servidor.
        /// </summary>
        /// <param name="sender"></param>
        partial void Touch_BtnLogin(UIButton sender)
        {
            // Desactivo boton
            btnLogin.Enabled = false;

            // Conexion con el servidor

        }
    }
}

