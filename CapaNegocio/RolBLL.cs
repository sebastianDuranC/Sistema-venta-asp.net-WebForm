using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class RolBLL
    {
        RolDAL rol = new RolDAL();

        public List<Rol> ObtenerRoles()
        {
            return rol.obtenerRol();
        }

        public Rol ObtenerRolPorId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID del rol debe ser mayor que cero");
            }
            return rol.ObtenerRolPorId(id);
        }

        public bool RegistrarRolConPermisos(Rol nombreRol, List<int> permisosIds)
        {
            if (string.IsNullOrEmpty(nombreRol.Nombre))
            {
                throw new ArgumentException("El nombre del rol no puede estar vacío");
            }
            if (permisosIds == null || permisosIds.Count == 0)
            {
                throw new ArgumentException("Debe seleccionar al menos un permiso");
            }

            DataTable dtPermisos = new DataTable();
            dtPermisos.Columns.Add("Id", typeof(int));
            foreach (int id in permisosIds)
            {
                dtPermisos.Rows.Add(id);
            }

            try
            {
                int nuevoRolId = rol.RegistrarRolConPermisos(nombreRol, dtPermisos);
                // Si el ID devuelto es mayor que 0, la operación fue exitosa
                return nuevoRolId > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool EditarRol(Rol rolEditado, List<int> permisosSeleccionadosIds)
        {
            if (string.IsNullOrEmpty(rolEditado.Nombre))
            {
                throw new ArgumentException("El nombre del rol no puede estar vacío.");
            }
            if (permisosSeleccionadosIds == null || permisosSeleccionadosIds.Count == 0)
            {
                throw new ArgumentException("Debe seleccionar al menos un permiso para el rol.");
            }

            DataTable dtPermisos = new DataTable();
            dtPermisos.Columns.Add("Id", typeof(int));
            foreach (int id in permisosSeleccionadosIds)
            {
                dtPermisos.Rows.Add(id);
            }

            rol.EditarRolConPermisos(rolEditado.Id, rolEditado.Nombre, dtPermisos);
            return true;
        }

        public bool EliminarRol(int id)
        {
            if (id <= 1) //Rol indspensble
            {
                throw new ArgumentException("El ID del rol debe ser mayor que cero");
            }
            try
            {
                return rol.EliminarRol(id);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // 50000 es el número de error por defecto para RAISERROR.
                {
                    throw new InvalidOperationException(ex.Message);
                }
                else
                {
                    throw; // Relanza cualquier otro error de SQL.
                }
            }
        }
    }
}
