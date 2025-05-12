using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public decimal? Stock { get; set; }
        public int CategoriaId { get; set; }
        public string FotoUrl { get; set; }
        public bool EstadoId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<DetalleVenta> DetallesVenta { get; set; }
        public virtual ICollection<ComposicionProducto> ComposicionesProducto { get; set; }
        public virtual ICollection<ConfiguracionDescartables> ConfiguracionesDescartables { get; set; }
    }
}
