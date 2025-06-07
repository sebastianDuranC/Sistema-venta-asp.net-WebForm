using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Pages.Ventas
{
    /// <summary>
    /// Descripción breve de EliminarVentaHandler
    /// </summary>
    public class EliminarVentaHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var serializer = new JavaScriptSerializer();

            try
            {
                int ventaId = int.Parse(context.Request["ventaId"]);
                VentaBLL oCN_Venta = new VentaBLL();
                bool resultado = oCN_Venta.EliminarVenta(ventaId);

                context.Response.Write(serializer.Serialize(new
                {
                    success = resultado,
                    message = resultado ? "Venta anulada correctamente" : "No se realizaron cambios"
                }));
            }
            catch (ApplicationException ex)
            {
                context.Response.Write(serializer.Serialize(new
                {
                    success = false,
                    message = ex.Message
                }));
            }
            catch (Exception ex)
            {
                context.Response.Write(serializer.Serialize(new
                {
                    success = false,
                    message = "Error crítico: " + ex.Message
                }));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}