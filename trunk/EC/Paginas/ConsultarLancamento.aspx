<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarLancamento.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarLancamento" %>

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


    <h3>Consultar Lançamento</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo Lancamento:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipolancamento" runat="server"></asp:DropDownList>
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

      
        <div class="form-separator"></div>
        <div class="form-bottombuttons">

            <ec:Button ID="btConsultar" runat="server" Text="Consultar" OnClick="btConsultar_Click"/>
            <ec:Button ID="btNovo" runat="server" Text="Novo" OnClick="btnovo_Click" />
        </div>

        <div class="form-separator"></div>

        <h3>Lançamentos</h3>

        <sgi:GridView ID="grdLancamento" runat="server" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID_TIPOLANCAMENTO" HeaderText="Tipo" />
                <asp:BoundField DataField="ID_TURMA" HeaderText="Turma" />
                <asp:BoundField DataField="DATA_LANCAMENTO" HeaderText="Data" />
                <asp:BoundField DataField="ID_FUNCIONARIO" HeaderText="Professor" />
                <asp:TemplateField HeaderText="Ações">
                    <ItemTemplate>
                        <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_LANCAMENTO")%>' CommandName="Editar" Text="Editar" CausesValidation="false"/>
                        |
                        <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_LANCAMENTO")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </sgi:GridView>

    </div>

</asp:Content>
