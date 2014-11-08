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
                <sgi:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" CausesValidation="false"></sgi:Button>
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
                    <asp:HiddenField ID="hddAluno" runat="server" />
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
                    Semestre:
                </div>
                <div class="column w200">
                    <asp:HiddenField ID="hddSemestre" runat="server" />
                    <asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>
                </div>
                <div class="column w50">
                    Turma:
                </div>
                <div class="column w200">
                    <asp:DropDownList ID="ddlTurma" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTurma" ErrorMessage="<br />Campo obrigatório" InitialValue="0"></asp:RequiredFieldValidator>
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
                    <asp:RadioButton ID="rdTitular" runat="server" Text="Titular" GroupName="tipoRepresentante" Checked="true"></asp:RadioButton>
                </div>
                <div class="column w100">
                    <asp:RadioButton ID="rdSuplente" runat="server" Text="Suplente" GroupName="tipoRepresentante"></asp:RadioButton>
                </div>
               
            </div>
            <div class="row">
            <div class="column w80">
                <sgi:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="Salvar_Click1" CausesValidation="true"></sgi:Button>
            </div>
        </div>
        </div>  
    </asp:Panel>
    <sgi:MessageBox ID="messageBox1" runat="server" />
    <sgi:AlertBox ID="alert" runat="server" AutoClose="true" Visible="false" Timeout="25" />
 </asp:Content>
