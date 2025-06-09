using CapaEntidades;
using CapaNegocio;
using System;
using System.Web.UI;
using System.Collections.Generic;

namespace CapaPresentacion.Pages.Permisos
{
    public partial class EditarPermisos1 : System.Web.UI.Page
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
            PermisoBLL permisoBLL = new PermisoBLL();
            int idPermiso = 0;

            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"].ToString(), out idPermiso))
            {
                CapaEntidades.Permisos form = permisoBLL.ObtenerFormPorId(idPermiso);

                if (form != null)
                {
                    txtFormNombre.Text = form.FormNombre;
                    txtFormRuta.Text = form.FormRuta;
                }
                else
                {
                    Response.Redirect("~/Pages/Permisos/Permisos.aspx");
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Permisos/Permisos.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            PermisoBLL permisoBLL = new PermisoBLL();
            int idPermiso = 0;

            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"].ToString(), out idPermiso))
            {
                // Guardar los cambios ------------------ CORRECCIÓN DE CÓDIGO ------------------
                
                bool resultado = permisoBLL.EditarForm(new CapaEntidades.Permisos
                { 
                    Id = idPermiso,
                    FormNombre = txtFormNombre.Text.Trim(),
                    FormRuta = txtFormRuta.Text.Trim()
                });

                if (resultado)
                {
                    //Mostrar mensaje tipo messagebox de que se edito correctame y redirigir
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Permiso editado correctamente.');", true);
                }
                else
                {
                    // Mostrar mensaje de error si la edición falla
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al editar el permiso.');", true);
                }
                // Redirigir después de guardar
                Response.Redirect("~/Pages/Permisos/Permisos.aspx");
            }
        }
    }
}