using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RolBLL
    {
        public List<Rol> ObtenerRoles()
        {
            RolDAL rol = new RolDAL();
            return rol.obtenerRol();
        }
    }
}
