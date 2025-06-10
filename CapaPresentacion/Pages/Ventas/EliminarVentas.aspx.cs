using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class EliminarVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDato();
        }

        private void cargarDato()
        {
            if (Request.QueryString["VentaId"] != null)
            {
                int ventaId;
                if (int.TryParse(Request.QueryString["VentaId"], out ventaId))
                {
                    VentaBLL venta = new VentaBLL();
                    rptVentas.DataSource = venta.ObtenerVentaPorId(ventaId);
                    rptVentas.DataBind();
                }
                else
                {
                    // Manejar el caso en que el ID no es válido
                    Response.Write("ID de venta no válido.");
                }
            }
            else
            {
                // Manejar el caso en que no se proporciona un ID de venta
                Response.Write("No se proporcionó un ID de venta.");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Ventas/Ventas.aspx");
        }
    }
}