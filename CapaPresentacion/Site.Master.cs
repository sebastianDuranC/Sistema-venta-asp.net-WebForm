using CapaEntidades;
using CapaNegocio;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaPresentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. Validar la sesión del usuario al cargar CUALQUIER página
            if (Session["usuario"] == null || Session["permisos"] == null)
            {
                // Si no hay sesión, y no estamos ya en la página de login, redirigir.
                if (Request.AppRelativeCurrentExecutionFilePath != "~/Acceso/Login.aspx")
                {
                    Response.Redirect("~/Acceso/Login.aspx");
                }
                return; // Detener la ejecución para páginas no autenticadas
            }

            // Si hay sesión, obtenemos los datos
            var permisos = (List<string>)Session["permisos"];

            //Validar que el usuario tenga permiso para ver la página ACTUAL
            ValidarAccesoPagina(permisos);

            if (!IsPostBack)
            {
                cargarUsarioRol();

                //Construir el menú dinámicamente basado en permisos
                ConfigurarVisibilidadMenu(permisos);
                VerificarAlertasDeStock();
            }
        }

        private void cargarUsarioRol()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            lblNombreUsuario.Text = usuario.Nombre;

            UsuarioBLL usuarioBLL = new UsuarioBLL();
            int rolId = usuarioBLL.ObtenerRolIdNombre(usuario.Nombre);
            RolBLL rol = new RolBLL();
            Rol rolLogeado = rol.ObtenerRolPorId(rolId);
            lblRol.Text = rolLogeado.Nombre;
        }

        private void ValidarAccesoPagina(List<string> permisos)
        {
            // Request.AppRelativeCurrentExecutionFilePath devuelve la ruta exacta en formato "~/Folder/Page.aspx"
            string paginaActual = Request.AppRelativeCurrentExecutionFilePath + ".aspx";

            // Compara la ruta actual con la lista de permisos, ignorando mayúsculas/minúsculas.
            bool tienePermiso = permisos.Any(p => p.Equals(paginaActual, StringComparison.InvariantCultureIgnoreCase));

            // CASOS ESPECIALES: Páginas que no requieren validación de permisos si ya estás logueado.
            if (paginaActual.Equals("~/Default.aspx", StringComparison.InvariantCultureIgnoreCase) ||
                paginaActual.Equals("~/Acceso/AccesoDenegado.aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                tienePermiso = true;
            }

            // REDIRECCIÓN: Si no tiene permiso, se redirige a la página de Acceso Denegado.
            if (!tienePermiso)
            {
                Response.Redirect("~/Acceso/AccesoDenegado.aspx");
            }
        }

        private void ConfigurarVisibilidadMenu(List<string> permisos)
        {
            // Dashboard
            pnlMenuDashboard.Visible = verificiarVisibilidad("~/Default.aspx", permisos);

            // Acceso y Administración
            pnlSubMenuItemUsuarios.Visible = verificiarVisibilidad("~/Pages/Usuarios/Usuarios.aspx", permisos);
            pnlSubMenuItemRoles.Visible = verificiarVisibilidad("~/Pages/Rol/Rol.aspx", permisos);
            pnlSubMenuItemFormularios.Visible = verificiarVisibilidad("~/Pages/Permisos/Permisos.aspx", permisos);
            //pnlSubMenuItemRolPermisos.Visible = verificiarVisibilidad("~/Pages/RolPermisos/RolPermisos.aspx", permisos);
            pnlSubMenuItemNegocio.Visible = verificiarVisibilidad("~/Pages/Negocio/Negocio.aspx", permisos);

            pnlModuleAcceso.Visible =
                pnlSubMenuItemUsuarios.Visible ||
                pnlSubMenuItemRoles.Visible ||
                pnlSubMenuItemFormularios.Visible ||
                pnlSubMenuItemNegocio.Visible;

            // Ventas
            pnlSubMenuItemGestionarVentas.Visible = verificiarVisibilidad("~/Pages/Ventas/Ventas.aspx", permisos);
            pnlSubMenuItemClientes.Visible = verificiarVisibilidad("~/Pages/Clientes/Clientes.aspx", permisos);
            pnlSubMenuItemProductos.Visible = verificiarVisibilidad("~/Pages/Productos/Productos.aspx", permisos);
            pnlSubMenuItemCategorias.Visible = verificiarVisibilidad("~/Pages/ProductoCategoria/ProductoCategorias.aspx", permisos);
            pnlSubMenuItemMetodoPago.Visible = verificiarVisibilidad("~/Pages/MetodosPago/MetodosPago.aspx", permisos);

            pnlModuleVentas.Visible =
                pnlSubMenuItemGestionarVentas.Visible ||
                pnlSubMenuItemClientes.Visible ||
                pnlSubMenuItemProductos.Visible ||
                pnlSubMenuItemCategorias.Visible;

            // Compras
            pnlSubMenuItemGestionarCompras.Visible = verificiarVisibilidad("~/Pages/Compras/Compras.aspx", permisos);
            pnlSubMenuItemInsumos.Visible = verificiarVisibilidad("~/Pages/Insumos/Insumos.aspx", permisos);
            pnlSubMenuItemProveedores.Visible = verificiarVisibilidad("~/Pages/Proveedores/Proveedores.aspx", permisos);
            pnlSubMenuItemUnidadMedida.Visible = verificiarVisibilidad("~/Pages/UnidadesMedida/UnidadesMedida.aspx", permisos);
            pnlSubMenuItemInsumoCategoria.Visible = verificiarVisibilidad("~/Pages/InsumoCategoria/InsumoCategorias.aspx", permisos);
            pnlSubMenuItemMovimientoInventario.Visible = verificiarVisibilidad("~/Pages/MovimientoInventario/MovimientosInventario.aspx", permisos);

            pnlModuleCompras.Visible =
                pnlSubMenuItemGestionarCompras.Visible ||
                pnlSubMenuItemInsumos.Visible ||
                pnlSubMenuItemProveedores.Visible ||
                pnlSubMenuItemUnidadMedida.Visible ||
                pnlSubMenuItemInsumoCategoria.Visible ||
                pnlSubMenuItemMovimientoInventario.Visible;

            //// Configuración
            //pnlSubMenuItemBaseDatos.Visible = verificiarVisibilidad("~/Pages/BD/Respaldo.aspx", permisos);

            //pnlModuleConfiguracion.Visible = pnlSubMenuItemBaseDatos.Visible;
        }

        private bool verificiarVisibilidad(string ruta, List<string> permisos)
        {
            return permisos.Any(p => p.Equals(ruta, StringComparison.InvariantCultureIgnoreCase));
        }

        protected void cerrarSesion_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Acceso/Login.aspx");
            }
        }

        protected void imgAlertbtn_Click(object sender, ImageClickEventArgs e)
        {
            // Alterna la visibilidad del panel de alertas
            pnlAlerts.Visible = !pnlAlerts.Visible;

            // Si el panel se hace visible, carga el contenido
            if (pnlAlerts.Visible)
            {
                CargarContenidoAlertas();
            }
        }

        private void VerificarAlertasDeStock()
        {
            // --- AQUÍ VA LA LÓGICA DEL SP ---
            // Llama a tu método de negocio que ejecuta el Stored Procedure
            // Por ejemplo, asumamos que tienes un método que devuelve una lista o un DataTable
            // CN_Producto cnProducto = new CN_Producto();
            // DataTable dtAlertas = cnProducto.ObtenerAlertasStock();

            // Simulación: vamos a asumir que tienes 2 alertas
            int numeroDeAlertas = 2; // Reemplaza esto con la cuenta real: dtAlertas.Rows.Count;

            if (numeroDeAlertas > 0)
            {
                // Si hay alertas, muestra el punto rojo
                pnlRedDot.Visible = true;
            }
            else
            {
                pnlRedDot.Visible = false;
            }
        }

        private void CargarContenidoAlertas()
        {
            int numeroDeAlertas = 2; // Reemplaza esto con la cuenta real: dtAlertas.Rows.Count;

            if (numeroDeAlertas > 0)
            {
                // Si hay alertas, muestra el punto rojo
                pnlRedDot.Visible = true;
                lblContenidoAlertas.Text = "Coca Cola 2l quedan solo 2 unidades<br/>";
                lblContenidoAlertas.Text += "Gabriel se llevo 2 platos";
            }
            else
            {
                pnlRedDot.Visible = false;
            }
        }
    }
}