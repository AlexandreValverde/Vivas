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

namespace Tareas.Usuario
{
    [Register ("Creditos_ViewController")]
    partial class Creditos_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnExit { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtCreditos { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnExit != null) {
                btnExit.Dispose ();
                btnExit = null;
            }

            if (txtCreditos != null) {
                txtCreditos.Dispose ();
                txtCreditos = null;
            }
        }
    }
}