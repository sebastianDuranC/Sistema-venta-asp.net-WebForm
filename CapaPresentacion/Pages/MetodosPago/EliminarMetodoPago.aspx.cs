using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.MetodosPago
{
    public partial class EliminarMetodoPago : System.Web.UI.Page
    {
        MetodoPagoBLL metodoPago = new MetodoPagoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos(Convert.ToInt32(Request.QueryString["Id"]));
            }
        }

        private void cargarDatos(int id)
        {
            CapaEntidades.MetodoPago METODO = metodoPago.ObtenerMetodoPagoPorId(id);
            rptMetodoPago.DataSource = new List<CapaEntidades.MetodoPago> { METODO };
            rptMetodoPago.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/MetodosPago/MetodosPago.aspx"); 
        }
    }
}