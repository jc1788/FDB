using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Site_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
    }
    
    //用中文名抓取學名
    protected void Get_NameFull(object sender, EventArgs e) 
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_Chinese_name = Chinese_name.Text;

        String sqlCommand_NameFull = " SELECT b.name_full , b.accepted_name_code FROM [Common_names] a,  scientific_names b Where a.name_code= b.name_code and a.common_name = @value_Chinese_name";

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
    
    //判斷siteid存在否
    protected void Get_SiteId(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_siteid = siteid.Text;
        String sqlCommand_NameFull = " SELECT siteid , x , y , inaccuracy , TM2_X , TM2_Y , CONVERT (char(10), date, 20) date , site_en , site_ch , highway_id , note FROM site Where siteid = @value_siteid ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_siteid",value_siteid);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    x.Text = reader["x"].ToString();
                    x.Enabled = false;
                    y.Text = reader["y"].ToString();
                    y.Enabled = false;
                    inaccuracy.Text = reader["inaccuracy"].ToString();
                    inaccuracy.Enabled = false;
                    TM2_X.Text = reader["TM2_X"].ToString();
                    TM2_X.Enabled = false;
                    TM2_Y.Text = reader["TM2_Y"].ToString();
                    TM2_Y.Enabled = false;
                    date.Text = reader["date"].ToString();
                    site_en.Text = reader["site_en"].ToString();
                    site_en.Enabled = false;
                    site_ch.Text = reader["site_ch"].ToString();
                    site_ch.Enabled = false;
                    note.Text = reader["note"].ToString();
                    note.Enabled = false;
                    check_ok.Width = 0;
                    check_ok.Height = 0;
                    imgCalendar1.Width = 0;
                    imgCalendar1.Height = 0;
                    //Table2.Enabled = true;
                    //Table2.Visible = true;
                    Add_Site.Enabled = false;
                }
            }
            else 
            {
                x.Text = "0.0";
                x.Enabled = true;
                y.Text = "0.0";
                y.Enabled = true;
                inaccuracy.Text = "0";
                inaccuracy.Enabled = true;
                TM2_X.Text = "0";
                TM2_X.Enabled = true;
                TM2_Y.Text = "0";
                TM2_Y.Enabled = true;
                site_en.Text = "";
                site_en.Enabled = true;
                site_ch.Text = "";
                site_ch.Enabled = true;
                note.Text = "";
                note.Enabled = true;
                check_ok.Width = 20;
                check_ok.Height = 20;
                imgCalendar1.Width = 20;
                imgCalendar1.Height = 20;
                //Table2.Enabled = false;
                //Table2.Visible = false;
                Add_Site.Enabled = true;
            }
        }
        
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    //新增生態調查
    protected void Add_Site_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        string sqlCommand = "Insert Into table_1(Chinese_name,short_name,[group],Density,way_ch,date1,x,y,site_city,site_area,site_ch,highway_id,direction,mileage,habit,file1,Collector_ch,plan_name,invest_company,notes,Sensitive_level,accepted_name_code,sec_record,inaccuracy,siteid,TM2_X,TM2_Y,full_name) Values(";
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
        string value_mlleage = mileage.Text.Trim();
