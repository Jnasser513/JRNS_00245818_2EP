using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Segundo_parcial.Modelo;

namespace Segundo_parcial.Visual
{
    public partial class frmPrincipal : Form
    {
        private APPUSER user;
        private List<Item_pedido> itemsAgregados = new List<Item_pedido>();
        public frmPrincipal(Usuario pUsuario)
        {
            InitializeComponent();
            user = pUsuario;
            cmbPermisos.DataSource = new List<String>() {"admin","user"};
        }

        

        private void frmPrincipal_Load_1(object sender, EventArgs e)
        {
            label2.Text = "Usuario : " + user.usuario;
            if (user.tipo.Equals("user"))
            {
               tabContenedor.TabPages[0].Parent=null;
               tabContenedor.TabPages[0].Parent=null;
               var dt= ConnectionDB.ExecuteQuery("SELECT inventario.nombre, item_pedido.cantidad, pedido.idpedido "+
                                                   "FROM inventario, pedido, item_pedido "+
                                                   "WHERE inventario.idproducto = item_pedido.idproducto " +
                                                   "AND item_pedido.idpedido = pedido.idpedido "+
                                                   $"AND pedido.idusuario='{user.usuario}'");
               dataGridView1.DataSource = dt;
               cmbProducto.DataSource = null;
               cmbProducto.ValueMember = "idproducto";
               cmbProducto.DisplayMember = "nombre";
               cmbProducto.DataSource = InventarioDAO.GetLista();
               rtxtItemsAgregados.AppendText("Productos agregados: \n\n");
            }
            else
            {
                var dt= ConnectionDB.ExecuteQuery("SELECT inventario.nombre, item_pedido.cantidad, " +
                                                  "pedido.idpedido, usuario.nombre "+
                                                  "FROM inventario, item_pedido, pedido, usuario");
                dataGridView1.DataSource = dt;
                tabContenedor.TabPages[3].Parent = null;


                cmbUsuarios.DataSource = null;
                cmbUsuarios.ValueMember = "contrasena";
                cmbUsuarios.DisplayMember = "usuario";
                cmbUsuarios.DataSource = UsuarioDAO.GetLista();

                cmbProductos.DataSource = null;
                cmbProductos.ValueMember = "idproducto";
                cmbProductos.DisplayMember = "nombre";
                cmbProductos.DataSource = InventarioDAO.GetLista();
            }
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir?", 
                "Tienda Capuleto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inventario u = (Inventario) cmbProducto.SelectedItem;
            nudCantidad.Maximum = u.stock;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Inventario u = (Inventario) cmbProducto.SelectedItem;
            var itemPed = new Item_pedido();
            itemPed.idproducto = u.idproducto;
            
            itemPed.cantidad = Convert.ToInt32(nudCantidad.Text);
  
            itemsAgregados.Add(itemPed);
            
            
            rtxtItemsAgregados.AppendText(u.nombre + "     "+itemPed.cantidad +"\n");
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            //descontar de stock, actualizar idpedido de cada item e insertarlos, hacer el pedido
            var idUltimo = PedidoDAO.CrearNuevo(user.usuario);
            foreach (var item in itemsAgregados)
            {
                item.idpedido = idUltimo;
                InventarioDAO.ActualizarStock(item);
                Item_pedidoDAO.CrearNuevo(item);
            }
            MessageBox.Show("Orden realizada con exito!","Tienda Capuleto",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputUsuario.Text.Length >= 5 && inputContra.Text.Length >= 5 &&
                    (inputTipo.Text.Length == 4 || inputTipo.Text.Length == 5))
                {
                    UsuarioDAO.CrearNuevo(inputUsuario.Text, inputContra.Text, inputTipo.Text);

                    MessageBox.Show("Usuario agregado con exito!",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inputUsuario.Clear();
                    inputContra.Clear();
                    inputTipo.Clear();
                }
                else
                {
                    MessageBox.Show("Datos invalidos",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Datos invalidos",
                    "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
           UsuarioDAO.ActualizarPermisos(cmbUsuarios.Text, cmbPermisos.Text);
           
           MessageBox.Show("Permisos actualizados con éxito!",
               "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCrearProducto_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(inputId.Text);
            double precio = Convert.ToDouble(inputPrecio.Text);
            int stock = Convert.ToInt32(InputStock.Text);
            try
            {
                if (id > 0 && inputNombreProd.Text.Length >= 5
                           && inputDesc.Text.Length >= 5 && precio > 0
                           && stock > 0)
                {
                    InventarioDAO.CrearNuevo(id, inputNombreProd.Text, inputDesc.Text, precio, stock);

                    MessageBox.Show("Poroducto agregado con éxito!",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Datos incorrectos",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que los datos ingresados sean váñidos",
                    "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el usuario?",
                "Tienda Capuleto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UsuarioDAO.EliminarUsuario(cmbUsuarios.Text);
            
                MessageBox.Show("Usuario eliminado exitosamente",
                    "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            int nstock = Convert.ToInt32(txtNuevoStock.Text);
            try
            {
                if (nstock > 0)
                {
                    InventarioDAO.ActNuevoStock(cmbProductos.Text, nstock);

                    MessageBox.Show("Stock actualizado con éxito",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Verifique que los datos ingresados sean válidos",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que los datos ingresados sean válidos",
                    "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta seguro de eliminar el producto?",
                "Tienda Capuleto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                InventarioDAO.EliminarProducto(cmbProductos.Text);

                MessageBox.Show("Producto eliminado exitosamente",
                    "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSavePrecio_Click(object sender, EventArgs e)
        {
            double nprecio = Convert.ToDouble(txtNuevoPrecio.Text);
            try
            {
                if (nprecio > 0)
                {
                    InventarioDAO.ActNuevoPrecio(cmbProductos.Text, nprecio);

                    MessageBox.Show("Precio actualizado con éxito",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Verifique que los datos ingresados sean válidos",
                        "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Verifique que los datos ingresados sean válidos",
                    "Tienda Capuleto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}