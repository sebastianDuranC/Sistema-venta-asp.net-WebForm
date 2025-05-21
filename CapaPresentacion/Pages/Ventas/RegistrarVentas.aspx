<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarVentas.aspx.cs" Inherits="CapaPresentacion.Pages.Ventas.RegistrarVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mx-auto p-4">
        <div class="flex items-center justify-between">
            <h1 class="mb-4 text-2xl font-bold">Nueva Venta</h1>
            <asp:Button Text="Cancelar" runat="server" ID="Button1" class="rounded-lg bg-white p-2 text-primary" />
        </div>
        <div class="flex flex-col gap-4 lg:flex-row">
            <div class="w-full rounded bg-white p-4 shadow lg:w-2/3">
                <h2 class="mb-4 text-xl font-semibold">Productos</h2>
                <!-- Busqueda por filtro -->
                <div class="mb-4">
                    <input type="text" placeholder="Buscar productos..." class="w-full rounded-md border px-3 py-2">
                </div>
                <!-- Categorias -->
                <div class="mb-4 flex space-x-2">
                    <button class="rounded-md bg-blue-500 px-4 py-2 text-white">Todos</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Platos</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Bebidas</button>
                    <button class="rounded-md bg-gray-200 px-4 py-2">Complementos</button>
                </div>
                <!-- Productos -->
                <div class="grid grid-cols-2 gap-4 md:grid-cols-3 lg:grid-cols-4">
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="1/4 Pollo" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">1/4 Pollo</h3>
                        <p class="text-gray-600">Bs. 15.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="1/2 Pollo" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">1/2 Pollo</h3>
                        <p class="text-gray-600">Bs. 29.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="Pollo Entero" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">Pollo Entero</h3>
                        <p class="text-gray-600">Bs. 55.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="Alitas BBQ (6 unid)" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">Alitas BBQ (6 unid)</h3>
                        <p class="text-gray-600">Bs. 18.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="Arroz con Pollo" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">Arroz con Pollo</h3>
                        <p class="text-gray-600">Bs. 16.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="Gaseosa Personal" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">Gaseosa Personal</h3>
                        <p class="text-gray-600">Bs. 3.50</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="Gaseosa 1.5L" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">Gaseosa 1.5L</h3>
                        <p class="text-gray-600">Bs. 9.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                    <div class="rounded-lg border p-4 text-center">
                        <img src="#" alt="Papas Fritas" class="mx-auto h-32 rounded-lg bg-contentbg"/>
                        <h3 class="font-semibold">Papas Fritas</h3>
                        <p class="text-gray-600">Bs. 8.90</p>
                        <button class="mt-2 rounded-full border px-3 py-1">+</button>
                    </div>
                </div>
            </div>

            <!-- Resumen de Pedido -->
            <div class="w-full rounded bg-white p-4 shadow lg:w-1/3">
                <h2 class="mb-4 text-xl font-semibold">Resumen de Pedido</h2>
                <!-- Cliente -->
                <div class="mb-4">
                    <label for="cliente" class="block text-sm font-medium text-gray-700">Cliente</label>
                    <input type="text" id="cliente" class="mt-1 block w-full rounded-md border px-3 py-2" value="admin">
                </div>
                <!-- Productos -->
                <div class="mb-4 space-y-4">
                    <!-- Lista de productos -->
                    <div class="flex items-center justify-between border-b pb-2">
                        <div>
                            <p class="font-semibold">1/2 Pollo</p>
                            <p class="text-sm text-gray-600">Bs. 29.90 x 2</p>
                        </div>
                        <div class="flex items-center space-x-2">
                            <button class="rounded border px-2 py-1">-</button>
                            <span>2</span>
                            <button class="rounded border px-2 py-1">+</button>
                            <button class="text-red-500"><i class="fas fa-trash"></i></button>
                        </div>
                    </div>
                    <div class="flex items-center justify-between border-b pb-2">
                        <div>
                            <p class="font-semibold">Pollo Entero</p>
                            <p class="text-sm text-gray-600">Bs. 55.90 x 1</p>
                        </div>
                        <div class="flex items-center space-x-2">
                            <button class="rounded border px-2 py-1">-</button>
                            <span>1</span>
                            <button class="rounded border px-2 py-1">+</button>
                            <button class="text-red-500"><i class="fas fa-trash"></i></button>
                        </div>
                    </div>
                </div>
                <!-- Total -->
                <div class="mb-4 space-y-2">
                    <div class="flex justify-between">
                        <span>Subtotal</span>
                        <span>S/. 115.70</span>
                    </div>
                    <div class="flex justify-between font-bold">
                        <span>Total</span>
                        <span>S/. 136.53</span>
                    </div>
                </div>
                <div class="flex flex-col space-y-2">
                    <button class="rounded-md bg-primary py-2 text-white">Completar Venta</button>
                    <button class="rounded-md bg-contentbg py-2 text-gray-800">Imprimir Comanda</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
