
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



public partial class View_fun02_011 : System.Web.UI.Page
{
    public string sql_data;
    public string map_url = "";
    public string hmap_url = "";
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["xy02_011a"] = null;
        //Session["hxy02_011a"] = null;
        /*
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun02_011a.aspx", "gmap02_011a.aspx");
        
        hmap_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        hmap_url = hmap_url.Replace("fun02_011a.aspx", "hmap02_011a.aspx");
        */


        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        
        Query();
        btnShowGVCount_Click1();
        /*
        if (IsPostBack)
        {
            Query();
            //QueryData(sender,e);
            //DropDownList_Page_SelectedIndexChanged(sender, e);
            //GridView_List_DataBound(sender, e);

        }
        
        
        string sql = " Select a.siteid , CONVERT (char(10), a.date, 126) AS invest_date , c.highway_name , d.Direction_ch , a.range , a.site_ch , Round(a.x,6) as x  , Round(a.y,6) as y , a.TM2_X , a.TM2_Y , b.species From Roadkill_site_new a , Roadkill_observation_new b , Highway c , Direction d Where a.highway_id = c.highway_id and a.direction =d.Direction and a.siteid = b.siteid ";
        string value_group = DropDownList1.SelectedValue;
        string value_highway_id = DropDownList2.SelectedValue;

        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;
        int value_up_mileage = int.Parse(value_Start_Mileston1) * 1000 + int.Parse(value_Start_Mileston2);
        int value_down_mileage = int.Parse(value_End_Mileston1) * 1000 + int.Parse(value_End_Mileston2);


        if (!value_group.Equals("ALL"))
        {
            sql += " and b.[animal] = '" + value_group + "'";
        }
        if (!value_highway_id.Equals("0"))
        {
            sql += " and a.[highway_id] = '" + value_highway_id + "'";
        }

        //有填入起始與結束的里程
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql += " and a.x <= (Select Top 1 WGS84X From Freeway Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql += " and a.x >= (Select Top 1 WGS84X From Freeway Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
            sql += " and a.y <= (Select Top 1 WGS84Y From Freeway Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql += " and a.y >= (Select Top 1 WGS84Y From Freeway Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
        }

        SDS_View1.SelectCommand = sql;
        GridView1.DataBind();
        GridView1.PageSize = 20;
        */

        
    }

    protected void Query()
    {
        string sql = " Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic, varify From v_Roadkill_new Where 1=1 ";
        string sql_xy = " Select Distinct x  , y  From v_Roadkill_new Where 1=1 ";
        string sql_hxy = " Select  x  , y  , count(*) as cnts From v_Roadkill_new Where x <> 0  ";

        string value_group = DropDownList1.SelectedValue;
        string value_highway_id = DropDownList2.SelectedValue;
        string value_Sensitive_Level = DropDownList3.SelectedValue;

        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        String value_KeyWords = KeyWords.Text;
        //string value_Order_By = Order_By.SelectedValue;

/*
	decimal dec_Start_Mileston1, dec_Start_Mileston2, dec_End_Mileston1, dec_End_Mileston2;
	decimal.TryParse(value_Start_Mileston1, out dec_Start_Mileston1);
	decimal.TryParse(value_Start_Mileston2, out dec_Start_Mileston2);
	decimal.TryParse(value_End_Mileston1, out dec_End_Mileston1);
	decimal.TryParse(value_End_Mileston2, out dec_End_Mileston2);
	decimal dec_up = dec_Start_Mileston1 + dec_Start_Mileston2 / 1000;
	decimal dec_down = dec_End_Mileston1 + dec_End_Mileston2 / 1000;
*/

        if (value_Start_Mileston1.Equals(""))
        {
            value_Start_Mileston1 = "0";
            Start_Mileston1.Text = "0";
        }

        if (value_Start_Mileston2.Equals(""))
        {
            value_Start_Mileston2 = "0";
            Start_Mileston2.Text = "0";
        }

        if (value_End_Mileston1.Equals(""))
        {
            value_End_Mileston1 = "0";
            End_Mileston1.Text = "0";
        }

        if (value_End_Mileston2.Equals(""))
        {
            value_End_Mileston2 = "0";
            End_Mileston2.Text = "0";
        }

        decimal dec_up = decimal.Parse(value_Start_Mileston1) + decimal.Parse(value_Start_Mileston2) / 1000;
        decimal dec_down = decimal.Parse(value_End_Mileston1) + decimal.Parse(value_End_Mileston2) / 1000;

        int value_up_mileage = int.Parse(value_Start_Mileston1) * 1000 + int.Parse(value_Start_Mileston2);
        int value_down_mileage = int.Parse(value_End_Mileston1) * 1000 + int.Parse(value_End_Mileston2);

        if (!value_group.Equals("ALL"))
        {
            sql += " and animal = '" + value_group + "'";
            sql_xy += " and animal = '" + value_group + "'";
            sql_hxy += " and animal = '" + value_group + "'";
        }

        if (!value_highway_id.Equals("0"))
        {
            sql += " and highway_id = '" + value_highway_id + "'";
            sql_xy += " and highway_id = '" + value_highway_id + "'";
            sql_hxy += " and highway_id = '" + value_highway_id + "'";
        }

        //if (!value_KeyWords.Equals(""))
        //{
        //    sql += " and (species Like '%" + value_KeyWords + "%'";
        //    sql += " or deduce_species Like '%" + value_KeyWords + "%')";
        //    sql_xy += " and (species Like '%" + value_KeyWords + "%'";
        //    sql_xy += " or deduce_species Like '%" + value_KeyWords + "%')";
        //    sql_hxy += " and (species Like '%" + value_KeyWords + "%'";
        //    sql_hxy += " or deduce_species Like '%" + value_KeyWords + "%')";
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            sql_xy += " and ( ";
            sql_hxy += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (species like '%" + item + "%' or deduce_species like '%" + item + "%') ";
                sql_xy += " (species like '%" + item + "%' or deduce_species like '%" + item + "%') ";
                sql_hxy += " (species like '%" + item + "%' or deduce_species like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                    sql_xy += " or ";
                    sql_hxy += " or ";
                }
                i++;
            }
            sql += " ) ";
            sql_xy += " ) ";
            sql_hxy += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (species like '%" + value_KeyWords + "%' or deduce_species like '%" + value_KeyWords + "%') ";
            sql_xy += " and (species like '%" + value_KeyWords + "%' or deduce_species like '%" + value_KeyWords + "%') ";
            sql_hxy += " and (species like '%" + value_KeyWords + "%' or deduce_species like '%" + value_KeyWords + "%') ";
        }

        if (!value_Sensitive_Level.Equals("ALL"))
        {
            sql += " and Sensitive_Level = '" + value_Sensitive_Level + "'";
            sql_xy += " and Sensitive_Level = '" + value_Sensitive_Level + "'";
            sql_hxy += " and Sensitive_Level = '" + value_Sensitive_Level + "'";
        }

        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0)) {
            sql += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
            sql_xy += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
            sql_hxy += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
        }

        /*
        //有填入起始與結束的里程
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql += " and x <= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql += " and x >= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
            sql += " and y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql += " and y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";

            sql_xy += " and x <= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql_xy += " and x >= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
            sql_xy += " and y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql_xy += " and y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";

        }
        */



        string value_species = species.SelectedValue.ToString();
        //關注物種
        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
            {
                sql += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
                sql_xy += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
                sql_hxy += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
            }
            if (value_species.Equals("is_endemic"))
            {
                sql += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
                sql_xy += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
                sql_hxy += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
            }
            if (value_species.Equals("is_alien"))
            {
                sql += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
                sql_xy += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
                sql_hxy += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
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
            sql_xy += " and id in (Select id  From v_Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
            sql_hxy += " and id in (Select id  From v_Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
        }

        sql_hxy += " group by x , y ";

        /*
        if (!value_Order_By.Equals("")) {
            sql += value_Order_By;
        }
        
        sql_data = sql;
        if (!sql_data.Equals("")){
            SDS_View.SelectCommand = sql_data;
        }
        */
        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();
        //googlemap.Src = map_url;
        //this.googlemap.hidden = false;

        if (!value_group.Equals("") || !value_highway_id.Equals("") || !value_Sensitive_Level.Equals("") || !value_KeyWords.Equals(""))
        {
            Get_XY(sql_xy);
            Get_HXY(sql_hxy);
	    Get_Lines(value_highway_id,value_up_mileage,value_down_mileage);
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
        //DropDownList_Page_SelectedIndexChanged(sender, e);
        //GridView_List_DataBound(sender, e);
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
            string[,] xy02_011a = new string[20000, 2];
            while (reader.Read())
            {
                xy02_011a[i, 0] = reader["y"].ToString();
                xy02_011a[i, 1] = reader["x"].ToString();
                i += 1;
            }
            //產生Session
            Session["xy02_011a"] = xy02_011a;
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


        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun02_011a_.aspx", "lmap02_011a.aspx");
        googlemap.Src = map_url;
    }

    protected void Get_HXY(string sql_xy)
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
            string[,] hxy02_011a = new string[20000, 3];
            while (reader.Read())
            {
                hxy02_011a[i, 0] = reader["y"].ToString();
                hxy02_011a[i, 1] = reader["x"].ToString();
                hxy02_011a[i, 2] = reader["cnts"].ToString();
                i += 1;
            }
            //產生Session
            Session["hxy02_011a"] = hxy02_011a;
            
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        hmap_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        hmap_url = hmap_url.Replace("fun02_011a_.aspx", "hmap02_011a.aspx");

        heatmap.Src = hmap_url;
    }

    protected void Get_Lines(string value_highway_id, int value_up_mileage, int value_down_mileage)
    {
	SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
	SqlCommand cmd;

	string sql = "select  highway_id,mstart,mend, a.WGS84X as sx,a.WGS84Y as sy, b.WGS84X as ex, b.WGS84Y as ey,count from ";
	sql += "(SELECT     highway_id,( mileage-100001) / 5000 * 5000 + 100000 as mstart, ( mileage-100001) / 5000 * 5000 + 105000 as mend, count(*) as count ";
	sql += "from v_Roadkill_new where 1 = 1";
	if (!value_highway_id.Equals("0"))
	    sql += "and highway_id = '" + value_highway_id + "'";
	if (value_up_mileage != 0)
	    sql += "and mileage >= " + value_up_mileage;
	if (value_down_mileage != 0)
	    sql += "and mileage <= " + value_down_mileage;
	sql += " group by highway_id,( mileage-100001) / 5000) as t inner join ";
	sql += "(select free_id, mileage, WGS84X, WGS84Y from freeway_new where ";
	if (!value_highway_id.Equals("0"))
	    sql += "free_id = '" + value_highway_id + "' and ";
	if (value_up_mileage != 0)
	    sql += "mileage >= " + value_up_mileage + " and ";
	if (value_down_mileage != 0)
	    sql += "mileage <= " + value_down_mileage + " and ";
	sql += "direction in ('S','E')) as a";
	sql += " on t.mstart = a.mileage and highway_id = a.free_id inner join (select free_id, mileage, WGS84X, WGS84Y from freeway_new where ";
	if (!value_highway_id.Equals("0"))
	    sql += "free_id = '" + value_highway_id + "' and ";
	if (value_up_mileage != 0)
	    sql += "mileage >= " + value_up_mileage + " and ";
	if (value_down_mileage != 0)
	    sql += "mileage <= " + value_down_mileage + " and ";
	sql += "direction in ('S','E')) as b";
	sql += " on t.mend = b.mileage and highway_id = b.free_id";
	conn.Open();
	cmd = new SqlCommand(sql, conn);
	SqlDataReader reader = cmd.ExecuteReader();

        int i = 0;
        string[,] lxyc = new string[1000, 5];
	while (reader.Read())
	{
	    lxyc[i, 0] = reader[3].ToString();
	    lxyc[i, 1] = reader[4].ToString();
	    lxyc[i, 2] = reader[5].ToString();
	    lxyc[i, 3] = reader[6].ToString();
	    lxyc[i, 4] = reader[7].ToString();
	    i++;
	}
	conn.Close();

	Session["lxyc"] = lxyc;

	string[,] map_lxy = Session["lxyc"] as string[,];
	string aaa = "";
	for (i = 0; i < 1000; i++)
	   aaa += map_lxy[i,0] + "," + map_lxy[i,1] + "," + map_lxy[i,2] + "," + map_lxy[i,3] + "," + map_lxy[i,4] + ";";
	aaa = aaa.Replace(",,,,;","");

        hmap_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        hmap_url = hmap_url.Replace("fun02_011a_.aspx", "hmap02_011b.aspx");
        heatmap.Src = hmap_url;
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
            string Excel_ShortTime = "roadkill_01.xls";
            string folderPath = "D:\\Down_Excel\\"+Excel_ShortTime;
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

    protected void DropDownList_Page_SelectedIndexChanged(object sender, EventArgs e)
    {
        //-- TopPagerRow的屬性值()
        //-- 型別:     System.Web.UI.WebControls.GridViewRow()
        //-- GridViewRow，表示控制項中的 [頂端]頁面巡覽列。注意！有另外一個 BottomPagerRow 屬性
        GridViewRow pagerRow = GridView_List.TopPagerRow;

        DropDownList DDL = (DropDownList)pagerRow.FindControl("DropDownList_Page");

        //-- 使用者在 DropDownList選擇的頁數，傳給 GridView來展示這一頁
        GridView_List.PageIndex = DDL.SelectedIndex;
    }


    //*************************************************
    //  注意這個事件！不是 RowDataBound事件喔～
    //*************************************************
    protected void GridView_List_DataBound(object sender, EventArgs e)
    {
        
        int a = GridView_List.PageIndex;
        //-- TopPagerRow的屬性值()
        //-- 型別:     System.Web.UI.WebControls.GridViewRow()
        //-- GridViewRow，表示控制項中的 [頂端]頁面巡覽列。注意！有另外一個 BottomPagerRow 屬性

        try
        {
            GridViewRow pagerRow = GridView_List.TopPagerRow;

            if (pagerRow.FindControl("DropDownList_Page") != null)
            {
                DropDownList DDL = (DropDownList)pagerRow.FindControl("DropDownList_Page");
                Label LB = (Label)pagerRow.FindControl("Label_Page");

                if (DDL != null)
                {
                    //-- 把 GridView的頁數，逐一放進下拉式選單的「子選項」
                    for (int i = 0; i < GridView_List.PageCount; i++)
                    {
                        // Create a ListItem object to represent a page.
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        //-- 把目前 GridView頁數，當成 DropDownList的（頁數）預設值
                        if (i == GridView_List.PageIndex)
                            item.Selected = true;

                        DDL.Items.Add(item);
                    }
                }

                if (LB != null)
                {
                    //-- GridView「目前」所在的頁數。因為頁數從零算起，所以要加一！
                    int currentPage = GridView_List.PageIndex + 1;

                    //LB.Text = "Page " + currentPage.ToString() + " of " + GridView_List.PageCount.ToString();
                    //LB.Text =  " of " + GridView_List.PageCount.ToString();
                }
            }

        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

        //Query();
    }

    public void bindDDL()
    {
        //讓下拉式選單顯示頁數
        DropDownList ddlSelectPage = (DropDownList)GridView_List.BottomPagerRow.FindControl("DropDownList_Page");
        for (int i = 0; i < GridView_List.PageCount; i++)
        {
            ddlSelectPage.Items.Add(new ListItem((i + 1).ToString(), i.ToString()));
        }

        //顯示目前頁數
        ddlSelectPage.SelectedIndex = GridView_List.PageIndex;
    }

    protected void ddlSelectPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlSelectPage = (DropDownList)GridView_List.BottomPagerRow.FindControl("DropDownList_Page");

        int pIndex = 0;
        if (int.TryParse(ddlSelectPage.SelectedValue, out pIndex))
        {
            GridView_List.PageIndex = pIndex;
            //getData(GridView_List, "select * from product;");
        }
    }
}