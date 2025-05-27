using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class DetalleVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["VentaId"] != null)
                {
                    int ventaId;
                    if (int.TryParse(Request.QueryString["VentaId"], out ventaId))
                    {
                        CargarDetalleVenta(ventaId);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No existe datos para esta venta');", true);
                    }
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Ventas/Ventas.aspx");
        }

        private void CargarDetalleVenta(int ventaId)
        {
            CN_DetalleVentas cnDetalleVentas = new CN_DetalleVentas();
            var detalle = cnDetalleVentas.ObtenerDetalleVentas(ventaId);

            gvDetalle.DataSource = detalle;
            gvDetalle.DataBind();
        }
    }
}