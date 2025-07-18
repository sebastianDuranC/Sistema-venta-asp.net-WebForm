using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Insumos
{
    public partial class EditarInsumo : System.Web.UI.Page
    {
        InsumoBLL InsumoBLL = new InsumoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                cargarDDLs();
                cargarDatos(id);
            }
        }

        private void cargarDDLs()
        {
            InsumoCategoriaBLL insumoCategoriaBLL = new InsumoCategoriaBLL();
            InsumoCategoriaId.DataSource = insumoCategoriaBLL.ObtenerInsumoCategorias();
            InsumoCategoriaId.DataTextField = "Nombre";
            InsumoCategoriaId.DataValueField = "Id";
            InsumoCategoriaId.DataBind();
            InsumoCategoriaId.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

            ProveedorBLL proveedorBLL = new ProveedorBLL();
            ProveedorId.DataSource = proveedorBLL.ObtenerProveedores();
            ProveedorId.DataTextField = "Nombre";
            ProveedorId.DataValueField = "Id";
            ProveedorId.DataBind();
            ProveedorId.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

            UnidadMedidaBLL unidadDeMedidaBLL = new UnidadMedidaBLL();
            UnidadDeMedidaId.DataSource = unidadDeMedidaBLL.ObtenerUnidadesMedida();
            UnidadDeMedidaId.DataTextField = "Nombre";
            UnidadDeMedidaId.DataValueField = "Id";
            UnidadDeMedidaId.DataBind();
            UnidadDeMedidaId.Items.Insert(0, new ListItem("Seleccione una unidad de medida", "0"));
        }

        private void cargarDatos(int id)
        {
            Insumo insumo = InsumoBLL.ObtenerInsumoPorId(id);
            if (insumo != null)
            {
                txtNombre.Text = insumo.Nombre ?? string.Empty;
                txtCosto.Text = insumo.Costo.HasValue ? insumo.Costo.Value.ToString("0.##") : string.Empty;
                txtStock.Text = insumo.Stock.HasValue ? insumo.Stock.Value.ToString("0.##") : string.Empty;
                txtStockMinimo.Text = insumo.StockMinimo.HasValue ? insumo.StockMinimo.Value.ToString("0.##") : string.Empty;
                InsumoCategoriaId.SelectedValue = insumo.InsumoCategoriaId.ToString();
                ProveedorId.SelectedValue = insumo.ProveedorId.ToString();
                UnidadDeMedidaId.SelectedValue = insumo.UnidadesMedidaId.ToString();
                imgFoto.ImageUrl = !string.IsNullOrEmpty(insumo.FotoUrl) ? insumo.FotoUrl : "~/wwwroot/images/no-image.png";
            }
            else
            {
                Response.Redirect("~/Pages/Insumos/Insumos.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Insumos/Insumos.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string fotoUrl = imgFoto.ImageUrl;
            // Guardar imagen localmente si es una nueva imagen subida (no la imagen por defecto)
            if (flpFotoNueva.HasFile)
            {
                // Obtener extensión y nombre seguro
                string extension = System.IO.Path.GetExtension(flpFotoNueva.FileName);
                string fileName = $"insumo_{Request.QueryString["Id"]}_{DateTime.Now.Ticks}{extension}";
                string relativePath = $"~/wwwroot/images/Insumos/{fileName}";
                string serverPath = Server.MapPath(relativePath);

                // Crear directorio si no existe
                string dir = System.IO.Path.GetDirectoryName(serverPath);
                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);

                // Guardar archivo
                flpFotoNueva.SaveAs(serverPath);
                fotoUrl = relativePath;
            }

            Insumo insumo = new Insumo
            {
                Id = Convert.ToInt32(Request.QueryString["Id"]),
                Nombre = txtNombre.Text.Trim(),
                Costo = string.IsNullOrWhiteSpace(txtCosto.Text) ? (decimal?)null : Convert.ToDecimal(txtCosto.Text),
                Stock = string.IsNullOrWhiteSpace(txtStock.Text) ? (decimal?)null : Convert.ToDecimal(txtStock.Text),
                StockMinimo = string.IsNullOrWhiteSpace(txtStockMinimo.Text) ? (decimal?)null : Convert.ToDecimal(txtStockMinimo.Text),
                InsumoCategoriaId = Convert.ToInt32(InsumoCategoriaId.SelectedValue),
                ProveedorId = Convert.ToInt32(ProveedorId.SelectedValue),
                UnidadesMedidaId = Convert.ToInt32(UnidadDeMedidaId.SelectedValue),
                FotoUrl = fotoUrl,
            };
            try
            {
                bool resultado = InsumoBLL.EditarInsumo(insumo);
                if (resultado)
                {
                    ShowToast("Editado exitosamente", "success", "Insumos.aspx");
                }
                else
                {
                    ShowToast("Error al editar el insumo", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "error");
            }
        }

        private void ShowToast(string titulo, string icono, string redirectUrl="")
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