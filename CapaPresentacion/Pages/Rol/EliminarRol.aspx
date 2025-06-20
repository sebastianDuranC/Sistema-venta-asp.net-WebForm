<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarRol.aspx.cs" Inherits="CapaPresentacion.Pages.Rol.EliminarRol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col items-center justify-center py-8">
        <div class="w-full max-w-lg rounded-lg bg-white p-6 shadow-lg">
            <h2 class="mb-4 text-center text-3xl font-extrabold text-gray-800">Eliminar Rol</h2>

            <asp:Repeater runat="server" ID="rptRol">
                <ItemTemplate>
                    <div class="mb-6 border-b border-t border-gray-200 py-4">
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nº del Rol:</span>
                            <span class="text-gray-900"><%# Eval("Id") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nombre de Rol:</span>
                            <span class="text-gray-900"><%# Eval("Nombre") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nº de Permisos Asignados: </span>
                            <span class="text-gray-900"><%# Eval("RolesPermisos.Count") %></span>
                        </div>
                    </div>
                    <%-- Botones de acción --%>
                    <div class="flex justify-center space-x-5">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                            CssClass="bg-secondary cursor-pointer rounded-md px-6 py-2 text-white shadow-sm transition-colors hover:bg-secondary/75 focus:ring-secondary focus:outline-none focus:ring-2 focus:ring-offset-2" />
                        <button type="button" onclick='confirmarEliminacion("<%# Eval("Id") %>")'
                            class="rounded-md bg-primary px-6 py-2 text-white shadow-sm transition-colors hover:bg-primary/85 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2">
                            Eliminar</button>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script>
        // Función para confirmar la eliminación
        function confirmarEliminacion(Id) {
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
                    eliminar(Id);
                }
            });
        }

        // Función para realizar la eliminación
        function eliminar(Id) {
            $.ajax({
                type: "POST",
                url: "/Pages/Rol/EliminarRolHandler.ashx",
                data: { Id: Id },
                dataType: "json",
                success: function (respuesta) {
                    if (respuesta.exito) {
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: respuesta.mensaje,
                            icon: 'success',
                            timer: 2000
                        }).then(() => {
                            window.location.href = "Rol.aspx"
                        });
                    } else {
                        Swal.fire('Error', respuesta.mensaje, 'error');
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error en la llamada AJAX:', { xhr, status, error });
                    Swal.fire('Error', 'Error al eliminar: ' + error, 'error');
                }
            });
        }
    </script>
</asp:Content>
