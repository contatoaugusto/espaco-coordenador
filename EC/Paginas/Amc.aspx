<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Amc.aspx.cs" Inherits="UI.Web.EC.Paginas.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <sgi:MessageBox ID="messageBox" runat="server" />

    <h3>Criar AMC</h3>

    <div class="form-in">
        <div class="row">
            <div class="column w150">
                Semestre:
            </div>
            <div class="column w250">
                <sgi:DropDownList ID="ddlSemestre" runat="server" RequiredField="True"></sgi:DropDownList>
            </div>
        </div>  

        <div class="row">
            <div class="column w150">
                Ano:
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlAno" runat="server" RequiredField="True" TabIndex="1"></asp:DropDownList>    
            </div>
        </div>

        <div class="row">
            <div class="column w150">
                Data Aplicação da AMC:
            </div>
            <div class="column w250">
               <asp:TextBox ID="dtAplicacao" runat="server" ></asp:TextBox>
            </div>
        </div>
        <br /><br />
        <div class="form-bottombuttons">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="Salvar_Click1" />
            <%--<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />--%>
        </div>
    
       <%-- <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
                <uwc:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#BFBFBF" BorderColor="#BFBFBF" BorderStyle="Solid" CssClass="gridview" DataSourceID="EntityDataSource1" GroupFooter="False" PageSize="50" ShowWhenEmpty="False">
                </uwc:GridView>
                <asp:EntityDataSource ID="EntityDataSource1" runat="server">
                </asp:EntityDataSource>--%>
    </div>
    <sgi:MessageBox ID="messageBox1" runat="server" />
    <sgi:AlertBox ID="alert" runat="server" AutoClose="true" Visible="false" Timeout="25" />
</asp:Content>
