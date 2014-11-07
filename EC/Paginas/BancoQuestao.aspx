<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/BancoQuestao.aspx.cs" Inherits="UI.Web.EC.Paginas.BancoQuestao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../EC.UI.WebControls/MessageBox/Styles/Outlook.css" rel="stylesheet" />
    <link href="../EC.UI.WebControls/MessageBox/Styles/UniCEUB.css" rel="stylesheet" />

    <script src="../Includes/ea.modal.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closeModalpopup").click(function () {
                document.location = "../default.aspx";
            });
        });
    </script>

    <h3>Consultar Questão</h3>


    <div class="form-in">
        <div class="row">
            <div class="column w100">
                AMC:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Disciplina:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlDisciplina" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Professor
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlFuncionario" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="form-bottombuttons">
            <asp:Button ID="btnConsultar" runat="server" Text="Pesquisar" OnClick="btnConsultar_Click" Width="65px" />
            <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="65px" OnClick="btnNovo_Click" />
        </div>

        <div class="form-separator"></div>

        <h3>Questões</h3>
        <sgi:GridView 
            ID="grdQuestao" 
            runat="server" 
            AutoGenerateColumns="False" 
            OnRowCommand="grdQuestao_RowCommand"
            AllowPaging="true" PageSize="10"
            OnPageIndexChanging="grdItens_PageIndexChanging">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="DESCRICAO" HeaderText="Questão" HeaderStyle-Width="55%" />
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
    </div>
   
</asp:Content>
