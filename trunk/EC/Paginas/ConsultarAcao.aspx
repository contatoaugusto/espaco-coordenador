<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAcao.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAcao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Includes/ea.modal.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closeModalpopup").click(function () {
                document.location = "../default.aspx";
            });
        });
    </script>


    <h3>Consultar Ação</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Responsável:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlPessoa" runat="server" Style="margin-left: 0px"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Status:
            </div>

            <div class="column w250">
                <asp:DropDownList ID="ddlStatus" runat="server" Style="margin-left: 0px"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Prioridade:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlPrioridade" runat="server"></asp:DropDownList>

            </div>
        </div>

        <div class="form-separator"></div>

        <div class="form-bottombuttons">
            <ec:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
            <ec:Button ID="btnNovo" runat="server" Text="Novo" OnClick="btnNovo_Click" />
        </div>
    </div>

    <div class="form-separator"></div>

    <h3>Ações Cadastradas</h3>
    <sgi:GridView ID="grdAcao" runat="server" AutoGenerateColumns="False" BackColor="#BFBFBF" BorderColor="#BFBFBF" BorderStyle="Solid" CssClass="gridview" GroupFooter="False" PageSize="50" ShowWhenEmpty="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ID_ACAO" HeaderText="Nº"></asp:BoundField>
            <asp:BoundField DataField="TITULO" HeaderText="Titulo"></asp:BoundField>
            <asp:BoundField DataField="INICIO" HeaderText="Data" />
            <asp:TemplateField HeaderText="Responsável">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("PESSOA.NOME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_ACAO")%>' CommandName="Editar" Text="Editar" CausesValidation="false" />
                    |
                        <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_ACAO")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

<HeaderStyle CssClass="gridviewrowheader"></HeaderStyle>

<PagerStyle CssClass="gridviewpaging"></PagerStyle>

<RowStyle CssClass="gridviewrow"></RowStyle>

    </sgi:GridView>


</asp:Content>
