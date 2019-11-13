using System;
using UIKit;

namespace Tareas.Componentes
{
    public partial class ButtonLoad : UIButton
    {
        // Variables
        private string buttonText;
        private UIActivityIndicatorView activityIndicator;

        public ButtonLoad(IntPtr handle) : base(handle)
        {
        }

        /// <summary>
        /// Muestro indicador de actividad.
        /// </summary>
        public void ShowLoading()
        {
            // Texto boton
            buttonText = Title(UIControlState.Normal);
            SetTitle("", UIControlState.Normal);

            // Creo indicador de actividad
            if (activityIndicator == null)
            {
                activityIndicator = new UIActivityIndicatorView
                {
                    HidesWhenStopped = true,
                    Color = UIColor.White,
                    TranslatesAutoresizingMaskIntoConstraints = false
                };
                AddSubview(activityIndicator);
                CenterActivityIndicator();
            }

            // Inicio indicador de actividad
            activityIndicator.StartAnimating();
        }

        /// <summary>
        /// Oculto indicador de actividad.
        /// </summary>
        public void HideLoading()
        {
            SetTitle(buttonText, UIControlState.Normal);
            activityIndicator.StopAnimating();
        }

        /// <summary>
        /// Añade constraints.
        /// </summary>
        private void CenterActivityIndicator()
        {
            NSLayoutConstraint x = NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, activityIndicator, NSLayoutAttribute.CenterX, 1, 0);
            AddConstraint(x);

            NSLayoutConstraint y = NSLayoutConstraint.Create(this, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, activityIndicator, NSLayoutAttribute.CenterY, 1, 0);
            AddConstraint(y);
        }
    }
}

