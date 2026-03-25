using System;
using System.Collections.Generic;
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

            // Logo/título → volver a la pantalla de inicio con notificaciones
            lblTitulo.Cursor = Cursors.Hand;
            lblTitulo.Click += (s, ev) => MostrarInicio();
        }

        // ─────────────────────────────────────────────────────────────────────
        // PANTALLA DE INICIO
        // ─────────────────────────────────────────────────────────────────────
        /// <summary>
        /// Cierra el formulario embebido activo y muestra la pantalla de inicio
        /// con el panel de notificaciones. Lo usa el clic en el logo y IrARestock().
        /// </summary>
        public void MostrarInicio()
        {
            if (formularioActivo != null)
            {
                formularioActivo.Close();
                formularioActivo = null;
            }

            panelContenedor.Controls.Clear();
            panelContenedor.Controls.Add(panelInicio);
            panelInicio.BringToFront();

            // Refrescar alertas para que estén al día
            CargarNotificaciones();
        }

        // ─────────────────────────────────────────────────────────────────────
        // PERMISOS
        // ─────────────────────────────────────────────────────────────────────
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
                                   btnEmpleados.Visible || btnProductos.Visible ||
                                   btnProveedores.Visible;

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
            panelAdministracion.Visible = btnAuditoria.Visible || btnSesiones.Visible ||
                                          btnCategorias.Visible;
        }

        private bool TryVer(string rol, string modulo)
        {
            try { return cnUsuario.PuedeVer(rol, modulo); }
            catch { return false; }
        }

        // ─────────────────────────────────────────────────────────────────────
        // NOTIFICACIONES
        // ─────────────────────────────────────────────────────────────────────
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

                // ── Stock bajo ────────────────────────────────────────────────
                if (stockBajo != null)
                {
                    foreach (DataRow row in stockBajo.Rows)
                    {
                        string nombre = row["nombre"].ToString();
                        int idproducto = Convert.ToInt32(row["idproducto"]);
                        int stock = Convert.ToInt32(row["stock"]);

                        LinkLabel lnk = new LinkLabel
                        {
                            Text = $"⚠️ {nombre} — stock: {stock}  (clic para reponer)",
                            Font = new Font("Segoe UI", 8.5F),
                            ForeColor = stock == 0
                                ? Color.FromArgb(231, 76, 60)
                                : Color.FromArgb(230, 126, 34),
                            AutoSize = false,
                            Size = new Size(panelNotificaciones.Width - 16, 22),
                            Location = new Point(8, y),
                            Tag = idproducto   // guardamos el idproducto como int
                        };
                        lnk.LinkColor = lnk.ForeColor;
                        lnk.ActiveLinkColor = Color.White;
                        lnk.LinkBehavior = LinkBehavior.HoverUnderline;
                        lnk.Cursor = Cursors.Hand;
                        lnk.LinkClicked += NotificacionStockBajo_Click;
                        panelNotificaciones.Controls.Add(lnk);
                        y += 24;
                    }
                }

                // ── Próximos a vencer ─────────────────────────────────────────
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
                            Tag = "vencimiento"
                        };
                        lnk.LinkColor = lnk.ForeColor;
                        lnk.LinkBehavior = LinkBehavior.HoverUnderline;
                        lnk.Cursor = Cursors.Hand;
                        lnk.LinkClicked += NotificacionVencimiento_Click;
                        panelNotificaciones.Controls.Add(lnk);
                        y += 24;
                    }
                }

                panelNotificaciones.AutoScrollMinSize = new Size(0, y + 5);
            }
            catch { /* Silencioso — no bloquear carga del menú */ }
        }

        // ─────────────────────────────────────────────────────────────────────
        // CLICK NOTIFICACIÓN STOCK BAJO → compra embebida y pre-cargada
        // ─────────────────────────────────────────────────────────────────────
        private void NotificacionStockBajo_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // 1. Obtener todos los productos con stock bajo
                DataTable todosStockBajo = CN_Producto.ObtenerProductosStockBajo();
                if (todosStockBajo == null || todosStockBajo.Rows.Count == 0) return;

                // 2. Obtener idproveedor de cada producto desde el listado completo
                DataTable todosProductos = CN_Producto.Listar();
                var proveedorDeProducto = new Dictionary<int, int>();
                foreach (DataRow row in todosProductos.Rows)
                {
                    if (row["idproveedor"] == DBNull.Value) continue;
                    proveedorDeProducto[Convert.ToInt32(row["idproducto"])] =
                        Convert.ToInt32(row["idproveedor"]);
                }

                // 3. Construir tabla solo con los que tienen proveedor
                DataTable conProveedor = new DataTable();
                conProveedor.Columns.Add("idproducto", typeof(int));
                conProveedor.Columns.Add("nombre", typeof(string));
                conProveedor.Columns.Add("stock", typeof(int));
                conProveedor.Columns.Add("idproveedor", typeof(int));

                foreach (DataRow row in todosStockBajo.Rows)
                {
                    int idprod = Convert.ToInt32(row["idproducto"]);
                    if (!proveedorDeProducto.ContainsKey(idprod)) continue;
                    conProveedor.Rows.Add(
                        idprod,
                        row["nombre"].ToString(),
                        Convert.ToInt32(row["stock"]),
                        proveedorDeProducto[idprod]);
                }

                if (conProveedor.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "⚠️ Los productos con stock bajo no tienen proveedor asignado.\n\n" +
                        "Asigne un proveedor desde Gestión → Productos → Editar.",
                        "Sin proveedor asignado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. Elegir proveedor principal (el que más productos concentra)
                var conteo = new Dictionary<int, int>();
                foreach (DataRow row in conProveedor.Rows)
                {
                    int idpv = Convert.ToInt32(row["idproveedor"]);
                    conteo[idpv] = conteo.ContainsKey(idpv) ? conteo[idpv] + 1 : 1;
                }
                int idProvPrincipal = -1, maxCount = 0;
                foreach (var kvp in conteo)
                    if (kvp.Value > maxCount) { maxCount = kvp.Value; idProvPrincipal = kvp.Key; }

                // 5. Filtrar solo los de ese proveedor
                DataTable productosParaCompra = conProveedor.Clone();
                foreach (DataRow row in conProveedor.Rows)
                    if (Convert.ToInt32(row["idproveedor"]) == idProvPrincipal)
                        productosParaCompra.ImportRow(row);

                int sinProveedor = todosStockBajo.Rows.Count - conProveedor.Rows.Count;
                int otrosProv = conProveedor.Rows.Count - productosParaCompra.Rows.Count;

                string resumen =
                    $"Se encontraron {todosStockBajo.Rows.Count} producto(s) con stock bajo.\n\n" +
                    $"✅ Se cargará una compra con {productosParaCompra.Rows.Count} producto(s) " +
                    "del proveedor principal.";
                if (sinProveedor > 0)
                    resumen += $"\n⚠️ {sinProveedor} producto(s) sin proveedor asignado (omitidos).";
                if (otrosProv > 0)
                    resumen += $"\n⚠️ {otrosProv} producto(s) son de otro proveedor " +
                               "(créales una compra separada manualmente).";
                resumen += "\n\n¿Continuar?";

                if (MessageBox.Show(resumen, "Compra Automática — Stock Bajo",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                // 6. Abrir FrmCompras EMBEBIDO con los datos
                AbrirFormularioHijo(new FrmCompras(idProvPrincipal, productosParaCompra));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la notificación: " + ex.Message,
                    "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotificacionVencimiento_Click(object sender, LinkLabelLinkClickedEventArgs e)
            => AbrirFormularioHijo(new FrmListadoProductos());

        // ─────────────────────────────────────────────────────────────────────
        // ABRIR FORMULARIO EMBEBIDO
        // ─────────────────────────────────────────────────────────────────────
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

        // ─────────────────────────────────────────────────────────────────────
        // GESTIÓN
        // ─────────────────────────────────────────────────────────────────────
        private void btnClientes_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoClientes());

        private void btnMascotas_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoMascotas());

        private void btnEmpleados_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoEmpleados());

        private void btnProductos_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoProductos());

        private void btnProveedores_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmListadoProveedores());

        // ─────────────────────────────────────────────────────────────────────
        // VETERINARIO
        // ─────────────────────────────────────────────────────────────────────
        private void btnCitas_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Citas en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnConsultas_Click(object sender, EventArgs e)
            => MessageBox.Show("Módulo de Consultas en desarrollo",
                "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // ─────────────────────────────────────────────────────────────────────
        // CAJA
        // ─────────────────────────────────────────────────────────────────────
        private void btnVentas_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmVentas());

        private void btnCompras_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmCompras());

        private void btnPagos_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmHistorialVentas());

        // ─────────────────────────────────────────────────────────────────────
        // ADMINISTRACIÓN
        // ─────────────────────────────────────────────────────────────────────
        private void btnAuditoria_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmAuditoria());

        private void btnSesiones_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmSesiones());

        private void btnCategorias_Click(object sender, EventArgs e)
            => AbrirFormularioHijo(new FrmGestionCategorias());

        // ─────────────────────────────────────────────────────────────────────
        // SESIÓN / SALIDA
        // ─────────────────────────────────────────────────────────────────────
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

        // ─────────────────────────────────────────────────────────────────────
        // NAVEGACIÓN PÚBLICA — llamada desde formularios hijos
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Vuelve a la pantalla de inicio donde aparecen las notificaciones.
        /// Lo invoca FrmListadoProductos al hacer clic en lblAlertaStock.
        /// El usuario puede entonces hacer clic en la notificación específica
        /// para iniciar el flujo de restock.
        /// </summary>
        public void IrARestock()
        {
            MostrarInicio();
        }

        // ─────────────────────────────────────────────────────────────────────
        // REFRESH PÚBLICOS
        // ─────────────────────────────────────────────────────────────────────
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
        public void RefrescarListadoEmpleados()
        {
            if (formularioActivo is FrmListadoEmpleados)
                ((FrmListadoEmpleados)formularioActivo).Mostrar();
        }
    }
}