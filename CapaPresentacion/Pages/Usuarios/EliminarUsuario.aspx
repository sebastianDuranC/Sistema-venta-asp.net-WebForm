<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EliminarUsuario.aspx.cs" Inherits="CapaPresentacion.Pages.Usuarios.EliminarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flex w-full flex-col items-center justify-center py-8">
        <div class="w-full max-w-lg rounded-lg bg-white p-6 shadow-lg">
            <h2 class="mb-4 text-center text-3xl font-extrabold text-gray-800">Eliminar usuario</h2>

            <asp:Repeater runat="server" ID="rptUsuario">
                <ItemTemplate>
                    <div class="mb-6 border-b border-t border-gray-200 py-4">
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nº registro</span>
                            <span class="text-gray-900"><%# Eval("Id") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Nombre</span>
                            <span class="text-gray-900"><%# Eval("Nombre") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Rol</span>
                            <span class="text-gray-900"><%# Eval("NombreRol") %></span>
                        </div>
                        <div class="flex justify-between py-2">
                            <span class="font-semibold text-gray-700">Trabaja</span>
                            <span class="text-gray-900"><%# Eval("NombreNegocio") %></span>
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
        // Función para mostrar la confirmación con SweetAlert
        function confirmarEliminacion(usuarioId) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "Esta acción no se puede deshacer y eliminará permanentemente al usuario.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sí, ¡eliminar!',
                cancelButtonText: 'Cancelar',
                backdrop: true,
                allowOutsideClick: false
            }).then((resultado) => {
                if (resultado.isConfirmed) {
                    // Si el usuario confirma, llamamos a la función que ejecuta la eliminación
                    eliminar(usuarioId);
                }
            });
        }

        // Función para realizar la eliminación mediante AJAX llamando al handler .ashx
        function eliminar(usuarioId) {
            $.ajax({
                type: "POST",
                // ¡IMPORTANTE! Asegúrate de que esta URL sea correcta según la ubicación de tu archivo .ashx
                url: "/Pages/Usuarios/EliminarUsuarioHandler.ashx",
                // Enviamos los datos. La clave "UsuarioId" debe coincidir con la que leemos en el handler.
                data: { Id: usuarioId },
                dataType: "json",
                success: function (respuesta) {
                    // La respuesta del handler .ashx es un objeto JSON directo
                    if (respuesta.exito) {
                        Swal.fire({
                            title: '¡Eliminado!',
                            text: respuesta.mensaje,
                            icon: 'success',
                            timer: 2000,
                            timerProgressBar: true
                        }).then(() => {
                            // Redirigir a la página de lista de usuarios
                            window.location.href = "Usuarios.aspx";
                        });
                    } else {
                        Swal.fire('Error', respuesta.mensaje, 'error');
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error en la llamada AJAX:', { xhr, status, error });
                    Swal.fire('Error Inesperado', 'No se pudo comunicar con el servidor: ' + error, 'error');
                }
            });
        }
    </script>
</asp:Content>
