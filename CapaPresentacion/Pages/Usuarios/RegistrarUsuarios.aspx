<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuarios.aspx.cs" Inherits="CapaPresentacion.Pages.Usuarios.RegistrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="relative flex min-h-screen items-center justify-center bg-gray-100 p-4">

        <div class="absolute left-6 top-6">
            <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click"
                CssClass="bg-secondary rounded-lg px-5 py-2 font-bold text-white shadow-md transition-colors hover:bg-secondary/90" />
        </div>

        <div class="w-full max-w-md rounded-2xl bg-white p-8 shadow-xl">

            <div class="mb-8 flex flex-col items-center text-center">
                <div class="mb-4 flex h-20 w-20 items-center justify-center rounded-full bg-gray-200">
                    <img src="/wwwroot/images/usuario.png" alt="Ícono de Usuario" class="h-10 w-10 text-gray-500" />
                </div>
                <h1 class="text-2xl font-bold text-gray-800">Crear Nuevo Usuario</h1>
                <p class="mt-2 text-sm text-gray-500">Completa los datos para registrar un nuevo usuario en el sistema</p>
            </div>

            <div class="flex flex-col gap-6">

                <div>
                    <asp:Label runat="server" For="txtUsuario" CssClass="mb-1 block text-sm font-medium text-gray-700">Nombre de Usuario</asp:Label>
                    <div class="relative">
                        <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                            <img src="/wwwroot/images/icons8-usuario-48.png" alt="Ícono Arroba" class="h-5 w-5 text-gray-400" />
                        </div>
                        <asp:TextBox ID="txtUsuario" runat="server" placeholder="Ej: jperez"
                            CssClass="w-full rounded-lg border-gray-300 py-2.5 pl-10 shadow-sm focus:border-primary focus:ring-primary"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label runat="server" For="txtPassword" CssClass="mb-1 block text-sm font-medium text-gray-700">Contraseña</asp:Label>
                    <div class="relative">
                        <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                            <img src="/wwwroot/images/icons8-contraseña-48.png" alt="Ícono Candado" class="h-5 w-5 text-gray-400" />
                        </div>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Ingresa tu contraseña"
                            CssClass="w-full rounded-lg border-gray-300 py-2.5 pl-10 pr-10 shadow-sm focus:border-primary focus:ring-primary"></asp:TextBox>
                    </div>
                </div>

                <div>
                    <asp:Label runat="server" For="ddlRol" CssClass="mb-1 block text-sm font-medium text-gray-700">Rol del Usuario</asp:Label>
                    <div class="relative">
                        <div class="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                            <img src="/wwwroot/images/icons8-escudo-50.png" alt="Ícono Rol" class="h-5 w-5 text-gray-400" />
                        </div>
                        <asp:DropDownList ID="ddlRol" runat="server"
                            CssClass="w-full appearance-none rounded-lg border-gray-300 py-2.5 pl-10 shadow-sm focus:border-primary focus:ring-primary">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>

            <div class="mt-8">
                <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar Usuario" OnClick="btnGuardarUsuario_Click"
                    CssClass="bg-primary w-full rounded-lg p-3 text-lg font-bold text-white shadow-md transition-colors hover:bg-primary/90" />
            </div>
        </div>
    </div>
</asp:Content>
