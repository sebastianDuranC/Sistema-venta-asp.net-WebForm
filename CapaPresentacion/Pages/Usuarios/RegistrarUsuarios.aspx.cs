using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Usuarios
{
    public partial class RegistrarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarRoles();
            }
        }

        private void cargarRoles()
        {
            RolBLL rolBLL = new RolBLL();
            ddlRol.DataSource = rolBLL.ObtenerRoles();
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataValueField = "Id";
            ddlRol.DataBind();
            ddlRol.Items.Insert(0, new ListItem("Seleccione un rol", "0")); // Agregar opción por defecto
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/Usuarios.aspx");
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            string nombreUsuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();
            int rolId = Convert.ToInt32(ddlRol.SelectedValue);
            
            Usuario usuario = new Usuario
            {
                Nombre = nombreUsuario,
                Contra = password,
                RolId = rolId,
                NegocioId = 1
            };

            try
            {
                if (usuarioBLL.RegistrarUsuario(usuario))
                {
                    ShowToast("¡Registrado exitosamente!", "success");
                    limpiarInputs();
                }
                else
                {
                    ShowToast("No se pudo registrar el usuario.", "error");
                }
            }
            catch (ArgumentException ex)
            {
                ShowToast(ex.Message, "warning");
            }
        }

        private void limpiarInputs()
        {
            txtUsuario.Text = string.Empty;
            txtPassword.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
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
    }
}