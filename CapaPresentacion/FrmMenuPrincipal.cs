using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmMenuPrincipal : Form
    {
        private Form formularioActivo = null;
        private CN_Usuario cnUsuario = new CN_Usuario();

        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuario: {FrmLogin.UsuarioActual}";
            lblRol.Text = $"Rol: {FrmLogin.RolActual}";
            ConfigurarPermisosPorRol();
        }

        private void ConfigurarPermisosPorRol()
        {
            string rol = FrmLogin.RolActual;
            bool esAdmin = (rol == "ADMINISTRADOR");

            btnClientes.Visible = esAdmin || TryVer(rol, "Clientes");
            btnMascotas.Visible = esAdmin || TryVer(rol, "Mascotas");
            btnEmpleados.Visible = esAdmin || TryVer(rol, "Empleados");
            btnProductos.Visible = esAdmin || TryVer(rol, "Productos");
            btnProveedores.Visible = esAdmin || TryVer(rol, "Proveedores");
            panelGestion.Visible = btnClientes.Visible || btnMascotas.Visible ||
                                     btnEmpleados.Visible || btnProductos.Visible || btnProveedores.Visible;

            btnCitas.Visible = esAdmin || TryVer(rol, "Citas");
            btnConsultas.Visible = esAdmin || TryVer(rol, "Consultas");
            panelVeterinario.Visible = btnCitas.Visible || btnConsultas.Visible;

            // ── Permisos y visibilidad del módulo Caja (ventas/compras/historial)
            ConfigurarPermisosCaja(rol, esAdmin);

            btnAuditoria.Visible = esAdmin || TryVer(rol, "Auditoria");
            btnSesiones.Visible = esAdmin || TryVer(rol, "Sesiones");
            btnCategorias.Visible = esAdmin;
            panelAdministracion.Visible = btnAuditoria.Visible || btnSesiones.Visible || btnCategorias.Visible;
        }

        private bool TryVer(string rol, string modulo)
        {
            try { return cnUsuario.PuedeVer(rol, modulo); }
            catch { return false; }
        }

        private void AbrirFormularioHijo(Form formularioHijo)
        {
            if (formularioActivo != null)
                formularioActivo.Close();

            formularioActivo = formularioHijo;

            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(formularioHijo);
            panelContenedor.Tag = formularioHijo;

            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        // ── GESTIÓN ────────────────────────────────────────────────────────────

        private void btnClientes_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoClientes());

        private void btnMascotas_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoMascotas());

        private void btnEmpleados_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Empleados en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnProductos_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoProductos());

        private void btnProveedores_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Proveedores en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // ── VETERINARIO ────────────────────────────────────────────────────────

        private void btnCitas_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Citas en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnConsultas_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Consultas en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // ── CAJA ───────────────────────────────────────────────────────────────
        // ► Reemplazados placeholders por apertura de formularios de Caja ◄

        private void btnVentas_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmVentas());

        private void btnCompras_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmCompras());

        // El control se llama btnPagos en el Designer; aquí abrimos el historial de caja
        private void btnPagos_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmHistorialVentas());

        // ── ADMINISTRACIÓN ─────────────────────────────────────────────────────

        private void btnAuditoria_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmAuditoria());

        private void btnSesiones_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmSesiones());

        // *** CORRECCIÓN: ahora Categorías se abre embebido igual que Auditoría/Sesiones.
        //     FrmGestionCategorias.Designer.cs ya tiene FormBorderStyle.None. ***
        private void btnCategorias_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmGestionCategorias());

        // ── SESIÓN / SALIDA ────────────────────────────────────────────────────

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Sistema Veterinaria",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (FrmLogin.IdSesionActual > 0)
                {
                    CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual);
                    FrmLogin.IdSesionActual = 0;
                }
                this.Hide();
                FrmLogin login = new FrmLogin();
                login.Show();
                login.FormClosed += (s, args) => this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del sistema?", "Sistema Veterinaria",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (FrmLogin.IdSesionActual > 0)
                {
                    CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual);
                    FrmLogin.IdSesionActual = 0;
                }
                Application.Exit();
            }
        }

        // ── REFRESH PÚBLICOS ───────────────────────────────────────────────────

        public void RefrescarListadoClientes()
        {
            if (formularioActivo is FrmListadoClientes)
                ((FrmListadoClientes)formularioActivo).Mostrar();
        }

        public void RefrescarListadoMascotas()
        {
            if (formularioActivo is FrmListadoMascotas)
                ((FrmListadoMascotas)formularioActivo).Mostrar();
        }

        public void RefrescarListadoProductos()
        {
            if (formularioActivo is FrmListadoProductos)
                ((FrmListadoProductos)formularioActivo).Mostrar();
        }

        // ► AGREGADO: Configurar permisos del módulo Caja de forma centralizada
        private void ConfigurarPermisosCaja(string rol, bool esAdmin)
        {
            bool puedeVerVentas    = esAdmin || TryVer(rol, "Ventas");
            bool puedeVerCompras   = esAdmin || TryVer(rol, "Compras");
            bool puedeVerPagos     = esAdmin || TryVer(rol, "Pagos"); // o "Historial de Caja" según tu BD

            if (btnVentas    != null) btnVentas.Visible    = puedeVerVentas;
            if (btnCompras   != null) btnCompras.Visible   = puedeVerCompras;
            if (btnPagos     != null) btnPagos.Visible     = puedeVerPagos;

            panelCaja.Visible = (btnVentas.Visible || btnCompras.Visible || btnPagos.Visible);
        }

        // ► AGREGADO: Refrescar módulo Caja (ej. después de confirmar una venta)
        public void RefrescarModuloCaja()
        {
            try
            {
                // Si FrmListadoProductos está embebido en panelContenedor, refrescarlo
                var frmProd = panelContenedor.Controls.OfType<FrmListadoProductos>().FirstOrDefault();
                if (frmProd != null)
                {
                    frmProd.Mostrar();
                }
            }
            catch { /* silencioso para no romper el flujo del menú */ }
        }
    }
}