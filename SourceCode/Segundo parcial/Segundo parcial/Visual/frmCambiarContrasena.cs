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
        
        private void CambiarContrasena_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "password";
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = UsuarioDAO.GetLista();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool actualIgual = comboBox1.SelectedValue.Equals(textBox2.Text);
            bool nuevaIgual = textBox3.Text.Equals(textBox4.Text);
            bool nuevaValida = textBox3.Text.Length > 0;

            if (actualIgual && nuevaIgual && nuevaValida)
            {
                try
                {
                    UsuarioDAO.ActualizarPassword(comboBox1.Text, textBox3.Text);
                    
                    MessageBox.Show("La contraseña ha sido actualizada", "HUGO APP");
                    
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Algo salio mal...", "HUGO APP");
                }
            }
            else
                MessageBox.Show("Datos incorrectos", "HUGO APP");
        }
        
    }
}