/*
        float i = 0;
        bool ToF = float.TryParse(mileage.Text.Trim(), out i);
        if (ToF)
        {
            value_mlleage = i * 1000 + "";
        }
*/
        string value_habit = habit.Text.Trim();
        string value_file1 = "";
        if (file1.HasFile)
        {
            value_file1 = file1.FileName;
            file1.SaveAs("D:\\freeway2\\Attachments\\Occurrence\\" + value_file1);
        }
        string value_Collector_ch = Collector_ch.Text.Trim();
        string value_plan_name = plan_name.Text.Trim();
        string value_invest_company = invest_company.Text.Trim();
        string value_notes = note.Text.Trim();
        string value_Sensitive_level = "";
        string value_accepted_name_code = "";
        string value_siteid = "";
        string value_TM2_X = "";
        string value_TM2_Y = "";
        string value_full_name = ScientificName.Text.Trim();

        sqlCommand += "@value_Chinese_name,";
        sqlCommand += "@value_Short_name,";
        sqlCommand += "@value_group,";
        sqlCommand += "@value_Density,";
        sqlCommand += "@value_way_ch,";
        sqlCommand += "@value_date1,";
        sqlCommand += "@value_x,";
        sqlCommand += "@value_y,";
        sqlCommand += "@value_site_city,";
        sqlCommand += "@value_site_area,";
        sqlCommand += "@value_site_ch,";
        sqlCommand += "@value_highway_id,";
        sqlCommand += "@value_direction,";
        sqlCommand += "@value_mlleage,";
        sqlCommand += "@value_habit,";
        sqlCommand += "@value_file1,";
        sqlCommand += "@value_Collector_ch,";
        sqlCommand += "@value_plan_name,";
        sqlCommand += "@value_invest_company,";
        sqlCommand += "@value_notes,";
        sqlCommand += "@value_Sensitive_level,";
        sqlCommand += "@value_accepted_name_code,";
        sqlCommand += "@value_sec_record,";
        sqlCommand += "@value_inaccuracy,";
        sqlCommand += "@value_siteid,";
        sqlCommand += "@value_TM2_X,";
        sqlCommand += "@value_TM2_Y,";
        sqlCommand += "@value_full_name)";


        //String sqlCommand = "Insert Into Site(siteid,x,y,inaccuracy,TM2_X,TM2_Y,date,site_en,site_ch,highway_id,note,uid) values(";
        //String sqlCommand_SiteId = "Select Count(siteid) cnts From site Where siteid=";

        //String value_siteid = siteid.Text;
        //decimal value_x = decimal.Parse(x.Text);
        //decimal value_y = decimal.Parse(y.Text);
        //decimal value_inaccuracy = decimal.Parse(inaccuracy.Text);
        //int value_TM2_X = int.Parse(TM2_X.Text);
        //int value_TM2_Y = int.Parse(TM2_Y.Text);

        ////String value_date = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
        //String value_date = date.Text;
        //String value_site_en = site_en.Text;
        //String value_site_ch = site_ch.Text;
        //String value_highway_id = highway_id.SelectedValue;
        //String value_note = note.Text;
        ////String Url="Occurrence_add1.aspx?siteid="+value_siteid;
        //String value_uid = Session["uid"].ToString();

        //Boolean NoData = false;
        //sqlCommand_SiteId += "@value_siteid + "'";

        //if (value_siteid.Equals(""))
        //{
        //    NoData = true;
        //    Response.Write("<script>alert('時間地點代號請輸入資料')</script>");
        //}
        
        //else{
        //    try
        //    {
        //        conn.Open();

        //        // 建立 SqlCommand 以便從提供的 SiteID 重複。
        //        cmd = new SqlCommand(sqlCommand_SiteId, conn);
                
        //        // 執行命令並將密碼欄位擷取到 lookupPassword 字串。
        //        //var siteid_cnts = cmd.ExecuteReader();

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            //String sid = reader[0].ToString;
        //            if (!reader[0].Equals(0))
        //            {
        //                Response.Write("<script>alert('時間地點代號已存在，請新增生態調查資料或修改時間地點代號!!')</script>");
        //                NoData = true;
        //                check_ok.Width = 0;
        //                check_ok.Height = 0;
        //                //check_ok.Style = "visibility:hidden";
        //                //check_ok.Style = "display:none";
        //                //Response.Redirect(Url);
        //            }
        //            else {
        //                check_ok.Width = 28;
        //                check_ok.Height = 28;
        //                //check_ok.Visible = true;
        //            }
        //           // Console.WriteLine(String.Format("{0}", reader[0]));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // 在這裡新增錯誤處理方式以便進行偵錯。
        //        System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        //    }
        //    conn.Close();
        //}

        //if (!NoData)
        //{
        //    if (value_y.Equals(0) || value_y.Equals(""))
        //    {
        //        Response.Write("<script>alert('緯度請輸入正確數字資料')</script>");
        //    }


        //    if (value_x.Equals(0) || value_x.Equals(""))
        //    {
        //        Response.Write("<script>alert('經度請輸入正確數字資料')</script>");
        //    }

        //    /*
        //    if (value_TM2_X.Equals(0) || value_TM2_X.Equals(""))
        //    {
        //        Response.Write("<script>alert('TM2_X請輸入正確數字資料')</script>");
        //    }

        //    if (value_TM2_Y.Equals(0) || value_TM2_Y.Equals(""))
        //    {
        //        Response.Write("<script>alert('TM2_Y請輸入正確數字資料')</script>");
        //    }
        //    */
        //}
        //sqlCommand += "@value_siteid,";
        //sqlCommand += "@value_x,";
        //sqlCommand += "@value_y,";
        //sqlCommand += "@value_inaccuracy,";
        //sqlCommand += "@value_TM2_X,";
        //sqlCommand += "@value_TM2_Y,";
        //sqlCommand += "@value_date,";
        //sqlCommand += "@value_site_en,";
        //sqlCommand += "@value_site_ch,";
        //sqlCommand += "@value_highway_id,";
        //sqlCommand += "@value_note,";
        //sqlCommand += "@value_uid)";

        //siteid有輸入
        bool NoData = false;
        if (!NoData)
        {
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
		cmd.Parameters.AddWithValue("value_date1",value_date1);
		cmd.Parameters.AddWithValue("value_x",value_x);
		cmd.Parameters.AddWithValue("value_y",value_y);
		cmd.Parameters.AddWithValue("value_site_city",value_site_city);
		cmd.Parameters.AddWithValue("value_site_area",value_site_area);
		cmd.Parameters.AddWithValue("value_site_ch",value_site_ch);
		cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
		cmd.Parameters.AddWithValue("value_direction",value_direction);
		cmd.Parameters.AddWithValue("value_mlleage",value_mlleage);
		cmd.Parameters.AddWithValue("value_habit",value_habit);
		cmd.Parameters.AddWithValue("value_file1",value_file1);
		cmd.Parameters.AddWithValue("value_Collector_ch",value_Collector_ch);
		cmd.Parameters.AddWithValue("value_plan_name",value_plan_name);
		cmd.Parameters.AddWithValue("value_invest_company",value_invest_company);
		cmd.Parameters.AddWithValue("value_notes",value_notes);
		cmd.Parameters.AddWithValue("value_Sensitive_level",value_Sensitive_level);
		cmd.Parameters.AddWithValue("value_accepted_name_code",value_accepted_name_code);
		cmd.Parameters.AddWithValue("value_sec_record",value_sec_record);
		cmd.Parameters.AddWithValue("value_inaccuracy",value_inaccuracy);
		cmd.Parameters.AddWithValue("value_siteid",value_siteid);
		cmd.Parameters.AddWithValue("value_TM2_X",value_TM2_X);
		cmd.Parameters.AddWithValue("value_TM2_Y",value_TM2_Y);
		cmd.Parameters.AddWithValue("value_full_name",value_full_name);

                // 執行命令
                cmd.ExecuteNonQuery();
                

                // 清理命令和連線物件。
                cmd.Dispose();
                conn.Dispose();

                //// 新增完成顯示新增生態出現紀錄
                //x.Enabled = false;
                //y.Enabled = false;
                //inaccuracy.Enabled = false;
                //TM2_X.Enabled = false;
                //TM2_Y.Enabled = false;
                //site_en.Enabled = false;
                //site_ch.Enabled = false;
                //note.Enabled = false;
                //check_ok.Width = 0;
                //check_ok.Height = 0;
                //imgCalendar1.Width = 0;
                //imgCalendar1.Height = 0;
                ////Table2.Enabled = true;
                ////Table2.Visible = true;
                //Add_Site.Enabled = false;
                Response.Write("<script>alert('新增生態調查資料完成!!');location.href='/newdefault.aspx';</script>");
            }
            catch (Exception ex)
            {
                // 在這裡新增錯誤處理方式以便進行偵錯。
                // 不應將此錯誤訊息傳回給呼叫者。
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
        }
    }

    //新增生態調查出現
    protected void Add_Occurrence_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Occurrence_add.aspx");
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String sqlCommand = "Insert Into Occurrence(siteid,Chinese_name,ScientificName,Density,Collector,Collector_ch,Way_ch,[Group],sec_record,habit,file1,path1,accepted_name_code,uid) Values(";
        String value_siteid = siteid.Text;
        String value_Chinese_name = Chinese_name.Text;
        String value_ScientificName = ScientificName.Text;
        String value_Density = Density.Text;
        String value_Collector = Collector.Text;
        String value_Collector_ch = Collector_ch.Text;
        String value_Way_ch = Way_ch.Text;
        String value_Group = Group.Text;
        String value_sec_record = sec_record.Text;
        String value_habit = habit.Text;
        String value_accepted_name_code = accepted_name_code.Text;
        String filepath = "/Attachments/Occurrence/";
        String filepath2 = "D:\\freeway2\\Attachments\\Occurrence\\";
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        String value_uid = Session["uid"].ToString();

        sqlCommand += "@value_siteid,";
        sqlCommand += "@value_Chinese_name,";
        sqlCommand += "@value_ScientificName,";
        sqlCommand += "@value_Density,";
        sqlCommand += "@value_Collector,";
        sqlCommand += "@value_Collector_ch,";
        sqlCommand += "@value_Way_ch,";
        sqlCommand += "@value_Group,";
        sqlCommand += "@value_sec_record,";
        sqlCommand += "@value_habit,";
        sqlCommand += "@value_file1,";
        sqlCommand += "@value_path1,";
        sqlCommand += "@value_accepted_name_code,";
        sqlCommand += "@value_uid)";

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_siteid",value_siteid);
	    cmd.Parameters.AddWithValue("value_Chinese_name",value_Chinese_name);
	    cmd.Parameters.AddWithValue("value_ScientificName",value_ScientificName);
	    cmd.Parameters.AddWithValue("value_Density",value_Density);
	    cmd.Parameters.AddWithValue("value_Collector",value_Collector);
	    cmd.Parameters.AddWithValue("value_Collector_ch",value_Collector_ch);
	    cmd.Parameters.AddWithValue("value_Way_ch",value_Way_ch);
	    cmd.Parameters.AddWithValue("value_Group",value_Group);
	    cmd.Parameters.AddWithValue("value_sec_record",value_sec_record);
	    cmd.Parameters.AddWithValue("value_habit",value_habit);
	    cmd.Parameters.AddWithValue("value_file1",value_file1);
	    cmd.Parameters.AddWithValue("value_path1",value_path1);
	    cmd.Parameters.AddWithValue("value_accepted_name_code",value_accepted_name_code);
	    cmd.Parameters.AddWithValue("value_uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //處理檔案上傳
            if (file1.HasFile)
            {
                file1.SaveAs(filepath2 + value_file1);
            }

            Response.Write("<script>alert('新增生態調查出現記錄完成!!')</script>");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        ScientificName.Text = "自行變更";
    }
}

    