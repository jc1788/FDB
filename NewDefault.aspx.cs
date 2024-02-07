using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

public partial class NewDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (WebSecurity.IsAuthenticated)
        {
            labuser.Text = "您好，" + WebSecurity.CurrentUserName + "!";
        }
        if (val1.Equals(false))
        {
            Response.Redirect("Account/logout.cshtml");
        }
    }
    protected void searchdata_Click(object sender, EventArgs e)
    {
        string value_searchtxt = searchtxt.Text.Trim();
        string value_s1 = (s1.Checked) ? "1" : "0";
        string value_s2 = (s2.Checked) ? "1" : "0";
        string value_s3 = (s3.Checked) ? "1" : "0";
        string value_highwayid = highwayid.SelectedValue.ToString();
        string value_start1 = start1.Text.Trim();
        string value_start2 = start2.Text.Trim();
        string value_end1 = end1.Text.Trim();
        string value_end2 = end2.Text.Trim();
        string value_year1 = year1.Text.Trim();
        string value_month1 = month1.SelectedValue.ToString();
        string value_year2 = year2.Text.Trim();
        string value_month2 = month2.SelectedValue.ToString();


        Response.Redirect("/control/CrossDatabaseSearch_.aspx?searchtxt=" + value_searchtxt + "&s1=" + value_s1 + "&s2=" + value_s2 + "&s3=" + value_s3 + "&highwayid=" + value_highwayid + "&start1=" + value_start1 + "&start2=" + value_start2 + "&end1=" + value_end1 + "&end2=" + value_end2 + "&year1=" + value_year1 + "&month1=" + value_month1 + "&year2=" + value_year2 + "&month2=" + value_month2);
    }
}