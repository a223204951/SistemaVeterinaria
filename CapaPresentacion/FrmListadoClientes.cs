using System;
using System.Data;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmListadoClientes : Form
    {
        public FrmListadoClientes()
        {
            InitializeComponent();
        }

        private void FrmListadoCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Mostrar();
        }

        public void Mostrar()
        {
            this.dlistado.DataSource = CN_Cliente.Listar();
            ActualizarContador();
        }

        private void ActualizarContador()
        {
            lblTotal.Text = $"Total de clientes: {dlistado.Rows.Count}";
        }

        private void BuscarNombre() => dlistado.DataSource = CN_Cliente.BuscarNombre(txtbuscar.Text);
        private void BuscarId() => dlistado.DataSource = CN_Cliente.BuscarId(txtbuscar.Text);

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (rbtnnombre.Checked) { BuscarNombre(); ActualizarContador(); }
            else if (rbtnidcliente.Checked) { BuscarId(); ActualizarContador(); }
            else
                MessageBox.Show("Seleccione un criterio de búsqueda",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Búsqueda en tiempo real mientras el usuario escribe
        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string texto = txtbuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    Mostrar();
                    return;
                }

                if (rbtnnombre.Checked)
                    BuscarNombre();
                else
                    BuscarId();

                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // *** CORRECCIÓN: ShowDialog() en lugar de Show()+Hide()
        //     Al cerrar el FrmRegistrarCliente con DialogResult.OK se refresca aquí mismo ***
        private void btnnuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarCliente form = new FrmRegistrarCliente();
            form.Insert = true;
            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (dlistado.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarCliente form = new FrmRegistrarCliente();
            form.Edit = true;
            form.txtidcliente.Text = dlistado.CurrentRow.Cells["idcliente"].Value.ToString();
            form.txtnombre.Text = dlistado.CurrentRow.Cells["nombre"].Value.ToString();
            form.txttelefono.Text = dlistado.CurrentRow.Cells["telefono"].Value.ToString();
            form.txtdireccion.Text = dlistado.CurrentRow.Cells["direccion"].Value.ToString();

            string estado = dlistado.CurrentRow.Cells["estado"].Value.ToString();
            if (estado == "ACTIVO") form.rbtnactivo.Checked = true;
            else form.rbtninactivo.Checked = true;

            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlistado.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un cliente para eliminar",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombre = dlistado.CurrentRow.Cells["nombre"].Value.ToString();
                if (MessageBox.Show($"¿Eliminar permanentemente al cliente '{nombre}'?",
                        "Sistema Veterinaria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.OK)
                {
                    string resultado = CN_Cliente.Eliminar(
                        Convert.ToInt32(dlistado.CurrentRow.Cells["idcliente"].Value),
                        FrmLogin.UsuarioActual);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Cliente eliminado correctamente",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                    }
                    else
                        MessageBox.Show(resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}