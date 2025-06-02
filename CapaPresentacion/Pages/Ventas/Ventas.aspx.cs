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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object EliminarVenta(int ventaId)
        {
            try
            {
                CN_Venta oCN_Venta = new CN_Venta();
                bool resultado = oCN_Venta.EliminarVenta(ventaId);
                System.Diagnostics.Debug.WriteLine($"Intentando eliminar venta ID: {ventaId}");
                return new
                {
                    success = resultado,
                    message = resultado ? "Venta eliminada correctamente." : "No se pudo eliminar la venta."
                };
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
                return new
                {
                    success = false,
                    message = "Error interno: " + ex.Message
                };
            }
        }
    }
}