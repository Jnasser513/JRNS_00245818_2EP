using Preparcial.Controlador;
using Preparcial.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Preparcial.Vista
{
    public partial class FrmPassword : Form
    {
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void FrmPassword_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Image.FromFile("../../Recursos/UCA.png");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            ActualizarControlers();
        }

        private void ActualizarControlers()
        {
            //Correcion: falta el datasource = null;
            comboBox1.DataSource = null;
            comboBox1.ValueMember = "Contrasena";
            //Correcion: DisplayMember tiene que ir antes que DataSource
            comboBox1.DisplayMember = "NombreUsuario";
            comboBox1.DataSource = ControladorUsuario.GetUsuarios();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Correcion: La contraseña nueva no puede quedar vacia
            if (txtOldPassword.Text.Equals(comboBox1.SelectedValue.ToString()) && txtNewPassword.Text.Length > 0)
            {       
                var obtenerUsuario = (Usuario)comboBox1.SelectedItem;

                ActualizarControlers();
                
                ControladorUsuario.ActualizarContrasena(obtenerUsuario.IdUsuario, 
                    txtNewPassword.Text);
                //Agregar para cerrar ventana despues de dar click en cambiar contrasenia
                this.Close();
            }
            else 
                MessageBox.Show("Contrasena actual incorrecta");
        }
    }
}
