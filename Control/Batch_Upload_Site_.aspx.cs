using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Configuration;
using Ionic.Zip;   


public partial class Control_Batch_Upload_Site : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        //Query_RoadKill_tmp();
    }
    protected void Upload_Data_Click(object sender, EventArgs e)
    {
        String filepath = "/Attachments/Batch_Upload/";
        String filepath2 = "D:\\freeway2\\Attachments\\Batch_Upload\\";
        String filepath3 = "D:\\freeway2\\Attachments\\";
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        //String value_file2 = file2.FileName;
        //String value_path2 = filepath;
        String value_uid = Session["uid"].ToString();

        //處理檔案上傳(照片檔)

        filepath3 += "Occurrence\\";
/*        if (file2.HasFile)
        {
            file2.SaveAs(filepath3 + value_file2);

            string old_file = "D:\\freeway2\\Attachments\\Occurrence\\" + value_file2;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file2;
            string new_file = "D:\\freeway2\\Attachments\\Occurrence\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }

            string zipFilePath = "~/Attachments/Occurrence/" + new_file_name;
            string zipExpressPath = "~/Attachments/Occurrence/";
            try
            {
                var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                using (ZipFile zip = ZipFile.Read(Server.MapPath(zipFilePath), options))
                {
                    //zip.Password = "arvin"; // 解壓密碼
                    zip.ExtractAll(Server.MapPath(zipExpressPath));  // 解壓全部
                    //TextBox1.Text += "生態調查照片檔上傳成功";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
*/
        //處理檔案上傳(EXCEL)
        if (file1.HasFile)
        {
            file1.SaveAs(filepath2 + value_file1);

            string old_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + value_file1;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file1;
            string new_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }

                //讀site與occurrence資料
                ReadExcel(filepath2 + new_file_name, "site", new_file_name);
                ReadExcel(filepath2 + new_file_name, "occurrence", new_file_name);
        }

        
    }

    //public static DataTable ReadExcel(string DataSource, string SheetName)
    public void ReadExcel(string DataSource, string SheetName ,string upload_file)
    {
        //  ========EXECL讀取顯示======
        //  Extended Properties=Excel 8.0，格式從Excel 97~Excel 2003 都相容
        //  HDR=Yes，代表Excel檔中的工作表第一列是欄位名稱
        //  IMEX=0 時為「匯出模式」，這個模式開啟的 Excel 檔案只能用來做「寫入」用途。
        //  IMEX=1 時為「匯入模式」，這個模式開啟的 Excel 檔案只能用來做「讀取」用途。
        //  IMEX=2 時為「連結模式」，這個模式開啟的 Excel 檔案可同時支援「讀取」與「寫入」用途。
        string OLEDBConnStr =   "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                "Data Source='" + DataSource + "';" +
                                //"Data Source='D:\\freeway2\\Attachments\\Batch_Upload\\site_sample.xlsx';" +
                                "Extended Properties='Excel 12.0;HDR=YES'";
                                //"Extended Properties='Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE'";
        DataSet ds = new DataSet();
        OleDbConnection conn = new OleDbConnection();
        conn.ToString();
        conn.ConnectionString = OLEDBConnStr;
        
        OleDbCommand com = new OleDbCommand();
        com.Connection = conn;
        String value_uid = Session["uid"].ToString();
        //DataTable dt = new DataTable();
        string redirect_str = "";
        //for insert sql
        SqlConnection conn_insert = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
       //String errorLog = "";

        com.CommandText = "select * from [" + SheetName + "$]";  // SheetName = Sheet1 + $
        try
        {
            conn.Open();

	    DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
	    string sht = "";
	    foreach(DataRow dr1 in Sheets.Rows)
	    {
		sht = dr1[2].ToString().Replace("'","");
		if (sht == (SheetName + "$")) break;
	    }
	    if (sht != (SheetName + "$")) {
		conn.Close();
		return;
	    }

            OleDbDataReader dr = com.ExecuteReader();
            string data_string = "";
            string error_siteid = "";
            //string data_error = "";

            //string error_siteid = "AR090423N0010D0500";
            int counts_ok = 0;
            int counts_error = 0;
            string value_siteid = "";
            switch (SheetName)
            {
                case "site":
                    data_string = "生態調查站: ";
                    while (dr.Read())
                    {
                        string sql_insert = "Insert Into Site(siteid,x,y,inaccuracy,TM2_X,TM2_Y,date,site_en,site_ch,highway_id,note,uid) values(";

                        value_siteid = dr[0].ToString();

                        if (!value_siteid.Equals(""))
                        {
                            string value_x = dr[1].ToString();
                            string value_y = dr[2].ToString();
                            string value_inaccuracy = dr[3].ToString();
                            string value_TM2_X = dr[4].ToString();
                            string value_TM2_Y = dr[5].ToString();
                            string value_date = dr[6].ToString();
                            string value_site_en = dr[7].ToString();
                            string value_site_ch = dr[8].ToString();
                            string value_highway_id = dr[9].ToString();
                            string value_note = dr[10].ToString();

                            value_date = value_date.Replace("上午", "");
                            value_date = value_date.Replace("下午", "");

                            sql_insert += "@value_siteid,";
                            sql_insert += "@value_x,";
                            sql_insert += "@value_y,";
                            sql_insert += "@value_inaccuracy,";
                            sql_insert += "@value_TM2_X,";
                            sql_insert += "@value_TM2_Y,";
                            sql_insert += " Convert(Date, @value_date, 126), ";
                            sql_insert += "@value_site_en,";
                            sql_insert += "@value_site_ch,";
                            sql_insert += "@value_highway_id,";
                            sql_insert += "@value_note,";
                            sql_insert += "@value_uid)";

                            try
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_insert, conn_insert);
				cmd.Parameters.AddWithValue("value_siteid",value_siteid);
				cmd.Parameters.AddWithValue("value_x",value_x);
				cmd.Parameters.AddWithValue("value_y",value_y);
				cmd.Parameters.AddWithValue("value_inaccuracy",value_inaccuracy);
				cmd.Parameters.AddWithValue("value_TM2_X",value_TM2_X);
				cmd.Parameters.AddWithValue("value_TM2_Y",value_TM2_Y);
				cmd.Parameters.AddWithValue("value_date",value_date);
				cmd.Parameters.AddWithValue("value_site_en",value_site_en);
				cmd.Parameters.AddWithValue("value_site_ch",value_site_ch);
				cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
				cmd.Parameters.AddWithValue("value_note",value_note);
				cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                                counts_ok += 1;
                            }
                            catch (Exception )
                            {
                                counts_error += 1;
                                error_siteid += value_siteid;
                            }
                            finally
                            {
                                conn_insert.Close();
                            }
                        }
                    }
                    data_string += "完成共" + counts_ok + "筆，失敗共" + counts_error + "筆";
                    break;


                case "occurrence":
                    
                    data_string = "生態調查紀錄: ";
                    while (dr.Read())
                    {
                        //string sql_insert = "Insert Into Occurrence(siteid,Chinese_name,ScientificName,Density,Collector,Collector_ch,Way_ch,[Group],sec_record,habit,file1,path1,accepted_name_code,uid) Values(";
                        string sql_insert = "Insert Into table_1(Chinese_name,short_name,[group],Density,way_ch,date1,x,y,site_city,site_area,site_ch,highway_id,direction,mileage,habit,file1,Collector_ch,plan_name,invest_company,notes,Sensitive_level,accepted_name_code,sec_record,inaccuracy,siteid,TM2_X,TM2_Y,full_name) Values(";

                        //value_siteid = dr[0].ToString();

                        if (!dr[0].ToString().Equals(""))
                        {
                            string value_Chinese_name = dr[0].ToString();
                            string value_Short_name = dr[1].ToString();
                            string value_group = dr[2].ToString();
                            string value_Density = dr[3].ToString();
                            string value_way_ch = dr[4].ToString();
                            string value_date1 = dr[6].ToString();
                            string value_x = dr[7].ToString();
                            string value_y = dr[8].ToString();
                            string value_site_city = dr[10].ToString();
                            string value_site_area = dr[11].ToString();
                            string value_site_ch = dr[12].ToString();
                            string value_highway_id = dr[13].ToString();
                            string value_direction = dr[14].ToString();
                            string value_milleage = "";
                            float i = 0;
                            bool ToF = float.TryParse(dr[15].ToString(), out i);
                            if (ToF)
                            {
                                value_milleage = i * 1000 + "";
                            }
                            string value_habit = dr[16].ToString();
                            string value_file1 = dr[17].ToString();
                            string value_Collector_ch = dr[18].ToString();
                            string value_plan_name = dr[19].ToString();
                            string value_invest_company = dr[20].ToString();
                            string value_notes = dr[21].ToString();
                            string value_Sensitive_level = "";
                            string value_accepted_name_code = "";
                            string value_sec_record = dr[5].ToString();
                            string value_inaccuracy = dr[9].ToString();
                            value_siteid = "";
                            string value_TM2_X = "";
                            string value_TM2_Y = "";
                            string value_full_name = dr[1].ToString();

                            sql_insert += "@value_Chinese_name,";
                            sql_insert += "@value_Short_name,";
                            sql_insert += "@value_group,";
                            sql_insert += "@value_Density,";
                            sql_insert += "@value_way_ch,";
                            sql_insert += "@value_date1,";
                            sql_insert += "@value_x,";
                            sql_insert += "@value_y,";
                            sql_insert += "@value_site_city,";
                            sql_insert += "@value_site_area,";
                            sql_insert += "@value_site_ch,";
                            sql_insert += "@value_highway_id,";
                            sql_insert += "@value_direction,";
                            sql_insert += "@value_milleage,";
                            sql_insert += "@value_habit,";
                            sql_insert += "@value_file1,";
                            sql_insert += "@value_Collector_ch,";
                            sql_insert += "@value_plan_name,";
                            sql_insert += "@value_invest_company,";
                            sql_insert += "@value_notes,";
                            sql_insert += "@value_Sensitive_level,";
                            sql_insert += "@value_accepted_name_code,";
                            sql_insert += "@value_sec_record,";
                            sql_insert += "@value_inaccuracy,";
                            sql_insert += "@value_siteid,";
                            sql_insert += "@value_TM2_X,";
                            sql_insert += "@value_TM2_Y,";
                            sql_insert += "@value_full_name)";

                            try
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_insert, conn_insert);
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
				cmd.Parameters.AddWithValue("value_milleage",value_milleage);
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
                                cmd.ExecuteNonQuery();
                                counts_ok += 1;
                            }
                            catch (Exception e)
                            {
                                counts_error += 1;
                                error_siteid += value_siteid;
                            }
                            finally
                            {
                                conn_insert.Close();
                            }
                        }
                    }
                    data_string += "完成共" + counts_ok + "筆，失敗共" + counts_error + "筆";
                    break;

                default:
                    data_string = "無資料！！";
                    break;
            }
            redirect_str = data_string;
        }
        catch (Exception e)
	{
            redirect_str = e.Message;
	}
        finally
        {
            conn.Close();
            /*string old_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + upload_file;
            string new_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + upload_file;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }*/
        }
        if (!redirect_str.Equals(""))
        {
            Response.Write("<script>alert('"+redirect_str+"'); location.href='Batch_Upload_Site_.aspx'</script>");
        }
    }


    decimal Get_X(string value_highway_id, string value_direction, int query_mileage)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        decimal x = 0;

        String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
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

        decimal y = 0;

        String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
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


    string Get_Sensitive_level(string value_highway_id, string value_direction, int query_mileage)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        string Sensitive_level = "";

        String sqlCommand_NameFull = " Select Sensitive_level From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
	    cmd.Parameters.AddWithValue("query_mileage",query_mileage);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Sensitive_level = reader["Sensitive_level"].ToString();
                }
            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        return Sensitive_level;
    }
}