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
    }
}
