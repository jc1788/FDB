using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;


public partial class plant_observation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }
    }

    public bool plant_observation_editable(string pid)
    {
	bool editable = false;
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = "Select pid From plant_observation Where id = @pid";
	    if (Session["role"].ToString() != "1")
		sql += " and uid = @uid";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("pid", pid);
	    cmd.Parameters.AddWithValue("uid", Session["uid"]);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
		editable = true;

	    conn.Close();
	}
	return editable;
    }

    protected void gv_plant_observation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
	if (e.CommandName == "Delete")
	{
	    string pid = e.CommandArgument.ToString();
	    string sql = "Delete Plant_observation WHERE [id] = @pid";

	    this.SqlDataSource1.DeleteCommand = sql;
	    this.SqlDataSource1.DeleteParameters.Add("pid", pid);
	}
    }
}
