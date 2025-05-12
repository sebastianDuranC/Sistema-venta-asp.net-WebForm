using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contra { get; set; }
        public bool EstadoId { get; set; }
        public int NegocioId { get; set; }
        public int RolId { get; set; }

        public virtual Negocio Negocio { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<MovimientoInventario> MovimientosInventario { get; set; }
    }
}
