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
        private void actualizarControlesA()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = DireccionDAO.verDirecciones(usuario);
            
            comboBox2.DataSource = null;
            comboBox2.ValueMember = "idaddress";
            comboBox2.DisplayMember = "address";
            comboBox2.DataSource = DireccionDAO.verDirecciones(usuario);
        }
        private void actualizarControlesB()
        {

            List<Negocio> lista = NegocioDAO.getLista();

            dataGridView3.DataSource = null;
            dataGridView3.DataSource = lista;
            
            comboBox3.DataSource = null;
            comboBox3.ValueMember = "idbusiness";
            comboBox3.DisplayMember = "name";
            comboBox3.DataSource = lista;
            
            comboBox5.DataSource = null;
            comboBox5.ValueMember = "idproduct";
            comboBox5.DisplayMember = "name";
            comboBox5.DataSource = ProductoDAO.getLista();
            
            comboBox4.DataSource = null;
            comboBox4.ValueMember = "idbusiness";
            comboBox4.DisplayMember = "name";
            comboBox4.DataSource = NegocioDAO.getLista(); 
        }
        private void actualizarControlesP()
        {
            List<Producto> lista = ProductoDAO.getLista();
            
            comboBox5.DataSource = null;
            comboBox5.ValueMember = "idproduct";
            comboBox5.DisplayMember = "name";
            comboBox5.DataSource = lista;
            
            dataGridView4.DataSource = null;
            dataGridView4.DataSource = lista;
            
            comboBox4.DataSource = null; 
            comboBox4.ValueMember = "idbusiness";
            comboBox4.DisplayMember = "name";
            comboBox4.DataSource = NegocioDAO.getLista(); 
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text.Length >= 6)
                {
                    DireccionDAO.agregarDireccion(textBox4.Text, usuario.username);

                    MessageBox.Show("La direccion ha sido agregada!", "HUGO APP");

                    textBox4.Clear();
                    actualizarControles();
                }
                else
                    MessageBox.Show("Ocurrio un error", "HUGO APP");
            }
            catch (Exception)
            {
                MessageBox.Show("Algo salio mal...", "HUGO APP");
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea eliminar esta direccion? " + comboBox2.Text ,
                    "HUGO APP", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DireccionDAO.eliminarDireccion(comboBox2.Text);

                MessageBox.Show("La direccion ha sido eliminada", "HUGO APP");
                
                actualizarControles();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text.Length >= 4)
                {
                    NegocioDAO.crearNegocio(textBox5.Text, textBox6.Text);

                    MessageBox.Show("¡Negocio agregado exitosamente!", "HUGO APP");

                    textBox5.Clear();
                    textBox6.Clear();
                    actualizarControlesB();
                }
                else
                    MessageBox.Show("Por favor digite un negocio con longitud minima de 4)",
                        "HUGO APP", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Algo salio mal...",
                    "HUGO APP", MessageBoxButtons.OK);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el negocio ? " + comboBox3.Text ,
                "HUGO APP", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NegocioDAO.eliminarNegocio(comboBox3.Text);

                MessageBox.Show("El negocio ha sido eliminado!", "HUGO APP");

                actualizarControlesB();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Negocio neg = new Negocio();
                neg.name = comboBox4.Text;

                
                Producto producto = new Producto();
                producto.name = textBox8.Text;

                ProductoDAO.agregarProducto(producto, neg);

                MessageBox.Show("El producto ha sido agregado!", "HUGO APP");

                textBox8.Clear();
                actualizarControlesP();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message, "Hugo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}