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

namespace Tareas.Tareas.Datos
{
    [Register ("Datos_Tarea_ViewController")]
    partial class Datos_Tarea_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnComplete { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tabla { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnComplete != null) {
                btnComplete.Dispose ();
                btnComplete = null;
            }

            if (tabla != null) {
                tabla.Dispose ();
                tabla = null;
            }
        }
    }
}