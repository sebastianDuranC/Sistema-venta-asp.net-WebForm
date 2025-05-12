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
                CN_Rol rolNegocio = new CN_Rol();
                var roles = rolNegocio.ObtenerRoles();
                rptRoles.DataSource = roles;
                rptRoles.DataBind();
            }
        }

        protected void rptRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Rol rol = (Rol)e.Item.DataItem;
                int rolId = rol.Id;

                Repeater rptForms = e.Item.FindControl("rptForm") as Repeater;
                CN_Form formNegocio = new CN_Form();
                var forms = formNegocio.obtenerForm();
                rptForms.DataSource = forms;
                rptForms.DataBind();

                CN_RolPermisos permisosNegocio = new CN_RolPermisos();

                foreach (RepeaterItem formItem in rptForms.Items)
                {
                    int formId = Convert.ToInt32((formItem.FindControl("hdnFormId") as HiddenField).Value);
                    CheckBox checkFormPermisos = formItem.FindControl("checkFormPermisos") as CheckBox;

                    bool isAllowed = permisosNegocio.ObtenerEsPermitidoForm(rolId, formId);
                    checkFormPermisos.Checked = isAllowed;
                }
            }
        }

        protected void btnRolPermisos_Click(object sender, EventArgs e)
        {
            int totalUpdates = 0;
            try
            {
                if (Session["usuario"] != null)
                {
                    foreach (RepeaterItem rolItem in rptRoles.Items)
                    {
                        int rolId = Convert.ToInt32(((Label)rolItem.FindControl("lblRolId")).Text);
                        Repeater rptForm = (Repeater)rolItem.FindControl("rptForm");
                        foreach (RepeaterItem formItem in rptForm.Items)
                        {
                            int formId = Convert.ToInt32(((HiddenField)formItem.FindControl("hdnFormId")).Value);
                            CheckBox checkBoxPermisos = (CheckBox)formItem.FindControl("checkFormPermisos");
                            bool isChecked = checkBoxPermisos.Checked;

                            CN_RolPermisos permisosNegocio = new CN_RolPermisos();
                            // Si tu método ActualizarRolPermisos puede devolver un bool/int de éxito, úsalo aquí
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
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al guardar permisos: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void rptForm_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}