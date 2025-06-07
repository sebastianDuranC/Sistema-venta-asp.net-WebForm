using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class DetalleVentaBLL
    {
         DetalleVentaDAL cd_detalleventas = new DetalleVentaDAL();

        public DataTable ObtenerDetalleVentas(int ventaId)
        {
            return cd_detalleventas.ObtenerDetalleVenta(ventaId);
        }
    }
}
