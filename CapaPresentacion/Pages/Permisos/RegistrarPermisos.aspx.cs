using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Form
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarForm_Click(object sender, EventArgs e)
        {
            string nombreForm = txtFormNombre.Text.Trim();
            string formRuta = txtformRuta.Text.Trim();

            PermisoBLL form = new PermisoBLL();
            if (form.RegistrarForm(nombreForm, formRuta))
            {
                lblMensaje.Text = "Formulario registrado correctamente.";
            }
            else
            {
                lblMensaje.Text = "Error al registrar el formulario.";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Permisos/Permisos.aspx");
        }
    }
}