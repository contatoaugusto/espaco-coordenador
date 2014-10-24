using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;

namespace UI.Web.EC.Paginas
{
    public partial class Prova : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CarregarAmc();
        }


        private void CarregarAmc()
        {

            foreach (var item in NQuestão.ConsultarAmc())   
            {
                ddlAmc.Items.Add(new ListItem(item.SEMESTRE + "º sem/" + item.ANO, item.ID_AMC.ToString()));
            }
        }

        protected void ddlAmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlDataSource_Questoes.SelectParameters["ID_AMC"].DefaultValue = ddlAmc.SelectedValue.ToString();
        }


        protected void btnGerarProva_Click(object sender, EventArgs e)
        {
            // Selecionar as questões dessa AMC e do curso do usuario logado (professor ou coordenador)
            NQuestão.ConsultarQuestao
        }
    }
}