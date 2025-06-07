<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RolPermisos.aspx.cs" Inherits="CapaPresentacion.Pages.rolpermisosmapping.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mx-auto max-w-5xl py-8">
        <h2 class="mb-6 text-3xl font-bold text-primary">Gestión de Permisos por Rol</h2>
        <div class="max-h-[500px] space-y-6 overflow-y-auto rounded-xl bg-gray-100 p-4 shadow">
            <asp:Repeater runat="server" ID="rptRoles" OnItemDataBound="rptRoles_ItemDataBound">
                <ItemTemplate>
                    <div class="flex flex-col rounded-lg bg-white shadow">
                        <div class="rounded-t-lg bg-secondary px-6 py-3 text-white">
                            <h4 class="m-0 text-lg font-semibold"><%# Eval("Nombre") %></h4>
                        </div>
                        <div class="flex flex-col gap-2 px-6 py-4">
                            <asp:Repeater runat="server" ID="rptForm">
                                <ItemTemplate>
                                    <div class="flex items-center rounded-md bg-gray-50 px-3 py-2">
                                        <asp:CheckBox runat="server" ID="checkFormPermisos" CssClass="mr-2 accent-blue-600" />
                                        <asp:Label ID="Label1" runat="server" CssClass="mr-2 font-medium text-gray-700"> <%# DataBinder.Eval(Container.DataItem, "FormNombre") %></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdnFormId" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <span class="text-xs text-gray-400"><%# DataBinder.Eval(Container.DataItem, "FormRuta") %> </span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="flex justify-end px-6 py-2">
                            <asp:Label runat="server" ID="lblRolId" Visible="true" Text=' <%# Eval("Id") %> '> </asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="mt-6 flex items-center gap-4">
            <asp:Button runat="server" CssClass="cursor-pointer rounded-lg bg-primary px-8 py-3 font-semibold text-white shadow transition hover:bg-gray-700" ID="btnRolPermisos" Text="Guardar Permisos" OnClick="btnRolPermisos_Click" />
            <asp:Label runat="server" CssClass="font-semibold text-green-600" ID="lblMensaje"></asp:Label>
        </div>
    </div>
</asp:Content>
