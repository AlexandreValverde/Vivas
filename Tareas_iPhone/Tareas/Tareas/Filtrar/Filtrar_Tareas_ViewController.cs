using System;
using Tareas.Clases;
using Tareas.Servidor;
using UIKit;

namespace Tareas.Tareas.Filtrar
{
    public partial class Filtrar_Tareas_ViewController : UIViewController
    {
        // Controller
        public Lista.Lista_Tareas_ViewController controllerLista;

        // Preferencias
        private Preferencias p;

        public Filtrar_Tareas_ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Obtengo preferencias
            p = UserDataDefaults.GetPreferences();

            // Cargo botones
            SetOrden();
            SetMostrar();
            SetTipo();
        }

        /// <summary>
        /// Cambia boton orden.
        /// </summary>
        public void SetOrden()
        {
            // Opcion
            switch(p.Orden)
            {
                case 0: btnOrderSelect.SetTitle("FECHA ASCENDIENTE", UIControlState.Normal);
                    break;
                case 1: btnOrderSelect.SetTitle("FECHA DESCENDIENTE", UIControlState.Normal);
                    break;
            }
        }

        /// <summary>
        /// Cambia boton mostrar.
        /// </summary>
        public void SetMostrar()
        {
            // Opcion
            switch (p.Mostrar)
            {
                case 0: btnMostrarSelect.SetTitle("TODAS LAS TAREAS", UIControlState.Normal);
                    break;
                case 1: btnMostrarSelect.SetTitle("SIN REALIZAR", UIControlState.Normal);
                    break;
                case 2: btnMostrarSelect.SetTitle("REALIZADAS", UIControlState.Normal);
                    break;
            }
        }

        /// <summary>
        /// Cambia boton tipo.
        /// </summary>
        public void SetTipo()
        {
            // Opcion
            switch (p.Tipo)
            {
                case 0: btnTipoSelect.SetTitle("TODAS", UIControlState.Normal);
                    break;
                case 1: btnTipoSelect.SetTitle("URGENTES", UIControlState.Normal);
                    break;
                case 2: btnTipoSelect.SetTitle("NORMALES", UIControlState.Normal);
                    break;
            }
        }

        /// <summary>
        /// Touch boton orden.
        /// </summary>
        /// <param name="sender"></param>
        partial void orden_touch(UIButton sender)
        {
            // Cambio
            p.Orden = p.Orden == 1 ? 0 : 1;

            // Guardo
            UserDataDefaults.SetPreferences(p.Orden, p.Mostrar, p.Tipo);

            // Actualizo
            SetOrden();
        }

        /// <summary>
        /// Touch boton mostrar.
        /// </summary>
        /// <param name="sender"></param>
        partial void mostrar_touch(UIButton sender)
        {
            // Cambio
            p.Mostrar++;

            // Restauro
            if (p.Mostrar > 2) p.Mostrar = 0;

            // Guardo
            UserDataDefaults.SetPreferences(p.Orden, p.Mostrar, p.Tipo);

            // Actualizo
            SetMostrar();
        }

        /// <summary>
        /// Touch boton tipo.
        /// </summary>
        /// <param name="sender"></param>
        partial void tipo_touch(UIButton sender)
        {
            // Cambio
            p.Tipo++;

            // Restauro
            if (p.Tipo > 2) p.Tipo = 0;


            // Guardo
            UserDataDefaults.SetPreferences(p.Orden, p.Mostrar, p.Tipo);

            // Actualizo
            SetTipo();
        }
    }
}

