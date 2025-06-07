using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Permitir acceso libre solo a Login y AccesoDenegado
            string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            if (currentPage == "~/Acceso/Login.aspx" || currentPage == "~/Acceso/AccesoDenegado.aspx")
                return;

            if (Session["usuario"] == null)
            {
                Response.Redirect("~/Acceso/Login.aspx");
                return;
            }

            UsuarioBLL usuarioBLL = new UsuarioBLL();
            string currentUsuario = Session["usuario"].ToString();

            PermisoBLL permisoBLL = new PermisoBLL();
            string currentNombreForm = "~" + Request.Url.AbsolutePath + ".aspx";

            UsuarioBLL CN_Login = new UsuarioBLL();
            if (!CN_Login.UsuarioTienePermisoForm(usuarioBLL.ObtenerRolIdNombre(currentUsuario), permisoBLL.ObtenerFormularioIdNombre(currentNombreForm)))
            {
                Response.Redirect("~/Acceso/AccesoDenegado.aspx");
                return;
            }
            //Si tiene permiso, deja que la página siga cargando
        }

        protected void cerrarSesion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Acceso/Login.aspx");
            }
        }
    }
}