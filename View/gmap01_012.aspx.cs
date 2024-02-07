using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_gmap01_012 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["xy01_012"] != null)
        {
            string[,] map_xy = Session["xy01_012"] as string[,];
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