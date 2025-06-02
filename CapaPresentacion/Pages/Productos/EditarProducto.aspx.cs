using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Productos
{
    public partial class EditarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Obtener el ID del producto desde la URL
                if (int.TryParse(Request.QueryString["Id"], out int productoId))
                {
                    cargarCategorias();
                    cargarProducto(productoId);
                }
                else
                {
                    Response.Redirect("~/Pages/Productos/Productos.aspx");
                }
            }
        }

        protected void btnVolverProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Productos/Productos.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            ProductoBLL productoBLL = new ProductoBLL();

            // Obtener el ID del producto
            int productoId = int.Parse(Request.QueryString["Id"]);

            // Obtener la URL de la imagen actual (por si no se sube una nueva)
            string urlImagenActual = imgProducto.ImageUrl;
            string urlRelativaBD = urlImagenActual;

            // Si el usuario sube una nueva imagen
            if (fotoUrl.HasFile)
            {
                // Obtener el nombre de la foto de forma segura
                string nombreFoto = Path.GetFileName(fotoUrl.PostedFile.FileName);
                string carpetaRelativa = "~/wwwroot/images/Productos/";
                string rutaFisica = Server.MapPath(carpetaRelativa) + nombreFoto;
                urlRelativaBD = carpetaRelativa.Replace("~", "") + nombreFoto;

                // Asegurarse de que la carpeta exista
                string directorioFisico = Server.MapPath(carpetaRelativa);
                if (!Directory.Exists(directorioFisico))
                {
                    Directory.CreateDirectory(directorioFisico);
                }

                try
                {
                    // Guardar la imagen localmente
                    fotoUrl.SaveAs(rutaFisica);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error al guardar la imagen: {ex.Message}');", true);
                    return;
                }
            }
            // Si no se sube nueva imagen, se mantiene la actual (urlRelativaBD ya tiene la actual)

            // Crear el objeto producto con los datos del formulario
            bool resultado = productoBLL.EditarProducto(new Producto
            {
                Id = productoId,
                Nombre = txtNombre.Text.Trim(),
                Precio = decimal.Parse(txtPrecio.Text.Trim()),
                Stock = string.IsNullOrEmpty(txtStock.Text.Trim()) ? (int?)null : int.Parse(txtStock.Text.Trim()),
                StockMinimo = string.IsNullOrEmpty(txtStockMinimo.Text.Trim()) ? (int?)null : int.Parse(txtStockMinimo.Text.Trim()),
                ProductoCategoriaId = int.Parse(ddlCategoria.SelectedValue),
                FotoUrl = urlRelativaBD // Asigna la URL de la imagen (nueva o actual)
            });

            if (resultado)
            {
                // Si la edición fue exitosa, redirigir a la lista de productos
                Response.Redirect("~/Pages/Productos/Productos.aspx");
            }
            else
            {
                // Si la edición falló, mostrar un mensaje de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al editar el producto. Por favor, verifique los datos ingresados.');", true);
            }
        }

        public void cargarCategorias()
        {
            ProductoCategoriaBLL productoBLL = new ProductoCategoriaBLL();
            List<ProductoCategoria> categorias = productoBLL.ObtenerCategoriasProducto();
            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
        }

        public void cargarProducto(int productoId)
        {
            ProductoBLL productoBLL = new ProductoBLL();
            var producto = productoBLL.ObtenerProductoPorId(productoId);
            if (producto != null && producto.Rows.Count > 0)
            {
                idProducto.Value = producto.Rows[0]["Id"].ToString();
                txtNombre.Text = producto.Rows[0]["Nombre"].ToString();

                // Conversión segura del precio a string con formato adecuado
                object precioObj = producto.Rows[0]["Precio"];
                if (precioObj != DBNull.Value && decimal.TryParse(precioObj.ToString(), out decimal precio))
                {
                    txtPrecio.Text = precio.ToString("0.##");
                }
                else
                {
                    txtPrecio.Text = string.Empty;
                }

                txtStock.Text = producto.Rows[0]["Stock"].ToString();
                txtStockMinimo.Text = producto.Rows[0]["StockMinimo"].ToString();
                ddlCategoria.SelectedValue = producto.Rows[0]["ProductoCategoriaId"].ToString();

                // Asegurarse de que la URL de la imagen sea relativa y comience con ~/
                string fotoUrl = producto.Rows[0]["FotoUrl"].ToString();
                if (!string.IsNullOrEmpty(fotoUrl))
                {
                    if (!fotoUrl.StartsWith("~/"))
                    {
                        fotoUrl = "~" + fotoUrl;
                    }
                    imgProducto.ImageUrl = fotoUrl;
                }
            }
        }
    }
}