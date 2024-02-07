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


public partial class View_fun01_021a : System.Web.UI.Page
{
    //public int GetCount = 1;
    public string map_url = "";
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun01_021a_.aspx", "gmap01_021a.aspx");
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        //Query();
        Query2();
        
        /*
        if (!IsPostBack)
        {
            Query();
        }
        */
    }

    protected void Query()
    {
        string sql = " SELECT distinct  a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ";
        string sql_xy = " Select Distinct a.x  , a.y  FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1  ";
        string value_group = DropDownList1.SelectedValue;
        string value_highway_id = DropDownList2.SelectedValue;
        string value_Sensitive_Level = DropDownList3.SelectedValue;

        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;
        String value_KeyWords = KeyWords.Text;

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


        int value_up_mileage = int.Parse(value_Start_Mileston1) * 1000 + int.Parse(value_Start_Mileston2);
        int value_down_mileage = int.Parse(value_End_Mileston1) * 1000 + int.Parse(value_End_Mileston2);

        if (!value_KeyWords.Equals(""))
        {
            sql += " And ( b.Chinese_Name Like '%" + value_KeyWords + "%' ";
            sql += "    or b.Short_Name Like '%" + value_KeyWords + "%' )";

            sql_xy += " And ( b.Chinese_Name Like '%" + value_KeyWords + "%' ";
            sql_xy += "    or b.Short_Name Like '%" + value_KeyWords + "%' )";
        }

        if (!value_group.Equals("ALL"))
        {
            sql += " and b.[group] = '" + value_group + "'";
            sql_xy += " and b.[group] = '" + value_group + "'";
        }

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql += " and a.[highway_id] = '" + value_highway_id + "'";
	        sql_xy += " and a.[highway_id] = '" + value_highway_id + "'";
        }

        
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql += " and a.x <= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql += " and a.x >= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
            sql += " and a.y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql += " and a.y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";

            sql_xy += " and a.x <= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql_xy += " and a.x >= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
            sql_xy += " and a.y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
            sql_xy += " and a.y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
        }

        if (!value_Sensitive_Level.Equals("ALL")) {
            sql += " and d.Sensitive_level = '" + value_Sensitive_Level + "'";
            sql_xy += " and d.Sensitive_level = '" + value_Sensitive_Level + "'";
        }


        string value_species = species.SelectedValue.ToString();
        
        //���`����
        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
            {
                sql += " and b.accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
                sql_xy += " and b.accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
            }
            if (value_species.Equals("is_endemic"))
            {
                sql += " and b.accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
                sql_xy += " and b.accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
            }
            if (value_species.Equals("is_alien"))
            {
                sql += " and b.accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
                sql_xy += " and b.accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
            }
        }


        //����϶�
        string value_StartYear = StartYear.Text;
        string value_StartMonth = StartMonth.SelectedValue.ToString();
        string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        string value_EndYear = EndYear.Text;
        string value_EndMonth = EndMonth.SelectedValue.ToString();
        string value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";

        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            sql += " and a.siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
            sql_xy += " and a.siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
        }

        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();

        if (!value_group.Equals("") || !value_highway_id.Equals("") || !value_Sensitive_Level.Equals("") )
        {
            Get_XY(sql_xy);
        }

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // �إ� SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        // ����R�O
        //dt_excel = cmd.ExecuteReader().;

        // �M�z�R�O�M�s�u����C
        cmd.Dispose();
        conn.Dispose();

    }

    protected void Query2()
    {
        string sql = " SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 Where 1=1 ";
        string sql_xy = " Select Distinct x, y  from table_1 Where 1=1  ";
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


        int value_up_mileage = int.Parse(value_Start_Mileston1) * 1000 + int.Parse(value_Start_Mileston2);
        int value_down_mileage = int.Parse(value_End_Mileston1) * 1000 + int.Parse(value_End_Mileston2);

        //if (!value_KeyWords.Equals(""))
        //{
        //    sql += " And ( Chinese_Name Like '%" + value_KeyWords + "%' ";
        //    sql += "    or Short_Name Like '%" + value_KeyWords + "%' )";

        //    sql_xy += " And ( Chinese_Name Like '%" + value_KeyWords + "%' ";
        //    sql_xy += "    or Short_Name Like '%" + value_KeyWords + "%' )";
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (chinese_name like '%" + item + "%' or Short_Name like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                }
                i++;
            }
            sql += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (chinese_name like '%" + value_KeyWords + "%' or Short_Name like '%" + value_KeyWords + "%') ";
        }

        if (!value_group.Equals("ALL"))
        {
            sql += " and [group] = '" + value_group + "'";
            sql_xy += " and [group] = '" + value_group + "'";
        }

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql += " and [highway_id] = '" + value_highway_id + "'";
            sql_xy += " and [highway_id] = '" + value_highway_id + "'";
        }


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

        if (!value_Sensitive_Level.Equals("ALL"))
        {
            sql += " and Sensitive_level = '" + value_Sensitive_Level + "'";
            sql_xy += " and Sensitive_level = '" + value_Sensitive_Level + "'";
        }


        string value_species = species.SelectedValue.ToString();

        //���`����
        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
            {
                sql += " and accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
                sql_xy += " and accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
            }
            if (value_species.Equals("is_endemic"))
            {
                sql += " and accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
                sql_xy += " and accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
            }
            if (value_species.Equals("is_alien"))
            {
                sql += " and accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
                sql_xy += " and accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
            }
        }


        //����϶�
        string value_StartYear = StartYear.Text;
        string value_StartMonth = StartMonth.SelectedValue.ToString();
        string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        string value_EndYear = EndYear.Text;
        string value_EndMonth = EndMonth.SelectedValue.ToString();
        string value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";

        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            //sql += " and siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
            //sql_xy += " and siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
            sql += " and Convert(Varchar(10),date1,126) >= '" + value_StartDate + "' And Convert(Varchar(10),date1,126) <= '" + value_EndDate + "' ";
            sql_xy += " and Convert(Varchar(10),date1,126) >= '" + value_StartDate + "' And Convert(Varchar(10),date1,126) <= '" + value_EndDate + "' ";
        }
        string value_datasource = DataSource.SelectedIndex.ToString();
        switch (value_datasource)
        {
            case "1":
                sql += " and invest_company = '�[��a�ͺA�U�ݦ������q' ";
                break;
            case "2":
                sql += " and invest_company = '�S���ͪ��O�|��s���ߤ����լd' ";
                break;
            default:
                break;
        }


        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();

        if (!value_group.Equals("") || !value_highway_id.Equals("") || !value_Sensitive_Level.Equals(""))
        {
            Get_XY(sql_xy);
        }

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // �إ� SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        // ����R�O
        //dt_excel = cmd.ExecuteReader().;

        // �M�z�R�O�M�s�u����C
        cmd.Dispose();
        conn.Dispose();

    }

    protected void QueryData(object sender, EventArgs e)
    {
        //Query();
        Query2();
    }

    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "�`���ơG" + count;
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
            string[,] xy01_021a = new string[10000, 2];
            while (reader.Read())
            {
                xy01_021a[i, 0] = reader["y"].ToString();
                xy01_021a[i, 1] = reader["x"].ToString();
                i += 1;
            }

            Session["xy01_021a"] = xy01_021a;
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
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
        //GetCount += 1;
    }

    protected void Export_Excel(object sender, EventArgs e)
    {
        GridView_List.PageSize = 100000;
        //�����̫᪺�s�����
        GridView_List.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        GridView_List.DataBind();

        //�ץXEXCEL��
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

            // ����EXCEL Sheet �]�w
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            // Detail Sheet
            ISheet u_sheet_Detail = workbook.CreateSheet("occurrence");

            // CellStyle
            IDataFormat num_format = workbook.CreateDataFormat();
            IFont content_font = workbook.CreateFont();
            content_font.FontName = "�s�ө���";
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

            // �t����u�@�� ��e
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



            //�]�wCellStyle
            //u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            //u_sheet_Detail.GetRow(0).GetCell(11).CellStyle = style_wrap_center;


            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("����W");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("�լd�a�I");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("��D�s��");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("�լd���");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("�g��");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("�n��");


            //�]�wCellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;

            int k = 0;

            // ����� ���
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

                    //�]�wCellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;

                }
            }

            workbook.Write(ms);

            //�NEXCEL�ɮ׶ץX��t�θ���x�s�ؿ�
            //string folderPath = ConfigurationManager.AppSettings["ATTACHMENT_FOLDER_ACCOUNT_PAYABLES"] + EST_YM + "\\" + EST_YM + "�{���w�������.xls";
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "occurrence_02.xls";
            string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
            FileStream fs = new FileStream(folderPath, FileMode.Create, FileAccess.Write);
            fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
            fs.Flush();
            fs.Close();
            workbook = null;
            ms.Close();
            ms.Dispose();

            System.Net.WebClient wc = new System.Net.WebClient(); //�I�s webclient �覡���ɮפU��
            byte[] xfile = null;
            //string docupath = Request.PhysicalApplicationPath;
            xfile = wc.DownloadData(folderPath);
            string xfileName = System.IO.Path.GetFileName(folderPath);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
            HttpContext.Current.Response.ContentType = "application/octet-stream"; //�G�i��覡
            HttpContext.Current.Response.BinaryWrite(xfile); //���e��X�@�ɮפU��
            HttpContext.Current.Response.End();

            //pDT_Account_Payables_Different_Detail = null;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }

    protected void Export_Excel_OO(GridView gv)
    {
        GridView GridView_Excel = gv;
        GridView_Excel.PageSize = 100000;
        //�����̫᪺�s�����
        GridView_Excel.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        GridView_Excel.DataBind();

        //�ץXEXCEL��
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
        GridView_Excel.EnableViewState = false;
        GridView_Excel.AllowPaging = false;
        GridView_Excel.AllowSorting = false;
        StringWriter objStringWriter = new StringWriter();
        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        GridView_Excel.RenderControl(objHtmlTextWriter);

        Response.Write(objStringWriter.ToString());
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // '�B�z'GridView' ����� 'GridView' �����m�� runat=server �����аO����   
    }

    protected void Export_Excel_All(object sender, EventArgs e)
    {
        //string sql = SDS_View1.SelectCommand;
        //SDS_View1.SelectCommand = sql;
        GridView_List.PageSize = 100000;
        GridView_List.DataBind();
        


        //�ץXEXCEL��
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
}