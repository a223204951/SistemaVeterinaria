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

        // ── Guardar ──────────────────────────────────────────────────────────

        public string Guardar(CD_Producto prod)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_producto", con);
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
                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al actualizar producto";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Eliminar REAL ─────────────────────────────────────────────────────
        // Elimina el historial de precios (que SÍ existe) y luego el producto.
        // NO intenta borrar detalle_venta porque esa tabla no existe en esta BD.

        public string Eliminar(CD_Producto prod)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlTransaction tx = con.BeginTransaction();
                    try
                    {
                        // 1. Eliminar historial de precios (tabla confirmada en el script SQL)
                        SqlCommand c1 = new SqlCommand(
                            "DELETE FROM historial_precios WHERE idproducto = @id", con, tx);
                        c1.Parameters.AddWithValue("@id", prod.Idproducto);
                        c1.ExecuteNonQuery();

                        // 2. Eliminar el producto
                        SqlCommand c2 = new SqlCommand(
                            "DELETE FROM producto WHERE idproducto = @id", con, tx);
                        c2.Parameters.AddWithValue("@id", prod.Idproducto);
                        c2.ExecuteNonQuery();

                        tx.Commit();
                        return "OK";
                    }
                    catch { tx.Rollback(); throw; }
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Búsquedas ────────────────────────────────────────────────────────

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
    }
}