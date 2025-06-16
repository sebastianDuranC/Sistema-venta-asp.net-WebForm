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
    public class ProductoBLL
    {
        private ProductoDAL cdProducto = new ProductoDAL();

        public DataTable ObtenerProductoPorId(int productoId)
        {
            try
            {
                if (productoId == 0)
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cdProducto.ObtenerProductoPorId(productoId);
        }

        public List<Producto> ObtenerProductos()
        {
            return cdProducto.ObtenerProductos();
        }

        public bool RegistrarProducto(Producto producto, DataTable productoInsumo)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0 || producto.ProductoCategoriaId <= 0)
                {
                    return false;
                }
                else
                {
                    cdProducto.RegistrarProducto(producto, productoInsumo);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditarProducto(Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0 || producto.ProductoCategoriaId <= 0)
                {
                    return false;
                }
                else
                {
                    cdProducto.EditarProducto(producto);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarProducto(int productoId)
        {
            try
            {
                return cdProducto.EliminarProducto(productoId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
