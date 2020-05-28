using System;
using System.Windows.Forms;
using Segundo_parcial.Controlador;
using Segundo_parcial.Modelo;

namespace Segundo_parcial.Visual
{
    public partial class frmCambiarContrasena : Form
    {
        public frmCambiarContrasena()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool actualIgual = UsuarioDAO.GetUser(txtUser, txtContraActual);
            bool NuevaIgual = txtNcontra.Text.Equals(txtCcontra.Text);
            bool nuevaValida = txtNcontra.Text.Length > 0;
            if (actualIgual && NuevaIgual && nuevaValida)
            {
                try
                {
                    AppUserDAO.ActualizarContra(txtUser.Text, txtNcontra.Text);
                    MessageBox.Show("Contraseña actualizada con éxito",
                        "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Favor verifique que los datos ingresados sean válidos",
                        "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Favor verifique que los datos ingresados sean válidos",
                    "Hugo App", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}