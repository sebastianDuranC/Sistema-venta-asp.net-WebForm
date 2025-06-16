using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
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

            if (usuarioBLL.RegistrarUsuario(usuario))
            {
                //Mostrar mensaje de guardado exitoso y redirigir
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Usuario registrado exitosamente.'); window.location.href = 'Usuarios.aspx';", true);
            }
            else
            {
                //Mostrar mensaje de error
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error al registrar el usuario.');", true);
            }
        }
    }
}