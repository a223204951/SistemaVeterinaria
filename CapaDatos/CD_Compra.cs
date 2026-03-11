using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS - MÓDULO DE COMPRAS
    /// Maneja la cabecera, detalle y confirmación de compras a proveedores
    /// </summary>
    public class CD_Compra
    {
        // ── Propiedades ───────────────────────────────────────────────────────
        public int Idcompra { get; set; }
        public int Idproveedor { get; set; }
        public string Usuario { get; set; }

        // ── Crear cabecera de compra ──────────────────────────────────────────
        public int CrearCompra(CD_Compra compra)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_compra_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproveedor", compra.Idproveedor);
                    cmd.Parameters.AddWithValue("@usuario", compra.Usuario);

                    object res = cmd.ExecuteScalar();
                    return res != null ? Convert.ToInt32(res) : -1;
                }
                catch { return -1; }
            }
        }

        // ── Agregar producto a la compra ──────────────────────────────────────
        public string AgregarDetalle(int idcompra, int idproducto, int cantidad, decimal precioUnit)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_insert_detalle_compra", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcompra", idcompra);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@precio_unit", precioUnit);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al agregar producto";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Confirmar compra ──────────────────────────────────────────────────
        public string ConfirmarCompra(int idcompra)
        {
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("dbo.sp_confirmar_compra", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idcompra", idcompra);

                    object res = cmd.ExecuteScalar();
                    return res != null ? res.ToString() : "Error al confirmar compra";
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        // ── Listar compras con filtro de fechas ───────────────────────────────
        public DataTable Listar(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dt = new DataTable("Compras");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_list_compras_caja", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // ── Obtener proveedores activos para ComboBox ─────────────────────────
        public DataTable ObtenerProveedores()
        {
            DataTable dt = new DataTable("Proveedores");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_list_proveedores_activos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // ── Obtener todos los productos activos para la compra ────────────────
        public DataTable ObtenerProductos(string buscar)
        {
            DataTable dt = new DataTable("Productos");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT p.idproducto, p.nombre, c.nombre AS categoria,
                                 p.precio AS precio_actual, p.stock
                          FROM producto p
                          INNER JOIN categoria_producto c ON p.idcategoria = c.idcategoria
                          WHERE p.estado = 'ACTIVO'
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

        // ── Listar movimientos de stock ───────────────────────────────────────
        public DataTable ListarMovimientos(DateTime fechaInicio, DateTime fechaFin, string tipo)
        {
            DataTable dt = new DataTable("Movimientos");
            using (SqlConnection con = new SqlConnection(CD_Conexion.Conn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_list_movimientos_stock", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    new SqlDataAdapter(cmd).Fill(dt);
                }
                catch { }
            }
            return dt;
        }
    }
}