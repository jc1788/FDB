using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class occurrence_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	if (Request.QueryString["id"] == null || Request.QueryString["id"] == "")
	{
	    Response.Redirect("occurrence_search.aspx");
	}

	if (!IsPostBack) SetContent();
    }

    private void SetContent()
    {
	    if (Session["role"].ToString() != "1")
	    {
		Response.Redirect("occurrence_search.aspx");
		return;
	    }

	string value_id = Request.QueryString["id"];
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = " Select sid,Chinese_name,short_name,[Group],Density,Way_ch,sec_record,Convert(Varchar(10),[date1],126) AS [date],x,y,inaccuracy,site_city,site_area,site_ch,highway_id,direction,mileage,habit,file1,Collector_ch,plan_name,invest_company,notes From table_1 Where sid = @value_sid";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("value_sid", value_id);
	    SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                    Chinese_name.Text = reader["chinese_name"].ToString();
                    ScientificName.Text = reader["short_name"].ToString();
                    Group.Text = reader["Group"].ToString();
                    Density.Text = reader["Density"].ToString();
                    Way_ch.Text = reader["Way_ch"].ToString();
                    sec_record.Text = reader["sec_record"].ToString();
                    date.Text = reader["date"].ToString();
                    x.Text = reader["x"].ToString();
                    y.Text = reader["y"].ToString();
                    inaccuracy.Text = reader["inaccuracy"].ToString();
                    site_city.Text = reader["site_city"].ToString();
                    site_area.Text = reader["site_area"].ToString();
                    site_ch.Text = reader["site_ch"].ToString();
                    highway_id.SelectedValue = reader["highway_id"].ToString();
                    Direction.SelectedValue = reader["direction"].ToString();
                    mileage.Text = reader["mileage"].ToString();
                    habit.Text = reader["habit"].ToString();
                    Collector_ch.Text = reader["Collector_ch"].ToString();
                    plan_name.Text = reader["plan_name"].ToString();
                    notes.Text = reader["notes"].ToString();

	    	conn.Close();
	    }
	    else
	    {
	    	conn.Close();
		Response.Redirect("searchall.aspx");
	    }
	}
    }

    protected void Edit_Occurrence_Click(object sender, EventArgs e)
    {
	    if (Session["role"].ToString() != "1")
	    {
		Response.Redirect("searchall.aspx");
		return;
	    }

        string value_id = Request.QueryString["id"];
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

        string value_Chinese_name = Chinese_name.Text.Trim();
        string value_Short_name = ScientificName.Text.Trim(); ;
        string value_group = Group.Text.Trim();
        string value_Density = Density.Text.Trim();
        string value_way_ch = Way_ch.Text.Trim();
        string value_sec_record = sec_record.Text.Trim();
        string value_date1 = date.Text.Trim();
        string value_x = x.Text.Trim();
        string value_y = y.Text.Trim();
        string value_inaccuracy = inaccuracy.Text.Trim();
        string value_site_city = site_city.Text.Trim();
        string value_site_area = site_area.Text.Trim();
        string value_site_ch = site_ch.Text.Trim();
        string value_highway_id = highway_id.SelectedValue.Trim();
        string value_direction = Direction.SelectedValue.Trim();
        string value_mileage = mileage.Text.Trim();
        string value_habit = habit.Text.Trim();
        string value_Collector_ch = Collector_ch.Text.Trim();
        string value_plan_name = plan_name.Text.Trim();
        string value_notes = notes.Text.Trim();
        string value_Sensitive_level = "";
        string value_accepted_name_code = "";
        string value_siteid = "";
        string value_TM2_X = "";
        string value_TM2_Y = "";
        string value_full_name = ScientificName.Text.Trim();

        string sqlCommand = " Update table_1 Set ";

        sqlCommand += " Chinese_name = @value_Chinese_name, ";
        sqlCommand += " short_name = @value_Short_name, ";
        sqlCommand += " [Group] = @value_group, ";
        sqlCommand += " Density = @value_Density, ";
        sqlCommand += " Way_ch = @value_way_ch, ";
        sqlCommand += " sec_record = @value_sec_record, ";
        sqlCommand += " [date1] = @value_date1, ";
        sqlCommand += " x = @value_x, ";
        sqlCommand += " y = @value_y, ";
        sqlCommand += " inaccuracy = @value_inaccuracy, ";
        sqlCommand += " site_city = @value_site_city, ";
        sqlCommand += " site_area = @value_site_area, ";
        sqlCommand += " site_ch = @value_site_ch, ";
        sqlCommand += " highway_id = @value_highway_id, ";
        sqlCommand += " direction = @value_direction, ";
        sqlCommand += " mileage = @value_mileage, ";
        sqlCommand += " habit = @value_habit, ";
        sqlCommand += " Collector_ch = @value_Collector_ch, ";
        sqlCommand += " plan_name = @value_plan_name, ";
        sqlCommand += " notes = @value_notes ";
        sqlCommand += " Where sid = @value_sid";

            // 建立 SqlCommand 
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_Chinese_name",value_Chinese_name);
	    cmd.Parameters.AddWithValue("value_Short_name",value_Short_name);
	    cmd.Parameters.AddWithValue("value_group",value_group);
	    cmd.Parameters.AddWithValue("value_Density",value_Density);
	    cmd.Parameters.AddWithValue("value_way_ch",value_way_ch);
	    cmd.Parameters.AddWithValue("value_sec_record",value_sec_record);
	    cmd.Parameters.AddWithValue("value_date1",value_date1);
	    cmd.Parameters.AddWithValue("value_x",value_x);
	    cmd.Parameters.AddWithValue("value_y",value_y);
	    cmd.Parameters.AddWithValue("value_inaccuracy",value_inaccuracy);
	    cmd.Parameters.AddWithValue("value_site_city",value_site_city);
	    cmd.Parameters.AddWithValue("value_site_area",value_site_area);
	    cmd.Parameters.AddWithValue("value_site_ch",value_site_ch);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
	    cmd.Parameters.AddWithValue("value_mileage",value_mileage);
	    cmd.Parameters.AddWithValue("value_habit",value_habit);
	    cmd.Parameters.AddWithValue("value_Collector_ch",value_Collector_ch);
	    cmd.Parameters.AddWithValue("value_plan_name",value_plan_name);
	    cmd.Parameters.AddWithValue("value_notes",value_notes);
	    cmd.Parameters.AddWithValue("value_sid",value_id);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();
	    conn.Close();

            Response.Write("<script>alert('修改資料完成!');window.close(); </script>");
	}
    }
}
