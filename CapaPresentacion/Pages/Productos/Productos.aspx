<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CapaPresentacion.Pages.Productos.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Encabezado de la página: Título y Botón de Crear --%>
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-3xl font-extrabold text-primary">Gestion de Productos</h2>
            <asp:Button
                ID="btnRegistrarProducto"
                runat="server"
                Text="Crear Producto"
                OnClick="btnRegistrarProducto_Click"
                CssClass="mt-2 inline-block rounded bg-primary px-4 py-2 font-semibold text-white shadow transition-colors hover:cursor-pointer hover:bg-primary/85 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2" />
        </div>
    </div>

    <%-- Contenedor principal de la tabla --%>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">

            <asp:Repeater runat="server" ID="rptProductos" OnItemCommand="rptProductos_ItemCommand">
                <%-- Cabecera de la tabla --%>
                <HeaderTemplate>
                    <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow" id="tbProductos" style="width: 100%">
                        <thead>
                            <tr class="bg-primary text-center text-white">
                                <th class="py-2">#</th>
                                <th class="py-2">Nombre</th>
                                <th class="py-2">Precio</th>
                                <th class="py-2">Stock</th>
                                <th class="py-2">Stock mínimo</th>
                                <th class="py-2">Categoría</th>
                                <th class="py-2">Foto</th>
                                <th class="py-2">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <%-- Cuerpo de la tabla (filas de datos) --%>
                <ItemTemplate>
                    <tr class="transition-colors hover:bg-gray-100">
                        <td class="px-4 py-2"><%# Eval("Id") %></td>
                        <td class="px-4 py-2"><%# Eval("Nombre") %></td>
                        <td class="px-4 py-2"><%# Eval("Precio", "{0:C}") %></td>
                        <td class="px-4 py-2"><%# string.IsNullOrEmpty(Eval("Stock")?.ToString()) ? "0" : Eval("Stock") %></td>
                        <td class="px-4 py-2"><%# string.IsNullOrEmpty(Eval("StockMinimo")?.ToString()) ? "0" : Eval("StockMinimo") %></td>
                        <td class="px-4 py-2"><%# Eval("ProductoCategoria.Nombre") %></td>
                        <td class="px-4 py-2">
                            <div style="display: flex; justify-content: center;">
                                <asp:Image ImageUrl='<%# Eval("FotoUrl") %>' runat="server" Width="80px" Height="80px" CssClass="rounded shadow" />
                            </div>
                        </td>

                        <td class="px-4 py-2">
                            <div style="display: flex; justify-content: center; gap: 0.5rem;">

                                <%-- Botón Ver --%>
                                <asp:Button runat="server" Text="Ver"
                                    CommandName="Ver" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="rounded bg-blue-500 px-3 py-1 text-white transition-colors hover:bg-blue-600 hover:cursor-pointer" />

                                <%-- Botón Editar --%>
                                <asp:Button runat="server" Text="Editar"
                                    CommandName="Editar" CommandArgument='<%# Eval("Id") %>'
                                    CssClass="rounded bg-yellow-500 px-3 py-1 text-white transition-colors hover:bg-yellow-600 hover:cursor-pointer" />

                                <%-- Botón Eliminar --%>
                                <asp:Button runat="server" Text="Eliminar"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                                    OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este registro?');"
                                    CssClass="rounded bg-red-500 px-3 py-1 text-white transition-colors hover:bg-red-600 hover:cursor-pointer" />

                            </div>
                        </td>
                    </tr>
                </ItemTemplate>

                <%-- Pie de la tabla --%>
                <FooterTemplate>
                    </tbody>
                    </table>
               
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>
    <script type="text/javascript">
        // Función para inicializar DataTables en nuestra tabla
        $(document).ready(function () {
            $('#tbProductos').DataTable({

                // 1. Opciones de visualización y cantidad de registros
                "pageLength": 5, // Inicia mostrando 5 registros por página
                "lengthMenu": [
                    [5, 10], // Valores internos de DataTables (-1 es 'Todos')
                    ['5 registros', '10 registros'] // Texto que ve el usuario
                ],

                // 2. Traducción al español para todos los elementos de la tabla
                "language": {
                    "url": "/Scripts/DataTables/es-ES.json", // Asegúrate que esta ruta es correcta
                    // Opcional: puedes definir textos específicos aquí si el JSON no los tiene
                    "lengthMenu": "Mostrar _MENU_", // Personaliza el texto del menú de longitud
                    "search": "Buscar:", // Cambia el texto del label de búsqueda
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ registros"
                },

                // 3. Diseño y orden de los controles (La clave para el diseño que buscas)
                //    l -> lengthMenu (El selector de "Mostrar X registros")
                //    f -> filtering (El campo de "Buscar")
                //    t -> table (La tabla en sí)
                //    i -> info (El texto "Mostrando X a Y de Z registros")
                //    p -> pagination (Los botones de paginación)
                "dom": '<"flex justify-between items-center mb-4"lf>t<"flex justify-between items-center mt-4"ip>',

                // 4. Mantenemos el diseño responsivo
                "responsive": true
            });
        });

        // Función para confirmar la eliminación del producto
        function confirmarEliminacionProducto(productoId) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción no se puede deshacer",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar',
                allowOutsideClick: false,
                allowEscapeKey: false,
                backdrop: true
            }).then((resultado) => {
                if (resultado.isConfirmed) {
                    eliminarProducto(productoId);
                }
            });
        }

        // Función para realizar la eliminación del producto
        function eliminarProducto(productoId) {
            $.ajax({
                type: "POST",
                url: "/Pages/Productos/EliminarProducto.ashx",
                data: { Id: productoId },
                dataType: "json",
                success: function (respuesta) {
                    if (respuesta.exito) {
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: respuesta.mensaje,
                            icon: 'success',
                            timer: 2000
                        }).then(() => {
                            // Recargar la página para actualizar la lista
                            window.location.reload();
                        });
                    } else {
                        Swal.fire('Error', respuesta.mensaje, 'error');
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error en la llamada AJAX:', { xhr, status, error });
                    Swal.fire('Error', 'Error al eliminar el producto: ' + error, 'error');
                }
            });
        }
    </script>
</asp:Content>
