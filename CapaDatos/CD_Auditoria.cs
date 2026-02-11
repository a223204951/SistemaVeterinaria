using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Auditoria
    {
        // MÉTODO LISTAR AUDITORÍA CON FILTROS
        public DataTable Listar(string operacion, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable resultado = new DataTable("Auditoria");
            SqlConnection conexion = new SqlConnection();

            try
            {
                conexion.ConnectionString = CD_Conexion.Conn;
                SqlCommand cmd = new SqlCommand("dbo.sp_list_auditoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@operacion", operacion);
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