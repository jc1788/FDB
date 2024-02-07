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

public partial class Control_CrossDatabaseSearch : System.Web.UI.Page
{
    public string map_url = "";
    public DataTable dt_excel = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("Control/CrossDatabaseSearch_.aspx", "View/gmap01_021a.aspx");
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (Request.Url.PathAndQuery.IndexOf("?") > 0)
        {
            string value_searchtxt = Request.QueryString["searchtxt"];
            string value_s1 = Request.QueryString["s1"];
            string value_s2 = Request.QueryString["s2"];
            string value_s3 = Request.QueryString["s3"];
            string value_highwayid = Request.QueryString["highwayid"];
            string value_start1 = Request.QueryString["start1"];
            string value_start2 = Request.QueryString["start2"];
            string value_end1 = Request.QueryString["end1"];
            string value_end2 = Request.QueryString["end2"];
            string value_year1 = Request.QueryString["year1"];
            string value_month1 = Request.QueryString["month1"];
            string value_year2 = Request.QueryString["year2"];
            string value_month2 = Request.QueryString["month2"];
            GridView_List.Visible = false;
            GridView_List_02.Visible = false;
            GridView_List_03.Visible = false;
            TotalCounts.Text = "";
            TotalCounts_02.Text = "";
            TotalCounts_03.Text = "";
            if (value_s1 == "0" & value_s2 == "0" & value_s3 == "0")
            {
                Query2(value_searchtxt, value_highwayid, value_start1, value_start2, value_end1, value_end2, value_year1, value_month1, value_year2, value_month2);
                Query2_02(value_searchtxt, value_highwayid, value_start1, value_start2, value_end1, value_end2, value_year1, value_month1, value_year2, value_month2);
                Query2_03(value_searchtxt, value_highwayid, value_start1, value_start2, value_end1, value_end2, value_year1, value_month1, value_year2, value_month2);
                GridView_List.Visible = true;
                GridView_List_02.Visible = true;
                GridView_List_03.Visible = true;
            }
            else
            {
                if (search1.Checked | value_s1 == "1")
                {
                    Query2(value_searchtxt, value_highwayid, value_start1, value_start2, value_end1, value_end2, value_year1, value_month1, value_year2, value_month2);
                    GridView_List.Visible = true;
                }
                if (search2.Checked | value_s2 == "1")
                {
                    Query2_02(value_searchtxt, value_highwayid, value_start1, value_start2, value_end1, value_end2, value_year1, value_month1, value_year2, value_month2);
                    GridView_List_02.Visible = true;
                }
                if (search3.Checked | value_s3 == "1")
                {
                    Query2_03(value_searchtxt, value_highwayid, value_start1, value_start2, value_end1, value_end2, value_year1, value_month1, value_year2, value_month2);
                    GridView_List_03.Visible = true;
                }
            }
        }
        //Query();
        //Query2();

