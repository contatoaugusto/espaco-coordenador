<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/Questao.aspx.cs" Inherits="UI.Web.EC.Paginas.Questao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<style type="text/css">
        .auto-style1 {
            height: 167px;
        }

        .auto-style2 {
            width: 202px;
            height: 167px;
        }

        .auto-style3 {
            width: 106px;
        }

        .auto-style4 {
            height: 167px;
            width: 106px;
        }
        .auto-style5 {
            width: 202px;
        }
    </style>--%>
</asp:Content>
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
    <sgi:MessageBox ID="messageBox" runat="server" />
    
    <div class="form-separator"></div>
    <h3>Incluir Questões</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                AMC:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlAmc" runat="server" RequiredField="true"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Disciplina:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlDisciplina" runat="server" OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged" AutoPostBack="true" RequiredField="true"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="column w100">
                Professor:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlFuncionario" runat="server"></sgi:DropDownList>
            </div>
        </div>

        <div class="row">
            <%--<div class="column w250">--%>
                Pergunta:<br/>
                <sgi:TextArea ID="TxtDescricao" runat="server" Width="80%" Rows="5" MaxLength="3000" RequiredField="true" ErrorMessage="É obrigatorio informar a pergunta"></sgi:TextArea> 
            <%--</div>--%>
        </div>

        <div class="row">
            <div class="column w250">
                <asp:FileUpload ID="upLoad" runat="server" />
            </div>
        </div>
         
        <div class="row">
            <div class="column w15p">
                Resposta Correta:<asp:CheckBox ID="Correta1" runat="server" Checked="False" />
            </div>
            <div class="column w500">
                <asp:TextBox ID="TxtEscolha1" runat="server" Width="600px" TextMode="MultiLine" Rows="2" ></asp:TextBox>
            </div>
        </div>
        
        <div class="row">
            <div class="column w15p">
                Resposta Correta:<asp:CheckBox ID="Correta2" runat="server" Checked="False" />
            </div>
            <div class="column w500">
                <asp:TextBox ID="TxtEscolha2" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="column w15p">
                Resposta Correta:<asp:CheckBox ID="Correta3" runat="server" Checked="False" />
            </div>
            <div class="column w500">
                <asp:TextBox ID="TxtEscolha3" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>

         <div class="row">
            <div class="column w15p">
                Resposta Correta:<asp:CheckBox ID="Correta4" runat="server" Checked="False" />
            </div>
            <div class="column w500">
                <asp:TextBox ID="TxtEscolha4" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>
         
        <div class="row">
            <div class="column w15p">
                Resposta Correta:<asp:CheckBox ID="Correta5" runat="server" Checked="False" /> 
            </div>
            <div class="column w500">
                <asp:TextBox ID="TxtEscolha5" runat="server" Width="600px" TextMode="MultiLine" Rows="2"></asp:TextBox>
            </div>
        </div>
           
        <div class="form-bottombuttons">
            <sgi:Button ID="Button1" runat="server" Text="Salvar" OnClick="Button1_Click1" />
            <sgi:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />
        </div>
    </div>
</asp:Content>
