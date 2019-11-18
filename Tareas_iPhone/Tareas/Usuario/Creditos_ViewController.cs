using System;
using UIKit;

namespace Tareas.Usuario
{
    public partial class Creditos_ViewController : UIViewController
    {
        public Creditos_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Texto
            txtCreditos.Text = "Iconos obtenidos de flaticon.es";
        }

        /// <summary>
        /// Touch boton salir.
        /// </summary>
        /// <param name="sender"></param>
        partial void Exit_Touch(UIBarButtonItem sender)
        {
            // Vuelvo a la View anterior
            DismissModalViewController(true);
        }
    }
}