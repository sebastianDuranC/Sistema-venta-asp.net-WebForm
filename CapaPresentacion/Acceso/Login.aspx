<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CapaPresentacion.Acceso.Login" %>

<!DOCTYPE html>
<html class="h-full bg-gray-100">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Login - Sistema de Venta</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css"/>
</head>
<body class="flex min-h-screen items-center justify-center">
    <form id="form1" runat="server" class="min-w-48 max-w-md">
        <div class="rounded-lg border border-gray-200 bg-white p-8 shadow-md">
            <div class="mb-10 text-center">
                <h2 class="text-3xl font-bold text-gray-800">Inicio de sesión</h2>
                <p class="mt-3 text-red-600">Ingresa tus credenciales para acceder</p>
            </div>

            <div class="space-y-8">
                <div>
                    <label class="mb-3 block text-gray-700">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" 
                        CssClass="w-full rounded-lg border border-gray-300 p-3 focus:outline-none focus:border-gray-500"
                        placeholder="Ingrese su usuario">
                    </asp:TextBox>
                </div>

                <div>
                    <label class="mb-3 block text-gray-700">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"
                        CssClass="w-full rounded-lg border border-gray-300 p-3 focus:outline-none focus:border-gray-500"
                        placeholder="Ingrese su contraseña">
                    </asp:TextBox>
                </div>

                <div class="mt-10">
                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar al sistema" 
                        CssClass="w-full rounded-lg bg-gray-800 p-3 font-medium text-white hover:bg-gray-700"
                        OnClick="btnIngresar_Click" />
                </div>

                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" 
                    CssClass="mt-4 rounded-lg border border-red-200 bg-red-50 p-3 text-red-600">
                    <asp:Label ID="lblMensaje" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
