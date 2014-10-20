<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DadosPessoais.ascx.cs"
    Inherits="UI.Web.EC.Coordenador.AlunoDadosPessoais" %>
<%@ Register TagPrefix="ec" Namespace="UI.Web.EC.Controls" Assembly="App_Code" %>

<h3>
    Dados Pessoais</h3>
<div class="form-in">
    <div class="row">
        Nome:<br />
        <sgi:TextBox ID="txtnmPessoa" runat="server" Width="400px" MaxLength="50"
            ErrorMessage="<br />O nome do aluno é obrigatório."></sgi:TextBox>
    </div>
    <div class="row">
        <div class="column w200">
            Sexo:<br />
            <asp:RadioButton ID="rbMasculino" runat="server" Checked="True" GroupName="grpSexo"
                Text="Masculino"></asp:RadioButton>
            <asp:RadioButton ID="rbFeminino" runat="server" GroupName="grpSexo" Text="Feminino">
            </asp:RadioButton>
        </div>
        <div>
            Estado Civil:<br />
            <sgi:DropDownList ID="ddlidEstadoCivil" runat="server">
            </sgi:DropDownList>
        </div>
    </div>
    <div class="row">
        Nome do Pai:<br />
        <sgi:TextBox ID="txtnmPai" runat="server" Width="400px" MaxLength="100"
            ErrorMessage="<br />O nome do pai do aluno é obrigatório." SetFocusOnError="true"></sgi:TextBox>
    </div>
    <div class="row">
        Nome da Mãe:<br />
        <sgi:TextBox ID="txtnmMae" runat="server" Width="400px" MaxLength="100"
            ErrorMessage="<br />O nome da mãe do aluno é obrigatório." SetFocusOnError="true"></sgi:TextBox>
    </div>
    <div class="cb">
    </div>
</div>
<div class="form-separator">
</div>
<h3>
    Origem</h3>
<div class="form-in">
    <div class="row">
        <div class="column w200">
            Data de Nascimento:<br />
            <sgi:TextBox ID="txtdtNascimento" runat="server" TextBoxType="Date"
                ErrorMessage="<br />*A data de nascimento é obrigatória." SetFocusOnError="true"
                CustomErrorMessage="Data inválida" Width="80"></sgi:TextBox>
        </div>
        <div class="column w250">
            Cidade de Nascimento:<br />
            <sgi:TextBox ID="txtdeNaturalidade" runat="server" Width="200px" MaxLength="50"
                ErrorMessage="<br />A naturalidade é obrigatória." SetFocusOnError="true"></sgi:TextBox>
        </div>
        <div class="column">
            UF de Nascimento:<br />
            <sgi:DropDownList ID="ddlidUFNascimento" runat="server" OnSelectedIndexChanged="ddlidUFNascimento_SelectedIndexChanged"
                AutoPostBack="true">
            </sgi:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="column w450">
            Município de Nascimento:<br />
            <sgi:DropDownList ID="ddlidMunicipioINEPNascimento" runat="server">
            </sgi:DropDownList>
        </div>
        <div class="column">
            Nacionalidade:<br />
            <sgi:DropDownList ID="ddlidPais" runat="server">
            </sgi:DropDownList>
        </div>
    </div>
    <div class="cb">
    </div>
</div>
<div class="form-separator">
</div>
<h3>
    Documentos de Identificação</h3>
