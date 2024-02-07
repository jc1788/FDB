using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.IO;
using System.Data;
using System.Web.Script.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.XSSF.Util;
using NPOI.SS.Util;
using NPOI;

public partial class Control_Roadkill_Export : System.Web.UI.Page
{
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
    }

    protected void Export_Excel(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "Select id , site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , range ,type , weather , animal , detail_animal , animal2 , collecter_ch , species , deduce_species , upload_file , Replace(CONVERT (char(10), upload_date, 126),'1900-01-01','') AS  upload_date , transfer , note , Case When file1 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file1 ELSE '' End AS file1_url , file1  , Case When file2 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file2 ELSE '' End AS file2_url , file2  ,Case When file3 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file3 ELSE '' End AS file3_url , file3  ,Case When file4 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file4 ELSE '' End AS file4_url , file4  ,Case When file5 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file5 ELSE '' End AS file5_url , file5 ,  Case When image_file <> '' Then 'https://ecodata.freeway.gov.tw' + path1+image_file ELSE '' End AS image_file  From roadkill     ";

        GridView_List.PageSize = 100000;
        //移除最後的連結欄位
        GridView_List.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        GridView_List.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=Roadkill_" + Excel_ShortTime + ".xls");
        //Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        Response.ContentEncoding = Encoding.GetEncoding("BIG5");
        //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentType = "application/ms-excel";
        GridView_List.EnableViewState = false;
        GridView_List.AllowPaging = false;
        GridView_List.AllowSorting = false;
        StringWriter objStringWriter = new StringWriter();
        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        GridView_List.RenderControl(objHtmlTextWriter);

        Response.Write(objStringWriter.ToString());
        Response.End();

    }

    protected void Export_Excel_new(object sender, EventArgs e)
    {

        string sql = "Select id , site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , range ,type , weather , animal , detail_animal , animal2 , collecter_ch , species , deduce_species , upload_file , Replace(CONVERT (char(10), upload_date, 126),'1900-01-01','') AS  upload_date , transfer , note , Case When file1 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file1 ELSE '' End AS file1_url , file1  , Case When file2 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file2 ELSE '' End AS file2_url , file2  ,Case When file3 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file3 ELSE '' End AS file3_url , file3  ,Case When file4 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file4 ELSE '' End AS file4_url , file4  ,Case When file5 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file5 ELSE '' End AS file5_url , file5 ,  Case When image_file <> '' Then 'https://ecodata.freeway.gov.tw' + path1+image_file ELSE '' End AS image_file  From roadkill     ";
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // 建立 SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        // 執行命令
        //dt_excel = cmd.ExecuteReader().;

        // 清理命令和連線物件。
        cmd.Dispose();
        conn.Dispose();

        System.DateTime now = new System.DateTime();
        now = System.DateTime.Now;
        //ExcelExporter export = new ExcelExporter();
        try
        {

            // 產生EXCEL Sheet 設定
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            // Detail Sheet
            ISheet u_sheet_Detail = workbook.CreateSheet("roadkill");

            // CellStyle
            IDataFormat num_format = workbook.CreateDataFormat();
            IFont content_font = workbook.CreateFont();
            content_font.FontName = "新細明體";
            content_font.FontHeightInPoints = 12;

            ICellStyle style_wrap_right = workbook.CreateCellStyle();
            //style_wrap_right.DataFormat = num_format.GetFormat("###,###,##0.00");
            style_wrap_right.Alignment = HorizontalAlignment.Right;
            style_wrap_right.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_right.WrapText = true;
            style_wrap_right.SetFont(content_font);

            ICellStyle style_wrap_left = workbook.CreateCellStyle();
            style_wrap_left.Alignment = HorizontalAlignment.Left;
            style_wrap_left.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_left.WrapText = true;
            style_wrap_left.SetFont(content_font);

            ICellStyle style_wrap_center = workbook.CreateCellStyle();
            style_wrap_center.Alignment = HorizontalAlignment.Center;
            style_wrap_center.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_center.WrapText = true;
            style_wrap_center.SetFont(content_font);

            ICellStyle style_wrap_right_unit = workbook.CreateCellStyle();
            //style_wrap_right_unit.DataFormat = num_format.GetFormat("###,##0.000000");
            style_wrap_right_unit.Alignment = HorizontalAlignment.Right;
            style_wrap_right_unit.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_right_unit.WrapText = true;
            style_wrap_right_unit.SetFont(content_font);

            // 差異表工作表 欄寬
            u_sheet_Detail.SetColumnWidth(0, 10 * 256);
            u_sheet_Detail.SetColumnWidth(1, 10 * 256);
            u_sheet_Detail.SetColumnWidth(2, 10 * 256);
            u_sheet_Detail.SetColumnWidth(3, 10 * 256);
            u_sheet_Detail.SetColumnWidth(4, 10 * 256);
            u_sheet_Detail.SetColumnWidth(5, 10 * 256);
            u_sheet_Detail.SetColumnWidth(6, 10 * 256);
            u_sheet_Detail.SetColumnWidth(7, 10 * 256);
            u_sheet_Detail.SetColumnWidth(8, 10 * 256);
            u_sheet_Detail.SetColumnWidth(9, 10 * 256);
            u_sheet_Detail.SetColumnWidth(10, 10 * 256);
            u_sheet_Detail.SetColumnWidth(11, 10 * 256);
            u_sheet_Detail.SetColumnWidth(12, 10 * 256);
            u_sheet_Detail.SetColumnWidth(13, 10 * 256);
            u_sheet_Detail.SetColumnWidth(14, 10 * 256);
            u_sheet_Detail.SetColumnWidth(15, 10 * 256);
            u_sheet_Detail.SetColumnWidth(16, 10 * 256);
            u_sheet_Detail.SetColumnWidth(17, 10 * 256);
            u_sheet_Detail.SetColumnWidth(18, 10 * 256);
            u_sheet_Detail.SetColumnWidth(19, 10 * 256);
            u_sheet_Detail.SetColumnWidth(20, 10 * 256);
            u_sheet_Detail.SetColumnWidth(21, 10 * 256);
            u_sheet_Detail.SetColumnWidth(22, 10 * 256);
            u_sheet_Detail.SetColumnWidth(23, 10 * 256);
            u_sheet_Detail.SetColumnWidth(24, 10 * 256);
            u_sheet_Detail.SetColumnWidth(25, 10 * 256);
            u_sheet_Detail.SetColumnWidth(26, 10 * 256);
            u_sheet_Detail.SetColumnWidth(27, 10 * 256);
            u_sheet_Detail.SetColumnWidth(28, 10 * 256);
            u_sheet_Detail.SetColumnWidth(29, 10 * 256);
            u_sheet_Detail.SetColumnWidth(30, 10 * 256);
            u_sheet_Detail.SetColumnWidth(31, 10 * 256);
            //設定CellStyle
            //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("id");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("工務段");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("國道編號");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("方向");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("里程");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("經度");
            u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("緯度");
            u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("日期");
            u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("範圍");
            u_sheet_Detail.GetRow(0).CreateCell(9).SetCellValue("工作類別");
            u_sheet_Detail.GetRow(0).CreateCell(10).SetCellValue("天氣");
            u_sheet_Detail.GetRow(0).CreateCell(11).SetCellValue("動物類群");
            u_sheet_Detail.GetRow(0).CreateCell(12).SetCellValue("大類");
            u_sheet_Detail.GetRow(0).CreateCell(13).SetCellValue("詳細類群");
            u_sheet_Detail.GetRow(0).CreateCell(14).SetCellValue("調查者");
            u_sheet_Detail.GetRow(0).CreateCell(15).SetCellValue("可能種類");
            u_sheet_Detail.GetRow(0).CreateCell(16).SetCellValue("可能種類推測");
            u_sheet_Detail.GetRow(0).CreateCell(17).SetCellValue("上傳檔名");
            u_sheet_Detail.GetRow(0).CreateCell(18).SetCellValue("上傳日期");
            u_sheet_Detail.GetRow(0).CreateCell(19).SetCellValue("後送");
            u_sheet_Detail.GetRow(0).CreateCell(20).SetCellValue("備註");
            u_sheet_Detail.GetRow(0).CreateCell(21).SetCellValue("照片1");
            u_sheet_Detail.GetRow(0).CreateCell(22).SetCellValue("照片1路徑");
            u_sheet_Detail.GetRow(0).CreateCell(23).SetCellValue("照片2");
            u_sheet_Detail.GetRow(0).CreateCell(24).SetCellValue("照片2路徑");
            u_sheet_Detail.GetRow(0).CreateCell(25).SetCellValue("照片3");
            u_sheet_Detail.GetRow(0).CreateCell(26).SetCellValue("照片3路徑");
            u_sheet_Detail.GetRow(0).CreateCell(27).SetCellValue("照片4");
            u_sheet_Detail.GetRow(0).CreateCell(28).SetCellValue("照片4路徑");
            u_sheet_Detail.GetRow(0).CreateCell(29).SetCellValue("照片5");
            u_sheet_Detail.GetRow(0).CreateCell(30).SetCellValue("照片5路徑");
            u_sheet_Detail.GetRow(0).CreateCell(31).SetCellValue("照片壓縮檔名");

            //設定CellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(6).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(7).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(8).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(9).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(10).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(12).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(13).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(14).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(15).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(16).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(17).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(18).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(19).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(20).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(21).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(22).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(23).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(24).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(25).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(26).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(27).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(28).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(29).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(30).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(31).CellStyle = style_wrap_center;
            int k = 0;

            // 比較表 資料
            foreach (DataRow pDR_FSData in dt_excel.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;

                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["site_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["highway_id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["direction"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["milestone"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["x"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["y"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["range"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(9).SetCellValue(pDR_FSData["type"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(10).SetCellValue(pDR_FSData["weather"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(11).SetCellValue(pDR_FSData["animal"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(12).SetCellValue(pDR_FSData["detail_animal"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(13).SetCellValue(pDR_FSData["animal2"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(14).SetCellValue(pDR_FSData["collecter_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(15).SetCellValue(pDR_FSData["species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(16).SetCellValue(pDR_FSData["deduce_species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(17).SetCellValue(pDR_FSData["upload_file"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(18).SetCellValue(pDR_FSData["upload_date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(19).SetCellValue(pDR_FSData["transfer"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(20).SetCellValue(pDR_FSData["note"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(21).SetCellValue(pDR_FSData["file1"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(22).SetCellValue(pDR_FSData["file1_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(23).SetCellValue(pDR_FSData["file2"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(24).SetCellValue(pDR_FSData["file2_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(25).SetCellValue(pDR_FSData["file3"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(26).SetCellValue(pDR_FSData["file3_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(27).SetCellValue(pDR_FSData["file4"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(28).SetCellValue(pDR_FSData["file4_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(29).SetCellValue(pDR_FSData["file5"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(30).SetCellValue(pDR_FSData["file5_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(31).SetCellValue(pDR_FSData["image_file"].ToString());

                    //設定CellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(6).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(7).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(8).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(9).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(10).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(11).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(12).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(13).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(14).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(15).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(16).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(17).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(18).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(19).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(20).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(21).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(22).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(23).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(24).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(25).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(26).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(27).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(28).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(29).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(30).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(31).CellStyle = style_wrap_left;
                }
            }

            /*
            u_sheet_Detail.AutoSizeColumn(0);
            u_sheet_Detail.AutoSizeColumn(1);
            u_sheet_Detail.AutoSizeColumn(2);
            u_sheet_Detail.AutoSizeColumn(3);
            u_sheet_Detail.AutoSizeColumn(4);
            u_sheet_Detail.AutoSizeColumn(5);
            u_sheet_Detail.AutoSizeColumn(6);
            u_sheet_Detail.AutoSizeColumn(7);
            u_sheet_Detail.AutoSizeColumn(8);
            u_sheet_Detail.AutoSizeColumn(9);
            u_sheet_Detail.AutoSizeColumn(10);
            u_sheet_Detail.AutoSizeColumn(11);
            u_sheet_Detail.AutoSizeColumn(12);
            u_sheet_Detail.AutoSizeColumn(13);
            u_sheet_Detail.AutoSizeColumn(14);
            u_sheet_Detail.AutoSizeColumn(15);
            u_sheet_Detail.AutoSizeColumn(16);
            u_sheet_Detail.AutoSizeColumn(17);
            u_sheet_Detail.AutoSizeColumn(18);
            u_sheet_Detail.AutoSizeColumn(19);
            u_sheet_Detail.AutoSizeColumn(20);
            u_sheet_Detail.AutoSizeColumn(21);
            u_sheet_Detail.AutoSizeColumn(22);
            u_sheet_Detail.AutoSizeColumn(23);
            u_sheet_Detail.AutoSizeColumn(24);
            u_sheet_Detail.AutoSizeColumn(25);
            u_sheet_Detail.AutoSizeColumn(26);
            u_sheet_Detail.AutoSizeColumn(27);
            u_sheet_Detail.AutoSizeColumn(28);
            u_sheet_Detail.AutoSizeColumn(29);
            u_sheet_Detail.AutoSizeColumn(30);
            u_sheet_Detail.AutoSizeColumn(31);
            */
            workbook.Write(ms);

            //將EXCEL檔案匯出到系統資料儲存目錄
            //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "roadkill.xls";
            string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
            FileStream fs = new FileStream(folderPath, FileMode.Create, FileAccess.Write);
            fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
            fs.Flush();
            fs.Close();
            workbook = null;
            ms.Close();
            ms.Dispose();

            System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
            byte[] xfile = null;
            //string docupath = Request.PhysicalApplicationPath;
            xfile = wc.DownloadData(folderPath);
            string xfileName = System.IO.Path.GetFileName(folderPath);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
            HttpContext.Current.Response.ContentType = "application/octet-stream"; //二進位方式
            HttpContext.Current.Response.BinaryWrite(xfile); //內容轉出作檔案下載
            HttpContext.Current.Response.End();

            //pDT_Account_Payables_Different_Detail = null;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void Export_Csv_new(object sender, EventArgs e)
    {

        string sql = "Select site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date ,animal2 , deduce_species From roadkill     ";
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // 建立 SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        //2018/06/07 mark by ian
        //{
        //// 執行命令
        ////dt_excel = cmd.ExecuteReader().;

        //// 清理命令和連線物件。
        //cmd.Dispose();
        //conn.Dispose();

        //System.DateTime now = new System.DateTime();
        //now = System.DateTime.Now;
        ////ExcelExporter export = new ExcelExporter();
        //try
        //{

        //    // 產生EXCEL Sheet 設定
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    MemoryStream ms = new MemoryStream();
        //    // Detail Sheet
        //    ISheet u_sheet_Detail = workbook.CreateSheet("roadkill");

        //    // CellStyle
        //    IDataFormat num_format = workbook.CreateDataFormat();
        //    IFont content_font = workbook.CreateFont();
        //    content_font.FontName = "新細明體";
        //    content_font.FontHeightInPoints = 12;

        //    ICellStyle style_wrap_right = workbook.CreateCellStyle();
        //    //style_wrap_right.DataFormat = num_format.GetFormat("###,###,##0.00");
        //    style_wrap_right.Alignment = HorizontalAlignment.Right;
        //    style_wrap_right.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_right.WrapText = true;
        //    style_wrap_right.SetFont(content_font);

        //    ICellStyle style_wrap_left = workbook.CreateCellStyle();
        //    style_wrap_left.Alignment = HorizontalAlignment.Left;
        //    style_wrap_left.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_left.WrapText = true;
        //    style_wrap_left.SetFont(content_font);

        //    ICellStyle style_wrap_center = workbook.CreateCellStyle();
        //    style_wrap_center.Alignment = HorizontalAlignment.Center;
        //    style_wrap_center.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_center.WrapText = true;
        //    style_wrap_center.SetFont(content_font);

        //    ICellStyle style_wrap_right_unit = workbook.CreateCellStyle();
        //    //style_wrap_right_unit.DataFormat = num_format.GetFormat("###,##0.000000");
        //    style_wrap_right_unit.Alignment = HorizontalAlignment.Right;
        //    style_wrap_right_unit.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_right_unit.WrapText = true;
        //    style_wrap_right_unit.SetFont(content_font);

        //    // 差異表工作表 欄寬
        //    u_sheet_Detail.SetColumnWidth(0, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(1, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(2, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(3, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(4, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(5, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(6, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(7, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(8, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(9, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(10, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(11, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(12, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(13, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(14, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(15, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(16, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(17, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(18, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(19, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(20, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(21, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(22, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(23, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(24, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(25, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(26, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(27, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(28, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(29, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(30, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(31, 10 * 256);
        //    //設定CellStyle
        //    //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


        //    u_sheet_Detail.CreateRow(0);
        //    //u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("id");
        //    u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("工務段");
        //    u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("國道編號");
        //    u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("方向");
        //    u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("里程");
        //    u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("經度");
        //    u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("緯度");
        //    u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("日期");
        //    //u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("範圍");
        //    //u_sheet_Detail.GetRow(0).CreateCell(9).SetCellValue("工作類別");
        //    //u_sheet_Detail.GetRow(0).CreateCell(10).SetCellValue("天氣");
        //    //u_sheet_Detail.GetRow(0).CreateCell(11).SetCellValue("動物類群");
        //    //u_sheet_Detail.GetRow(0).CreateCell(12).SetCellValue("大類");
        //    u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("詳細類群");
        //    //u_sheet_Detail.GetRow(0).CreateCell(14).SetCellValue("調查者");
        //    //u_sheet_Detail.GetRow(0).CreateCell(15).SetCellValue("可能種類");
        //    u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("可能種類推測");
        //    //u_sheet_Detail.GetRow(0).CreateCell(17).SetCellValue("上傳檔名");
        //    //u_sheet_Detail.GetRow(0).CreateCell(18).SetCellValue("上傳日期");
        //    //u_sheet_Detail.GetRow(0).CreateCell(19).SetCellValue("後送");
        //    //u_sheet_Detail.GetRow(0).CreateCell(20).SetCellValue("備註");
        //    //u_sheet_Detail.GetRow(0).CreateCell(21).SetCellValue("照片1");
        //    //u_sheet_Detail.GetRow(0).CreateCell(22).SetCellValue("照片1路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(23).SetCellValue("照片2");
        //    //u_sheet_Detail.GetRow(0).CreateCell(24).SetCellValue("照片2路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(25).SetCellValue("照片3");
        //    //u_sheet_Detail.GetRow(0).CreateCell(26).SetCellValue("照片3路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(27).SetCellValue("照片4");
        //    //u_sheet_Detail.GetRow(0).CreateCell(28).SetCellValue("照片4路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(29).SetCellValue("照片5");
        //    //u_sheet_Detail.GetRow(0).CreateCell(30).SetCellValue("照片5路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(31).SetCellValue("照片壓縮檔名");

        //    //設定CellStyle
        //    u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(6).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(7).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(8).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(9).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(10).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(12).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(13).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(14).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(15).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(16).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(17).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(18).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(19).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(20).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(21).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(22).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(23).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(24).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(25).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(26).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(27).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(28).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(29).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(30).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(31).CellStyle = style_wrap_center;
        //    int k = 0;

        //    // 比較表 資料
        //    foreach (DataRow pDR_FSData in dt_excel.Rows)
        //    {
        //        if (pDR_FSData[0] != DBNull.Value)
        //        {
        //            k++;

        //            u_sheet_Detail.CreateRow(k);
        //            //u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["id"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["site_ch"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["highway_id"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["direction"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["milestone"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["x"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["y"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["date"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["range"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(9).SetCellValue(pDR_FSData["type"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(10).SetCellValue(pDR_FSData["weather"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(11).SetCellValue(pDR_FSData["animal"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(12).SetCellValue(pDR_FSData["detail_animal"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["animal2"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(14).SetCellValue(pDR_FSData["collecter_ch"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(15).SetCellValue(pDR_FSData["species"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["deduce_species"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(17).SetCellValue(pDR_FSData["upload_file"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(18).SetCellValue(pDR_FSData["upload_date"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(19).SetCellValue(pDR_FSData["transfer"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(20).SetCellValue(pDR_FSData["note"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(21).SetCellValue(pDR_FSData["file1"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(22).SetCellValue(pDR_FSData["file1_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(23).SetCellValue(pDR_FSData["file2"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(24).SetCellValue(pDR_FSData["file2_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(25).SetCellValue(pDR_FSData["file3"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(26).SetCellValue(pDR_FSData["file3_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(27).SetCellValue(pDR_FSData["file4"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(28).SetCellValue(pDR_FSData["file4_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(29).SetCellValue(pDR_FSData["file5"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(30).SetCellValue(pDR_FSData["file5_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(31).SetCellValue(pDR_FSData["image_file"].ToString());

        //            //設定CellStyle
        //            u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(6).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(7).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(8).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(9).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(10).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(11).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(12).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(13).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(14).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(15).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(16).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(17).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(18).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(19).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(20).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(21).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(22).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(23).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(24).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(25).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(26).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(27).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(28).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(29).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(30).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(31).CellStyle = style_wrap_left;
        //        }
        //    }

        //    /*
        //    u_sheet_Detail.AutoSizeColumn(0);
        //    u_sheet_Detail.AutoSizeColumn(1);
        //    u_sheet_Detail.AutoSizeColumn(2);
        //    u_sheet_Detail.AutoSizeColumn(3);
        //    u_sheet_Detail.AutoSizeColumn(4);
        //    u_sheet_Detail.AutoSizeColumn(5);
        //    u_sheet_Detail.AutoSizeColumn(6);
        //    u_sheet_Detail.AutoSizeColumn(7);
        //    u_sheet_Detail.AutoSizeColumn(8);
        //    u_sheet_Detail.AutoSizeColumn(9);
        //    u_sheet_Detail.AutoSizeColumn(10);
        //    u_sheet_Detail.AutoSizeColumn(11);
        //    u_sheet_Detail.AutoSizeColumn(12);
        //    u_sheet_Detail.AutoSizeColumn(13);
        //    u_sheet_Detail.AutoSizeColumn(14);
        //    u_sheet_Detail.AutoSizeColumn(15);
        //    u_sheet_Detail.AutoSizeColumn(16);
        //    u_sheet_Detail.AutoSizeColumn(17);
        //    u_sheet_Detail.AutoSizeColumn(18);
        //    u_sheet_Detail.AutoSizeColumn(19);
        //    u_sheet_Detail.AutoSizeColumn(20);
        //    u_sheet_Detail.AutoSizeColumn(21);
        //    u_sheet_Detail.AutoSizeColumn(22);
        //    u_sheet_Detail.AutoSizeColumn(23);
        //    u_sheet_Detail.AutoSizeColumn(24);
        //    u_sheet_Detail.AutoSizeColumn(25);
        //    u_sheet_Detail.AutoSizeColumn(26);
        //    u_sheet_Detail.AutoSizeColumn(27);
        //    u_sheet_Detail.AutoSizeColumn(28);
        //    u_sheet_Detail.AutoSizeColumn(29);
        //    u_sheet_Detail.AutoSizeColumn(30);
        //    u_sheet_Detail.AutoSizeColumn(31);
        //    */
        //    workbook.Write(ms);

        //    //將EXCEL檔案匯出到系統資料儲存目錄
        //    //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
        //    //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
        //    string Excel_ShortTime = "roadkill_new.csv";
        //    string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
        //    FileStream fs = new FileStream(folderPath, FileMode.Create, FileAccess.Write);
        //    fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
        //    fs.Flush();
        //    fs.Close();
        //    workbook = null;
        //    ms.Close();
        //    ms.Dispose();
        //}
        try
        {
            string Excel_ShortTime = "roadkill.csv";
            string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
            using (FileStream fs = new FileStream(folderPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine("工務段,國道編號,方向,里程,經度,緯度,日期,詳細類群,可能種類推測");
                foreach (DataRow dr in dt_excel.Rows)
                {
                    sw.WriteLine(dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString() + "," + dr[6].ToString() + "," + dr[7].ToString() + "," + dr[8].ToString());
                }
                sw.Close();
            }

            System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
            byte[] xfile = null;
            //string docupath = Request.PhysicalApplicationPath;
            xfile = wc.DownloadData(folderPath);
            string xfileName = System.IO.Path.GetFileName(folderPath);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
            HttpContext.Current.Response.ContentType = "application/octet-stream"; //二進位方式
            HttpContext.Current.Response.BinaryWrite(xfile); //內容轉出作檔案下載
            HttpContext.Current.Response.End();

            //pDT_Account_Payables_Different_Detail = null;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void Export_Excel_History(object sender, EventArgs e)
    {
        SqlDataSource1.SelectCommand = "Select id , site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , range ,type , weather , animal , detail_animal , animal2 , collecter_ch , species , deduce_species , upload_file , Replace(CONVERT (char(10), upload_date, 126),'1900-01-01','') AS  upload_date , transfer , note , Case When file1 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file1 ELSE '' End AS file1_url , file1  , Case When file2 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file2 ELSE '' End AS file2_url , file2  ,Case When file3 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file3 ELSE '' End AS file3_url , file3  ,Case When file4 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file4 ELSE '' End AS file4_url , file4  ,Case When file5 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file5 ELSE '' End AS file5_url , file5     From roadkill_new ";
        //SqlDataSource1.SelectCommand = "Select site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , animal2 , deduce_species From roadkill_new ";

        GridView_List.PageSize = 100000;
        //移除最後的連結欄位
        GridView_List.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        GridView_List.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=Roadkill_" + Excel_ShortTime + ".xls");
        //Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        Response.ContentEncoding = Encoding.GetEncoding("BIG5");
        //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentType = "application/ms-excel";
        GridView_List.EnableViewState = false;
        GridView_List.AllowPaging = false;
        GridView_List.AllowSorting = false;
        StringWriter objStringWriter = new StringWriter();
        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        GridView_List.RenderControl(objHtmlTextWriter);

        Response.Write(objStringWriter.ToString());
        Response.End();

    }

    protected void Export_Excel_History_new(object sender, EventArgs e)
    {

        string sql = "Select site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date ,animal2 , deduce_species From roadkill_new     ";
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // 建立 SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        //2018/06/07 mark by ian
        //{
        //// 執行命令
        ////dt_excel = cmd.ExecuteReader().;

        //// 清理命令和連線物件。
        //cmd.Dispose();
        //conn.Dispose();

        //System.DateTime now = new System.DateTime();
        //now = System.DateTime.Now;
        ////ExcelExporter export = new ExcelExporter();
        //try
        //{

        //    // 產生EXCEL Sheet 設定
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    MemoryStream ms = new MemoryStream();
        //    // Detail Sheet
        //    ISheet u_sheet_Detail = workbook.CreateSheet("roadkill");

        //    // CellStyle
        //    IDataFormat num_format = workbook.CreateDataFormat();
        //    IFont content_font = workbook.CreateFont();
        //    content_font.FontName = "新細明體";
        //    content_font.FontHeightInPoints = 12;

        //    ICellStyle style_wrap_right = workbook.CreateCellStyle();
        //    //style_wrap_right.DataFormat = num_format.GetFormat("###,###,##0.00");
        //    style_wrap_right.Alignment = HorizontalAlignment.Right;
        //    style_wrap_right.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_right.WrapText = true;
        //    style_wrap_right.SetFont(content_font);

        //    ICellStyle style_wrap_left = workbook.CreateCellStyle();
        //    style_wrap_left.Alignment = HorizontalAlignment.Left;
        //    style_wrap_left.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_left.WrapText = true;
        //    style_wrap_left.SetFont(content_font);

        //    ICellStyle style_wrap_center = workbook.CreateCellStyle();
        //    style_wrap_center.Alignment = HorizontalAlignment.Center;
        //    style_wrap_center.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_center.WrapText = true;
        //    style_wrap_center.SetFont(content_font);

        //    ICellStyle style_wrap_right_unit = workbook.CreateCellStyle();
        //    //style_wrap_right_unit.DataFormat = num_format.GetFormat("###,##0.000000");
        //    style_wrap_right_unit.Alignment = HorizontalAlignment.Right;
        //    style_wrap_right_unit.VerticalAlignment = VerticalAlignment.Center;
        //    style_wrap_right_unit.WrapText = true;
        //    style_wrap_right_unit.SetFont(content_font);

        //    // 差異表工作表 欄寬
        //    u_sheet_Detail.SetColumnWidth(0, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(1, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(2, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(3, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(4, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(5, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(6, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(7, 10 * 256);
        //    u_sheet_Detail.SetColumnWidth(8, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(9, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(10, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(11, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(12, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(13, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(14, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(15, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(16, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(17, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(18, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(19, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(20, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(21, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(22, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(23, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(24, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(25, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(26, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(27, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(28, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(29, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(30, 10 * 256);
        //    //u_sheet_Detail.SetColumnWidth(31, 10 * 256);
        //    //設定CellStyle
        //    //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


        //    u_sheet_Detail.CreateRow(0);
        //    //u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("id");
        //    u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("工務段");
        //    u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("國道編號");
        //    u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("方向");
        //    u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("里程");
        //    u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("經度");
        //    u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("緯度");
        //    u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("日期");
        //    //u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("範圍");
        //    //u_sheet_Detail.GetRow(0).CreateCell(9).SetCellValue("工作類別");
        //    //u_sheet_Detail.GetRow(0).CreateCell(10).SetCellValue("天氣");
        //    //u_sheet_Detail.GetRow(0).CreateCell(11).SetCellValue("動物類群");
        //    //u_sheet_Detail.GetRow(0).CreateCell(12).SetCellValue("大類");
        //    u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("詳細類群");
        //    //u_sheet_Detail.GetRow(0).CreateCell(14).SetCellValue("調查者");
        //    //u_sheet_Detail.GetRow(0).CreateCell(15).SetCellValue("可能種類");
        //    u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("可能種類推測");
        //    //u_sheet_Detail.GetRow(0).CreateCell(17).SetCellValue("上傳檔名");
        //    //u_sheet_Detail.GetRow(0).CreateCell(18).SetCellValue("上傳日期");
        //    //u_sheet_Detail.GetRow(0).CreateCell(19).SetCellValue("後送");
        //    //u_sheet_Detail.GetRow(0).CreateCell(20).SetCellValue("備註");
        //    //u_sheet_Detail.GetRow(0).CreateCell(21).SetCellValue("照片1");
        //    //u_sheet_Detail.GetRow(0).CreateCell(22).SetCellValue("照片1路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(23).SetCellValue("照片2");
        //    //u_sheet_Detail.GetRow(0).CreateCell(24).SetCellValue("照片2路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(25).SetCellValue("照片3");
        //    //u_sheet_Detail.GetRow(0).CreateCell(26).SetCellValue("照片3路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(27).SetCellValue("照片4");
        //    //u_sheet_Detail.GetRow(0).CreateCell(28).SetCellValue("照片4路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(29).SetCellValue("照片5");
        //    //u_sheet_Detail.GetRow(0).CreateCell(30).SetCellValue("照片5路徑");
        //    //u_sheet_Detail.GetRow(0).CreateCell(31).SetCellValue("照片壓縮檔名");

        //    //設定CellStyle
        //    u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(6).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(7).CellStyle = style_wrap_center;
        //    u_sheet_Detail.GetRow(0).GetCell(8).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(9).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(10).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(12).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(13).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(14).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(15).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(16).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(17).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(18).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(19).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(20).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(21).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(22).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(23).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(24).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(25).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(26).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(27).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(28).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(29).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(30).CellStyle = style_wrap_center;
        //    //u_sheet_Detail.GetRow(0).GetCell(31).CellStyle = style_wrap_center;
        //    int k = 0;

        //    // 比較表 資料
        //    foreach (DataRow pDR_FSData in dt_excel.Rows)
        //    {
        //        if (pDR_FSData[0] != DBNull.Value)
        //        {
        //            k++;

        //            u_sheet_Detail.CreateRow(k);
        //            //u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["id"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["site_ch"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["highway_id"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["direction"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["milestone"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["x"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["y"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["date"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["range"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(9).SetCellValue(pDR_FSData["type"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(10).SetCellValue(pDR_FSData["weather"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(11).SetCellValue(pDR_FSData["animal"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(12).SetCellValue(pDR_FSData["detail_animal"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["animal2"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(14).SetCellValue(pDR_FSData["collecter_ch"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(15).SetCellValue(pDR_FSData["species"].ToString());
        //            u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["deduce_species"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(17).SetCellValue(pDR_FSData["upload_file"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(18).SetCellValue(pDR_FSData["upload_date"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(19).SetCellValue(pDR_FSData["transfer"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(20).SetCellValue(pDR_FSData["note"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(21).SetCellValue(pDR_FSData["file1"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(22).SetCellValue(pDR_FSData["file1_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(23).SetCellValue(pDR_FSData["file2"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(24).SetCellValue(pDR_FSData["file2_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(25).SetCellValue(pDR_FSData["file3"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(26).SetCellValue(pDR_FSData["file3_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(27).SetCellValue(pDR_FSData["file4"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(28).SetCellValue(pDR_FSData["file4_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(29).SetCellValue(pDR_FSData["file5"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(30).SetCellValue(pDR_FSData["file5_url"].ToString());
        //            //u_sheet_Detail.GetRow(k).CreateCell(31).SetCellValue(pDR_FSData["image_file"].ToString());

        //            //設定CellStyle
        //            u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(6).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(7).CellStyle = style_wrap_left;
        //            u_sheet_Detail.GetRow(k).GetCell(8).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(9).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(10).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(11).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(12).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(13).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(14).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(15).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(16).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(17).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(18).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(19).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(20).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(21).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(22).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(23).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(24).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(25).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(26).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(27).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(28).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(29).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(30).CellStyle = style_wrap_left;
        //            //u_sheet_Detail.GetRow(k).GetCell(31).CellStyle = style_wrap_left;
        //        }
        //    }

        //    /*
        //    u_sheet_Detail.AutoSizeColumn(0);
        //    u_sheet_Detail.AutoSizeColumn(1);
        //    u_sheet_Detail.AutoSizeColumn(2);
        //    u_sheet_Detail.AutoSizeColumn(3);
        //    u_sheet_Detail.AutoSizeColumn(4);
        //    u_sheet_Detail.AutoSizeColumn(5);
        //    u_sheet_Detail.AutoSizeColumn(6);
        //    u_sheet_Detail.AutoSizeColumn(7);
        //    u_sheet_Detail.AutoSizeColumn(8);
        //    u_sheet_Detail.AutoSizeColumn(9);
        //    u_sheet_Detail.AutoSizeColumn(10);
        //    u_sheet_Detail.AutoSizeColumn(11);
        //    u_sheet_Detail.AutoSizeColumn(12);
        //    u_sheet_Detail.AutoSizeColumn(13);
        //    u_sheet_Detail.AutoSizeColumn(14);
        //    u_sheet_Detail.AutoSizeColumn(15);
        //    u_sheet_Detail.AutoSizeColumn(16);
        //    u_sheet_Detail.AutoSizeColumn(17);
        //    u_sheet_Detail.AutoSizeColumn(18);
        //    u_sheet_Detail.AutoSizeColumn(19);
        //    u_sheet_Detail.AutoSizeColumn(20);
        //    u_sheet_Detail.AutoSizeColumn(21);
        //    u_sheet_Detail.AutoSizeColumn(22);
        //    u_sheet_Detail.AutoSizeColumn(23);
        //    u_sheet_Detail.AutoSizeColumn(24);
        //    u_sheet_Detail.AutoSizeColumn(25);
        //    u_sheet_Detail.AutoSizeColumn(26);
        //    u_sheet_Detail.AutoSizeColumn(27);
        //    u_sheet_Detail.AutoSizeColumn(28);
        //    u_sheet_Detail.AutoSizeColumn(29);
        //    u_sheet_Detail.AutoSizeColumn(30);
        //    u_sheet_Detail.AutoSizeColumn(31);
        //    */
        //    workbook.Write(ms);

        //    //將EXCEL檔案匯出到系統資料儲存目錄
        //    //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
        //    //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
        //    string Excel_ShortTime = "roadkill_new.csv";
        //    string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
        //    FileStream fs = new FileStream(folderPath, FileMode.Create, FileAccess.Write);
        //    fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
        //    fs.Flush();
        //    fs.Close();
        //    workbook = null;
        //    ms.Close();
        //    ms.Dispose();
        //}
        try
        {
            string Excel_ShortTime = "roadkill_new.csv";
            string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
            using (FileStream fs = new FileStream(folderPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine("工務段,國道編號,方向,里程,經度,緯度,日期,詳細類群,可能種類推測");
                foreach (DataRow dr in dt_excel.Rows)
                {
                    sw.WriteLine(dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString() + "," + dr[6].ToString() + "," + dr[7].ToString() + "," + dr[8].ToString());
                }
                sw.Close();
            }

            System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
            byte[] xfile = null;
            //string docupath = Request.PhysicalApplicationPath;
            xfile = wc.DownloadData(folderPath);
            string xfileName = System.IO.Path.GetFileName(folderPath);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
            HttpContext.Current.Response.ContentType = "application/octet-stream"; //二進位方式
            HttpContext.Current.Response.BinaryWrite(xfile); //內容轉出作檔案下載
            HttpContext.Current.Response.End();

            //pDT_Account_Payables_Different_Detail = null;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void Export_Excel_History_new2(object sender, EventArgs e)
    {

        string sql = "Select id , site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , range ,type , weather , animal , detail_animal , animal2 , collecter_ch , species , deduce_species , upload_file , Replace(CONVERT (char(10), upload_date, 126),'1900-01-01','') AS  upload_date , transfer , note , Case When file1 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file1 ELSE '' End AS file1_url , file1  , Case When file2 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file2 ELSE '' End AS file2_url , file2  ,Case When file3 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file3 ELSE '' End AS file3_url , file3  ,Case When file4 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file4 ELSE '' End AS file4_url , file4  ,Case When file5 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file5 ELSE '' End AS file5_url , file5 ,  Case When image_file <> '' Then 'https://ecodata.freeway.gov.tw' + path1+image_file ELSE '' End AS image_file  From roadkill_new ";
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // 建立 SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        
        // 執行命令
        //dt_excel = cmd.ExecuteReader().;

        // 清理命令和連線物件。
        cmd.Dispose();
        conn.Dispose();

        System.DateTime now = new System.DateTime();
        now = System.DateTime.Now;
        //ExcelExporter export = new ExcelExporter();
        try
        {
            // 產生EXCEL Sheet 設定
            XSSFWorkbook workbook = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            // Detail Sheet
            ISheet u_sheet_Detail = workbook.CreateSheet("roadkill");

            // CellStyle
            IDataFormat num_format = workbook.CreateDataFormat();
            IFont content_font = workbook.CreateFont();
            content_font.FontName = "新細明體";
            content_font.FontHeightInPoints = 12;

            ICellStyle style_wrap_right = workbook.CreateCellStyle();
            //style_wrap_right.DataFormat = num_format.GetFormat("###,###,##0.00");
            style_wrap_right.Alignment = HorizontalAlignment.Right;
            style_wrap_right.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_right.WrapText = true;
            style_wrap_right.SetFont(content_font);

            ICellStyle style_wrap_left = workbook.CreateCellStyle();
            style_wrap_left.Alignment = HorizontalAlignment.Left;
            style_wrap_left.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_left.WrapText = true;
            style_wrap_left.SetFont(content_font);

            ICellStyle style_wrap_center = workbook.CreateCellStyle();
            style_wrap_center.Alignment = HorizontalAlignment.Center;
            style_wrap_center.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_center.WrapText = true;
            style_wrap_center.SetFont(content_font);

            ICellStyle style_wrap_right_unit = workbook.CreateCellStyle();
            //style_wrap_right_unit.DataFormat = num_format.GetFormat("###,##0.000000");
            style_wrap_right_unit.Alignment = HorizontalAlignment.Right;
            style_wrap_right_unit.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_right_unit.WrapText = true;
            style_wrap_right_unit.SetFont(content_font);

            // 差異表工作表 欄寬
            u_sheet_Detail.SetColumnWidth(0, 10 * 256);
            u_sheet_Detail.SetColumnWidth(1, 10 * 256);
            u_sheet_Detail.SetColumnWidth(2, 10 * 256);
            u_sheet_Detail.SetColumnWidth(3, 10 * 256);
            u_sheet_Detail.SetColumnWidth(4, 10 * 256);
            u_sheet_Detail.SetColumnWidth(5, 10 * 256);
            u_sheet_Detail.SetColumnWidth(6, 10 * 256);
            u_sheet_Detail.SetColumnWidth(7, 10 * 256);
            u_sheet_Detail.SetColumnWidth(8, 10 * 256);
            u_sheet_Detail.SetColumnWidth(9, 10 * 256);
            u_sheet_Detail.SetColumnWidth(10, 10 * 256);
            u_sheet_Detail.SetColumnWidth(11, 10 * 256);
            u_sheet_Detail.SetColumnWidth(12, 10 * 256);
            u_sheet_Detail.SetColumnWidth(13, 10 * 256);
            u_sheet_Detail.SetColumnWidth(14, 10 * 256);
            u_sheet_Detail.SetColumnWidth(15, 10 * 256);
            u_sheet_Detail.SetColumnWidth(16, 10 * 256);
            u_sheet_Detail.SetColumnWidth(17, 10 * 256);
            u_sheet_Detail.SetColumnWidth(18, 10 * 256);
            u_sheet_Detail.SetColumnWidth(19, 10 * 256);
            u_sheet_Detail.SetColumnWidth(20, 10 * 256);
            u_sheet_Detail.SetColumnWidth(21, 10 * 256);
            u_sheet_Detail.SetColumnWidth(22, 10 * 256);
            u_sheet_Detail.SetColumnWidth(23, 10 * 256);
            u_sheet_Detail.SetColumnWidth(24, 10 * 256);
            u_sheet_Detail.SetColumnWidth(25, 10 * 256);
            u_sheet_Detail.SetColumnWidth(26, 10 * 256);
            u_sheet_Detail.SetColumnWidth(27, 10 * 256);
            u_sheet_Detail.SetColumnWidth(28, 10 * 256);
            u_sheet_Detail.SetColumnWidth(29, 10 * 256);
            u_sheet_Detail.SetColumnWidth(30, 10 * 256);
            u_sheet_Detail.SetColumnWidth(31, 10 * 256);
            //設定CellStyle
            //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("id");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("工務段");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("國道編號");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("方向");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("里程");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("經度");
            u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("緯度");
            u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("日期");
            u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("範圍");
            u_sheet_Detail.GetRow(0).CreateCell(9).SetCellValue("工作類別");
            u_sheet_Detail.GetRow(0).CreateCell(10).SetCellValue("天氣");
            u_sheet_Detail.GetRow(0).CreateCell(11).SetCellValue("動物類群");
            u_sheet_Detail.GetRow(0).CreateCell(12).SetCellValue("大類");
            u_sheet_Detail.GetRow(0).CreateCell(13).SetCellValue("詳細類群");
            u_sheet_Detail.GetRow(0).CreateCell(14).SetCellValue("調查者");
            u_sheet_Detail.GetRow(0).CreateCell(15).SetCellValue("可能種類");
            u_sheet_Detail.GetRow(0).CreateCell(16).SetCellValue("可能種類推測");
            u_sheet_Detail.GetRow(0).CreateCell(17).SetCellValue("上傳檔名");
            u_sheet_Detail.GetRow(0).CreateCell(18).SetCellValue("上傳日期");
            u_sheet_Detail.GetRow(0).CreateCell(19).SetCellValue("後送");
            u_sheet_Detail.GetRow(0).CreateCell(20).SetCellValue("備註");
            u_sheet_Detail.GetRow(0).CreateCell(21).SetCellValue("照片1");
            u_sheet_Detail.GetRow(0).CreateCell(22).SetCellValue("照片1路徑");
            u_sheet_Detail.GetRow(0).CreateCell(23).SetCellValue("照片2");
            u_sheet_Detail.GetRow(0).CreateCell(24).SetCellValue("照片2路徑");
            u_sheet_Detail.GetRow(0).CreateCell(25).SetCellValue("照片3");
            u_sheet_Detail.GetRow(0).CreateCell(26).SetCellValue("照片3路徑");
            u_sheet_Detail.GetRow(0).CreateCell(27).SetCellValue("照片4");
            u_sheet_Detail.GetRow(0).CreateCell(28).SetCellValue("照片4路徑");
            u_sheet_Detail.GetRow(0).CreateCell(29).SetCellValue("照片5");
            u_sheet_Detail.GetRow(0).CreateCell(30).SetCellValue("照片5路徑");
            u_sheet_Detail.GetRow(0).CreateCell(31).SetCellValue("照片壓縮檔名");

            //設定CellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(6).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(7).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(8).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(9).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(10).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(12).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(13).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(14).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(15).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(16).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(17).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(18).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(19).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(20).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(21).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(22).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(23).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(24).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(25).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(26).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(27).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(28).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(29).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(30).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(31).CellStyle = style_wrap_center;
            int k = 0;

             //比較表 資料
            foreach (DataRow pDR_FSData in dt_excel.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;

                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["site_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["highway_id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["direction"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["milestone"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["x"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["y"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["range"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(9).SetCellValue(pDR_FSData["type"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(10).SetCellValue(pDR_FSData["weather"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(11).SetCellValue(pDR_FSData["animal"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(12).SetCellValue(pDR_FSData["detail_animal"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(13).SetCellValue(pDR_FSData["animal2"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(14).SetCellValue(pDR_FSData["collecter_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(15).SetCellValue(pDR_FSData["species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(16).SetCellValue(pDR_FSData["deduce_species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(17).SetCellValue(pDR_FSData["upload_file"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(18).SetCellValue(pDR_FSData["upload_date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(19).SetCellValue(pDR_FSData["transfer"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(20).SetCellValue(pDR_FSData["note"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(21).SetCellValue(pDR_FSData["file1"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(22).SetCellValue(pDR_FSData["file1_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(23).SetCellValue(pDR_FSData["file2"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(24).SetCellValue(pDR_FSData["file2_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(25).SetCellValue(pDR_FSData["file3"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(26).SetCellValue(pDR_FSData["file3_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(27).SetCellValue(pDR_FSData["file4"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(28).SetCellValue(pDR_FSData["file4_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(29).SetCellValue(pDR_FSData["file5"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(30).SetCellValue(pDR_FSData["file5_url"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(31).SetCellValue(pDR_FSData["image_file"].ToString());

                    //設定CellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(6).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(7).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(8).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(9).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(10).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(11).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(12).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(13).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(14).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(15).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(16).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(17).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(18).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(19).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(20).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(21).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(22).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(23).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(24).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(25).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(26).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(27).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(28).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(29).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(30).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(31).CellStyle = style_wrap_left;
                }
            }

            /*
            u_sheet_Detail.AutoSizeColumn(0);
            u_sheet_Detail.AutoSizeColumn(1);
            u_sheet_Detail.AutoSizeColumn(2);
            u_sheet_Detail.AutoSizeColumn(3);
            u_sheet_Detail.AutoSizeColumn(4);
            u_sheet_Detail.AutoSizeColumn(5);
            u_sheet_Detail.AutoSizeColumn(6);
            u_sheet_Detail.AutoSizeColumn(7);
            u_sheet_Detail.AutoSizeColumn(8);
            u_sheet_Detail.AutoSizeColumn(9);
            u_sheet_Detail.AutoSizeColumn(10);
            u_sheet_Detail.AutoSizeColumn(11);
            u_sheet_Detail.AutoSizeColumn(12);
            u_sheet_Detail.AutoSizeColumn(13);
            u_sheet_Detail.AutoSizeColumn(14);
            u_sheet_Detail.AutoSizeColumn(15);
            u_sheet_Detail.AutoSizeColumn(16);
            u_sheet_Detail.AutoSizeColumn(17);
            u_sheet_Detail.AutoSizeColumn(18);
            u_sheet_Detail.AutoSizeColumn(19);
            u_sheet_Detail.AutoSizeColumn(20);
            u_sheet_Detail.AutoSizeColumn(21);
            u_sheet_Detail.AutoSizeColumn(22);
            u_sheet_Detail.AutoSizeColumn(23);
            u_sheet_Detail.AutoSizeColumn(24);
            u_sheet_Detail.AutoSizeColumn(25);
            u_sheet_Detail.AutoSizeColumn(26);
            u_sheet_Detail.AutoSizeColumn(27);
            u_sheet_Detail.AutoSizeColumn(28);
            u_sheet_Detail.AutoSizeColumn(29);
            u_sheet_Detail.AutoSizeColumn(30);
            u_sheet_Detail.AutoSizeColumn(31);
            */
            workbook.Write(ms);
        
            //將EXCEL檔案匯出到系統資料儲存目錄
            //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "roadkill_new.xlsx";
            string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
            FileStream fs = new FileStream(folderPath, FileMode.Create, FileAccess.Write);
            fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
            fs.Flush();
            fs.Close();
            workbook = null;
            ms.Close();
            ms.Dispose();
        
            System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
            byte[] xfile = null;
            //string docupath = Request.PhysicalApplicationPath;
            xfile = wc.DownloadData(folderPath);
            string xfileName = System.IO.Path.GetFileName(folderPath);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
            HttpContext.Current.Response.ContentType = "application/octet-stream"; //二進位方式
            HttpContext.Current.Response.BinaryWrite(xfile); //內容轉出作檔案下載
            HttpContext.Current.Response.End();

            //pDT_Account_Payables_Different_Detail = null;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void Export_Excel_csv(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String sqlCommand = "Select id , site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , range ,type , weather , animal , detail_animal , animal2 , collecter_ch , species , deduce_species , upload_file , Replace(CONVERT (char(10), upload_date, 126),'1900-01-01','') AS  upload_date , transfer , note , Case When file1 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file1 ELSE '' End AS file1_url , file1  , Case When file2 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file2 ELSE '' End AS file2_url , file2  ,Case When file3 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file3 ELSE '' End AS file3_url , file3  ,Case When file4 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file4 ELSE '' End AS file4_url , file4  ,Case When file5 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file5 ELSE '' End AS file5_url , file5 , Case When image_file <> '' Then 'https://ecodata.freeway.gov.tw' + path1+image_file ELSE '' End AS image_file   From roadkill     ";

        string title = "id,工務段,國道編號,方向,里程,經度,緯度,日期,範圍,工作類別,天氣,動物類群,大類,詳細類群,調查者,可能種類,可能種類推測,上傳檔名,上傳日期,後送,備註,照片1,照片1路徑,照片2,照片2路徑,照片3,照片3路徑,照片4,照片4路徑,照片5,照片5路徑,照片壓縮檔名,";

        StringWriter sw = new StringWriter();


        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                sw.Write(title);
                sw.WriteLine();
                while (reader.Read())
                {
                    sw.Write(reader["id"].ToString() + ",");
                    sw.Write(reader["site_ch"].ToString() + ",");
                    sw.Write(reader["highway_id"].ToString() + ",");
                    sw.Write(reader["direction"].ToString() + ",");
                    sw.Write(reader["milestone"].ToString() + ",");
                    sw.Write(reader["x"].ToString() + ",");
                    sw.Write(reader["y"].ToString() + ",");
                    sw.Write(reader["date"].ToString() + ",");
                    sw.Write(reader["range"].ToString() + ",");
                    sw.Write(reader["type"].ToString() + ",");
                    sw.Write(reader["weather"].ToString() + ",");
                    sw.Write(reader["animal"].ToString() + ",");
                    sw.Write(reader["detail_animal"].ToString() + ",");
                    sw.Write(reader["animal2"].ToString() + ",");
                    sw.Write(reader["collecter_ch"].ToString() + ",");
                    sw.Write(reader["species"].ToString() + ",");
                    sw.Write(reader["deduce_species"].ToString() + ",");
                    sw.Write(reader["upload_file"].ToString() + ",");
                    sw.Write(reader["upload_date"].ToString() + ",");
                    sw.Write(reader["transfer"].ToString() + ",");
                    sw.Write(reader["note"].ToString() + ",");
                    sw.Write(reader["file1"].ToString() + ",");
                    sw.Write(reader["file1_url"].ToString() + ",");
                    sw.Write(reader["file2"].ToString() + ",");
                    sw.Write(reader["file2_url"].ToString() + ",");
                    sw.Write(reader["file3"].ToString() + ",");
                    sw.Write(reader["file3_url"].ToString() + ",");
                    sw.Write(reader["file4"].ToString() + ",");
                    sw.Write(reader["file4_url"].ToString() + ",");
                    sw.Write(reader["file5"].ToString() + ",");
                    sw.Write(reader["file5_url"].ToString() + ",");
                    sw.Write(reader["image_file"].ToString() + ",");
                    sw.WriteLine();
                }
            }

            string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            Response.Charset = "BIG5";
            Response.AppendHeader("Content-Disposition", "attachment;filename=Roadkill_" + Excel_ShortTime + ".csv");

            Response.ContentType = "application/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("BIG5");
            Response.Write(sw);

            //Response.ContentEncoding = Encoding.GetEncoding("BIG5");

            Response.End();
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

        finally
        {
            conn.Close();
            conn = null;
            cmd = null;

        }

    }

    protected void Export_Excel_History_csv(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String sqlCommand = "Select id , site_ch , highway_id , direction , milestone , x , y , Replace(CONVERT (char(10), date, 126),'1900-01-01','') AS  date , range ,type , weather , animal , detail_animal , animal2 , collecter_ch , species , deduce_species , upload_file , Replace(CONVERT (char(10), upload_date, 126),'1900-01-01','') AS  upload_date , transfer , note , Case When file1 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file1 ELSE '' End AS file1_url , file1  , Case When file2 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file2 ELSE '' End AS file2_url , file2  ,Case When file3 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file3 ELSE '' End AS file3_url , file3  ,Case When file4 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file4 ELSE '' End AS file4_url , file4  ,Case When file5 <> '' Then 'https://ecodata.freeway.gov.tw' + path1+file5 ELSE '' End AS file5_url , file5     From roadkill_new     ";

        string title = "id,工務段,國道編號,方向,里程,經度,緯度,日期,範圍,工作類別,天氣,動物類群,大類,詳細類群,調查者,可能種類,可能種類推測,上傳檔名,上傳日期,後送,備註,照片1,照片1路徑,照片2,照片2路徑,照片3,照片3路徑,照片4,照片4路徑,照片5,照片5路徑,";

        StringWriter sw = new StringWriter();


        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                sw.Write(title);
                sw.WriteLine();
                while (reader.Read())
                {
                    sw.Write(reader["id"].ToString() + ",");
                    sw.Write(reader["site_ch"].ToString() + ",");
                    sw.Write(reader["highway_id"].ToString() + ",");
                    sw.Write(reader["direction"].ToString() + ",");
                    sw.Write(reader["milestone"].ToString() + ",");
                    sw.Write(reader["x"].ToString() + ",");
                    sw.Write(reader["y"].ToString() + ",");
                    sw.Write(reader["date"].ToString() + ",");
                    sw.Write(reader["range"].ToString() + ",");
                    sw.Write(reader["type"].ToString() + ",");
                    sw.Write(reader["weather"].ToString() + ",");
                    sw.Write(reader["animal"].ToString() + ",");
                    sw.Write(reader["detail_animal"].ToString() + ",");
                    sw.Write(reader["animal2"].ToString() + ",");
                    sw.Write(reader["collecter_ch"].ToString() + ",");
                    sw.Write(reader["species"].ToString() + ",");
                    sw.Write(reader["deduce_species"].ToString() + ",");
                    sw.Write(reader["upload_file"].ToString() + ",");
                    sw.Write(reader["upload_date"].ToString() + ",");
                    sw.Write(reader["transfer"].ToString() + ",");
                    sw.Write(reader["note"].ToString() + ",");
                    sw.Write(reader["file1"].ToString() + ",");
                    sw.Write(reader["file1_url"].ToString() + ",");
                    sw.Write(reader["file2"].ToString() + ",");
                    sw.Write(reader["file2_url"].ToString() + ",");
                    sw.Write(reader["file3"].ToString() + ",");
                    sw.Write(reader["file3_url"].ToString() + ",");
                    sw.Write(reader["file4"].ToString() + ",");
                    sw.Write(reader["file4_url"].ToString() + ",");
                    sw.Write(reader["file5"].ToString() + ",");
                    sw.Write(reader["file5_url"].ToString() + ",");
                    sw.WriteLine();
                }
            }

            string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            Response.Charset = "BIG5";
            Response.AppendHeader("Content-Disposition", "attachment;filename=Roadkill_" + Excel_ShortTime + ".csv");

            Response.ContentType = "application/csv";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("BIG5");
            Response.Write(sw);

            //Response.ContentEncoding = Encoding.GetEncoding("BIG5");

            Response.End();
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

        finally
        {
            conn.Close();
            conn = null;
            cmd = null;

        }

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }
}
