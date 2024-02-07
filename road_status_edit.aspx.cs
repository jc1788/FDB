using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class road_status_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

        if (!IsPostBack)
        {
            Get_Data();
        }
    }

    protected void Get_Data()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_id = Request.QueryString["id"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = '" + value_id + "' ";

        string sql_GetData = " Select highway_id , direction , mileage , location , sensitive , status , institution , year ,  CONVERT(CHAR(10), start_date, 120) AS start_date ,  CONVERT(CHAR(10), end_date, 120) AS end_date , owner , mileage1 , mileage2 , mileage3 , mileage4 , x , y , doc_no , land1 , land2 , land3 , land4 , notes , area , plan_use , maintain1 , maintain2  From Road_status Where id = '" + value_id + "' ";
        id.Text = value_id;

        try
        {
            conn.Open();

	    string sql = "Select * From road_status Where id = @value_id";
	    if (Session["role"].ToString() != "1")
		sql += " and uid = @uid";
	    SqlCommand cmd1 = new SqlCommand(sql, conn);
	    cmd1.Parameters.AddWithValue("value_id", value_id);
	    cmd1.Parameters.AddWithValue("uid", Session["uid"]);
	    SqlDataReader reader1 = cmd1.ExecuteReader();

	    if (!reader1.HasRows)
	    {
		conn.Close();
		Response.Redirect("road_status.aspx");
	    }
	    reader1.Close();

            cmd = new SqlCommand(sql_GetData, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    highway_id.Text = reader["highway_id"].ToString();
                    direction.Text = reader["direction"].ToString();
                    //mileage.Text = reader["mileage"].ToString();
                    location.Text = reader["location"].ToString();
                    sensitive.Text = reader["sensitive"].ToString();
                    status.Text = reader["status"].ToString();
                    institution.Text = reader["institution"].ToString();
                    year.Text = reader["year"].ToString();
                    start_date.Text = reader["start_date"].ToString();
                    end_date.Text = reader["end_date"].ToString();
                    owner.Text = reader["owner"].ToString();
                    range.Text = reader["mileage1"].ToString();
                    milestone.Text = reader["mileage2"].ToString();
                    range2.Text = reader["mileage3"].ToString();
                    milestone2.Text = reader["mileage4"].ToString();
                    //x.Text = reader["x"].ToString();
                    //y.Text = reader["y"].ToString();
                    doc_no.Text = reader["doc_no"].ToString();
                    land1.Text = reader["land1"].ToString();
                    land2.Text = reader["land2"].ToString();
                    land3.Text = reader["land3"].ToString();
                    land4.Text = reader["land4"].ToString();
                    area.Text = reader["area"].ToString();
                    notes.Text = reader["notes"].ToString();
                    plan_use.Text = reader["plan_use"].ToString();
                    maintain1.Text = reader["maintain1"].ToString();
                    maintain2.Text = reader["maintain2"].ToString();
                }
            }
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    protected void Edit_Road_status_Click(object sender, EventArgs e)
    {
        //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        //SqlCommand cmd = new SqlCommand();

        
        string filepath = "/App_Data/Road_status/";
        //string filepath2 = "C:\\freeway2\\App_Data\\Road_status\\";
        string value_id = id.Text;
        string value_highway_id = highway_id.SelectedValue;
        string value_direction = direction.Text;
        string value_maintain1 = maintain1.Text;
        string value_maintain2 = maintain2.Text;
        //里程暫時組成起訖公里
        string value_mileage = range.Text + "k+" + milestone.Text + "-" + range2.Text + "k+" + milestone2.Text;
        string value_range = range.Text;
        string value_milestone = milestone.Text;
        string value_range2 = range2.Text;
        string value_milestone2 = milestone2.Text;
        string value_location = location.Text;
        string value_sensitive = sensitive.Text;
        string value_status = status.Text;
        string value_institution = institution.Text;
        string value_year = year.Text;
        //String value_start_date = start_date.SelectedDate.ToString("yyyy-MM-dd");
        //String value_end_date = end_date.SelectedDate.ToString("yyyy-MM-dd");
        string value_start_date = start_date.Text;
        string value_end_date = end_date.Text;

        string value_owner = owner.Text;
        
        string value_path1 = filepath;

        string value_doc_no = doc_no.Text;
        string value_land1 = land1.Text;
        string value_land2 = land2.Text;
        string value_land3 = land3.Text;
        string value_land4 = land4.Text;
        string value_notes = notes.Text;
        string value_area = area.Text;
        string value_plan_use = plan_use.Text;

        //新增欄位方便修改與查詢
        int value_mileage1 = int.Parse(range.Text);
        int value_mileage2 = int.Parse(milestone.Text);
        int value_mileage3 = int.Parse(range2.Text);
        int value_mileage4 = int.Parse(milestone2.Text);
        string value_uid = Session["uid"].ToString();

        //增加query_mileage來抓取X與Y
        int query_mileage = (int.Parse(value_range) * 1000) + Convert.ToInt16(Math.Round((double.Parse(value_milestone) / 100) + 0.049, 0)) * 100;

        decimal value_x = 0;
        decimal value_y = 0;

        value_x = Get_X(value_highway_id, value_direction, query_mileage);
        value_y = Get_Y(value_highway_id, value_direction, query_mileage);

        string sqlCommand = "Update Road_status Set ";

        sqlCommand += " highway_id = '" + value_highway_id + "', ";
        sqlCommand += " direction = '" + value_direction + "', ";
        sqlCommand += " mileage = '" + value_mileage + "',";
        sqlCommand += " location = '" + value_location + "',";
        sqlCommand += " sensitive = '" + value_sensitive + "',";
        sqlCommand += " status = '" + value_status + "',";
        sqlCommand += " institution = '" + value_institution + "',";
        sqlCommand += " year = '" + value_year + "',";
        sqlCommand += " start_date = '" + value_start_date + "',";
        sqlCommand += " end_date = '" + value_end_date + "',";
        sqlCommand += " owner = '" + value_owner + "',";
        sqlCommand += " mileage1 = '" + value_mileage1 + "',";
        sqlCommand += " mileage2 = '" + value_mileage2 + "',";
        sqlCommand += " mileage3 = '" + value_mileage3 + "',";
        sqlCommand += " mileage4 = '" + value_mileage4 + "',";
        sqlCommand += " x = '" + value_x + "',";
        sqlCommand += " y = '" + value_y + "',";
        sqlCommand += " doc_no = '" + value_doc_no + "',";
        sqlCommand += " land1 = '" + value_land1 + "',";
        sqlCommand += " land2 = '" + value_land2 + "',";
        sqlCommand += " land3 = '" + value_land3 + "',";
        sqlCommand += " land4 = '" + value_land4 + "',";
        sqlCommand += " notes = '" + value_notes + "',";
        sqlCommand += " area = '" + value_area + "',";
        sqlCommand += " plan_use = '" + value_plan_use + "',";
        sqlCommand += " maintain1 = '" + value_maintain1 + "',";
        sqlCommand += " maintain2 = '" + value_maintain2 + "',";
        sqlCommand += " uid = '" + value_uid + "'";
        sqlCommand += " Where id = '" + value_id + "'";
        //Response.Write(sqlCommand);
        //Response.Write("<script>alert('"+sqlCommand+"')</script>");

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
            conn.Open();

            // 建立 SqlCommand 
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            /*
            //上傳附件
            if (file1.HasFile)
            {
                string File_file1 = filepath2 + value_file1;
                file1.SaveAs(File_file1);
            }
            */
            Response.Write("<script>alert('修改路權空間使用情形完成!!')</script>");
            Response.Redirect("Road_status.aspx");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
    }

    //判斷敏感等級存在否
    protected void Get_Sensitive_level(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_free_id = highway_id.SelectedValue;
        String value_direction = direction.Text;
        String value_range = range.Text;
        String value_milestone = milestone.Text;
        //改為計算mileage
        int value_mileage = (int.Parse(value_range) * 1000) + Convert.ToInt16(Math.Round((double.Parse(value_milestone) / 100) + 0.049, 0)) * 100;

        String value_other = value_range + "K";
        //判斷是否有輸入-號
        int i = value_milestone.IndexOf("-");
        if (i == 0)
        {
            value_other = value_other + value_milestone;
        }
        else
        {
            value_other = value_other + "+" + value_milestone;
        }

        //String sqlCommand = " SELECT Sensitive_level FROM Freeway Where free_id = '" + value_free_id + "' and direction ='" + value_direction + "' and other = '" + value_other + "' ";
        String sqlCommand = " SELECT Sensitive_level FROM Freeway_new Where free_id = '" + value_free_id + "' and direction ='" + value_direction + "' and mileage = '" + value_mileage + "' ";
        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sensitive.Text = reader["Sensitive_level"].ToString();
                }
            }
            else
            {
                sensitive.Text = "無";
            }
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }


    decimal Get_X(string value_highway_id, string value_direction, int query_mileage)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        decimal x = 0;

        String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage + "' ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    x = decimal.Parse(reader["x"].ToString());
                }
            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        return x;
    }

    decimal Get_Y(string value_highway_id, string value_direction, int query_mileage)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        decimal y = 0;

        String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage + "' ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    y = decimal.Parse(reader["y"].ToString());
                }
            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        return y;
    }
}
