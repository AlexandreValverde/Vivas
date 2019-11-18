using System;
using Foundation;
using UIKit;
using Tareas.Clases;

namespace Tareas.Nueva.Add
{
    public partial class Add_Tarea_ViewController : UIViewController
    {
        public Add_Tarea_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Apariencia botones
            btnOp1.Layer.CornerRadius = 5;
            btnOp2.Layer.CornerRadius = 5;
        }

        /// <summary>
        /// Touch boton tipo 1 (amarillo).
        /// </summary>
        /// <param name="sender"></param>
        partial void Btn1_Touch(UIButton sender)
        {
            // Muestro view
            PerformSegue("newNavSegue", null);

            // Tipo de tarea
            (UIApplication.SharedApplication.Delegate as AppDelegate).tipoNueva = false;
        }

        /// <summary>
        /// Touch boton tipo 2 (azul);
        /// </summary>
        /// <param name="sender"></param>
        partial void Btn2_Touch(UIButton sender)
        {
            // Muestro view
            PerformSegue("newNavSegue", null);

            // Tipo de tarea
            (UIApplication.SharedApplication.Delegate as AppDelegate).tipoNueva = true;
        }
    }
}

