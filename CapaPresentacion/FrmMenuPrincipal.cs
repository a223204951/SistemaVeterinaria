using System;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    /// <summary>
    /// FORMULARIO DE MENÚ PRINCIPAL (DASHBOARD)
    /// Centro de control del sistema con navegación basada en permisos
    /// Muestra u oculta opciones según el rol del usuario
    /// </summary>
    public partial class FrmMenuPrincipal : Form
    {
        // =============================================
        // VARIABLE PARA CONTROLAR EL FORMULARIO HIJO ACTIVO
        // =============================================
        private Form formularioActivo = null;

        // =============================================
        // INSTANCIA DE LA CAPA DE NEGOCIO PARA VERIFICAR PERMISOS
        // =============================================
        private CN_Usuario cnUsuario = new CN_Usuario();

        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// EVENTO LOAD DEL FORMULARIO
        /// Configura la interfaz según el rol del usuario
        /// </summary>
        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            // =============================================
            // MOSTRAR INFORMACIÓN DEL USUARIO ACTUAL
            // =============================================
            lblUsuario.Text = $"Usuario: {FrmLogin.UsuarioActual}";
            lblRol.Text = $"Rol: {FrmLogin.RolActual}";

            // =============================================
            // CONFIGURAR PERMISOS SEGÚN EL ROL
            // =============================================
            ConfigurarPermisosPorRol();
        }

        /// <summary>
        /// MÉTODO PARA CONFIGURAR LA VISIBILIDAD DE LOS MÓDULOS SEGÚN EL ROL
        /// Cada rol tiene acceso a diferentes secciones del sistema
        /// </summary>
        private void ConfigurarPermisosPorRol()
        {
            string rol = FrmLogin.RolActual;

            // =============================================
            // PANEL DE GESTIÓN (Clientes, Mascotas, Empleados, Productos, Proveedores)
            // =============================================
            bool verClientes = cnUsuario.PuedeVer(rol, "Clientes");
            bool verMascotas = cnUsuario.PuedeVer(rol, "Mascotas");
            bool verEmpleados = cnUsuario.PuedeVer(rol, "Empleados");
            bool verProductos = cnUsuario.PuedeVer(rol, "Productos");
            bool verProveedores = cnUsuario.PuedeVer(rol, "Proveedores");

            // Mostrar u ocultar botones del panel de gestión
            btnClientes.Visible = verClientes;
            btnMascotas.Visible = verMascotas;
            btnEmpleados.Visible = verEmpleados;
            btnProductos.Visible = verProductos;
            btnProveedores.Visible = verProveedores;

            // Si ningún botón es visible, ocultar todo el panel
            panelGestion.Visible = verClientes || verMascotas || verEmpleados || verProductos || verProveedores;

            // =============================================
            // PANEL VETERINARIO (Citas, Consultas)
            // =============================================
            bool verCitas = cnUsuario.PuedeVer(rol, "Citas");
            bool verConsultas = cnUsuario.PuedeVer(rol, "Consultas");

            btnCitas.Visible = verCitas;
            btnConsultas.Visible = verConsultas;

            // Si ningún botón es visible, ocultar todo el panel
            panelVeterinario.Visible = verCitas || verConsultas;

            // =============================================
            // PANEL DE CAJA (Ventas, Compras, Pagos)
            // =============================================
            bool verVentas = cnUsuario.PuedeVer(rol, "Ventas");
            bool verCompras = cnUsuario.PuedeVer(rol, "Compras");
            bool verPagos = cnUsuario.PuedeVer(rol, "Pagos");

            btnVentas.Visible = verVentas;
            btnCompras.Visible = verCompras;
            btnPagos.Visible = verPagos;

            // Si ningún botón es visible, ocultar todo el panel
            panelCaja.Visible = verVentas || verCompras || verPagos;

            // =============================================
            // PANEL DE ADMINISTRACIÓN (Auditoría, Sesiones)
            // Solo visible para ADMINISTRADOR
            // =============================================
            bool verAuditoria = cnUsuario.PuedeVer(rol, "Auditoria");
            bool verSesiones = cnUsuario.PuedeVer(rol, "Sesiones");

            btnAuditoria.Visible = verAuditoria;
            btnSesiones.Visible = verSesiones;

            // Si ningún botón es visible, ocultar todo el panel
            panelAdministracion.Visible = verAuditoria || verSesiones;
        }

        /// <summary>
        /// MÉTODO PARA ABRIR FORMULARIOS HIJOS DENTRO DEL PANEL CONTENEDOR
        /// Evita tener múltiples ventanas abiertas
        /// </summary>
        private void AbrirFormularioHijo(Form formularioHijo)
        {
            // =============================================
            // CERRAR EL FORMULARIO ACTIVO SI EXISTE
            // =============================================
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            // =============================================
            // ESTABLECER EL NUEVO FORMULARIO COMO ACTIVO
            // =============================================
            formularioActivo = formularioHijo;

            // =============================================
            // CONFIGURAR EL FORMULARIO HIJO PARA QUE SE MUESTRE DENTRO DEL PANEL
            // =============================================
            formularioHijo.TopLevel = false; // No es un formulario de nivel superior
            formularioHijo.FormBorderStyle = FormBorderStyle.None; // Sin bordes
            formularioHijo.Dock = DockStyle.Fill; // Llenar todo el panel contenedor

            // =============================================
            // LIMPIAR EL PANEL Y AGREGAR EL NUEVO FORMULARIO
            // =============================================
            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(formularioHijo);
            panelContenedor.Tag = formularioHijo;

            // Mostrar el formulario
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        // =============================================
        // EVENTOS CLICK DE BOTONES DEL PANEL DE GESTIÓN
        // =============================================

        /// <summary>
        /// ABRIR MÓDULO DE CLIENTES
        /// </summary>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmListadoClientes());
        }

        /// <summary>
        /// ABRIR MÓDULO DE MASCOTAS
        /// </summary>
        private void btnMascotas_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmListadoMascotas());
        }

        /// <summary>
        /// ABRIR MÓDULO DE EMPLEADOS
        /// </summary>
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Empleados en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// ABRIR MÓDULO DE PRODUCTOS
        /// </summary>
        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmListadoProductos());
        }

        /// <summary>
        /// ABRIR MÓDULO DE PROVEEDORES
        /// </summary>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Proveedores en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =============================================
        // EVENTOS CLICK DE BOTONES DEL PANEL VETERINARIO
        // =============================================

        /// <summary>
        /// ABRIR MÓDULO DE CITAS
        /// </summary>
        private void btnCitas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Citas en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// ABRIR MÓDULO DE CONSULTAS
        /// </summary>
        private void btnConsultas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Consultas en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =============================================
        // EVENTOS CLICK DE BOTONES DEL PANEL DE CAJA
        // =============================================

        /// <summary>
        /// ABRIR MÓDULO DE VENTAS
        /// </summary>
        private void btnVentas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Ventas en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// ABRIR MÓDULO DE COMPRAS
        /// </summary>
        private void btnCompras_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Compras en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// ABRIR MÓDULO DE PAGOS
        /// </summary>
        private void btnPagos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Módulo de Pagos en desarrollo",
                "Sistema Veterinaria",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // =============================================
        // EVENTOS CLICK DE BOTONES DEL PANEL DE ADMINISTRACIÓN
        // =============================================

        /// <summary>
        /// ABRIR MÓDULO DE AUDITORÍA (SOLO ADMIN)
        /// </summary>
        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmAuditoria());
        }

        /// <summary>
        /// ABRIR MÓDULO DE SESIONES (SOLO ADMIN)
        /// </summary>
        private void btnSesiones_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(new FrmSesiones());
        }

        // =============================================
        // EVENTOS DE CIERRE DE SESIÓN Y SALIDA
        // =============================================

        /// <summary>
        /// CERRAR SESIÓN Y VOLVER AL LOGIN
        /// </summary>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Sistema Veterinaria",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // =============================================
                // REGISTRAR CIERRE DE SESIÓN EN LA BASE DE DATOS
                // =============================================
                if (FrmLogin.IdSesionActual > 0)
                {
                    CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual);
                    FrmLogin.IdSesionActual = 0;
                }

                // =============================================
                // VOLVER AL LOGIN
                // =============================================
                this.Hide();
                FrmLogin login = new FrmLogin();
                login.Show();

                // Al cerrar el login, cerrar toda la aplicación
                login.FormClosed += (s, args) => this.Close();
            }
        }

        /// <summary>
        /// SALIR COMPLETAMENTE DEL SISTEMA
        /// </summary>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea salir del sistema?",
                "Sistema Veterinaria",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // =============================================
                // REGISTRAR CIERRE DE SESIÓN
                // =============================================
                if (FrmLogin.IdSesionActual > 0)
                {
                    CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual);
                    FrmLogin.IdSesionActual = 0;
                }

                // CERRAR LA APLICACIÓN COMPLETA
                Application.Exit();
            }
        }

        // =============================================
        // MÉTODOS PÚBLICOS PARA REFRESCAR LISTADOS
        // =============================================

        /// <summary>
        /// MÉTODO PÚBLICO PARA REFRESCAR EL LISTADO DE CLIENTES
        /// Llamado desde otros formularios después de guardar cambios
        /// </summary>
        public void RefrescarListadoClientes()
        {
            if (formularioActivo != null && formularioActivo is FrmListadoClientes)
            {
                ((FrmListadoClientes)formularioActivo).Mostrar();
            }
        }

        /// <summary>
        /// MÉTODO PÚBLICO PARA REFRESCAR EL LISTADO DE MASCOTAS
        /// </summary>
        public void RefrescarListadoMascotas()
        {
            if (formularioActivo != null && formularioActivo is FrmListadoMascotas)
            {
                ((FrmListadoMascotas)formularioActivo).Mostrar();
            }
        }

        /// <summary>
        /// MÉTODO PÚBLICO PARA REFRESCAR EL LISTADO DE PRODUCTOS
        /// </summary>
        public void RefrescarListadoProductos()
        {
            if (formularioActivo != null && formularioActivo is FrmListadoProductos)
            {
                ((FrmListadoProductos)formularioActivo).Mostrar();
            }
        }
    }
}