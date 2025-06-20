using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Rol
{
    public partial class Rol : System.Web.UI.Page
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
            RolBLL rolBLL = new RolBLL();
            rptRoles.DataSource = rolBLL.ObtenerRoles();
            rptRoles.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Rol/RegistrarRol.aspx");
        }

        protected void rptRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int rolId = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName)
            {
                case "Ver":
                    Response.Redirect($"~/Pages/Rol/DetalleRol.aspx?Id={rolId}");
                    break;
                case "Editar":
                    Response.Redirect($"~/Pages/Rol/EditarRol.aspx?Id={rolId}");
                    break;
                case "Eliminar":
                    Response.Redirect($"~/Pages/Rol/EliminarRol.aspx?Id={rolId}");
                    break;
            }
        }
    }
}