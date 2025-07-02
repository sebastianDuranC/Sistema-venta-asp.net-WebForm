using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MetodoPagoBLL
    {
        private MetodoPagoDAL metodoPagos = new MetodoPagoDAL();
        public List<MetodoPago> ObtenerMetodoPagosParaVenta()
        {
            return metodoPagos.ObtenerMetodoVenta();
        }

        public bool RegistrarMetodoPago(MetodoPago metodoPago)
        {
            if (string.IsNullOrWhiteSpace(metodoPago.Nombre))
            {
                throw new ArgumentException("El nombre del método de pago no puede estar vacío.");
            }
            return metodoPagos.RegistrarMetodoPago(metodoPago);
        }
    }
}
