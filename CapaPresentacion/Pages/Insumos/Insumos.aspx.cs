using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Insumos
{
    public partial class Insumos : System.Web.UI.Page
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
            InsumoBLL insumoBLL = new InsumoBLL();
            rptInsumo.DataSource = insumoBLL.ObtenerInsumos();
            rptInsumo.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Insumos/RegistrarInsumo.aspx");
        }

        protected void rptInsumo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idInsumo = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/Insumos/VerInsumo.aspx?Id={idInsumo}");
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/Insumos/EditarInsumo.aspx?Id={idInsumo}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/Insumos/EliminarInsumo.aspx?Id={idInsumo}");
            }
        }
    }
}