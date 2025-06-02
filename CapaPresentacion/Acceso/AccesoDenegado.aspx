<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="CapaPresentacion.Acceso.AccesoDenegado" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Acceso Denegado</title>
    <link href="~/wwwroot/css/tailwind.css" rel="stylesheet" type="text/css" />
    <style>
        body {
            background-color: #fcebeb;
            background-image: linear-gradient(to top, #fcebeb 0%, #fefefe 100%);
        }
    </style>
</head>
<body class="flex min-h-screen items-center justify-center">
    <form id="form1" runat="server" class="w-full max-w-md">
        <div class="mx-auto rounded-2xl border border-gray-100 bg-white p-8 text-center shadow-xl">
            <div class="mx-auto mb-6 flex h-32 w-32 flex-col items-center justify-center rounded-full bg-red-100">
                <img src="/wwwroot/images/icon-accesodenegado.png" alt="Acceso Denegado" class="mb-2 h-36 w-36" />
            </div>
            <div class="flex justify-center">
                <span class="block w-16 border-b-4 border-red-300"></span>
            </div>
            <h1 class="mb-3 text-3xl font-extrabold text-gray-800">Acceso Denegado</h1>
            <p class="mb-8 px-4 leading-relaxed text-gray-600">
                No tienes permisos para acceder a esta sección del sistema de venta. Contacta al administrador si necesitas acceso.
            </p>

            <div class="mb-8 flex flex-col items-center rounded-lg border border-orange-200 bg-orange-50 px-6 py-4 text-left text-orange-800">
                <div class="mb-2 flex w-full items-center">
                    <img src="#" alt="Icono" />
                    <span class="text-lg font-semibold">Sistema de venta - El Fogón</span>
                </div>
                <p class="w-full text-sm">Código de error: <span class="font-bold">403</span></p>
            </div>

            <asp:Button ID="btnRegresar" runat="server" Text="Regresar"
                CssClass="inline-flex w-full items-center justify-center rounded-lg border border-transparent bg-orange-600 px-6 py-3 text-base font-semibold text-white shadow-sm transition-colors duration-200 hover:bg-orange-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-orange-500"
                OnClick="btnRegresar_Click"></asp:Button>
        </div>
    </form>
</body>
</html>
