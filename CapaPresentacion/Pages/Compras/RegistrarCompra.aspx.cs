using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion.Pages.Compras
{
    public partial class RegistrarCompra : System.Web.UI.Page
    {
        // Usamos la Sesión para mantener la lista de detalles de la compra entre postbacks.
        private List<DetalleCompra> ListaDetalleCompra
        {
            get => Session["DetalleCompra"] as List<DetalleCompra> ?? new List<DetalleCompra>();
            set => Session["DetalleCompra"] = value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("DetalleCompra");
                cargarDatos();
                ActualizarResumenCompra();
            }
        }

        private void cargarDatos()
        {
            ProveedorBLL proveedorBLL = new ProveedorBLL();
            ddlProveedor.DataSource = proveedorBLL.ObtenerProveedores();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

            InsumoBLL insumoBLL = new InsumoBLL();
            ddlInsumos.DataSource = insumoBLL.ObtenerInsumos();
            ddlInsumos.DataTextField = "Nombre";
            ddlInsumos.DataValueField = "Id";
            ddlInsumos.DataBind();
            ddlInsumos.Items.Insert(0, new ListItem("Seleccione un insumo", "0"));
        }

        protected void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Compras/Compras.aspx");
        }

        protected void btnAgregarInsumo_Click(object sender, EventArgs e)
        {
            if (ddlInsumos.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtCosto.Text))
            {
                ShowToast("Todos los campos para añadir un insumo son requeridos.", "warning");
                return;
            }

            int insumoId = Convert.ToInt32(ddlInsumos.SelectedValue);
            var listaActual = this.ListaDetalleCompra;

            //Usamos decimal.Parse con CultureInfo.InvariantCulture ---
            decimal cantidad = decimal.Parse(txtCantidad.Text, CultureInfo.InvariantCulture);
            decimal costo = decimal.Parse(txtCosto.Text, CultureInfo.InvariantCulture);

            var itemExistente = listaActual.FirstOrDefault(i => i.InsumoId == insumoId);
            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
                itemExistente.Costo = costo;
            }
            else
            {
                // Lógica para obtener el nombre del insumo para mostrarlo en el repeater
                InsumoBLL bll = new InsumoBLL();
                Insumo insumoInfo = bll.ObtenerInsumoPorId(insumoId);

                listaActual.Add(new DetalleCompra
                {
                    InsumoId = insumoId,
                    Cantidad = cantidad,
                    Costo = costo,
                    Insumo = insumoInfo // Guardas el objeto completo para acceder a su nombre
                });
            }

            this.ListaDetalleCompra = listaActual;
            ActualizarResumenCompra();

            txtCantidad.Text = "";
            txtCosto.Text = "";
            ddlInsumos.SelectedIndex = 0;
        }

        protected void rptDetalleCompra_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int insumoId = Convert.ToInt32(e.CommandArgument);
                var listaActual = this.ListaDetalleCompra;
                listaActual.RemoveAll(i => i.InsumoId == insumoId);
                this.ListaDetalleCompra = listaActual;
                ActualizarResumenCompra();
            }
        }

        private void ActualizarResumenCompra()
        {
            var listaActual = this.ListaDetalleCompra;
            rptDetalleCompra.DataSource = listaActual;
            rptDetalleCompra.DataBind();

            pnlNoItems.Visible = !listaActual.Any();

            decimal total = listaActual.Sum(i => i.Cantidad * i.Costo);
            lblTotalCompra.Text = $"Bs. {total:N2}";
        }

        protected void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            if (ddlProveedor.SelectedValue == "0" || !this.ListaDetalleCompra.Any())
            {
                ShowToast("Debe seleccionar un proveedor y añadir al menos un insumo.", "warning");
                return;
            }

            DataTable dtDetalles = new DataTable();
            dtDetalles.Columns.Add("InsumoId", typeof(int));
            dtDetalles.Columns.Add("Cantidad", typeof(decimal));
            dtDetalles.Columns.Add("Costo", typeof(decimal));

            foreach (var item in this.ListaDetalleCompra)
            {
                dtDetalles.Rows.Add(item.InsumoId, item.Cantidad, item.Costo);
            }

            int proveedorId = Convert.ToInt32(ddlProveedor.SelectedValue);
            string usuario = (string)Session["usuario"];
            UsuarioBLL usuarioBLL = new UsuarioBLL();
            int usuarioId = usuarioBLL.ObtenerRolIdNombre(usuario);

            Compra compra = new Compra
            {
                UsuarioId = usuarioId,
                ProveedorId = proveedorId,
                DetallesCompra = this.ListaDetalleCompra
            };
            ComprasBLL compraBLL = new ComprasBLL();
            try
            {
                bool exito = compraBLL.RegistrarCompra(compra);
                if (exito)
                {
                    ShowToast("Compra registrada exitosamente", "success");
                    Session.Remove("DetalleCompra");
                }
                else
                {
                    ShowToast("Error al registrar la compra", "error");
                }
            }
            catch (Exception ex)
            {
                ShowToast(ex.Message, "warning");
            }
        }

        private void ShowToast(string titulo, string icono)
        {
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
    }
}
