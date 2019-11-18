// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Tareas.Nueva.Crear
{
    [Register ("Crear_Tarea_TableViewCell1")]
    partial class Crear_Tarea_TableViewCell1
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNombre { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNombre { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtTel { get; set; }

        [Action ("TxtApellidos_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtApellidos_Changed (UIKit.UITextField sender);

        [Action ("TxtNombre_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtNombre_Changed (UIKit.UITextField sender);

        [Action ("TxtTelefono_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtTelefono_Changed (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (lblNombre != null) {
                lblNombre.Dispose ();
                lblNombre = null;
            }

            if (lblTel != null) {
                lblTel.Dispose ();
                lblTel = null;
            }

            if (txtNombre != null) {
                txtNombre.Dispose ();
                txtNombre = null;
            }

            if (txtTel != null) {
                txtTel.Dispose ();
                txtTel = null;
            }
        }
    }
}