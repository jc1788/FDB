using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;


public partial class road_status : System.Web.UI.Page
{
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

        //Query();
    }

    protected void Query()
    {
        string sql = " SELECT [id], [plan_name], [plan_id], [type], CONVERT (char(10), [start_date], 126) AS [start_date], CONVERT (char(10), [end_date], 126) AS [end_date], [owner], [provider], [location], [keywords], [abstracts] FROM [Eco_report] ";
        string value_query = TextBox1.Text.ToString();

        if (!value_query.Equals(""))
        {
            sql += " Where plan_name like '%' + @value_query + '%'";
            sql += " or plan_id like '%' + @value_query + '%'";
            sql += " or type like '%' + @value_query + '%'";
            sql += " or start_date like '%' + @value_query + '%'";
            sql += " or end_date like '%' + @value_query + '%'";
            sql += " or owner like '%' + @value_query + '%'";
            sql += " or provider like '%' + @value_query + '%'";
            sql += " or location like '%' + @value_query + '%'";
            sql += " or keywords like '%' + @value_query + '%'";
            sql += " or abstracts like '%' + @value_query + '%'";
        }

        SqlDataSource1.SelectCommand = sql.ToString();
	SqlDataSource1.SelectParameters.Add("value_query",value_query);
        gv_road_status.DataBind();
    }

    public bool road_status_editable(string id)
    {
	bool editable = false;
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = "Select id From road_status Where id = @id";
	    if (Session["role"].ToString() != "1")
		sql += " and uid = @uid";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("id", id);
	    cmd.Parameters.AddWithValue("uid", Session["uid"]);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
		editable = true;

	    conn.Close();
	}
	return editable;
    }

    protected void flexCheck_Changed(object sender, EventArgs e)
    {
	if (flexCheck.Checked)
	{
	    this.gv_road_status.Columns[16].Visible = true;
	    this.gv_road_status.Columns[17].Visible = true;
	}
	else
	{
	    this.gv_road_status.Columns[16].Visible = false;
	    this.gv_road_status.Columns[17].Visible = false;
	}
    }

    protected void gv_road_status_RowCommand(object sender, GridViewCommandEventArgs e)
    {
	if (e.CommandName == "Delete")
	{
	    string id = e.CommandArgument.ToString();
	    string sql = "Delete road_status WHERE [id] = @rid";

	    this.SqlDataSource1.DeleteCommand = sql;
	    this.SqlDataSource1.DeleteParameters.Add("rid", id);
	}
    }
}
