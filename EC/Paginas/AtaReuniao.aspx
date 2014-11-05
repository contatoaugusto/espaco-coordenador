<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/AtaReuniao.aspx.cs" Inherits="UI.Web.EC.Paginas.AtaReunião" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../EC.UI.WebControls/MessageBox/Styles/Outlook.css" rel="stylesheet" />
    <link href="../EC.UI.WebControls/MessageBox/Styles/UniCEUB.css" rel="stylesheet" />


    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Incluir Ata de Reunião</h3>


    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Reuniao: 
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlReuniao" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Pendências:
            </div>
            <div class="column w250">
                <asp:Button ID="btntransferirPendencias" runat="server" Text="Transferir Pendências" Width="143px" />
            </div>
        </div>


        <sgi:GridView ID="grdPendencias" runat="server" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                <asp:BoundField DataField="TIPO_ASSUNTO.DESCRICAO" HeaderText="Tipo" />
            </Columns>
        </sgi:GridView>

        <div class="form-separator"></div>

        <div class="row">
            <div class="column w100">
                Assuntos Tratados:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtAssunto" runat="server" Width="500px" TextMode="MultiLine" Rows="2" Style="margin-left: 0px"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Tipo:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipoAssunto" runat="server"></asp:DropDownList>
                <asp:ImageButton ID="btnIncluirAssunto" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluirAssunto_Click" /></td>
            </div>
        </div>

        <sgi:GridView ID="grdAssunto" runat="server" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                <asp:TemplateField HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID_TIPOASSTRATADO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField Text="Alterar" />
                <asp:ButtonField Text="Excluir" />
            </Columns>
        </sgi:GridView>

        <div class="form-separator"></div>

        <div class="row">
            <div class="column w100">
                Compromissos:
            </div>
            <div class="column w250">

                <asp:TextBox ID="TxtCompromisso" runat="server" Width="500px" TextMode="MultiLine" Rows="2" Style="margin-left: 0px"></asp:TextBox>
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

        <div class="row">
            <div class="column w100">
                Data
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlDia" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMes" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAno" runat="server"></asp:DropDownList>

                <asp:ImageButton ID="btnIncluircompromisso" runat="server" ImageUrl="~/Imagens/adicionar.jpg" Width="23px" OnClick="btnIncluircompromisso_Click1" />
            </div>
        </div>

        <sgi:GridView ID="grdCompromisso" runat="server" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />

                <asp:BoundField HeaderText="Data " />
                <asp:BoundField DataField="ID_PESSOA" HeaderText="Responsável" />

            </Columns>
        </sgi:GridView>



        <div class="form-bottombuttons">

            <asp:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" Width="65px" OnClick="btnSalvarReuniao_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px" OnClick="btnVoltar_Click" />
        </div>
    </div>
</asp:Content>
