using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Usuarios
{
    public partial class Usuarios : System.Web.UI.Page
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
            rptUsuarios.DataSource = usuarioBLL.ObtenerUsuarios();
            rptUsuarios.DataBind();
        }

        protected void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/RegistrarUsuarios.aspx");
        }

        protected void rptUsuarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idUsuario = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"~/Pages/Usuarios/EditarUsuario.aspx?Id={idUsuario}");
                    break;
                case "Eliminar":
                    Response.Redirect($"~/Pages/Usuarios/EliminarUsuario.aspx?Id={idUsuario}");
                    break;
            }
        }
    }
}