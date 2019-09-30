// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Tareas.Nueva.Buscar
{
    [Register ("Buscar_Ubic_TableViewCell")]
    partial class Buscar_Ubic_TableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblInfo != null) {
                lblInfo.Dispose ();
                lblInfo = null;
            }
        }
    }
}