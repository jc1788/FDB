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
using System.Drawing.Imaging;
using System.Drawing;
public partial class plant_batch : System.Web.UI.Page
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
        String filepath = "/Attachments/Batch_UploadPlants/";
        String filepath2 = "D:\\freeway2\\Attachments\\Batch_UploadPlants\\";
        String filepath3 = "D:\\freeway2\\Attachments\\";
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        String value_file2 = file2.FileName;
        String value_path2 = filepath;
        String value_file3 = file3.FileName;
        String value_path3 = filepath;
        String value_uid = Session["uid"].ToString();

        String Value_image_file = "";
        String Value_image_file2 = "";

        //處理檔案上傳(照片檔)
        filepath3 += "Plants\\";

        if (file2.HasFile)
        {
	    userlog(value_uid, "BATCH_UPLOAD_PLANTS: " + filepath3 + value_file2);

            file2.SaveAs(filepath3 + value_file2);

            string old_file = "D:\\freeway2\\Attachments\\Plants\\" + value_file2;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file2;
            string new_dic_path = "D:\\freeway2\\Attachments\\Plants\\" + value_file2;
            new_dic_path = new_dic_path.Split('.')[0];
            if (!Directory.Exists(new_dic_path)) Directory.CreateDirectory(new_dic_path);
            string new_file = "D:\\freeway2\\Attachments\\Plants\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }
            Value_image_file = value_file2.Split('.')[0];
            //string zipFilePath = "~/Attachments/Roadkill/" + value_file2; ;
            string zipFilePath = "~/Attachments/Plants/" + new_file_name; ;
            string sub_file_name = Path.GetExtension(zipFilePath);
            //string rarFilePath = "D:\\freeway2\\Attachments\\Roadkill\\" + value_file2;
            string rarFilePath = "D:\\freeway2\\Attachments\\Plants\\" + new_file_name;
            //string rarExpressPath = "D:\\freeway2\\Attachments\\Plants\\" + new_file_name;
            string rarExpressPath = new_dic_path;

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
                            DirectoryInfo di = new DirectoryInfo(rarExpressPath);
                            foreach (var fi in di.GetFiles())
                            {
                                System.Drawing.Image image = System.Drawing.Image.FromFile(rarExpressPath+"\\"+fi.Name);
                                //必須使用絕對路徑
                                ImageFormat thisFormat = image.RawFormat;
                                //取得影像的格式
                                int fixWidth = 0;
                                int fixHeight = 0;
                                //第一種縮圖用
                                int maxPx = 600;
                                //宣告一個最大值，demo是把該值寫到web.config裡
                                if (image.Width > maxPx || image.Height > maxPx)
                                //如果圖片的寬大於最大值或高大於最大值就往下執行
                                {
                                    if (image.Width >= image.Height)
                                    //圖片的寬大於圖片的高
                                    {
                                        fixWidth = maxPx;
                                        //設定修改後的圖寬
                                        fixHeight = Convert.ToInt32((Convert.ToDouble(fixWidth) / Convert.ToDouble(image.Width)) * Convert.ToDouble(image.Height));
                                        //設定修改後的圖高
                                    }
                                    else
                                    {
                                        fixHeight = maxPx;
                                        //設定修改後的圖高
                                        fixWidth = Convert.ToInt32((Convert.ToDouble(fixHeight) / Convert.ToDouble(image.Height)) * Convert.ToDouble(image.Width));
                                        //設定修改後的圖寬
                                    }

                                }
                                else
                                //圖片沒有超過設定值，不執行縮圖
                                {
                                    fixHeight = image.Height;
                                    fixWidth = image.Width;
                                }
                                Bitmap imageOutput = new Bitmap(image, fixWidth, fixHeight);

                                //輸出一個新圖(就是修改過的圖)
                                string fixSaveName = fi.Name;
                                //副檔名不應該這樣給，但因為此範例沒有讀取檔案的部份所以demo就直接給啦
                                imageOutput.Save(@"D:\freeway2\Attachments\Plants\" + fixSaveName, thisFormat);
                                //imageOutput.Save(string.Concat(Server.MapPath("~/Attachments/Plants/"), fixSaveName), thisFormat);
                                //將修改過的圖存於設定的位子
                                imageOutput.Dispose();
                                //釋放記憶體
                                image.Dispose();
                                //釋放掉圖檔 
                            }
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

        if (file3.HasFile)
        {
	    userlog(value_uid, "BATCH_UPLOAD_PLANTS: " + filepath3 + value_file3);

            file3.SaveAs(filepath3 + value_file3);

            string old_file = "D:\\freeway2\\Attachments\\Plants\\" + value_file3;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file3;
            string new_dic_path = "D:\\freeway2\\Attachments\\Plants\\" + value_file3;
            new_dic_path = new_dic_path.Split('.')[0];
            if (!Directory.Exists(new_dic_path)) Directory.CreateDirectory(new_dic_path);
            string new_file = "D:\\freeway2\\Attachments\\Plants\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }
            Value_image_file2 = value_file3.Split('.')[0];
            //string zipFilePath = "~/Attachments/Roadkill/" + value_file3; ;
            string zipFilePath = "~/Attachments/Plants/" + new_file_name; ;
            string sub_file_name = Path.GetExtension(zipFilePath);
            //string rarFilePath = "D:\\freeway2\\Attachments\\Roadkill\\" + value_file3;
            string rarFilePath = "D:\\freeway2\\Attachments\\Plants\\" + new_file_name;
            //string rarExpressPath = "D:\\freeway2\\Attachments\\Plants\\" + new_file_name;
            string rarExpressPath = new_dic_path;

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
                            DirectoryInfo di = new DirectoryInfo(rarExpressPath);
                            foreach (var fi in di.GetFiles())
                            {
                                System.Drawing.Image image = System.Drawing.Image.FromFile(rarExpressPath+"\\"+fi.Name);
                                //必須使用絕對路徑
                                ImageFormat thisFormat = image.RawFormat;
                                //取得影像的格式
                                int fixWidth = 0;
                                int fixHeight = 0;
                                //第一種縮圖用
                                int maxPx = 600;
                                //宣告一個最大值，demo是把該值寫到web.config裡
                                if (image.Width > maxPx || image.Height > maxPx)
                                //如果圖片的寬大於最大值或高大於最大值就往下執行
                                {
                                    if (image.Width >= image.Height)
                                    //圖片的寬大於圖片的高
                                    {
                                        fixWidth = maxPx;
                                        //設定修改後的圖寬
                                        fixHeight = Convert.ToInt32((Convert.ToDouble(fixWidth) / Convert.ToDouble(image.Width)) * Convert.ToDouble(image.Height));
                                        //設定修改後的圖高
                                    }
                                    else
                                    {
                                        fixHeight = maxPx;
                                        //設定修改後的圖高
                                        fixWidth = Convert.ToInt32((Convert.ToDouble(fixHeight) / Convert.ToDouble(image.Height)) * Convert.ToDouble(image.Width));
                                        //設定修改後的圖寬
                                    }

                                }
                                else
                                //圖片沒有超過設定值，不執行縮圖
                                {
                                    fixHeight = image.Height;
                                    fixWidth = image.Width;
                                }
                                Bitmap imageOutput = new Bitmap(image, fixWidth, fixHeight);

                                //輸出一個新圖(就是修改過的圖)
                                string fixSaveName = fi.Name;
                                //副檔名不應該這樣給，但因為此範例沒有讀取檔案的部份所以demo就直接給啦
                                imageOutput.Save(@"D:\freeway2\Attachments\Plants\" + fixSaveName, thisFormat);
                                //imageOutput.Save(string.Concat(Server.MapPath("~/Attachments/Plants/"), fixSaveName), thisFormat);
                                //將修改過的圖存於設定的位子
                                imageOutput.Dispose();
                                //釋放記憶體
                                image.Dispose();
                                //釋放掉圖檔 
                            }
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
	    userlog(value_uid, "BATCH_UPLOAD_PLANTS: " + filepath2 + value_file1);

            file1.SaveAs(filepath2 + value_file1);

            string old_file = "D:\\freeway2\\Attachments\\Batch_UploadPlants\\" + value_file1;
            string new_file_name = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + value_file1;
            string new_file = "D:\\freeway2\\Attachments\\Batch_UploadPlants\\" + new_file_name;
            if (File.Exists(old_file))
            {
                File.Move(old_file, new_file);
            }
            ReadExcel(filepath2 + new_file_name, "Plants", new_file_name, Value_image_file, Value_image_file2);
            //ReadExcel(filepath2 + new_file_name, "工作表1", new_file_name, Value_image_file);
        }


    }


    //public static DataTable ReadExcel(string DataSource, string SheetName)
    public void ReadExcel(string DataSource, string SheetName, string upload_file, string image_file, string image_file2)
    {
        //  ========EXECL讀取顯示======
        //  Extended Properties=Excel 8.0，格式從Excel 97~Excel 2003 都相容
        //  HDR=Yes，代表Excel檔中的工作表第一列是欄位名稱
        //  IMEX=0 時為「匯出模式」，這個模式開啟的 Excel 檔案只能用來做「寫入」用途。
        //  IMEX=1 時為「匯入模式」，這個模式開啟的 Excel 檔案只能用來做「讀取」用途。
        //  IMEX=2 時為「連結模式」，這個模式開啟的 Excel 檔案可同時支援「讀取」與「寫入」用途。
        string OLEDBConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                "Data Source='" + DataSource + "';" +
            //"Data Source='D:\\freeway2\\Attachments\\Batch_Upload\\site_sample.xlsx';" +
            //"Extended Properties='Excel 12.0;HDR=YES'";
        "Extended Properties='Excel 12.0;IMEX=1'";
        //"Extended Properties='Excel 12.0;HDR=YES;IMEX=1;READONLY=TRUE'";


        //判斷新舊版EXCEL
        Boolean isNewFormat = true;
        OleDbConnection connCheckFromat = new OleDbConnection();
        //connCheckFromat.ToString();
        connCheckFromat.ConnectionString = OLEDBConnStr;
        OleDbCommand comCheckFromat = new OleDbCommand();
        comCheckFromat.Connection = connCheckFromat;
        comCheckFromat.CommandText = "select * from [" + SheetName + "$]";  // SheetName = Sheet1 + $
        //try
        //{
        //    connCheckFromat.Open();
        //    OleDbDataReader drCheckFromat = comCheckFromat.ExecuteReader();
        //    while (drCheckFromat.Read())
        //    {
        //        //新版有此欄位，舊版則無
        //        string value_format = drCheckFromat[16].ToString();
        //    }
        //}
        //catch
        //{
        //    isNewFormat = false;
        //}
        //finally
        //{

        //}


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
        string value_image_file2 = image_file2;

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

            data_string = "植栽調查資料:\r\n";

            //Delete tmp data
            string sql_delete = "Begin Delete Plants_tmp Where uid = @value_uid; Delete Plants_error Where uid = @value_uid; End;";
            try
            {
                conn_insert.Open();
                cmd = new SqlCommand(sql_delete, conn_insert);
		cmd.Parameters.AddWithValue("value_uid",value_uid);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                conn_insert.Close();
            }
            //Delete tmp data

            string inputer = Context.Session["user_institution"].ToString();
            while (dr.Read())
            {
                //string sql_insert = "Insert Into Roadkill_tmp(id,site_ch,highway_id,direction,milestone,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid,Sensitive_level,upload_file,upload_date,image_file) values(";
                string sql_insert = "";
                //新版增加四個檔案
                if (isNewFormat)
                {
                    sql_insert = "Insert Into Plants_tmp(sid,Office,Segment,Highway_Name,Direction,Start1,Start2,Start,End1,End2,[End],PointX,PointY,PointXE,PointYE,Plant_Name,Plant_Number,unit2,SpecificationTall,SpecificationCrown,SpecificationMeter,LifeStyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,Plant_Loca,Img,FinishImg,Date,Note,uid,user_institution,upload_date,upload_xls) values(";
                }
                else
                {
                    sql_insert = "Insert Into Plants_tmp(sid,Office,Segment,Highway_Name,Direction,Start1,Start2,Start,End1,End2,[End],PointX,PointY,PointXE,PointYE,Plant_Name,Plant_Number,unit2,SpecificationTall,SpecificationCrown,SpecificationMeter,LifeStyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,Plant_Loca,Img,FinishImg,Date,Note,uid,user_institution,upload_date,upload_xls) values(";
                }

                value_siteid = dr[0].ToString();

                if (!value_siteid.Equals(""))
                {
		    string sid = dr[0].ToString();
                    string Office = dr[1].ToString();
                    string Segment = dr[2].ToString();
                    string Highway_Name = dr[3].ToString();
                    string Direction = dr[4].ToString();
                    string Start1 = "";
                    string Start2 = "";
                    string Start = "";
                    string StartTmp = dr[5].ToString();
                    if (StartTmp.IndexOf('.') > 0)
                    {
                        Start1 = StartTmp.Split('.')[0];
                        Start2 = StartTmp.Split('.')[1];
                    }
                    else
                    {
                        if (StartTmp == "")
                            Start1 = "000";
                        else
                            Start1 = StartTmp;
                        Start2 = "000";
                    }
                    if (Start1.Length == 2)
                    {
                        Start1 = "0" + Start1;
                    }
                    else if (Start1.Length == 1)
                    {
                        Start1 = "00" + Start1;
                    }
                    if (Start2.Length == 2)
                    {
                        Start2 = Start2 + "0";
                    }
                    else if (Start2.Length == 1)
                    {
                        Start2 = Start2 + "00";
                    }
                    if (Convert.ToInt32(Start2.Substring(1, 1)) >= 5)
                    {
                        Start2 = Convert.ToInt32(Start2.Substring(0, 1)) + 1 + "00";
                        if (Start2.Length >= 4)
                        {
                            Start2 = "900";
                        }
                    }
                    else
                    {
                        Start2 = Start2.Substring(0, 1) + "00";
                    }
                    int StartMileage = Convert.ToInt32(Start1) * 1000 + Convert.ToInt32(Start2);
                    Start = Start1 + "." + Start2;
                    string End1 = "";
                    string End2 = "";
                    string End = "";
                    string EndTmp = dr[6].ToString();
                    if (EndTmp.IndexOf('.') > 0)
                    {
                        End1 = EndTmp.Split('.')[0];
                        End2 = EndTmp.Split('.')[1];
                    }
                    else
                    {
                        if (EndTmp == "")
                            End1 = "000";
                        else
                            End1 = EndTmp;
                        End2 = "000";
                    }
                    if (End1.Length == 2)
                    {
                        End1 = "0" + End1;
                    }
                    else if (End1.Length == 1)
                    {
                        End1 = "00" + End1;
                    }
                    if (End2.Length == 2)
                    {
                        End2 = End2 + "0";
                    }
                    else if (End2.Length == 1)
                    {
                        End2 = End2 + "00";
                    }
                    if (Convert.ToInt32(End2.Substring(1, 1)) >= 5)
                    {
                        End2 = Convert.ToInt32(End2.Substring(0, 1)) + 1 + "00";
                        if (End2.Length >= 4)
                        {
                            End2 = "900";
                        }
                    }
                    else
                    {
                        End2 = End2.Substring(0, 1) + "00";
                    }
                    int EndMileage = Convert.ToInt32(End1) * 1000 + Convert.ToInt32(End2);
                    End = End1 + "." + End2;
                    string Plant_Name = dr[7].ToString();
                    string Plant_Number = dr[8].ToString();
                    string unit2 = dr[9].ToString();
                    string SpecificationTall = dr[10].ToString();
                    string SpecificationCrown = dr[11].ToString();
                    string SpecificationMeter = dr[12].ToString();
                    string LifeStyle = dr[13].ToString();

                    int error = 0;
		    Boolean check_flore = true;

                    string florescence = dr[14].ToString();
                    string tmp = "";

                    string FlowerColor = dr[15].ToString();
                    string FruitPeriod = dr[16].ToString();
                    string tmp2 = "";

                    string LeafColor = dr[17].ToString();
                    string LeafPeriod = dr[18].ToString();
                    string tmp3 = "";

		    try
		    {
                    if (florescence.IndexOf('|') > 0)
                    {
                        string[] array_florescence = florescence.Split('|');
                        foreach (var item in array_florescence)
                        {
                            if (item.IndexOf('-') > 0)
                            {
                                int S = Convert.ToInt32(item.Split('-')[0]);
                                int E = Convert.ToInt32(item.Split('-')[1]);
                                tmp += S + ",";
                                do
                                {
                                    S++;
                                    if (S > 12)
                                    {
                                        S = 1;
                                    }
                                    tmp += S + ",";

                                } while (S != E);
                            }
                            else
                            {
                                tmp += item + ",";
                            }
                        }
                    }
                    else
                    {
                        if (florescence.IndexOf('-') > 0)
                        {
                            int S = Convert.ToInt32(florescence.Split('-')[0]);
                            int E = Convert.ToInt32(florescence.Split('-')[1]);
                            tmp += S + ",";
                            do
                            {
                                S++;
                                if (S > 12)
                                {
                                    S = 1;
                                }
                                tmp += S + ",";

                            } while (S != E);
                        }
                        else
                        {
                            if (florescence !="")
                            {
                                tmp += florescence + ",";
                            }
                        }
                    }
                    if (FruitPeriod.IndexOf('|') > 0)
                    {
                        string[] array_FruitPeriod = FruitPeriod.Split('|');
                        foreach (var item in array_FruitPeriod)
                        {
                            if (item.IndexOf('-') > 0)
                            {
                                int S = Convert.ToInt32(item.Split('-')[0]);
                                int E = Convert.ToInt32(item.Split('-')[1]);
                                tmp2 += S + ",";
                                do
                                {
                                    S++;
                                    if (S > 12)
                                    {
                                        S = 1;
                                    }
                                    tmp2 += S + ",";

                                } while (S != E);
                            }
                            else
                            {
                                tmp2 += item + ",";
                            }
                        }
                    }
                    else
                    {
                        if (FruitPeriod.IndexOf('-') > 0)
                        {
                            int S = Convert.ToInt32(FruitPeriod.Split('-')[0]);
                            int E = Convert.ToInt32(FruitPeriod.Split('-')[1]);
                            tmp2 += S + ",";
                            do
                            {
                                S++;
                                if (S > 12)
                                {
                                    S = 1;
                                }
                                tmp2 += S + ",";

                            } while (S != E);
                        }
                        else
                        {
                            if (FruitPeriod!="")
                            {
                                tmp2 += FruitPeriod + ",";
                            }
                        }
                    }
                    if (LeafPeriod.IndexOf('|') > 0)
                    {
                        string[] array_LeafPeriod = LeafPeriod.Split('|');
                        foreach (var item in array_LeafPeriod)
                        {
                            if (item.IndexOf('-') > 0)
                            {
                                int S = Convert.ToInt32(item.Split('-')[0]);
                                int E = Convert.ToInt32(item.Split('-')[1]);
                                tmp3 += S + ",";
                                do
                                {
                                    S++;
                                    if (S > 12)
                                    {
                                        S = 1;
                                    }
                                    tmp3 += S + ",";

                                } while (S != E);
                            }
                            else
                            {
                                tmp3 += item + ",";
                            }
                        }
                    }
                    else
                    {
                        if (LeafPeriod.IndexOf('-') > 0)
                        {
                            int S = Convert.ToInt32(LeafPeriod.Split('-')[0]);
                            int E = Convert.ToInt32(LeafPeriod.Split('-')[1]);
                            tmp3 += S + ",";
                            do
                            {
                                S++;
                                if (S > 12)
                                {
                                    S = 1;
                                }
                                tmp3 += S + ",";

                            } while (S != E);
                        }
                        else
                        {
                            if (LeafPeriod!="")
                            {
                                tmp3 += LeafPeriod + ",";
                            }
                        }
                    }
		    }
		    catch (Exception fex)
		    {
			check_flore = false;
                        string error_note = "花期、果期或葉色轉變期格式有誤";
                        string sql_error = " Insert Into Plants_error (sid,errornote,uid) values (@sid,@error_note,@value_uid) ;";
                        try
                        {
                            conn_insert.Open();
                            cmd = new SqlCommand(sql_error, conn_insert);
			    cmd.Parameters.AddWithValue("sid",sid);
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
                        error = 1;
		    }

                    string Plant_Loca = dr[19].ToString();
                    string Img = dr[20].ToString();
                    string FinishImg = dr[21].ToString();
                    string Date = dr[22].ToString();
                    string Note = dr[23].ToString();
                    //新版增加四個檔案
                    //if (isNewFormat)
                    //{
                    //    value_file1 = dr[14].ToString();
                    //    value_file2 = dr[15].ToString();
                    //    value_file3 = dr[16].ToString();
                    //    value_file4 = dr[17].ToString();
                    //    value_file5 = dr[18].ToString();
                    //    value_note = dr[19].ToString();
                    //}
                    //else
                    //{
                    //    value_file1 = dr[14].ToString();
                    //    value_note = dr[15].ToString();
                    //}
                    string value_transfer = "";
                    string value_path1 = "/Attachments/Plants/";
                    string value_upload_file = upload_file;
                    string value_Sensitive_level;

                    //測試欄位輸入正常
                    //檢查日期格式
                    DateTime ddd;
                    Boolean check_date = true;
                    check_date = DateTime.TryParse(Date, out ddd);
		    if (check_date) {
                    //檢察調查日期是否大於今天
                    int result = DateTime.Compare(ddd, DateTime.Now);

                    if (int.Parse(ddd.ToString("yyyy")) < 2010 || result > 0)
                    {
                        check_date = false;
                    }
		    }

                    if (!check_date)
                    {
                        string error_note = "日期格式或內容有誤";
                        string sql_error = " Insert Into Plants_error (sid,date,errornote,uid) values (@sid,@Date,@error_note,@value_uid) ;";
                        try
                        {
                            conn_insert.Open();
                            cmd = new SqlCommand(sql_error, conn_insert);
			    cmd.Parameters.AddWithValue("sid",sid);
			    cmd.Parameters.AddWithValue("Date",Date);
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
                        error = 1;
                    }

                    Boolean check_highway_id = true;
                    if (Highway_Name.Equals("1")
                        || Highway_Name.Equals("2")
                        || Highway_Name.Equals("3")
                        || Highway_Name.Equals("4")
                        || Highway_Name.Equals("5")
                        || Highway_Name.Equals("6")
                        || Highway_Name.Equals("7")
                        || Highway_Name.Equals("8")
                        || Highway_Name.Equals("9")
                        || Highway_Name.Equals("10")
                        || Highway_Name.Equals("3甲")
                        || Highway_Name.Equals("台二己"))
                    {
                        check_highway_id = true;
                    }
                    else
                    {
                        check_highway_id = false;
                        string error_note = "國道編號資料有誤";
                        string sql_error = " Insert Into Plants_error (sid,Highway_Name,errornote,uid) values (@sid,@Highway_Name,@error_note,@value_uid) ;";
                        try
                        {
                            if (error == 0)
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_error, conn_insert);
			        cmd.Parameters.AddWithValue("sid",sid);
			        cmd.Parameters.AddWithValue("Highway_Name",Highway_Name);
			        cmd.Parameters.AddWithValue("error_note",error_note);
			        cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                            }
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
                        error = 1;
                    }


                    Boolean check_direction = true;
		    Direction = Direction.ToUpper();
                    if (((Highway_Name.Equals("1")
                       || Highway_Name.Equals("3")
                       || Highway_Name.Equals("5")
                       || Highway_Name.Equals("7")
                       || Highway_Name.Equals("9")) &&
			(Direction.Equals("N")
                       || Direction.Equals("S")))
                       || ((Highway_Name.Equals("2")
                       || Highway_Name.Equals("3甲")
                       || Highway_Name.Equals("4")
                       || Highway_Name.Equals("6")
                       || Highway_Name.Equals("8")
                       || Highway_Name.Equals("10")) &&
                        (Direction.Equals("E")
                        || Direction.Equals("W")))
                        || Direction.Equals("M")
			|| Direction.Equals("R"))
                    {
                        check_direction = true;
                    }
                    else
                    {
                        check_direction = false;
                        string error_note = "方向資料有誤";
                        string sql_error = " Insert Into Plants_error (sid,direction,errornote,uid) values (@sid,@Direction,@error_note,@value_uid) ;";
			if (Direction.Equals("N")
			 || Direction.Equals("S")
			 || Direction.Equals("E")
			 || Direction.Equals("W"))
			{
			    error_note = "國道編號與方向資料不符";
                            sql_error = " Insert Into Plants_error (sid,Highway_Name,direction,errornote,uid) values (@sid,@Highway_Name,@Direction,@error_note,@value_uid) ;";
			}

                        try
                        {
                            if (error == 0)
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_error, conn_insert);
			        cmd.Parameters.AddWithValue("sid",sid);
			        cmd.Parameters.AddWithValue("Direction",Direction);
			        cmd.Parameters.AddWithValue("Highway_Name",Highway_Name);
			        cmd.Parameters.AddWithValue("error_note",error_note);
			        cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                            }
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
                        error = 1;
                    }

                    //檢查里程為數值
                    Decimal ms1, ms2;
                    Boolean check_milestone = true;
                    check_milestone = Decimal.TryParse(Start, out ms1);
                    check_milestone = Decimal.TryParse(End, out ms2);
                    if (!check_milestone)
                    {
                        string error_note = "里程格式有誤";
                        string sql_error = " Insert Into Plants_error (sid,Start,[End],errornote,uid) values (@sid,@Start,@End,@error_note,@value_uid) ;";
                        try
                        {
                            if (error == 0)
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_error, conn_insert);
			        cmd.Parameters.AddWithValue("sid",sid);
			        cmd.Parameters.AddWithValue("Start",Start);
			        cmd.Parameters.AddWithValue("End",End);
			        cmd.Parameters.AddWithValue("error_note",error_note);
			        cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                            }
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
                        error = 1;
                    }
                    else if (!check_freeway_milestone(Highway_Name, Start, End))
                    {
                        string error_note = "里程超出範圍";
                        string sql_error = " Insert Into Plants_error (sid,Highway_Name,Start,[End],errornote,uid) values (@sid,@Highway_Name,@Start,@End,@error_note,@value_uid) ;";
                        try
                        {
                            if (error == 0)
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_error, conn_insert);
			        cmd.Parameters.AddWithValue("sid",sid);
			        cmd.Parameters.AddWithValue("Highway_Name",Highway_Name);
			        cmd.Parameters.AddWithValue("Start",Start);
			        cmd.Parameters.AddWithValue("End",End);
			        cmd.Parameters.AddWithValue("error_note",error_note);
			        cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                            }
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
                        error = 1;
                    }
                    //if (value_milestone.Equals(""))
                    //{
                    //    value_milestone = "0";
                    //}
                    //int value_mileage = 0;
                    //if (check_milestone)
                    //{
                    //    value_mileage = (int)(float.Parse(value_milestone) * 10) * 100;
                    //}
                    //string value_direction1 = "";

                    //if ((value_highway_id.Equals("1") || value_highway_id.Equals("3") || value_highway_id.Equals("5") || value_highway_id.Equals("7") || value_highway_id.Equals("9")) && (value_direction.Equals("R") || value_direction.Equals("")))
                    //{
                    //    value_direction1 = "N";
                    //}
                    //else if ((value_highway_id.Equals("2") || value_highway_id.Equals("4") || value_highway_id.Equals("6") || value_highway_id.Equals("8") || value_highway_id.Equals("10") || value_highway_id.Equals("3甲")) && (value_direction.Equals("R") || value_direction.Equals("")))
                    //{
                    //    value_direction1 = "E";
                    //}
                    //else
                    //{
                    //    value_direction1 = value_direction;
                    //}

                    //value_date = value_date.Replace("上午", "");
                    //value_date = value_date.Replace("下午", "");

		    if (Segment.Length > 2) Segment = Segment.Substring(0,2);

				Boolean check_site_ch = true;
				if (check_milestone
				&& ((Segment.Equals("內湖") && Highway_Name.Equals("1") && ms2 <= 50.85m)
				 || (Segment.Equals("中壢") && ((Highway_Name.Equals("1") && ms1 >= 30.85m && ms2 <= 103.5m) || Highway_Name.Equals("2") || (Highway_Name.Equals("3") && ms1 >= 32m && ms2 <= 72m)))
				 || (Segment.Equals("木柵") && ((Highway_Name.Equals("3") && ms2 <= 52m) || Highway_Name.Equals("3甲")))
				 || (Segment.Equals("關西") && ((Highway_Name.Equals("3") && ms1 >= 32m && ms2 <= 120.703m) || (Highway_Name.Equals("2") && ms1 > 8.2m) || (Highway_Name.Equals("1") && ms1 >= 83.5m && ms2 <= 110.8m)))
				 || (Segment.Equals("頭城") && Highway_Name.Equals("5"))
				 || (Segment.Equals("苗栗") && ((Highway_Name.Equals("1") && ms1 >= 90.8m && ms2 <= 183.5m) || Highway_Name.Equals("4")))
				 || (Segment.Equals("大甲") && Highway_Name.Equals("3") && ms1 >= 100.703m && ms2 <= 205.462m)
				 || (Segment.Equals("南投") && ((Highway_Name.Equals("3") && ms1 >= 185.462m && ms2 <= 280m) || Highway_Name.Equals("6")))
				 || (Segment.Equals("斗南") && Highway_Name.Equals("1") && ms1 >= 163.5m && ms2 <= 261.1m)
				 || (Segment.Equals("白河") && Highway_Name.Equals("3") && ms1 >= 260m && ms2 <= 393m)
				 || (Segment.Equals("新營") && ((Highway_Name.Equals("1") && ms1 >= 240.1m && ms2 <= 330m) || Highway_Name.Equals("8")))
				 || (Segment.Equals("岡山") && ((Highway_Name.Equals("1") && ms1 >= 310m) || (Highway_Name.Equals("10") && ms2 <= 28.4m)))
				 || (Segment.Equals("屏東") && ((Highway_Name.Equals("3") && ms1 >= 348m) || (Highway_Name.Equals("10") && ms1 >= 8.4m)))
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
                                    string sql_error = " Insert Into Plants_error (sid,Segment,Highway_Name,Start,[End],errornote,uid) values (@sid,@Segment,@Highway_Name,@Start,@End,@error_note,@value_uid) ;";
                        try
                        {
                            if (error == 0)
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_error, conn_insert);
			        cmd.Parameters.AddWithValue("sid",sid);
			        cmd.Parameters.AddWithValue("Segment",Segment);
			        cmd.Parameters.AddWithValue("Highway_Name",Highway_Name);
			        cmd.Parameters.AddWithValue("Start",Start);
			        cmd.Parameters.AddWithValue("End",End);
			        cmd.Parameters.AddWithValue("error_note",error_note);
			        cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                            }
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
                        error = 1;
		    }
		}

		    Boolean check_pnumber = true;
		    float pnumber;
		    if (!Single.TryParse(Plant_Number, out pnumber))
		    {
			check_pnumber = false;
                        string error_note = "數量不正確(需為數值)";
                        string sql_error = " Insert Into Plants_error (sid,Plant_Number,errornote,uid) values (@sid,@Plant_Number,@error_note,@value_uid) ;";
                        try
                        {
                            if (error == 0)
                            {
                                conn_insert.Open();
                                cmd = new SqlCommand(sql_error, conn_insert);
			        cmd.Parameters.AddWithValue("sid",sid);
			        cmd.Parameters.AddWithValue("Plant_Number",Plant_Number);
			        cmd.Parameters.AddWithValue("error_note",error_note);
			        cmd.Parameters.AddWithValue("value_uid",value_uid);
                                cmd.ExecuteNonQuery();
                            }
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
                        error = 1;
		    }

                    decimal value_dxS = 0;
                    decimal value_dyS = 0;
                    decimal value_dxE = 0;
                    decimal value_dyE = 0;

                    value_Sensitive_level = "";

                    if (!Highway_Name.Equals("") && !Direction.Equals(""))
                    {
                        value_dxS = Get_X(Highway_Name, Direction, StartMileage);
                        value_dyS = Get_Y(Highway_Name, Direction, StartMileage);
                        value_dxE = Get_X(Highway_Name, Direction, EndMileage);
                        value_dyE = Get_Y(Highway_Name, Direction, EndMileage);
                        //value_Sensitive_level = Get_Sensitive_level(Highway_Name, Direction, Mileage);
                    }
                    sql_insert += "@sid,";
                    sql_insert += "@Office,";
                    sql_insert += "@Segment,";
                    sql_insert += "@Highway_Name,";
                    sql_insert += "@Direction,";

                    ////若經緯度沒填資料則依照里程抓取
                    //if ((value_x.Equals("") && value_y.Equals("")) || value_x.Equals("0") && value_y.Equals("0"))
                    //{
                    //    //sql_insert += "@value_dx, ";
                    //    //sql_insert += "@value_dy, ";
                    //sql_insert += "NULL, ";
                    //sql_insert += "NULL, ";
                    //}
                    //else
                    //{
                    //}
                    sql_insert += "@Start1,";
                    sql_insert += "@Start2,";
                    sql_insert += "@Start,";
                    sql_insert += "@End1,";
                    sql_insert += "@End2,";
                    sql_insert += "@End,";
		    sql_insert += "@value_dxS,";
		    sql_insert += "@value_dyS,";
		    sql_insert += "@value_dxE,";
		    sql_insert += "@value_dyE,";

                    sql_insert += "@Plant_Name,";
                    sql_insert += "@Plant_Number,";
                    sql_insert += "@unit2,";
                    sql_insert += "@SpecificationTall,";
                    sql_insert += "@SpecificationCrown,";
                    sql_insert += "@SpecificationMeter,";
                    sql_insert += "@LifeStyle,";
                    //新版增加四個檔案
                    //if (isNewFormat)
                    //{
                    sql_insert += "@tmp,";
                    sql_insert += "@FlowerColor,";
                    sql_insert += "@tmp2,";
                    sql_insert += "@LeafColor,";
                    sql_insert += "@tmp3,";
                    sql_insert += "@Plant_Loca,";
		    if (!String.IsNullOrWhiteSpace(Img))
                    sql_insert += "'" + value_image_file + "/" + Img + "',";
		    else
		    sql_insert += "'',";
		    if (!String.IsNullOrWhiteSpace(FinishImg))
                    sql_insert += "'" + value_image_file2 + "/" + FinishImg + "',";
		    else
		    sql_insert += "'',";
                    //}
                    //else
                    //{
                    //sql_insert += " Convert(Date, @Date, 126), ";
                    sql_insert += "'" + ddd.ToString("yyyyMMdd") + "',";
                    sql_insert += "@Note,";
                    sql_insert += "@value_uid,";
                    sql_insert += "@inputer,";
		    sql_insert += "'" + System.DateTime.Now.ToString("yyyyMMddhhmm") + "',";
		    sql_insert += "@upload_file)";
                    //    sql_insert += "@value_transfer,";
                    //    sql_insert += "@value_note,";
                    //}
                    //sql_insert += "@value_uid,";
                    //sql_insert += "@value_Sensitive_level,";
                    //sql_insert += "@value_upload_file,";
                    //sql_insert += "getdate() , ";
                    //sql_insert += "@value_image_file) ";

                    //日期格式、國道編號、方向、里程皆正確才新增到資料庫
                    if ((check_flore == true) && (check_date == true) && (check_highway_id == true)
		    && (check_direction == true) && (check_milestone == true) && (check_site_ch == true) && (check_pnumber == true))
                    {
                        try
                        {
                            conn_insert.Open();
                            cmd = new SqlCommand(sql_insert, conn_insert);
			    cmd.Parameters.AddWithValue("sid",sid);
			    cmd.Parameters.AddWithValue("Office",Office);
			    cmd.Parameters.AddWithValue("Segment",Segment);
			    cmd.Parameters.AddWithValue("Highway_Name",Highway_Name);
			    cmd.Parameters.AddWithValue("Direction",Direction);
			    cmd.Parameters.AddWithValue("Start1",Start1);
			    cmd.Parameters.AddWithValue("Start2",Start2);
			    cmd.Parameters.AddWithValue("Start",Start);
			    cmd.Parameters.AddWithValue("End1",End1);
			    cmd.Parameters.AddWithValue("End2",End2);
			    cmd.Parameters.AddWithValue("End",End);
			    cmd.Parameters.AddWithValue("value_dxS",value_dxS.ToString());
			    cmd.Parameters.AddWithValue("value_dyS",value_dyS.ToString());
			    cmd.Parameters.AddWithValue("value_dxE",value_dxE.ToString());
			    cmd.Parameters.AddWithValue("value_dyE",value_dyE.ToString());
			    cmd.Parameters.AddWithValue("Plant_Name",Plant_Name);
			    cmd.Parameters.AddWithValue("Plant_Number",pnumber);
			    cmd.Parameters.AddWithValue("unit2",unit2);
			    cmd.Parameters.AddWithValue("SpecificationTall",SpecificationTall);
			    cmd.Parameters.AddWithValue("SpecificationCrown",SpecificationCrown);
			    cmd.Parameters.AddWithValue("SpecificationMeter",SpecificationMeter);
			    cmd.Parameters.AddWithValue("LifeStyle",LifeStyle);
			    cmd.Parameters.AddWithValue("tmp",tmp);
			    cmd.Parameters.AddWithValue("FlowerColor",FlowerColor);
			    cmd.Parameters.AddWithValue("tmp2",tmp2);
			    cmd.Parameters.AddWithValue("LeafColor",LeafColor);
			    cmd.Parameters.AddWithValue("tmp3",tmp3);
			    cmd.Parameters.AddWithValue("Plant_Loca",Plant_Loca);
			    cmd.Parameters.AddWithValue("Img",value_image_file + "/" + Img);
			    cmd.Parameters.AddWithValue("FinishImg",value_image_file2 + "/" + FinishImg);
			    cmd.Parameters.AddWithValue("ddd",ddd.ToString("yyyyMMdd"));
			    cmd.Parameters.AddWithValue("Note",Note);
			    cmd.Parameters.AddWithValue("value_uid",Convert.ToInt32(value_uid));
			    cmd.Parameters.AddWithValue("inputer",inputer);
			    cmd.Parameters.AddWithValue("upload_file",upload_file);
                            cmd.ExecuteNonQuery();
                            counts_ok += 1;

                        }
                        catch (Exception e)
                        {
                            counts_error += 1;
                            error_siteid += value_siteid + "、";
			    Response.Write(e.Message);
                        }
                        finally
                        {
                            conn_insert.Close();
                        }
                    }
                }
            }
            data_string += "完成共" + counts_ok + "筆，失敗共" + counts_error + "筆\r\n";
            data_string += "上傳失敗的id:\r\n" + error_siteid + "\r\n";
            if (!counts_error.Equals(0))
            {
                data_error = error_siteid;
		Response.Write(error_siteid);
            }

            conn.Close();

            //Response.Write("<script>alert('上傳資料完成!');location.href='RoadKill_tmp.aspx?counts_error=" +counts_error+"'; </script>");


            redirect_str = "plant_batch_tmp.aspx?counts_error=" + counts_error + "&counts_ok=" + counts_ok;
	    Response.Write(redirect_str);

            //redirect_str = "<script>alert('上傳資料完成!');location.href='RoadKill_tmp.aspx?counts_error=" + counts_error+"'; </script>";

            //redirect_str += "<script>alert('上傳完成'); location.href='" + redirect_str + "'; </script>";

            //Response.Write("<script>alert('上傳完成'); location.href='Default.aspx'; </script>");
        }
        catch { }
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

    Boolean check_freeway_milestone(string Highway_Name, string StartMileage, string EndMileage)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        Boolean match = false;

        String sqlCommand_NameFull = " Select * From Freeway_milestone Where highway_id = @Highway_Name and max_milestone >= @StartMileage and max_milestone >= @EndMileage";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("Highway_Name",Highway_Name);
	    cmd.Parameters.AddWithValue("StartMileage",StartMileage);
	    cmd.Parameters.AddWithValue("EndMileage",EndMileage);
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

        String sqlCommand_NameFull = " Select WGS84X AS x  From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage";

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

        String sqlCommand_NameFull = " Select WGS84Y AS y  From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage";

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

        String sqlCommand_NameFull = " Select Sensitive_level From Freeway_new Where free_id = @value_highway_id and direction = @value_direction and mileage = @query_mileage";

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
