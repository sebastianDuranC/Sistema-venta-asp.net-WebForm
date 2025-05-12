using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Rol
    {
        public List<Rol> ObtenerRoles()
        {
            CD_Rol rol = new CD_Rol();
            return rol.obtenerRol();
        }
    }
}
