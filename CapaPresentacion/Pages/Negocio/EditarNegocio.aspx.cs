using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Negocio
{
    public partial class EditarNegocio : System.Web.UI.Page
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
            // Validamos que el ID venga en la URL
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Response.Redirect("~/Pages/Negocio/Negocio.aspx");
                return;
            }

            int idNegocio = Convert.ToInt32(Request.QueryString["id"]);
            NegocioBLL negocioBLL = new NegocioBLL();
            var negocio = negocioBLL.ObtenerNegocioPorId(idNegocio);

            if (negocio != null)
            {
                // Llenamos los campos con los datos actuales
                txtNombreNegocio.Text = negocio.Nombre;
                txtDireccionNegocio.Text = negocio.Direccion;

                // Mostramos la imagen actual y guardamos su ruta en el campo oculto
                if (!string.IsNullOrEmpty(negocio.LogoUrl))
                {
                    imgPreview.ImageUrl = negocio.LogoUrl;
                    hfLogoUrlActual.Value = negocio.LogoUrl;
                }
                else
                {
                    // Si no hay logo, mostramos una imagen por defecto
                    imgPreview.ImageUrl = "~/wwwroot/images/placeholder.png";
                    hfLogoUrlActual.Value = string.Empty;
                }
            }
            else
            {
                // Si no se encuentra el negocio, notificamos y podríamos redirigir
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('No se encontró el negocio.'); window.location='Negocio.aspx';", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Negocio/Negocio.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevaUrlLogo = hfLogoUrlActual.Value; // Por defecto, mantenemos la URL actual.

                // Verificamos si el usuario ha seleccionado un archivo nuevo
                if (fileUploadNuevo.HasFile)
                {
                    // 1. Definir la carpeta donde se guardarán los logos
                    string carpetaRelativa = "~/wwwroot/images/logos/";
                    string directorioFisico = Server.MapPath(carpetaRelativa);

                    // 2. Asegurarse de que la carpeta exista
                    if (!Directory.Exists(directorioFisico))
                    {
                        Directory.CreateDirectory(directorioFisico);
                    }

                    // 3. Generar un nombre de archivo único para evitar sobreescribir
                    string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(fileUploadNuevo.FileName);
                    string rutaFisicaCompleta = Path.Combine(directorioFisico, nombreArchivo);

                    // 4. Guardar el nuevo archivo
                    fileUploadNuevo.SaveAs(rutaFisicaCompleta);

                    // 5. Actualizar la URL que se guardará en la base de datos
                    nuevaUrlLogo = carpetaRelativa.Replace("~", "") + nombreArchivo;

                    // Opcional: Eliminar el archivo de logo anterior si existe
                    if (!string.IsNullOrEmpty(hfLogoUrlActual.Value))
                    {
                        string rutaAntigua = Server.MapPath(hfLogoUrlActual.Value);
                        if (File.Exists(rutaAntigua))
                        {
                            File.Delete(rutaAntigua);
                        }
                    }
                }

                // Creamos el objeto negocio con los datos actualizados
                CapaEntidades.Negocio negocio = new CapaEntidades.Negocio
                {
                    Id = Convert.ToInt32(Request.QueryString["id"]),
                    Nombre = txtNombreNegocio.Text.Trim(),
                    Direccion = txtDireccionNegocio.Text.Trim(),
                    LogoUrl = nuevaUrlLogo // Usamos la nueva URL (o la antigua si no se subió archivo)
                };

                // Llamamos a la capa de negocio para realizar la actualización
                NegocioBLL negocioBLL = new NegocioBLL();
                bool exito = negocioBLL.EditarNegocio(negocio);

                if (exito)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('¡Negocio actualizado con éxito!'); window.location='Negocio.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error al actualizar el negocio.');", true);
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores inesperados
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", $"alert('Ocurrió un error: {ex.Message}');", true);
            }
        }
    }
}