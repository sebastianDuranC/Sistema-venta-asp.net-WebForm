<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaPresentacion.Acceso.Login" %>

<!DOCTYPE html>
<html class="h-full bg-gray-50">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - Sistema de Venta</title>
    <link href="~/css/styles.css" rel="stylesheet" />
</head>
<body class="flex h-full items-center justify-center">
    <form id="form1" runat="server" class="w-full max-w-md">
        <div class="rounded-lg bg-white px-10 py-8 shadow-lg">
            <div class="mb-8 text-center">
                <h2 class="text-2xl font-semibold text-gray-800">Inicio de sesión</h2>
            </div>

            <div class="space-y-6">
                <div>
                    <label class="mb-1 block text-sm font-medium text-gray-700">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" 
                        CssClass="w-full rounded-lg border border-gray-300 px-3 py-2 shadow-sm focus:outline-none focus:border-blue-500 focus:ring-1 focus:ring-blue-500"
                        placeholder="Ingrese su usuario">
                    </asp:TextBox>
                </div>

                <div>
                    <label class="mb-1 block text-sm font-medium text-gray-700">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"
                        CssClass="w-full rounded-lg border border-gray-300 px-3 py-2 shadow-sm focus:outline-none focus:border-blue-500 focus:ring-1 focus:ring-blue-500"
                        placeholder="Ingrese su contraseña">
                    </asp:TextBox>
                </div>

                <div>
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar al sistema" 
                        CssClass="w-full rounded-lg bg-blue-600 px-4 py-2 text-white transition-colors hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                        OnClick="btnIngresar_Click" />
                </div>

                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" 
                    CssClass="rounded-lg border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-600">
                    <asp:Label ID="lblMensaje" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
