using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public bool Login(string user, string pass)
        {
            using (SqlConnection conexion = new SqlConnection(CD_Conexion.Conn))
            {
                conexion.Open();
                string query = "SELECT Count(*) FROM usuario WHERE usuario = @usuario AND pass = @pass AND estado = 'ACTIVO'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@usuario", user);
                cmd.Parameters.AddWithValue("@pass", pass);

                int result = (int)cmd.ExecuteScalar();
                return result > 0;
            }
        }
    }
}