using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_MetodoPagos
    {
        private CD_MetodoPagos metodoPagos = new CD_MetodoPagos();
        public List<MetodoPago> ObtenerMetodoPagosParaVenta()
        {
            return metodoPagos.ObtenerMetodoVenta();
        }
    }
}
