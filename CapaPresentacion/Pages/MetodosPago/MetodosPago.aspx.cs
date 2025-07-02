using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.MetodosPago
{
    public partial class MetodosPago : System.Web.UI.Page
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
            MetodoPagoBLL metodoPagoBLL = new MetodoPagoBLL();
            rptMetodoPago.DataSource = metodoPagoBLL.ObtenerMetodoPagosParaVenta();
            rptMetodoPago.DataBind();
        }

        protected void btnCrearNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/MetodosPago/RegistrarMetodoPago.aspx");
        }

        protected void rptMetodoPago_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idMetodoPago = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Ver")
            {
                Response.Redirect("~/Pages/MetodosPago/VerMetodoPago.asox");
            }
            if (e.CommandName == "Editar")
            {
                Response.Redirect("~/Pages/MetodosPago/EditarMetodoPago.asox");
            }
            if (e.CommandName == "Eliminar")
            {
                Response.Redirect("~/Pages/MetodosPago/EliminarMetodoPago.asox");
            }
        }
    }
}