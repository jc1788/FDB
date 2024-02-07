using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class occurrence_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	SetContent();
    }

    private void SetContent()
    {
	    if (Session["role"].ToString() != "1")
	    {
		Response.Write("<script>window.close();</script>");
		return;
	    }
    }

    protected void Add_Occurrence_Click(object sender, EventArgs e)
    {
	    if (Session["role"].ToString() != "1")
	    {
		Response.Write("<script>window.close();</script>");
		return;
	    }

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

       string sqlCommand = "Insert Into table_1(Chinese_name,short_name,[group],Density,way_ch,sec_record,date1,x,y,inaccuracy,site_city,site_area,site_ch,highway_id,direction,mileage,habit,Collector_ch,plan_name,Sensitive_level,accepted_name_code,notes,siteid,TM2_X,TM2_Y,full_name) Values(";

        sqlCommand += "@value_Chinese_name,";
        sqlCommand += "@value_Short_name,";
        sqlCommand += "@value_group,";
        sqlCommand += "@value_Density,";
        sqlCommand += "@value_way_ch,";
        sqlCommand += "@value_sec_record,";
        sqlCommand += "@value_date1,";
        sqlCommand += "@value_x,";
        sqlCommand += "@value_y,";
        sqlCommand += "@value_inaccuracy,";
        sqlCommand += "@value_site_city,";
        sqlCommand += "@value_site_area,";
        sqlCommand += "@value_site_ch,";
        sqlCommand += "@value_highway_id,";
        sqlCommand += "@value_direction,";
        sqlCommand += "@value_mileage,";
        sqlCommand += "@value_habit,";
        sqlCommand += "@value_Collector_ch,";
        sqlCommand += "@value_plan_name,";
        sqlCommand += "@value_Sensitive_level,";
        sqlCommand += "@value_accepted_name_code,";
        sqlCommand += "@value_notes,";
        sqlCommand += "@value_siteid,";
        sqlCommand += "@value_TM2_X,";
        sqlCommand += "@value_TM2_Y,";
        sqlCommand += "@value_full_name)";

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
		cmd.Parameters.AddWithValue("value_Sensitive_level",value_Sensitive_level);
		cmd.Parameters.AddWithValue("value_accepted_name_code",value_accepted_name_code);
		cmd.Parameters.AddWithValue("value_notes",value_notes);
		cmd.Parameters.AddWithValue("value_siteid",value_siteid);
		cmd.Parameters.AddWithValue("value_TM2_X",value_TM2_X);
		cmd.Parameters.AddWithValue("value_TM2_Y",value_TM2_Y);
		cmd.Parameters.AddWithValue("value_full_name",value_full_name);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();
	    conn.Close();

            Response.Write("<script>alert('新增資料完成!');window.close(); </script>");
	}
    }
}
