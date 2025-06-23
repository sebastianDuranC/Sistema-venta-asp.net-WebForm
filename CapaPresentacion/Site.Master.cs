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
            var usuario = (string)Session["usuario"];
            var permisos = (List<string>)Session["permisos"];

            // 2. Validar que el usuario tenga permiso para ver la página ACTUAL
            ValidarAccesoPagina(permisos);

            if (!IsPostBack)
            {
                // 3. Poblar la información del usuario en el sidebar
                lblUsuarioNombre.Text = usuario;
                //lblUsuarioRol.Text = usuario.NombreRol; // Asumo que tienes NombreRol en tu objeto Usuario

                // 4. Construir el menú dinámicamente basado en permisos
                ConfigurarVisibilidadMenu(permisos);
            }
        }

        private void ValidarAccesoPagina(List<string> permisos)
        {
            // OBTENCIÓN DE RUTA SIMPLIFICADA Y DIRECTA
            // Request.AppRelativeCurrentExecutionFilePath devuelve la ruta exacta en formato "~/Folder/Page.aspx"
            string paginaActual = Request.AppRelativeCurrentExecutionFilePath + ".aspx";

            // COMPARACIÓN DIRECTA Y CLARA
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

            // Administración
            pnlSubMenuItemUsuarios.Visible = verificiarVisibilidad("~/Pages/Usuarios/Usuarios.aspx", permisos);
            pnlSubMenuItemRoles.Visible = verificiarVisibilidad("~/Pages/Rol/Rol.aspx", permisos);
            pnlSubMenuItemFormularios.Visible = verificiarVisibilidad("~/Pages/Permisos/Permisos.aspx", permisos);
            pnlSubMenuItemRolPermisos.Visible = verificiarVisibilidad("~/Pages/RolPermisos/RolPermisos.aspx", permisos);

            pnlModuleAcceso.Visible =
                pnlSubMenuItemUsuarios.Visible ||
                pnlSubMenuItemRoles.Visible ||
                pnlSubMenuItemFormularios.Visible ||
                pnlSubMenuItemRolPermisos.Visible;

            // Ventas
            pnlSubMenuItemGestionarVentas.Visible = verificiarVisibilidad("~/Pages/Ventas/Ventas.aspx", permisos);
            pnlSubMenuItemClientes.Visible = verificiarVisibilidad("~/Pages/Clientes/Clientes.aspx", permisos);
            pnlSubMenuItemProductos.Visible = verificiarVisibilidad("~/Pages/Productos/Productos.aspx", permisos);
            pnlSubMenuItemCategorias.Visible = verificiarVisibilidad("~/Pages/Productos/Categorias.aspx", permisos);

            pnlModuleVentas.Visible =
                pnlSubMenuItemGestionarVentas.Visible ||
                pnlSubMenuItemClientes.Visible ||
                pnlSubMenuItemProductos.Visible ||
                pnlSubMenuItemCategorias.Visible;

            // Compras
            pnlSubMenuItemGestionarCompras.Visible = verificiarVisibilidad("~/Pages/Compras/Compras.aspx", permisos);
            pnlSubMenuItemInsumos.Visible = verificiarVisibilidad("~/Pages/Insumos/Insumos.aspx", permisos);
            pnlSubMenuItemProveedores.Visible = verificiarVisibilidad("~/Pages/Proveedores/Proveedores.aspx", permisos);

            pnlModuleCompras.Visible =
                pnlSubMenuItemGestionarCompras.Visible ||
                pnlSubMenuItemInsumos.Visible ||
                pnlSubMenuItemProveedores.Visible;

            // Configuración
            pnlSubMenuItemNegocio.Visible = verificiarVisibilidad("~/Pages/Negocio/Negocio.aspx", permisos);
            pnlModuleConfiguracion.Visible = pnlSubMenuItemNegocio.Visible;
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
    }
}