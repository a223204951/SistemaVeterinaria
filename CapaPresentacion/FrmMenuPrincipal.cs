using System;
using System.Data;
using System.Drawing;
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
            CargarNotificaciones();
        }

        // ── Permisos ──────────────────────────────────────────────────────────
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

            btnVentas.Visible = esAdmin || TryVer(rol, "Ventas");
            btnCompras.Visible = esAdmin || TryVer(rol, "Compras");
            btnPagos.Visible = esAdmin || TryVer(rol, "Pagos");
            panelCaja.Visible = btnVentas.Visible || btnCompras.Visible || btnPagos.Visible;

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

        // ── Notificaciones de stock bajo ──────────────────────────────────────
        private void CargarNotificaciones()
        {
            try
            {
                panelNotificaciones.Controls.Clear();

                DataTable stockBajo = CN_Producto.ObtenerProductosStockBajo();
                DataTable proxVencer = CN_Producto.ObtenerProductosProximosVencer();

                int totalAlertas = (stockBajo?.Rows.Count ?? 0) + (proxVencer?.Rows.Count ?? 0);

                if (totalAlertas == 0)
                {
                    Label lblOk = new Label
                    {
                        Text = "✅ Sin alertas pendientes",
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                        ForeColor = Color.FromArgb(46, 204, 113),
                        AutoSize = true,
                        Location = new Point(8, 8)
                    };
                    panelNotificaciones.Controls.Add(lblOk);
                    lblNotifBadge.Visible = false;
                    return;
                }

                lblNotifBadge.Text = totalAlertas.ToString();
                lblNotifBadge.Visible = true;

                int y = 5;

                // Stock bajo
                if (stockBajo != null)
                {
                    foreach (DataRow row in stockBajo.Rows)
                    {
                        string nombre = row["nombre"].ToString();
                        int stock = Convert.ToInt32(row["stock"]);

                        LinkLabel lnk = new LinkLabel
                        {
                            Text = $"⚠️ {nombre} — stock: {stock}",
                            Font = new Font("Segoe UI", 8.5F),
                            ForeColor = stock == 0
                                ? Color.FromArgb(231, 76, 60)
                                : Color.FromArgb(230, 126, 34),
                            AutoSize = false,
                            Size = new Size(panelNotificaciones.Width - 16, 22),
                            Location = new Point(8, y),
                            Tag = "compras"
                        };
                        lnk.LinkColor = lnk.ForeColor;
                        lnk.ActiveLinkColor = Color.White;
                        lnk.LinkBehavior = LinkBehavior.HoverUnderline;
                        lnk.LinkClicked += Notificacion_Click;
                        panelNotificaciones.Controls.Add(lnk);
                        y += 24;
                    }
                }

                // Próximos a vencer
                if (proxVencer != null)
                {
                    foreach (DataRow row in proxVencer.Rows)
                    {
                        string nombre = row["nombre"].ToString();
                        DateTime fecha = Convert.ToDateTime(row["fecha_vencimiento"]);
                        int dias = (int)(fecha - DateTime.Today).TotalDays;

                        LinkLabel lnk = new LinkLabel
                        {
                            Text = $"🕐 {nombre} — vence en {dias}d",
                            Font = new Font("Segoe UI", 8.5F),
                            ForeColor = Color.FromArgb(231, 76, 60),
                            AutoSize = false,
                            Size = new Size(panelNotificaciones.Width - 16, 22),
                            Location = new Point(8, y),
                            Tag = "productos"
                        };
                        lnk.LinkColor = lnk.ForeColor;
                        lnk.LinkBehavior = LinkBehavior.HoverUnderline;
                        lnk.LinkClicked += Notificacion_Click;
                        panelNotificaciones.Controls.Add(lnk);
                        y += 24;
                    }
                }

                panelNotificaciones.AutoScrollMinSize = new Size(0, y + 5);
            }
            catch { /* Silencioso — no bloquear carga del menú */ }
        }

        // Al hacer clic en una notificación redirige al módulo correspondiente
        private void Notificacion_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string destino = (sender as LinkLabel)?.Tag?.ToString() ?? "";
            if (destino == "compras")
                AbrirFormularioHijo(new FrmCompras());
            else if (destino == "productos")
                AbrirFormularioHijo(new FrmListadoProductos());
        }

        // ── Abrir formulario embebido ─────────────────────────────────────────
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

        // ── GESTIÓN ───────────────────────────────────────────────────────────
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
            => AbrirFormularioHijo(new FrmListadoProveedores());

        // ── VETERINARIO ───────────────────────────────────────────────────────
        private void btnCitas_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Citas en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnConsultas_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Consultas en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // ── CAJA ──────────────────────────────────────────────────────────────
        private void btnVentas_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmVentas());

        private void btnCompras_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmCompras());

        private void btnPagos_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmHistorialVentas());

        // ── ADMINISTRACIÓN ────────────────────────────────────────────────────
        private void btnAuditoria_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmAuditoria());

        private void btnSesiones_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmSesiones());

        private void btnCategorias_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmGestionCategorias());

        // ── SESIÓN / SALIDA ───────────────────────────────────────────────────
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Sistema Veterinaria",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (FrmLogin.IdSesionActual > 0)
                { CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual); FrmLogin.IdSesionActual = 0; }
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
                { CN_Sesion.CerrarSesion(FrmLogin.IdSesionActual); FrmLogin.IdSesionActual = 0; }
                Application.Exit();
            }
        }

        // ── Refresh públicos ──────────────────────────────────────────────────
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
    }
}