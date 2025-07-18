<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarProducto.aspx.cs" Inherits="CapaPresentacion.Pages.Productos.EliminarProducto1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col items-center justify-center py-8">
    <div class="w-full max-w-lg rounded-lg bg-white p-6 shadow-lg">
        <h2 class="mb-4 text-center text-3xl font-extrabold text-gray-800">Eliminar Producto</h2>

        <asp:Repeater runat="server" ID="rptProducto">
            <ItemTemplate>
                <div class="mb-6 border-b border-t border-gray-200 py-4">
                    <div class="flex justify-between py-2">
                        <span class="font-semibold text-gray-700">#:</span>
                        <span class="text-gray-900"><%# Eval("Id") %></span>
                    </div>
                    <div class="flex justify-between py-2">
                        <span class="font-semibold text-gray-700">Nombre:</span>
                        <span class="text-gray-900"><%# Eval("Nombre") %></span>
                    </div>
                    <div class="flex justify-between py-2">
                        <span class="font-semibold text-gray-700">Abreviatura:</span>
                        <span class="text-gray-900"><%# Eval("Precio") %></span>
                    </div>
                </div>
                <%-- Botones de acción --%>
                <div class="flex justify-center space-x-5 pt-4">
                    
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                        CssClass="cursor-pointer rounded-lg bg-primary px-6 py-2.5 font-medium text-white shadow-sm transition-all hover:bg-primary/65 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2" />

                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"
                        OnClientClick="return confirmarEliminacion(this);" 
                        CssClass="cursor-pointer rounded-lg bg-secondary px-6 py-2.5 font-medium text-white shadow-sm transition-all hover:brightness-90 focus:outline-none focus:ring-2 focus:ring-secondary focus:ring-offset-2" />

                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<script>
        function confirmarEliminacion(button) {
            let postback = false;
            event.preventDefault();

            Swal.fire({
                title: '¿Estás realmente seguro?',
                text: "Una vez eliminado, no podrás recuperar este registro.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#111827', // Negro
                cancelButtonColor: '#ef4444',  // Rojo
                confirmButtonText: 'Sí, ¡elimínalo!',
                cancelButtonText: 'No, cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack(button.name, '');
                }
            });

            return postback;
        }
    </script>
</asp:Content>
