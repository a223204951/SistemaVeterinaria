using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Producto
    {
        public int Idproducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; }
        public int Idcategoria { get; set; }
        public bool EsMedicamento { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Buscar { get; set; }
        public string CodigoBarras { get; set; }  // ← NUEVO
        public int? Idproveedor { get; set; }  // ← proveedor principal

        // ── Listar ───────────────────────────────────────────────────────────
        public DataTable Listar()
        {
            DataTable dt = new DataTable("Producto");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_list_producto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        // ── Guardar (con codigo_barras) ───────────────────────────────────────
        public string Guardar(CD_Producto prod)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    // Usar el nuevo SP v2 que admite codigo_barras
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_producto_v2", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", prod.Precio);
                    cmd.Parameters.AddWithValue("@stock", prod.Stock);
                    cmd.Parameters.AddWithValue("@estado", prod.Estado);
                    cmd.Parameters.AddWithValue("@idcategoria", prod.Idcategoria);
                    cmd.Parameters.AddWithValue("@es_medicamento", prod.EsMedicamento);
                    cmd.Parameters.AddWithValue("@fecha_vencimiento",
                        prod.FechaVencimiento.HasValue
                            ? (object)prod.FechaVencimiento.Value
                            : DBNull.Value);
                    cmd.Parameters.AddWithValue("@codigo_barras",
                        !string.IsNullOrEmpty(prod.CodigoBarras)
                            ? (object)prod.CodigoBarras
                            : DBNull.Value);
                    cmd.Parameters.AddWithValue("@idproveedor",
                        prod.Idproveedor.HasValue
                            ? (object)prod.Idproveedor.Value
                            : DBNull.Value);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al guardar producto";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Editar ───────────────────────────────────────────────────────────
        public string Editar(CD_Producto prod)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_update_producto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto", prod.Idproducto);
                    cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", prod.Precio);
                    cmd.Parameters.AddWithValue("@stock", prod.Stock);
                    cmd.Parameters.AddWithValue("@estado", prod.Estado);
                    cmd.Parameters.AddWithValue("@idcategoria", prod.Idcategoria);
                    cmd.Parameters.AddWithValue("@es_medicamento", prod.EsMedicamento);
                    cmd.Parameters.AddWithValue("@fecha_vencimiento",
                        prod.FechaVencimiento.HasValue
                            ? (object)prod.FechaVencimiento.Value
                            : DBNull.Value);
                    cmd.Parameters.AddWithValue("@idproveedor",
                        prod.Idproveedor.HasValue
                            ? (object)prod.Idproveedor.Value
                            : DBNull.Value);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al actualizar producto";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Eliminar (desactivación lógica) ──────────────────────────────────
        // No se elimina físicamente: el producto puede tener ventas en detalle_venta.
        // Se marca como INACTIVO para conservar la integridad referencial.
        public string Eliminar(CD_Producto prod)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE producto SET estado = 'INACTIVO' WHERE idproducto = @id", con);
                    cmd.Parameters.AddWithValue("@id", prod.Idproducto);
                    cmd.ExecuteNonQuery();
                    return "OK";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Guardar código de barras a un producto existente ──────────────────
        public string GuardarCodigoBarras(int idproducto, string codigoBarras)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_set_codigo_barras", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.AddWithValue("@codigo_barras", codigoBarras);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Buscar por código de barras (escáner) ─────────────────────────────
        public DataTable BuscarPorCodigoBarras(string codigoBarras)
        {
            DataTable dt = new DataTable("Producto");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_producto_barcode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_barras", codigoBarras);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        // ── Búsquedas existentes ─────────────────────────────────────────────
        public DataTable BuscarNombre(CD_Producto prod)
        {
            DataTable dt = new DataTable("Producto");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_producto_nombre", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", prod.Buscar);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable BuscarCategoria(int idcategoria)
        {
            DataTable dt = new DataTable("Producto");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_producto_categoria", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        // ── Precios dinámicos ────────────────────────────────────────────────
        public string AjustarPreciosVenta(int idproductoVendido, int cantidadVendida)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_ajustar_precios_venta", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto_vendido", idproductoVendido);
                    cmd.Parameters.AddWithValue("@cantidad_vendida", cantidadVendida);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al ajustar precios";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        public string AjustarPreciosCompraMultiple(string idsProductos)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_ajustar_precios_compra_multiple", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productos", idsProductos);
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al ajustar precios";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        public DataTable ObtenerHistorialPrecios(int idproducto)
        {
            DataTable dt = new DataTable("HistorialPrecios");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_historial_precios_producto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", idproducto);
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable ObtenerProductosStockBajo()
        {
            DataTable dt = new DataTable("StockBajo");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idproducto, nombre, stock FROM producto WHERE stock <= 10 AND estado='ACTIVO'", con);
                cmd.CommandType = CommandType.Text;
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }

        public DataTable ObtenerProductosProximosVencer()
        {
            DataTable dt = new DataTable("ProximosVencer");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT idproducto, nombre, fecha_vencimiento
                      FROM producto
                      WHERE es_medicamento = 1
                        AND estado = 'ACTIVO'
                        AND fecha_vencimiento IS NOT NULL
                        AND fecha_vencimiento <= DATEADD(DAY, 30, GETDATE())", con);
                cmd.CommandType = CommandType.Text;
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }
    }
}