<div class="form-in">
    <div class="row">
        <div class="column w200">
            Documento de Identidade:<br />
            <sgi:TextBox ID="txtnuRG" runat="server" MaxLength="20" Width="130"
                ErrorMessage="<br />O nº do documento de identidade é obrigatório." SetFocusOnError="true"></sgi:TextBox>
        </div>
        <div class="column w150">
            Órgão Emissor:<br />
            <sgi:TextBox ID="txtsgOrgaoExpedidorRG" runat="server" MaxLength="10" Width="75px"
                ErrorMessage="<br />O órgão emissor da identidade é obrigatório."
                SetFocusOnError="true"></sgi:TextBox>
        </div>
        <div class="column w150">
            UF:<br />
            <sgi:DropDownList ID="ddlidUFExpedicaoRG" runat="server">
            </sgi:DropDownList>
        </div>
        <div class="column">
            Data de Expedição:<br />
            <sgi:TextBox ID="txtdtExpedicao" runat="server" MaxLength="10" TextBoxType="Date"
                RequiredField="true" ErrorMessage="<div><div class='validation'>A data de expedição é obrigatória.</div></div>"
                SetFocusOnError="true" CustomErrorMessage="Data inválida" Width="80"></sgi:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="column w200">
            Título de Eleitor:<br />
            <sgi:TextBox ID="txtnuTituloEleitor" runat="server" MaxLength="20" Size="0" Width="130"></sgi:TextBox>
        </div>
        <div class="column w150">
            Zona:<br />
            <sgi:TextBox ID="txtnuZonaTituloEleitor" runat="server" MaxLength="5" Width="75px"></sgi:TextBox>
        </div>
        <div class="column">
            UF:<br />
            <sgi:DropDownList ID="ddlidUFEmissaoTituloEleitor" runat="server">
            </sgi:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="column w200">
            Certificado de Reservista:<br />
            <sgi:TextBox ID="txtnuCSM" runat="server" MaxLength="15" Width="130"></sgi:TextBox>
        </div>
        <div class="column w150">
            Órgão Emissor:<br />
            <sgi:TextBox ID="txtsgOrgaoExpedidorCSM" runat="server" MaxLength="10" Width="75px"></sgi:TextBox>
        </div>
        <div class="column">
            UF:<br />
            <sgi:DropDownList ID="ddlidUFExpedicaoCSM" runat="server">
            </sgi:DropDownList>
        </div>
    </div>
    <div class="row">
        <div class="column w200">
            CPF:<br />
            <sgi:TextBox ID="txtnuCPF" runat="server" TextBoxType="CPF" CustomErrorMessage="CPF inválido"
                ErrorMessage="<br />O CPF do aluno é obrigatório" SetFocusOnError="true" Enabled="false" Width="130"></sgi:TextBox>
        </div>
        <div class="column">
            CPF Próprio:<br />
            <asp:RadioButton ID="rbCPFProprioSim" runat="server" Checked="True" GroupName="grpCPF"
                Text="Sim" />
            <asp:RadioButton ID="rbCPFProprioNao" runat="server" GroupName="grpCPF" Text="Não" />
        </div>
    </div>
    <div class="cb">
    </div>
</div>
<div class="form-separator">
</div>
<h3>
    Cadastro de Endereço</h3>
