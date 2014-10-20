using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
//using SGI.UI.Process.Coorporativo;

namespace UI.Web.EC.Coordenador
{
    public partial class AlunoDadosPessoais : UserControl
    {
        #region Events

        public event EventHandler Error;
        public event EventHandler Success;
        public event EventHandler Confirm;
        public event EventHandler Cancel;

        #endregion

       // private IUFProcess _ufProcess;

        public string UrlRedirect { get; set; }

        private const string MENSAGEM_CPF = "CPF obrigatório. Favor entrar em contato com a Secretaria Geral para regularizar sua documentação.";

        protected void Page_Init(object sender, EventArgs e)
        {
         //   _ufProcess = new UFProcess();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindParams();

            if (!IsPostBack)
            {
                BindControl();
            }
        }

        private void BindParams()
        {
            if (Request.QueryString["urlredirect"] != null)
                UrlRedirect = HttpUtility.UrlDecode(Request.QueryString["urlredirect"]);
        }

        public void BindPage()
        {
            if (Utils.CoordenadorNaoLogado())
            {
                Response.Redirect(Utils.PortalUrl);
            }
            //else
            //{
            //    if (SessionMatricula.icUsuarioAcompanhamentoMatricula)
            //    {
            //        messageBox.OnConfirmClient = "hideModal();return false;";
            //        messageBox.Show("A atualização dos dados pessoais só pode ser realizada pelo próprio aluno.", "Acesso Restrito", SGI.UI.WebControls.MessageBoxType.Information);
            //        btnConfirm.Visible = false;
            //    }

            //    var a = new SGI.DataContext.Controller.Academico.Aluno().Bind(SGI.Common.Session.idAluno);

            //    if (a != null)
            //    {
            //        lbldtUltimaAtualizacao.Text = string.Format("Última atualização em {0} às {1}", a.dtUltimaAtualizacao.ToString("dd/MM/yyyy"), a.dtUltimaAtualizacao.ToString("HH:mm:ss"));

            //        txtdeNaturalidade.Text = a.deNaturalidade;
            //        txtdtNascimento.Text = a.dtNascimento.ToString("dd/MM/yyyy");
            //        txtedEmailPessoal.Text = a.edEmailPessoal;
            //        txtedEmailProfissional.Text = a.edEmailProfissional;
            //        txtnmMae.Text = a.nmMae;
            //        txtnmPai.Text = a.nmPai;
            //        txtnmPessoa.Text = a.nmPessoa;
            //        txtnuCPF.Mask = a.nuCPF;

            //        if (string.IsNullOrEmpty(a.nuCPF))
            //        {
            //            messageBox.Show(MENSAGEM_CPF, "Atenção", SGI.UI.WebControls.MessageBoxType.Warning);
            //            alert.Show(MENSAGEM_CPF);
            //            btnConfirm.Visible = false;
            //        }

            //        txtnuCSM.Text = a.nuCSM;
            //        txtnuRG.Text = a.nuRG;
            //        txtnuTituloEleitor.Text = a.nuTituloEleitor;
            //        txtnuZonaTituloEleitor.Text = a.nuZonaTituloEleitor;
            //        txtsgOrgaoExpedidorCSM.Text = a.sgOrgaoExpedidorCSM;
            //        txtsgOrgaoExpedidorRG.Text = a.sgOrgaoExpedidorRG;

            //        if (Library.IsDateNotEmpty(a.dtExpedicaoRG))
            //            txtdtExpedicao.Text = a.dtExpedicaoRG.ToString("dd/MM/yyyy");

            //        var end = new SGI.DataContext.Controller.Coorporativo.Endereco().BindCorrespondenciaByPessoa(a.idPessoa);

            //        if (end != null)
            //        {
            //            if (new CEPProcess().Valido(end.nuCEP))
            //            {
            //                txtedLogradouro.Text = end.edLogradouro;
            //                txtedComplemento.Text = end.edComplemento;
            //                txtnmBairro.Text = end.nmBairro;
            //                txtnmCidade.Text = end.nmCidade;
            //                txtnuCEP.Text = Library.MaskCEP(end.nuCEP);
            //                txtnuNumero.Text = end.nuEndereco;
            //            }

            //            ddlidUFEndereco.SelectedValue = Library.ToString(end.idUF);
            //            ddlidTipoEndereco.SelectedValue = Library.ToString(end.idTipoEndereco);

            //            // "Endereço": Telefone Residencial (Telefone1)
            //            if (Library.ToLong(end.nuDDD) > 0)
            //                txtnuDDDResidencial.Text = end.nuDDD;
            //            if (Library.ToLong(Library.ClearMask(end.nuTelefone1)) > 0)
            //                txtnuTelefoneResidencial.Text = Library.FormatString(end.nuTelefone1, TypeString.Telephone);
            //            if (Library.ToLong(end.nuRamal1) > 0)
            //                txtnuRamal1.Text = end.nuRamal1;
            //            // "Endereço": Telefone Comercial (Telefone2)
            //            if (Library.ToLong(end.nuDDD2) > 0)
            //                txtnuDDDComercial.Text = end.nuDDD2.ToString();
            //            if (Library.ToLong(Library.ClearMask(end.nuTelefone2)) > 0)
            //                txtnuTelefoneComercial.Text = Library.FormatString(end.nuTelefone2, TypeString.Telephone);
            //            if (Library.ToLong(end.nuRamal2) > 0)
            //                txtnuRamal2.Text = end.nuRamal2;
            //        }


            //        ddlidEstadoCivil.SelectedValue = Library.ToString(a.idEstadoCivil);
            //        ddlidPais.SelectedValue = Library.ToString(a.idNacionalidade);
            //        ddlidUFEmissaoTituloEleitor.SelectedValue = Library.ToString(a.idUFEmissaoTituloEleitor);
            //        ddlidUFExpedicaoCSM.SelectedValue = Library.ToString(a.idUFExpedicaoCSM);
            //        ddlidUFExpedicaoRG.SelectedValue = Library.ToString(a.idUFExpedicaoRG);
            //        ddlidUFNascimento.SelectedValue = Library.ToString(a.idUFNascimento);


            //        // "Pessoa": Telefone Celular (TelefoneCelular)
            //        if (Library.ToLong(a.nuDDDTelefoneCelular) > 0)
            //            txtnuDDDCelular.Text = a.nuDDDTelefoneCelular;
            //        if (Library.ToLong(Library.ClearMask(a.nuTelefoneCelular)) > 0)
            //            txtnuCelular.Text = Library.FormatString(a.nuTelefoneCelular, TypeString.Telephone);
            //        // "Pessoa": Telefone Emergencia (TelefoneEmergencia)
            //        if (Library.ToLong(a.nuDDDTelefoneEmergencia) > 0)
            //            txtnuDDDTelefoneEmergencia.Text = a.nuDDDTelefoneEmergencia;
            //        if (Library.ToLong(Library.ClearMask(a.nuTelefoneEmergencia)) > 0)
            //            txtnuTelefoneEmergencia.Text = Library.FormatString(a.nuTelefoneEmergencia, TypeString.Telephone);

            //        txtaaConclusaoEnsinoMedio.Text = a.aaConclusaoEnsinoMedio.ToString();

            //        rbCPFProprioSim.Checked = (a.icCPFProprio == "S");
            //        rbCPFProprioNao.Checked = !(a.icCPFProprio == "S");

            //        rbMasculino.Checked = (a.coSexo == "M");
            //        rbFeminino.Checked = !(a.coSexo == "M");

            //        rbicCanhotoSim.Checked = (a.icCanhoto == "S");
            //        rbicCanhotoNao.Checked = !(a.icCanhoto == "S");

            //        ckbicReceberAlertaEspacoAluno.Checked = a.icReceberAlertaEmailEspacoAluno;
            //        chkicReceberEmail.Checked = a.icReceberEmail;

            //        txtPerfilFacebook.Text = a.nmPerfilFacebook;
            //        txtPerfilLinkedln.Text = a.nmPerfilLinkedIn;
            //        txtnmUsuarioTwitter.Text = a.nmUsuarioTwitter;
            //        txtnmOutraRedeSocial.Text = a.nmOutraRedeSocial;
            //        txtedCurriculoLattes.Text = a.edCurriculoLattes;

            //        //chkicDeficienciaAuditiva.Checked = (a.icDeficienciaAuditiva == "S" ? true : false);
            //        //chkicDeficienciaFisica.Checked = (a.icDeficienciaFisica == "S" ? true : false);
            //        //chkicDeficienciaMental.Checked = (a.icDeficienciaMental == "S" ? true : false);
            //        //chkicDeficienciaMultipla.Checked = (a.icDeficienciaMultipla == "S" ? true : false);
            //        //chkicDeficienciaVisual.Checked = (a.icDeficienciaVisual == "S" ? true : false);

            //        var oPneList = new SGI.DataContext.Controller.Coorporativo.PessoaNecessidadeEspecial().BindListByPessoa((int)a.idPessoa);
            //        if (oPneList.Count > 0)
            //        {
            //            rblNecessitaAcompanhamento.ClearSelection();
            //            rblNecessitaAcompanhamento.Items[0].Selected = true;
            //            rblNecessitaAcompanhamento_SelectedIndexChanged(new object(), EventArgs.Empty);
            //            SGI.BusinessObject.Coorporativo.PessoaNecessidadeEspecial oPne = oPneList[0];
            //            SGI.Common.Library.SelectDropDownList(ddlidTipoNecessidadeEspecial, oPne.idTipoNecessidadeEspecial.ToString());
            //        }

            //        SGI.Common.Library.BindDropDownList(ddlidMunicipioINEPNascimento, new SGI.DataContext.Controller.Coorporativo.MunicipioINEP().GetList(a.idUFNascimento.ToInt32()), "idMunicipioINEP", "nmMunicipio");
            //        ddlidMunicipioINEPNascimento.SelectedValue = a.idMunicipioINEPNascimento.ToString();

            //        if (!a.deTipoSanguineoRh.IsNull())
            //        {
            //            ListItem itm = ddlidTipoSanguineo.Items.FindByText(a.deTipoSanguineoRh);
            //            if (itm != null)
            //                itm.Selected = true;
            //        }
            //    }
            //    else
            //        ddlidMunicipioINEPNascimento.Enabled = false;

            //    HabilitarCampos(false);
            //}
        }

