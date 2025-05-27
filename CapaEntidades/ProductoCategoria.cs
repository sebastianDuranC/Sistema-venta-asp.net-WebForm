using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    [Serializable]
    public class ProductoCategoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
