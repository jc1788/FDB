using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class occurrence_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	if (Request.QueryString["sid"] == null || Request.QueryString["sid"] == "")
	{
	    Response.Redirect("searchall.aspx");
	}

	if (!IsPostBack) SetContent();
    }

    private void SetContent()
    {
        string value_siteid = Request.QueryString[0].ToString();
        string value_Short_Name = Request.QueryString[1].ToString();
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = " Select TOP 1 sid, siteid, x , y , inaccuracy , TM2_X , TM2_Y , CONVERT (char(10), date1, 126) AS date , site_ch , highway_id , Chinese_name , Short_Name as ScientificName , Density , Collector_ch , Way_ch , [Group] , sec_record , habit From table_1 where (siteid = @sid or sid = @sid) and Short_Name = @Short_Name";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("sid", value_siteid);
	    cmd.Parameters.AddWithValue("Short_Name", value_Short_Name);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
	    {
		Chinese_name.Text = reader["Chinese_name"].ToString();
		ScientificName.Text = reader["ScientificName"].ToString();
		Density.Text = reader["Density"].ToString();
		Collector_ch.Text = reader["Collector_ch"].ToString();
		Way_ch.Text = reader["Way_ch"].ToString();
		date.Text = reader["date"].ToString();
		site_ch.Text = reader["site_ch"].ToString();
		highway_id.Text = reader["highway_id"].ToString();
		x.Text = reader["x"].ToString();
		y.Text = reader["y"].ToString();
		TM2_X.Text = reader["TM2_X"].ToString();
		TM2_Y.Text = reader["TM2_Y"].ToString();
		inaccuracy.Text = reader["inaccuracy"].ToString();
		Group.Text = reader["Group"].ToString();
		sec_record.Text = reader["sec_record"].ToString();
		habit.Text = reader["habit"].ToString();
		siteid.Text = reader["siteid"].ToString();
	    	conn.Close();
	    }
	    else
	    {
	    	conn.Close();
		Response.Redirect("searchall.aspx");
	    }
	}
    }
}
