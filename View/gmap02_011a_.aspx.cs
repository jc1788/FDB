using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_gmap02_011a : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["xy02_011a"] != null)
        {
            string[,] map_xy = Session["xy02_011a"] as string[,];
            string aaa = "";
            for (int i = 0; i < 10000; i++)
            {
                if (map_xy[i, 0] == null & map_xy[i, 1] == null)
                {
                    break;
                }
                aaa = aaa + map_xy[i, 0] + "," + map_xy[i, 1] + "," + map_xy[i, 6] + "," + map_xy[i, 7] + "," + map_xy[i, 5] + ";";
            }
            aaa = aaa.Replace(",;", ";");
            mapxy.Text = aaa;
        }
    }
}