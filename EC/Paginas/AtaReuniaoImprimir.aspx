<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AtaReuniaoImprimir.aspx.cs" Inherits="UI.Web.EC.Paginas.AtaReuniaoImprimir" %>
<%@ Import Namespace="UI.Web.EC" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onload="window.print()">
    <form id="form1" runat="server">
    

    <div class="form-in">
        
        
        <div class="row">
            <div class="column w100">
                Reuniao: 
            </div>
            <div class="column w250">
                <asp:DropDownList ID="ddlReuniao" runat="server" OnSelectedIndexChanged="ddlReuniao_SelectedIndexChanged" AutoPostBack="true" Enabled="false"></asp:DropDownList>
            </div>
        </div>
        <div class="form-separator"></div>
    
        <asp:Panel ID="pnlAta" runat="server" Visible="false">
            <div class="form-subtitle">
                Identificação da Reunião
            </div>

            <div class="row">
                <div class="column w80">
                    Pauta nº:
                </div>
                <div class="column w250">
                    <asp:Label ID="lblNumeroReunião" runat="server"/>
                </div>
            </div>
            <div class="row">
                <div class="column w90">
                    Data:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblDataTeuniao" runat="server"/>
                </div>
                <div class="column w80">
                    Hora:
                </div>
                <div class="column w100">
                    <asp:Label ID="lblHoraReuniao" runat="server"/>
                </div>
                <div class="column w80">
                    Local:
                </div>
                <div class="column w150">
                    <asp:Label ID="lblLocalReuniao" runat="server"/>
                </div>
            </div>
            <div class="form-separator"></div>
            <sgi:GridView 
                ID="grdParticipantesReuniao" 
                runat="server" 
                AutoGenerateColumns="False"
                EmptyDataText="Nenhum registro encontrado">
                <Columns>
                    <asp:TemplateField HeaderText="Nome">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PESSOA.NOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="E-mail">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PESSOA.EMAIL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Telefone">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("PESSOA.TELEFONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cargo/Função">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Utils.GetCargos(Eval("ID_PESSOA").ToInt32()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Presente" DataField="PRESENCA"/>
                </Columns>
            </sgi:GridView>
            <br/>
            
            <div class="form-subtitle">
                Pauta de Reunião
            </div>

            <div class="row">
                <div class="column">
                    <asp:Label ID="lblPauta" runat="server"/>
                </div>
            </div>
            
            <div class="form-subtitle">
                Pendências Anteriores
            </div>
            <sgi:GridView ID="grdPendencias" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:BoundField DataField="TIPO_ASSUNTO.DESCRICAO" HeaderText="Tipo" />
                </Columns>
            </sgi:GridView>

            <br />
            <div class="form-subtitle">
                Assuntos Tratados
            </div>
            <div class="row">
                <div class="column w450">
                    Descrição:
                </div>
                <div class="column w150">
                    Tipo
                </div>
            </div>

            <sgi:GridView ID="grdAssunto" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("TIPO_ASSUNTO_TRATADO.DESCRICAO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sgi:GridView>

            <br />
            <div class="form-subtitle">
                Compromissos
            </div>
            
            <div class="row">
                <div class="column w450">
                    Descrição:
                </div>
                <div class="column w200">
                    Responsável
                </div>
                 <div class="column w200">
                    Data
                </div>
            </div>
            
            <sgi:GridView ID="grdCompromisso" runat="server" AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Nº" DataField="ITEM" />
                    <asp:BoundField HeaderText="Descricão" DataField="DESCRICAO" />
                    <asp:BoundField HeaderText="Data" DataField="DATA"/>
                    <asp:TemplateField HeaderText="Responsável">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PESSOA.NOME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </sgi:GridView>

           <br />
            <div class="form-subtitle">
                Identificação do Registro
            </div>
            <div class="row">
                <div class="column w150">
                    Ata elaborada por:
                </div>
                <div class="column w200">
                    <asp:Label ID="lblResponsavelAta" runat="server" ></asp:Label>
                    <asp:HiddenField ID="hddResponsavelAta" runat="server" />
                </div>
                <div class="column w150">
                    Data do fechamento
                </div>
                <div class="column w200">
                    <asp:DropDownList ID="ddlDiaFechamento" runat="server"  Enabled="false"></asp:DropDownList>
                    <asp:DropDownList ID="ddlMesFechamento" runat="server" Enabled="false"></asp:DropDownList>
                    <asp:DropDownList ID="ddlAnoFechamento" runat="server" Enabled="false"></asp:DropDownList>
                </div>
            </div>

        </asp:Panel>
    </div>

    </form>
</body>
</html>
