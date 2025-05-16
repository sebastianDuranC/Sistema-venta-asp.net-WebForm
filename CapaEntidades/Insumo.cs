using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Insumo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal? Costo { get; set; }
        public decimal? Stock { get; set; }
        public decimal? StockMinimo { get; set; }
        public int InsumoCategoriaId { get; set; }
        public int ProveedorId { get; set; }
        public string FotoUrl { get; set; }
        public bool Estado { get; set; }
        public int UnidadesMedidaId { get; set; }

        public virtual InsumoCategoria InsumoCategoria { get; set; }
        public virtual Proveedor Proveedor { get; set; }
        public virtual UnidadesMedida UnidadesMedida { get; set; }
        public virtual ICollection<DetalleCompra> DetallesCompra { get; set; }
        public virtual ICollection<MovimientoInventario> MovimientosInventario { get; set; }
        public virtual ICollection<ComposicionProducto> ComposicionesProducto { get; set; }
        public virtual ICollection<ConfiguracionDescartables> ConfiguracionesDescartables { get; set; }
    }
}
