using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Ventas
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void crearVentas_Click(object sender, EventArgs e)
        {
            //Redirigirme a la página de crear ventas
            Response.Redirect("~/Pages/Ventas/RegistrarVentas.aspx");
        }
    }
}