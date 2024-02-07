using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;


public partial class View_fun_011 : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    //DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        Page.MaintainScrollPositionOnPostBack = true;

        //Query();
        Query2();

        /*
        DataSet ds = new DataSet();

        //ds.Tables["Data"].Clear();
        cmd.Connection = conn;
        //cmd.CommandText = " select siteid as '時間地點代碼' , chinese_name as '中文名' , ScientificName as '學名' , [group] as '物種類群' , Collector_ch as '調查者中文名' , Way_ch as '調查方法' from Occurrence_new where 1=1 ";
        String sqlQuery = " select Distinct chinese_name as '中文名' , ScientificName as '學名' , [group] as '物種類群' , Collector_ch as '調查者中文名' , Way_ch as '調查方法' from Occurrence_new where 1=1 ";
        cmd.CommandText = sqlQuery;
        da.SelectCommand = cmd;
        da.Fill(ds, "Data");

        //GridView1.Visible = true;
        GridView1.DataSource = ds;
        GridView1.DataBind();
        GridView1.Visible = false;
        */

        /*
            
        if (IsPostBack)
        {
            Query();

            SqlDataSource1.SelectCommand = "Select DISTINCT a.Chinese_name , a.ScientificName , a.[Group] From occurrence_new a , site_new b Where a.siteid= b.siteid ";
            if (!TextBox1.Text.Equals("ALL")) {
                SqlDataSource1.SelectCommand += " and (a.chinese_name like '%" + TextBox1.Text.ToString() + "%' or a.ScientificName like '%" + Group.SelectedValue.ToString() + "%') ";
            }

            if (!Group.SelectedValue.Equals("ALL"))
            {
                SqlDataSource1.SelectCommand += " and a.[Group] = '" + Group.SelectedValue.ToString() + "' ";
            }
            
            GridView2.DataBind();
            

            
            String value_chinese_name = TextBox1.Text.ToString();
            String value_group = Group.SelectedValue.ToString();
            String sql = "Select DISTINCT a.Chinese_name , a.ScientificName , a.[Group] From occurrence_new a , site_new b Where a.siteid= b.siteid ";

            


            //"ORDER BY [Group], [Chinese_name]";
            if (!value_chinese_name.Equals("ALL"))
            {
                sql += " and (a.chinese_name like '%" + value_chinese_name + "%' or a.ScientificName like '%" + value_chinese_name + "%') ";
            }
            if (!value_group.Equals("ALL"))
            {
                sql += " and a.[Group] = '" + value_group + "' ";
            }

            sql += " ORDER BY [Group], [Chinese_name] ";
            SqlDataSource1.SelectCommand = sql.ToString();
            GridView2.DataBind();
            //GridView2.PageSize = 20;
            
        }
        */
    }

    protected void QueryData(object sender, EventArgs e)
    {
        //Query();
        Query2();
    }

    protected void Query()
    {
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
        string value_chinese_name = chinese_name.Text.ToString();
        string value_group = Group.SelectedValue.ToString();
        string value_datasource = DataSource.SelectedIndex.ToString();

        //string sql = "Select DISTINCT a.Chinese_name , a.ScientificName  , a.[Group] , a.ScientificName + '&StartDate= " + value_StartDate + "'+"+ "'&EndDate= " + value_EndDate + "' AS url    From occurrence_new a , site_new b Where a.siteid= b.siteid ";
        string sql = "Select DISTINCT a.Chinese_name , a.Short_Name  , a.[Group] , a.Short_Name + '&StartDate= " + value_StartDate + "'+" + "'&EndDate= " + value_EndDate + "' AS url    From occurrence_new a join site_new b on a.siteid= b.siteid Where 1=1 and Short_name <> '' ";

        //"ORDER BY [Group], [Chinese_name]";
        if (!value_chinese_name.Equals("ALL") && !value_chinese_name.Equals(""))
        {
            sql += " and (a.chinese_name like '%" + value_chinese_name + "%' or a.Short_Name like '%" + value_chinese_name + "%') ";
        }
        if (!value_group.Equals("ALL") && !value_group.Equals("0"))
        {
            sql += " and a.[Group] = '" + value_group + "' ";
        }

        string value_species = species.SelectedValue.ToString();
        //關注物種
        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
            {
                sql += " and a.accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
            }
            if (value_species.Equals("is_endemic"))
            {
                sql += " and a.accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
            }
            if (value_species.Equals("is_alien"))
            {
                sql += " and a.accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
            }
        }

        //日期區間


        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            sql += " and b.siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
        }

        sql += " ORDER BY [Group], [Chinese_name] ";
        SDS_View.SelectCommand = sql.ToString();
        GridView_List.DataBind();
        //ObjectDataSource ODS= new ObjectDataSource ;
        btnShowGVCount_Click1();
    }

    protected void Query2()
    {
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
        string tmp_chinese_name = chinese_name.Text.ToString();
        string[] tmp_chinese_name_array = (chinese_name.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_chinese_name = chinese_name.Text.ToString();
        string value_group = Group.SelectedValue.ToString();
	string value_site_ch = tb_site_ch.Text;

        //string sql = "Select DISTINCT a.Chinese_name , a.ScientificName  , a.[Group] , a.ScientificName + '&StartDate= " + value_StartDate + "'+"+ "'&EndDate= " + value_EndDate + "' AS url    From occurrence_new a , site_new b Where a.siteid= b.siteid ";
        string sql = "Select DISTINCT Chinese_name , Short_Name  , [Group] , Short_Name + '&StartDate= " + value_StartDate + "'+" + "'&EndDate= " + value_EndDate + "'+" + "'&SiteCh=" + value_site_ch + "' AS url From Table_1 Where 1=1 and Short_name <> '' ";
        string value_datasource = DataSource.SelectedIndex.ToString();
        switch (value_datasource)
        {
            case "1":
                sql += " and invest_company = '觀察家生態顧問有限公司' ";
                break;
            case "2":
                sql += " and invest_company = '特有生物保育研究中心公民調查' ";
                break;
            default:
                break;
        }

        //"ORDER BY [Group], [Chinese_name]";
        //if (!value_chinese_name.Equals("ALL") && !value_chinese_name.Equals(""))
        //{
        //    sql += " and (chinese_name like '%" + value_chinese_name + "%' or Short_Name like '%" + value_chinese_name + "%') ";
        //}
        if (!value_group.Equals("ALL") && !value_group.Equals("0"))
        {
            sql += " and [Group] = '" + value_group + "' ";
        }

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
        else if (!value_chinese_name.Equals("ALL") && !value_chinese_name.Equals(""))
        {
            sql += " and (chinese_name like '%" + value_chinese_name + "%' or Short_Name like '%" + value_chinese_name + "%') ";
        }

	if (!String.IsNullOrEmpty(value_site_ch))
	    sql += " and (site_ch like '%' + @site_ch + '%') ";

        string value_species = species.SelectedValue.ToString();
        //關注物種
        if (!value_species.Equals(""))
        {
            if (value_species.Equals("coa_code"))
            {
                sql += " and accepted_name_code in (select name_code from Endanger_species where coa_code <> '') ";
            }
            if (value_species.Equals("is_endemic"))
            {
                sql += " and accepted_name_code in (select name_code from Endanger_species where is_endemic <> '0') ";
            }
            if (value_species.Equals("is_alien"))
            {
                sql += " and accepted_name_code in (select name_code from Endanger_species where is_alien <> '0') ";
            }
        }

        //日期區間


        if (!value_StartYear.Equals("") && !value_StartMonth.Equals("") && !value_EndYear.Equals("") && !value_EndMonth.Equals(""))
        {
            //sql += " and siteid in (Select siteid  From Site_new Where Convert(Varchar(10),date1) >= '" + value_StartDate + "' And Convert(Varchar(10),date1) <= '" + value_EndDate + "' ) ";
            sql += " and Convert(Varchar(10),date1,126) >= '" + value_StartDate + "' And Convert(Varchar(10),date1,126) <= '" + value_EndDate + "' ";
        }

        sql += " ORDER BY [Group], [Chinese_name] ";
        SDS_View.SelectCommand = sql.ToString();
	SDS_View.SelectParameters.Clear();
	if (!String.IsNullOrEmpty(value_site_ch))
	    SDS_View.SelectParameters.Add("site_ch",value_site_ch);
        GridView_List.DataBind();
        //ObjectDataSource ODS= new ObjectDataSource ;
        btnShowGVCount_Click1();
    }

    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }

    /*
    protected void Button1_Click(object sender, EventArgs e)
    {
        //String sqlQuery = " select siteid as '時間地點代碼' , chinese_name as '中文名' , ScientificName as '學名' , [group] as '物種類群' , Collector_ch as '調查者中文名' , Way_ch as '調查方法' from Occurrence_new where 1=1 ";
        String sqlQuery = " select Distinct chinese_name as '中文名' , ScientificName as '學名' , [group] as '物種類群' , Collector_ch as '調查者中文名' , Way_ch as '調查方法' from Occurrence_new where 1=1 ";
        String query_text = TextBox1.Text;
        String value_Group = Group.SelectedValue;
        
        //GetData();
        DataSet ds = new DataSet();

        //ds.Tables["Data"].Clear();
        cmd.Connection = conn;
        
        if (!query_text.Equals("")){
            sqlQuery = sqlQuery + " and (chinese_name like '%" + query_text + "%' or ScientificName like '%" + query_text + "%')";
        }

        if (!value_Group.Equals(""))
        {
            sqlQuery = sqlQuery + " and [group]='" + value_Group + "'";
        }

        //sqlQuery = sqlQuery + " order by chinese_name , siteid ";
        sqlQuery = sqlQuery + " order by chinese_name  ";

        cmd.CommandText = sqlQuery;
        

        da.SelectCommand = cmd;
        da.Fill(ds, "Data");

        //GridView1.Visible = true;
        GridView1.DataSource = ds; 
        GridView1.DataBind();
        GridView1.Visible = true;
    }
    */

    /*
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //OnPageIndexChanged="GridView1_PageIndexChanging" OnPageIndexChanging="GridView1_PageIndexChanging"
    }
    */

    /*
    protected void GetData()
    {
        
        try
        {
            ds.Tables["Data"].Clear();
        }
        catch
        {
            cmd.Connection = conn;
            cmd.CommandText = "select siteid,x,y,TM2_X,TM2_Y,date,site_ch,highway_id,range,direction From	Roadkill_site_old";
            da.SelectCommand = cmd;
            da.Fill(ds, "Data");
        }
    }
    */
    protected void Button2_Click(object sender, EventArgs e)
    {
        //Page_Load();
    }


}