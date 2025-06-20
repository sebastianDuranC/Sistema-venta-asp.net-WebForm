using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Pages.Rol
{
    /// <summary>
    /// Descripción breve de EliminarRolHandler
    /// </summary>
    public class EliminarRolHandler : IHttpHandler
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
                    throw new Exception("El ID del Rol no es válido");
                }

                // Crear instancia de la capa de negocio
                RolBLL obj = new RolBLL();

                // Intentar eliminar el producto
                bool resultado = obj.EliminarRol(Id);

                // Crear objeto de respuesta
                var respuesta = new
                {
                    exito = resultado,
                    mensaje = resultado ? "Rol eliminado correctamente" : "Error al eliminar el Rol"
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