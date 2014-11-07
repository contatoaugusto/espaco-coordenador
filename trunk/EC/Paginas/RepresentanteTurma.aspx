<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="RepresentanteTurma.aspx.cs" Inherits="UI.Web.EC.Paginas.RepresentanteTurma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />

    <div class="form-separator"></div>
    <h3>Incluir Representante de Turma</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w50">
                RA:
            </div>
            <div class="column w80">
                <asp:TextBox ID="txtRA" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtRA" ErrorMessage="<br />Campo obrigatório"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row">
            <div class="column w80">
                <sgi:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" CausesValidation="true"></sgi:Button>
            </div>
        </div>

    <div class="form-separator"></div>
    
    <asp:Panel ID="pnlRepresentante" runat="server" Visible="false">
        <div class="form-in">
            <div class="row">
                <div class="column w100">
                    RA:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblRA" runat="server"></asp:Label>
                </div>
                <div class="column w50">
                    Nome:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblNome" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    E-mail:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </div>
                <div class="column w50">
                    Telefone:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblTelefone" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="column w100">
                    Representante:
                </div>
                <div class="column w100">
                    <sgi:CheckBox ID="chkTitular" runat="server" Text="Titular"></sgi:CheckBox>
                </div>
                <div class="column w100">
                    <sgi:CheckBox ID="chkSuplente" runat="server" Text="Suplente"></sgi:CheckBox>
                </div>
               
            </div>
            <div class="row">
            <div class="column w80">
                <sgi:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" CausesValidation="false"></sgi:Button>
            </div>
        </div>
        </div>  
    </asp:Panel>
    <sgi:MessageBox ID="messageBox1" runat="server" />
    <sgi:AlertBox ID="alert" runat="server" AutoClose="true" Visible="false" Timeout="25" />
 </asp:Content>
