using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class UnidadesMedida
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public bool EstadoId { get; set; }

        public virtual ICollection<Insumo> Insumos { get; set; }
    }
}
