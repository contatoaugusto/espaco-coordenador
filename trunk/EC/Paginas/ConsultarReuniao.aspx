<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarReuniao.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarReuniao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 524px;
        }

        .auto-style2 {
            width: 122px;
        }
        .auto-style3 {
            width: 141px; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 1280px; height: 768px;">


        <h2 style="width: 100%; text-align: center; position: relative;">Dados da Reunião</h2>

        <table style="width: 90%";>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style2">Tipo de Reuniao:</td>
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlTipoReuniao" runat="server"></asp:DropDownList></td>
                <td></td>
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style2">Semestre:</td>
                    <td class="auto-style3">
                        <asp:DropDownList ID="ddlSemestre" runat="server">
                            <asp:ListItem Value="1">01</asp:ListItem>
                            <asp:ListItem Value="2">02</asp:ListItem>
                        </asp:DropDownList></td><td></td>
                </tr>
                <tr>
                    <td class="auto-style1"></td>
                    <td class="auto-style2">
                        <asp:Button ID="btConsultar" runat="server" Text="Consultar"  Width="65px" OnClick="btConsultar_Click" />
                       </td>
                       <td class="auto-style3"> <asp:Button ID="btNovo" runat="server" Text="Novo"  Width="65px" OnClick="btNovo_Click" /></td> 
                    <td></td> </tr>

        </table>
        <asp:GridView ID="grdReuniao" runat="server" AutoGenerateColumns="False" Style="margin-left: 132px; margin-top: 19px" Width="818px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="TITULO" HeaderText="Titulo" />
                <asp:BoundField DataField="DATAHORA" HeaderText="Data" />
                <asp:BoundField DataField="LOCAL" HeaderText="Local" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerSettings FirstPageImageUrl="~/Imagens/button-first-big-inactive.gif"
                LastPageImageUrl="~/Imagens/button-last.gif"
                NextPageImageUrl="~/Imagens/button-next-big-inactive.gif"
                PreviousPageImageUrl="~/Imagens/button-prev.gif" PageButtonCount="8" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </div>
</asp:Content>
