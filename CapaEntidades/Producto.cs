using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    [Serializable]
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int? Stock { get; set; }
        public int? StockMinimo { get; set; }
        public int ProductoCategoriaId { get; set; }
        public string FotoUrl { get; set; }
        public bool Estado { get; set; }

        public virtual ProductoCategoria ProductoCategoria { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
        public virtual ICollection<ProductoInsumo> ProductosInsumo { get; set; }
    }
}
