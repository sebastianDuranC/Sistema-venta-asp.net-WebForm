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
    }
}
