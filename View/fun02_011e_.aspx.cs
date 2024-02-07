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

public partial class View_fun02_011e : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        //Query();
    }

    protected void Query_Url(object sender, EventArgs e)
    {
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = KeyWords.Text;
        string x1 = "";
        string y1 = "";
        string x2 = "";
        string y2 = "";
        if (IsPostBack)
        {
            string[] marker1 = box_marker1.Text.Split(',');
            if (!marker1.Equals(""))
            {
                x1 = marker1[1];
                y1 = marker1[0];
            }

            string[] marker2 = box_marker2.Text.Split(',');
            if (!marker1.Equals(""))
            {
                x2 = marker2[1];
                y2 = marker2[0];
            }
        }
        string value_species = species.SelectedValue.ToString();

        //日期區間
        string value_StartYear = StartYear.Text;
        string value_StartMonth = StartMonth.SelectedValue.ToString();
        string value_StartDate = "";
        if (!value_StartYear.Equals("") && !value_StartMonth.Equals(""))
        {
            value_StartDate = value_StartYear + "-" + value_StartMonth + "-01";
        }

        string value_EndYear = EndYear.Text;
        string value_EndMonth = EndMonth.SelectedValue.ToString();
        string value_EndDate = "";
        if (!value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            value_EndDate = value_EndYear + "-" + value_EndMonth + "-31";
        }

        //this.Page.Controls.Add(new LiteralControl("<script>window.open('fun01_021f.aspx','new','menubar=no,status=no,scrollbars=no,top=0,left=0,toolbar=no,width=800,height=600');</script>")); 

        //string url = "fun01_021f.aspx?KeyWords=" + value_KeyWords + "&species=" + value_species + "&StartDate=" + value_StartDate + "&EndDate=" + value_EndDate + "&x1=" + x1 + "&y1=" + y1 + "&x2="+x2+"&y2="+y2;

        if (tmp_chinese_name_array!=null && tmp_chinese_name_array[1]!="")
        {
            foreach (var item in tmp_chinese_name_array)
            {
                string url = "<script language='javascript'>window.open('" + "fun02_011f_.aspx?KeyWords=" + item + "&species=" + value_species + "&StartDate=" + value_StartDate + "&EndDate=" + value_EndDate + "&x1=" + x1 + "&y1=" + y1 + "&x2=" + x2 + "&y2=" + y2 + "','_blank');</script>)";
                Response.Write(url);
            }
        }
        else
        {
            string url = "<script language='javascript'>window.open('" + "fun02_011f_.aspx?KeyWords=" + value_KeyWords + "&species=" + value_species + "&StartDate=" + value_StartDate + "&EndDate=" + value_EndDate + "&x1=" + x1 + "&y1=" + y1 + "&x2=" + x2 + "&y2=" + y2 + "','_blank');</script>)";
            Response.Write(url);
        }
      
        //Response.Redirect(url,false);
        //Response.Write("<script language='javascript'>window.open('" + url + "','_blank')</script>");
    }
    /*
    protected void Query()
    {
        string sql = " Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , '..'+path1+file1 AS lo From Roadkill_new Where 1=1 ";
        string sql_xy = " Select Distinct x  , y  From Roadkill_new Where 1=1 ";


        String value_KeyWords = KeyWords.Text;

        if (IsPostBack)
        {
            string[] marker1 = box_marker1.Text.Split(',');
            if (!marker1.Equals(""))
            {
                string x1 = marker1[1];
                string y1 = marker1[0];
                sql += " And x >= '" + x1 + "'";
                sql += " And y >= '" + y1 + "'";
            }

            string[] marker2 = box_marker2.Text.Split(',');
            if (!marker1.Equals(""))
            {
                string x2 = marker2[1];
                string y2 = marker2[0];
                sql += " And x <= '" + x2 + "'";
                sql += " And y <= '" + y2 + "'";
            }
        }

        if (!value_KeyWords.Equals(""))
        {
            sql += " and (species Like '%" + value_KeyWords + "%'";
            sql += " or deduce_species Like '%" + value_KeyWords + "%')";
            sql_xy += " and (species Like '%" + value_KeyWords + "%'";
            sql_xy += " or deduce_species Like '%" + value_KeyWords + "%')";
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
            sql += " and id in (Select id  From Roadkill_new Where Convert(Varchar(10),date) >= '" + value_StartDate + "' And Convert(Varchar(10),date) <= '" + value_EndDate + "' ) ";
        }

        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        btnShowGVCount_Click1();


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
            string[,] xy02_011a = new string[10000, 2];
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }
    */
}