using System;
using System.Windows.Forms;
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
                var appuser = UsuarioDAO.GetUser(textBox1.Text, textBox2.Text);
                if (appuser.user.Equals("") || appuser.password.Equals(""))
                {
                    MessageBox.Show("Usuario y/o contraseña incorrecta","HUGO APP",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Bienvenido","HUGO APP",
                        MessageBoxButtons.OK,MessageBoxIcon.Information);
                    frmPrincipal ventana = new frmPrincipal(appuser);
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