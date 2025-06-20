using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.rolpermisosmapping
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        protected void rptRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CapaEntidades.Rol rol = (CapaEntidades.Rol)e.Item.DataItem;
                int rolId = rol.Id;

                Repeater rptForms = e.Item.FindControl("rptForm") as Repeater;
                PermisoBLL permisoForm  = new PermisoBLL();
                var forms = permisoForm.obtenerForm();
                rptForms.DataSource = forms;
                rptForms.DataBind();

                //Aqui indicamos que si el roldId y el permisoId(form) tienen el estado=1 en la tabla RolPermiso, entonces el checkbox se marca como permitido
                RolPermisoBLL permisosNegocio = new RolPermisoBLL();

                foreach (RepeaterItem formItem in rptForms.Items)
                {
                    int formId = Convert.ToInt32((formItem.FindControl("hdnFormId") as HiddenField).Value);
                    CheckBox checkFormPermisos = formItem.FindControl("checkFormPermisos") as CheckBox;

                    bool isAllowed = permisosNegocio.ObtenerEsPermitidoForm(rolId, formId);
                    checkFormPermisos.Checked = isAllowed;
                }
            }
        }

        public void CargarDatos()
        {
            // Cargar los roles desde la base de datos y enlazarlos al Repeater
            RolBLL rolNegocio = new RolBLL();
            var roles = rolNegocio.ObtenerRoles();
            rptRoles.DataSource = roles;
            rptRoles.DataBind();
        }

        protected void btnRolPermisos_Click(object sender, EventArgs e)
        {
            EditarPermisos();
        }

        private void EditarPermisos()
        {
            int totalUpdates = 0;
            if (Session["usuario"] != null)
            {
                foreach (RepeaterItem rolItem in rptRoles.Items)
                {
                    int rolId = Convert.ToInt32(((Label)rolItem.FindControl("lblRolId")).Text);
                    Repeater rptForm = (Repeater)rolItem.FindControl("rptForm");
                    foreach (RepeaterItem formItem in rptForm.Items)
                    {
                        int formId = Convert.ToInt32(((HiddenField)formItem.FindControl("hdnFormId")).Value); //PermisoId
                        CheckBox checkBoxPermisos = (CheckBox)formItem.FindControl("checkFormPermisos"); // Estado del checkbox/rolpermisos
                        bool isChecked = checkBoxPermisos.Checked;

                        RolPermisoBLL permisosNegocio = new RolPermisoBLL();
                        permisosNegocio.ActualizarRolPermisos(rolId, formId, isChecked);
                        totalUpdates++;
                    }
                }
            }

            if (totalUpdates > 0)
            {
                lblMensaje.Text = "Permisos guardados correctamente.";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMensaje.Text = "¡Error al guardar permisos!";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}