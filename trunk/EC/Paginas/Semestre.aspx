<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="/Paginas/Semestre.aspx.cs" Inherits="UI.Web.EC.Paginas.Semestre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        function Confirma() {
            var x;
            if (confirm("Deseja abrir semestre!") == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>

    
    <div class="form-separator"></div>

    <h3>Abrir Semestre</h3>
    <div class="form-in">
        <div class="row" id="divInstrucoesGeraProva" runat="server">
            Pode existir apenas um semestre ativo por vez (aberto). <br/>
        </div>

        <asp:Panel ID="pnlSemestreAberto" runat="server" Visible="true">
            <h1>Início</h1>
            <div class="row">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="column w100">
                    Ano:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblAnoInicio" runat="server"></asp:Label>
                </div>
                <div class="column w100">
                    Semestre:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblSemestreInicio" runat="server"></asp:Label>
                </div>
            </div>
            <div class="form-separator"></div>
            <h1>Corrente</h1>
            <div class="row">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <div class="column w100">
                    Ano:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblAnoCorrente" runat="server"></asp:Label>
                </div>
                <div class="column w100">
                    Semestre:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblSemestreCorrente" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>    
    </div>
    
    <div class="cb"></div>    
    <div class="form-bottombuttons">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button ID="btnAbrirSemestre" runat="server" OnClientClick="return Confirma();" Text="Abrir semestre" OnClick="BtnAbrirSemestreClick" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
 

    <div class="form-separator"></div>

     <h3>Semestres Cadastrados</h3>
    <asp:Panel ID="pnlGridSemestre" runat="server">
        <sgi:GridView ID="gridViewSemestre" 
            runat="server" 
            AutoGenerateColumns="False" 
            DataKeyNames="ID_SEMESTRE" 
            BackColor="#BFBFBF" 
            BorderColor="#BFBFBF" 
            BorderStyle="Solid" 
            CssClass="gridview" 
            GroupFooter="False" 
            PageSize="50" 
            ShowWhenEmpty="False"
        
            >
            <AlternatingRowStyle CssClass="gridviewrowalternating"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField DataField="ANO" HeaderText="Ano" SortExpression="ANO" />
                <asp:BoundField DataField="SEMESTRE1" HeaderText="Semestre" SortExpression="SEMESTRE1" />
                 <asp:BoundField DataField="ATIVO" HeaderText="ATIVO" SortExpression="ATIVO" />
                <asp:TemplateField HeaderText="Ativo">
                    <ItemTemplate>
                        <asp:RadioButton ID="ATIVO" runat="server" GroupName="SuppliersGroup" OnCheckedChanged="ATIVO_CheckedChanged"  AutoPostBack="true" Checked='<%# Eval("ATIVO") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                Não existe semestre cadastrado.
            </EmptyDataTemplate>
        </sgi:GridView>
    </asp:Panel>
</asp:Content>

