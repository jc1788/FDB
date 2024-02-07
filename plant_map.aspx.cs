using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plant_map : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["plant_map"] != null)
        {
            mapxy.Text = Session["plant_map"].ToString();
        }
    }
}
