using System;
using System.Windows.Forms;
using Segundo_parcial.Controlador;
using Segundo_parcial.Modelo;

namespace Segundo_parcial.Visual
{
    public partial class frmInicioSesion : Form
    {
        public frmInicioSesion()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var user = UsuarioDAO.GetUser(textBox1.Text, textBox2.Text);
                if (user.username.Equals("") || user.password.Equals(""))
                {
                    MessageBox.Show("Por favor revisar credenciales", "HUGO APP");
                }
                else
                {
                    MessageBox.Show("Bienvenido!", "HUGO APP");
                    frmPrincipal ventana = new frmPrincipal(user);
                    ventana.Show();
                    this.Hide();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                throw;
            }
        }

        private void frmInicioSesion_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCambiarContrasena unaVentana = new frmCambiarContrasena();
            unaVentana.ShowDialog();
        }
    }
}