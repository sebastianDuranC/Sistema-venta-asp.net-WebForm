<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarInsumo.aspx.cs" Inherits="CapaPresentacion.Pages.Insumos.EliminarInsumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col items-center justify-center py-8">
        <div class="w-full max-w-lg rounded-lg bg-white p-6 shadow-lg">
            <h2 class="mb-4 text-center text-3xl font-extrabold text-gray-800">Eliminar Insumo</h2>

            <asp:Repeater runat="server" ID="rptInsumo">
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
                            <span class="font-semibold text-gray-700">Costo:</span>
                            <span class="text-gray-900"><%# Eval("Costo") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Stock:</span>
                            <span class="text-gray-900"><%# Eval("Stock") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Stock minimo:</span>
                            <span class="text-gray-900"><%# Eval("StockMinimo") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Foto:</span>
                            <asp:Image runat="server" ImageUrl='<%# Eval("FotoUrl") %>' CssClass="w-12 h-12 object-cover"/>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Proveedor:</span>
                            <span class="text-gray-900"><%# Eval("Proveedor.Nombre") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Categoria:</span>
                            <span class="text-gray-900"><%# Eval("InsumoCategoria.Nombre") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Unidad de Medida:</span>
                            <span class="text-gray-900"><%# Eval("UnidadesMedida.Nombre") %></span>
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
