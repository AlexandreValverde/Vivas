// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Tareas.Tareas.Filtrar
{
    [Register ("Filtrar_Tareas_ViewController")]
    partial class Filtrar_Tareas_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnMostrarSelect { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOrderSelect { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnTipoSelect { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblMostrar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOrdenar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTipo { get; set; }

        [Action ("mostrar_touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void mostrar_touch (UIKit.UIButton sender);

        [Action ("orden_touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void orden_touch (UIKit.UIButton sender);

        [Action ("tipo_touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void tipo_touch (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnMostrarSelect != null) {
                btnMostrarSelect.Dispose ();
                btnMostrarSelect = null;
            }

            if (btnOrderSelect != null) {
                btnOrderSelect.Dispose ();
                btnOrderSelect = null;
            }

            if (btnTipoSelect != null) {
                btnTipoSelect.Dispose ();
                btnTipoSelect = null;
            }

            if (lblMostrar != null) {
                lblMostrar.Dispose ();
                lblMostrar = null;
            }

            if (lblOrdenar != null) {
                lblOrdenar.Dispose ();
                lblOrdenar = null;
            }

            if (lblTipo != null) {
                lblTipo.Dispose ();
                lblTipo = null;
            }
        }
    }
}