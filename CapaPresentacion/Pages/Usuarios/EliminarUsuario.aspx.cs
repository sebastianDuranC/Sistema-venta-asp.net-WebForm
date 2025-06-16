using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Usuarios
{
    public partial class EliminarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            int idUsuario = Convert.ToInt32(Request.QueryString["Id"]);
            rptUsuario.DataSource = usuarioBLL.ObtenerUsuarioPorId(idUsuario);
            rptUsuario.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/Usuarios.aspx");
        }
    }
}