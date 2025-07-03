using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.UnidadesMedida
{
    public partial class UnidadesMedida : System.Web.UI.Page
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
            UnidadMedidaBLL unidadMedidaBLL = new UnidadMedidaBLL();
            rpttbUnidadMedida.DataSource = unidadMedidaBLL.ObtenerUnidadesMedida();
            rpttbUnidadMedida.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/UnidadesMedida/RegistrarUnidadMedida.aspx");
        }

        protected void rpttbUnidadMedida_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int unidadMedidaId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect($"~/Pages/UnidadesMedida/VerUnidadMedida.aspx?Id={unidadMedidaId}");
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect($"~/Pages/UnidadesMedida/EditarUnidadMedida.aspx?Id={unidadMedidaId}");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect($"~/Pages/UnidadesMedida/EliminarUnidadMedida.aspx?Id={unidadMedidaId}");
            }
        }
    }
}