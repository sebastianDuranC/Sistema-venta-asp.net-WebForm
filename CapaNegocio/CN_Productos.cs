using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Productos
    {
        private CD_Productos cdProducto = new CD_Productos();

        public List<Producto> ObtenerProductos()
        {
            return cdProducto.ObtenerProductosVenta();
        }
    }
}
