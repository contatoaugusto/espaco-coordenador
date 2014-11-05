<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarEvento.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarEvento" %>

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


    <h3>Consultar Evento</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo de Evento:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipoEvento" runat="server"></asp:DropDownList></td>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Responsável:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlPessoa" runat="server"></asp:DropDownList>

            </div>
        </div>
        <div class="form-bottombuttons">
            <asp:Button ID="btConsultar" runat="server" Text="Consultar" OnClick="btConsultar_Click" Width="65px" />
            <asp:Button ID="btNovo" runat="server" Text="Novo" OnClick="btnovo_Click" Width="65px" />
        </div>
    </div>
    <div class="form-separator"></div>

    <h3>Eventos</h3>

    <sgi:GridView ID="grdEvento" runat="server" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="NOME" HeaderText="Nome">
                <ControlStyle BackColor="#CCCCCC" BorderColor="Silver" />
            </asp:BoundField>
            <asp:BoundField DataField="LOCAL" HeaderText="Local">
                <ControlStyle BackColor="#CCCCCC" />
            </asp:BoundField>
            <asp:BoundField DataField="INICIO" HeaderText="Inicio">
                <ControlStyle BackColor="#CCCCCC" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </sgi:GridView>
 
    
</asp:Content>
