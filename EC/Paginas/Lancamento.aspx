<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Lancamento.aspx.cs" Inherits="UI.Web.EC.Paginas.Lancamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 114px;
        }
        .auto-style2 {
            width: 506px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="width: 1280px; height: 768px;">

        <h2 style="width: 100%; text-align: center; position: relative;">Dados do Lançamento</h2>


        <table style="width: 90%;">
            <tr>
                <td></td>
                <td class="auto-style1">Tipo Lancamento:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlTipolancamento" runat="server"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">Professor</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">Turma:</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlTurma" runat="server"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
           
            <tr>
                <td></td>

           <td class="auto-style1">Data</td>
            <td class="auto-style2">
                <asp:DropDownList ID="ddlDia" runat="server"></asp:DropDownList> &nbsp&nbsp&nbsp
                <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList> &nbsp&nbsp&nbsp 
                 <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList><td></td>
 </tr> 
                <tr>
                    <td></td>
                    <td class="auto-style1">Justificativa</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtJustificativa" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="auto-style1">Providências</td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtProvidencias" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td></td>
                    <td class="auto-style1">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="Button1_Click" Width="65px" /></td>
                       <td> <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px"  />
                    </td>
                    <td></td>
                </tr>
        </table>

        <asp:GridView ID="grdLancamento" runat="server"  BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" Style="margin-left: 15px; margin-top: 32px;" Width="70%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID_TIPOLANCAMENTO" HeaderText="Tipo" />
                <asp:TemplateField HeaderText="Turma">
                   
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID_TURMA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DATA_LANCAMENTO" HeaderText="Data" />
                <asp:TemplateField HeaderText="Professor">
                    
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ID_PESSOA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
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
