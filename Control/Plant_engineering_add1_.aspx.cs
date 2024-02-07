using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Control_Plant_engineering_add1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
    }
    protected void Add_Plant_engineering_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand = "Insert Into Plant_engineering(plant_id,number,plant_name,institution,design_date,work_date,maintain,file1,path1,uid) values(";
        String filepath = "/Attachments/Plant_engineering/";
        String filepath2 = "D:\\freeway2\\Attachments\\Plant_engineering\\";
        
        //String sqlCommand_SiteId = "Select Count(siteid) cnts From Roadkill_Site Where siteid=";
        
        String value_plant_id = plant_id.Text;
        String value_number = number.Text;
        String value_plant_name = plant_name.Text;
        String value_institution = institution.Text;
        //String value_design_date = design_date.SelectedDate.ToString("yyyy-MM-dd");
        String value_design_date = design_date.Text;
        //String value_work_date = work_date.SelectedDate.ToString("yyyy-MM-dd");
        String value_work_date = work_date.Text;
        String value_maintain = DropDownList1.SelectedValue + DropDownList2.SelectedValue;
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        String value_uid = Session["uid"].ToString();

        //String Url="Occurrence_add1.aspx?siteid="+value_siteid;

        Boolean NoData = false;
        if (value_plant_id.Equals(""))
        {
            NoData = true;
            Response.Write("<script>alert('編號請輸入資料')</script>");
        }


        

        sqlCommand += "'" + value_plant_id + "',";
        sqlCommand += "'" + value_number + "',";
        sqlCommand += "'" + value_plant_name + "',";
        sqlCommand += "'" + value_institution + "',";
        sqlCommand += "'" + value_design_date + "',";
        sqlCommand += "'" + value_work_date + "',";
        sqlCommand += "'" + value_maintain + "',";
        sqlCommand += "'" + value_file1 + "',";
        sqlCommand += "'" + value_path1 + "',";
        sqlCommand += "'" + value_uid + "')";

        //siteid有輸入
        if (!NoData)
        {
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

                if (file1.HasFile)
                {
                    String File_file1 = filepath2 + value_file1;
                    file1.SaveAs(File_file1);
                }
                Get_Plant_Id(sender,e);
                /*
                number.Enabled = false;
                plant_name.Enabled = false;
                institution.Enabled = false;
                check_ok.Width = 20;
                check_ok.Height = 20;
                imgCalendar1.Width = 0;
                imgCalendar1.Height = 0;
                imgCalendar2.Width = 0;
                imgCalendar2.Height = 0;
                Table2.Enabled = true;
                Table2.Visible = true;
                Add_Plant_engineering.Enabled = false;
                */
                Response.Write("<script>alert('新增植栽工程資料完成!!')</script>");
            }
            catch (Exception ex)
            {
                // 在這裡新增錯誤處理方式以便進行偵錯。
                // 不應將此錯誤訊息傳回給呼叫者。
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
        }
    }

    
    protected void Add_Plant_observation_Click(object sender, EventArgs e)
    {
        
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String sqlCommand = "Insert Into Plant_observation(chinese_name,ScientificName,high,width,m_high,amount,highway_id,direction,range,location,pid,start_mileage1,start_mileage2,end_mileage1,end_mileage2,start_x,start_y,end_x,end_y,uid) Values(";
        String value_chinese_name = Chinese_name.Text;
        String value_ScientificName = ScientificName.Text;
        String value_high = high.Text;
        String value_width = width.Text;
        String value_m_high = m_high.Text;
        String value_amount = amount.Text;
        String value_highway_id = highway_id.SelectedValue;
        String value_direction = direction.SelectedValue;
        String value_range = range1.Text + "K＋" + milestone1.Text + "－" + range2.Text + "K＋" + milestone2.Text;
        String value_location = location.Text;
        String value_pid = pid.Text;
        
        String value_uid = Session["uid"].ToString();
        Boolean NoData = false;

        //新增欄位方便修改與查詢
        int value_start_mileage1 = int.Parse(range1.Text);
        int value_start_mileage2 = int.Parse(milestone1.Text);
        int value_end_mileage1 = int.Parse(range2.Text);
        int value_end_mileage2 = int.Parse(milestone2.Text);

        //增加query_mileage來抓取X與Y
        int query_mileage1 = (value_start_mileage1 * 1000) + Convert.ToInt16(Math.Round((value_start_mileage2 / 100) + 0.049, 0)) * 100;
        int query_mileage2 = (value_end_mileage1 * 1000) + Convert.ToInt16(Math.Round((value_end_mileage2 / 100) + 0.049, 0)) * 100;


        decimal value_start_x = 0;
        decimal value_start_y = 0;
        decimal value_end_x = 0;
        decimal value_end_y = 0;

        value_start_x = Get_X(value_highway_id, value_direction, query_mileage1);
        value_start_y = Get_Y(value_highway_id, value_direction, query_mileage1);
        value_end_x = Get_X(value_highway_id, value_direction, query_mileage2);
        value_end_y = Get_Y(value_highway_id, value_direction, query_mileage2);


        sqlCommand += "'" + value_chinese_name + "',";
        sqlCommand += "'" + value_ScientificName + "',";
        sqlCommand += "'" + value_high + "',";
        sqlCommand += "'" + value_width + "',";
        sqlCommand += "'" + value_m_high + "',";
        sqlCommand += "'" + value_amount + "',";
        sqlCommand += "'" + value_highway_id + "',";
        sqlCommand += "'" + value_direction + "',";
        sqlCommand += "'" + value_range + "',";
        sqlCommand += "'" + value_location + "',";
        sqlCommand += "'" + value_pid + "',";
        sqlCommand += "'" + value_start_mileage1 + "',";
        sqlCommand += "'" + value_start_mileage2 + "',";
        sqlCommand += "'" + value_end_mileage1 + "',";
        sqlCommand += "'" + value_end_mileage2 + "',";
        sqlCommand += "'" + value_start_x + "',";
        sqlCommand += "'" + value_start_y + "',";
        sqlCommand += "'" + value_end_x + "',";
        sqlCommand += "'" + value_end_y + "',";

        //sqlCommand += " (Select Top 1 WGS84X From Freeway Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage1 + "') ,";
        //sqlCommand += " (Select Top 1 WGS84Y From Freeway Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage1 + "') ,";
        //sqlCommand += " (Select Top 1 WGS84X From Freeway Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage2 + "') ,";
        //sqlCommand += " (Select Top 1 WGS84Y From Freeway Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage2 + "') ,";
        sqlCommand += "'" + value_uid + "')";

        if (value_pid.Equals(""))
        {
            NoData = true;
            Response.Write("<script>alert('編號請輸入資料')</script>");
        }
        
        if (!NoData)
        {
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

                Response.Write("<script>alert('新增植栽工程記錄完成!!');location.href='/newdefault.aspx';</script>");
            }
            catch (Exception ex)
            {
                // 在這裡新增錯誤處理方式以便進行偵錯。
                // 不應將此錯誤訊息傳回給呼叫者。
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
        }
    }

    //判斷plant_id存在否
    protected void Get_Plant_Id(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_plant_id = plant_id.Text;
        String sqlCommand_NameFull = " SELECT pid , number , plant_name , institution , CONVERT (char(10), design_date, 20) design_date , CONVERT (char(10), work_date, 20) work_date , maintain FROM Plant_engineering Where plant_id = '" + value_plant_id + "'";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    pid.Text = reader["pid"].ToString();
                    number.Text = reader["number"].ToString();
                    number.Enabled = false;
                    plant_name.Text = reader["plant_name"].ToString();
                    plant_name.Enabled = false;
                    institution.Text = reader["institution"].ToString();
                    institution.Enabled = false;
                    design_date.Text = reader["design_date"].ToString();
                    work_date.Text = reader["work_date"].ToString();
                    check_ok.Width = 0;
                    check_ok.Height = 0;
                    imgCalendar1.Width = 0;
                    imgCalendar1.Height = 0;
                    imgCalendar2.Width = 0;
                    imgCalendar2.Height = 0;
                    Table2.Enabled = true;
                    Table2.Visible = true;
                    Add_Plant_engineering.Enabled = false;
                }
            }
            else
            {
                pid.Text = "";
                number.Text = "";
                number.Enabled = true;
                plant_name.Text = "";
                plant_name.Enabled = true;
                institution.Text = "";
                institution.Enabled = true;
                design_date.Text = "";
                work_date.Text = "";
                check_ok.Width = 20;
                check_ok.Height = 20;
                imgCalendar1.Width = 20;
                imgCalendar1.Height = 20;
                imgCalendar2.Width = 20;
                imgCalendar2.Height = 20;
                Table2.Enabled = false;
                Table2.Visible = false;
                Add_Plant_engineering.Enabled = true;
            }
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    //用中文名抓取學名
    protected void Get_NameFull(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_Chinese_name = Chinese_name.Text;

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
    decimal Get_X(string value_highway_id, string value_direction, int query_mileage)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
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
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
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