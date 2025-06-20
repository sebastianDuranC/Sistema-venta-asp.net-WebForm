using CapaNegocio;
using CapaPresentacion.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace CapaPresentacion.Pages.Rol
{
    public partial class RegistrarRol : System.Web.UI.Page
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
            var permisos = permisoBLL.obtenerForm()
                .OrderBy(p => p.FormRuta)
                .ToList();
            rptPermisos.DataSource = permisos;
            rptPermisos.DataBind();
        }

        protected void btnCrearRol_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string nombreRol = txtNombreRol.Text.Trim();
                List<int> permisosSeleccionadosIds = new List<int>();

                foreach (RepeaterItem item in rptPermisos.Items)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        CheckBox chk = (CheckBox)item.FindControl("chkPermiso");
                        if (chk != null && chk.Checked)
                        {
                            HiddenField hf = (HiddenField)item.FindControl("hfPermisoId");
                            if (hf != null && int.TryParse(hf.Value, out int permisoId))
                            {
                                permisosSeleccionadosIds.Add(permisoId);
                            }
                        }
                    }
                }

                try
                {
                    RolBLL _rolBLL = new RolBLL();
                    CapaEntidades.Rol nuevoRol = new CapaEntidades.Rol { Nombre = nombreRol };
                    bool exito = _rolBLL.RegistrarRolConPermisos(nuevoRol, permisosSeleccionadosIds);
                    if (exito)
                    {
                        txtNombreRol.Text = string.Empty;
                        // Desmarcar todos los checkboxes
                        foreach (RepeaterItem item in rptPermisos.Items)
                        {
                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                ((CheckBox)item.FindControl("chkPermiso")).Checked = false;
                            }
                        }
                        // Creamos una función para encapsular la lógica y reutilizarla
                        ShowSweetAlert( "El rol ha sido creado correctamente", "success");
                    }
                    else
                    {
                        ShowSweetAlert("No se pudo crear el rol", "warning");
                    }
                }
                catch (ArgumentException argEx)
                {
                    ShowSweetAlert(argEx.Message, "error");
                }
                catch (Exception EX)
                {
                    // Atrapa cualquier otro tipo de error inesperado (fallo de conexión a la BD, etc.)
                    ShowSweetAlert( "Ha ocurrido un problema en el servidor. Intente de nuevo más tarde.", "error");
                }
            }
        }

        /// <summary>
        /// Muestra una notificación emergente no intrusiva (toast) usando SweetAlert2.
        /// </summary>
        /// <param name="title">El mensaje principal que se mostrará en el toast.</param>
        /// <param name="icon">El tipo de ícono a mostrar ('success', 'error', 'warning', 'info', 'question').</param>
        private void ShowSweetAlert(string title, string icon)
        {
            // Este script coincide con el ejemplo de "custom positioned dialog".
            // Es ideal para notificaciones rápidas que no requieren interacción del usuario.
            string script = $@"
        Swal.fire({{
            position: 'top-end',
            icon: '{icon}',
            title: '{title}',
            showConfirmButton: false,
            timer: 2500,
            toast: true
        }});";

            // Usamos ScriptManager para ejecutar el script en el lado del cliente.
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ShowToastScript", // Una clave única para el script
                script,
                true // Agrega las etiquetas <script> automáticamente
            );
        }
    }
}