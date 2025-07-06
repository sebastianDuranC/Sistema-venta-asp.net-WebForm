using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoCategoriaBLL
    {
        ProductoCategoriaDAL cdProductoCategoria = new ProductoCategoriaDAL();
        public List<ProductoCategoria> ObtenerCategoriasProducto()
        {
            return cdProductoCategoria.obtenerCategoriasProducto();
        }

        public bool RegistrarProductoCategoria(ProductoCategoria categoria)
        {
            if (string.IsNullOrEmpty(categoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            }
            return cdProductoCategoria.RegistrarProductoCategoria(categoria);
        }

        public ProductoCategoria ObtenerCategoriaPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID de la categoría debe ser mayor que cero.");
            }
            return cdProductoCategoria.ObtenerProductoCategoriaPorId(id);
        }

        public bool ActualizarProductoCategoria(ProductoCategoria categoria)
        {
            if (categoria.Id <= 0)
            {
                throw new ArgumentException("El ID de la categoría debe ser mayor que cero.");
            }
            if (string.IsNullOrEmpty(categoria.Nombre))
            {
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");
            }
            return cdProductoCategoria.EditarProductoCategoria(categoria);
        }

        public bool EliminarProductoCategoria(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID de la categoría debe ser mayor que cero.");
            }
            return cdProductoCategoria.EliminarProductoCategoria(id);
        }
    }
}
