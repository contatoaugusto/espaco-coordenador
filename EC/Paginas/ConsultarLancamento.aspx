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
                <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Turma:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTurma" runat="server"></asp:DropDownList>
           
            </div>
        </div>

        <div class="form-bottombuttons">

            <asp:Button ID="btConsultar" runat="server" Text="Consultar" OnClick="btConsultar_Click" Width="65px" />
            <asp:Button ID="btNovo" runat="server" Text="Novo" OnClick="btnovo_Click" Width="65px" />
        </div>

        <div class="form-separator"></div>

        <h3>Lançamentos</h3>

        <sgi:GridView ID="grdLancamento" runat="server" AutoGenerateColumns="False">
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
        </sgi:GridView>

    </div>

</asp:Content>
