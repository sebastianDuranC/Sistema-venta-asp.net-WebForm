using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;

namespace CapaPresentacion.Acceso
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si es la primera vez que se carga la página
            if (!IsPostBack)
            {
                Session.Clear();
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario
            {
                Nombre = txtUsuario.Text.Trim(),
                Contra = txtPassword.Text.Trim()
            };

            UsuarioBLL CN_negocio = new UsuarioBLL();
            bool resultado = CN_negocio.ValidarCredencialesUsuario(usuario);
            if (resultado)
            {
                FormsAuthentication.SetAuthCookie(usuario.Nombre, false);
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                int rolIdDelUsuario = usuarioBLL.ObtenerRolIdNombre(usuario.Nombre);
                Session["usuario"] = usuario;

                // Obtener y guardar la lista de rutas permitidas
                RolPermisoBLL permisoBLL = new RolPermisoBLL();
                List<string> permisos = permisoBLL.ObtenerRutasPermitidasPorRol(rolIdDelUsuario);
                Session["permisos"] = permisos;

                // Redirigir al Default
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                MostrarMensaje("Usuario o contraseña incorrectos");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }

    }
}