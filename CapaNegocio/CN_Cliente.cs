using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objetoCD = new CD_Cliente();

        public DataTable MostrarClientes()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }

        public void InsertarCliente(string nombre, string telefono, string direccion, string estado)
        {
            objetoCD.Insertar(nombre, telefono, direccion, estado);
        }
    }
}