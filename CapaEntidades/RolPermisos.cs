using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class RolPermisos
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int FormId { get; set; }
        public bool Estado { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Permisos Permisos { get; set; }
    }
}
