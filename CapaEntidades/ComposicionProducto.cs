using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class ComposicionProducto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int InsumoId { get; set; }
        public decimal Cantidad { get; set; }
        public bool EstadoId { get; set; }

        public virtual Producto Producto { get; set; }
        public virtual Insumo Insumo { get; set; }
    }
}
