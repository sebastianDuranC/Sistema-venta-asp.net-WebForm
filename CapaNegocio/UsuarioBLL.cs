using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using CapaEntidades;
using System.Data;

namespace CapaNegocio
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDal = new UsuarioDAL();

        public DataTable ObtenerUsuarioPorId(int id)
        {
            try
            {
                return usuarioDal.ObtenerUsarioPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar obtener el usuario por ID: " + ex);
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                return usuarioDal.ObtenerUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar obtener los usuarios: " + ex);
            }
        }

        public bool ValidarCredencialesUsuario(Usuario usuario)
        {
            //VALIDACIONES
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Contra))
            {
                return false;
            }
            var datos = usuarioDal.ValidarCredencialesUsuario(usuario.Nombre); //datos completo del usuario por su nombre
            if (datos == null) //si no hay datos del usuario
            {
                return false; //usuario no existe
            }

            //VALIDACION DE CONTRASEÑA
            string passwordHash = datos.FirstOrDefault()?.Contra; //contraseña hasheada del usuario
            bool credencialesValidas = false;
            try
            {
                credencialesValidas = BCrypt.Net.BCrypt.Verify(usuario.Contra, passwordHash); //verifica si la contraseña ingresada coincide con la hasheada
            }
            catch (Exception)
            {
                return false;
            }
            return credencialesValidas; //devuelve true si las credenciales son válidas, false en caso contrario
        }

        public bool UsuarioTienePermisoForm(int currentUsuario, int currentNombreForm)
        {
            RolPermisoDAL cD_RolPermisos = new RolPermisoDAL();
            try
            {
                return cD_RolPermisos.UsuarioTienePermisoForm(currentUsuario, currentNombreForm);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar saber el permiso al form: " + ex);
            }
        }

        public int ObtenerRolIdNombre(string currentUsuario)
        {
            try
            {
                return usuarioDal.ObtenerRolIdNombre(currentUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar obtener el rol del usuario: " + ex);
            }
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Contra) || usuario.RolId == 0 || usuario.NegocioId == 0)
            {
                throw new ArgumentException("Todos los campos deben ser completados");
            }

            // Hashear la contraseña antes de guardarla  
            usuario.Contra = BCrypt.Net.BCrypt.HashPassword(usuario.Contra);

            return usuarioDal.RegistrarUsuario(usuario);
        }

        public bool EditarUsuario(Usuario usuario, string contraNueva)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Contra) || usuario.RolId == 0 || usuario.NegocioId == 0)
            {
                throw new ArgumentException("Todos los campos deben ser completados");
            }

            // Hashear la contraseña antes de guardarla
            var datos = usuarioDal.ValidarCredencialesUsuario(usuario.Nombre); //datos completo del usuario por su nombre
            string passwordHash = datos.FirstOrDefault()?.Contra; //contraseña hasheada del usuario
            bool resultado = BCrypt.Net.BCrypt.Verify(usuario.Contra, passwordHash);
            if (resultado) // si la contraseña ingresada es igual a la hasheada
            {
                usuario.Contra = contraNueva; // se asigna la nueva contraseña
            }else // si la contraseña ingresada es diferente a la hasheada
            {
                throw new ArgumentException("La contraseña no es correcta");
            }

            usuario.Contra = BCrypt.Net.BCrypt.HashPassword(contraNueva);
            try
            {
                return usuarioDal.EditarUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar editar el usuario: " + ex);
            }
        }

        public bool EliminarUsuario(int id)
        {
            try
            {
                return usuarioDal.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el usuario: " + ex);
            }
        }
    }
}
