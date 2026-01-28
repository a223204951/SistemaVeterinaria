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
        public int Idcliente { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Buscar { get; set; }

        // MÉTODO LISTAR CLIENTES
        public DataTable Listar()
        {
            DataTable resul = new DataTable("Cliente");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand Cmd = new SqlCommand("sp_list_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(Cmd);
                SqlDat.Fill(resul);
            }
            catch (Exception ex)
            {
                resul = null;
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return resul;
        }

        // MÉTODO GUARDAR CLIENTE
        public string Guardar(CD_Cliente Cli)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();
            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand Cmd = new SqlCommand("sp_insert_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@idcliente", SqlDbType.Int).Direction = ParameterDirection.Output;
                Cmd.Parameters.AddWithValue("@nombre", Cli.Nombre);
                Cmd.Parameters.AddWithValue("@telefono", Cli.Telefono);
                Cmd.Parameters.AddWithValue("@direccion", Cli.Direccion);
                Cmd.Parameters.AddWithValue("@estado", Cli.Estado);

                resultado = Cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo insertar el registro";
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
                SqlCommand Cmd = new SqlCommand("sp_update_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@idcliente", cli.Idcliente);
                Cmd.Parameters.AddWithValue("@nombre", cli.Nombre);
                Cmd.Parameters.AddWithValue("@telefono", cli.Telefono);
                Cmd.Parameters.AddWithValue("@direccion", cli.Direccion);
                Cmd.Parameters.AddWithValue("@estado", cli.Estado);
                resultado = Cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
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
                SqlCommand Cmd = new SqlCommand("sp_delete_cliente", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Parameters.AddWithValue("@idcliente", cli.Idcliente);

                resultado = Cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
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
                SqlCommand Cmd = new SqlCommand("sp_buscar_cliente_nombre", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@nombre", cli.Buscar);
                SqlDataAdapter SqlDat = new SqlDataAdapter(Cmd);
                SqlDat.Fill(resultado);
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
                SqlCommand Cmd = new SqlCommand("sp_buscar_cliente_id", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@id", cli.Buscar);
                SqlDataAdapter SqlDat = new SqlDataAdapter(Cmd);
                SqlDat.Fill(resultado);
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
