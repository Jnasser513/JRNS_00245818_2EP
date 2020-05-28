using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Segundo_parcial.Modelo;

namespace Segundo_parcial.Visual
{
    public partial class frmPrincipal : Form
    {
        private APPUSER usuario;
        public frmPrincipal(APPUSER pUsuario)
        {
            InitializeComponent();
            usuario = pUsuario;
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            label4.Text = 
                "Bienvenido " + usuario.username + " [" + (usuario.type ? "Administrador" : "Usuario") + "]";

            if (usuario.type)
            {
                
            }
            else
            {
                // Los usuarios NO administradores no tienen permiso de acceder a estas pestanas
                tabControl1.TabPages[0].Parent = null;
                tabControl1.TabPages[1].Parent = null;
                tabControl1.TabPages[1].Parent = null;
                tabControl1.TabPages[2].Parent = null;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    UsuarioDAO.NewUser(textBox1.Text, textBox2.Text, textBox3.Text, radioButton1.Checked);

                    MessageBox.Show("El administrador ha sido agregado!",
                        "HUGO APP");

                    textBox2.Clear();
                    textBox1.Clear();
                    textBox3.Clear();
                    actualizarControles();
                }
                else if (radioButton2.Checked)
                {
                    UsuarioDAO.NewUser(textBox1.Text, textBox2.Text, textBox3.Text, radioButton1.Checked);

                    MessageBox.Show("El cliente ha sido agregado!",
                        "HUGO APP");

                    textBox2.Clear();
                    textBox1.Clear();
                    textBox3.Clear();
                    actualizarControles();
                }
                else
                    MessageBox.Show("Algo salio mal...",
                        "HUGO APP");
            }
            catch (Exception)
            {
                MessageBox.Show("El usuario no existe", "HUGO APP");
            }
        }
        private void actualizarControles()
        {

            List<APPUSER> lista = UsuarioDAO.GetLista();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "password";
            comboBox1.DisplayMember = "username";
            comboBox1.DataSource = lista;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar este usuario? " + comboBox1.Text ,
                "HUGO APP", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UsuarioDAO.EliminarUsuario(comboBox1.Text);

                MessageBox.Show("El usuario ha sido eliminado", "HUGO APP");

                actualizarControles();
            }
        }
    }
}