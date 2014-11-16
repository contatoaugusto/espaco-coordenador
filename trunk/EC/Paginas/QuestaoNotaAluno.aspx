<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/QuestaoNotaAluno.aspx.cs" Inherits="UI.Web.EC.Paginas.QuestaoNotaAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h3>Consultar Questão Nota Aluno</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w100">
                AMC:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlAmc" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Menção
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlMencao" runat="server"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="form-bottombuttons">
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" Width="65px" />
        <asp:Button ID="btnVoltar" runat="server" Text="Novo" Width="65px" OnClick="btnVoltar_Click" />
    </div>
    <div class="form-separator"></div>

    <h3>Questões Nota Aluno</h3>
    <sgi:GridView 
        ID="grdAlunoMatricula" 
        runat="server" 
        AutoGenerateColumns="False" 
        OnRowCommand="grdAlunoMatricula_RowCommand" 
        AllowPaging="true" PageSize="10"
        OnPageIndexChanging="grdItens_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Aluno">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ALUNO_MATRICULA.ALUNO.PESSOA.NOME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Menção">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("NOTA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
              
        </Columns>
    </sgi:GridView>
    
</asp:Content>
