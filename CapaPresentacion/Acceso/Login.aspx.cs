using CapaNegocio;
using System;
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
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string password = txtPassword.Text.Trim();

                CN_Login negocio = new CN_Login();
                if (negocio.UsuarioDatos(usuario, password))
                {
                    FormsAuthentication.SetAuthCookie(usuario, false);
                    Session["usuario"] = usuario;
                    Response.Redirect("~/Default.aspx");
                }
                else
                {
                    MostrarMensaje("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al intentar iniciar sesión: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }

    }
}