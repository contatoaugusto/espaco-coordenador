<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarAta.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarAta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closeModalpopup").click(function () {
                document.location = "../default.aspx";
            });
        });<a href="ConsultarReuniao.aspx">ConsultarReuniao.aspx</a>
    </script>


    <h3>Consultar Ata de Reunião</h3>

    <div class="row">
        <div class="column w100">
            Reuniao: 
        </div>
        <div class="column w250">
            <asp:DropDownList ID="ddlReuniao" runat="server" OnSelectedIndexChanged="ddlReuniao_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>

        <div class="form-separator"></div>

        <div class="form-bottombuttons">
            <asp:Button ID="btConsultar" runat="server" Text="Consultar" OnClick="btConsultar_Click" />
            <asp:Button ID="btNovo" runat="server" Text="Novo" OnClick="btNovo_Click" />
        </div>

        <div class="form-separator"></div>

        <h3>Atas Cadastradas</h3>

        <sgi:GridView
            ID="grdReuniao"
            runat="server"
            AutoGenerateColumns="False"
            AllowPaging="true" PageSize="10"
            OnPageIndexChanging="grdItens_PageIndexChanging"
            OnRowCommand="grdReuniao_RowCommand">

            <Columns>
                <asp:BoundField DataField="TITULO" HeaderText="Titulo" />
                <asp:BoundField DataField="DATAHORA" HeaderText="Data" />
                <asp:BoundField DataField="LOCAL" HeaderText="Local" />
                <asp:TemplateField HeaderText="Ações">
                    <ItemTemplate>
                        <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_REUNIAO")%>' CommandName="Editar" Text="Editar" CausesValidation="false" />
                        |
                        <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_REUNIAO")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sgi:GridView>
    </div>
</asp:Content>
