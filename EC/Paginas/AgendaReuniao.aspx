<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/AgendaReuniao.aspx.cs" Inherits="UI.Web.EC.Reuniao.AgendaReuniao" %>

<%@ Import Namespace="EC.Negocio" %>
<%@ Import Namespace="UI.Web.EC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form-separator"></div>
    <h3>Cadastrar Reunião</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo de Reuniao:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlTipoReuniao" runat="server" RequiredField="true"></sgi:DropDownList>
                <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="ddlTipoReuniao" ErrorMessage="<br />Campo obrigatório" InitialValue="0"></asp:RequiredFieldValidator>
            </div>
            <div class="column w100">
                Semestre:
            </div>
            <div class="column w200">
                <asp:HiddenField ID="hddSemestreCorrente" runat="server"></asp:HiddenField>
                <asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Data:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlDia" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMes" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlAno" runat="server"></sgi:DropDownList>

            </div>

            <div class="column w100">
                Hora:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlHora" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMinuto" runat="server"></sgi:DropDownList>
               
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Título:
            </div>
            <div class="column w500">
                <asp:TextBox ID="txtTitulo" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTitulo" ErrorMessage="<br />Campo obrigatório"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Local:
            </div>
            <div class="column w500">
                <asp:TextBox ID="txtLocal" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLocal" ErrorMessage="<br />Campo obrigatório"></asp:RequiredFieldValidator>

            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Participantes:
            </div>
            <div class="column w500">
                <sgi:DropDownList ID="ddlParticipante" runat="server"></sgi:DropDownList>
                <asp:ImageButton ID="imgAdicionaParticipante" runat="server" ImageUrl="~/Imagens/adicionar.jpg" OnClick="imgAdicionaParticipante_Click" Width="23px" />
            </div>
        </div>

        <sgi:GridView ID="grdParticipante" runat="server" AutoGenerateColumns="False"
            BackColor="#BFBFBF"
            BorderColor="#BFBFBF"
            BorderStyle="Solid"
            CssClass="gridview"
            GroupFooter="False"
            PageSize="10"
            ShowWhenEmpty="False">
            <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField HeaderText="Nome" DataField="NOME" />
                <asp:BoundField HeaderText="E-Mail" DataField="EMAIL" />
                <asp:BoundField HeaderText="Telefone" DataField="TELEFONE" />
                <asp:TemplateField HeaderText="Cargo/Função">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Utils.GetCargos(Eval("ID_PESSOA").ToInt32()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sgi:GridView>

        <div class="form-separator"></div>

        <div class="row">
            <div class="column w100">
                Pauta:
            </div>
            <div class="column w550">
                <asp:TextBox ID="txtPauta" runat="server" Width="500px" TextMode="MultiLine" Rows="3" Style="margin-left: 0px"></asp:TextBox>
                <asp:ImageButton ID="btnIncluirauta" runat="server" ImageUrl="~/Imagens/adicionar.jpg" OnClick="ImageButton1_Click" Width="23px" />
            </div>
        </div>
    </div>


    <sgi:GridView
        ID="grdPauta" runat="server" AutoGenerateColumns="False"
        BackColor="#BFBFBF"
        BorderColor="#BFBFBF"
        BorderStyle="Solid"
        CssClass="gridview"
        GroupFooter="False"
        PageSize="10"
        ShowWhenEmpty="False">
        <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField HeaderText="Nº" DataField="ITEM" />
            <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
        </Columns>
    </sgi:GridView>

    <div class="form-separator"></div>
    
    <div class="form-bottombuttons">
        <asp:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" Width="65px" OnClick="btnSalvarReuniao_Click" />
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="65px" OnClick="btnVoltar_Click" />
    </div>
  
</asp:Content>
