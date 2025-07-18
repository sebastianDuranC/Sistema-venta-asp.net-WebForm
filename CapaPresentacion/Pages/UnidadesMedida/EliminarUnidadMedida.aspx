<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarUnidadMedida.aspx.cs" Inherits="CapaPresentacion.Pages.UnidadesMedida.EliminarUnidadMedida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="w-full px-4 py-12">
        <div class="mx-auto max-w-2xl rounded-xl border border-gray-200 bg-white shadow-lg">

            <%-- Cabecera del Formulario --%>
            <div class="flex items-center gap-1 border-b border-gray-200 p-6">
                <div>
                    <img src="/wwwroot/images/icons/icon-eliminar.png" alt="icon eliminar" class="mr-2 h-12 w-12 rounded-[50px] object-contain"/>
                </div>
                <div>
                    <h1 class="text-primary text-2xl font-bold">Eliminar Unidad de Medida</h1>
                    <p class="mt-1 text-sm text-gray-500">
                        Confirma los datos antes de eliminar el registro de forma permanente.
                    </p>
                </div>
            </div>

            <asp:Repeater runat="server" ID="rptUnidadMedida">
                <ItemTemplate>
                    <%-- Cuerpo del formulario con diseño de rejilla --%>
                    <div class="p-6">
                        <div class="grid grid-cols-1 gap-x-6 gap-y-8 md:grid-cols-2">
                            
                            <div>
                                <label class="mb-2 block text-sm font-medium text-gray-700">Id:</label>
                                <div class="block w-full rounded-md border-gray-200 bg-gray-50 px-3 py-2 text-gray-800 shadow-sm">
                                    <%# Eval("Id") %>
                                </div>
                            </div>
                            
                            <div>
                                <label class="mb-2 block text-sm font-medium text-gray-700">Nombre:</label>
                                <div class="block w-full rounded-md border-gray-200 bg-gray-50 px-3 py-2 text-gray-800 shadow-sm">
                                    <%# Eval("Nombre") %>
                                </div>
                            </div>
                            
                            <div>
                                <label class="mb-2 block text-sm font-medium text-gray-700">Abreviatura:</label>
                                <div class="block w-full rounded-md border-gray-200 bg-gray-50 px-3 py-2 text-gray-800 shadow-sm">
                                    <%# Eval("Abreviatura") %>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="flex items-center justify-end space-x-4 rounded-b-xl border-t border-gray-200 bg-gray-50 p-6">
                        
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                            CssClass="bg-secondary cursor-pointer rounded-lg px-5 py-2.5 text-center text-sm font-medium text-white shadow-sm transition-all
                                        duration-200 hover:brightness-90 focus:ring-secondary focus:outline-none focus:ring-2 focus:ring-offset-2" />
                        
                        <span class="bg-primary inline-flex cursor-pointer items-center rounded-md px-3 py-1.5 text-white shadow-sm transition-colors focus-within:ring-primary focus-within:ring-2 focus-within:ring-offset-2 hover:bg-primary/85">
                            <img src="/wwwroot/images/icons/boton-eliminar2.png" class="mr-0.5 h-4 w-4" alt="Eliminar" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"
                                OnClientClick="return confirmarEliminacion(this);"
                                CssClass="ml-2 cursor-pointer border-none bg-transparent p-0 py-1 text-sm font-semibold text-white shadow-none focus:outline-none" />
                        </span>

                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </main>

    <script>
        function confirmarEliminacion(button) {
            event.preventDefault();

            Swal.fire({
                title: '¿Estás realmente seguro?',
                text: "Una vez eliminado, no podrás recuperar este registro.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#111827', // Negro (color primario)
                cancelButtonColor: '#ef4444',  // Rojo
                confirmButtonText: 'Sí, ¡elimínalo!',
                cancelButtonText: 'No, cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    __doPostBack(button.name, '');
                }
            });

            return false;
        }
    </script>
</asp:Content>