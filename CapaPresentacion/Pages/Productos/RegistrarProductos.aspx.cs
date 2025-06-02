using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO; // Añadido para Path.GetFileName

namespace CapaPresentacion.Pages.Productos
{
    public partial class RegistrarProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();
            }
        }

        protected void btnVolverProductos_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Productos/Productos.aspx");
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado una foto
            // Mantengo esta validación porque es una excepción que no viene de un campo de texto
            // y es crucial para la lógica de guardado de la imagen.
            if (!fotoUrl.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe seleccionar una imagen para el producto.');", true);
                return;
            }

            // Obtener el nombre de la foto de forma segura (solo el nombre del archivo, sin ruta cliente)
            string nombreFoto = Path.GetFileName(fotoUrl.PostedFile.FileName); // Usar PostedFile.FileName para mayor seguridad
            string carpetaRelativa = "~/wwwroot/images/Productos/"; // Ruta relativa al sitio web
            string rutaFisica = Server.MapPath(carpetaRelativa) + nombreFoto; // Ruta física en el servidor
            string urlRelativaBD = carpetaRelativa.Replace("~", "") + nombreFoto; // Ruta para guardar en la BD

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


            // Las conversiones asumen que los datos son válidos
            // (basado en tu indicación de que las validaciones se manejan en CapaNegocio)
            string nombreProducto = "";
            nombreProducto = txtNombre.Text.Trim();
            decimal precio = Convert.ToDecimal(txtPrecio.Text.Trim());

            // Usamos TryParse para Stock y StockMinimo para manejar int? correctamente
            // y evitar el error si el campo está vacío, sin lanzar una excepción aquí.
            int? stock = null;
            if (int.TryParse(txtStock.Text.Trim(), out int tempStock))
            {
                stock = tempStock;
            }

            int? stockMinimo = null;
            if (int.TryParse(txtStockMinimo.Text.Trim(), out int tempStockMinimo))
            {
                stockMinimo = tempStockMinimo;
            }
            int categoriaId = Convert.ToInt32(ddlCategoria.SelectedValue);


            ProductoBLL productoBLL = new ProductoBLL();
            Producto producto = new Producto
            {
                Nombre = nombreProducto,
                Precio = precio,
                Stock = stock,
                StockMinimo = stockMinimo,
                FotoUrl = urlRelativaBD, // Usar la URL relativa que se guardará en la BD
                ProductoCategoriaId = categoriaId,
                Estado = true // Asumiendo que por defecto el producto está activo al registrar
            };

            bool resultado = productoBLL.RegistrarProducto(producto);
            if (resultado)
            {
                limpiarCampos();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Producto registrado exitosamente.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error al registrar el producto. La Capa de Negocio no pudo registrarlo. Verifique logs.');", true);
            }
        }

        private void limpiarCampos()
        {
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtStockMinimo.Text = "";
            ddlCategoria.SelectedValue = "0";
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
    }
}