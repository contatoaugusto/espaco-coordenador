<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeFile="Avatar.aspx.cs" Inherits="UI.Web.EC.Coordenador.Avatar1" EnableEventValidation="false"%>

<%--<%@ Register TagPrefix="ec" Namespace="UI.Web.EC.Coordenador" Assembly="App_Code" %>--%>

<%@ Import Namespace="EC.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="../Includes/jquery.Jcrop.min.js"></script>
    <script type="text/javascript">

        var validImages = /\.(jpeg|jpg|gif|png|bmp)/i;

        function UploadPhotoSubmit() {

            if (ValidateFileName(document.getElementById('<%= fileUpload.ClientID %>'))) {
                HideError();
                try {
                    __doPostBack('<%= cehUpload.UniqueID %>', '0');
                } catch (e) {
                    alert(e.message);
                }
            }
        }
        function ValidateFileName(inputField) {
            if (inputField.value == "")
                return false;

            if (validImages.exec(inputField.value) == null) {
                ShowError();
                return false;
            }
            return true;
        }

        function HideError() {
            $('#error-image').hide();
        }
        function ShowError() {
            $('#error-image').show();
        }

        function updateCoords(c) {
            $('#<%= hdfX.ClientID %>').val(c.x);
            $('#<%= hdfY.ClientID %>').val(c.y);
            $('#<%= hdfW.ClientID %>').val(c.w);
            $('#<%= hdfH.ClientID %>').val(c.h);
        };

        $(document).ready(function () {
            $('#<%= imageCrop.ClientID %>').Jcrop({
                aspectRatio: 1,
                onSelect: updateCoords
            });
        });
    </script>
    <asp:HiddenField ID="hdfX" runat="server" />
    <asp:HiddenField ID="hdfY" runat="server" />
    <asp:HiddenField ID="hdfW" runat="server" />
    <asp:HiddenField ID="hdfH" runat="server" />
    <sgi:Alert ID="alert" runat="server" />
    <h3>
        Foto Disponíveis</h3>

    <div class="form-in">
        <div class="row">
            <asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand">
                <HeaderTemplate>
                    <div class="column">
                        <img id="imbFotoPessoa" alt="Foto do Coordenador" onclick="<%# "__doPostBack('" + cevFotoUsuario.UniqueID + "', '0');" %>"
                            src='<%# string.Format("{0}?{1}",Library.ResolveClientUrl("~/Includes/FotoPessoa.ashx"), idPessoa.ToString()) %>'
                            class="foto-aprovada" />
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="column" style="text-align: center">
                        <img alt="Foto do aluno" onclick="<%# (Library.ToBoolean(Eval("icAprovado"))?"__doPostBack('" + cevFotoUsuario.UniqueID + "', '" + Eval("idFotoUsuario") + "');":"") %>"
                            src="<%# string.Format("{0}?{1}",Library.ResolveClientUrl("~/aluno/imagem.ashx"), Eval("idFotoUsuario")) %>"
                            class="<%# (Library.ToBoolean(Eval("icAprovado"))?"foto-aprovada":"foto-nao-avaliada")%>" /><br />
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Excluir" CommandArgument='<%# Eval("idFotoUsuario") %>'
                            CausesValidation="false" CommandName="DEL" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="row">
            A(s) foto(s) com a borda em vermelho ainda não foi(ram) aprovada(s) pelo departamento
            responsável.</div>
    </div>
    <div id="divIncluirImagem" runat="server">
        <br />
        <h3>
            Incluir Foto</h3>
        <div class="form-in">
            <div class="row" id="divUploadImage0" runat="server">
                Você pode enviar arquivos JPG, GIF, BMP ou PNG com <b>tamanho máximo de 3MB</b>.
                Não envie imagens que contenham personagens de desenho animado, pessoas famosas,
                nudez, trabalhos artísticos ou qualquer material protegido por direitos autorais
                ou direitos de imagem. Fotos que violem essas restrições poderão ser removidas a
                qualquer tempo, sem aviso prévio ao aluno. A foto deve ter no mínimo <b>32 x 32 pixels</b>.
            </div>
            <div class="row" id="divUploadImage1" runat="server">
                <asp:FileUpload ID="fileUpload" runat="server" Width="100%" CssClass="textbox" onchange="javascript:UploadPhotoSubmit();" />
            </div>
            <div id="error-image" class="row" style="display: none">
                Imagem inválida. envie um arquivo com extensão JPG, JPEG, BMP, GIF ou PNG.
            </div>
            <div class="row" id="divCropImage0" runat="server" visible="false">
                Para usar a foto em suas dimensões originais, clique o botão “Original”.
            </div>
            <div class="row" id="divCropImage1" runat="server" visible="false">
                Para selecionar uma parte de sua foto, aponte o ponteiro do mouse (cursor) para
                um canto, clique e segure o botão direito e arraste até o canto oposto da área que
                deseja recortar. Ajuste a seleção clicando os marcadores da imagem e arrastando
                na direção desejada. Quando estiver satisfeito com a seleção, clique o botão Recortar.
            </div>
            <div class="row" id="divCropImage2" runat="server" visible="false">
                <%--<center>--%>
                    <asp:Image ID="imageCrop" runat="server" Visible="true" />
                <%--</center>--%>
            </div>
            <div class="row" id="divCropImage3" runat="server" visible="false">
                Senha do Espaço Professor<br />
                <asp:TextBox ID="txtcoAcesso" runat="server" TextMode="Password" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv0" runat="server" ControlToValidate="txtcoAcesso"
                    ErrorMessage="<br />É obrigatório a senha do coordenador para troca da foto."></asp:RequiredFieldValidator>
            </div>
            <div class="row" id="divCropImage4" runat="server" visible="false">
                <label class="detail">
                    Lembramos que a foto passará por um processo de aprovação.</label>
            </div>
        </div>
        <div class="separator">
        </div>
        <div id="divUploadPhotoButtons" runat="server" visible="false">
            <sgi:Button ID="btnOriginal" runat="server" Text="Usar original" OnClick="btnOriginal_Click" />
            &nbsp;<sgi:Button ID="btnCrop" runat="server" Text="Recortar" OnClick="btnCrop_Click" />
            &nbsp;<sgi:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                OnClick="btnCancel_Click" />
        </div>
    </div>
    <div class="cb">
    </div>
    <sgi:MessageBox ID="messageBox" runat="server" />
    <asp:Button ID="cevFotoUsuario" runat="server" OnClick="cevFotoUsuario_CustomEvent"
        CssClass="dn" />
    <asp:Button ID="cehUpload" runat="server" OnClick="cehUpload_CustomEvent" CssClass="dn" />

</asp:Content>
