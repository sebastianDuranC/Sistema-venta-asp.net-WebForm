using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Proveedores
{
    public partial class EditarProveedor : System.Web.UI.Page
    {
        ProveedorBLL ProveedorBLL = new ProveedorBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                cargarDatos(id);
            }
        }

        private void cargarDatos(int id)
        {
            Proveedor proveedor = ProveedorBLL.ObtenerProveedorPorId(id);
            txtNombre.Text = proveedor.Nombre;
            txtContacto.Text = proveedor.Contacto;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Proveedores/Proveedores.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Proveedor proveedor = new Proveedor
            {
                Id = Convert.ToInt32(Request.QueryString["Id"]),
                Nombre = txtNombre.Text.Trim(),
                Contacto = txtContacto.Text.Trim()
            };

            try
            {
                bool resultado = ProveedorBLL.EditarProveedor(proveedor);
                if (resultado)
                {
                    ShowToast("Editado correctamente", "success", "Proveedores.aspx");
                }
                else 
                {
                    ShowToast("Error al editar el proveedor", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "warning");
            }
        }

        private void ShowToast(string titulo, string icono)
        {
            // Escapamos las comillas simples para evitar errores de JavaScript
            string safeTitle = titulo.Replace("'", "\\'");
            string script = $"Swal.fire({{ " +
                $"position: 'top-end'," +
                $" icon: '{icono}'," +
                $" title: '{safeTitle}'," +
                $" showConfirmButton: false," +
                $" timer: 2500," +
                $" toast: true}});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowToastScript", script, true);
        }

        private void ShowToast(string titulo, string icono, string redirectUrl)
        {
            // 1. Prepara el objeto de configuración para SweetAlert
            string swalConfig = $@"{{
                position: 'top-end',
                icon: '{icono}',
                title: '{titulo.Replace("'", "\\'")}',
                showConfirmButton: false,
                timer: 2000,
                toast: true
            }}";

            // 2. Llama a Swal.fire() y LUEGO, usando .then(), ejecuta la redirección.
            string script = $"Swal.fire({swalConfig}).then(() => {{ window.location.href = '{redirectUrl}'; }});";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowToastScript", script, true);
        }
    }
}