using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using WebMatrix.WebData;

public partial class front : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
	this.cb1.InputAttributes["class"] = "form-check-input";

        if (WebSecurity.IsAuthenticated)
        {
            luser.Text = "您好，" + WebSecurity.CurrentUserName + "　";
        }

        string role = Session["role"].ToString();
        if (!role.Equals("1"))
        {
	    admin.Visible = false;
	}

	if (!IsPostBack)
	{
	    SetContent();
	    SetContent2(sender,e);
	}
    }

    private void SetContent()
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataTable dt_highway = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM [Highway]", conn);
	    apt.Fill(dt_highway);
	    highwayid.DataSource = dt_highway;
	    highwayid.DataBind();

	    if (Session["highwayid"] != null)
		highwayid.SelectedValue = Session["highwayid"].ToString();

	    conn.Close();
	}
    }

    private void SetContent2(object sender, EventArgs e)
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataTable dt_part = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT 0 AS id , 'ALL' AS  class1 UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY id", conn);
	    apt.Fill(dt_part);
	    DropDownList4.DataSource = dt_part;
	    DropDownList4.DataBind();

	    if (Session["value_class1"] != null && Session["value_class1"] != "")
	    {
		DropDownList4.SelectedValue = Session["value_class1"].ToString();
		DropDownList4_SelectedIndexChanged(sender, e);
		if (DropDownList4.SelectedValue != "ALL"
		&& Session["value_class2"] != null && Session["value_class2"] != "")
		DropDownList5.SelectedValue = Session["value_class2"].ToString();
	    }

	    conn.Close();
	}
    }

    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    if(DropDownList4.SelectedValue != "ALL")
	    {
	    DataTable dt_site = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT 'ALL' as class2, 0 as position UNION ALL (SELECT DISTINCT class2, position FROM [Engineering_road] Where (class1=@class1)) order by position", conn);
	    apt.SelectCommand.Parameters.Add("class1", DropDownList4.SelectedValue);
	    apt.Fill(dt_site);
	    DropDownList5.DataSource = dt_site;
	    DropDownList5.DataBind();
	    Session["value_class1"] = DropDownList4.SelectedValue;
	    }
	    else
	    {
	    DropDownList5.Items.Clear();
	    DropDownList5.Items.Add(new ListItem("ALL","ALL"));
	    Session["value_class2"] = "ALL";
	    }

	    conn.Close();
	}
    }
}
