using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    /// <summary>
    /// CAPA DE DATOS - GESTIÓN DE MASCOTAS
    /// </summary>
    public class CD_Mascota
    {
        public int Idmascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; }
        public int Idcliente { get; set; }
        public string Buscar { get; set; }

        public DataTable Listar()
        {
            DataTable resultado = new DataTable("Mascota");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_list_mascota", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex) { resultado = null; throw ex; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        public string Guardar(CD_Mascota mas)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_insert_mascota", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", mas.Nombre);
                cmd.Parameters.AddWithValue("@especie", mas.Especie);
                cmd.Parameters.AddWithValue("@raza", mas.Raza);
                cmd.Parameters.AddWithValue("@sexo", mas.Sexo);
                cmd.Parameters.AddWithValue("@edad", mas.Edad);
                cmd.Parameters.AddWithValue("@peso", mas.Peso);
                cmd.Parameters.AddWithValue("@color", mas.Color);
                cmd.Parameters.AddWithValue("@estado", mas.Estado);
                cmd.Parameters.AddWithValue("@idcliente", mas.Idcliente);
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo insertar la mascota";
            }
            catch (Exception ex) { resultado = ex.Message; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        public string Editar(CD_Mascota mas)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_update_mascota", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idmascota", mas.Idmascota);
                cmd.Parameters.AddWithValue("@nombre", mas.Nombre);
                cmd.Parameters.AddWithValue("@especie", mas.Especie);
                cmd.Parameters.AddWithValue("@raza", mas.Raza);
                cmd.Parameters.AddWithValue("@sexo", mas.Sexo);
                cmd.Parameters.AddWithValue("@edad", mas.Edad);
                cmd.Parameters.AddWithValue("@peso", mas.Peso);
                cmd.Parameters.AddWithValue("@color", mas.Color);
                cmd.Parameters.AddWithValue("@estado", mas.Estado);
                cmd.Parameters.AddWithValue("@idcliente", mas.Idcliente);
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo actualizar la mascota";
            }
            catch (Exception ex) { resultado = ex.Message; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        public string Eliminar(CD_Mascota mas)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_delete_mascota", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idmascota", mas.Idmascota);
                int filasAfectadas = cmd.ExecuteNonQuery();
                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo eliminar la mascota";
            }
            catch (Exception ex) { resultado = ex.Message; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        public DataTable BuscarNombre(CD_Mascota mas)
        {
            DataTable resultado = new DataTable("Mascota");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_mascota_nombre", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", mas.Buscar);
                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex) { resultado = null; throw ex; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        public DataTable BuscarPorCliente(int idcliente)
        {
            DataTable resultado = new DataTable("Mascota");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_mascota_cliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcliente", idcliente);
                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex) { resultado = null; throw ex; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        /// <summary>
        /// MÉTODO PARA BUSCAR MASCOTAS POR NOMBRE DEL DUEÑO (búsqueda parcial)
        /// Usa el stored procedure sp_buscar_mascota_nombre_cliente
        /// </summary>
        public DataTable BuscarPorNombreCliente(CD_Mascota mas)
        {
            DataTable resultado = new DataTable("Mascota");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_mascota_nombre_cliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", mas.Buscar);
                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex) { resultado = null; throw ex; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }

        public DataTable ObtenerClientes()
        {
            DataTable resultado = new DataTable("Clientes");
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand(
                    "SELECT idcliente, nombre FROM cliente WHERE estado = 'ACTIVO' ORDER BY nombre",
                    conexion);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter sqlDat = new SqlDataAdapter(cmd);
                sqlDat.Fill(resultado);
            }
            catch (Exception ex) { resultado = null; throw ex; }
            finally { if (conexion.State == ConnectionState.Open) conexion.Close(); }
            return resultado;
        }
    }
}