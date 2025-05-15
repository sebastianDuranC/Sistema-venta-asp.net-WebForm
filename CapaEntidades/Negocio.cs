using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Negocio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string LogoUrl { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
