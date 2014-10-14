<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarLancamento.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarLancamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 270px;
        }

        .auto-style2 {
            width: 110px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 1280px; height: 768px;">

        <h2 style="width: 100%; text-align: center; position: relative;">Consultar Lançamentos</h2>

        <table style="width: 90%; margin-left: 20%;">

            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style2">Tipo Lancamento:</td>
                <td>
                    <asp:DropDownList ID="ddlTipolancamento" runat="server"></asp:DropDownList></td>
            </tr>

            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style2">Professor</td>
                <td>
                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList></td>
            </tr>

            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style2">Turma:</td>
                <td>
                    <asp:DropDownList ID="ddlTurma" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="auto-style1"></td>
                <td class="auto-style2">
                    <asp:Button ID="btConsultar" runat="server" Text="Consultar" OnClick="btConsultar_Click" Width="65px" />
                    <td>
                        <asp:Button ID="btNovo" runat="server" Text="Novo" OnClick="btnovo_Click" Width="65px" /></td>
            </tr>
        </table>

        <asp:GridView ID="grdLancamento" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" Style="margin-left: 160px; margin-top: 32px;" Width="734px">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID_TIPOLANCAMENTO" HeaderText="Tipo" />
                <asp:BoundField DataField="ID_TURMA" HeaderText="Turma" />
                <asp:BoundField DataField="DATA_LANCAMENTO" HeaderText="Data" />
                <asp:BoundField DataField="ID_PESSOA" HeaderText="Professor" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
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
