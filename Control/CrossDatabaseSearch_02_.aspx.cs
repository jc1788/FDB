using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI;

public partial class Control_CrossDatabaseSearch_02 : System.Web.UI.Page
{
    public string map_url = "";
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("Control/CrossDatabaseSearch_02_.aspx", "View/gmap01_021b.aspx");
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        //Query2();
    }

    protected void Query2()
    {
        string sql = " SELECT distinct  a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ";
        string sql_xy = " Select Distinct a.x  , a.y  FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid Where 1=1 ";
        string value_class1 = DropDownList2.SelectedValue;
        string value_class2 = DropDownList4.SelectedValue;
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = KeyWords.Text;
        //if (!value_KeyWords.Equals(""))
        //{
        //    sql += " And ( b.Chinese_Name Like '%" + value_KeyWords + "%' ";
        //    sql += "    or b.Short_Name Like '%" + value_KeyWords + "%' )";

        //    sql_xy += " And ( b.Chinese_Name Like '%" + value_KeyWords + "%' ";
        //    sql_xy += "    or b.Short_Name Like '%" + value_KeyWords + "%' )";
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            sql_xy += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (b.Chinese_Name like '%" + item + "%' or b.Short_Name like '%" + item + "%') ";
                sql_xy += " (b.Chinese_Name like '%" + item + "%' or b.Short_Name like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                    sql_xy += " or ";
                }
                i++;
            }
            sql += " ) ";
            sql_xy += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (b.Chinese_Name like '%" + value_KeyWords + "%' or b.Short_Name like '%" + value_KeyWords + "%') ";
            sql_xy += " and (b.Chinese_Name like '%" + value_KeyWords + "%' or b.Short_Name like '%" + value_KeyWords + "%') ";
        }

        //工務段有選擇且工務段轄區=ALL
        if (!value_class2.Equals("ALL"))
        {
            sql += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";

            sql_xy += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql_xy += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql_xy += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql_xy += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
        }

        //工程處有選擇且工務段與工務段轄區=ALL
        if (!value_class1.Equals("ALL") && value_class2.Equals("ALL"))
        {
            sql += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";

            sql_xy += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql_xy += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql_xy += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql_xy += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
        }

        //日期區間
        string value_StartYear = StartYear.Text;
        string value_StartMonth = StartMonth.SelectedValue.ToString();
        string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        string value_EndYear = EndYear.Text;
        string value_EndMonth = EndMonth.SelectedValue.ToString();
        string value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";

        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            sql += " and siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
            sql_xy += " and siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
        }

        SDS_View.SelectCommand = sql;
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

    protected void Query2_02()
    {
        string value_class1 = DropDownList2.SelectedValue;
        string value_class2 = DropDownList4.SelectedValue;
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = KeyWords.Text;
        string sql = " Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic From Roadkill_new Where 1=1 ";
        string sql_xy = " Select Distinct x  , y  From Roadkill_new Where 1=1 ";

        //if (!value_KeyWords.Equals(""))
        //{
        //    sql += " and (species Like '%" + value_KeyWords + "%'";
        //    sql += " or deduce_species Like '%" + value_KeyWords + "%')";
        //    sql_xy += " and (species Like '%" + value_KeyWords + "%'";
        //    sql_xy += " or deduce_species Like '%" + value_KeyWords + "%')";
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            sql_xy += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (species like '%" + item + "%' or deduce_species like '%" + item + "%') ";
                sql_xy += " (species like '%" + item + "%' or deduce_species like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                    sql_xy += " or ";
                }
                i++;
            }
            sql += " ) ";
            sql_xy += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (species like '%" + value_KeyWords + "%' or deduce_species like '%" + value_KeyWords + "%') ";
            sql_xy += " and (species like '%" + value_KeyWords + "%' or deduce_species like '%" + value_KeyWords + "%') ";
        }

        //工務段有選擇且工務段轄區=ALL
        //if (!value_class2.Equals("ALL") && value_start_end.Equals("ALL"))
        if (!value_class2.Equals("ALL"))
        {

            sql += " and site_ch = '" + value_class2.Replace("工務段", "") + "' ";
            sql_xy += " and site_ch = '" + value_class2.Replace("工務段", "") + "' ";
            /*
            sql += " and x <= (select max_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and x >= (select min_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and y <= (select max_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and y >= (select min_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";

            sql_xy += " and x <= (select max_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql_xy += " and x >= (select min_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql_xy += " and y <= (select max_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql_xy += " and y >= (select min_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            */
        }
        //工程處有選擇且工務段與工務段轄區=ALL
        //if (!value_class1.Equals("ALL") && value_class2.Equals("ALL") && value_start_end.Equals("ALL"))
        if (!value_class1.Equals("ALL") && value_class2.Equals("ALL"))
        {
            sql += " and x <= (select max_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and x >= (select min_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and y <= (select max_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and y >= (select min_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";

            sql_xy += " and x <= (select max_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql_xy += " and x >= (select min_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql_xy += " and y <= (select max_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql_xy += " and y >= (select min_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
        }

        //日期區間
        string value_StartYear = StartYear.Text;
        string value_StartMonth = StartMonth.SelectedValue.ToString();
        string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        string value_EndYear = EndYear.Text;
        string value_EndMonth = EndMonth.SelectedValue.ToString();
        string value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";

        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            sql += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
        }


        SDS_View_02.SelectCommand = sql;
        GridView_List_02.DataBind();
        GridView_List_02.PageSize = 20;
        btnShowGVCount_02_Click1();

        //if (!value_group.Equals("") || !value_KeyWords.Equals("") || !value_Sensitive_Level.Equals("") || !value_start_end.Equals("") || !value_class2.Equals("") || !value_class1.Equals("")){
        //if (!value_group.Equals("") || !value_KeyWords.Equals("") || !value_Sensitive_Level.Equals("") || !value_class2.Equals("") || !value_class1.Equals(""))
        //{
        //    Get_XY(sql_xy);
        //}

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
        GridView_List.Visible = false;
        GridView_List_02.Visible = false;
        if (!search1.Checked & !search2.Checked)
        {
            Query2();
            Query2_02();
            GridView_List.Visible = true;
            GridView_List_02.Visible = true;
        }
        else
        {
            if (search1.Checked)
            {
                Query2();
                GridView_List.Visible = true;
            }
            if (search2.Checked)
            {
                Query2_02();
                GridView_List_02.Visible = true;
            }
        }
    }

    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }
    protected void btnShowGVCount_02_Click1()
    {
        DataView view = (DataView)SDS_View_02.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts_02.Text = "總筆數：" + count;
        view.Dispose();
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
            string[,] xy01_021b = new string[10000, 2];
            while (reader.Read())
            {
                xy01_021b[i, 0] = reader["y"].ToString();
                xy01_021b[i, 1] = reader["x"].ToString();
                i += 1;
            }
            //產生Session
            Session["xy01_021b"] = xy01_021b;
            //string aa = "aa";
            //reader = cmd.ExecuteReader();


            //string[,] xy = new string[i,2];

            /*
            int j = 0;
            while (reader.Read())
            {
                j += 1;
                //String sid = reader[0].ToString;
                if (!reader[0].Equals(0))
                {
                    xy[j,0] = reader["x"].ToString();
                    xy[j,1] = reader["y"].ToString();
                }
            }
            */

        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
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
            string Excel_ShortTime = "occurrence_03.xls";
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
}