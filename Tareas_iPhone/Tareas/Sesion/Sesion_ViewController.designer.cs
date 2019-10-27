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

namespace Tareas.Sesion
{
    [Register ("Sesion_ViewController")]
    partial class Sesion_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgLogo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPass { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblUser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPass { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtUser { get; set; }

        [Action ("Touch_BtnLogin:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Touch_BtnLogin (UIKit.UIButton sender);

        [Action ("Txt_ValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Txt_ValueChanged (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnLogin != null) {
                btnLogin.Dispose ();
                btnLogin = null;
            }

            if (imgLogo != null) {
                imgLogo.Dispose ();
                imgLogo = null;
            }

            if (lblPass != null) {
                lblPass.Dispose ();
                lblPass = null;
            }

            if (lblUser != null) {
                lblUser.Dispose ();
                lblUser = null;
            }

            if (txtPass != null) {
                txtPass.Dispose ();
                txtPass = null;
            }

            if (txtUser != null) {
                txtUser.Dispose ();
                txtUser = null;
            }
        }
    }
}