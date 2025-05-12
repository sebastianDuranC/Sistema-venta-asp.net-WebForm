using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class MovimientoInventario
    {
        public int Id { get; set; }
        public int InsumoId { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Cantidad { get; set; }
        public string Observacion { get; set; }
        public int UsuarioId { get; set; }
        public bool EstadoId { get; set; }

        public virtual Insumo Insumo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
