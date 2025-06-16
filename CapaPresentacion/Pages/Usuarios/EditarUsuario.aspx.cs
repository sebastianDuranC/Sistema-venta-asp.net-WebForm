using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Usuarios
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }

        private void cargarDatos()
        {
            //cargar los datos de roles en el DropDownList
            RolBLL rol = new RolBLL();
            ddlRol.DataSource = rol.ObtenerRoles();
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataValueField = "Id";
            ddlRol.DataBind();

            // Cargar los datos de los usuarios en el Repeater
            // 1. Obtener el DataTable desde usuarioBLL.ObtenerUsuarioPorId(...)
            // 2. Usar LINQ para seleccionar la primera fila (si existe).
            // 3. Asignar los valores de las columnas a los controles txt.

            // Código:
            var dt = usuarioBLL.ObtenerUsuarioPorId(Convert.ToInt32(Request.QueryString["id"]));
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.AsEnumerable().First();
                txtUsuario.Text = row.Field<string>("Nombre");
                ddlRol.SelectedValue = row.Field<int>("RolId").ToString();
                // Si tienes más campos, puedes asignarlos aquí
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Usuarios/Usuarios.aspx");
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contra = txtPassword.Text.Trim();
            int rolId = Convert.ToInt32(ddlRol.SelectedValue);
            int usuarioId = Convert.ToInt32(Request.QueryString["Id"]);

            Usuario usuario = new Usuario
            {
                Id = usuarioId,
                Nombre = nombreUsuario,
                Contra = contra, // Asegúrate de hashear la contraseña antes de guardarla
                RolId = rolId,
                NegocioId = 1
            };

            if (usuarioBLL.EditarUsuario(usuario))
            {
                //mostrar mensaje de éxito
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Usuario editado correctamente.'); window.location.href = 'Usuarios.aspx';", true);
            }
            else
            {
                //mostrar mensaje de error
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error al editar el usuario.');", true);
            }
        }
    }
}