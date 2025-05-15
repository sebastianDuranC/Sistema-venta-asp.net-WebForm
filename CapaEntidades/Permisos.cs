using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Permisos
    {
        public int Id { get; set; }
        public string FormNombre { get; set; }
        public string FormRuta { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<RolPermisos> RolPermisos { get; set; }
    }
}
