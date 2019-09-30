// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Tareas.Tareas.Lista
{
    [Register ("Lista_Tareas_TableViewCell")]
    partial class Lista_Tareas_TableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblFecha { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblInfo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblKms { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblUbic { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblFecha != null) {
                lblFecha.Dispose ();
                lblFecha = null;
            }

            if (lblInfo != null) {
                lblInfo.Dispose ();
                lblInfo = null;
            }

            if (lblKms != null) {
                lblKms.Dispose ();
                lblKms = null;
            }

            if (lblUbic != null) {
                lblUbic.Dispose ();
                lblUbic = null;
            }
        }
    }
}