        /*
        if (!IsPostBack)
        {
            Query();
        }
        */
    }

    //protected void Query()
    //{
    //    string sql = " SELECT distinct  a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ";
    //    string sql_xy = " Select Distinct a.x  , a.y  FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1  ";
    //    string value_group = DropDownList1.SelectedValue;
    //    string value_highway_id = DropDownList2.SelectedValue;
    //    string value_Sensitive_Level = DropDownList3.SelectedValue;

    //    String value_Start_Mileston1 = Start_Mileston1.Text;
    //    String value_Start_Mileston2 = Start_Mileston2.Text;
    //    String value_End_Mileston1 = End_Mileston1.Text;
    //    String value_End_Mileston2 = End_Mileston2.Text;
    //    String value_KeyWords = KeyWords.Text;

    //    if (value_Start_Mileston1.Equals(""))
    //    {
    //        value_Start_Mileston1 = "0";
    //        Start_Mileston1.Text = "0";
    //    }

    //    if (value_Start_Mileston2.Equals(""))
    //    {
    //        value_Start_Mileston2 = "0";
    //        Start_Mileston2.Text = "0";
    //    }

    //    if (value_End_Mileston1.Equals(""))
    //    {
    //        value_End_Mileston1 = "0";
    //        End_Mileston1.Text = "0";
    //    }

    //    if (value_End_Mileston2.Equals(""))
    //    {
    //        value_End_Mileston2 = "0";
    //        End_Mileston2.Text = "0";
    //    }


    //    int value_up_mileage = int.Parse(value_Start_Mileston1) * 1000 + int.Parse(value_Start_Mileston2);
    //    int value_down_mileage = int.Parse(value_End_Mileston1) * 1000 + int.Parse(value_End_Mileston2);

    //    if (!value_KeyWords.Equals(""))
    //    {
    //        sql += " And ( b.Chinese_Name Like '%" + value_KeyWords + "%' ";
    //        sql += "    or b.Short_Name Like '%" + value_KeyWords + "%' )";

    //        sql_xy += " And ( b.Chinese_Name Like '%" + value_KeyWords + "%' ";
    //        sql_xy += "    or b.Short_Name Like '%" + value_KeyWords + "%' )";
    //    }

    //    if (!value_group.Equals("ALL"))
    //    {
    //        sql += " and b.[group] = '" + value_group + "'";
    //        sql_xy += " and b.[group] = '" + value_group + "'";
    //    }

    //    if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
    //    {
    //        sql += " and a.[highway_id] = '" + value_highway_id + "'";
    //        sql_xy += " and a.[highway_id] = '" + value_highway_id + "'";
    //    }


    //    if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
    //    {
    //        sql += " and a.x <= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
    //        sql += " and a.x >= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
    //        sql += " and a.y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
    //        sql += " and a.y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";

    //        sql_xy += " and a.x <= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
    //        sql_xy += " and a.x >= (Select Top 1 WGS84X From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
    //        sql_xy += " and a.y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_up_mileage + "') ";
    //        sql_xy += " and a.y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = '" + value_highway_id + "' and mileage = '" + value_down_mileage + "') ";
    //    }

    //    if (!value_Sensitive_Level.Equals("ALL"))
    //    {
    //        sql += " and d.Sensitive_level = '" + value_Sensitive_Level + "'";
    //        sql_xy += " and d.Sensitive_level = '" + value_Sensitive_Level + "'";
    //    }


    //    string value_species = species.SelectedValue.ToString();

    //    //關注物種
    //    if (!value_species.Equals(""))
    //    {
    //        if (value_species.Equals("coa_code"))
    //        {
    //            sql += " and b.accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
    //            sql_xy += " and b.accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
    //        }
    //        if (value_species.Equals("is_endemic"))
    //        {
    //            sql += " and b.accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
    //            sql_xy += " and b.accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
    //        }
    //        if (value_species.Equals("is_alien"))
    //        {
    //            sql += " and b.accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
    //            sql_xy += " and b.accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
    //        }
    //    }


    //    //日期區間
    //    string value_StartYear = StartYear.Text;
    //    string value_StartMonth = StartMonth.SelectedValue.ToString();
    //    string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
    //    string value_EndYear = EndYear.Text;
    //    string value_EndMonth = EndMonth.SelectedValue.ToString();
    //    string value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";

    //    if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
    //    {
    //        sql += " and a.siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
    //        sql_xy += " and a.siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
    //    }

    //    SDS_View.SelectCommand = sql;
    //    GridView_List.DataBind();
    //    GridView_List.PageSize = 20;
    //    btnShowGVCount_Click1();

    //    if (!value_group.Equals("") || !value_highway_id.Equals("") || !value_Sensitive_Level.Equals(""))
    //    {
    //        Get_XY(sql_xy);
    //    }

    //    SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
    //    conn.Open();
    //    SqlCommand cmd;
    //    // 建立 SqlCommand 
    //    cmd = new SqlCommand(sql, conn);
    //    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
    //    {
    //        a.Fill(dt_excel);
    //    }
    //    int aa = dt_excel.Rows.Count;
    //    // 執行命令
    //    //dt_excel = cmd.ExecuteReader().;

    //    // 清理命令和連線物件。
    //    cmd.Dispose();
    //    conn.Dispose();

    //}

    protected void Query2()
    {
        string sql = "SELECT distinct  a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ";
        string sql_xy = " Select Distinct a.x  , a.y  FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1  ";
        string value_highway_id = DropDownList2.SelectedValue;
        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        String value_KeyWords = KeyWords.Text;

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
                sql += " (Chinese_Name like '%" + item + "%' or Short_Name like '%" + item + "%') ";
                sql_xy += " (Chinese_Name like '%" + item + "%' or Short_Name like '%" + item + "%') ";
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
            sql += " and (Chinese_Name like '%" + value_KeyWords + "%' or Short_Name like '%" + value_KeyWords + "%') ";
            sql_xy += " and (Chinese_Name like '%" + value_KeyWords + "%' or Short_Name like '%" + value_KeyWords + "%') ";
        }

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

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql += " and [highway_id] = '" + value_highway_id + "'";
            sql_xy += " and [highway_id] = '" + value_highway_id + "'";
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
        string sql = " Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic From Roadkill_new Where 1=1 ";
        string sql_xy = " Select Distinct x  , y  From Roadkill_new Where 1=1 ";
        string sql_hxy = " Select  x  , y  , count(*) as cnts From Roadkill_new Where x <> 0  ";
        string value_highway_id = DropDownList2.SelectedValue;
        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;
        decimal dec_up = decimal.Parse(value_Start_Mileston1) + decimal.Parse(value_Start_Mileston2) / 1000;
        decimal dec_down = decimal.Parse(value_End_Mileston1) + decimal.Parse(value_End_Mileston2) / 1000;
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        String value_KeyWords = KeyWords.Text;
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
        if (!value_highway_id.Equals("0"))
        {
            sql += " and highway_id = '" + value_highway_id + "'";
            sql_xy += " and highway_id = '" + value_highway_id + "'";
            sql_hxy += " and highway_id = '" + value_highway_id + "'";
        }
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
            sql_xy += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
            sql_hxy += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
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
            sql_xy += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
            sql_hxy += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
        }
        sql_hxy += " group by x , y ";
        SDS_View_02.SelectCommand = sql;
        GridView_List_02.DataBind();
        GridView_List_02.PageSize = 20;
        btnShowGVCount_02_Click1();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        cmd.Dispose();
        conn.Dispose();
    }

    protected void Query2_03()
    {
        string sql = "";
        string sql_ori = "";
        string sql_xy = "";
        string sql_where = "";
        string value_highway_id = DropDownList2.SelectedValue;
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = KeyWords.Text;
        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;

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

        //不統計里程
        //sql = "Select chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage , sum(amount) as amount From Plant_observation  Where chinese_name <> '' "; 
        //sql = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage , amount From Plant_observation  Where chinese_name <> '' "; 

        //填入里程或者關鍵字將數量加總，不填入抓id當KEY值傳到明細頁
        //sql_ori = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        //sql_ori只要填入物種名稱抓取資料的RANGE統計
        //sql_ori = "Select id chinese_name , highway_id , direction , plant_class , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        sql_ori = "Select  chinese_name , highway_id , direction , plant_class , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , start_mileage , end_mileage ,  sum(amount) as amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        sql = "Select chinese_name , highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , sum(amount) as amount , MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage From Plant_observation  Where chinese_name <> '' and delete_flag = '' ";
        sql_where += " Where chinese_name <> '' and delete_flag='' ";
        //sql_where += " Where 1=1 ";
        /*
        if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0))
        {
            sql_ori = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' "; 
        }
        else {
            sql = "Select chinese_name , highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , sum(amount) as amount , MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage From Plant_observation  Where chinese_name <> '' ";  
        }
        */

        //if (!value_KeyWords.Equals(""))
        //{
        //    //if (QueryType.Checked)
        //    //{
        //    //    sql_ori += " And chinese_name Like '%" + value_KeyWords + "%' ";
        //    //    sql += " And chinese_name Like '%" + value_KeyWords + "%' ";
        //    //    sql_where += " And chinese_name Like '%" + value_KeyWords + "%' ";
        //    //}
        //    //else
        //    //{
        //    sql_ori += " And chinese_name = '" + value_KeyWords + "' ";
        //    sql += " And chinese_name = '" + value_KeyWords + "' ";
        //    sql_where += " And chinese_name = '" + value_KeyWords + "' ";
        //    //}
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            sql_where += " and ( ";
            sql_ori += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (chinese_name like '%" + item + "%') ";
                sql_where += " (chinese_name like '%" + item + "%') ";
                sql_ori += " (chinese_name like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                    sql_where += " or ";
                    sql_ori += " or ";
                }
                i++;
            }
            sql += " ) ";
            sql_where += " ) ";
            sql_ori += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (chinese_name like '%" + value_KeyWords + "%' ) ";
            sql_where += " and (chinese_name like '%" + value_KeyWords + "%' ) ";
            sql_ori += " and (chinese_name like '%" + value_KeyWords + "%' ) ";
        }

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql_ori += " and highway_id = '" + value_highway_id + "'";
            sql += " and highway_id = '" + value_highway_id + "'";
            sql_where += " and highway_id = '" + value_highway_id + "'";
        }
        //有填入起始與結束的里程或者關鍵字
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql_ori += " and center_mileage >= '" + value_up_mileage + "' ";
            sql_ori += " and center_mileage <= '" + value_down_mileage + "' ";
            sql += " and center_mileage >= '" + value_up_mileage + "' ";
            sql += " and center_mileage <= '" + value_down_mileage + "' ";
            sql_where += " and center_mileage >= '" + value_up_mileage + "' ";
            sql_where += " and center_mileage <= '" + value_down_mileage + "' ";
            /*
            sql += " and ( " ;
            sql += "        (";
            sql += "            (start_mileage1*1000+start_mileage2) >= '" + value_up_mileage + "' ";
            sql += "            and (start_mileage1*1000+start_mileage2) <= '" + value_down_mileage + "' ";
            sql += "        ) or ";
            sql += "        (";
            sql += "            (end_mileage1*1000+end_mileage2) >= '" + value_up_mileage + "' ";
            sql += "            and (end_mileage1*1000+end_mileage2) <= '" + value_down_mileage + "' ";
            sql += "        ) ";
            sql += "    ) ";

            sql_where += " and ( ";
            sql_where += "        (";
            sql_where += "            (start_mileage1*1000+start_mileage2) >= '" + value_up_mileage + "' ";
            sql_where += "            and (start_mileage1*1000+start_mileage2) <= '" + value_down_mileage + "' ";
            sql_where += "        ) or ";
            sql_where += "        (";
            sql_where += "            (end_mileage1*1000+end_mileage2) >= '" + value_up_mileage + "' ";
            sql_where += "            and (end_mileage1*1000+end_mileage2) <= '" + value_down_mileage + "' ";
            sql_where += "        ) ";
            sql_where += "    ) ";
             */
            //sql += " and b.start_x <= (Select Min(WGS84X) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
            //sql += " and b.start_x >= (Select Max(WGS84X) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
            //sql += " and b.start_y <= (Select Min(WGS84Y) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
            //sql += " and b.start_y >= (Select Max(WGS84Y) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
        }

        //填入物種名稱
        //sql_ori += "Order by  id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ";
        sql_ori += " Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage  ";
        sql += " Group By chinese_name , highway_id , direction , plant_class ";
        /*
        SDS_View_Ori.SelectCommand = sql_ori;
        GridView_Ori.DataBind();
        GridView_Ori.PageSize = 20;
        */

        /*
        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
         */
        //if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0) && value_KeyWords.Equals(""))
        //if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        if (!value_KeyWords.Equals(""))
        {
            //GridView_Ori.EnableViewState = true;
            //GridView_Ori.Visible = true;
            SDS_View_03.SelectCommand = sql_ori;
            GridView_List_03.DataBind();
            GridView_List_03.PageSize = 20;
            GridView_Ori.Visible = false;
            GridView_List_03.Visible = true;
            // btnShowGVCount_Ori();
            btnShowGVCount_03_Click1();
        }
        else
        {
            SDS_View_03.SelectCommand = sql;
            GridView_List_03.DataBind();
            GridView_List_03.PageSize = 20;
            GridView_Ori.Visible = false;
            GridView_List_03.Visible = true;
            btnShowGVCount_03_Click1();
        }


        //組googlemap所需的資料

        //先拿掉統計數據
        //sql_xy += " Select Distinct (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y ,  ";
        //sql_xy += " Select (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y , sum(amount) AS amount , ";
        //改為抓Center_x與Center_y
        //if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0) && value_KeyWords.Equals(""))
        if (!value_KeyWords.Equals(""))
        {
            sql_xy = "";
            sql_xy += " Select center_x AS x , center_y AS y , center_x , center_y , sum(amount) as amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id , direction ";
            sql_xy += "        , (Select Max(amount) AS max_amount From (select sum(amount) AS amount From Plant_observation " + sql_where + " and start_x is not null  Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage , center_x , center_y ) tmp ) AS max_amount";
            sql_xy += " From Plant_observation " + sql_where;
            sql_xy += " Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage , center_x , center_y ";
            /*
            sql_xy += " Select center_x AS x , center_y AS y , sum(amount) AS amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id , ";
            sql_xy += " (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " and start_x is not null Group By Center_Mileage,chinese_Name) tmp) AS max_amount ,";
            sql_xy += " (Select (Max(start_x) + Min(start_x)) /2 From  Plant_observation " + sql_where + " And start_x is not null) AS center_x ,";
            sql_xy += " (Select (Max(start_y) + Min(start_y)) /2 From  Plant_observation " + sql_where + " And start_x is not null) AS center_y ";
            sql_xy += " From Plant_observation ";
            sql_xy += sql_where;
            sql_xy += " Group By center_x  , center_y ,chinese_name , highway_id , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) Order By center_x , center_y";
            */
            /*
            sql_xy = "";
            sql_xy += " Select center_x AS x , center_y AS y , center_x , center_y ,amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id ";
            sql_xy += "        , (Select Max(amount) AS max_amount From Plant_observation " + sql_where + " and start_x is not null) AS max_amount";
            sql_xy += " From Plant_observation " + sql_where ;
             */

        }
        else
        {
            sql_xy = "";
            sql_xy += " Select a.* , b.x , b.y , b.x AS center_x , b.y AS center_y  ";
            sql_xy += "  From ( ";
            sql_xy += "          Select chinese_name , highway_id  , plant_class, sum(amount) AS amount , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , Round((MIN(start_mileage)+MAX(end_mileage))/2,1)*1000 AS center_mileage ";
            sql_xy += "                , Case When highway_id in ('1','3','5','7','9') And Direction Not In ('N','S') Then 'N' When highway_id not in ('1','3','5','7','9') And Direction Not In  ('E','W') Then 'E' Else Direction End AS direction ";
            sql_xy += "                , (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " Group By chinese_name , highway_id , direction , plant_class ) tmp ) AS max_amount  ";
            sql_xy += "          From plant_observation ";
            sql_xy += sql_where;
            sql_xy += "          Group By chinese_name , highway_id , direction , plant_class ";
            sql_xy += "         ) a , freeway_2016 b ";
            sql_xy += " Where a.highway_id = b.free_id and a.direction = b.direction and a.center_mileage = b.mileage ";


            /*
            sql_xy += " Select a.* , b.TMD67X AS x , b.TMD67Y AS y  , '23.123' AS center_y , '123.3' AS center_x , ";
            sql_xy += " (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " and start_x is not null Group By chinese_name , highway_id , direction , plant_class) tmp) AS max_amount ";
            sql_xy += " From  ";
            sql_xy += " ( ";
            sql_xy += "   Select chinese_name ,  highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range ,  MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage , Sum(amount) AS amount , Round((MIN(start_mileage)+MAX(end_mileage))/2,1)*1000 AS centter_mileage ";
            sql_xy += "   From ( Select delete_flag , end_mileage , start_mileage , chinese_name , highway_id , plant_class , start_mileage1  , start_mileage2 , end_mileage1  , end_mileage2 , amount , Case When highway_id in ('1','3','5','7','9') And Direction Not In ('N','S') Then 'N' When highway_id not in ('1','3','5','7','9') And Direction Not In  ('E','W') Then 'E' Else Direction End AS direction From Plant_observation ) P  ";
            sql_xy += sql_where;
            sql_xy += "   Group By chinese_name , highway_id , direction , plant_class ";
            sql_xy += " ) a , Freeway_new b";
            sql_xy += " Where a.highway_id=b.free_id ";
            sql_xy += " And a.direction = b.direction  ";
            sql_xy += " And a.centter_mileage=b.mileage ";
             */

        }
        //sql += " Group By chinese_name , highway_id , direction , plant_class ,center_mileage";
        Get_XY(sql_xy);
    }

    protected void Query2(string value_searchtxt, string value_highwayid, string value_start1, string value_start2, string value_end1, string value_end2, string value_year1, string value_month1, string value_year2, string value_month2)
    {
        string sql = "SELECT distinct  a.siteid, CONVERT (char(10), a.date1, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,2) as x , Round(a.y,2) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ";
        string sql_xy = " Select Distinct a.x  , a.y  FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1  ";
        string value_highway_id = value_highwayid;
        String value_Start_Mileston1 = value_start1;
        String value_Start_Mileston2 = value_start2;
        String value_End_Mileston1 = value_end1;
        String value_End_Mileston2 = value_end2;
        string tmp_chinese_name = value_searchtxt;
        string[] tmp_chinese_name_array = (value_searchtxt.IndexOf(" ") > 0) ? tmp_chinese_name.Split(' ') : null;
        String value_KeyWords = value_searchtxt;

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
                sql += " (Chinese_Name like '%" + item + "%' or Short_Name like '%" + item + "%') ";
                sql_xy += " (Chinese_Name like '%" + item + "%' or Short_Name like '%" + item + "%') ";
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
            sql += " and (Chinese_Name like '%" + value_KeyWords + "%' or Short_Name like '%" + value_KeyWords + "%') ";
            sql_xy += " and (Chinese_Name like '%" + value_KeyWords + "%' or Short_Name like '%" + value_KeyWords + "%') ";
        }

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

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql += " and [highway_id] = '" + value_highway_id + "'";
            sql_xy += " and [highway_id] = '" + value_highway_id + "'";
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

        //日期區間
        string value_StartYear = value_year1;
        string value_StartMonth = value_month1;
        string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        string value_EndYear = value_year2;
        string value_EndMonth = value_month2;
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

    protected void Query2_02(string value_searchtxt, string value_highwayid, string value_start1, string value_start2, string value_end1, string value_end2, string value_year1, string value_month1, string value_year2, string value_month2)
    {
        string sql = " Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic From Roadkill_new Where 1=1 ";
        string sql_xy = " Select Distinct x  , y  From Roadkill_new Where 1=1 ";
        string sql_hxy = " Select  x  , y  , count(*) as cnts From Roadkill_new Where x <> 0  ";
        string value_highway_id = value_highwayid;
        String value_Start_Mileston1 = value_start1;
        String value_Start_Mileston2 = value_start2;
        String value_End_Mileston1 = value_end1;
        String value_End_Mileston2 = value_end2;
        string tmp_chinese_name = value_searchtxt;
        string[] tmp_chinese_name_array = (value_searchtxt.IndexOf(" ") > 0) ? tmp_chinese_name.Split(' ') : null;
        String value_KeyWords = value_searchtxt;
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
        if (!value_highway_id.Equals("0"))
        {
            sql += " and highway_id = '" + value_highway_id + "'";
            sql_xy += " and highway_id = '" + value_highway_id + "'";
            sql_hxy += " and highway_id = '" + value_highway_id + "'";
        }
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
            sql_xy += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
            sql_hxy += " and milestone <= '" + dec_down + "' and milestone >= '" + dec_up + "' ";
        }
        //日期區間
        string value_StartYear = value_year1;
        string value_StartMonth = value_month1;
        string value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        string value_EndYear = value_year2;
        string value_EndMonth = value_month2;
        string value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";
        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            sql += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
            sql_xy += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
            sql_hxy += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
        }
        sql_hxy += " group by x , y ";
        SDS_View_02.SelectCommand = sql;
        GridView_List_02.DataBind();
        GridView_List_02.PageSize = 20;
        btnShowGVCount_02_Click1();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        cmd.Dispose();
        conn.Dispose();
    }

    protected void Query2_03(string value_searchtxt, string value_highwayid, string value_start1, string value_start2, string value_end1, string value_end2, string value_year1, string value_month1, string value_year2, string value_month2)
    {
        string sql = "";
        string sql_ori = "";
        string sql_xy = "";
        string sql_where = "";
        string value_highway_id = value_highwayid;
        string tmp_chinese_name = value_searchtxt;
        string[] tmp_chinese_name_array = (value_searchtxt.IndexOf(" ") > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = value_searchtxt;
        String value_Start_Mileston1 = value_start1;
        String value_Start_Mileston2 = value_start2;
        String value_End_Mileston1 = value_end1;
        String value_End_Mileston2 = value_end2;

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

        //不統計里程
        //sql = "Select chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage , sum(amount) as amount From Plant_observation  Where chinese_name <> '' "; 
        //sql = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage , amount From Plant_observation  Where chinese_name <> '' "; 

        //填入里程或者關鍵字將數量加總，不填入抓id當KEY值傳到明細頁
        //sql_ori = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        //sql_ori只要填入物種名稱抓取資料的RANGE統計
        //sql_ori = "Select id chinese_name , highway_id , direction , plant_class , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        sql_ori = "Select  chinese_name , highway_id , direction , plant_class , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , start_mileage , end_mileage ,  sum(amount) as amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        sql = "Select chinese_name , highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , sum(amount) as amount , MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage From Plant_observation  Where chinese_name <> '' and delete_flag = '' ";
        sql_where += " Where chinese_name <> '' and delete_flag='' ";
        //sql_where += " Where 1=1 ";
        /*
        if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0))
        {
            sql_ori = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' "; 
        }
        else {
            sql = "Select chinese_name , highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , sum(amount) as amount , MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage From Plant_observation  Where chinese_name <> '' ";  
        }
        */

        //if (!value_KeyWords.Equals(""))
        //{
        //    //if (QueryType.Checked)
        //    //{
        //    //    sql_ori += " And chinese_name Like '%" + value_KeyWords + "%' ";
        //    //    sql += " And chinese_name Like '%" + value_KeyWords + "%' ";
        //    //    sql_where += " And chinese_name Like '%" + value_KeyWords + "%' ";
        //    //}
        //    //else
        //    //{
        //    sql_ori += " And chinese_name = '" + value_KeyWords + "' ";
        //    sql += " And chinese_name = '" + value_KeyWords + "' ";
        //    sql_where += " And chinese_name = '" + value_KeyWords + "' ";
        //    //}
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            sql_where += " and ( ";
            sql_ori += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (chinese_name like '%" + item + "%') ";
                sql_where += " (chinese_name like '%" + item + "%') ";
                sql_ori += " (chinese_name like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                    sql_where += " or ";
                    sql_ori += " or ";
                }
                i++;
            }
            sql += " ) ";
            sql_where += " ) ";
            sql_ori += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (chinese_name like '%" + value_KeyWords + "%' ) ";
            sql_where += " and (chinese_name like '%" + value_KeyWords + "%' ) ";
            sql_ori += " and (chinese_name like '%" + value_KeyWords + "%' ) ";
        }

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql_ori += " and highway_id = '" + value_highway_id + "'";
            sql += " and highway_id = '" + value_highway_id + "'";
            sql_where += " and highway_id = '" + value_highway_id + "'";
        }
        //有填入起始與結束的里程或者關鍵字
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql_ori += " and center_mileage >= '" + value_up_mileage + "' ";
            sql_ori += " and center_mileage <= '" + value_down_mileage + "' ";
            sql += " and center_mileage >= '" + value_up_mileage + "' ";
            sql += " and center_mileage <= '" + value_down_mileage + "' ";
            sql_where += " and center_mileage >= '" + value_up_mileage + "' ";
            sql_where += " and center_mileage <= '" + value_down_mileage + "' ";
            /*
            sql += " and ( " ;
            sql += "        (";
            sql += "            (start_mileage1*1000+start_mileage2) >= '" + value_up_mileage + "' ";
            sql += "            and (start_mileage1*1000+start_mileage2) <= '" + value_down_mileage + "' ";
            sql += "        ) or ";
            sql += "        (";
            sql += "            (end_mileage1*1000+end_mileage2) >= '" + value_up_mileage + "' ";
            sql += "            and (end_mileage1*1000+end_mileage2) <= '" + value_down_mileage + "' ";
            sql += "        ) ";
            sql += "    ) ";

            sql_where += " and ( ";
            sql_where += "        (";
            sql_where += "            (start_mileage1*1000+start_mileage2) >= '" + value_up_mileage + "' ";
            sql_where += "            and (start_mileage1*1000+start_mileage2) <= '" + value_down_mileage + "' ";
            sql_where += "        ) or ";
            sql_where += "        (";
            sql_where += "            (end_mileage1*1000+end_mileage2) >= '" + value_up_mileage + "' ";
            sql_where += "            and (end_mileage1*1000+end_mileage2) <= '" + value_down_mileage + "' ";
            sql_where += "        ) ";
            sql_where += "    ) ";
             */
            //sql += " and b.start_x <= (Select Min(WGS84X) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
            //sql += " and b.start_x >= (Select Max(WGS84X) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
            //sql += " and b.start_y <= (Select Min(WGS84Y) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
            //sql += " and b.start_y >= (Select Max(WGS84Y) From Freeway Where free_id = '" + value_highway_id + "' and mileage >= '" + value_up_mileage + "' and mileage <= '" + value_down_mileage + "') ";
        }

        //填入物種名稱
        //sql_ori += "Order by  id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ";
        sql_ori += " Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage  ";
        sql += " Group By chinese_name , highway_id , direction , plant_class ";
        /*
        SDS_View_Ori.SelectCommand = sql_ori;
        GridView_Ori.DataBind();
        GridView_Ori.PageSize = 20;
        */

        /*
        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
         */
        //if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0) && value_KeyWords.Equals(""))
        //if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        if (!value_KeyWords.Equals(""))
        {
            //GridView_Ori.EnableViewState = true;
            //GridView_Ori.Visible = true;
            SDS_View_03.SelectCommand = sql_ori;
            GridView_List_03.DataBind();
            GridView_List_03.PageSize = 20;
            GridView_Ori.Visible = false;
            GridView_List_03.Visible = true;
            // btnShowGVCount_Ori();
            btnShowGVCount_03_Click1();
        }
        else
        {
            SDS_View_03.SelectCommand = sql;
            GridView_List_03.DataBind();
            GridView_List_03.PageSize = 20;
            GridView_Ori.Visible = false;
            GridView_List_03.Visible = true;
            btnShowGVCount_03_Click1();
        }


        //組googlemap所需的資料

        //先拿掉統計數據
        //sql_xy += " Select Distinct (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y ,  ";
        //sql_xy += " Select (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y , sum(amount) AS amount , ";
        //改為抓Center_x與Center_y
        //if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0) && value_KeyWords.Equals(""))
        if (!value_KeyWords.Equals(""))
        {
            sql_xy = "";
            sql_xy += " Select center_x AS x , center_y AS y , center_x , center_y , sum(amount) as amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id , direction ";
            sql_xy += "        , (Select Max(amount) AS max_amount From (select sum(amount) AS amount From Plant_observation " + sql_where + " and start_x is not null  Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage , center_x , center_y ) tmp ) AS max_amount";
            sql_xy += " From Plant_observation " + sql_where;
            sql_xy += " Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage , center_x , center_y ";
            /*
            sql_xy += " Select center_x AS x , center_y AS y , sum(amount) AS amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id , ";
            sql_xy += " (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " and start_x is not null Group By Center_Mileage,chinese_Name) tmp) AS max_amount ,";
            sql_xy += " (Select (Max(start_x) + Min(start_x)) /2 From  Plant_observation " + sql_where + " And start_x is not null) AS center_x ,";
            sql_xy += " (Select (Max(start_y) + Min(start_y)) /2 From  Plant_observation " + sql_where + " And start_x is not null) AS center_y ";
            sql_xy += " From Plant_observation ";
            sql_xy += sql_where;
            sql_xy += " Group By center_x  , center_y ,chinese_name , highway_id , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) Order By center_x , center_y";
            */
            /*
            sql_xy = "";
            sql_xy += " Select center_x AS x , center_y AS y , center_x , center_y ,amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id ";
            sql_xy += "        , (Select Max(amount) AS max_amount From Plant_observation " + sql_where + " and start_x is not null) AS max_amount";
            sql_xy += " From Plant_observation " + sql_where ;
             */

        }
        else
        {
            sql_xy = "";
            sql_xy += " Select a.* , b.x , b.y , b.x AS center_x , b.y AS center_y  ";
            sql_xy += "  From ( ";
            sql_xy += "          Select chinese_name , highway_id  , plant_class, sum(amount) AS amount , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , Round((MIN(start_mileage)+MAX(end_mileage))/2,1)*1000 AS center_mileage ";
            sql_xy += "                , Case When highway_id in ('1','3','5','7','9') And Direction Not In ('N','S') Then 'N' When highway_id not in ('1','3','5','7','9') And Direction Not In  ('E','W') Then 'E' Else Direction End AS direction ";
            sql_xy += "                , (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " Group By chinese_name , highway_id , direction , plant_class ) tmp ) AS max_amount  ";
            sql_xy += "          From plant_observation ";
            sql_xy += sql_where;
            sql_xy += "          Group By chinese_name , highway_id , direction , plant_class ";
            sql_xy += "         ) a , freeway_2016 b ";
            sql_xy += " Where a.highway_id = b.free_id and a.direction = b.direction and a.center_mileage = b.mileage ";


            /*
            sql_xy += " Select a.* , b.TMD67X AS x , b.TMD67Y AS y  , '23.123' AS center_y , '123.3' AS center_x , ";
            sql_xy += " (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " and start_x is not null Group By chinese_name , highway_id , direction , plant_class) tmp) AS max_amount ";
            sql_xy += " From  ";
            sql_xy += " ( ";
            sql_xy += "   Select chinese_name ,  highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range ,  MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage , Sum(amount) AS amount , Round((MIN(start_mileage)+MAX(end_mileage))/2,1)*1000 AS centter_mileage ";
            sql_xy += "   From ( Select delete_flag , end_mileage , start_mileage , chinese_name , highway_id , plant_class , start_mileage1  , start_mileage2 , end_mileage1  , end_mileage2 , amount , Case When highway_id in ('1','3','5','7','9') And Direction Not In ('N','S') Then 'N' When highway_id not in ('1','3','5','7','9') And Direction Not In  ('E','W') Then 'E' Else Direction End AS direction From Plant_observation ) P  ";
            sql_xy += sql_where;
            sql_xy += "   Group By chinese_name , highway_id , direction , plant_class ";
            sql_xy += " ) a , Freeway_new b";
            sql_xy += " Where a.highway_id=b.free_id ";
            sql_xy += " And a.direction = b.direction  ";
            sql_xy += " And a.centter_mileage=b.mileage ";
             */

        }
        //sql += " Group By chinese_name , highway_id , direction , plant_class ,center_mileage";
        Get_XY(sql_xy);
    }

    protected void QueryData(object sender, EventArgs e)
    {
        GridView_List.Visible = false;
        GridView_List_02.Visible = false;
        GridView_List_03.Visible = false;
        TotalCounts.Text = "";
        TotalCounts_02.Text = "";
        TotalCounts_03.Text = "";
        if (!search1.Checked & !search2.Checked & !search3.Checked)
        {
            Query2();
            Query2_02();
            Query2_03();
            GridView_List.Visible = true;
            GridView_List_02.Visible = true;
            GridView_List_03.Visible = true;
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
            if (search3.Checked)
            {
                Query2_03();
                GridView_List_03.Visible = true;
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
    protected void btnShowGVCount_03_Click1()
    {
        DataView view = (DataView)SDS_View_03.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts_03.Text = "總筆數：" + count;
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
            string Excel_ShortTime = "occurrence_02.xls";
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

    protected void Export_Excel_OO(GridView gv)
    {
        GridView GridView_Excel = gv;
        GridView_Excel.PageSize = 100000;
        //移除最後的連結欄位
        GridView_Excel.Columns.RemoveAt(GridView_List.Columns.Count - 1);
        GridView_Excel.DataBind();

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
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }

    protected void Export_Excel_All(object sender, EventArgs e)
    {
        //string sql = SDS_View1.SelectCommand;
        //SDS_View1.SelectCommand = sql;
        GridView_List.PageSize = 100000;
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
}