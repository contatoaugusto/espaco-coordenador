﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Prova.aspx.cs" Inherits="UI.Web.EC.Paginas.Prova" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <sgi:MessageBox ID="messageBox" runat="server" />
    <div class="form-separator"></div>

    <h3>Incluir Questão na prova da AMC</h3>
    <asp:Panel ID="pnlProva" runat="server">
    
         <div class="form-in">
            <div class="row" id="divInstrucoesGeraProva" runat="server">
                A partir do banco de questões previamente criado, ao cliclar no botão Gerar Prova, 
                o sistema irá selecionar, de forma aleatória, 32 dessas questões pertencentes a AMC selecionada e 
                motar uma prova com seu gabarito correspondente.
            </div>


            <div class="row">
                <div class="column w150">
                    AMC:
                </div>
                <div class="column w250">
                    <sgi:DropDownList ID="ddlAmc" runat="server" equiredField="true"></sgi:DropDownList>
                    <%--<asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>--%>
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
                <div class="column w150">
                    Quantidade de questões:
                </div>
                <div class="column w250">
                    <asp:TextBox ID="txtQuantidadeQuestoes" runat="server" RequiredField="true"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="column w150">
                    Observação
                </div>
                <div class="column w250">
                    <asp:TextBox ID="txtObservacao" runat="server" RequiredField="true"></asp:TextBox>
                </div>
            </div>
        </div>
        
        <div class="form-separator"></div>
        s
        <div class="form-bottombuttons">
            <asp:Button ID="btnSalvar" runat="server" Text="Gerar Prova" OnClick="btnGerarProva_Click"/>
            <%--<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"  />--%>
        </div>
    </asp:Panel>
    <div class="form-separator"></div>

    <h3>AMCs Cadastradas</h3>
    <sgi:GridView ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="ID_PROVA"
        DataSourceID="SqlDataSource"
        BackColor="#BFBFBF"
        BorderColor="#BFBFBF"
        BorderStyle="Solid"
        CssClass="gridview"
        GroupFooter="False"
        PageSize="50"
        ShowWhenEmpty="False"
        OnRowCommand="GridView1_RowCommand">
        <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="DATA_CRIACAO" HeaderText="Data Criação" SortExpression="DATA_CRIACAO" />
            <asp:BoundField DataField="OBSERVACAO" HeaderText="Observação" SortExpression="OBSERVACAO" />
            <asp:BoundField DataField="QTDE_QUESTOES" HeaderText="Qtde Questões" SortExpression="QTDE_QUESTOES" />
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_PROVA")%>' CommandName="Imprimir" Text="Imprimir" CausesValidation="false" />
                    |
                    <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_PROVA")%>' CommandName="Gabarito" Text="Gabarito" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

        <HeaderStyle CssClass="gridviewrowheader"></HeaderStyle>

        <PagerStyle CssClass="gridviewpaging"></PagerStyle>

        <RowStyle CssClass="gridviewrow"></RowStyle>
    </sgi:GridView>
    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ECEntities %>" SelectCommand="SELECT * FROM [PROVA]"></asp:SqlDataSource>
</asp:Content>