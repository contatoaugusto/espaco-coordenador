<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Avatar.ascx.cs" Inherits="UI.Web.EC.Coordenador.Avatar" %>


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
        o = document.getElementById('error-image');
        o.style.display = 'none';
    }
    function ShowError() {
        o = document.getElementById('error-image');
        o.style.display = 'block';
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
<asp:ImageButton ID="imgAluno" runat="server" Width="53" Height="64" OnClick="imgAluno_Click" />
<asp:UpdatePanel ID="UpdatePanel" runat="server">
    <contenttemplate>

        <asp:Button runat="server" ID="btnFotoAluno" Style="display: none" />
        <actk:ModalPopupExtender ID="mpeFotoAluno" runat="server" PopupControlID="pnlFotoAluno" BackgroundCssClass="modalbackground" TargetControlID="btnFotoAluno"></actk:ModalPopupExtender>
        <asp:Panel ID="pnlFotoAluno" runat="server" Style="display: none;height: 600px;">
            <div class="modalpopup">
                

                <div class="form-topbuttons"><ea:Button ID="btnClose" runat="server" Text="Fechar" CausesValidation="false" OnClick="btnClose_Click" /></div>
                <div class="form-in">
                    <h3>
                        Alterar Foto</h3>
                    <div class="row">
                        <asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand">
                            <HeaderTemplate>
                                <div class="column">
                                    Teste
                                    <%--<img id="imbFotoPessoa" alt="Foto do aluno" onclick="<%# "__doPostBack('ctl00$ctl00$CPHContent$header$avatar$cevFotoUsuario', '0');" %>" src='<%# string.Format("{0}?{1}",EC.Common.Library.ResolveClientUrl("~/Includes/FotoPessoa.ashx"), idPessoa.ToString()) %>' class="foto-aprovada" />--%>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="column" style="text-align: center">
                                <img alt="Foto do coordenador" onclick="<%# (EC.Common.Library.ToBoolean(Eval("icAprovado"))?"__doPostBack('ctl00$ctl00$CPHContent$header$avatar$cevFotoUsuario', '" + Eval("idFotoUsuario") + "');":"") %>" src="<%# string.Format("{0}?{1}",EC.Common.Library.ResolveClientUrl("~/Includes/FotoUsuario.ashx"), Eval("idFotoUsuario")) %>" class="<%# (EC.Common.Library.ToBoolean(Eval("icAprovado"))?"foto-aprovada":"foto-nao-avaliada")%>" /><br />
                                <asp:LinkButton runat="server" Text="Excluir" CommandArgument='<%# Eval("idFotoUsuario") %>' CausesValidation="false" CommandName="DEL" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="row">
                        A(s) foto(s) com a borda em vermelho ainda não foi(ram) aprovada(s) pelo departamento
                        responsável.</div>
                </div>
                <div id="divIncluirImagem" runat="server">
                <div class="separator"></div>
                <div class="form-in" style="overflow: auto; height:auto; max-height: 270px">
                    <h3>
                        Incluir Foto</h3>
                    <ec:Alert ID="alerterror" runat="server"></ec:Alert>
                    <div id="divUploadImage" runat="server">
                    <div class="row">
                        Você pode enviar arquivos JPG, GIF, BMP ou PNG com <b>tamanho máximo de 3MB</b>.
                        Não envie imagens que contenham personagens de desenho animado, pessoas famosas,
                        nudez, trabalhos artísticos ou qualquer material protegido por direitos autorais
                        ou direitos de imagem. Fotos que violem essas restrições poderão ser removidas a
                        qualquer tempo, sem aviso prévio ao aluno. A foto deve ter no mínimo <b>32 x 32 pixels</b>.
                    <div class="row">
                        <asp:FileUpload ID="fileUpload" runat="server" Width="100%" CssClass="textbox" onchange="javascript:UploadPhotoSubmit();"/>
                    </div>
                    </div>
                    <div id="error-image" class="row" style="display: none">
                    Imagem inválida. envie um arquivo com extensão JPG, JPEG, BMP, GIF ou PNG.
                    </div>
                    <div id="divCropImage" runat="server" visible="false">
                    <div class="row">
                    Para usar a foto em suas dimensões originais, clique o botão “Original”.
                    </div>
                    <div class="row">
                    Para selecionar uma parte de sua foto, aponte o ponteiro do mouse (cursor) para
                    um canto, clique e segure o botão direito e arraste até o canto oposto da área que
                    deseja recortar. Ajuste a seleção clicando os marcadores da imagem e arrastando
                    na direção desejada. Quando estiver satisfeito com a seleção, clique o botão Recortar.
                    </div>
                    <div id="upload-photo">
                    <div class="row">
                    <center>
                        <asp:Image ID="imageCrop" runat="server" Visible="true" />
                    </center>
                    </div>
                    <div class="row">
                    Senha do Espaço Aluno<br />
                    <sgi:TextBox ID="txtcoAcesso" runat="server" TextMode="Password" RequiredField="true" ErrorMessage="É obrigatório a senha do aluno para troca da foto." MaxLength="30"></sgi:TextBox>
                    </div>
                    <div class="row">
                    <label class="detail">Lembramos que a foto passará por um processo de aprovação.</label>
                    </div>
                    </div>
                    </div>
                </div>
                <div class="separator"></div>
                
                <div id="divUploadPhotoButtons">
                    <sgi:Button ID="btnOriginal" runat="server" Text="Usar original" OnClick="btnOriginal_Click" />&nbsp;<ea:Button ID="btnCrop"
                            runat="server" Text="Recortar" OnClick="btnCrop_Click" />&nbsp;<ea:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false" OnClick="btnCancel_Click" /></div>
                </div>
                <div class="cb"></div>

            </div>
            <%--<sgi:MessageBox ID="messageBox" runat="server" />--%>
        </asp:Panel>
        <sgi:CustomEventHandler ID="cevFotoUsuario" runat="server" OnCustomEvent="cevFotoUsuario_CustomEvent"></sgi:CustomEventHandler>
        <sgi:CustomEventHandler ID="cehUpload" runat="server" OnCustomEvent="cehUpload_CustomEvent"></sgi:CustomEventHandler>

    </contenttemplate>
</asp:UpdatePanel>