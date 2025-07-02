using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Clientes
{
    public partial class EditarCliente : System.Web.UI.Page
    {
        CapaEntidades.Cliente cliente = new CapaEntidades.Cliente();
        ClienteBLL clienteBLL = new ClienteBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idcliente = Convert.ToInt32(Request.QueryString["Id"]);
                cargarDatos(idcliente);
            }
        }

        private void cargarDatos(int clienteId)
        {
            cliente = clienteBLL.ObtenerClientePorId(clienteId);
            txtNombre.Text = cliente.Nombre;
            txtNumeroLocal.Text = cliente.NumeroLocal;
            txtPasillo.Text = cliente.Pasillo;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Clientes/Clientes.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                cliente = new CapaEntidades.Cliente
                {
                    Id = Convert.ToInt32(Request.QueryString["Id"]),
                    Nombre = txtNombre.Text.Trim(),
                    NumeroLocal = txtNumeroLocal.Text.Trim(),
                    Pasillo = txtPasillo.Text.Trim()
                };

                bool resultado = clienteBLL.ActualizarCliente(cliente);
                if (resultado)
                {
                    ShowToast("Cliente editado", "success", "Clientes.aspx");
                }
                else
                {
                    ShowToast("Error al editar el cliente", "error", "Clientes.aspx");
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