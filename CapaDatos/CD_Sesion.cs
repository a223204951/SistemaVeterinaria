using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Sesion
    {
        // MÉTODO PARA REGISTRAR INICIO DE SESIÓN
        public int IniciarSesion(string usuario)
        {
            int idSesion = 0;
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_insert_sesion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario", usuario);

                // Obtener el ID de la sesión creada
                object resultado = cmd.ExecuteScalar();
                idSesion = resultado != null ? Convert.ToInt32(resultado) : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return idSesion;
        }

        // MÉTODO PARA REGISTRAR CIERRE DE SESIÓN
        public string CerrarSesion(int idSesion)
        {
            string resultado = "";
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_close_sesion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idsesion", idSesion);

                object res = cmd.ExecuteScalar();
                int filasAfectadas = res != null ? Convert.ToInt32(res) : 0;
                resultado = filasAfectadas >= 1 ? "OK" : "No se pudo cerrar la sesión";
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

        // MÉTODO PARA LISTAR SESIONES CON FILTROS
        public DataTable Listar(string usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable resultado = new DataTable("Sesiones");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_list_sesiones", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

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