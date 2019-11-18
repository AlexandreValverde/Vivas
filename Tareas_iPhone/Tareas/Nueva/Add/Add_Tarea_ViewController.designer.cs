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

namespace Tareas.Nueva.Add
{
    [Register ("Add_Tarea_ViewController")]
    partial class Add_Tarea_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOp1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnOp2 { get; set; }

        [Action ("Btn1_Touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Btn1_Touch (UIKit.UIButton sender);

        [Action ("Btn2_Touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Btn2_Touch (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnOp1 != null) {
                btnOp1.Dispose ();
                btnOp1 = null;
            }

            if (btnOp2 != null) {
                btnOp2.Dispose ();
                btnOp2 = null;
            }
        }
    }
}