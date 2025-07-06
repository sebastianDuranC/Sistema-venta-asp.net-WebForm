using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProveedorBLL
    {
        ProveedorDAL proveedorDAL = new ProveedorDAL();
        public List<Proveedor> ObtenerProveedores()
        {
            return proveedorDAL.ObtenerProveedores();
        }

        public Proveedor ObtenerProveedorPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del proveedor debe ser mayor que cero.");
            }
            return proveedorDAL.ObtenerProveedorPorId(id);
        }

        public bool RegistrarProveedor(Proveedor proveedor)
        {
            if (string.IsNullOrWhiteSpace(proveedor.Nombre))
            {
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            }
            return proveedorDAL.RegistrarProveedor(proveedor);
        }

        public bool EditarProveedor(Proveedor proveedor)
        {
            if (proveedor.Id <= 0)
            {
                throw new ArgumentException("El ID del proveedor debe ser mayor que cero.");
            }
            if (string.IsNullOrWhiteSpace(proveedor.Nombre))
            {
                throw new ArgumentException("El nombre del proveedor es obligatorio.");
            }
            return proveedorDAL.EditarProveedor(proveedor);
        }

        public bool EliminarProveedor(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del proveedor debe ser mayor que cero.");
            }
            return proveedorDAL.EliminarProveedor(id);
        }
    }
}
