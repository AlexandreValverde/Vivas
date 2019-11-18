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

namespace Tareas.Nueva.Crear
{
    [Register ("Crear_Tarea_ViewController")]
    partial class Crear_Tarea_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnAdd { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tabla { get; set; }

        [Action ("Add_Touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Add_Touch (UIKit.UIBarButtonItem sender);

        [Action ("BtnCancel_Touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void BtnCancel_Touch (UIKit.UIBarButtonItem sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnAdd != null) {
                btnAdd.Dispose ();
                btnAdd = null;
            }

            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (tabla != null) {
                tabla.Dispose ();
                tabla = null;
            }
        }
    }
}