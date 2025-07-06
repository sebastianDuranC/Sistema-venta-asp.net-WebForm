using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Pages.ProductoCategoria
{
    /// <summary>
    /// Descripción breve de EliminarProductoCategoriasHandler
    /// </summary>
    public class EliminarProductoCategoriasHandler : IHttpHandler
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
                    throw new Exception("No se proporcionó el ID");
                }

                // Obtener el ID desde la solicitud
                int Id;
                if (!int.TryParse(context.Request.Form["Id"], out Id))
                {
                    throw new Exception("El ID de la categoria de producto no es válido");
                }

                // Crear instancia de la capa de negocio
                ProductoCategoriaBLL obj = new ProductoCategoriaBLL();

                // Intentar eliminar el producto
                bool resultado = obj.EliminarProductoCategoria(Id);

                // Crear objeto de respuesta
                var respuesta = new
                {
                    exito = resultado,
                    mensaje = resultado ? "Registro eliminado correctamente" : "Error al eliminar el registro"
                };

                // Serializar y enviar la respuesta
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                context.Response.Write(serializer.Serialize(respuesta));
            }
            catch (Exception ex)
            {
                // Registrar el error en el log de eventos
                EventLog.WriteEntry("Application",
                    $"Error en EliminarClaseHandler: {ex.Message}\nStackTrace: {ex.StackTrace}",
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