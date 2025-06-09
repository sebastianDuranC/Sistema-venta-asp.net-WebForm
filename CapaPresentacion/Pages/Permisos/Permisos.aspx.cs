using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace CapaPresentacion.Pages.Permisos
{
    public partial class Permisos : System.Web.UI.Page
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
            PermisoBLL form = new PermisoBLL();
            rptPermisos.DataSource = form.obtenerForm();
            rptPermisos.DataBind();
        }

        protected void btnRegistrarPermisos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Permisos/RegistrarPermisos.aspx");
        }

        protected void rptPermisos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"~/Pages/Permisos/EditarPermisos.aspx?Id={id}");
                    break;
                case "Eliminar":
                    Response.Redirect($"~/Pages/Permisos/EliminarPermisos.aspx?Id={id}");
                    break;
            }
        }
    }
}