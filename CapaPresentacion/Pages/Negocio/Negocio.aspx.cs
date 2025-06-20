using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Negocio
{
    public partial class Negocio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDato();
            }
        }

        private void cargarDato()
        {
            NegocioBLL negocioBLL = new NegocioBLL();
            rptNegocio.DataSource = negocioBLL.ObtenerNegocio();
            rptNegocio.DataBind();
        }

        protected void rptNegocio_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idNegocio = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"~/Pages/Negocio/EditarNegocio.aspx?Id={idNegocio}");
                    break;
            }
        }
    }
}