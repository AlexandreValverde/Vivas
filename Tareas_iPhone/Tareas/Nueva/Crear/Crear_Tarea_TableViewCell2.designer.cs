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
    [Register ("Crear_Tarea_TableViewCell2")]
    partial class Crear_Tarea_TableViewCell2
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOp1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOp2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch switchOp1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch switchOp2 { get; set; }

        [Action ("SwitchOp2_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SwitchOp2_Changed (UIKit.UISwitch sender);

        [Action ("SwitchUrgente_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SwitchUrgente_Changed (UIKit.UISwitch sender);

        void ReleaseDesignerOutlets ()
        {
            if (lblOp1 != null) {
                lblOp1.Dispose ();
                lblOp1 = null;
            }

            if (lblOp2 != null) {
                lblOp2.Dispose ();
                lblOp2 = null;
            }

            if (switchOp1 != null) {
                switchOp1.Dispose ();
                switchOp1 = null;
            }

            if (switchOp2 != null) {
                switchOp2.Dispose ();
                switchOp2 = null;
            }
        }
    }
}