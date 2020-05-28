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
    }
}