        protected void BindDropDownList()
        {
            // DataTable dttUF = new Controller.Coorporativo.UF().GetList();

            //Library.BindDropDownList(ddlidEstadoCivil, new SGI.DataContext.Controller.Coorporativo.EstadoCivil().GetList(), "idEstadoCivil", "deEstadoCivil");
            //Library.BindDropDownList(ddlidPais, new SGI.DataContext.Controller.Coorporativo.Pais().GetList(), "idPais", "nmPais");

            ////Library.BindDropDownList(ddlidUFNascimento, dttUF, "idUF", "sgUF");
            //ddlidUFNascimento.DataBind(_ufProcess.GetList(), "idUF", "sgUF", "Selecione", 0);

            //Library.BindDropDownList(ddlidMunicipioINEPNascimento, new SGI.DataContext.Controller.Coorporativo.MunicipioINEP().GetList(Library.ToInteger(ddlidUFNascimento.SelectedValue)), "idMunicipioINEP", "nmMunicipio", true);

            ////Library.BindDropDownList(ddlidUFExpedicaoRG, dttUF, "idUF", "sgUF");
            //ddlidUFExpedicaoRG.DataBind(_ufProcess.GetList(), "idUF", "sgUF", "Selecione", 0);

            ////Library.BindDropDownList(ddlidUFEmissaoTituloEleitor, dttUF, "idUF", "sgUF");
            //ddlidUFEmissaoTituloEleitor.DataBind(_ufProcess.GetList(), "idUF", "sgUF", "Selecione", 0);

            ////Library.BindDropDownList(ddlidUFExpedicaoCSM, dttUF, "idUF", "sgUF");
            //ddlidUFExpedicaoCSM.DataBind(_ufProcess.GetList(), "idUF", "sgUF", "Selecione", 0);

            ////Library.BindDropDownList(ddlidUFEndereco, dttUF, "idUF", "sgUF");
            //ddlidUFEndereco.DataBind(_ufProcess.GetList(), "idUF", "sgUF", "Selecione", 0);

            //Library.BindDropDownList(ddlidTipoEndereco, new SGI.DataContext.Controller.Coorporativo.TipoEndereco().GetList(), "idTipoEndereco", "deTipoEndereco");
            //Library.BindDropDownList(ddlidTipoNecessidadeEspecial, new SGI.DataContext.Controller.Coorporativo.TipoNecessidadeEspecial().GetList(), "idTipoNecessidadeEspecial", "deTipoNecessidadeEspecial");

            //Library.BindDropDownList(ddlidTipoSanguineo, new SGI.DataContext.Controller.Coorporativo.TipoSanguineo().GetList(), "idTipoSanguineo", "deTipoSanguineo");

        }

        protected void rblNecessitaAcompanhamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlidTipoNecessidadeEspecial.ClearSelection();
            //divTipoNecessidade.Visible = SGI.Common.Library.ToBoolean(rblNecessitaAcompanhamento.SelectedValue);
            //Page.ClientScript.RegisterClientScriptBlock(GetType(), "rblNecessitaAcompanhamento_SelectedIndexChanged", "document.location.hash='divTipoNecessidade';", true);
            //rblNecessitaAcompanhamento.Focus();
        }

        protected void ddlidUFNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SGI.Common.Library.BindDropDownList(ddlidMunicipioINEPNascimento, new SGI.DataContext.Controller.Coorporativo.MunicipioINEP().GetList(ddlidUFNascimento.SelectedValue.ToInt32()), "idMunicipioINEP", "nmMunicipio", true);

            //if (ddlidUFNascimento.SelectedValue.ToInt32() > 0)
            //{
            //    if (ddlidUFNascimento.SelectedItem.Text == "EX")
            //    {
            //        ddlidPais.SelectedIndex = 0;
            //        ddlidMunicipioINEPNascimento.SelectedValue = "5565"; // Codigo correspondente ao "Exterior"
            //    }
            //    else
            //        ddlidPais.SelectedValue = "39"; // Código correspondente ao "Brasil"                
            //}
        }

        protected void txtnuCEP_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Library.ClearMask(txtnuCEP.Text)))
            //{
            //    txtnmBairro.Text =
            //        txtedLogradouro.Text =
            //            txtnmCidade.Text =
            //                txtnuCEP.Text =
            //                    txtedComplemento.Text =
            //                        txtnuNumero.Text = string.Empty;

            //    ddlidUFEndereco.SelectedValue = "0";
            //    //messageBox.Show("O CEP informado não foi encontrado, favor insira um CEP válido!", "Atenção!");
            //}
            //else
            //{
            //    var searchCep = new SGI.DataContext.Controller.Coorporativo.CEP().BindByCEP(Library.ClearMask(txtnuCEP.Text));

            //    if (searchCep.IsNull())
            //    {
            //        txtnmBairro.Text =
            //            txtedLogradouro.Text =
            //                txtnmCidade.Text =
            //                    txtnuCEP.Text = string.Empty;

            //        ddlidUFEndereco.SelectedValue = "0";

            //        /*if (messageBox.Visible == false)*/
            //        messageBox.Show("O CEP informado não foi encontrado, insira um CEP válido!", "Atenção");
            //    }
            //    else
            //    {
            //        txtnmBairro.Text = searchCep.nmBairro.ToString().Trim();
            //        txtedLogradouro.Text = searchCep.edLogradouro.ToString().Trim();
            //        txtnmCidade.Text = searchCep.nmCidade.ToString().Trim();
            //        ddlidUFEndereco.SelectedValue = searchCep.idUF.ToString();
            //    }
            //}
            //txtnuCEP.Focus();
        }

        protected void RedirectSiteCorreios_OnClick(object sender, ImageClickEventArgs e)
        {
           // messageBoxCorreios.Show("Você está sendo direcionado para um site pertencente aos Correios, externo ao UniCEUB. Deseja continuar?", "Confirmação", SGI.UI.WebControls.MessageBoxType.Question, "Sim", "Não");
        }

        protected void ConfirmRedirectSiteCorreios_OnClick(object sender, EventArgs e)
        {
            Library.OpenWindowCenter("http://www.buscacep.correios.com.br/servicos/dnec/index.do", this.Page);
        }

        protected void verificaExistenciaCEP()
        {
            //var searchCepLocalizado = new SGI.DataContext.Controller.Coorporativo.CEP().BindByCEP(txtnuCEP.NoMaskText);

            //if (searchCepLocalizado.IsNull())
            //{
            //    txtnmBairro.Text = txtedLogradouro.Text = txtnmCidade.Text = txtnuCEP.Text = txtedComplemento.Text = txtnuNumero.Text = string.Empty;
            //    ddlidUFEndereco.SelectedValue = "0";
            //}
            //else
            //{
            //    txtnmBairro.Text = searchCepLocalizado.nmBairro.ToString().Trim();
            //    txtedLogradouro.Text = searchCepLocalizado.edLogradouro.ToString().Trim();
            //    txtnmCidade.Text = searchCepLocalizado.nmCidade.ToString().Trim();

            //    //var oUF = new SGI.DataContext.Controller.Coorporativo.UF().Get(searchCepLocalizado.idUF);
            //    var oUF = _ufProcess.Get(searchCepLocalizado.idUF);
            //    ddlidUFEndereco.SelectedValue = oUF.idUF.ToString();
            //}
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //if (Library.ToInteger(txtnuCEP.NoMaskText) == 0)
            //{
            //    txtnuCEP.Focus();
            //    alert.Show("911");
            //    return;
            //}

            //if (Confirm != null)
            //    Confirm.Invoke(this, new ObjectEventArgs(sender));

            //var o = new SGI.DataContext.Controller.Academico.Aluno().Bind(SessionAluno.idAluno);

            //o.SetProperties(new SGI.DataContext.Controller.Coorporativo.Pessoa().Bind(o.idPessoa).ToDataRow());

            //if (o != null)
            //{
            //    #region Dados do Aluno
            //    o.deNaturalidade = txtdeNaturalidade.Text.Trim();
            //    o.dtNascimento = txtdtNascimento.Date;
            //    o.edEmailPessoal = txtedEmailPessoal.Text.Trim().ToLower();
            //    o.edEmailProfissional = txtedEmailProfissional.Text.Trim().ToLower();
            //    o.nmMae = txtnmMae.Text.Trim();
            //    o.nmPai = txtnmPai.Text.Trim();
            //    o.nmPessoa = txtnmPessoa.Text.Trim();
            //    o.nuCPF = Library.ClearMask(txtnuCPF.Text);
            //    o.nuCSM = txtnuCSM.Text.Trim();
            //    o.nuDDDTelefoneCelular = txtnuDDDResidencial.Text.Trim();
            //    o.coSexo = (rbMasculino.Checked ? "M" : "F");

            //    o.nuRG = txtnuRG.Text.Trim();
            //    o.nuTituloEleitor = txtnuTituloEleitor.Text.Trim();
            //    o.nuZonaTituloEleitor = txtnuZonaTituloEleitor.Text.Trim();
            //    o.sgOrgaoExpedidorCSM = txtsgOrgaoExpedidorCSM.Text.Trim();
            //    o.sgOrgaoExpedidorRG = txtsgOrgaoExpedidorRG.Text.Trim();

            //    o.idEstadoCivil = Library.ToInteger(ddlidEstadoCivil.SelectedValue);
            //    o.idNacionalidade = Library.ToInteger(ddlidPais.SelectedValue);
            //    o.idUFEmissaoTituloEleitor = Library.ToInteger(ddlidUFEmissaoTituloEleitor.SelectedValue);
            //    o.idUFExpedicaoCSM = Library.ToInteger(ddlidUFExpedicaoCSM.SelectedValue);
            //    o.idUFExpedicaoRG = Library.ToInteger(ddlidUFExpedicaoRG.SelectedValue);
            //    o.dtExpedicaoRG = txtdtExpedicao.Date;
            //    o.idUFNascimento = Library.ToInteger(ddlidUFNascimento.SelectedValue);
            //    o.icCPFProprio = (rbCPFProprioSim.Checked ? "S" : "N");
            //    o.icCanhoto = (rbicCanhotoSim.Checked ? "S" : "N");

            //    o.icDeficienciaAuditiva = "N";
            //    o.icDeficienciaFisica = "N";
            //    o.icDeficienciaMental = "N";
            //    o.icDeficienciaMultipla = "N";
            //    o.icDeficienciaVisual = "N";

            //    o.icReceberEmail = chkicReceberEmail.Checked;
            //    o.icReceberAlertaEmailEspacoAluno = ckbicReceberAlertaEspacoAluno.Checked;

            //    o.nmUsuarioTwitter = txtnmUsuarioTwitter.Text;
            //    o.nmPerfilFacebook = txtPerfilFacebook.Text;
            //    o.nmPerfilLinkedIn = txtPerfilLinkedln.Text;
            //    o.edCurriculoLattes = txtedCurriculoLattes.Text;
            //    o.nmOutraRedeSocial = txtnmOutraRedeSocial.Text;

            //    #endregion

            //    #region Endereço

            //    verificaExistenciaCEP();

            //    o.edLogradouro = txtedLogradouro.Text;
            //    o.nmBairro = txtnmBairro.Text;
            //    o.nmCidade = txtnmCidade.Text;
            //    o.edComplemento = txtedComplemento.Text;
            //    o.nuCEP = txtnuCEP.NoMaskText;
            //    o.idUFEndereco = Library.ToInteger(ddlidUFEndereco.SelectedValue);
            //    o.idTipoEndereco = Library.ToInteger(ddlidTipoEndereco.SelectedValue);
            //    o.idMunicipioINEPNascimento = ddlidMunicipioINEPNascimento.SelectedValue.ToInt32();
            //    o.nuEndereco = txtnuNumero.Text;
            //    #endregion Endereço

            //    #region Telefone
            //    //Residencial
            //    o.nuDDD = txtnuDDDResidencial.Text;
            //    o.nuTelefone1 = Library.ClearMask(txtnuTelefoneResidencial.Text);
            //    o.nuRamal1 = txtnuRamal1.Text;
            //    //Comercial
            //    o.nuDDD2 = txtnuDDDComercial.Text;
            //    o.nuTelefone2 = Library.ClearMask(txtnuTelefoneComercial.Text);
            //    o.nuRamal2 = txtnuRamal2.Text;
            //    //Celular
            //    o.nuDDDTelefoneCelular = txtnuDDDCelular.Text;
            //    o.nuTelefoneCelular = Library.ClearMask(txtnuCelular.Text);
            //    //Emergencia
            //    o.nuDDDTelefoneEmergencia = txtnuDDDTelefoneEmergencia.Text;
            //    o.nuTelefoneEmergencia = Library.ClearMask(txtnuTelefoneEmergencia.Text);
            //    #endregion Telefone

            //    o.deTipoSanguineoRh = ddlidTipoSanguineo.SelectedItem.Text.Replace("Selecione", "");

            //    AlertList alertList = new AlertList();
            //    if (o.edEmailPessoal.Trim().Length > 0)
            //        if (!Library.ValidEmail(o.edEmailPessoal))
            //            alertList.Add("447");
            //    if (o.edEmailProfissional.Trim().Length > 0)
            //        if (!Library.ValidEmail(o.edEmailProfissional))
            //            alertList.Add("448");

            //    #region Validação Endereço

            //    if (string.IsNullOrEmpty(o.edLogradouro))
            //    {
            //        txtedLogradouro.Focus();
            //        alert.Show("134");
            //        return;
            //    }
            //    else if (string.IsNullOrEmpty(o.nmBairro))
            //    {
            //        txtnmBairro.Focus();
            //        alert.Show("012");
            //        return;
            //    }
            //    else if (string.IsNullOrEmpty(o.nmCidade))
            //    {
            //        txtnmCidade.Focus();
            //        alert.Show("013");
            //        return;
            //    }
            //    else if (o.idUFEndereco == 0)
            //    {
            //        ddlidUFEndereco.Focus();
            //        alert.Show("014");
            //        return;
            //    }
            //    else if (string.IsNullOrEmpty(o.nuCEP))
            //    {
            //        txtnuCEP.Focus();
            //        alert.Show("015");
            //        return;
            //    }
            //    #endregion

            //    if (!Library.IsDateNotEmpty(o.dtExpedicaoRG)) alertList.Add("008");

            //    if (!Library.ValidCPF(o.nuCPF)) alertList.Add("387");

            //    #region Informações Gerais

            //    o.aaConclusaoEnsinoMedio = txtaaConclusaoEnsinoMedio.Text.ToInt32();
            //    if (o.aaConclusaoEnsinoMedio < 1900 || o.aaConclusaoEnsinoMedio > DateTime.Now.Year) alertList.Add("405");

            //    #endregion Informações Gerais


            //    if (alertList.HasAlert())
            //    {
            //        if (Error != null)
            //            Error.Invoke(alertList, new ObjectEventArgs(sender));
            //        else
            //            messageBox.Show(alertList[0].GetDescription(), "Erro", SGI.UI.WebControls.MessageBoxType.Error);

            //        return;
            //    }

            //    o.icReceberAlertaEmailEspacoAluno = ckbicReceberAlertaEspacoAluno.Checked;
            //    o.idTipoNecessidadeEspecial = ddlidTipoNecessidadeEspecial.SelectedValue.ToInt32();

            //    var returnObject = new SGI.UI.Process.Academico.AlunoProcess().UpdateDadosPessoaisEspacoAluno(o);

            //    if (returnObject.AlertList.HasAlert())
            //    {
            //        messageBox.Show(returnObject.AlertList[0].Key, "Erro", SGI.UI.WebControls.MessageBoxType.Error);
            //        return;
            //    }

            //    SessionAluno.SetDescadastramentoAlerta(ckbicReceberAlertaEspacoAluno.Checked);

            //    if (Success != null)
            //        Success.Invoke(o, new ObjectEventArgs(sender));
            //    else
            //        messageBox.Show("823", "Informação", SGI.UI.WebControls.MessageBoxType.Information);

            //    lbldtUltimaAtualizacao.Text = string.Format("Última atualização em {0} as {1}", o.dtUltimaAtualizacao.ToString("dd/MM/yyyy"), o.dtUltimaAtualizacao.ToString("HH:mm:ss"));

            //    if (!string.IsNullOrEmpty(UrlRedirect))
            //        Response.Redirect(UrlRedirect, false);
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Cancel != null)
                Cancel.Invoke(this, new ObjectEventArgs(sender));
        }

        public override void BindControl()
        {
            BindDropDownList();
            BindPage();
        }

        public void HabilitarCampos(bool habilitar)
        {
            //txtnmPessoa.Enabled =
            //    rbFeminino.Enabled = rbMasculino.Enabled =
            //        ddlidEstadoCivil.Enabled =
            //            txtnmPai.Enabled =
            //                txtnmMae.Enabled =
            //                    txtdtNascimento.Enabled =
            //                        txtdeNaturalidade.Enabled =
            //                            ddlidUFNascimento.Enabled =
            //                                ddlidMunicipioINEPNascimento.Enabled =
            //                                    ddlidPais.Enabled =
            //                                        txtnuRG.Enabled =
            //                                            txtsgOrgaoExpedidorRG.Enabled =
            //                                                ddlidUFExpedicaoRG.Enabled =
            //                                                    txtnuTituloEleitor.Enabled =
            //                                                        txtnuZonaTituloEleitor.Enabled =
            //                                                            ddlidUFEmissaoTituloEleitor.Enabled =
            //                                                                txtnuCSM.Enabled =
            //                                                                    txtsgOrgaoExpedidorCSM.Enabled =
            //                                                                        ddlidUFExpedicaoCSM.Enabled =
            //                                                                            txtnuCPF.Enabled =
            //                                                                                rbCPFProprioSim.Enabled = rbCPFProprioNao.Enabled = habilitar;
        }
    }
}