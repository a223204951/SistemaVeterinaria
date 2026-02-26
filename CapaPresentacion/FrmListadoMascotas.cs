using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE LISTADO DE MASCOTAS
    /// Muestra todas las mascotas registradas con opciones de búsqueda
    /// Control de permisos según el rol del usuario
    /// </summary>
    public partial class FrmListadoMascotas : Form
    {
        // =============================================
        // INSTANCIA DE LA CAPA DE NEGOCIO PARA VERIFICAR PERMISOS
        // =============================================
        private CN_Usuario cnUsuario = new CN_Usuario();

        public FrmListadoMascotas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// Configura permisos y carga los datos iniciales
        /// </summary>
        private void FrmListadoMascotas_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;

            Mostrar();
            ConfigurarPermisos();

            // *** YA NO SE LLAMA CargarClientesBusqueda() — se eliminó el ComboBox ***
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR PERMISOS SEGÚN EL ROL
        /// </summary>
        private void ConfigurarPermisos()
        {
            string rol = FrmLogin.RolActual;

            bool puedeCrear = cnUsuario.PuedeCrear(rol, "Mascotas");
            bool puedeEditar = cnUsuario.PuedeEditar(rol, "Mascotas");
            bool puedeEliminar = cnUsuario.PuedeEliminar(rol, "Mascotas");

            btnnuevo.Visible = puedeCrear;
            btneditar.Visible = puedeEditar;
            btneliminar.Visible = puedeEliminar;

            if (!puedeEditar && !puedeEliminar)
            {
                dgvMascotas.ReadOnly = true;
                dgvMascotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        /// <summary>
        /// MÉTODO PÚBLICO PARA MOSTRAR TODAS LAS MASCOTAS
        /// </summary>
        public void Mostrar()
        {
            try
            {
                DataTable datos = CN_Mascota.Listar();
                dgvMascotas.DataSource = datos;
                ConfigurarColumnas();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las mascotas: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR LA APARIENCIA DE LAS COLUMNAS
        /// </summary>
        private void ConfigurarColumnas()
        {
            if (dgvMascotas.Columns.Count > 0)
            {
                if (dgvMascotas.Columns.Contains("idcliente"))
                    dgvMascotas.Columns["idcliente"].Visible = false;

                if (dgvMascotas.Columns.Contains("idmascota"))
                    dgvMascotas.Columns["idmascota"].HeaderText = "ID";
                if (dgvMascotas.Columns.Contains("nombre"))
                    dgvMascotas.Columns["nombre"].HeaderText = "Nombre";
                if (dgvMascotas.Columns.Contains("especie"))
                    dgvMascotas.Columns["especie"].HeaderText = "Especie";
                if (dgvMascotas.Columns.Contains("raza"))
                    dgvMascotas.Columns["raza"].HeaderText = "Raza";
                if (dgvMascotas.Columns.Contains("sexo"))
                    dgvMascotas.Columns["sexo"].HeaderText = "Sexo";
                if (dgvMascotas.Columns.Contains("edad"))
                    dgvMascotas.Columns["edad"].HeaderText = "Edad";
                if (dgvMascotas.Columns.Contains("peso"))
                    dgvMascotas.Columns["peso"].HeaderText = "Peso (kg)";
                if (dgvMascotas.Columns.Contains("color"))
                    dgvMascotas.Columns["color"].HeaderText = "Color";
                if (dgvMascotas.Columns.Contains("estado"))
                    dgvMascotas.Columns["estado"].HeaderText = "Estado";
                if (dgvMascotas.Columns.Contains("cliente"))
                    dgvMascotas.Columns["cliente"].HeaderText = "Dueño";

                dgvMascotas.Columns["idmascota"].Width = 50;
                dgvMascotas.Columns["edad"].Width = 60;
                dgvMascotas.Columns["peso"].Width = 80;
                dgvMascotas.Columns["sexo"].Width = 80;
                dgvMascotas.Columns["estado"].Width = 90;
            }
        }

        /// <summary>
        /// ACTUALIZAR EL CONTADOR DE MASCOTAS
        /// </summary>
        private void ActualizarContador()
        {
            int total = dgvMascotas.Rows.Count;
            lblTotal.Text = $"Total de mascotas: {total}";
        }

        // =============================================
        // BÚSQUEDA POR NOMBRE
        // =============================================

        private void BuscarNombre()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBuscarNombre.Text))
                {
                    // Si también hay texto en el buscador de dueño, respetar ese filtro
                    if (!string.IsNullOrWhiteSpace(txtBuscarCliente.Text))
                        BuscarPorCliente();
                    else
                        Mostrar();
                }
                else
                {
                    DataTable datos = CN_Mascota.BuscarNombre(txtBuscarNombre.Text.Trim());
                    dgvMascotas.DataSource = datos;
                    ConfigurarColumnas();
                    ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // BÚSQUEDA POR DUEÑO (ahora por texto / nombre)
        // =============================================

        /// <summary>
        /// Busca mascotas cuyo dueño contenga el texto ingresado.
        /// Llama a CN_Mascota.BuscarPorNombreCliente que debes tener en CapaNegocio.
        /// </summary>
        private void BuscarPorCliente()
        {
            try
            {
                string texto = txtBuscarCliente.Text.Trim();

                if (string.IsNullOrWhiteSpace(texto))
                {
                    Mostrar();
                }
                else
                {
                    // Usa el nuevo método que busca por nombre de cliente (texto)
                    DataTable datos = CN_Mascota.BuscarPorNombreCliente(texto);
                    dgvMascotas.DataSource = datos;
                    ConfigurarColumnas();
                    ActualizarContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =============================================
        // EVENTOS DE CONTROLES
        // =============================================

        private void btnBuscarNombre_Click(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        /// <summary>Búsqueda en tiempo real mientras se escribe el nombre de la mascota</summary>
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        /// <summary>Búsqueda en tiempo real mientras se escribe el nombre del dueño</summary>
        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            BuscarPorCliente();
        }

        /// <summary>Botón buscar dueño (disparo manual)</summary>
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            BuscarPorCliente();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscarNombre.Clear();
            txtBuscarCliente.Clear();   // *** limpiar el nuevo TextBox ***
            Mostrar();
        }

        // =============================================
        // CRUD — NUEVO / EDITAR / ELIMINAR
        // =============================================

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeCrear(FrmLogin.RolActual, "Mascotas"))
            {
                MessageBox.Show("No tiene permisos para registrar mascotas",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmRegistrarMascota form = new FrmRegistrarMascota();
            form.Insert = true;

            if (form.ShowDialog() == DialogResult.OK)
                Mostrar();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (!cnUsuario.PuedeEditar(FrmLogin.RolActual, "Mascotas"))
            {
                MessageBox.Show("No tiene permisos para editar mascotas",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvMascotas.SelectedRows.Count > 0)
            {
                FrmRegistrarMascota form = new FrmRegistrarMascota();
                form.Edit = true;

                form.txtIdMascota.Text = dgvMascotas.CurrentRow.Cells["idmascota"].Value.ToString();
                form.txtNombre.Text = dgvMascotas.CurrentRow.Cells["nombre"].Value.ToString();
                form.txtRaza.Text = dgvMascotas.CurrentRow.Cells["raza"].Value.ToString();
                form.nudEdad.Value = Convert.ToDecimal(dgvMascotas.CurrentRow.Cells["edad"].Value);
                form.nudPeso.Value = Convert.ToDecimal(dgvMascotas.CurrentRow.Cells["peso"].Value);
                form.txtColor.Text = dgvMascotas.CurrentRow.Cells["color"].Value.ToString();

                string especie = dgvMascotas.CurrentRow.Cells["especie"].Value.ToString();
                if (especie == "Perro") form.rbtnPerro.Checked = true;
                else if (especie == "Gato") form.rbtnGato.Checked = true;
                else form.rbtnOtro.Checked = true;

                string sexo = dgvMascotas.CurrentRow.Cells["sexo"].Value.ToString();
                if (sexo == "Macho") form.rbtnMacho.Checked = true;
                else form.rbtnHembra.Checked = true;

                string estado = dgvMascotas.CurrentRow.Cells["estado"].Value.ToString();
                if (estado == "ACTIVO") form.rbtnActivo.Checked = true;
                else form.rbtnInactivo.Checked = true;

                form.IdClienteSeleccionado = Convert.ToInt32(dgvMascotas.CurrentRow.Cells["idcliente"].Value);

                if (form.ShowDialog() == DialogResult.OK)
                    Mostrar();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una mascota para editar",
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cnUsuario.PuedeEliminar(FrmLogin.RolActual, "Mascotas"))
                {
                    MessageBox.Show("No tiene permisos para eliminar mascotas",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvMascotas.SelectedRows.Count > 0)
                {
                    string nombreMascota = dgvMascotas.CurrentRow.Cells["nombre"].Value.ToString();
                    string cliente = dgvMascotas.CurrentRow.Cells["cliente"].Value.ToString();

                    DialogResult opcion = MessageBox.Show(
                        $"¿Está seguro que desea dar de baja a la mascota?\n\n" +
                        $"Mascota: {nombreMascota}\n" +
                        $"Dueño: {cliente}\n\n" +
                        $"Nota: La mascota será marcada como INACTIVA pero no se eliminará del sistema.",
                        "Sistema Veterinaria", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (opcion == DialogResult.Yes)
                    {
                        int idmascota = Convert.ToInt32(dgvMascotas.CurrentRow.Cells["idmascota"].Value);
                        string resultado = CN_Mascota.Eliminar(idmascota);

                        if (resultado == "OK")
                        {
                            MessageBox.Show("✅ Mascota dada de baja correctamente",
                                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Mostrar();
                        }
                        else
                        {
                            MessageBox.Show(resultado,
                                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione una mascota para dar de baja",
                        "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMascotas_DoubleClick(object sender, EventArgs e)
        {
            if (cnUsuario.PuedeEditar(FrmLogin.RolActual, "Mascotas"))
                btneditar_Click(sender, e);
        }

        private void FrmListadoMascotas_Load_1(object sender, EventArgs e) { }
    }
}