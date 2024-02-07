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
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI;

public partial class View_fun11_01 : System.Web.UI.Page
{
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        Query();
    }

    protected void Query()
    {

        string sql = " Select a.id , CONVERT (char(10), a.date, 126) AS date , CONVERT(CHAR(12), a.time, 114) AS time , a.highway_id , a.mileage  , a.reporter , a.institution , a.species , a.status , Case When a.file1 <> '' then a.path1+a.file1 Else file1 End file1 , a.path1 , a.owner From Roadkill_bulletin a Where 1=1 ";
        
        string value_KeyWords = KeyWords.Text;

        if (!value_KeyWords.Equals(""))
        {
            sql += " And ( a.date Like '%" + value_KeyWords + "%' ";
            sql += "    or a.time Like '%" + value_KeyWords + "%' ";
            sql += "    or a.mileage Like '%" + value_KeyWords + "%' ";
            sql += "    or a.reporter Like '%" + value_KeyWords + "%' ";
            sql += "    or a.institution Like '%" + value_KeyWords + "%' ";
            sql += "    or a.species Like '%" + value_KeyWords + "%' ";
            sql += "    or a.owner Like '%" + value_KeyWords + "%' ";
            sql += "    or a.status Like '%" + value_KeyWords + "%' )";
        }

        SDS_View.SelectCommand = sql;
        //GridView1.DataSource = SDS_View1;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

        btnShowGVCount_Click1();

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
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
    }


    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }

    protected void Export_Excel(object sender, EventArgs e)
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        view.Dispose();

        GridView_List.PageSize = count;
        //移除最後的連結欄位
        GridView_List.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        GridView_List.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=Road_status_" + Excel_ShortTime + ".xls");
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
        System.DateTime now = new System.DateTime();
        now = System.DateTime.Now;
        //ExcelExporter export = new ExcelExporter();
        try
        {

            // 產生EXCEL Sheet 設定
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            // Detail Sheet
            ISheet u_sheet_Detail = workbook.CreateSheet("Bulletin");

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
            u_sheet_Detail.SetColumnWidth(1, 20 * 256);
            u_sheet_Detail.SetColumnWidth(2, 20 * 256);
            u_sheet_Detail.SetColumnWidth(3, 20 * 256);
            u_sheet_Detail.SetColumnWidth(4, 30 * 256);
            u_sheet_Detail.SetColumnWidth(5, 30 * 256);
            u_sheet_Detail.SetColumnWidth(6, 60 * 256);
            u_sheet_Detail.SetColumnWidth(7, 50 * 256);




            //設定CellStyle
            //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("國道編號");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("里程");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("發現日期");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("發現時間");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("可能種類");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("工程處");
            u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("通報單位");
            u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("狀態");
            

            //設定CellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(6).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(7).CellStyle = style_wrap_center;
           
            int k = 0;

            // 比較表 資料
            foreach (DataRow pDR_FSData in dt_excel.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;

                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["highway_id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["mileage"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["time"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["owner"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["reporter"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["status"].ToString());
                    
                    //設定CellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(6).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(7).CellStyle = style_wrap_left;
                    
                }
            }

            workbook.Write(ms);

            //將EXCEL檔案匯出到系統資料儲存目錄
            //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "Bulletin.xls";
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }

    /*
    protected void btnShowGVCount_Click(object sender, EventArgs e)
    {
        DataView view = (DataView)SDS_View1.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }
    */
}