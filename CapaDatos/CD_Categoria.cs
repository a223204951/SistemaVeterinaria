using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS - GESTIÓN DE CATEGORÍAS DE PRODUCTOS
    /// Maneja las operaciones CRUD para categorías
    /// </summary>
    public class CD_Categoria
    {
        // =============================================
        // PROPIEDADES DE LA CLASE CATEGORÍA
        // =============================================
        public int Idcategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        /// <summary>
        /// MÉTODO PARA LISTAR TODAS LAS CATEGORÍAS
        /// Incluye el total de productos por categoría
        /// </summary>
        public DataTable Listar()
        {
            DataTable resultado = new DataTable("Categoria");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_list_categoria", conexion);
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
        /// MÉTODO PARA LISTAR SOLO CATEGORÍAS ACTIVAS
        /// Útil para ComboBox
        /// </summary>
        public DataTable ListarActivas()
        {
            DataTable resultado = new DataTable("CategoriaActiva");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("SELECT idcategoria, nombre FROM categoria_producto WHERE estado = 'ACTIVO' ORDER BY nombre", conexion);
                cmd.CommandType = CommandType.Text;

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
        /// MÉTODO PARA GUARDAR UNA NUEVA CATEGORÍA
        /// </summary>
        public string Guardar(CD_Categoria cat)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_insert_categoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", cat.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", cat.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@estado", cat.Estado);

                // EJECUTAR Y OBTENER RESULTADO
                object res = cmd.ExecuteScalar();
                resultado = res != null ? res.ToString() : "Error al insertar categoría";
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
        /// MÉTODO PARA EDITAR UNA CATEGORÍA EXISTENTE
        /// </summary>
        public string Editar(CD_Categoria cat)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_update_categoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idcategoria", cat.Idcategoria);
                cmd.Parameters.AddWithValue("@nombre", cat.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", cat.Descripcion ?? "");
                cmd.Parameters.AddWithValue("@estado", cat.Estado);

                object res = cmd.ExecuteScalar();
                resultado = res != null ? res.ToString() : "Error al actualizar categoría";
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
        /// MÉTODO PARA ELIMINAR UNA CATEGORÍA (ELIMINACIÓN LÓGICA)
        /// No permite eliminar si tiene productos asociados
        /// </summary>
        public string Eliminar(CD_Categoria cat)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_delete_categoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idcategoria", cat.Idcategoria);

                object res = cmd.ExecuteScalar();
                resultado = res != null ? res.ToString() : "Error al eliminar categoría";
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
    }
}