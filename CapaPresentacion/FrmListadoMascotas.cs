using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmListadoMascotas : Form
    {
        private CN_Usuario cnUsuario = new CN_Usuario();

        public FrmListadoMascotas()
        {
            InitializeComponent();
        }

        private void FrmListadoMascotas_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            Mostrar();
            ConfigurarPermisos();
        }

        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;
            bool esAdmin = (rol == "ADMINISTRADOR");

            btnnuevo.Visible = esAdmin || TryPerm(rol, "crear");
            btneditar.Visible = esAdmin || TryPerm(rol, "editar");
            btneliminar.Visible = esAdmin || TryPerm(rol, "eliminar");

            if (!btneditar.Visible && !btneliminar.Visible)
                dgvMascotas.ReadOnly = true;
        }

        private bool TryPerm(string rol, string tipo)
        {
            try
            {
                switch (tipo)
                {
                    case "crear": return cnUsuario.PuedeCrear(rol, "Mascotas");
                    case "editar": return cnUsuario.PuedeEditar(rol, "Mascotas");
                    case "eliminar": return cnUsuario.PuedeEliminar(rol, "Mascotas");
                    default: return false;
                }
            }
            catch { return false; }
        }

        public void Mostrar()
        {
            try
            {
                dgvMascotas.DataSource = CN_Mascota.Listar();
                ConfigurarColumnas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las mascotas: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvMascotas.Columns.Count == 0) return;

            if (dgvMascotas.Columns.Contains("idcliente"))
                dgvMascotas.Columns["idcliente"].Visible = false;

            void R(string c, string h) { if (dgvMascotas.Columns.Contains(c)) dgvMascotas.Columns[c].HeaderText = h; }
            R("idmascota", "ID"); R("nombre", "Nombre"); R("especie", "Especie");
            R("raza", "Raza"); R("sexo", "Sexo"); R("edad", "Edad");
            R("peso", "Peso (kg)"); R("color", "Color"); R("estado", "Estado");
            R("cliente", "Dueño");

            void W(string c, int w) { if (dgvMascotas.Columns.Contains(c)) dgvMascotas.Columns[c].Width = w; }
            W("idmascota", 50); W("edad", 60); W("peso", 80); W("sexo", 80); W("estado", 90);
        }

        private void ActualizarContador()
        {
            lblTotal.Text = $"Total de mascotas: {dgvMascotas.Rows.Count}";
        }

        // ── BÚSQUEDA ───────────────────────────────────────────────────────────

        /// <summary>
        /// Realiza la búsqueda según el radio button activo.
        /// </summary>
        private void Buscar()
        {
            try
            {
                string texto = txtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    Mostrar();
                    return;
                }

                DataTable datos;

                if (rbtnNombreMascota.Checked)
                    datos = CN_Mascota.BuscarNombre(texto);
                else
                    datos = CN_Mascota.BuscarPorNombreCliente(texto);

                dgvMascotas.DataSource = datos;
                ConfigurarColumnas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Búsqueda en tiempo real mientras el usuario escribe
        private void txtBuscar_TextChanged(object sender, EventArgs e)
            => Buscar();

        // Botón buscar (disparo manual)
        private void btnBuscar_Click(object sender, EventArgs e)
            => Buscar();

        // Al cambiar el radio button, re-ejecutar la búsqueda con el texto actual
        // (no necesita evento explícito — el TextChanged del txtBuscar se dispara
        //  si hay texto; si está vacío, simplemente mostrará todo de nuevo al escribir)

        // ── CRUD ───────────────────────────────────────────────────────────────

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarMascota form = new FrmRegistrarMascota();
            form.Insert = true;
            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (dgvMascotas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una mascota para editar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarMascota form = new FrmRegistrarMascota();
            form.Edit = true;

            DataGridViewRow row = dgvMascotas.CurrentRow;
            form.txtIdMascota.Text = row.Cells["idmascota"].Value.ToString();
            form.txtNombre.Text = row.Cells["nombre"].Value.ToString();
            form.txtRaza.Text = row.Cells["raza"].Value.ToString();
            form.nudEdad.Value = Convert.ToDecimal(row.Cells["edad"].Value);
            form.nudPeso.Value = Convert.ToDecimal(row.Cells["peso"].Value);
            form.txtColor.Text = row.Cells["color"].Value.ToString();

            string especie = row.Cells["especie"].Value.ToString();
            if (especie == "Perro") form.rbtnPerro.Checked = true;
            else if (especie == "Gato") form.rbtnGato.Checked = true;
            else form.rbtnOtro.Checked = true;

            string sexo = row.Cells["sexo"].Value.ToString();
            if (sexo == "Macho") form.rbtnMacho.Checked = true;
            else form.rbtnHembra.Checked = true;

            string estado = row.Cells["estado"].Value.ToString();
            if (estado == "ACTIVO") form.rbtnActivo.Checked = true;
            else form.rbtnInactivo.Checked = true;

            form.IdClienteSeleccionado = Convert.ToInt32(row.Cells["idcliente"].Value);

            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (dgvMascotas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una mascota para dar de baja",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = dgvMascotas.CurrentRow.Cells["nombre"].Value.ToString();
            string cliente = dgvMascotas.CurrentRow.Cells["cliente"].Value.ToString();

            if (MessageBox.Show(
                    $"¿Dar de baja a la mascota?\n\nMascota: {nombre}\nDueño: {cliente}\n\n" +
                    $"Será marcada como INACTIVA.",
                    "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvMascotas.CurrentRow.Cells["idmascota"].Value);
                    string resultado = CN_Mascota.Eliminar(id);

                    if (resultado == "OK")
                    {
                        MessageBox.Show("✅ Mascota dada de baja correctamente",
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Mostrar();
                    }
                    else
                        MessageBox.Show(resultado,
                            "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message,
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvMascotas_DoubleClick(object sender, EventArgs e)
        {
            if (TryPerm(FrmLogin.RolActual, "editar") || FrmLogin.RolActual == "ADMINISTRADOR")
                btneditar_Click(sender, e);
        }
    }
}