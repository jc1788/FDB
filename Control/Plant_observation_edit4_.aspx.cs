using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Plant_observation_edit4 : System.Web.UI.Page
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


    protected void Edit_Plant_observation_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String value_id = id.Text;
        String value_chinese_name = chinese_name.Text;
        String value_ScientificName = ScientificName.Text;
        String value_high = high.Text;
        String value_width = width.Text;
        String value_m_high = m_high.Text;
        String value_amount = amount.Text;
        String value_highway_id = highway_id.SelectedValue;
        //String value_direction = direction.Text;
        String value_direction = direction.SelectedValue;
        //String value_range = range1.Text + "K＋" + milestone1.Text + "－" + range2.Text + "K＋" + milestone2.Text;
        String value_range = range1.Text + milestone1.Text + "-" + range2.Text + milestone2.Text;
        String value_location = location.SelectedValue;
        String value_pid = pid.Text;
        String value_plant_class = plant_class.Text;
        String value_plant_type = plant_type.Text;
        String value_uid = Session["uid"].ToString();

        //新增欄位方便修改與查詢
        int value_start_mileage1 = int.Parse(range1.Text);
        int value_start_mileage2 = int.Parse(milestone1.Text);
        int value_end_mileage1 = int.Parse(range2.Text);
        int value_end_mileage2 = int.Parse(milestone2.Text);

        //增加query_mileage來抓取X與Y
        int query_mileage1 = (value_start_mileage1 * 1000) + Convert.ToInt16(Math.Round((value_start_mileage2 / 100) + 0.049, 0)) * 100;
        int query_mileage2 = (value_end_mileage1 * 1000) + Convert.ToInt16(Math.Round((value_end_mileage2 / 100) + 0.049, 0)) * 100;

        int query_center = ((value_start_mileage1 + value_end_mileage1) / 2 * 1000) + (Convert.ToInt16(Math.Round(((value_start_mileage2 + value_end_mileage2) / 2 / 100) + 0.049, 0)) * 100);
        
        decimal value_start_x = 0;
        decimal value_start_y = 0;
        decimal value_end_x = 0;
        decimal value_end_y = 0;

        decimal value_center_x = 0;
        decimal value_center_y = 0;

        value_start_x = Get_X(value_highway_id, value_direction, query_mileage1);
        value_start_y = Get_Y(value_highway_id, value_direction, query_mileage1);
        value_end_x = Get_X(value_highway_id, value_direction, query_mileage2);
        value_end_y = Get_Y(value_highway_id, value_direction, query_mileage2);

        value_center_x = Get_X(value_highway_id, value_direction, query_center);
        value_center_y = Get_Y(value_highway_id, value_direction, query_center);

        String sqlCommand = " " ;

        sqlCommand += " Begin Update Plant_observation Set ";

        sqlCommand += " chinese_name = '" + value_chinese_name + "' , ";
        sqlCommand += " ScientificName = '" + value_ScientificName + "' , ";
        sqlCommand += " high = '" + value_high + "' , ";
        sqlCommand += " width = '" + value_width + "' , ";
        sqlCommand += " m_high = '" + value_m_high + "' , ";
        sqlCommand += " amount = '" + value_amount + "' , ";
        sqlCommand += " highway_id = '" + value_highway_id + "' , ";
        sqlCommand += " direction = '" + value_direction + "' , ";
        sqlCommand += " location = '" + value_location + "' , ";
        sqlCommand += " start_mileage1 = '" + value_start_mileage1 + "' , ";
        sqlCommand += " start_mileage2 = '" + value_start_mileage2 + "' , ";
        sqlCommand += " end_mileage1 = '" + value_end_mileage1 + "' , ";
        sqlCommand += " end_mileage2 = '" + value_end_mileage2 + "' , ";
        sqlCommand += " start_x = '" + value_start_x + "' , ";
        sqlCommand += " start_y = '" + value_start_y + "' , ";
        sqlCommand += " end_x = '" + value_end_x + "' , ";
        sqlCommand += " end_y = '" + value_end_y + "' , ";
        sqlCommand += " center_x = '" + value_center_x + "' , ";
        sqlCommand += " center_y = '" + value_center_y + "' , ";
        sqlCommand += " range = '" + value_range + "' , ";
        sqlCommand += " plant_class = '" + value_plant_class + "' , ";
        sqlCommand += " plant_type = '" + value_plant_type + "' , ";
        sqlCommand += " uid = '" + value_uid + "'  ";
        sqlCommand += " Where id = '" + value_id + "' ;";
        sqlCommand += " Insert Into Plant_observation_log (uid , plant_observation_id , update_date) Values ( ";
        sqlCommand += " '" + value_uid + "' , '" + value_id + "' , getdate()) ; End; ";



        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //string url = "Plant_observation_edit3.aspx?pid=" + value_pid;
            Response.Write("<script>alert('修改植栽資料完成!!'); location.href='Plant_observation_edit5_.aspx';</script>");
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
        string value_id = Request.QueryString["id"];
        string value_pid = Request.QueryString["pid"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = '" + value_id + "' ";

        string sql_GetData = " Select id , chinese_name , ScientificName , high , width , m_high , amount , highway_id , direction , location , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , plant_class , plant_type From Plant_observation Where id = '" + value_id + "' ";
        id.Text = value_id;
        pid.Text = value_pid;
        try
        {
            conn.Open();
            cmd = new SqlCommand(sql_GetData, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    chinese_name.Text = reader["chinese_name"].ToString();
                    ScientificName.Text = reader["ScientificName"].ToString();
                    high.Text = reader["high"].ToString();
                    width.Text = reader["width"].ToString();
                    m_high.Text = reader["m_high"].ToString();
                    amount.Text = reader["amount"].ToString();
                    range1.Text = reader["start_mileage1"].ToString();
                    milestone1.Text = reader["start_mileage2"].ToString();
                    range2.Text = reader["end_mileage1"].ToString();
                    milestone2.Text = reader["end_mileage2"].ToString();
                    plant_class.Text = reader["plant_class"].ToString();
                    plant_type.Text = reader["plant_type"].ToString();
                    highway_id.SelectedValue = reader["highway_id"].ToString();
                    direction.SelectedValue = reader["direction"].ToString();
                    location.SelectedValue = reader["location"].ToString();
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
            ||!value_direction.Equals("S")))
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

        //String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage + "' ";
        String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = '" + value_highway_id + "' and direction = '" + sql_direction + "' and mileage = '" + query_mileage + "' ";

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

        //String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage + "' ";
        String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = '" + value_highway_id + "' and direction = '" + sql_direction + "' and mileage = '" + query_mileage + "' ";

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

    //用中文名抓取學名
    protected void Get_NameFull(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_Chinese_name = chinese_name.Text;

        String sqlCommand_NameFull = " SELECT b.name_full , b.accepted_name_code FROM [Common_names] a,  scientific_names b Where a.name_code= b.name_code and a.common_name = '" + value_Chinese_name + "'";

        try
        {
            conn.Open();

            // 建立 SqlCommand 以便從提供的 SiteID 重複。
            cmd = new SqlCommand(sqlCommand_NameFull, conn);

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
