using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_gmap03_013 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["xy03_013"] != null)
        {
            string[,] map_xy = Session["xy03_013"] as string[,];
            string aaa = "";
            for (int i = 0; i < 10000; i++)
            {
                aaa = aaa + map_xy[i, 0] + "," + map_xy[i, 1] + ";";
            }
            aaa = aaa.Replace(",;", "");
            mapxy.Text = aaa;
        }
    }
}