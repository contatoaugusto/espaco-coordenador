using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarRepresentanteTurma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (IsPostBack)
                    return;

               CarregarRepresentanteTurma();
       
            }
        }

        private void CarregarRepresentanteTurma()
        {
            grdRepresentanteTurma.DataSource = NRepresentanteTurma.Consultar();
            grdRepresentanteTurma.DataBind();

        }

        protected void btNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("RepresentanteTurma.aspx", true);
        }

        
        protected void grdItens_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRepresentanteTurma.PageIndex = e.NewPageIndex;
            CarregarRepresentanteTurma();
        }
    }
}