<div class="form-in">
    <div class="row">
        Tipo de Endereço:<br />
        <sgi:DropDownList ID="ddlidTipoEndereco" runat="server">
        </sgi:DropDownList>
        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="ddlidTipoEndereco"
            MinimumValue="1" MaximumValue="999999" SetFocusOnError="true" ErrorMessage="<div><div class='validation'>O tipo de endereço é obrigatório.</div></div>"></asp:RangeValidator>
    </div>
    <div class="row">
        <div class="column w200">
            CEP:<br />
            <%--<sgi:TextBox ID="txtnuCEP" runat="server" MaxLength="10" TextBoxType="CEP" onBlur="__doPostBack('txtnuCEP','txtnuCEP_TextChanged')"
                OnTextChanged="txtnuCEP_TextChanged" AutoPostBack="true" RequiredField="true"
                ErrorMessage="<br />O CEP é obrigatório." SetFocusOnError="true"></sgi:TextBox>--%>
            <sgi:TextBox ID="txtnuCEP" runat="server" MaxLength="10" TextBoxType="CEP"
                RequiredField="true" ErrorMessage="<div><div class='validation'>O CEP é obrigatório.</div></div>" 
                SetFocusOnError="true" ontextchanged="txtnuCEP_TextChanged" ></sgi:TextBox>
        </div>
        <div class="column" style="float: left">
            <asp:ImageButton ImageUrl="/Content/Images/busca-cep.gif" runat="server" ID="imageButtonCorreios"
                OnClick="RedirectSiteCorreios_OnClick" Style="width: 94px; height: 37px;" border="0"
                CausesValidation="false" />
        </div>
    </div>
    <div class="row">
        Endereço:<br />
        <sgi:TextBox ID="txtedLogradouro" runat="server" MaxLength="250" Width="255px" Enabled="false"
            EnableViewState="true" />
    </div>
    <div class="row">
        <div class="column w300">
            Bairro:<br />
            <sgi:TextBox ID="txtnmBairro" runat="server" Enabled="false" EnableViewState="true" />
        </div>
        <div class="column w200">
            Localidade:<br />
            <sgi:TextBox ID="txtnmCidade" runat="server" Enabled="false" EnableViewState="true" />
        </div>
        <div class="column">
            UF:<br />
            <sgi:DropDownList ID="ddlidUFEndereco" runat="server" Enabled="False">
            </sgi:DropDownList>
            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddlidUFEndereco"
                SkinID="RangeValidator" MinimumValue="0" MaximumValue="999999" SetFocusOnError="true"
                ErrorMessage="<div><div class='validation'>A UF é obrigatóra.</div></div>"></asp:RangeValidator>
        </div>
    </div>
    <div class="row">
        <div class="column w300">
            Complemento:<br />
            <sgi:TextBox ID="txtedComplemento" runat="server" MaxLength="250" Width="210px"></sgi:TextBox>
        </div>
        <div class="column">
            Nº / Apto:<br />
            <sgi:TextBox ID="txtnuNumero" runat="server" MaxLength="5" Width="50px" RequiredField="true"
                ErrorMessage="<div><div class='validation'>O Nº/Apto é obrigatório.</div></div>" SetFocusOnError="true"></sgi:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="column w300">
            E-Mail Pessoal:<br />
            <sgi:TextBox ID="txtedEmailPessoal" runat="server" Width="233px" MaxLength="70" Size="50"
                TextBoxType="Email" CustomErrorMessage="<div><div class='validation'>Email inválido.</div></div>" RequiredField="true"
                ErrorMessage="<div><div class='validation'>O e-mail do aluno é obrigatório.</div></div>" SetFocusOnError="true"></sgi:TextBox>
        </div>
        <div class="column">
            E-Mail Profissional:<br />
            <sgi:TextBox ID="txtedEmailProfissional" runat="server" Width="233px" MaxLength="70"
                Size="50" TextBoxType="Email" CustomErrorMessage="<div><div class='validation'>E-mail inválido.</div></div>" SetFocusOnError="true" />
        </div>
    </div>
    <div class="row">
                <div class="column w300">
                    Perfil no Facebook:<br />
                    <sgi:TextBox ID="txtPerfilFacebook" Width="250" MaxLength="255" runat="server" />
                </div>
                <div class="column">
                    Perfil do Linkedln:<br />
                    <sgi:TextBox ID="txtPerfilLinkedln" Width="250" MaxLength="255" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="column w300">
                    Endereço do Twitter:<br />
                    <sgi:TextBox ID="txtnmUsuarioTwitter" Width="250"  MaxLength="255" runat="server" />
                </div>
                <div class="column">
                    Outra Rede Social:<br />
                    <sgi:TextBox ID="txtnmOutraRedeSocial" Width="250"  MaxLength="255" runat="server" />
                </div>
            </div>
            <div class="row">
                Endereço Lattes:<br />
                <sgi:TextBox ID="txtedCurriculoLattes" Width="500"  MaxLength="255" runat="server" />
            </div>
    <div class="row">
        <div class="column w300">
            DDD / Telefone Residencial:<br />
            <sgi:TextBox ID="txtnuDDDResidencial" runat="server" TextBoxType="DDD" Size="3" MaxLength="3"></sgi:TextBox>&nbsp;<sgi:TextBox
                ID="txtnuTelefoneResidencial" runat="server" TextBoxType="Phone" /><asp:RequiredFieldValidator
                ID="rfvDDDTelefoneResidencial" runat="server" ControlToValidate="txtnuDDDResidencial" Display="Dynamic" SetFocusOnError="true" ErrorMessage="<div><div class='validation'>O DDD do telefone residencial é obrigatório.</div></div>"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                ID="rfvNuTelefoneResidencial" runat="server" ControlToValidate="txtnuTelefoneResidencial" Display="Dynamic" SetFocusOnError="true" ErrorMessage="<div><div class='validation'>O telefone residencial é obrigatório.</div></div>"></asp:RequiredFieldValidator>
        </div>
        <div class="column w100">
            Ramal:<br />
            &nbsp;<sgi:TextBox ID="txtnuRamal1" runat="server" TextBoxType="Number" Size="4" MaxLength="4" />
        </div>
    </div>
    <div class="row">
        <div class="column w300">
            DDD / Telefone Celular:<br />
            <sgi:TextBox ID="txtnuDDDCelular" runat="server" TextBoxType="DDD" Size="3" MaxLength="3" ></sgi:TextBox>&nbsp;<sgi:TextBox
                ID="txtnuCelular" runat="server" TextBoxType="Phone"></sgi:TextBox><asp:RequiredFieldValidator
                ID="rfvDDDTelefoneCelular" runat="server" ControlToValidate="txtnuDDDCelular" Display="Dynamic" SetFocusOnError="true" ErrorMessage="<div><div class='validation'>O DDD do telefone celular é obrigatório.</div></div>"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                ID="rfvNuTelefoneCelular" runat="server" ControlToValidate="txtnuCelular" Display="Dynamic" SetFocusOnError="true" ErrorMessage="<div><div class='validation'>O telefone celular é obrigatório.</div></div>"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="column w300">
            DDD / Telefone Comercial:<br />
            <sgi:TextBox ID="txtnuDDDComercial" runat="server" TextBoxType="DDD" Size="3" MaxLength="3"></sgi:TextBox>&nbsp;<sgi:TextBox
                ID="txtnuTelefoneComercial" runat="server" TextBoxType="Phone" CustomErrorMessage="<div><div class='validation'>Telefone inválido.</div></div>"></sgi:TextBox>
        </div>
        <div class="column">
            Ramal:<br />
            <sgi:TextBox ID="txtnuRamal2" runat="server" TextBoxType="Number" Size="4" MaxLength="4" />
        </div>
    </div>
    <div class="row">
        <div class="column">
            DDD / Telefone de Emergência:<br />
            <sgi:TextBox ID="txtnuDDDTelefoneEmergencia" runat="server" TextBoxType="DDD" Size="3"
                MaxLength="3"></sgi:TextBox>&nbsp;<sgi:TextBox ID="txtnuTelefoneEmergencia" runat="server"
                    TextBoxType="Phone" SetFocusOnError="true" CustomErrorMessage="<div><div class='validation'>Telefone inválido.</div></div>"></sgi:TextBox>
        </div>
    </div>
    <div class="cb">
    </div>
