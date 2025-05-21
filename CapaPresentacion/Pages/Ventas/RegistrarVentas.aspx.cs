using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class RegistrarVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cancelarVenta_Click(object sender, EventArgs e)
        {
            //Volver a la vista de Ventas.asp
            Response.Redirect("/Pages/Ventas/Ventas.aspx");
        }
    }
}