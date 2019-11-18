// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Tareas.Tareas.Datos
{
    [Register ("Datos_Tarea_TableViewCell")]
    partial class Datos_Tarea_TableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCalle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCiudad { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNombre { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblNotas { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblOp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblTelefono { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView map { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblCalle != null) {
                lblCalle.Dispose ();
                lblCalle = null;
            }

            if (lblCiudad != null) {
                lblCiudad.Dispose ();
                lblCiudad = null;
            }

            if (lblNombre != null) {
                lblNombre.Dispose ();
                lblNombre = null;
            }

            if (lblNotas != null) {
                lblNotas.Dispose ();
                lblNotas = null;
            }

            if (lblOp != null) {
                lblOp.Dispose ();
                lblOp = null;
            }

            if (lblTelefono != null) {
                lblTelefono.Dispose ();
                lblTelefono = null;
            }

            if (map != null) {
                map.Dispose ();
                map = null;
            }
        }
    }
}