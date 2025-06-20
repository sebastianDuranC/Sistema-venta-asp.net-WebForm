<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Negocio.aspx.cs" Inherits="CapaPresentacion.Pages.Negocio.Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Encabezado de la página: Título y Botón de Crear --%>
    <div class="flex w-full flex-col justify-between py-4">
        <div class="mb-4 flex items-center justify-between">
            <h2 class="text-primary text-3xl font-extrabold">Gestion de Negocio</h2>
        </div>
    </div>

    <%-- Contenedor principal de la tabla --%>
    <div class="flex rounded-lg bg-white p-4">
        <div class="flex w-full flex-col justify-between">
            
            <asp:Repeater runat="server" ID="rptNegocio" OnItemCommand="rptNegocio_ItemCommand">
                <%-- Cabecera de la tabla --%>
                <HeaderTemplate>
                    <table class="w-full table-auto border-collapse overflow-hidden rounded-lg shadow" id="tablaGenerica" style="width: 100%">
                        <thead>
                            <tr class="bg-primary text-center text-white">
                                <th class="py-2">Foto</th>
                                <th class="py-2">Nombre</th>
                                <th class="py-2">Direccion</th>
                                <th class="py-2">Accion</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>

                <%-- Cuerpo de la tabla (filas de datos) --%>
                <ItemTemplate>
                    <tr class="text-center transition-colors hover:bg-gray-100">
                        <td class="m-auto h-32 w-32 object-contain" >
                            <asp:Image ImageUrl='<%# Eval("LogoUrl") %>' runat="server" />
                        </td>
                        <td class="px-4 py-2"><%# Eval("Nombre") %></td>
                        <td class="px-4 py-2"><%# Eval("Direccion") %></td>
                        
                        <td class="px-4 py-2">
                            <div style="display: flex; justify-content: center; gap: 0.5rem;">
                                
                                <asp:Button runat="server" Text="Editar" 
                                    CommandName="Editar" CommandArgument='<%# Eval("Id") %>' 
                                    CssClass="rounded bg-yellow-500 px-3 py-1 text-white transition-colors hover:bg-yellow-600 hover:cursor-pointer" />
                               
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>

                <%-- Pie de la tabla --%>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>
</asp:Content>
