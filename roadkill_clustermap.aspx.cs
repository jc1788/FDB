using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class roadkill_clustermap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["roadkill_clustermap"] != null)
        {
/*            string[,] map_xy = Session["roadkill_clustermap"] as string[,];
            string aaa = "";
            for (int i = 0; i < 50000; i++)
            {
                aaa = aaa + map_xy[i, 0] + "," + map_xy[i, 1] + "," + map_xy[i, 2] + ";";
            }
            aaa = aaa.Replace(",,;", "");
            mapxy.Text = aaa;
*/	    mapxy.Text = Session["roadkill_clustermap"].ToString();
        }
    }
}
