<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CapaPresentacion.Pages.Productos.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-3xl font-bold text-primary">Lista de productos</h2>
            <asp:Button CssClass="mt-2 inline-block rounded bg-primary px-4 py-2 font-semibold text-white shadow transition-colors hover:cursor-pointer hover:bg-primary-dark" Text="Crear Productos" runat="server" ID="btnRegistrarProducto" OnClick="btnRegistrarProducto_Click" />
        </div>
    </div>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">
            <asp:Repeater runat="server" ID="rptProductos" OnItemCommand="rptProductos_ItemCommand">
                <HeaderTemplate>
                    <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow">
                        <thead>
                            <tr class="bg-primary text-white">
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
                <ItemTemplate>
                    <tr>
                        <td class="px-4 py-2 text-center font-semibold text-secondary"><%# Eval("Id") %></td>
                        <td class="px-4 py-2 text-center"><%# Eval("Nombre") %></td>
                        <td class="px-4 py-2 text-center"><%# Eval("Precio", "{0:C}") %></td>
                        <td class="px-4 py-2 text-center"><%# string.IsNullOrEmpty(Eval("Stock")?.ToString()) ? "0" : Eval("Stock") %></td>
                        <td class="px-4 py-2 text-center"><%# string.IsNullOrEmpty(Eval("StockMinimo")?.ToString()) ? "0" : Eval("StockMinimo") %></td>
                        <td class="px-4 py-2 text-center"><%# Eval("ProductoCategoria.Nombre") %></td>
                        <td class="px-4 py-2 text-center">
                            <div style="display: flex; justify-content: center;">
                                <asp:Image ImageUrl='<%# Eval("FotoUrl") %>' runat="server" Width="80px" Height="80px" CssClass="rounded shadow" />
                            </div>
                        </td>
                        <td class="px-4 py-2">
                            <div style="display: flex; justify-content: center; gap: 0.5rem;">
                                <asp:Button runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' CssClass="rounded bg-yellow-500 px-3 py-1 text-white transition-colors hover:bg-yellow-600 hover:cursor-pointer" />
                                <button type="button" onclick='confirmarEliminacionProducto("<%# Eval("Id") %>")' 
                                    class="rounded bg-secondary px-3 py-1 text-white transition-colors hover:bg-red-600 hover:cursor-pointer">
                                    Eliminar
                                </button>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>

    <script type="text/javascript">
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
