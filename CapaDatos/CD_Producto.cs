using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS - GESTIÓN DE PRODUCTOS
    /// Incluye sistema de precios dinámicos:
    /// - Sube 10% el producto vendido
    /// - Baja 10% los productos no vendidos (mínimo $1)
    /// - Compra múltiple: todos los productos suben 10%
    /// </summary>
    public class CD_Producto
    {
        // =============================================
        // PROPIEDADES DE LA CLASE PRODUCTO
        // =============================================
        public int Idproducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioBase { get; set; }
        public decimal PrecioMinimo { get; set; }
        public int Stock { get; set; }
        public string Estado { get; set; }
        public string Categoria { get; set; }
        public bool EsMedicamento { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int TotalVendido { get; set; }
        public string Buscar { get; set; }

        /// <summary>
        /// MÉTODO PARA LISTAR TODOS LOS PRODUCTOS
        /// Incluye alertas de stock y porcentaje de cambio de precio
        /// </summary>
        public DataTable Listar()
        {
            DataTable resultado = new DataTable("Producto");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_list_producto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex)
            {
                resultado = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA GUARDAR UN NUEVO PRODUCTO
        /// </summary>
        public string Guardar(CD_Producto prod)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_insert_producto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // AGREGAR PARÁMETROS
                cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@precio", prod.Precio);
                cmd.Parameters.AddWithValue("@stock", prod.Stock);
                cmd.Parameters.AddWithValue("@estado", prod.Estado);
                cmd.Parameters.AddWithValue("@categoria", prod.Categoria);
                cmd.Parameters.AddWithValue("@es_medicamento", prod.EsMedicamento);
                cmd.Parameters.AddWithValue("@fecha_vencimiento",
                    prod.FechaVencimiento.HasValue ? (object)prod.FechaVencimiento.Value : DBNull.Value);

                // EJECUTAR Y OBTENER ID DEL PRODUCTO INSERTADO
                object idproducto = cmd.ExecuteScalar();

                resultado = idproducto != null ? "OK" : "No se pudo insertar el producto";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA EDITAR UN PRODUCTO EXISTENTE
        /// </summary>
        public string Editar(CD_Producto prod)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_update_producto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // AGREGAR PARÁMETROS
                cmd.Parameters.AddWithValue("@idproducto", prod.Idproducto);
                cmd.Parameters.AddWithValue("@nombre", prod.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@precio", prod.Precio);
                cmd.Parameters.AddWithValue("@stock", prod.Stock);
                cmd.Parameters.AddWithValue("@estado", prod.Estado);
                cmd.Parameters.AddWithValue("@categoria", prod.Categoria);
                cmd.Parameters.AddWithValue("@es_medicamento", prod.EsMedicamento);
                cmd.Parameters.AddWithValue("@fecha_vencimiento",
                    prod.FechaVencimiento.HasValue ? (object)prod.FechaVencimiento.Value : DBNull.Value);

                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo actualizar el producto";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA ELIMINAR UN PRODUCTO (ELIMINACIÓN LÓGICA)
        /// </summary>
        public string Eliminar(CD_Producto prod)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_delete_producto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idproducto", prod.Idproducto);

                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo eliminar el producto";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR PRODUCTOS POR NOMBRE
        /// </summary>
        public DataTable BuscarNombre(CD_Producto prod)
        {
            DataTable resultado = new DataTable("Producto");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_producto_nombre", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", prod.Buscar);

                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex)
            {
                resultado = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR PRODUCTOS POR CATEGORÍA
        /// </summary>
        public DataTable BuscarCategoria(string categoria)
        {
            DataTable resultado = new DataTable("Producto");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_producto_categoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoria", categoria);

                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex)
            {
                resultado = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA AJUSTAR PRECIOS DESPUÉS DE UNA VENTA
        /// Implementa la lógica: producto vendido sube 10%, los demás bajan 10% (mín $1)
        /// </summary>
        public string AjustarPreciosVenta(int idproductoVendido, int cantidadVendida)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_ajustar_precios_venta", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120; // Timeout extendido por los cursores

                cmd.Parameters.AddWithValue("@idproducto_vendido", idproductoVendido);
                cmd.Parameters.AddWithValue("@cantidad_vendida", cantidadVendida);

                // EJECUTAR Y OBTENER RESULTADO
                object res = cmd.ExecuteScalar();
                resultado = res != null ? res.ToString() : "Error en el ajuste de precios";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA AJUSTAR PRECIOS EN COMPRA MÚLTIPLE
        /// Todos los productos comprados suben 10%, los demás bajan 10%
        /// </summary>
        public string AjustarPreciosCompraMultiple(string idsProductos)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_ajustar_precios_compra_multiple", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120; // Timeout extendido

                cmd.Parameters.AddWithValue("@productos", idsProductos);

                // EJECUTAR Y OBTENER RESULTADO
                object res = cmd.ExecuteScalar();
                resultado = res != null ? res.ToString() : "Error en el ajuste de precios";
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA OBTENER EL HISTORIAL DE PRECIOS DE UN PRODUCTO
        /// </summary>
        public DataTable ObtenerHistorialPrecios(int idproducto)
        {
            DataTable resultado = new DataTable("HistorialPrecios");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_historial_precios_producto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idproducto", idproducto);

                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex)
            {
                resultado = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resultado;
        }
    }
}