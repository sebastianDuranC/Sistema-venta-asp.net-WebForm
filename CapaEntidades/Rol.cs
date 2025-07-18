﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<RolPermisos> RolesPermisos { get; set; }
    }
}
