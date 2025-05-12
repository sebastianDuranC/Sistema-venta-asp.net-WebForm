using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int InsumoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Costo { get; set; }
        public bool EstadoId { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Insumo Insumo { get; set; }
    }
}
