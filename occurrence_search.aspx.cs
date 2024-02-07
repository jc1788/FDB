using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI;


public partial class occurrence_search : System.Web.UI.Page
{
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	this.flexCheck.InputAttributes["class"] = "form-check-input";

	if (!IsPostBack) SetContent();
	else if (Session["occurrence_qtype"] != null && Session["occurrence_qtype"].ToString() == "1")
	    btnsearch_Click(sender, e);
	else if (Session["occurrence_qtype"] != null && Session["occurrence_qtype"].ToString() == "2")
	    btnsearch2_Click(sender, e);
    }

    private void SetContent()
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataTable dt_highway = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM [Highway]", conn);
	    apt.Fill(dt_highway);
	    highwayida.DataSource = dt_highway;
	    highwayida.DataBind();

	    DataTable dt_part = new DataTable();
	    apt = new SqlDataAdapter("SELECT 0 AS id , 'ALL' AS  class1 UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY id", conn);
	    apt.Fill(dt_part);
	    DropDownList4a.DataSource = dt_part;
	    DropDownList4a.DataBind();

	    conn.Close();
	}
    }

    protected void DropDownList4a_SelectedIndexChanged(object sender, EventArgs e)
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    if(DropDownList4a.SelectedValue != "ALL")
	    {
	    DataTable dt_site = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT 'ALL' as class2, 0 as position UNION ALL (SELECT DISTINCT class2, position FROM [Engineering_road] Where (class1=@class1)) order by position", conn);
	    apt.SelectCommand.Parameters.Add("class1", DropDownList4a.SelectedValue);
	    apt.Fill(dt_site);
	    DropDownList5a.DataSource = dt_site;
	    DropDownList5a.DataBind();
	    }
	    else
	    {
	    DropDownList5a.Items.Clear();
	    DropDownList5a.Items.Add(new ListItem("ALL","ALL"));
	    }

	    conn.Close();
	}
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
	Session["occurrence_qtype"] = "1";
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    SqlCommand cmd = new SqlCommand();
	    SqlDataAdapter apt = new SqlDataAdapter();
	    apt.SelectCommand = cmd;

	    string searchtxt = this.searchtxta.Text;
	    string start_date = this.start_datea.Text;
	    string end_date = this.end_datea.Text;
	    string highwayid = this.highwayida.SelectedValue;
	    string start1 = this.start1a.Text;
	    string start2 = this.start2a.Text;
	    string end1 = this.end1a.Text;
	    string end2 = this.end2a.Text;
	    string value_Sensitive_Level = this.DropDownList3a.SelectedValue;
	    string value_group = this.DropDownList1a.SelectedValue;
	    string value_species = this.DropDownList2a.SelectedValue;

	    int start = 0;
	    if (start1 != "")
		start += int.Parse(start1) * 1000;
	    if (start2 != "")
		start += int.Parse(start2);
	    int end = 0;
	    if (end1 != "")
		end += int.Parse(end1) * 1000;
	    if (end2 != "")
		end += int.Parse(end2);

	    string qstr = "";
	    string sql = "SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 Where 1=1 ";
	    string sql_xy = " Select Distinct x, y  from table_1 Where 1=1 ";
	    string sql3 = "";
	    this.sds_occurrence.SelectParameters.Clear();
	    
	    if (searchtxt != "")
	    {
	        string[] searchtxts = searchtxt.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql3 += " and (Chinese_Name like '%' + @kwd"+i+" + '%' or Short_Name like '%' + @kwd"+i+" + '%')";
		    this.sds_occurrence.SelectParameters.Add("kwd"+i, searchtxts[i]);
		    cmd.Parameters.AddWithValue("kwd"+i, searchtxts[i]);
		}
		qstr += searchtxt;
	    }
	    if (highwayid != "")
	    {
		sql3 += " and highway_id = @highwayid";
		this.sds_occurrence.SelectParameters.Add("highwayid", highwayid);
		cmd.Parameters.AddWithValue("highwayid", highwayid);
		if (qstr != "")
		    qstr += "、";
		qstr += "國道" + highwayid;
	    }
	    if (start_date != "")
	    {
		sql3 += " and Convert(Varchar(10),date1) >= @start_date";
		this.sds_occurrence.SelectParameters.Add("start_date", start_date);
		cmd.Parameters.AddWithValue("start_date", start_date);
		if (qstr != "")
		    qstr += "、";
		qstr += "開始日期=" + start_date;
	    }
	    if (end_date != "")
	    {
		sql3 += " and Convert(Varchar(10),date1) <= @end_date";
		this.sds_occurrence.SelectParameters.Add("end_date", end_date);
		cmd.Parameters.AddWithValue("end_date", end_date);
		if (qstr != "")
		    qstr += "、";
		qstr += "結束日期=" + end_date;
	    }
	    if (start != 0)
	    {
		sql3 += " and x <= (Select Top 1 WGS84X From Freeway_new Where free_id = @highwayid and mileage = @start) ";
		sql3 += " and y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = @highwayid and mileage = @start) ";
		this.sds_occurrence.SelectParameters.Add("start", start.ToString());
		cmd.Parameters.AddWithValue("start", start.ToString());
		if (qstr != "")
		    qstr += "、";
		qstr += "起始里程=" + (start1 != "" ? start1 : "0") + "K+" + (start2 != "" ? start2 : "0");
	    }
	    if (end != 0)
	    {
		sql3 += " and x >= (Select Top 1 WGS84X From Freeway_new Where free_id = @highwayid and mileage = @end) ";
		sql3 += " and y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = @highwayid and mileage = @end) ";
		this.sds_occurrence.SelectParameters.Add("end", end.ToString());
		cmd.Parameters.AddWithValue("end", end.ToString());
		if (qstr != "")
		    qstr += "、";
		qstr += "結束里程=" + (end1 != "" ? end1 : "0") + "K+" + (end2 != "" ? end2 : "0");
	    }

	    if (!value_Sensitive_Level.Equals("ALL"))
	    {
		sql3 += " and Sensitive_Level = @Sensitive_Level";
		this.sds_occurrence.SelectParameters.Add("Sensitive_Level", value_Sensitive_Level);
		cmd.Parameters.AddWithValue("Sensitive_Level", value_Sensitive_Level);
		if (qstr != "")
		    qstr += "、";
		qstr += "敏感里程等級=" + value_Sensitive_Level;
	    }

	    if (!value_group.Equals("ALL"))
	    {
		sql3 += " and [group] = @value_group";
		this.sds_occurrence.SelectParameters.Add("value_group", value_group);
		cmd.Parameters.AddWithValue("value_group", value_group);
		if (qstr != "")
		    qstr += "、";
		qstr += "物種類群=" + value_group;
	    }

        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
                sql3 += " and accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
            if (value_species.Equals("is_endemic"))
                sql3 += " and accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
            if (value_species.Equals("is_alien"))
                sql3 += " and accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
		if (qstr != "")
		    qstr += "、";
		qstr += "關注物種=" + value_species;
        }

	    sql += sql3;
	    sql_xy += sql3;

	    apt.SelectCommand.CommandText = sql_xy;
	    apt.SelectCommand.Connection = conn;
	    DataTable dt = new DataTable();
	    apt.Fill(dt);
	    apt.Dispose();
	    string oc = "";
	    foreach(DataRow dr in dt.Rows)
	    {
		if (oc != "")
		    oc += ";";
		oc += dr[0].ToString() + "," + dr[1].ToString();
	    }
	    Session["occurrence_clustermap"] = oc;
	    this.occurence_clustermap.Src = "occurrence_clustermap.aspx";
	    this.dmap.Visible = true;

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    this.sds_occurrence.SelectCommand = sql;
	    this.gv_occurrence.DataBind();
	    this.gv_occurrence.UseAccessibleHeader = true;
	    if (this.gv_occurrence.HeaderRow != null)
	    this.gv_occurrence.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_occurrence.Visible = true;
	    DataView view = (DataView)this.sds_occurrence.Select(DataSourceSelectArguments.Empty);
	    dt_excel = view.ToTable();
	    TotalCounts.Text = "總筆數：" + view.Count;
	    view.Dispose();

	    conn.Close();
	}
    }

    protected void btnsearch2_Click(object sender, EventArgs e)
    {
	Session["occurrence_qtype"] = "2";
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    SqlCommand cmd = new SqlCommand();
	    SqlDataAdapter apt = new SqlDataAdapter();
	    apt.SelectCommand = cmd;

	    string searchtxt2 = this.searchtxt2a.Text;
	    string start_date2 = this.start_date2a.Text;
	    string end_date2 = this.end_date2a.Text;
	    string value_class1 = this.DropDownList4a.SelectedValue;
	    string value_class2 = this.DropDownList5a.SelectedValue;
	    string value_Sensitive_Level = this.DropDownList32a.SelectedValue;
	    string value_group = this.DropDownList12a.SelectedValue;
	    string value_species = this.DropDownList22a.SelectedValue;

	    string qstr = "";
	    string sql = "SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 Where 1=1 ";
	    string sql_xy = " Select Distinct x, y  from table_1 Where 1=1 ";
	    string sql3 = "";
	    this.sds_occurrence.SelectParameters.Clear();
	    if (searchtxt2 != "")
	    {
	        string[] searchtxts = searchtxt2.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql3 += " and (Chinese_Name like '%' + @kwd"+i+" + '%' or Short_Name like '%' + @kwd"+i+" + '%')";
		    this.sds_occurrence.SelectParameters.Add("kwd"+i, searchtxts[i]);
		    cmd.Parameters.AddWithValue("kwd"+i, searchtxts[i]);
		}
		qstr += searchtxt2;
	    }

	    if (start_date2 != "")
	    {
		sql3 += " and CONVERT (char(10), date1, 126) >= @start_date2";
		this.sds_occurrence.SelectParameters.Add("start_date2", start_date2);
		cmd.Parameters.AddWithValue("start_date2", start_date2);
		if (qstr != "")
		    qstr += "、";
		qstr += "開始日期=" + start_date2;
	    }
	    if (end_date2 != "")
	    {
		sql3 += " and CONVERT (char(10), date1, 126) <= @end_date2";
		this.sds_occurrence.SelectParameters.Add("end_date2", end_date2);
		cmd.Parameters.AddWithValue("end_date2", end_date2);
		if (qstr != "")
		    qstr += "、";
		qstr += "結束日期=" + end_date2;
	    }

        if (!value_class2.Equals("ALL"))
        {
            sql3 += " and x <= (select max_x from Engineering_road_scope where start_end = @value_class2) ";
            sql3 += " and x >= (select min_x from Engineering_road_scope where start_end = @value_class2) ";
            sql3 += " and y <= (select max_y from Engineering_road_scope where start_end = @value_class2) ";
            sql3 += " and y >= (select min_y from Engineering_road_scope where start_end = @value_class2) ";
	    this.sds_occurrence.SelectParameters.Add("value_class2", value_class2);
	    cmd.Parameters.AddWithValue("value_class2", value_class2);
		if (qstr != "")
		    qstr += "、";
		qstr += value_class2;
        }
        else if (!value_class1.Equals("ALL"))
        {
            sql3 += " and x <= (select max_x from Engineering_road_scope where start_end = @value_class1) ";
            sql3 += " and x >= (select min_x from Engineering_road_scope where start_end = @value_class1) ";
            sql3 += " and y <= (select max_y from Engineering_road_scope where start_end = @value_class1) ";
            sql3 += " and y >= (select min_y from Engineering_road_scope where start_end = @value_class1) ";
	    this.sds_occurrence.SelectParameters.Add("value_class1", value_class1);
	    cmd.Parameters.AddWithValue("value_class1", value_class1);
		if (qstr != "")
		    qstr += "、";
		qstr +=value_class1;
        }

	    if (!value_Sensitive_Level.Equals("ALL"))
	    {
		sql3 += " and Sensitive_Level = @Sensitive_Level";
		this.sds_occurrence.SelectParameters.Add("Sensitive_Level", value_Sensitive_Level);
		cmd.Parameters.AddWithValue("Sensitive_Level", value_Sensitive_Level);
		if (qstr != "")
		    qstr += "、";
		qstr += "敏感里程等級=" + value_Sensitive_Level;
	    }

	    if (!value_group.Equals("ALL"))
	    {
		sql3 += " and [group] = @value_group";
		this.sds_occurrence.SelectParameters.Add("value_group", value_group);
		cmd.Parameters.AddWithValue("value_group", value_group);
		if (qstr != "")
		    qstr += "、";
		qstr += "物種類群=" + value_group;
		if (qstr != "")
		    qstr += "、";
		qstr += "關注物種=" + value_species;
	    }

        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
                sql3 += " and accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
            if (value_species.Equals("is_endemic"))
                sql3 += " and accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
            if (value_species.Equals("is_alien"))
                sql3 += " and accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
        }

	    sql += sql3;
	    sql_xy += sql3;

	    apt.SelectCommand.CommandText = sql_xy;
	    apt.SelectCommand.Connection = conn;
	    DataTable dt = new DataTable();
	    apt.Fill(dt);
	    apt.Dispose();
	    string oc = "";
	    foreach(DataRow dr in dt.Rows)
	    {
		if (oc != "")
		    oc += ";";
		oc += dr[0].ToString() + "," + dr[1].ToString();
	    }
	    Session["occurrence_clustermap"] = oc;
	    this.occurence_clustermap.Src = "occurrence_clustermap.aspx";
	    this.dmap.Visible = true;

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    this.sds_occurrence.SelectCommand = sql;
	    this.gv_occurrence.DataBind();
	    this.gv_occurrence.UseAccessibleHeader = true;
	    if (this.gv_occurrence.HeaderRow != null)
	    this.gv_occurrence.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_occurrence.Visible = true;
	    DataView view = (DataView)this.sds_occurrence.Select(DataSourceSelectArguments.Empty);
	    dt_excel = view.ToTable();
	    TotalCounts.Text = "總筆數：" + view.Count;
	    view.Dispose();

	    conn.Close();
	}
    }

    protected void Export_Excel(object sender, EventArgs e)
    {
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
            u_sheet_Detail.SetColumnWidth(0, 20 * 256);
            u_sheet_Detail.SetColumnWidth(1, 40 * 256);
            u_sheet_Detail.SetColumnWidth(2, 10 * 256);
            u_sheet_Detail.SetColumnWidth(3, 20 * 256);
            u_sheet_Detail.SetColumnWidth(4, 20 * 256);
            u_sheet_Detail.SetColumnWidth(5, 20 * 256);

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
            string Excel_ShortTime = "occurrence_search_export.xls";
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

    public bool occurrence_editable(string id)
    {
	bool editable = false;

	if (Session["role"].ToString() == "1")
	    editable = true;

	return editable;
    }

    protected void flexCheck_Changed(object sender, EventArgs e)
    {
	if (flexCheck.Checked)
	{
	    this.gv_occurrence.Columns[8].Visible = true;
	    this.gv_occurrence.Columns[9].Visible = true;
	}
	else
	{
	    this.gv_occurrence.Columns[8].Visible = false;
	    this.gv_occurrence.Columns[9].Visible = false;
	}
	if (Session["occurrence_qtype"] == null || Session["occurrence_qtype"].ToString() == "1")
	    btnsearch_Click(sender, e);
	else if (Session["occurrence_qtype"] != null && Session["occurrence_qtype"].ToString() == "2")
	    btnsearch2_Click(sender, e);
    }

    protected void gv_occurrence_RowCommand(object sender, GridViewCommandEventArgs e)
    {
	if (e.CommandName == "Delete")
	{
	    string sid = e.CommandArgument.ToString();
	    string sql = "Delete table_1 WHERE [sid] = @sid";

	    this.sds_occurrence.DeleteCommand = sql;
	    this.sds_occurrence.DeleteParameters.Add("sid", sid);
	}
    }
}
