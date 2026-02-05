using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmRegistrarCliente : Form
    {
        public bool Insert = false;
        public bool Edit = false;
        public FrmRegistrarCliente()
        {
            InitializeComponent();
        }

        private void FrmRegistrarCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
        }
    }
}
