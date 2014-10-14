<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarEvento.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarEvento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
        width: 107px;
    }

        .auto-style2 {
            width: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 1280px; height: 768px;">

        <h2 style="width: 100%; text-align: center; position: relative;">Consultar Eventos</h2>

        <table style="width: 90%; margin-left: 20%;">
            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style1">
                    <asp:Label ID="lbTipoEvento" runat="server" Text="Label">Tipo de Evento:</asp:Label>
                    &nbsp </td>
                <td>
                    <asp:DropDownList ID="ddlTipoEvento" runat="server"></asp:DropDownList></td>
            </tr>
            <br />

            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style1">Responsável: </td>
                <td>
                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <br />
            <tr>
                <td class="auto-style2"></td>
                <td class="auto-style1">
                    <asp:Button ID="btConsultar" runat="server" Text="Consultar" OnClick="btConsultar_Click" Width="65px" /></td>
                <td>
                    <asp:Button ID="btNovo" runat="server" Text="Novo" OnClick="btnovo_Click" Width="65px" /></td>
            </tr>
        </table>
        <div style="width: 90%; margin-left: 10%">
            <asp:GridView ID="grdEvento" runat="server" AllowCustomPaging="True" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" Style="margin-left: 145px; margin-top: 22px" Width="841px" BorderStyle="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="NOME" HeaderText="Nome">
                        <ControlStyle BackColor="#CCCCCC" BorderColor="Silver" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOCAL" HeaderText="Local">
                        <ControlStyle BackColor="#CCCCCC" />
                    </asp:BoundField>
                    <asp:BoundField DataField="INICIO" HeaderText="Inicio">
                        <ControlStyle BackColor="#CCCCCC" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
        </div>
        <div>
</asp:Content>
