using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace CapaPresentacion.Pages.Usuarios
{
    /// <summary>
    /// Descripción breve de EliminarUsuarioHandler
    /// </summary>
    public class EliminarUsuarioHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Establecemos que la respuesta será en formato JSON
            context.Response.ContentType = "application/json";

            // Usamos un serializador para convertir nuestro objeto de respuesta a JSON
            var serializer = new JavaScriptSerializer();

            try
            {
                // Verificamos que se haya enviado el ID del usuario desde el cliente
                if (string.IsNullOrEmpty(context.Request.Form["Id"]))
                {
                    throw new ArgumentException("No se proporcionó el ID del usuario.");
                }

                // Intentamos convertir el ID a un número entero
                int usuarioId;
                if (!int.TryParse(context.Request.Form["Id"], out usuarioId))
                {
                    throw new ArgumentException("El ID del usuario proporcionado no es válido.");
                }

                // Creamos una instancia de nuestra capa de negocio
                UsuarioBLL usuarioBLL = new UsuarioBLL();

                // Llamamos al método para eliminar el usuario
                bool resultado = usuarioBLL.EliminarUsuario(usuarioId);

                // Preparamos la respuesta según el resultado
                var respuesta = new
                {
                    exito = resultado,
                    mensaje = resultado ? "Usuario eliminado con éxito." : "No se pudo eliminar el usuario."
                };

                context.Response.Write(serializer.Serialize(respuesta));
            }
            catch (Exception ex)
            {
                // En caso de error, lo registramos para futura revisión
                EventLog.WriteEntry("Application", $"Error en EliminarUsuarioHandler: {ex.Message}\nStackTrace: {ex.StackTrace}", EventLogEntryType.Error);

                // Enviamos una respuesta de error al cliente
                var respuestaError = new
                {
                    exito = false,
                    mensaje = "Ocurrió un error en el servidor: " + ex.Message
                };

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