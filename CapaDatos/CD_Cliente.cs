using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Cliente
    {
        // PROPIEDADES
        public int Idcliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Buscar { get; set; }
        public string Usuario { get; set; } // PARA AUDITORÍA

        // MÉTODO LISTAR CLIENTES
        public DataTable Listar()
        {
            DataTable resultado = new DataTable("Cliente");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_list_cliente", conexion);
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

        // MÉTODO GUARDAR CLIENTE
        public string Guardar(CD_Cliente cli)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_insert_cliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", cli.Nombre);
                cmd.Parameters.AddWithValue("@telefono", cli.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cli.Direccion);
                cmd.Parameters.AddWithValue("@estado", cli.Estado);
                cmd.Parameters.AddWithValue("@usuario", string.IsNullOrEmpty(cli.Usuario) ? "SISTEMA" : cli.Usuario);

                // Leer el resultado del SELECT que retorna el procedimiento
                object resultObj = cmd.ExecuteScalar();
                int filasAfectadas = resultObj != null ? Convert.ToInt32(resultObj) : 0;

                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo insertar el registro";
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

        // MÉTODO EDITAR CLIENTE
        public string Editar(CD_Cliente cli)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_update_cliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idcliente", cli.Idcliente);
                cmd.Parameters.AddWithValue("@nombre", cli.Nombre);
                cmd.Parameters.AddWithValue("@telefono", cli.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cli.Direccion);
                cmd.Parameters.AddWithValue("@estado", cli.Estado);
                cmd.Parameters.AddWithValue("@usuario", string.IsNullOrEmpty(cli.Usuario) ? "SISTEMA" : cli.Usuario);

                // Leer el resultado del SELECT que retorna el procedimiento
                object resultObj = cmd.ExecuteScalar();
                int filasAfectadas = resultObj != null ? Convert.ToInt32(resultObj) : 0;

                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo actualizar el registro";
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

        // MÉTODO ELIMINAR CLIENTE
        public string Eliminar(CD_Cliente cli)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_delete_cliente", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idcliente", cli.Idcliente);
                cmd.Parameters.AddWithValue("@usuario", string.IsNullOrEmpty(cli.Usuario) ? "SISTEMA" : cli.Usuario);

                // Leer el resultado del SELECT que retorna el procedimiento
                object resultObj = cmd.ExecuteScalar();
                int filasAfectadas = resultObj != null ? Convert.ToInt32(resultObj) : 0;

                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo eliminar el registro";
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

        // MÉTODO BUSCAR CLIENTE POR NOMBRE
        public DataTable BuscarNombre(CD_Cliente cli)
        {
            DataTable resultado = new DataTable("Cliente");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_cliente_nombre", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", cli.Buscar);

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

        // MÉTODO BUSCAR CLIENTE POR ID
        public DataTable BuscarId(CD_Cliente cli)
        {
            DataTable resultado = new DataTable("Cliente");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_buscar_cliente_id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", cli.Buscar);

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