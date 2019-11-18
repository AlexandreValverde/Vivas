using System;
using UIKit;
using Tareas.Servidor;
using Foundation;

namespace Tareas.Usuario
{
    public partial class Usuario_ViewController : UIViewController
    {
        public Usuario_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Obtengo nombre usuario
            RestManager.Connection().GetData((int)URIS.UserName, new string[] { UserDataDefaults.GetUserID().ToString() }, null, (arg) =>
            {
                // Compruebo datos
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    InvokeOnMainThread(() =>
                    {
                        // Actualizo nombre
                        lblName.Text = arg;
                    });
                }
            });

            // Version
            lblVersion.Text = (NSString)NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"];
        }

        /// <summary>
        /// Touch boton desconectar.
        /// </summary>
        /// <param name="sender"></param>
        partial void btnDesconectar_touch(UIButton sender)
        {
            // Elimino base de datos
            SQLiteManager.Connection().Eliminar();

            // Elimino datos usuario
            UserDataDefaults.Eliminar();

            // Desconecto
            (UIApplication.SharedApplication.Delegate as AppDelegate).Desconectar();
        }
    }
}
