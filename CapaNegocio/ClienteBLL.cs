using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ClienteBLL
    {
        private ClienteDAL cdCliente = new ClienteDAL();

        public List<Cliente> ObtenerClientes()
        {
            DataTable clientesDataTable = cdCliente.ObtenerClientes();
            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow row in clientesDataTable.Rows)
            {
                Cliente cliente = new Cliente
                {
                    Id = row.Field<int>("Id"),
                    Nombre = row.Field<string>("Nombre"),
                    EsComerciante = row.Field<bool>("EsComerciante"),
                    NumeroLocal = row.Field<string>("NumeroLocal"),
                    Pasillo = row.Field<string>("Pasillo")
                };
                clientes.Add(cliente);
            }

            return clientes;
        }

        public List<Cliente> ListarClientes()
        {
            DataTable clientesDataTable = cdCliente.ListarClientes();
            List<Cliente> clientes = new List<Cliente>();

            foreach (DataRow row in clientesDataTable.Rows)
            {
                Cliente cliente = new Cliente
                {
                    Id = row.Field<int>("Id"),
                    Nombre = row.Field<string>("Nombre"),
                    EsComerciante = row.Field<bool>("EsComerciante"),
                    NumeroLocal = row.Field<string>("NumeroLocal"),
                    Pasillo = row.Field<string>("Pasillo")
                };
                clientes.Add(cliente);
            }

            return clientes;
        }

        public bool RegistrarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre del cliente no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.NumeroLocal))
                throw new ArgumentException("El número de local del cliente no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.Pasillo))
                throw new ArgumentException("El dato del pasillo del cliente no puede estar vacío.");
            return cdCliente.RegistrarCliente(cliente);
        }

        public Cliente ObtenerClientePorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser mayor que cero");
            }
            return cdCliente.ObtenerClientePorId(id);
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre del cliente no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.NumeroLocal))
                throw new ArgumentException("El número de local del cliente no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(cliente.Pasillo))
                throw new ArgumentException("El dato del pasillo del cliente no puede estar vacío.");
            return cdCliente.EditarCliente(cliente);
        }

        public bool EliminarCliente(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser mayor que cero");
            }
            return cdCliente.EliminarCliente(id);
        }
    }
}
