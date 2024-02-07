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

public partial class roadkill_batch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }
    }

    protected void Upload_Data_Click(object sender, EventArgs e)
    {
        String filepath = "/Attachments/Batch_Upload/";
        String filepath2 = "D:\\freeway2\\Attachments\\Batch_Upload\\";
        String filepath3 = "D:\\freeway2\\Attachments\\";
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        String value_file2 = file2.FileName;
        String value_path2 = filepath;
        String value_uid = Session["uid"].ToString();

        String Value_image_file = "";

        //處理檔案上傳(照片檔)
        filepath3 += "Roadkill\\";

        if (file2.HasFile)
        {
	    userlog(value_uid, "BATCH_UPLOAD_ROADKILL: " + filepath3 + value_file2);

            file2.SaveAs(filepath3 + value_file2);

            string old_file = "D:\\freeway2\\Attachments\\Roadkill\\" + value_file2;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file2;
            string new_file = "D:\\freeway2\\Attachments\\Roadkill\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }
            Value_image_file = new_file_name;
            //string zipFilePath = "~/Attachments/Roadkill/" + value_file2; ;
            string zipFilePath = "~/Attachments/Roadkill/" + new_file_name; ;
            string sub_file_name = Path.GetExtension(zipFilePath);
            //string rarFilePath = "D:\\freeway2\\Attachments\\Roadkill\\" + value_file2;
            string rarFilePath = "D:\\freeway2\\Attachments\\Roadkill\\" + new_file_name;
            string rarExpressPath = "D:\\freeway2\\Attachments\\Roadkill";

            try
            {
                System.Diagnostics.Process Process1 = new System.Diagnostics.Process();
                //Process1.StartInfo.FileName = "C:\\Program Files\\WinRAR\\WinRAR.exe";
                Process1.StartInfo.FileName = "WinRAR.exe";
                Process1.StartInfo.CreateNoWindow = true;

                Process1.StartInfo.Arguments = " x -o+ " + rarFilePath + " " + rarExpressPath;

                try
                {
                    Process1.Start();
                    if (Process1.HasExited)
                    {
                        int iExitCode = Process1.ExitCode;
                        if (iExitCode == 0)
                        {
                            Response.Write(iExitCode.ToString() + " 正常完成");
                        }
                        else
                        {
                            Response.Write(iExitCode.ToString() + " 有錯完成");
                        }
                    }
                }

                catch (Exception rar_e)
                {
                    throw rar_e;
                }

                finally
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //處理檔案上傳(EXCEL)
        if (file1.HasFile)
        {
	    userlog(value_uid, "BATCH_UPLOAD_ROADKILL: " + filepath2 + value_file1);

            file1.SaveAs(filepath2 + value_file1);

            string old_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + value_file1;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file1;
            string new_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }
            ReadExcel(filepath2 + new_file_name, "roadkill", new_file_name, Value_image_file);
            //ReadExcel(filepath2 + new_file_name, "工作表1", new_file_name, Value_image_file);
        }

        
    }
    

    //public static DataTable ReadExcel(string DataSource, string SheetName)
    public void ReadExcel(string DataSource, string SheetName ,string upload_file,string image_file)
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


        //判斷新舊版EXCEL
        Boolean isNewFormat = true;
        OleDbConnection connCheckFromat = new OleDbConnection();
        //connCheckFromat.ToString();
        connCheckFromat.ConnectionString = OLEDBConnStr;
        OleDbCommand comCheckFromat = new OleDbCommand();
        comCheckFromat.Connection = connCheckFromat;
        comCheckFromat.CommandText = "select * from [" + SheetName + "$]";  // SheetName = Sheet1 + $
        try
        {
            connCheckFromat.Open();
            OleDbDataReader drCheckFromat = comCheckFromat.ExecuteReader();
            while (drCheckFromat.Read())
            {
                //新版有此欄位，舊版則無
                string value_format = drCheckFromat[16].ToString();
            }
        }
        catch 
        {
            isNewFormat = false;
        }
        finally
        {
            
        }


        DataSet ds = new DataSet();
        OleDbConnection conn = new OleDbConnection();
        //conn.ToString();
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
        string value_image_file = image_file;


        try
        {
            conn.Open();

	    DataTable Sheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
	    SheetName = SheetName + "$";
	    string sht = "";
	    foreach(DataRow dr1 in Sheets.Rows)
	    {
		sht = dr1[2].ToString().Replace("'","");
		if (sht == SheetName) break;		
	    }
	    if (sht != SheetName) {
	      SheetName = "Sheet1$";
	      foreach(DataRow dr2 in Sheets.Rows)
	      {
		  sht = dr2[2].ToString().Replace("'","");
		  if (sht == SheetName) break;		
	      }
	      if (sht != SheetName) {
		SheetName = "工作表1$";
	        foreach(DataRow dr3 in Sheets.Rows)
	        {
		    sht = dr3[2].ToString().Replace("'","");
		    if (sht == SheetName) break;		
	        }
		if (sht != SheetName) {
		  SheetName = Sheets.Rows[0][2].ToString().Replace("'","");
		}
              }
	    }

            com.CommandText = "select * from [" + SheetName + "]";  // SheetName = Sheet1 + $
            OleDbDataReader dr = com.ExecuteReader();
            string data_string = "";
            string error_siteid = "";
            string data_error = "";

            //string error_siteid = "AR090423N0010D0500";
            int counts_ok = 0;
            int counts_error = 0;
            string value_siteid = "";
           
                    data_string = "道路致死資料:\r\n";
                    
                    //Delete tmp data
                    string sql_delete = "Begin Delete Roadkill_tmp Where uid = @value_uid; Delete Roadkill_error Where uid = @value_uid; End;";
                    try
                    {
                        conn_insert.Open();
                        cmd = new SqlCommand(sql_delete, conn_insert);
			cmd.Parameters.AddWithValue("value_uid",value_uid);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception )
                    {
                        
                    }
                    finally
                    {
                        conn_insert.Close();
                    }
                    //Delete tmp data
                   

                    while (dr.Read())
                    {
                        //string sql_insert = "Insert Into Roadkill_tmp(id,site_ch,highway_id,direction,milestone,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid,Sensitive_level,upload_file,upload_date,image_file) values(";
                        string sql_insert = "";
                            //新版增加四個檔案
                            if (isNewFormat)
                            {
                                sql_insert = "Insert Into Roadkill_tmp(id,site_ch,highway_id,direction,milestone,mileage,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid,Sensitive_level,upload_file,upload_date,image_file) values(";
                            }
                            else
                            {
                                sql_insert = "Insert Into Roadkill_tmp(id,site_ch,highway_id,direction,milestone,mileage,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,collecter_ch,species,deduce_species,file1,path1,transfer,note,uid,Sensitive_level,upload_file,upload_date,image_file) values(";
                            }

                            value_siteid = dr[0].ToString();
                            

                            if (!value_siteid.Equals(""))
                            {
                                string value_id = dr[0].ToString();
                                string value_site_ch = dr[1].ToString();
                                string value_highway_id = dr[2].ToString();
                                string value_direction = dr[3].ToString();
                                string value_milestone = dr[4].ToString();
                                string value_x = dr[5].ToString();
                                string value_y = dr[6].ToString();
                                string value_TM2_X = "0";
                                string value_TM2_Y = "0";
                                string value_date = dr[7].ToString().Replace(" 上午 12:00:00", "");
                                string value_range = dr[8].ToString();
                                string value_type = dr[9].ToString();
                                string value_weather = dr[10].ToString();
                                string value_animal = dr[11].ToString();
                                string value_detail_animal = "";
                                string value_collecter_ch = dr[12].ToString();
                                string value_species = dr[13].ToString();
                                string value_deduce_species = "";
                                string value_file1 = "";
                                string value_file2 = "";
                                string value_file3 = "";
                                string value_file4 = "";
                                string value_file5 = "";
                                string value_note = "";
                                //新版增加四個檔案
                                if (isNewFormat)
                                {
                                    value_file1 = dr[14].ToString();
                                    value_file2 = dr[15].ToString();
                                    value_file3 = dr[16].ToString();
                                    value_file4 = dr[17].ToString();
                                    value_file5 = dr[18].ToString();
                                    value_note = dr[19].ToString();
                                }
                                else {
                                    value_file1 = dr[14].ToString();
                                    value_note = dr[15].ToString();
                                }
                                string value_transfer = "";
                                string value_path1 = "/Attachments/Roadkill/";
                                string value_upload_file = upload_file;
                                string value_Sensitive_level;

                                //測試欄位輸入正常
                                //檢查日期格式
                                DateTime ddd;
                                Boolean check_date = true;
                                check_date = DateTime.TryParse(value_date, out ddd);
				if (check_date) {
                                //檢察調查日期是否大於今天
                                int result = DateTime.Compare(DateTime.Parse(value_date), DateTime.Now);

                                if (int.Parse(ddd.ToString("yyyy")) < 2010 || result > 0)
                                {
                                    check_date = false;
                                }
				}

                                if (!check_date)
                                {
                                    string error_note = "日期格式或內容有誤";
                                    string sql_error = " Insert Into Roadkill_error (id,date,note,uid) values (@value_id,@value_date,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_date",value_date);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;
                                }



                                Boolean check_highway_id = true;
                                if (value_highway_id.Equals("1")
                                    || value_highway_id.Equals("2")
                                    || value_highway_id.Equals("3")
                                    || value_highway_id.Equals("4")
                                    || value_highway_id.Equals("5")
                                    || value_highway_id.Equals("6")
                                    || value_highway_id.Equals("7")
                                    || value_highway_id.Equals("8")
                                    || value_highway_id.Equals("9")
                                    || value_highway_id.Equals("10")
                                    || value_highway_id.Equals("3甲")
                                    || value_highway_id.Equals("台二己"))
                                {
                                    check_highway_id = true;
                                }
                                else
                                {
                                    check_highway_id = false;
                                    string error_note = "國道編號資料有誤";
                                    string sql_error = " Insert Into Roadkill_error (id,highway_id,note,uid) values (@value_id,@value_highway_id,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;

                                }


                                Boolean check_direction = true;
                                if (value_direction.Equals("")
				 || (((value_highway_id.Equals("1")
                                    || value_highway_id.Equals("3")
                                    || value_highway_id.Equals("5")
                                    || value_highway_id.Equals("7")
                                    || value_highway_id.Equals("9")) &&
				    (value_direction.Equals("N")
				    || value_direction.Equals("S")))
                                    || ((value_highway_id.Equals("2")
                                    || value_highway_id.Equals("3甲")
                                    || value_highway_id.Equals("4")
                                    || value_highway_id.Equals("6")
                                    || value_highway_id.Equals("8")
                                    || value_highway_id.Equals("10")) &&
                                    (value_direction.Equals("E")
                                    || value_direction.Equals("W")))
                                    || value_direction.Equals("高架")
                                    || value_direction.Equals("R")))
                                {
                                    check_direction = true;
                                }
                                else
                                {
                                    check_direction = false;
                                    string error_note = "方向資料有誤";
                                    string sql_error = " Insert Into Roadkill_error (id,direction,note,uid) values (@value_id,@value_direction,@error_note,@value_uid) ;";
				    if (value_direction.Equals("N")
					|| value_direction.Equals("S")
					|| value_direction.Equals("E")
					|| value_direction.Equals("W"))
				    {
					error_note = "國道編號與方向資料不符";
                                        sql_error = " Insert Into Roadkill_error (id,highway_id,direction,note,uid) values (@value_id,@value_highway_id,@value_direction,@error_note,@value_uid) ;";
				    }
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
					cmd.Parameters.AddWithValue("value_direction",value_direction);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;

                                }

                                //檢查里程為數值
                                Decimal ms;
                                Boolean check_milestone = true;
                                check_milestone = Decimal.TryParse(value_milestone, out ms);
                                if (!check_milestone)
                                {
                                    string error_note = "里程格式有誤";
                                    string sql_error = " Insert Into Roadkill_error (id,milestone,note,uid) values (@value_id,@value_milestone,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_milestone",value_milestone);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;
                                }
				else if (!check_freeway_milestone(value_highway_id, value_milestone))
				{
				    check_milestone = false;
                                    string error_note = "里程超出範圍";
                                    string sql_error = " Insert Into Roadkill_error (id,highway_id,milestone,note,uid) values (@value_id,@value_highway_id,@value_milestone,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
					cmd.Parameters.AddWithValue("value_milestone",value_milestone);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;
				}

				Boolean check_site_ch = true;
				if (check_milestone
				&& ((value_site_ch.Equals("內湖") && value_highway_id.Equals("1") && ms <= 50.85m)
				 || (value_site_ch.Equals("中壢") && ((value_highway_id.Equals("1") && ms >= 30.85m && ms <= 103.5m) || value_highway_id.Equals("2") || (value_highway_id.Equals("3") && ms >= 32m && ms <= 72m)))
				 || (value_site_ch.Equals("木柵") && ((value_highway_id.Equals("3") && ms <= 52m) || value_highway_id.Equals("3甲")))
				 || (value_site_ch.Equals("關西") && ((value_highway_id.Equals("3") && ms >= 32m && ms <= 120.703m) || (value_highway_id.Equals("2") && ms > 8.2m) || (value_highway_id.Equals("1") && ms >= 83.5m && ms <= 110.8m)))
				 || (value_site_ch.Equals("頭城") && value_highway_id.Equals("5"))
				 || (value_site_ch.Equals("苗栗") && ((value_highway_id.Equals("1") && ms >= 90.8m && ms <= 183.5m) || value_highway_id.Equals("4")))
				 || (value_site_ch.Equals("大甲") && value_highway_id.Equals("3") && ms >= 100.703m && ms <= 205.462m)
				 || (value_site_ch.Equals("南投") && ((value_highway_id.Equals("3") && ms >= 185.462m && ms <= 280m) || value_highway_id.Equals("6")))
				 || (value_site_ch.Equals("斗南") && value_highway_id.Equals("1") && ms >= 163.5m && ms <= 261.1m)
				 || (value_site_ch.Equals("白河") && value_highway_id.Equals("3") && ms >= 260m && ms <= 393m)
				 || (value_site_ch.Equals("新營") && ((value_highway_id.Equals("1") && ms >= 240.1m && ms <= 330m) || value_highway_id.Equals("8")))
				 || (value_site_ch.Equals("岡山") && ((value_highway_id.Equals("1") && ms >= 310m) || (value_highway_id.Equals("10") && ms <= 28.4m)))
				 || (value_site_ch.Equals("屏東") && ((value_highway_id.Equals("3") && ms >= 348m) || (value_highway_id.Equals("10") && ms >= 8.4m)))
				))
				{
				    check_site_ch = true;
				}
				else
				{
				    check_site_ch = false;
				    if (check_milestone)
				    {
                                    string error_note = "國道里程超出工務段轄區範圍";
                                    string sql_error = " Insert Into Roadkill_error (id,site_ch,highway_id,milestone,note,uid) values (@value_id,@value_site_ch,@value_highway_id,@value_milestone,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_site_ch",value_site_ch);
					cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
					cmd.Parameters.AddWithValue("value_milestone",value_milestone);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;
				    }
				}

				Boolean check_animal = true;
                                if (value_animal.Equals("貓狗")
                                    || value_animal.Equals("大鳥")
                                    || value_animal.Equals("中小鳥")
                                    || value_animal.Equals("果子狸")
                                    || value_animal.Equals("其他"))
                                {
                                    check_animal = true;
                                }
                                else
                                {
                                    check_animal = false;
                                    string error_note = "動物類群內容有誤";
				    if (value_animal.Contains("貓") || value_animal.Contains("狗"))
					error_note = "請填寫貓狗, 勿單填貓或狗";
				    else if (value_animal.Contains("鳥"))
					error_note = "鳥類請更改填寫為中小鳥或大鳥";
                                    string sql_error = " Insert Into Roadkill_error (id,animal,note,uid) values (@value_id,@value_animal,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_animal",value_animal);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;

                                }

				Boolean check_species = true;
				if (!value_species.Equals("")
				&& ((value_animal.Equals("貓狗") && ((!value_species.Contains("貓") && !value_species.Contains("狗")) || value_species.Contains("貓頭") || value_species.Contains("魚狗")))
				|| (value_species.Contains("鳥") && !value_animal.Contains("鳥"))))
                                {
                                    check_species = false;
                                    string error_note = "動物類群內容與可能種類不符";
                                    string sql_error = " Insert Into Roadkill_error (id,animal,species,note,uid) values (@value_id,@value_animal,@value_species,@error_note,@value_uid) ;";
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_error, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_animal",value_animal);
					cmd.Parameters.AddWithValue("value_species",value_species);
					cmd.Parameters.AddWithValue("error_note",error_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        //counts_error += 1;
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                    counts_error += 1;

                                }

                                if (value_milestone.Equals(""))
                                {
                                    value_milestone = "0";
                                }
                                int value_mileage = 0;
                                if (check_milestone)
                                {
                                    value_mileage = (int)(float.Parse(value_milestone) * 10) * 100;
                                }
                                string value_direction1 = "";

                                if ((value_highway_id.Equals("1") || value_highway_id.Equals("3") || value_highway_id.Equals("5") || value_highway_id.Equals("7") || value_highway_id.Equals("9")) && (value_direction.Equals("R") || value_direction.Equals("")))
                                {
                                    value_direction1 = "N";
                                }
                                else if ((value_highway_id.Equals("2") || value_highway_id.Equals("4") || value_highway_id.Equals("6") || value_highway_id.Equals("8") || value_highway_id.Equals("10") || value_highway_id.Equals("3甲")) && (value_direction.Equals("R") || value_direction.Equals("")))
                                {
                                    value_direction1 = "E";
                                }
                                else
                                {
                                    value_direction1 = value_direction;
                                }

                                value_date = value_date.Replace("上午", "");
                                value_date = value_date.Replace("下午", "");

                                decimal value_dx = 0;
                                decimal value_dy = 0;
                                value_Sensitive_level = "";

                                if (!value_highway_id.Equals("") && !value_direction.Equals("") && value_mileage != 0)
                                {
                                    value_dx = Get_X(value_highway_id, value_direction, value_mileage);
                                    value_dy = Get_Y(value_highway_id, value_direction, value_mileage);
                                    value_Sensitive_level = Get_Sensitive_level(value_highway_id, value_direction, value_mileage);
                                }
                                sql_insert += "@value_id,";
                                sql_insert += "@value_site_ch,";
                                sql_insert += "@value_highway_id,";
                                sql_insert += "@value_direction,";
                                sql_insert += "@value_milestone,";
                                sql_insert += "@value_mileage,";

                                //若經緯度沒填資料則依照里程抓取
                                if ((value_x.Equals("") && value_y.Equals("")) || value_x.Equals("0") && value_y.Equals("0"))
                                {
                                    //sql_insert += "@value_dx, ";
                                    //sql_insert += "@value_dy, ";
                                    sql_insert += "NULL, ";
                                    sql_insert += "NULL, ";
                                }
                                else
                                {
                                    sql_insert += "@value_x,";
                                    sql_insert += "@value_y,";
                                }

                                sql_insert += "@value_TM2_X,";
                                sql_insert += "@value_TM2_Y,";
                                sql_insert += " Convert(Date, @value_date, 126), ";
                                sql_insert += "@value_range,";
                                sql_insert += "@value_type,";
                                sql_insert += "@value_weather,";
                                sql_insert += "@value_animal,";
                                sql_insert += "@value_detail_animal,";
                                sql_insert += "@value_collecter_ch,";
                                sql_insert += "@value_species,";
                                sql_insert += "@value_deduce_species,";
                                //新版增加四個檔案
                                if (isNewFormat)
                                {
                                    sql_insert += "@value_file1,";
                                    sql_insert += "@value_file2,";
                                    sql_insert += "@value_file3,";
                                    sql_insert += "@value_file4,";
                                    sql_insert += "@value_file5,";
                                    sql_insert += "@value_path1,";
                                    sql_insert += "@value_transfer,";
                                    sql_insert += "@value_note,";
                                }
                                else {
                                    sql_insert += "@value_file1,";
                                    sql_insert += "@value_path1,";
                                    sql_insert += "@value_transfer,";
                                    sql_insert += "@value_note,";
                                }
                                sql_insert += "@value_uid,";
                                sql_insert += "@value_Sensitive_level,";
                                sql_insert += "@value_upload_file,";
                                sql_insert += "getdate() , ";
                                sql_insert += "@value_image_file) ";
                                
                                //日期格式、國道編號、方向、里程皆正確才新增到資料庫
				//不正確也可強制上傳 2019/3/18
                                //if ((check_date == true) && (check_highway_id == true) && (check_direction == true) && (check_milestone == true) && (check_site_ch == true) && (check_animal == true) && (check_species == true))
                                //{
                                    try
                                    {
                                        conn_insert.Open();
                                        cmd = new SqlCommand(sql_insert, conn_insert);
					cmd.Parameters.AddWithValue("value_id",value_id);
					cmd.Parameters.AddWithValue("value_site_ch",value_site_ch);
					cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
					cmd.Parameters.AddWithValue("value_direction",value_direction);
					cmd.Parameters.AddWithValue("value_milestone",value_milestone);
					cmd.Parameters.AddWithValue("value_mileage",value_mileage.ToString());
					cmd.Parameters.AddWithValue("value_x",value_x);
					cmd.Parameters.AddWithValue("value_y",value_y);
					cmd.Parameters.AddWithValue("value_TM2_X",value_TM2_X);
					cmd.Parameters.AddWithValue("value_TM2_Y",value_TM2_Y);
					cmd.Parameters.AddWithValue("value_date",value_date);
					cmd.Parameters.AddWithValue("value_range",value_range);
					cmd.Parameters.AddWithValue("value_type",value_type);
					cmd.Parameters.AddWithValue("value_weather",value_weather);
					cmd.Parameters.AddWithValue("value_animal",value_animal);
					cmd.Parameters.AddWithValue("value_detail_animal",value_detail_animal);
					cmd.Parameters.AddWithValue("value_collecter_ch",value_collecter_ch);
					cmd.Parameters.AddWithValue("value_species",value_species);
					cmd.Parameters.AddWithValue("value_deduce_species",value_deduce_species);
					cmd.Parameters.AddWithValue("value_file1",value_file1);
					cmd.Parameters.AddWithValue("value_file2",value_file2);
					cmd.Parameters.AddWithValue("value_file3",value_file3);
					cmd.Parameters.AddWithValue("value_file4",value_file4);
					cmd.Parameters.AddWithValue("value_file5",value_file5);
					cmd.Parameters.AddWithValue("value_path1",value_path1);
					cmd.Parameters.AddWithValue("value_transfer",value_transfer);
					cmd.Parameters.AddWithValue("value_note",value_note);
					cmd.Parameters.AddWithValue("value_uid",value_uid);
					cmd.Parameters.AddWithValue("value_Sensitive_level",value_Sensitive_level);
					cmd.Parameters.AddWithValue("value_upload_file",value_upload_file);
					cmd.Parameters.AddWithValue("value_image_file",value_image_file);
                                        cmd.ExecuteNonQuery();
                                        counts_ok += 1;
                                        
                                    }
                                    catch (Exception)
                                    {
                                        counts_error += 1;
                                        error_siteid += value_siteid + "、";
                                    }
                                    finally
                                    {
                                        conn_insert.Close();
                                    }
                                //}
                                
                          
                        }
                    }
                    data_string += "完成共" + counts_ok + "筆，失敗共" + counts_error + "筆\r\n";
                    data_string += "上傳失敗的id:\r\n" + error_siteid + "\r\n";
                    if (!counts_error.Equals(0))
                    {
                        data_error = error_siteid;
                    }

            conn.Close();

                //Response.Write("<script>alert('上傳資料完成!');location.href='RoadKill_batch_tmp.aspx?counts_error=" +counts_error+"'; </script>");


                    redirect_str = "RoadKill_batch_tmp.aspx?counts_error=" + counts_error + "&counts_ok=" + counts_ok;
                Response.Write(redirect_str);
                //redirect_str = "<script>alert('上傳資料完成!');location.href='RoadKill_batch_tmp.aspx?counts_error=" + counts_error+"'; </script>";
                
                //redirect_str += "<script>alert('上傳完成'); location.href='" + redirect_str + "'; </script>";
                
                //Response.Write("<script>alert('上傳完成'); location.href='Default.aspx'; </script>");
        }
        catch {}
        finally
        {
            conn.Close();

            /*
            string old_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + upload_file;
            string new_file = "D:\\freeway2\\Attachments\\Batch_Upload\\" + System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + upload_file;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }
            */
        }
        if (!redirect_str.Equals(""))
        {
            Response.Redirect(redirect_str);
        }
        
    }

    Boolean check_freeway_milestone(string value_highway_id, string value_milestone)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        Boolean match = false;

        String sqlCommand_NameFull = " Select * From Freeway_milestone Where highway_id = @value_highway_id and max_milestone >= @value_milestone";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_milestone",value_milestone);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
		match = true;
            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        return match;
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

    protected void Upload_Month_NoRecord(object sender, EventArgs e)
    {
        int value_years = DateTime.Today.Year;
        int value_month = DateTime.Today.Month;
        string value_uid = Session["uid"].ToString();

        string sql_NoRecord = " Insert Into Roadkill_Month_NoRecord (years , months , uid , user_institution) values(";
        sql_NoRecord += "@value_years , ";
        sql_NoRecord += "@value_month , ";
        sql_NoRecord += "@value_uid , ";
        sql_NoRecord += "(Select user_institution From Userdata Where uid = @value_uid ) );";

        SqlConnection conn_insert = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        
        try
        {
            conn_insert.Open();
            cmd = new SqlCommand(sql_NoRecord, conn_insert);
	    cmd.Parameters.AddWithValue("value_years",value_years);
	    cmd.Parameters.AddWithValue("value_month",value_month);
	    cmd.Parameters.AddWithValue("value_uid",value_uid);
            cmd.ExecuteNonQuery();
            
        }
        catch (Exception)
        {
            //counts_error += 1;
        }
        finally
        {
            conn_insert.Close();
        }
        Response.Write("<script>alert('設定本月無資料完成!');window.close();</script>");
        
    }

    private void userlog(string uid, string action)
    {
	string source_ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
	if (source_ip == null) source_ip = Request.ServerVariables["REMOTE_ADDR"];

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

	conn.Open();
        string sql = "insert into userlog(uid,action,atime,source_ip) values(@uid,@action,getdate(),@source_ip)";
	cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("uid", uid);
	cmd.Parameters.AddWithValue("action", action);
	cmd.Parameters.AddWithValue("source_ip", source_ip);
	cmd.ExecuteNonQuery();

	cmd.Dispose();
	conn.Dispose();
    }
}
