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


public partial class View_fun01_012 : System.Web.UI.Page
{
    public string map_url = "";
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun01_012_.aspx", "gmap01_012.aspx");
        //iframe1.src = map_url;
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        //Query();
        Query2();
        btnShowGVCount_Click1();
    }

    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }

    protected void Query()
    {
        string value_Short_Name = Request.QueryString[0].ToString();
        string value_StartDate = Request.QueryString[1].ToString();
        string value_EndDate = Request.QueryString[2].ToString();

        //string sql = " SELECT distinct a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid WHERE b.Short_Name =  '" + value_Short_Name + "'";
        string sql = " SELECT distinct a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch,  a.x , a.y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid WHERE b.Short_Name =  '" + value_Short_Name + "'";
        string sql_xy = " SELECT distinct a.x , a.y FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid WHERE b.Short_Name =  '" + value_Short_Name + "' ";


        value_StartDate = value_StartDate.Replace("+-01-01","");
        value_StartDate = value_StartDate.Replace("+", "");
        value_StartDate = value_StartDate.Replace(" ", "");
        value_EndDate = value_EndDate.Replace("+-01-31", "");
        value_EndDate = value_EndDate.Replace("+", "");
        value_EndDate = value_EndDate.Replace(" ", "");
        if (!value_StartDate.Equals("") && !value_EndDate.Equals("")) {
            sql += " and CONVERT (char(10), a.date1, 126) >= '" + value_StartDate + "' and CONVERT (char(10), a.date1, 126) <= '" + value_EndDate + "' ";
            sql_xy += " and CONVERT (char(10), a.date1, 126) >= '" + value_StartDate + "' and CONVERT (char(10), a.date1, 126) <= '" + value_EndDate + "' ";
        }
        
        


        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();
        if (!value_Short_Name.Equals(""))
        {
            Get_XY(sql_xy);
        }

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

    protected void Query2()
    {
        string value_Short_Name = Request.QueryString[0].ToString();
        string value_StartDate = Request.QueryString[1].ToString();
        string value_EndDate = Request.QueryString[2].ToString();
	string value_site_ch = Request.QueryString[3].ToString();

        //string sql = " SELECT distinct a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid WHERE b.Short_Name =  '" + value_Short_Name + "'";
        string sql = " SELECT distinct sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch,  Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 WHERE Short_Name =  '" + value_Short_Name + "'";
        string sql_xy = " SELECT distinct x , y FROM Table_1 WHERE Short_Name =  '" + value_Short_Name + "' ";


        value_StartDate = value_StartDate.Replace("+-01-01", "");
        value_StartDate = value_StartDate.Replace("+", "");
        value_StartDate = value_StartDate.Replace(" ", "");
        value_EndDate = value_EndDate.Replace("+-01-31", "");
        value_EndDate = value_EndDate.Replace("+", "");
        value_EndDate = value_EndDate.Replace(" ", "");
        if (!value_StartDate.Equals("") && !value_EndDate.Equals(""))
        {
            sql += " and CONVERT (char(10), date1, 126) >= '" + value_StartDate + "' and CONVERT (char(10), date1, 126) <= '" + value_EndDate + "' ";
            sql_xy += " and CONVERT (char(10), date1, 126) >= '" + value_StartDate + "' and CONVERT (char(10), date1, 126) <= '" + value_EndDate + "' ";
        }
	if (!value_site_ch.Equals(""))
	{
	    sql += " and site_ch like '%' + @site_ch + '%' ";
	    sql_xy += " and site_ch like '%" + value_site_ch + "%' ";
	}

        SDS_View.SelectCommand = sql;
	SDS_View.SelectParameters.Clear();
	if (!value_site_ch.Equals(""))
	    SDS_View.SelectParameters.Add("site_ch", value_site_ch);

        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();
        if (!value_Short_Name.Equals(""))
        {
            Get_XY(sql_xy);
        }

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // 建立 SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
	    a.SelectCommand.Parameters.Add("site_ch", value_site_ch);
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        // 執行命令
        //dt_excel = cmd.ExecuteReader().;

        // 清理命令和連線物件。
        cmd.Dispose();
        conn.Dispose();

    }

    protected void Get_XY(string sql_xy)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand_NameFull = sql_xy;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            string[,] xy01_012 = new string[10000, 2];
            while (reader.Read())
            {
                xy01_012[i, 0] = reader["y"].ToString();
                xy01_012[i, 1] = reader["x"].ToString();
                i += 1;
                //mapxy.Text += reader["y"].ToString() + "," + reader["x"].ToString() + ";";
            }
            Session["xy01_012"] = xy01_012;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    protected void Export_Excel(object sender, EventArgs e)
    {
        GridView_List.PageSize = 100000;
        //移除最後的連結欄位
        GridView_List.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        
        GridView_List.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //string sql_str = SqlDataSource1.SelectCommand;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=occurrence_" + Excel_ShortTime + ".xls");
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
        //GridView1.PageSize = 100000;
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
            ISheet u_sheet_Detail = workbook.CreateSheet("occurrence");

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
            u_sheet_Detail.SetColumnWidth(0, 30 * 256);
            u_sheet_Detail.SetColumnWidth(1, 10 * 256);
            u_sheet_Detail.SetColumnWidth(2, 10 * 256);
            u_sheet_Detail.SetColumnWidth(3, 30 * 256);
            u_sheet_Detail.SetColumnWidth(4, 30 * 256);
            u_sheet_Detail.SetColumnWidth(5, 20 * 256);
            u_sheet_Detail.SetColumnWidth(6, 20 * 256);
            u_sheet_Detail.SetColumnWidth(7, 20 * 256);
            u_sheet_Detail.SetColumnWidth(8, 20 * 256);
            u_sheet_Detail.SetColumnWidth(9, 30 * 256);
            u_sheet_Detail.SetColumnWidth(10, 20 * 256);
            u_sheet_Detail.SetColumnWidth(11, 30 * 256);
            u_sheet_Detail.SetColumnWidth(12, 20 * 256);
            u_sheet_Detail.SetColumnWidth(13, 20 * 256);



            //設定CellStyle
            //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("中文名");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("調查地點");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("國道編號");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("調查日期");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("經度");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("緯度");


            //設定CellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
            
            int k = 0;

            // 比較表 資料
            foreach (DataRow pDR_FSData in dt_excel.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;

                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["Chinese_Name"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["site_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["highway_id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["invest_date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["x"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["y"].ToString());
                    
                    //設定CellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;

                }
            }

            workbook.Write(ms);

            //將EXCEL檔案匯出到系統資料儲存目錄
            //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "occurrence_01.xls";
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


    protected void ExportExcelFile2()
    {
        string filename = "SampleExcelFile";
        string title = " Excel Title ";

        string html = "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=big5\">"
                     + "<title>新增網頁1</title></head>" + title + "<br>";
        html += "<table border=\"1\">";

        //表頭
        html += "<tr>";
        for (int i = 0; i < GridView_List.Columns.Count; i++)
        {
            if (!GridView_List.Columns[i].Visible)
            {
                continue;
            }
            html += "<td bgcolor=\"#E4E4E4\">" + GridView_List.HeaderRow.Cells[i].Text;
        }

        html += "</tr>";

        //先把分頁關掉
        GridView_List.AllowPaging = false;
        
        //內容
        for (int i = 0; i < GridView_List.Rows.Count; i++)
        {
            html += "<tr>";
            for (int c = 0; c < GridView_List.Columns.Count; c++)
            {
                if (!GridView_List.Columns[c].Visible)
                {
                    continue;
                }
                if (GridView_List.Rows[i].Cells[c].Controls.Count > 0)
                {
                    //html += "<td>" + ((Label)gv.Rows[i].Cells[c].Controls[1]).Text + "</td>";
                    html += "<td>" + GridView_List.Rows[i].Cells[c].Text + "</td>";
                }
                else
                {
                    html += "<td>" + GridView_List.Rows[i].Cells[c].Text + "</td>";
                }

            }
            html += "</tr>";
        }
        html += "</table>";

        GridView_List.AllowPaging = true;
        

        string sContentDisposition = "";
        sContentDisposition += "attachment; "; // 強制存檔，未設定則依瀏覽器預設開啟或存檔
        sContentDisposition += "filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls"; //設定檔名可為中文_#1

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-disposition", sContentDisposition);
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excle";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        //utt8 to ascii
        Byte[] bytes = System.Text.Encoding.Unicode.GetBytes(html);
        Byte[] outByte = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.Default, bytes);

        HttpContext.Current.Response.BinaryWrite(outByte);
        HttpContext.Current.Response.End();
    }
}