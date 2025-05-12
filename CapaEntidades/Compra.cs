using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Compra
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int UsuarioId { get; set; }
        public bool EstadoId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DetalleCompra> DetallesCompra { get; set; }
    }
}
