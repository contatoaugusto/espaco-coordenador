<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Evento.aspx.cs" Inherits="UI.Web.EC.Paginas.Evento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Cadastrar Evento</h3>


    <div class="form-in">
        <div class="row">
            <div class="column w100">
                Tipo de Evento:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlTipoEvento" runat="server"></asp:DropDownList>
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
                Nome:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtNome" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Descrição:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtDescricao" runat="server" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Local:
            </div>
            <div class="column w250">
                <asp:TextBox ID="TxtLocal" runat="server" Width="500px" TextMode="MultiLine" Rows="1"></asp:TextBox>
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
                Data Inicio:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlDia" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMes" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlAno" runat="server"></sgi:DropDownList>

                           </div>

            <div class="column w100">
                Hora Inicio:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlHora" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMinuto" runat="server"></sgi:DropDownList>
               
            </div>
        </div>
        
        <div class="row">
            <div class="column w100">
                Data Fim:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlDia1" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMes1" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlAno1" runat="server"></sgi:DropDownList>

           
            </div>

            <div class="column w100">
                Hora Fim:
            </div>
            <div class="column w200">
                <sgi:DropDownList ID="ddlHora1" runat="server"></sgi:DropDownList>
                <sgi:DropDownList ID="ddlMinuto1" runat="server"></sgi:DropDownList>
                
            </div>
        </div>
        <div class="form-bottombuttons">
            <sgi:Button ID="btnSalvarReuniao" runat="server" Text="Salvar" />
            <sgi:Button ID="btnEnviaEmail" runat="server" Text="Enviar E-mail"/>
            <sgi:Button ID="btnVoltar" runat="server" Text="Voltar" />
        </div>
    </div>
    
</asp:Content>






















