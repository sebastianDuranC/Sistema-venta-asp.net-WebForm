using CapaEntidades;
using CapaNegocio;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Collections.Generic;

namespace CapaPresentacion.Pages.Productos
{
    public partial class EditarProducto : System.Web.UI.Page
    {
        private DataTable InsumosAgregadosTable
        {
            get
            {
                if (ViewState["InsumosTable"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("InsumoId", typeof(int));
                    dt.Columns.Add("InsumoNombre", typeof(string));
                    dt.Columns.Add("Cantidad", typeof(decimal));
                    dt.Columns.Add("Tipo", typeof(string));
                    ViewState["InsumosTable"] = dt;
                }
                return (DataTable)ViewState["InsumosTable"];
            }
            set { ViewState["InsumosTable"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarControles();
                if (int.TryParse(Request.QueryString["Id"], out int productoId))
                {
                    CargarDatosProducto(productoId);
                }
                else
                {
                    Response.Redirect("Productos.aspx");
                }
            }
        }

        private void CargarDatosProducto(int productoId)
        {
            ProductoBLL productoBLL = new ProductoBLL();
            DataTable producto = productoBLL.ObtenerProductoPorId(productoId);

            if (producto != null && producto.Rows.Count > 0)
            {
                DataRow row = producto.Rows[0];
                hfProductoId.Value = row["Id"].ToString();
                txtNombre.Text = row["Nombre"].ToString();
                txtPrecio.Text = row["Precio"] != DBNull.Value ? Convert.ToDecimal(row["Precio"]).ToString(System.Globalization.CultureInfo.InvariantCulture) : "0"; txtStock.Text = row["Stock"] != DBNull.Value ? row["Stock"].ToString() : "0";
                txtStockMinimo.Text = row["StockMinimo"] != DBNull.Value ? row["StockMinimo"].ToString() : "0";
                ddlCategoria.SelectedValue = row["ProductoCategoriaId"].ToString();

                string fotoUrl = row.Table.Columns.Contains("FotoUrl") ? row["FotoUrl"].ToString() : string.Empty;
                if (!string.IsNullOrEmpty(fotoUrl))
                {
                    imgPreview.ImageUrl = ResolveUrl(fotoUrl);
                    hfFotoUrlActual.Value = fotoUrl;
                    divPlaceholder.Visible = false;
                }

                ProductoInsumoBLL productoInsumoBLL = new ProductoInsumoBLL();
                DataTable receta = productoInsumoBLL.ObtenerRecetaPorProductoId(productoId);
                this.InsumosAgregadosTable = receta;
                BindInsumosRepeater();
            }
            else
            {
                Response.Redirect("~/Pages/Productos/Productos.aspx");
            }
        }

        private Producto MapearFormularioAProducto()
        {
            int id = 0, stock = 0, stockMinimo = 0, categoriaId = 0;
            decimal precio = 0;

            int.TryParse(hfProductoId.Value, out id);
            decimal.TryParse(txtPrecio.Text, out precio);
            int.TryParse(txtStock.Text, out stock);
            int.TryParse(txtStockMinimo.Text, out stockMinimo);
            int.TryParse(ddlCategoria.SelectedValue, out categoriaId);

            return new Producto
            {
                Id = id,
                Nombre = txtNombre.Text.Trim(),
                Precio = precio,
                Stock = stock,
                StockMinimo = stockMinimo,
                ProductoCategoriaId = categoriaId
            };
        }

        private string GestionarSubidaDeImagen()
        {
            string urlParaGuardar = hfFotoUrlActual.Value;

            if (fotoUrl.HasFile)
            {
                string carpetaRelativa = "~/wwwroot/images/Productos/";
                string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(fotoUrl.FileName);
                string rutaFisica = Server.MapPath(carpetaRelativa + nombreArchivo);

                fotoUrl.SaveAs(rutaFisica);
                urlParaGuardar = carpetaRelativa.Replace("~", "") + nombreArchivo;

                if (!string.IsNullOrEmpty(hfFotoUrlActual.Value))
                {
                    string rutaAntigua = Server.MapPath(hfFotoUrlActual.Value);
                    if (File.Exists(rutaAntigua))
                    {
                        File.Delete(rutaAntigua);
                    }
                }
            }

            return urlParaGuardar;
        }

        private DataTable PrepararInsumosParaGuardar()
        {
            DataTable dtOriginal = this.InsumosAgregadosTable;
            DataTable dtParaEnviar = new DataTable();
            dtParaEnviar.Columns.Add("InsumoId", typeof(int));
            dtParaEnviar.Columns.Add("Cantidad", typeof(decimal));
            dtParaEnviar.Columns.Add("Tipo", typeof(string));

            foreach (DataRow row in dtOriginal.Rows)
            {
                dtParaEnviar.Rows.Add(row["InsumoId"], row["Cantidad"], row["Tipo"]);
            }
            return dtParaEnviar;
        }

        private void CargarControles()
        {
            ProductoCategoriaBLL productoCategoriaBLL = new ProductoCategoriaBLL();
            ddlCategoria.DataSource = productoCategoriaBLL.ObtenerCategoriasProducto();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione", "0"));

            InsumoBLL insumoBLL = new InsumoBLL();
            ddlInsumo.DataSource = insumoBLL.ObtenerInsumos();
            ddlInsumo.DataTextField = "Nombre";
            ddlInsumo.DataValueField = "Id";
            ddlInsumo.DataBind();
            ddlInsumo.Items.Insert(0, new ListItem("Seleccione", "0"));
        }

        private void BindInsumosRepeater()
        {
            DataTable dt = this.InsumosAgregadosTable;
            rptInsumosAgregados.DataSource = dt;
            rptInsumosAgregados.DataBind();
            pnlNoInsumos.Visible = dt.Rows.Count == 0;
            rptInsumosAgregados.Visible = dt.Rows.Count > 0;
        }

        protected void btnAgregarInsumo_Click1(object sender, EventArgs e)
        {
            if (ddlInsumo.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtInsumoCantidad.Text)) return;

            int insumoId = Convert.ToInt32(ddlInsumo.SelectedValue);
            DataTable dt = this.InsumosAgregadosTable;

            if (dt.AsEnumerable().Any(row => row.Field<int>("InsumoId") == insumoId)) return;

            DataRow newRow = dt.NewRow();
            newRow["InsumoId"] = insumoId;
            newRow["InsumoNombre"] = ddlInsumo.SelectedItem.Text;
            newRow["Cantidad"] = Convert.ToDecimal(txtInsumoCantidad.Text);
            newRow["Tipo"] = ddlInsumoTipo.SelectedValue;
            dt.Rows.Add(newRow);

            this.InsumosAgregadosTable = dt;
            BindInsumosRepeater();

            ddlInsumo.SelectedIndex = 0;
            txtInsumoCantidad.Text = "";
        }

        protected void rptInsumosAgregados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int insumoId = Convert.ToInt32(e.CommandArgument);
                DataTable dt = this.InsumosAgregadosTable;
                DataRow rowToDelete = dt.AsEnumerable().FirstOrDefault(row => row.Field<int>("InsumoId") == insumoId);

                if (rowToDelete != null)
                {
                    dt.Rows.Remove(rowToDelete);
                }

                this.InsumosAgregadosTable = dt;
                BindInsumosRepeater();
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                Producto productoEditado = MapearFormularioAProducto();
                productoEditado.FotoUrl = GestionarSubidaDeImagen();
                DataTable insumosFinales = PrepararInsumosParaGuardar();

                ProductoBLL productoBLL = new ProductoBLL();
                bool exito = productoBLL.EditarProducto(productoEditado, insumosFinales);

                if (exito)
                {
                    string script = "alert('¡Producto actualizado con éxito!'); window.location='Productos.aspx';";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertRedirect", script, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertError", "alert('No se pudo actualizar el producto.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertException", $"alert('Ocurrió un error inesperado: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnVolverProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Productos/Productos.aspx");
        }
    }
}