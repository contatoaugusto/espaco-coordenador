<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAcao.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAcao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style7 {
            width: 270px;
        }

        .auto-style8 {
        width: 64px;
    }

        .auto-style9 {
            width: 327px;
        }

        .auto-style10 {
            width: 149px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 1280px; height: 768px;">

        <h2 style="text-align: center; width: 100%;">Consultar Ações</h2>


        <table style="width: 100%; margin: 5px;">

            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style8">Responsável:</td>
                <td class="auto-style10">
                    <asp:DropDownList ID="ddlPessoa" runat="server" Style="margin-left: 0px"></asp:DropDownList></td>
                <td class="auto-style7"></td>
            </tr>

            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style8">Status: </td>
                <td class="auto-style10">
                    <asp:DropDownList ID="ddlStatus" runat="server" Style="margin-left: 0px"></asp:DropDownList></td>
                <td class="auto-style7"></td>
            </tr>

            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style8">Prioridade: </td>
                <td class="auto-style10">
                    <asp:DropDownList ID="ddlPrioridade" runat="server"></asp:DropDownList></td>
                <td class="auto-style7"></td>
            </tr>
            <tr>
                <td class="auto-style9"></td>
                <td class="auto-style8">
                    <asp:Button ID="btnConsultar" runat="server" Text="Pesquisar" OnClick="btnConsultar_Click" Width="65px" />
                </td>
                <td class="auto-style10">
                    <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="65px" OnClick="btnNovo_Click" />
                </td>
                <td class="auto-style7"></td>
            </tr>
        </table>

        </table>

        <asp:GridView ID="grdAcao" runat="server" AutoGenerateColumns="False" Width="1000px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Style="margin-left: 127px; margin-top: 27px;" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID_ACAO" HeaderText="Nº"></asp:BoundField>
                <asp:BoundField DataField="TITULO" HeaderText="Titulo"></asp:BoundField>
                <asp:BoundField DataField="INICIO" HeaderText="Data" />
                <asp:BoundField DataField="ID_PESSOA" HeaderText="Responsável" />
                <asp:ButtonField Text="Excluir" />
                <asp:ButtonField Text="Alterar" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#F7F7DE" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    </div>

</asp:Content>
