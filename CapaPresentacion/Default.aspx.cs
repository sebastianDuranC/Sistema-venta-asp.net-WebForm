using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DashboardBLL cnDashboard = new DashboardBLL();
                lblVentasMes.Text = string.Format(new CultureInfo("es-BO"), "Bs {0:N2}", cnDashboard.ObtenerVentasTotalesMes());
                lblProductosVendidos.Text = cnDashboard.ObtenerProductosVendidosMes().ToString();
                lblItemsInventario.Text = cnDashboard.ObtenerTotalItemsInventario().ToString();
            }
        }
    }
}