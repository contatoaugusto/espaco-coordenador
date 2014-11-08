<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="~/Paginas/ConsultarRepresentanteTurma.aspx.cs" Inherits="UI.Web.EC.Paginas.ConsultarRepresentanteTurma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<script src="../Includes/ea.modal.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#closeModalpopup").click(function () {
                document.location = "../default.aspx";
            });
        });
    </script>


    <h3>Consultar Representante Turma</h3>

    <div class="form-in">
        
        <div class="form-separator"></div>

        <h3>Representantes Cadastrados</h3>

        <sgi:GridView 
            ID="grdRepresentanteTurma" 
            runat="server" 
            AutoGenerateColumns="False"
            AllowPaging="true" PageSize="10"
            OnPageIndexChanging="grdItens_PageIndexChanging">
            
            <Columns>
                <asp:TemplateField HeaderText="Ano">
                    <ItemTemplate>
                        <asp:Label ID="lblSemestreAno" runat="server" Text='<%# Bind("TURMA.SEMESTRE.ANO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semestre">
                    <ItemTemplate>
                        <asp:Label ID="lblSemestre" runat="server" Text='<%# Bind("TURMA.SEMESTRE.SEMESTRE1") %>'></asp:Label>º 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Turma">
                    <ItemTemplate>
                        <asp:Label ID="lblTurmaDescricao" runat="server" Text='<%# Bind("TURMA.TIPO_TURMA.DESCRICAO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Titular">
                    <ItemTemplate>
                        <asp:Label ID="lblTitular" runat="server" Text='<%#  Library.ToInteger(Eval("ID_TIPOREPRESENTANTE")) == 1 ? Eval("ALUNO_MATRICULA.ALUNO.PESSOA.NOME") :"" %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Suplente">
                    <ItemTemplate>
                        <asp:Label ID="lblSuplente" runat="server" Text='<%#  Library.ToInteger(Eval("ID_TIPOREPRESENTANTE")) == 2 ? Eval("ALUNO_MATRICULA.ALUNO.PESSOA.NOME") :"" %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Ações" >
                    <ItemTemplate>
                        <asp:LinkButton ID="HyperLink1" runat="server" CommandArgument='<%# Eval("ID_REPRESENTANTE")%>' CommandName="Editar" Text="Editar" CausesValidation="false" />
                        |
                        <asp:LinkButton ID="HyperLink2" runat="server" CommandArgument='<%# Eval("ID_REPRESENTANTE")%>' CommandName="Excluir" Text="Excluir" CausesValidation="false" />
                        |
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("ID_REPRESENTANTE")%>' CommandName="Imprimir" Text="Imprimir" CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
       </sgi:GridView>
    </div>
</asp:Content>
