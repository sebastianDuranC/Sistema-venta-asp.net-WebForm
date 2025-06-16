using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data; // Añadido para Path.GetFileName

namespace CapaPresentacion.Pages.Productos
{
    public partial class RegistrarProductos : System.Web.UI.Page
    {
        // Esta propiedad nos permitirá mantener la lista de insumos entre clics de botón.
        private DataTable InsumosAgregadosTable
        {
            get
            {
                // Si la tabla no existe en el ViewState, la creamos.
                if (ViewState["InsumosTable"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("InsumoId", typeof(int));
                    dt.Columns.Add("InsumoNombre", typeof(string)); // Para mostrar en el Repeater
                    dt.Columns.Add("Cantidad", typeof(decimal));
                    dt.Columns.Add("Tipo", typeof(string));
                    ViewState["InsumosTable"] = dt;
                }
                return (DataTable)ViewState["InsumosTable"];
            }
            set
            {
                ViewState["InsumosTable"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
                BindInsumosRepeater();
            }
        }

        private void BindInsumosRepeater()
        {
            DataTable dt = this.InsumosAgregadosTable;

            if (dt.Rows.Count > 0)
            {
                rptInsumosAgregados.DataSource = dt;
                rptInsumosAgregados.DataBind();
                rptInsumosAgregados.Visible = true;
                pnlNoInsumos.Visible = false; // Ocultamos el mensaje "No hay insumos"
            }
            else
            {
                rptInsumosAgregados.Visible = false; // Ocultamos la tabla si no hay datos
                pnlNoInsumos.Visible = true; // Mostramos el mensaje
            }
        }

        //Registrar insumos relacionados a un producto
        protected void btnAgregarInsumo_Click(object sender, EventArgs e)
        {
            // Validaciones básicas
            if (ddlInsumo.SelectedValue == "0")
            {
                // Muestra un mensaje al usuario para que seleccione un insumo.
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe seleccionar un insumo.');", true);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtInsumoCantidad.Text) || !decimal.TryParse(txtInsumoCantidad.Text, out _))
            {
                // Muestra un mensaje para que ingrese una cantidad válida.
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Debe ingresar una cantidad válida.');", true);
                return;
            }

            int insumoId = Convert.ToInt32(ddlInsumo.SelectedValue);

            // Obtenemos la tabla actual del ViewState
            DataTable dt = this.InsumosAgregadosTable;

            // Verificamos si el insumo ya fue agregado para evitar duplicados
            if (dt.AsEnumerable().Any(row => row.Field<int>("InsumoId") == insumoId))
            {
                // Opcional: Mostrar un mensaje de que el insumo ya está en la lista.
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('El insumo ya ha sido agregado.');", true);
                return;
            }

            // Creamos una nueva fila y la poblamos
            DataRow newRow = dt.NewRow();
            newRow["InsumoId"] = insumoId;
            newRow["InsumoNombre"] = ddlInsumo.SelectedItem.Text; // Guardamos el nombre para mostrarlo
            newRow["Cantidad"] = Convert.ToDecimal(txtInsumoCantidad.Text);
            newRow["Tipo"] = ddlInsumoTipo.SelectedValue;

            dt.Rows.Add(newRow);

            // Guardamos la tabla actualizada en el ViewState
            this.InsumosAgregadosTable = dt;

            // Actualizamos el Repeater
            BindInsumosRepeater();

            // Limpiamos los campos de insumo
            ddlInsumo.SelectedIndex = 0;
            txtInsumoCantidad.Text = "";
        }
        //Lista de los insumos relacionado a un producto
        protected void rptInsumosAgregados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int insumoId = Convert.ToInt32(e.CommandArgument);

                DataTable dt = this.InsumosAgregadosTable;

                // Buscamos la fila que coincida con el InsumoId a eliminar
                DataRow rowToDelete = dt.AsEnumerable().FirstOrDefault(row => row.Field<int>("InsumoId") == insumoId);

                if (rowToDelete != null)
                {
                    dt.Rows.Remove(rowToDelete);
                }

                // Guardamos la tabla actualizada en el ViewState
                this.InsumosAgregadosTable = dt;

                // Actualizamos la vista del Repeater
                BindInsumosRepeater();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado una foto
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
                FotoUrl = urlRelativaBD, 
                ProductoCategoriaId = categoriaId,
                Estado = true
            };

            // Obtenemos la tabla de insumos desde el ViewState
            DataTable dtInsumosOriginal = this.InsumosAgregadosTable;

            // Creamos una nueva tabla que coincida EXACTAMENTE con el tipo de SQL
            DataTable dtInsumosParaEnviar = new DataTable();
            dtInsumosParaEnviar.Columns.Add("InsumoId", typeof(int));
            dtInsumosParaEnviar.Columns.Add("Cantidad", typeof(decimal));
            dtInsumosParaEnviar.Columns.Add("Tipo", typeof(string));

            // Copiamos los datos necesarios de nuestra tabla del ViewState a la nueva tabla
            foreach (DataRow row in dtInsumosOriginal.Rows)
            {
                dtInsumosParaEnviar.Rows.Add(
                    row["InsumoId"],
                    row["Cantidad"],
                    row["Tipo"]
                );
            }

            bool resultado = productoBLL.RegistrarProducto(producto, dtInsumosParaEnviar);
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

        public void cargarDatos()
        {
            // Cargar categorías de productos en el DropDownList
            ProductoCategoriaBLL productoBLL = new ProductoCategoriaBLL();
            List<ProductoCategoria> categorias = productoBLL.ObtenerCategoriasProducto();
            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

            // Cargar insumos para productos en el dropdownlist
            InsumoBLL insumosBLL = new InsumoBLL();
            List<Insumo> insumos = insumosBLL.ObtenerInsumos();
            ddlInsumo.DataSource = insumos;
            ddlInsumo.DataTextField = "Nombre";
            ddlInsumo.DataValueField = "Id";
            ddlInsumo.DataBind();
        }

        protected void btnVolverProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Productos/Productos.aspx");
        }
    }
}