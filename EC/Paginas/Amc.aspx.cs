using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
            }
         }

        public void BindControl()
        {
            BindDropDownList();
            //BindPage();
        }


        protected void BindDropDownList()
        {

            //Semestre
            ddlSemestre.Items.Add(new ListItem("1", "1"));
            ddlSemestre.Items.Add(new ListItem("2", "2"));

            // Preenche ano
            int anoAtual = DateTime.Now.Year;
            for (int i = anoAtual; i <= anoAtual + 2; i++)
            {
                ddlAno.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }

        protected void Salvar_Click1(object sender, EventArgs e)
        {
            AMC amc = new AMC();
            amc.SEMESTRE = ddlSemestre.SelectedValue.ToInt32();
            amc.ANO = ddlAno.SelectedValue.ToInt32();
            amc.DATA_APLICACAO = Library.ToDate(dtAplicacao.Text);

            if (NAmc.Salvar(amc))
                alert.Show("Operação efetuada com sucesso!");
            else
            {
                alert.Show("Não foi possível criar uma nova AMC");
                return;
            }
            
        }
    }
}