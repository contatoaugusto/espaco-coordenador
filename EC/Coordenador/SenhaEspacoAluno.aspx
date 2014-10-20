<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master"
    AutoEventWireup="true" CodeBehind="SenhaEspacoAluno.aspx.cs" Inherits="UI.Web.EC.Coordenador.Aluno_SenhaEspacoAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">Alteração de senha do Apoio ao Aluno</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form-in">
        <div class="row">
            Senha atual:<br />
            <sgi:TextBox ID="txtPassWordActual" runat="server" TextMode="Password" RequiredField="true"
                ErrorMessage="Digite sua senha atual do espaço aluno" Width="250px"></sgi:TextBox>
        </div>
        <div class="row">
            Nova senha:<br />
            <sgi:TextBox ID="txtNewPassWord" runat="server" TextMode="Password" RequiredField="true"
                ErrorMessage="Digite sua nova senha" Width="250px"></sgi:TextBox>
        </div>
        <div class="row">
            Confirmação da nova senha:<br />
            <sgi:TextBox ID="txtConfirmPassWord" runat="server" TextMode="Password" RequiredField="true"
                ErrorMessage="Digite a confirmação da sua nova senha" Width="250px"></sgi:TextBox>
        </div>
    </div>
    <div class="form-bottombuttons">
        <ea:MessageBox ID="messageBox" runat="server" />
        <ea:Button ID="ButtonConfirm" runat="server" Text="Confirmar" OnClick="ButtonConfirm_Click" />
    </div>
</asp:Content>

