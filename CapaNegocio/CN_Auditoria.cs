using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Auditoria
    {
        private static CD_Auditoria objDato = new CD_Auditoria();

        // MÉTODO LISTAR AUDITORÍA
        public static DataTable Listar(string operacion, DateTime fechaInicio, DateTime fechaFin)
        {
            return objDato.Listar(operacion, fechaInicio, fechaFin);
        }
    }
}