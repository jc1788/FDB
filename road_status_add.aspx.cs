using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class road_status_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }
    }

    protected void Add_Road_status_Click(object sender, EventArgs e)
    {
        //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        //SqlCommand cmd = new SqlCommand();

        string sqlCommand = "Insert Into Road_status(highway_id,direction,mileage,location,sensitive,status,institution,year,start_date,end_date,owner,file1,path1,mileage1,mileage2,mileage3,mileage4,x,y,doc_no,land1,land2,land3,land4,notes,area,plan_use,maintain1,maintain2,uid) values(";
        string filepath = "/App_Data/Road_status/";
        string filepath2 = "C:\\freeway2\\App_Data\\Road_status\\";

        string value_highway_id = highway_id.SelectedValue;
        string value_direction = direction.SelectedValue;
        string value_maintain1 = maintain1.SelectedValue;
        string value_maintain2 = maintain2.SelectedValue;
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
        string value_file1 = file1.FileName;
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

        sqlCommand += "'" + value_highway_id + "',";
        sqlCommand += "'" + value_direction + "',";
        sqlCommand += "'" + value_mileage + "',";
        sqlCommand += "'" + value_location + "',";
        sqlCommand += "'" + value_sensitive + "',";
        sqlCommand += "'" + value_status + "',";
        sqlCommand += "'" + value_institution + "',";
        sqlCommand += "'" + value_year + "',";
        sqlCommand += "'" + value_start_date + "',";
        sqlCommand += "'" + value_end_date + "',";
        sqlCommand += "'" + value_owner + "',";
        sqlCommand += "'" + value_file1 + "',";
        sqlCommand += "'" + value_path1 + "',";
        sqlCommand += "'" + value_mileage1 + "',";
        sqlCommand += "'" + value_mileage2 + "',";
        sqlCommand += "'" + value_mileage3 + "',";
        sqlCommand += "'" + value_mileage4 + "',";
        //sqlCommand += " (Select Top 1 WGS84X From Freeway Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage + "') ,";
        //sqlCommand += " (Select Top 1 WGS84Y From Freeway Where free_id = '" + value_highway_id + "' and direction = '" + value_direction + "' and mileage = '" + query_mileage + "') ,";
        sqlCommand += "'" + value_x + "',";
        sqlCommand += "'" + value_y + "',";
        sqlCommand += "'" + value_doc_no + "',";
        sqlCommand += "'" + value_land1 + "',";
        sqlCommand += "'" + value_land2 + "',";
        sqlCommand += "'" + value_land3 + "',";
        sqlCommand += "'" + value_land4 + "',";
        sqlCommand += "'" + value_notes + "',";
        sqlCommand += "'" + value_area + "',";
        sqlCommand += "'" + value_plan_use + "',";
        sqlCommand += "'" + value_maintain1 + "',";
        sqlCommand += "'" + value_maintain2 + "',";
        sqlCommand += "'" + value_uid + "')";

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
            

            //上傳附件
            if (file1.HasFile)
            {
                string File_file1 = filepath2 + value_file1;
                file1.SaveAs(File_file1);
            }

            Response.Write("<script>alert('新增路權空間完成!!');location.href='/road_status.aspx';</script>");
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
        String value_direction = direction.SelectedValue;
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
        else {
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

    decimal Get_X ( string value_highway_id , string value_direction , int query_mileage )
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        
        decimal x=0;
        
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
                    x=decimal.Parse(reader["x"].ToString());
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

    decimal Get_Y ( string value_highway_id , string value_direction , int query_mileage )
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
