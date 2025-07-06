<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarProveedor.aspx.cs" Inherits="CapaPresentacion.Pages.Proveedores.EliminarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col items-center justify-center py-8">
        <div class="w-full max-w-lg rounded-lg bg-white p-6 shadow-lg">
            <h2 class="mb-4 text-center text-3xl font-extrabold text-gray-800">Eliminar Proveedor</h2>

            <asp:Repeater runat="server" ID="rptProveeodr">
                <ItemTemplate>
                    <div class="mb-6 border-b border-t border-gray-200 py-4">
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">#:</span>
                            <span class="text-gray-900"><%# Eval("Id") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nombre de proveedor:</span>
                            <span class="text-gray-900"><%# Eval("Nombre") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nombre de proveedor:</span>
                            <span class="text-gray-900"><%# Eval("Contacto") %></span>
                        </div>
                    </div>
                    <%-- Botones de acción --%>
                    <div class="flex justify-center space-x-5 pt-4">
                        
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                            CssClass="cursor-pointer rounded-lg bg-gray-500 px-6 py-2.5 font-medium text-white shadow-sm transition-all hover:bg-gray-600 focus:outline-none focus:ring-2 focus:ring-gray-400 focus:ring-offset-2" />

                        <%-- CAMBIO: Convertido a un asp:Button para usar el evento OnClick en C# --%>
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
            // El return false es importante para detener el postback inicial.
            // El postback solo ocurrirá si el usuario confirma en el diálogo.
            let postback = false;

            event.preventDefault(); // Detenemos el evento de clic por defecto.

            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción no se puede deshacer.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc2626', // Rojo
                cancelButtonColor: '#6b7280',  // Gris
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Si se confirma, llamamos a __doPostBack para ejecutar el OnClick del servidor.
                    __doPostBack(button.name, '');
                }
            });

            return postback;
        }
    </script>
</asp:Content>
