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
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI;

public partial class View_fun02_011b : System.Web.UI.Page
{
    public string map_url = "";
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun02_011b_.aspx", "gmap02_011b.aspx");
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        Query();
        /*
        string value_group = DropDownList1.SelectedValue;
        string value_class1 = DropDownList2.SelectedValue;
        string value_class2 = DropDownList3.SelectedValue;
        string value_start_end = DropDownList4.SelectedValue;

        string sql = " Select a.siteid , CONVERT (char(10), a.date, 126) AS invest_date , c.highway_name , d.Direction_ch , a.range , a.site_ch , Round(a.x,6) as x  , Round(a.y,6) as y , a.TM2_X , a.TM2_Y , b.species From Roadkill_site_new a , Roadkill_observation_new b , Highway c , Direction d Where a.highway_id = c.highway_id and a.direction =d.Direction and a.siteid = b.siteid ";

        if (!value_group.Equals("ALL"))
        {
            sql += " and b.[animal] = '" + value_group + "'";
        }

        //工務段轄區有選擇
        if (!value_start_end.Equals("ALL"))
        {
            sql += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_start_end + "') ";
        }

        //工務段有選擇且工務段轄區=ALL
        if (!value_class2.Equals("ALL") && value_start_end.Equals("ALL"))
        {
            sql += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
            sql += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_class2 + "') ";
        }

        //工程處有選擇且工務段與工務段轄區=ALL
        if (!value_class1.Equals("ALL") && value_class2.Equals("ALL") && value_start_end.Equals("ALL"))
        {
            sql += " and a.x <= (select max_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and a.x >= (select min_x from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and a.y <= (select max_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
            sql += " and a.y >= (select min_y from Engineering_road_scope where start_end = '" + value_class1 + "') ";
        }

        SDS_View1.SelectCommand = sql;
        GridView1.DataBind();
        GridView1.PageSize = 20;
        */
    }

    protected void Query()
    {
        string value_group = DropDownList1.SelectedValue;
        string value_class1 = DropDownList2.SelectedValue;
        string value_class2 = DropDownList4.SelectedValue;
        //string value_start_end = DropDownList5.SelectedValue;
        string value_Sensitive_Level = DropDownList3.SelectedValue;

        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = KeyWords.Text;
        string sql = " Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic, varify From v_Roadkill_new Where 1=1 ";
        string sql_xy = " Select Distinct x  , y  From v_Roadkill_new Where 1=1 ";
        
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

        if (!value_group.Equals("ALL"))
        {
            sql += " and animal = '" + value_group + "'";
            sql_xy += " and animal = '" + value_group + "'";
        }

        if (!value_Sensitive_Level.Equals("ALL"))
        {
            sql += " and Sensitive_Level = '" + value_Sensitive_Level + "'";
            sql_xy += " and Sensitive_Level = '" + value_Sensitive_Level + "'";
        }

        /*
        //工務段轄區有選擇
        if (!value_start_end.Equals("ALL"))
        {
            sql += " and x <= (select max_x from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql += " and x >= (select min_x from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql += " and y <= (select max_y from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql += " and y >= (select min_y from Engineering_road_scope where start_end = '" + value_start_end + "') ";

            sql_xy += " and x <= (select max_x from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql_xy += " and x >= (select min_x from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql_xy += " and y <= (select max_y from Engineering_road_scope where start_end = '" + value_start_end + "') ";
            sql_xy += " and y >= (select min_y from Engineering_road_scope where start_end = '" + value_start_end + "') ";
        }
        */

        //工務段有選擇且工務段轄區=ALL
        //if (!value_class2.Equals("ALL") && value_start_end.Equals("ALL"))
        if (!value_class2.Equals("ALL"))
        {

            sql += " and site_ch = '" + value_class2.Replace("工務段","") + "' ";
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

        string value_species = species.SelectedValue.ToString();
        //關注物種
        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
            {
                sql += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
                sql_xy += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
            }
            if (value_species.Equals("is_endemic"))
            {
                sql += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
                sql_xy += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
            }
            if (value_species.Equals("is_alien"))
            {
                sql += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
                sql_xy += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
            }
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
            sql += " and id in (Select id  From v_Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
        }


        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();

        //if (!value_group.Equals("") || !value_KeyWords.Equals("") || !value_Sensitive_Level.Equals("") || !value_start_end.Equals("") || !value_class2.Equals("") || !value_class1.Equals("")){
        if (!value_group.Equals("") || !value_KeyWords.Equals("") || !value_Sensitive_Level.Equals("") || !value_class2.Equals("") || !value_class1.Equals(""))
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
            string[,] xy02_011b = new string[10000, 2];
            while (reader.Read())
            {
                xy02_011b[i, 0] = reader["y"].ToString();
                xy02_011b[i, 1] = reader["x"].ToString();
                i += 1;
            }
            //產生Session
            Session["xy02_011b"] = xy02_011b;
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
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("工務段");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("國道編號");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("方向");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("國道里程");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("日期");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("工作類別");
            u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("可能種類");
            u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("可能種類推測");
            u_sheet_Detail.GetRow(0).CreateCell(8).SetCellValue("照片");
            u_sheet_Detail.GetRow(0).CreateCell(9).SetCellValue("校對");


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


            int k = 0;

            // 比較表 資料
            foreach (DataRow pDR_FSData in dt_excel.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;

                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["site_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["highway_id"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["direction"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["milestone"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["type"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["deduce_species"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(8).SetCellValue(pDR_FSData["Pic"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(9).SetCellValue(pDR_FSData["varify"].ToString());

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

                }
            }

            workbook.Write(ms);

            //將EXCEL檔案匯出到系統資料儲存目錄
            //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "現金預估比較表.xls";
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "roadkill_02.xls";
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