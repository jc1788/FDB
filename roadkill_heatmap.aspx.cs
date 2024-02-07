using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class roadkill_heatmap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Session["roadkill_heatmap"] != null)
        {
/*            string[,] hmap_lxy = Session["roadkill_heatmap"] as string[,];
            string aaa = "";
	for (int i = 0; i < 1000; i++)
	   aaa += hmap_lxy[i,0] + "," + hmap_lxy[i,1] + "," + hmap_lxy[i,2] + "," + hmap_lxy[i,3] + "," + hmap_lxy[i,4] + ";";
	aaa = aaa.Replace(",,,,;","");
            hmapxy.Text = aaa;
*/	    hmapxy.Text = Session["roadkill_heatmap"].ToString();
        }
    }
}