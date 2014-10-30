<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/BancoQuestao.aspx.cs" Inherits="UI.Web.EC.Paginas.BancoQuestao" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Consultar Questões</h3>

    <div style="width: 1280px; height: 768px;">
        <table style="width: 100%; margin: 5px; height: 94px;">

            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style6">AMC:</td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList></td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style6">Disciplina:</td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlDisciplina" runat="server"></asp:DropDownList>
                    <td class="auto-style8"></td>
                </td>
            </tr>
            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style6">Professor</td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlFuncionario" runat="server"></asp:DropDownList>
                    <td class="auto-style8"></td>
                </td>
            </tr>

            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style6">
                    <asp:Button ID="btnConsultar" runat="server" Text="Pesquisar" OnClick="btnConsultar_Click" Width="65px" />
                </td>
                <td class="auto-style9">
                    <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="65px" OnClick="btnNovo_Click" />
                </td>
            </tr>
        </table>

        <div style="width: 100%; height: 151px;">
            <asp:GridView ID="grdQuestao" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" CellPadding="1" ForeColor="Black" GridLines="Vertical" Style="margin-top: 10px; margin-left: 5%;" Width="80%" AutoGenerateColumns="False" Height="89px">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="DESCRICAO" HeaderText="Pergunta" />
                    <asp:TemplateField HeaderText="Funcionario">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("FUNCIONARIO.PESSOA.NOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disciplina">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("DISCIPLINA.DESCRICAO") %>'></asp:Label>
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
            <br />
        </div>
    </div>
</asp:Content>
