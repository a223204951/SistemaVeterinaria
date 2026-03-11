using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS - MÓDULO DE VENTAS
    /// Maneja la cabecera, detalle y confirmación de ventas
    /// </summary>
    public class CD_Venta
    {
        // ── Propiedades ───────────────────────────────────────────────────────
        public int Idventa { get; set; }
        public int Idcliente { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public string Buscar { get; set; }

        // ── Crear cabecera de venta ───────────────────────────────────────────
        /// <summary>
        /// Crea la cabecera de la venta y devuelve el idventa generado.
        /// Devuelve -1 si el usuario no fue encontrado.
        /// </summary>
        public int CrearVenta(CD_Venta venta)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_venta_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcliente", venta.Idcliente);
                    cmd.Parameters.AddWithValue("@usuario", venta.Usuario);

                    object res = cmd.ExecuteScalar();
                    return res != null ? Convert.ToInt32(res) : -1;
                }
                catch { return -1; }
            }
        }

        // ── Agregar producto al carrito ────────────────────────────────────────
        /// <summary>
        /// Agrega un producto a la venta (descuenta stock inmediatamente).
        /// Devuelve "OK" o mensaje de error.
        /// </summary>
        public string AgregarDetalle(int idventa, int idproducto, int cantidad)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_detalle_venta_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventa", idventa);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al agregar producto";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Quitar producto del carrito ───────────────────────────────────────
        public string EliminarDetalle(int iddetalle)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_delete_detalle_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@iddetalle", iddetalle);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al eliminar detalle";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Confirmar venta (aplica regla 10%) ───────────────────────────────
        /// <summary>
        /// Confirma la venta: recalcula totales y aplica ajuste de precios 10%.
        /// </summary>
        public string ConfirmarVenta(int idventa)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    // Timeout extendido: el SP actualiza TODOS los productos
                    SqlCommand cmd = new SqlCommand("dbo.sp_confirmar_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;
                    cmd.Parameters.AddWithValue("@idventa", idventa);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al confirmar venta";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Cancelar venta (devuelve stock) ──────────────────────────────────
        public string CancelarVenta(int idventa)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_cancelar_venta_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventa", idventa);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al cancelar venta";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Listar ventas con filtros ─────────────────────────────────────────
        public DataTable Listar(DateTime fechaInicio, DateTime fechaFin, string estado)
        {
            DataTable dt = new DataTable("Ventas");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_list_ventas_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // ── Obtener detalle de una venta ─────────────────────────────────────
        public DataTable ObtenerDetalle(int idventa)
        {
            DataTable dt = new DataTable("DetalleVenta");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_get_detalle_venta_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventa", idventa);
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // ── Obtener clientes activos para ComboBox ────────────────────────────
        public DataTable ObtenerClientes()
        {
            DataTable dt = new DataTable("Clientes");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT idcliente, nombre FROM cliente WHERE estado='ACTIVO' ORDER BY nombre", con);
                    cmd.CommandType = CommandType.Text;
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // ── Obtener productos activos con stock para búsqueda ─────────────────
        public DataTable BuscarProducto(string buscar)
        {
            DataTable dt = new DataTable("Productos");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT p.idproducto, p.nombre, c.nombre AS categoria,
                                 p.precio, p.stock
                          FROM producto p
                          INNER JOIN categoria_producto c ON p.idcategoria = c.idcategoria
                          WHERE p.estado = 'ACTIVO' AND p.stock > 0
                            AND p.nombre LIKE '%' + @buscar + '%'
                          ORDER BY p.nombre", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@buscar", buscar ?? "");
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }
    }
}