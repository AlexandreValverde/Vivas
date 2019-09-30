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
    [Register ("Crear_Tarea_TableViewCell3")]
    partial class Crear_Tarea_TableViewCell3
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDescrip { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtDescrip { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblDescrip != null) {
                lblDescrip.Dispose ();
                lblDescrip = null;
            }

            if (txtDescrip != null) {
                txtDescrip.Dispose ();
                txtDescrip = null;
            }
        }
    }
}