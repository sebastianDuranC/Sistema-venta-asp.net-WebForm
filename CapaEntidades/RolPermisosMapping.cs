using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class RolPermisosMapping
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int FormId { get; set; }
        public bool EstadoId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual Form Form { get; set; }
    }
}
