using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    partial class FrmListadoUsuarios
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle csHeader = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle csCell = new System.Windows.Forms.DataGridViewCellStyle();

            csHeader.BackColor = Color.FromArgb(52, 73, 94);
            csHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            csHeader.ForeColor = Color.White;
            csHeader.SelectionBackColor = Color.FromArgb(52, 73, 94);
            csHeader.Alignment = DataGridViewContentAlignment.MiddleLeft;

            csCell.BackColor = Color.White;
            csCell.Font = new Font("Segoe UI", 9F);
            csCell.ForeColor = Color.FromArgb(52, 73, 94);
            csCell.SelectionBackColor = Color.FromArgb(52, 152, 219);
            csCell.SelectionForeColor = Color.White;
            csCell.WrapMode = DataGridViewTriState.False;

            this.lblTitulo = new System.Windows.Forms.Label();

            // Panel búsqueda
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();

            // Panel filtros
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.lblFiltrosTitulo = new System.Windows.Forms.Label();
            this.chkAdmin = new System.Windows.Forms.CheckBox();
            this.chkVet = new System.Windows.Forms.CheckBox();
            this.chkCajero = new System.Windows.Forms.CheckBox();
            this.chkAsistente = new System.Windows.Forms.CheckBox();
            this.chkInactivo = new System.Windows.Forms.CheckBox();

            // Grid
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();

            // Panel botones
            this.panelBotones = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnResetPass = new System.Windows.Forms.Button();

            this.panelBusqueda.SuspendLayout();
            this.panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // ── lblTitulo ─────────────────────────────────────────────────────
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Text = "👥 Gestión de Usuarios";

            // ── panelBusqueda ─────────────────────────────────────────────────
            this.panelBusqueda.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right);
            this.panelBusqueda.BackColor = Color.White;
            this.panelBusqueda.BorderStyle = BorderStyle.FixedSingle;
            this.panelBusqueda.Location = new System.Drawing.Point(27, 70);
            this.panelBusqueda.Size = new System.Drawing.Size(1150, 90);

            this.lblBuscar.Text = "Buscar usuario o empleado:";
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblBuscar.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblBuscar.Location = new System.Drawing.Point(12, 14);

            this.txtBuscar.Font = new Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(12, 36);
            this.txtBuscar.Size = new System.Drawing.Size(440, 30);
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);

            this.btnLimpiar.Text = "🔄 Limpiar";
            this.btnLimpiar.Location = new System.Drawing.Point(460, 33);
            this.btnLimpiar.Size = new System.Drawing.Size(90, 35);
            this.btnLimpiar.BackColor = Color.FromArgb(149, 165, 166);
            this.btnLimpiar.ForeColor = Color.White;
            this.btnLimpiar.FlatStyle = FlatStyle.Flat;
            this.btnLimpiar.FlatAppearance.BorderSize = 0;
            this.btnLimpiar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnLimpiar.Cursor = Cursors.Hand;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            this.panelBusqueda.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblBuscar, this.txtBuscar, this.btnLimpiar });

            // ── panelFiltros ──────────────────────────────────────────────────
            this.panelFiltros.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right);
            this.panelFiltros.BackColor = Color.White;
            this.panelFiltros.BorderStyle = BorderStyle.FixedSingle;
            this.panelFiltros.Location = new System.Drawing.Point(27, 172);
            this.panelFiltros.Size = new System.Drawing.Size(1150, 52);

            this.lblFiltrosTitulo.Text = "Filtrar por acceso:";
            this.lblFiltrosTitulo.AutoSize = true;
            this.lblFiltrosTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFiltrosTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblFiltrosTitulo.Location = new System.Drawing.Point(15, 16);

            void MakeChk(CheckBox chk, string text, Color clr, int x)
            {
                chk.AutoSize = true;
                chk.Text = text;
                chk.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                chk.ForeColor = clr;
                chk.Location = new System.Drawing.Point(x, 14);
                chk.Cursor = Cursors.Hand;
                chk.CheckedChanged += new System.EventHandler(this.chkFiltro_CheckedChanged);
            }

            MakeChk(this.chkAdmin, "🟣 Administrador", Color.FromArgb(142, 68, 173), 162);
            MakeChk(this.chkVet, "🟢 Veterinario", Color.FromArgb(22, 160, 133), 332);
            MakeChk(this.chkCajero, "🔵 Cajero", Color.FromArgb(41, 128, 185), 482);
            MakeChk(this.chkAsistente, "🟠 Asistente", Color.FromArgb(230, 126, 34), 602);
            MakeChk(this.chkInactivo, "⬜ Inactivo", Color.FromArgb(149, 165, 166), 752);

            this.panelFiltros.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFiltrosTitulo,
                this.chkAdmin, this.chkVet, this.chkCajero,
                this.chkAsistente, this.chkInactivo
            });

            // ── dgvUsuarios ───────────────────────────────────────────────────
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right);
            this.dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = Color.White;
            this.dgvUsuarios.BorderStyle = BorderStyle.None;
            this.dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = csHeader;
            this.dgvUsuarios.ColumnHeadersHeight = 40;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsuarios.DefaultCellStyle = csCell;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.GridColor = Color.FromArgb(231, 231, 231);
            this.dgvUsuarios.Location = new System.Drawing.Point(27, 236);
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowTemplate.Height = 36;
            this.dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(1150, 305);
            this.dgvUsuarios.DoubleClick += new System.EventHandler(this.dgvUsuarios_DoubleClick);

            // ── panelBotones ──────────────────────────────────────────────────
            this.panelBotones.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right);
            this.panelBotones.Location = new System.Drawing.Point(27, 548);
            this.panelBotones.Size = new System.Drawing.Size(1150, 90);

            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.FromArgb(52, 73, 94);
            this.lblTotal.Location = new System.Drawing.Point(5, 10);
            this.lblTotal.Text = "Total: 0 usuarios";

            void MakeBtn(Button btn, string text, Color bg, int x, System.EventHandler click)
            {
                btn.Text = text;
                btn.Location = new System.Drawing.Point(x, 7);
                btn.Size = new System.Drawing.Size(130, 38);
                btn.BackColor = bg;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                btn.UseVisualStyleBackColor = false;
                btn.Click += click;
            }

            MakeBtn(this.btnNuevo, "➕ Nuevo", Color.FromArgb(46, 204, 113), 730, this.btnNuevo_Click);
            MakeBtn(this.btnEditar, "✏️ Editar", Color.FromArgb(241, 196, 15), 866, this.btnEditar_Click);
            MakeBtn(this.btnEliminar, "🗑️ Dar de baja", Color.FromArgb(231, 76, 60), 1002, this.btnEliminar_Click);
            MakeBtn(this.btnResetPass, "🔑 Resetear Pass", Color.FromArgb(142, 68, 173), 400, this.btnResetPass_Click);
            this.btnResetPass.Size = new System.Drawing.Size(160, 38);

            this.panelBotones.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTotal, this.btnResetPass,
                this.btnNuevo, this.btnEditar, this.btnEliminar
            });

            // ── FrmListadoUsuarios ────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitulo, this.panelBusqueda, this.panelFiltros,
                this.dgvUsuarios, this.panelBotones });
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "FrmListadoUsuarios";
            this.Text = "Gestión de Usuarios";
            this.Load += new System.EventHandler(this.FrmListadoUsuarios_Load);

            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label lblFiltrosTitulo;
        private System.Windows.Forms.CheckBox chkAdmin;
        private System.Windows.Forms.CheckBox chkVet;
        private System.Windows.Forms.CheckBox chkCajero;
        private System.Windows.Forms.CheckBox chkAsistente;
        private System.Windows.Forms.CheckBox chkInactivo;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnResetPass;
    }
}