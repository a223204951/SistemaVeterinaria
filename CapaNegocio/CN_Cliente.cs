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
        // MÉTODO LISTAR
        // QUE LLAMA AL MÉTODO LISTAR DE LA CLASE CD_CLIENTE DE LA CAPADATOS
        public static DataTable Listar()
        {
            CD_Cliente Datos = new CD_Cliente();
            return Datos.Listar();
        }

        // MÉTODO GUARDAR
        // QUE LLAMA AL MÉTODO GUARDAR DE LA CLASE CD_CLIENTE DE LA CAPADATOS
        public static string Guardar(string nombre, string telefono, string direccion, string estado)
        {
            CD_Cliente Datos = new CD_Cliente();
            Datos.Nombre = nombre;
            Datos.Telefono = telefono;
            Datos.Direccion = direccion;
            Datos.Estado = estado;
            return Datos.Guardar(Datos);
        }

        // MÉTODO EDITAR
        // QUE LLAMA AL MÉTODO EDITAR DE LA CLASE CD_CLIENTE DE LA CAPADATOS
        public static string Editar(int idcliente, string nombre, string telefono, string direccion, string estado)
        {
            CD_Cliente Datos = new CD_Cliente();
            Datos.Idcliente = idcliente;
            Datos.Nombre = nombre;
            Datos.Telefono = telefono;
            Datos.Direccion = direccion;
            Datos.Estado = estado;
            return Datos.Editar(Datos);
        }

        // MÉTODO ELIMINAR
        // QUE LLAMA AL MÉTODO ELIMINAR DE LA CLASE CD_CLIENTE DE LA CAPADATOS
        public static string Eliminar(int idcliente)
        {
            CD_Cliente Datos = new CD_Cliente();
            Datos.Idcliente = idcliente;
            return Datos.Eliminar(Datos);
        }

        // MÉTODO BUSCARNOMBRE
        // QUE LLAMA AL MÉTODO BUSCARNOMBRE DE LA CLASE CD_CLIENTE DE LA CAPADATOS
        public static DataTable BuscarNombre(string textobuscar)
        {
            CD_Cliente Datos = new CD_Cliente();
            Datos.Buscar = textobuscar;
            return Datos.BuscarNombre(Datos);
        }

        // MÉTODO BUSCARID
        // QUE LLAMA AL MÉTODO BUSCARID DE LA CLASE CD_CLIENTE DE LA CAPADATOS
        public static DataTable BuscarId(string textobuscar)
        {
            CD_Cliente Datos = new CD_Cliente();
            Datos.Buscar = textobuscar;
            return Datos.BuscarId(Datos);
        }
    }
}