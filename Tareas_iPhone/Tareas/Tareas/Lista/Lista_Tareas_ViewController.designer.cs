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

namespace Tareas.Tareas.Lista
{
    [Register ("Lista_Tareas_ViewController")]
    partial class Lista_Tareas_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnFiltrar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView table { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnFiltrar != null) {
                btnFiltrar.Dispose ();
                btnFiltrar = null;
            }

            if (table != null) {
                table.Dispose ();
                table = null;
            }
        }
    }
}