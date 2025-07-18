using CapaNegocio;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Insumos
{
    public partial class RegistrarInsumo : System.Web.UI.Page
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
            //CATEGORIA PARA INSUMOS
            InsumoCategoriaBLL insumoBLL = new InsumoCategoriaBLL();
            InsumoCategoriaId.DataSource = insumoBLL.ObtenerInsumoCategorias();
            InsumoCategoriaId.DataTextField = "Nombre";
            InsumoCategoriaId.DataValueField = "Id";
            InsumoCategoriaId.DataBind();
            InsumoCategoriaId.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

            //PROVEEDORES 
            ProveedorBLL proveedorBLL = new ProveedorBLL();
            ProveedorId.DataSource = proveedorBLL.ObtenerProveedores();
            ProveedorId.DataTextField = "Nombre";
            ProveedorId.DataValueField = "Id";
            ProveedorId.DataBind();
            ProveedorId.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

            //UNIDADES DE MEDIDA
            UnidadMedidaBLL unidadesMedidaBLL = new UnidadMedidaBLL();
            UnidadDeMedidaId.DataSource = unidadesMedidaBLL.ObtenerUnidadesMedida();
            UnidadDeMedidaId.DataTextField = "Nombre";
            UnidadDeMedidaId.DataValueField = "Id";
            UnidadDeMedidaId.DataBind();
            UnidadDeMedidaId.Items.Insert(0, new ListItem("Seleccione una unidad de medida", "0"));
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Insumos/Insumos.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            InsumoBLL insumoNuevo = new InsumoBLL();
            string fotoUrl = null;

            // Guardar la foto si se seleccionó una
            if (flUploadFoto.HasFile)
            {
                string folderPath = Server.MapPath("~/wwwroot/images/Insumos/");
                // Crear la carpeta si no existe
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }

                string fileName = System.IO.Path.GetFileName(flUploadFoto.FileName);
                string filePath = System.IO.Path.Combine(folderPath, fileName);

                // Guardar el archivo
                flUploadFoto.SaveAs(filePath);

                // Construir la URL completa para mostrar en la vista
                fotoUrl = ResolveUrl("~/wwwroot/images/Insumos/" + fileName);
            }

            CapaEntidades.Insumo insumos = new CapaEntidades.Insumo
            {
                Nombre = txtNombre.Text.Trim(),
                Costo = string.IsNullOrWhiteSpace(txtCosto.Text) ? (decimal?)null : Convert.ToDecimal(txtCosto.Text),
                Stock = string.IsNullOrWhiteSpace(txtStock.Text) ? (decimal?)null : Convert.ToDecimal(txtStock.Text),
                StockMinimo = string.IsNullOrWhiteSpace(txtStockMinimo.Text) ? (decimal?)null : Convert.ToDecimal(txtStockMinimo.Text),
                InsumoCategoriaId = Convert.ToInt32(InsumoCategoriaId.SelectedValue),
                ProveedorId = Convert.ToInt32(ProveedorId.SelectedValue),
                UnidadesMedidaId = Convert.ToInt32(UnidadDeMedidaId.SelectedValue),
                FotoUrl = fotoUrl
            };

            try
            {
                bool resultado = insumoNuevo.RegistrarInsumo(insumos);
                if (resultado)
                {
                    ShowToast("Registro exitoso", "success");
                    limpiarInputs();
                }
                else
                {
                    ShowToast("Error al registrar el insumo", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "warning");
            }
        }

        private void limpiarInputs()
        {
            txtNombre.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtStock.Text = string.Empty;
            txtStockMinimo.Text = string.Empty;
            InsumoCategoriaId.SelectedIndex = 0;
            ProveedorId.SelectedIndex = 0;
            UnidadDeMedidaId.SelectedIndex = 0;
            flUploadFoto.Attributes.Clear(); // Limpia el archivo subido
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