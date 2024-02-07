using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Occurrence_edit4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        if (!IsPostBack)
        {
            Get_Data();
        }
    }


    protected void Edit_Occurrence_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String value_sid = sid.Text;
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
/*
        float i = 0;
        bool ToF = float.TryParse(mileage.Text.Trim(), out i);
        if (ToF)
        {
            value_mileage = i * 1000 + "";
        }
*/
        string value_habit = habit.Text.Trim();
        string value_file1 = file1.Text.Trim();
        string value_Collector_ch = Collector_ch.Text.Trim();
        string value_plan_name = plan_name.Text.Trim();
        string value_invest_company = invest_company.Text.Trim();
        string value_notes = notes.Text.Trim();
        string value_Sensitive_level = "";
        string value_accepted_name_code = "";
        string value_siteid = "";
        string value_TM2_X = "";
        string value_TM2_Y = "";
        string value_full_name = ScientificName.Text.Trim();

        String sqlCommand = " ";

        sqlCommand += " Update table_1 Set ";

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
        sqlCommand += " file1 = @value_file1, ";
        sqlCommand += " Collector_ch = @value_Collector_ch, ";
        sqlCommand += " plan_name = @value_plan_name, ";
        sqlCommand += " invest_company = @value_invest_company, ";
        sqlCommand += " notes = @value_notes ";
        sqlCommand += " Where sid = @value_sid";
        

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
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
	    cmd.Parameters.AddWithValue("value_file1",value_file1);
	    cmd.Parameters.AddWithValue("value_Collector_ch",value_Collector_ch);
	    cmd.Parameters.AddWithValue("value_plan_name",value_plan_name);
	    cmd.Parameters.AddWithValue("value_invest_company",value_invest_company);
	    cmd.Parameters.AddWithValue("value_notes",value_notes);
	    cmd.Parameters.AddWithValue("value_sid",value_sid);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //string url = "Plant_observation_edit3.aspx?pid=" + value_pid;
            Response.Write("<script>alert('修改生態資料完成!!')</script>");
            Response.Write("<script>window.opener=null;window.close();</script>");
            //Response.Redirect(url);
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
    }

    protected void Get_Data()
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_sid = Request.QueryString["sid"];
        
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = @value_id";

        string sql_GetData = " Select sid,Chinese_name,short_name,[Group],Density,Way_ch,sec_record,Convert(Varchar(10),[date1],126) AS [date],x,y,inaccuracy,site_city,site_area,site_ch,highway_id,direction,mileage,habit,file1,Collector_ch,plan_name,invest_company,notes From table_1 Where sid = @value_sid  ";

        sid.Text = value_sid;
        
        try
        {
            conn.Open();
            cmd = new SqlCommand(sql_GetData, conn);
	    cmd.Parameters.AddWithValue("value_sid",value_sid);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sid.Text = reader["sid"].ToString();
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
                    file1.Text = reader["file1"].ToString();
                    Collector_ch.Text = reader["Collector_ch"].ToString();
                    plan_name.Text = reader["plan_name"].ToString();
                    invest_company.Text = reader["invest_company"].ToString();
                    notes.Text = reader["notes"].ToString();
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


    decimal Get_X(string value_highway_id, string value_direction, int query_mileage)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String sql_direction = "";
        if ((value_highway_id.Equals("1")
            || value_highway_id.Equals("3")
            || value_highway_id.Equals("5")
            || value_highway_id.Equals("7")
            || value_highway_id.Equals("9")
            || value_highway_id.Equals("3甲"))
            && (!value_direction.Equals("N")
            || !value_direction.Equals("S")))
        {
            sql_direction = "N";
        }

        if ((value_highway_id.Equals("2")
            || value_highway_id.Equals("4")
            || value_highway_id.Equals("6")
            || value_highway_id.Equals("8")
            || value_highway_id.Equals("10"))
            && (!value_direction.Equals("E")
            || !value_direction.Equals("W")))
        {
            sql_direction = "E";
        }

        decimal x = 0;

        //String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage";
        String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = @value_highway_id and direction = @sql_direction and mileage = @query_mileage";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("sql_direction",sql_direction);
	    cmd.Parameters.AddWithValue("query_mileage",query_mileage);
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
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sql_direction = "";
        if ((value_highway_id.Equals("1")
            || value_highway_id.Equals("3")
            || value_highway_id.Equals("5")
            || value_highway_id.Equals("7")
            || value_highway_id.Equals("9")
            || value_highway_id.Equals("3甲"))
            && (!value_direction.Equals("N")
            || !value_direction.Equals("S")))
        {
            sql_direction = "N";
        }

        if ((value_highway_id.Equals("2")
            || value_highway_id.Equals("4")
            || value_highway_id.Equals("6")
            || value_highway_id.Equals("8")
            || value_highway_id.Equals("10"))
            && (!value_direction.Equals("E")
            || !value_direction.Equals("W")))
        {
            sql_direction = "E";
        }

        decimal y = 0;

        //String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage";
        String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = @value_highway_id and direction = @sql_direction and mileage = @query_mileage";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("sql_direction",sql_direction);
	    cmd.Parameters.AddWithValue("query_mileage",query_mileage);
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

    //用中文名抓取學名
    protected void Get_NameFull(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_Chinese_name = Chinese_name.Text;

        String sqlCommand_NameFull = " SELECT b.name_full , b.accepted_name_code FROM [Common_names] a,  scientific_names b Where a.name_code= b.name_code and a.common_name = @value_Chinese_name ";

        try
        {
            conn.Open();

            // 建立 SqlCommand 以便從提供的 SiteID 重複。
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_Chinese_name",value_Chinese_name);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //String sid = reader[0].ToString;
                if (!reader[0].Equals(0))
                {
                    ScientificName.Text = reader["name_full"].ToString();
                    accepted_name_code.Text = reader["accepted_name_code"].ToString();
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
}