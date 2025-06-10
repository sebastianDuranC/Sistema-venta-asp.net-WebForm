using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            // Configurar la respuesta como JSON
            context.Response.ContentType = "application/json";

            try
            {
                // Verificar si el formulario tiene el parámetro Id
                if (context.Request.Form["VentaId"] == null)
                {
                    throw new Exception("No se proporcionó el ID de la venta");
                }

                // Obtener el ID del producto desde la solicitud
                int VentaId;
                if (!int.TryParse(context.Request.Form["VentaId"], out VentaId))
                {
                    throw new Exception("El ID no es válido");
                }

                // Crear instancia de la capa de negocio
                VentaBLL venta = new VentaBLL();

                // Intentar eliminar el producto
                bool resultado = venta.EliminarVenta(VentaId);

                // Crear objeto de respuesta
                var respuesta = new
                {
                    exito = resultado,
                    mensaje = resultado ? "Venta eliminado correctamente" : "Error al eliminar la Venta"
                };

                // Serializar y enviar la respuesta
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                context.Response.Write(serializer.Serialize(respuesta));
            }
            catch (Exception ex)
            {
                // Registrar el error en el log de eventos
                EventLog.WriteEntry("Application",
                    $"Error en EliminarProductoHandler: {ex.Message}\nStackTrace: {ex.StackTrace}",
                    EventLogEntryType.Error);

                // En caso de error, enviar respuesta de error
                var respuestaError = new
                {
                    exito = false,
                    mensaje = "Error: " + ex.Message
                };

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                context.Response.Write(serializer.Serialize(respuestaError));
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