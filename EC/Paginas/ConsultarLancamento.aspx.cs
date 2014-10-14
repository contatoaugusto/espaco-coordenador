using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Dado;
using EC.Negocio;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarLancamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Lancamento.aspx", true);
        }
    }
}