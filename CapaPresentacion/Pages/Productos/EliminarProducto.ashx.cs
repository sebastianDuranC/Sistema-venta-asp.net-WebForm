using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Pages.Productos
{
    /// <summary>
    /// Descripción breve de EliminarProducto
    /// </summary>
    public class EliminarProducto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Configurar la respuesta como JSON
            context.Response.ContentType = "application/json";

            try
            {
                // Verificar si el formulario tiene el parámetro Id
                if (context.Request.Form["Id"] == null)
                {
                    throw new Exception("No se proporcionó el ID del producto");
                }

                // Obtener el ID del producto desde la solicitud
                int productoId;
                if (!int.TryParse(context.Request.Form["Id"], out productoId))
                {
                    throw new Exception("El ID del producto no es válido");
                }

                // Crear instancia de la capa de negocio
                ProductoBLL negocioProducto = new ProductoBLL();

                // Intentar eliminar el producto
                bool resultado = negocioProducto.EliminarProducto(productoId);

                // Crear objeto de respuesta
                var respuesta = new
                {
                    exito = resultado,
                    mensaje = resultado ? "Producto eliminado correctamente" : "Error al eliminar el producto"
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