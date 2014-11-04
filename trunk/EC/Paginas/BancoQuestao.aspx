<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/BancoQuestao.aspx.cs" Inherits="UI.Web.EC.Paginas.BancoQuestao" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Consultar Questões</h3>

    <div class="form-in">
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

            
        </table>
    </div>
    <div class="form-bottombuttons">
        <asp:Button ID="btnConsultar" runat="server" Text="Pesquisar" OnClick="btnConsultar_Click" Width="65px" />
        <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="65px" OnClick="btnNovo_Click" />
     </div>

    <div class="form-separator"></div>

    <h3>Questões</h3>
    <sgi:GridView ID="grdQuestao" runat="server"  AutoGenerateColumns="False" OnRowCommand="grdQuestao_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="DESCRICAO" HeaderText="Questão" HeaderStyle-Width="60%"/>
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
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_QUESTAO")%>' CommandName="Editar" Text="Editar" CausesValidation="false" />
                    |
                    <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_QUESTAO")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        
    </sgi:GridView>
</asp:Content>
