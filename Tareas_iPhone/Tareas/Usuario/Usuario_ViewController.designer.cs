﻿// WARNING
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
    [Register ("Usuario_ViewController")]
    partial class Usuario_ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnCreditos { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnDesconectar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgPerfil { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblVersion { get; set; }

        [Action ("btnDesconectar_touch:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void btnDesconectar_touch (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnCreditos != null) {
                btnCreditos.Dispose ();
                btnCreditos = null;
            }

            if (btnDesconectar != null) {
                btnDesconectar.Dispose ();
                btnDesconectar = null;
            }

            if (imgPerfil != null) {
                imgPerfil.Dispose ();
                imgPerfil = null;
            }

            if (lblName != null) {
                lblName.Dispose ();
                lblName = null;
            }

            if (lblVersion != null) {
                lblVersion.Dispose ();
                lblVersion = null;
            }
        }
    }
}