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
        private static CD_Cliente objDato = new CD_Cliente();

        // MÉTODO LISTAR CLIENTES
        public static DataTable Listar()
        {
            return objDato.Listar();
        }

        // MÉTODO GUARDAR CLIENTE
        public static string Guardar(string nombre, string telefono, string direccion, string estado)
        {
            CD_Cliente obj = new CD_Cliente
            {
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion,
                Estado = estado
            };

            return objDato.Guardar(obj);
        }

        // MÉTODO EDITAR CLIENTE
        public static string Editar(int idcliente, string nombre, string telefono, string direccion, string estado)
        {
            CD_Cliente obj = new CD_Cliente
            {
                Idcliente = idcliente,
                Nombre = nombre,
                Telefono = telefono,
                Direccion = direccion,
                Estado = estado
            };

            return objDato.Editar(obj);
        }

        // MÉTODO ELIMINAR CLIENTE
        public static string Eliminar(int idcliente)
        {
            CD_Cliente obj = new CD_Cliente
            {
                Idcliente = idcliente
            };

            return objDato.Eliminar(obj);
        }

        // MÉTODO BUSCAR CLIENTE POR NOMBRE
        public static DataTable BuscarNombre(string nombre)
        {
            CD_Cliente obj = new CD_Cliente
            {
                Buscar = nombre
            };

            return objDato.BuscarNombre(obj);
        }

        // MÉTODO BUSCAR CLIENTE POR ID
        public static DataTable BuscarId(string id)
        {
            CD_Cliente obj = new CD_Cliente
            {
                Buscar = id
            };

            return objDato.BuscarId(obj);
        }
    }
}