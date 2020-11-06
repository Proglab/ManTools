using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTools2020
{
    public partial class Deco : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Utilisateur"] = null;
            Response.Redirect("default.aspx");
        }
    }
}