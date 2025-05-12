using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Form
    {
        public int Id { get; set; }
        public string FormNombre { get; set; }
        public string FormRuta { get; set; }
        public bool EstadoId { get; set; }

        public virtual ICollection<RolPermisosMapping> RolPermisosMappings { get; set; }
    }
}
