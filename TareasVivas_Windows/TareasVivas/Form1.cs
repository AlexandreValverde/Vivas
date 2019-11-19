using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TareasVivas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Boton añadir pulsado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Compruebo datos
            if (CheckData())
            {
                // Conexion
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("http://192.168.0.35:31416/tareasvivas/app/users/")
                };

                // Parametros
                string p = string.Format("{0}?nombre={1}&email={2}&pass={3}", chkAdmin.Checked ? "nuevoadmin" : "nuevousuario", txtNombre.Text.Trim(), txtEmail.Text.Trim(), txtPass.Text.Trim());

                // Añado Accept
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                // Depuracion
                Console.WriteLine("URL -> " + p);

                // Obtengo datos
                HttpResponseMessage response = client.GetAsync(p).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Actualizo info
                    lblInfo.Text = "Usuario añadido!";
                }
                else
                {
                    // Depuracion
                    Console.WriteLine("Codigo respuesta servirdor -> " + response.StatusCode);

                    // Actualizo info
                    lblInfo.Text = "Error al añadir usuario...";
                }
            }
            else
            {
                // Actualizo info
                lblInfo.Text = "Introduce todos los datos";
            }
        }

        /// <summary>
        /// Comprueba los datos.
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) return false;
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) return false;
            if (string.IsNullOrWhiteSpace(txtPass.Text)) return false;
            return true;
        }
    }
}
