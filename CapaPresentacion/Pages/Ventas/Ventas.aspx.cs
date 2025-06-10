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
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            // Obtener la lista de ventas desde la capa de negocio
            VentaBLL cn_venta = new VentaBLL();
            DataTable ventas = cn_venta.ListarVentas();
            rptVentas.DataSource = ventas;
            rptVentas.DataBind();
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object EliminarVenta(int ventaId)
        {
            try
            {
                VentaBLL oCN_Venta = new VentaBLL();
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

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            //Redirigirme a la página de crear ventas
            Response.Redirect("~/Pages/Ventas/RegistrarVentas.aspx");
        }

        protected void rptVentas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"~/Pages/Ventas/EditarVentas.aspx?VentaId={id}");
                    break;
                case "Ver":
                    Response.Redirect($"~/Pages/Ventas/DetalleVentas.aspx?VentaId={id}");
                    break;
                case "Eliminar":
                    Response.Redirect($"~/Pages/Ventas/EliminarVentas.aspx?VentaId={id}");
                    break;
            }
        }
    }
}