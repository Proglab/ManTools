using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Css.Ast.Selectors;

namespace ManTools2020
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Utilisateur"] == null)
            {
                Menu1.Visible = false;
            }
            else
            {
                Menu1.Visible = true;
            }
        }


        protected void Envoyer_Click(object sender, EventArgs e)
        {
            Session["Utilisateur"] = null;
            Response.Redirect("default.aspx");
        }
    }
}