</div>
<div class="form-separator">
</div>
<h3>
    Informações Gerais</h3>
<div class="form-in">
    <div class="row">
		<div class="column w500">
        Ano de conclusão do ensino médio:<br />
        <sgi:TextBox ID="txtaaConclusaoEnsinoMedio" runat="server" TextBoxType="Year" Size="4"
            MaxLength="4" RequiredField="true" ErrorMessage="<div><div class='validation'>O ano de conclusão do ensino médio é obrigatório.</div></div>"
            SetFocusOnError="true" CustomErrorMessage="<div><div class='validation'>Ano inválido.</div></div><br />"></sgi:TextBox>
        <asp:RangeValidator ID="rvaaConclusaoEnsinoMedio" runat="server" ControlToValidate="txtaaConclusaoEnsinoMedio" Type="Integer"
                SkinID="RangeValidator" MinimumValue="1900" MaximumValue="99999999" SetFocusOnError="true" Display="Dynamic"
                ErrorMessage="<div><div class='validation'>O ano de conclusão do ensino médio deverá ser maior ou igual a 1900.</div></div>"></asp:RangeValidator>
		</div>
	</div>
    <div class="row">
        Canhoto:<br />
        <asp:RadioButton ID="rbicCanhotoSim" runat="server" GroupName="grpicCanhoto" Text="Sim">
        </asp:RadioButton>
        <asp:RadioButton ID="rbicCanhotoNao" runat="server" GroupName="grpicCanhoto" Text="Não"
            Checked="true"></asp:RadioButton>
    </div>
    <div class="row">
        Tipo Sanguíneo e Fator Rh:<br />
        <asp:DropDownList ID="ddlidTipoSanguineo" runat="server" />
    </div>
    <div class="row">
        Necessita de acompanhamento de acessibilidade ou pedagógico em relação a sua aprendizagem?<br />
        <asp:RadioButtonList ID="rblNecessitaAcompanhamento" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="rblNecessitaAcompanhamento_SelectedIndexChanged" RepeatDirection="Horizontal">
            <asp:ListItem Value="true">Sim</asp:ListItem>
            <asp:ListItem Value="false" Selected="True">Não</asp:ListItem>
        </asp:RadioButtonList>
        <div runat="server" id="divTipoNecessidade" visible="False">
            Tipo de necessidades especiais:
            <br />
            <sgi:DropDownList ID="ddlidTipoNecessidadeEspecial" runat="server">
            </sgi:DropDownList>
        </div>
    </div>
    <div class="row">
        <asp:CheckBox ID="chkicReceberEmail" runat="server" Text="O UniCEUB manterá você atualizado(a) sobre o seu curso, eventos, palestras, cursos de extensão, atividades extracurriculares e outras realizações." /><br />
    </div>
    <div class="row">
        <asp:CheckBox ID="ckbicReceberAlertaEspacoAluno" runat="server" Text="Quero que o Espaço Aluno me envie um e-mail quando receber novos eventos, mensagens ou arquivos." />
    </div>
    <div class="cb">
    </div>
