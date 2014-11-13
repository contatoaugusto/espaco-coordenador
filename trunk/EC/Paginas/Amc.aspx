<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Amc.aspx.cs" Inherits="UI.Web.EC.Paginas.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <sgi:MessageBox ID="messageBox" runat="server" />
    <%--<sgi:UpdatePanel ID="pnlAmc" runat="server">--%>

    <div class="form-separator"></div>
    <h3>Abrir AMC</h3>

    <asp:Panel ID="pnlAmc" runat="server">

        <div class="row">
            <div class="column w150">
                Semestre corrente:
            </div>
            <div class="column w250">
                <asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="column w150">
                Curso:
            </div>
            <div class="column w250">
                <asp:Label ID="lblCursoUnico" runat="server"></asp:Label>
                
            </div>
        </div>
        <div class="row">
            <div class="column w100">
                Data da Aplicação:
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
        <div class="form-bottombuttons">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="Salvar_Click1" />
         
               <%--<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />--%>
        </div>
    </asp:Panel>
    <div class="form-separator"></div>
    <hr class="line" />
    <h3>AMCs Cadastradas</h3>

    <sgi:GridView ID="GridView_AMC" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_AMC" Width="980px" BackColor="#BFBFBF" BorderColor="#BFBFBF"
        BorderStyle="Solid" CssClass="gridview" GroupFooter="False" PageSize="50" ShowWhenEmpty="False"
        OnRowCommand="GridView_AMC_RowCommand">
        <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
        <Columns>
     
            <asp:TemplateField HeaderText="Semestre/Ano">
                <ItemTemplate>
                    <asp:Label ID="lblSemestereAno" runat="server" Text='<%# Bind("SEMESTRE.SEMESTRE1") %>'></asp:Label>º sem/
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("SEMESTRE.ANO") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:BoundField DataField="DATA_APLICACAO" HeaderText="Data e hora da Aplicação" SortExpression="DATA_APLICACAO" />
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_AMC")%>' CommandName="Editar" Text="Editar" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <HeaderStyle CssClass="gridviewrowheader"></HeaderStyle>

        <PagerStyle CssClass="gridviewpaging"></PagerStyle>

        <RowStyle CssClass="gridviewrow"></RowStyle>
    </sgi:GridView>




    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ECEntities" DefaultContainerName="ECEntities" EnableDelete="True" EnableFlattening="False" EnableUpdate="True" EntitySetName="AMC">
    </asp:EntityDataSource>


    <sgi:MessageBox ID="messageBox1" runat="server" />
    <sgi:AlertBox ID="alert" runat="server" AutoClose="true" Visible="false" Timeout="25" />
    <%--</sgi:UpdatePanel>--%>
</asp:Content>
