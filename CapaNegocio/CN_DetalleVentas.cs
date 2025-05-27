using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_DetalleVentas
    {
         CD_DetalleVentas cd_detalleventas = new CD_DetalleVentas();

        public DataTable ObtenerDetalleVentas(int ventaId)
        {
            return cd_detalleventas.ObtenerDetalleVenta(ventaId);
        }
    }
}
