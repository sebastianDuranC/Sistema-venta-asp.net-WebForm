using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Rol
{
    public partial class EditarRol : System.Web.UI.Page
    {
        RolBLL rolBLL = new RolBLL();
        private List<RolPermisos> _permisosDelRolActual;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null && int.TryParse(Request.QueryString["Id"], out int id))
                {
                    cargarDatos(id);
                }
                else
                {
                    Response.Redirect("Rol.aspx"); 
                }
            }
        }

        private void cargarDatos(int id)
        {
            RolBLL rolBLL = new RolBLL();
            PermisoBLL permisoBLL = new PermisoBLL();

            // 1. Obtenemos el rol específico con su lista de permisos asignados
            var rolAEditar = rolBLL.ObtenerRolPorId(id);

            // 2. Obtenemos TODOS los permisos disponibles para mostrarlos en la lista
            var todosLosPermisos = permisoBLL.obtenerForm();

            try
            {
                if (rolAEditar != null)
                {
                    // Guardamos los datos del rol en los controles del formulario
                    hfRolId.Value = rolAEditar.Id.ToString();
                    txtNombreRol.Text = rolAEditar.Nombre;

                    // Almacenamos la lista de permisos del rol en nuestra variable de clase
                    _permisosDelRolActual = rolAEditar.RolesPermisos.ToList();

                    // Asignamos la lista COMPLETA de permisos al Repeater
                    rptPermisos.DataSource = todosLosPermisos.OrderBy(p => p.FormRuta);
                    rptPermisos.DataBind(); // Esto disparará el evento ItemDataBound
                }
                else
                {
                    ShowToast("El rol que intenta editar no existe.", "error");
                    btnGuardarCambios.Enabled = false;
                }
            }
            catch (ArgumentException arg)
            {
                ShowToast("Error al cargar los datos." + arg, "error");
            }
        }

        protected void rptPermisos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Salvaguarda por si la lista de permisos del rol está vacía o no se cargó
                if (_permisosDelRolActual == null) return;

                // Obtenemos el ID del permiso que se está renderizando en esta fila del Repeater
                var hfPermisoId = (HiddenField)e.Item.FindControl("hfPermisoId");
                int permisoActualId = int.Parse(hfPermisoId.Value);

                // La clave: Verificamos si en la lista de permisos del rol actual existe alguno
                // cuyo PermisosId coincida con el ID del permiso de esta fila.
                bool tienePermiso = _permisosDelRolActual.Any(p => p.PermisosId == permisoActualId);

                if (tienePermiso)
                {
                    // Si tiene el permiso, encontramos el CheckBox de esta fila y lo marcamos.
                    var chkPermiso = (CheckBox)e.Item.FindControl("chkPermiso");
                    chkPermiso.Checked = true;
                }
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Recolectar datos del formulario
                var rolEditado = new CapaEntidades.Rol
                {
                    Id = int.Parse(hfRolId.Value),
                    Nombre = txtNombreRol.Text.Trim()
                };

                // 2. Recolectar la nueva lista de permisos seleccionados
                var permisosSeleccionadosIds = new List<int>();
                foreach (RepeaterItem item in rptPermisos.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var chk = (CheckBox)item.FindControl("chkPermiso");
                        if (chk.Checked)
                        {
                            var hf = (HiddenField)item.FindControl("hfPermisoId");
                            permisosSeleccionadosIds.Add(int.Parse(hf.Value));
                        }
                    }
                }

                bool resultado = rolBLL.EditarRol(rolEditado, permisosSeleccionadosIds);
                if (resultado)
                {
                    ShowToast("Rol actualizado exitosamente", "success", "Rol.aspx");
                }
            }
            catch (ArgumentException argEx)
            {
                ShowToast(argEx.Message, "error");
            }
            catch (Exception ex)
            {
                ShowToast("Error inesperado al guardar." + ex, "error");
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