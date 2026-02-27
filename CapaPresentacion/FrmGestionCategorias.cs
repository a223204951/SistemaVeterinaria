using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmGestionCategorias : Form
    {
        public FrmGestionCategorias()
        {
            InitializeComponent();
        }

        private void FrmGestionCategorias_Load(object sender, EventArgs e)
        {
            if (FrmLogin.RolActual != "ADMINISTRADOR")
            {
                MessageBox.Show("⚠️ Solo los administradores pueden gestionar categorías",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Mostrar();
        }

        private void Mostrar()
        {
            try
            {
                DataTable datos = CN_Categoria.Listar();
                dgvCategorias.DataSource = datos;
                ConfigurarColumnas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvCategorias.Columns.Count == 0) return;

            if (dgvCategorias.Columns.Contains("fecha_creacion"))
                dgvCategorias.Columns["fecha_creacion"].Visible = false;

            if (dgvCategorias.Columns.Contains("idcategoria")) dgvCategorias.Columns["idcategoria"].HeaderText = "ID";
            if (dgvCategorias.Columns.Contains("nombre")) dgvCategorias.Columns["nombre"].HeaderText = "Nombre";
            if (dgvCategorias.Columns.Contains("descripcion")) dgvCategorias.Columns["descripcion"].HeaderText = "Descripción";
            if (dgvCategorias.Columns.Contains("estado")) dgvCategorias.Columns["estado"].HeaderText = "Estado";
            if (dgvCategorias.Columns.Contains("total_productos")) dgvCategorias.Columns["total_productos"].HeaderText = "Productos";

            if (dgvCategorias.Columns.Contains("idcategoria")) dgvCategorias.Columns["idcategoria"].Width = 50;
            if (dgvCategorias.Columns.Contains("nombre")) dgvCategorias.Columns["nombre"].Width = 200;
            if (dgvCategorias.Columns.Contains("estado")) dgvCategorias.Columns["estado"].Width = 100;
            if (dgvCategorias.Columns.Contains("total_productos")) dgvCategorias.Columns["total_productos"].Width = 100;

            // Colorear estado
            foreach (DataGridViewRow row in dgvCategorias.Rows)
            {
                if (row.Cells["estado"].Value == null) continue;

                string estado = row.Cells["estado"].Value.ToString();
                if (estado == "ACTIVO")
                {
                    row.Cells["estado"].Style.ForeColor = Color.FromArgb(46, 204, 113);
                    row.Cells["estado"].Value = "✓ ACTIVO";
                }
                else
                {
                    row.Cells["estado"].Style.ForeColor = Color.FromArgb(231, 76, 60);
                    row.Cells["estado"].Value = "✗ INACTIVO";
                }
            }
        }

        private void ActualizarContador()
        {
            int total = dgvCategorias.Rows.Count;
            int activas = 0;

            foreach (DataGridViewRow row in dgvCategorias.Rows)
                if (row.Cells["estado"].Value != null &&
                    row.Cells["estado"].Value.ToString().Contains("ACTIVO"))
                    activas++;

            lblTotal.Text = $"Total: {total} categorías | Activas: {activas}";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarCategoria form = new FrmRegistrarCategoria();
            form.Insert = true;
            // ShowDialog() funciona aunque este form esté embebido (TopLevel=false)
            // porque FrmRegistrarCategoria sí tiene TopLevel=true por defecto
            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría para editar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarCategoria form = new FrmRegistrarCategoria();
            form.Edit = true;

            form.txtIdCategoria.Text = dgvCategorias.CurrentRow.Cells["idcategoria"].Value.ToString();
            form.txtNombre.Text = dgvCategorias.CurrentRow.Cells["nombre"].Value.ToString();
            form.txtDescripcion.Text = dgvCategorias.CurrentRow.Cells["descripcion"].Value.ToString();

            // *** Usar Contains() porque la celda ya fue decorada con "✓ ACTIVO" / "✗ INACTIVO" ***
            string estado = dgvCategorias.CurrentRow.Cells["estado"].Value.ToString();
            if (estado.Contains("ACTIVO"))
                form.rbtnActivo.Checked = true;
            else
                form.rbtnInactivo.Checked = true;

            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una categoría para eliminar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreCategoria = dgvCategorias.CurrentRow.Cells["nombre"].Value.ToString();

            // Si tiene productos asociados, no permitir eliminar
            if (dgvCategorias.Columns.Contains("total_productos"))
            {
                int totalProductos = Convert.ToInt32(dgvCategorias.CurrentRow.Cells["total_productos"].Value);
                if (totalProductos > 0)
                {
                    MessageBox.Show(
                        $"⚠️ No se puede eliminar '{nombreCategoria}'.\n\n" +
                        $"Tiene {totalProductos} producto(s) asociado(s).\n" +
                        $"Primero reasigne o elimine esos productos.",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (MessageBox.Show($"¿Eliminar la categoría '{nombreCategoria}'?",
                "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvCategorias.CurrentRow.Cells["idcategoria"].Value);
                    string resultado = CN_Categoria.Eliminar(id);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Categoría eliminada correctamente",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                    }
                    else
                        MessageBox.Show(resultado, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCategorias_DoubleClick(object sender, EventArgs e)
            => btnEditar_Click(sender, e);

        private void btnCerrar_Click(object sender, EventArgs e)
            => this.Close();
    }
}