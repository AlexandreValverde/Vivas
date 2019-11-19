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
    [Register ("Crear_Tarea_TableViewCell4")]
    partial class Crear_Tarea_TableViewCell4
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgMarcador { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCiudad { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblDirec { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNombreLugar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNum { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblPais { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblProv { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView mapa { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtCiudad { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtDireccion { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNombreLugar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNum { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtPais { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtProvincia { get; set; }

        [Action ("TxtCiudad_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtCiudad_Changed (UIKit.UITextField sender);

        [Action ("TxtDireccion_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtDireccion_Changed (UIKit.UITextField sender);

        [Action ("TxtLugar_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtLugar_Changed (UIKit.UITextField sender);

        [Action ("TxtNumero_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtNumero_Changed (UIKit.UITextField sender);

        [Action ("TxtPais_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtPais_Changed (UIKit.UITextField sender);

        [Action ("TxtProvincia_Changed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void TxtProvincia_Changed (UIKit.UITextField sender);

        void ReleaseDesignerOutlets ()
        {
            if (imgMarcador != null) {
                imgMarcador.Dispose ();
                imgMarcador = null;
            }

            if (lblCiudad != null) {
                lblCiudad.Dispose ();
                lblCiudad = null;
            }

            if (lblDirec != null) {
                lblDirec.Dispose ();
                lblDirec = null;
            }

            if (lblNombreLugar != null) {
                lblNombreLugar.Dispose ();
                lblNombreLugar = null;
            }

            if (lblNum != null) {
                lblNum.Dispose ();
                lblNum = null;
            }

            if (lblPais != null) {
                lblPais.Dispose ();
                lblPais = null;
            }

            if (lblProv != null) {
                lblProv.Dispose ();
                lblProv = null;
            }

            if (mapa != null) {
                mapa.Dispose ();
                mapa = null;
            }

            if (txtCiudad != null) {
                txtCiudad.Dispose ();
                txtCiudad = null;
            }

            if (txtDireccion != null) {
                txtDireccion.Dispose ();
                txtDireccion = null;
            }

            if (txtNombreLugar != null) {
                txtNombreLugar.Dispose ();
                txtNombreLugar = null;
            }

            if (txtNum != null) {
                txtNum.Dispose ();
                txtNum = null;
            }

            if (txtPais != null) {
                txtPais.Dispose ();
                txtPais = null;
            }

            if (txtProvincia != null) {
                txtProvincia.Dispose ();
                txtProvincia = null;
            }
        }
    }
}