</div>
<br />
<sgi:Label ID="lbldtUltimaAtualizacao" runat="server" CssClass="fcr"></sgi:Label>
<hr class="line" />
<sgi:Alert ID="alert" runat="server">
</sgi:Alert>
<br />
<sgi:Button ID="btnConfirm" runat="server" Text="Atualizar" CausesValidation="true"
    OnClick="btnConfirm_Click" />
&nbsp;
<ea:MessageBox ID="messageBox" runat="server" OnConfirmClient="hideModal();return false;" />
<ea:MessageBox ID="messageBoxCorreios" runat="server" OnCancelClientClick="hideModal();return;"
    OnConfirmClient="openPopup('http://www.buscacep.correios.com.br/servicos/dnec/index.do');hideModal();return false;"
    OnConfirmClick="ConfirmRedirectSiteCorreios_OnClick" Type="Question" ButtonOKText="Sim"
    ButtonCancelText="Não"></ea:MessageBox>
<div class="cb">
</div>
<script type="text/javascript">


    $(document).ready(function () {

        $('#<%= txtnuCEP.ClientID %>').blur(function () {
            if (this.value.length != 10 && this.value != "")
                return;
            $.ajax({
                type: "GET",
                url: '<%= Library.ResolveClientUrl("~/Aluno/cep.ashx") %>' + "?" + $(this).val().replace('.', '').replace('-', ''),
                dataType: "text/json",
                cache: false,
                success: function (data) {
                    var json = eval(data);

                    var b = $('#<%= txtnmBairro.ClientID %>');
                    var c = $('#<%= txtnmCidade.ClientID %>');
                    var l = $('#<%= txtedLogradouro.ClientID %>');
                    var u = $('#<%= ddlidUFEndereco.ClientID %>');

                    if (json != undefined) {

                        if (json.length > 0) {

                            b.val(json[0].nmBairro);
                            l.val(json[0].edLogradouro);
                            c.val(json[0].nmCidade);
                            u.val(json[0].idUF);
                            if (json[0].nmBairro.length > 0) return;
                        }
                        else {
                            b.val('');
                            l.val('');
                            c.val('');
                            u.val(0);
                        }
                    }
                    else {
                        b.val('');
                        l.val('');
                        c.val('');
                        u.val(0);
                        return;
                    }

                    __doPostBack('txtnuCEP', 'txtnuCEP_TextChanged');
                }
				});

        });

    });

</script>