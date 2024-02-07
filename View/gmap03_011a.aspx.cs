using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View_gmap03_011a : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["xy03_011a"] != null)
        {
            string[,] map_xy = Session["xy03_011a"] as string[,];
            string aaa = "";
            int a = map_xy.Length;
            for (int i = 0; i < 20000; i++)
            {
                //aaa = aaa + map_xy[i, 0] + "," + map_xy[i, 1] + ";";
                //string a = map_xy[i, 0];
                if (map_xy[i, 0] != null)
                {
                    aaa = aaa + map_xy[i, 0] + "," + map_xy[i, 1] + "," + map_xy[i, 2] + "," + map_xy[i, 3] + "," + map_xy[i, 4] + "," + map_xy[i, 5] + "," + map_xy[i, 6] + "," + map_xy[i, 7] + ";";
                }
            }
            aaa = aaa.Replace(",;", "");
            mapxy.Text = aaa;
        }
    }
}