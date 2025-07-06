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

        public MetodoPago ObtenerMetodoPagoPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del método de pago debe ser mayor que cero.");
            }
            return metodoPagos.ObtenerMetodoPagoPorId(id);
        }

        public bool ActualizarMetodoPago(MetodoPago metodoPago)
        {
            if (metodoPago == null)
            {
                throw new ArgumentNullException(nameof(metodoPago), "El método de pago no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(metodoPago.Nombre))
            {
                throw new ArgumentException("El nombre del método de pago no puede estar vacío.");
            }
            return metodoPagos.ActualizarMetodoPago(metodoPago);
        }

        public bool EliminarMetodoPago(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("El método de pago no puede ser 0");
            }
            return metodoPagos.EliminarMetodoPago(id);
        }
    }
}
