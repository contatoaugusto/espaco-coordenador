<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarReuniao.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarReuniao" %>

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
                Tipo de Reuniao:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipoReuniao" runat="server"></asp:DropDownList>
            </div>
        </div>


        <div class="form-bottombuttons">
            <asp:Button ID="btConsultar" runat="server" Text="Consultar" Width="65px" OnClick="btConsultar_Click" />
            <asp:Button ID="btNovo" runat="server" Text="Novo" Width="65px" OnClick="btNovo_Click" />

            <div class="form-separator"></div>

            <h3>Lançamentos</h3>

            <sgi:GridView ID="grdReuniao" runat="server" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>

                    <asp:BoundField DataField="TITULO" HeaderText="Titulo" />
                    <asp:BoundField DataField="DATAHORA" HeaderText="Data" />
                    <asp:BoundField DataField="LOCAL" HeaderText="Local" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerSettings FirstPageImageUrl="~/Imagens/button-first-big-inactive.gif"
                    LastPageImageUrl="~/Imagens/button-last.gif"
                    NextPageImageUrl="~/Imagens/button-next-big-inactive.gif"
                    PreviousPageImageUrl="~/Imagens/button-prev.gif" PageButtonCount="8" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </sgi:GridView>
        </div>
    </div>
    </asp:Content>
