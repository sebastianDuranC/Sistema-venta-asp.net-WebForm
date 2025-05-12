using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public bool EnLocal { get; set; }
        public int? ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public int MetodoPagoId { get; set; }
        public bool EstadoId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
