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


public partial class roadkill_statistic : System.Web.UI.Page
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

	if (!IsPostBack)
	{
	    SetContent();
	    SetContent2();
	}
	else if (Session["roadkill_statistic"] == null || Session["roadkill_statistic"].ToString() == "0")
	    ;
	else if (Session["roadkill_qtype"] != null && Session["roadkill_qtype"].ToString() == "1")
	    btnsearch_Click(sender, e);
	else if (Session["roadkill_qtype"] != null && Session["roadkill_qtype"].ToString() == "2")
	    btnsearch2_Click(sender, e);
    }

    private void SetContent()
    {
	string[] focus_spec1 = {"石虎","麝香貓","環頸雉","台灣山羌"};
	string[] focus_spec2 = {"台灣獼猴","大冠鷲","穿山甲","鼬獾"};
	string[] focus_spec3 = {"台灣野兔","領角鴞","鳳頭蒼鷹","白鼻心"};
	string[] roadkill_line = new string[13];
	string[] site_ch = {"中壢","內湖","木柵","頭城","關西","大甲","斗南","南投","苗栗","白河","岡山","屏東","新營"};
	string[] roadkill_upcount = new string[7];
	int[] roadkill_monthsum = new int[7];
	int[] roadkill_sitesum = new int[13];

	int year = DateTime.Now.Year-1;
	//Select_Year.SelectedValue = year.ToString();
	string focus_year = "";
	for ( int i = year - 9; i <= year; i++ )
	{
	    if (focus_year != "")
		focus_year += ",";
	    focus_year += (i-1911).ToString();
	}
	this.ltl_roadkill_year1.Text = focus_year;
	this.ltl_roadkill_year2.Text = focus_year;
	this.ltl_roadkill_year3.Text = focus_year;

	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

            DataTable roadkill_species = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("select top(9) deduce_species, count(deduce_species) as count from v_Roadkill_new where deduce_species <> '無法辨識' and deduce_species <> '' group by deduce_species order by count desc", conn);
	    //apt.SelectCommand.Parameters.Add("@year", year);
	    apt.Fill(roadkill_species);
	    rptRoadkill_species.DataSource = roadkill_species;
	    rptRoadkill_species.DataBind();

	    string species_count = "";
	    foreach (DataRow dr in roadkill_species.Rows)
	    {
		if (species_count != "")
		    species_count += ",";
		species_count += "{name: \"" + dr[0].ToString() + "\", value: " + dr[1].ToString() + "}";
	    }
	    this.ltl_roadkill_time.Text = "(全部)";
	    this.ltl_roadkill_species.Text = species_count;

            DataTable roadkill_focus = new DataTable();
	    apt = new SqlDataAdapter("select top(9) deduce_species, count(deduce_species) as count from v_Roadkill_new where deduce_species in('白鼻心','台灣獼猴','鳳頭蒼鷹','台灣山羌','領角鴞','石虎','紅尾伯勞','麝香貓','台灣野兔','大赤鼯鼠','八哥','鼬獾','黑翅鳶','台灣畫眉','朱鸝','台灣藍鵲','黃嘴角鴞','大冠鷲','水鹿','穿山甲','環頸雉','梅花鹿') group by deduce_species order by count desc", conn);
	    //apt.SelectCommand.Parameters.Add("@year", year);
	    apt.Fill(roadkill_focus);
	    rptRoadkill_focus.DataSource = roadkill_focus;
	    rptRoadkill_focus.DataBind();

	    string focus_name = "";
	    string focus_count = "";
	    foreach (DataRow dr in roadkill_focus.Rows)
	    {
		if (focus_name != "")
		    focus_name += ",";
		focus_name += "\"" + dr[0].ToString() + "\"";
		if (focus_count != "")
		    focus_count += ",";
		focus_count += dr[1].ToString();
	    }
	    this.ltl_roadkill_time2.Text = "(全部)";
	    this.ltl_roadkill_focus_name.Text = focus_name;
	    this.ltl_roadkill_focus_count.Text = focus_count;
/*
	for ( int i = year - 9; i <= year; i++ )
	{
	    if ( i > year - 9 )
	        for ( int j = 1; j <= 12; j++ )
		    roadkill_line[j] += ",";

	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '石虎'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[1] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '麝香貓'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[2] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '環頸雉'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[3] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '台灣山羌'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[4] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '台灣獼猴'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[5] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '大冠鷲'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[6] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '穿山甲'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[7] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '鼬獾'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[8] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '台灣野兔'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[9] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '領角鴞'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[10] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '鳳頭蒼鷹'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[11] += sdr[0].ToString();
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where yy = "+i+" and deduce_species = '白鼻心'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_line[12] += sdr[0].ToString();
		sdr.Close();
	    }
	}
	    this.ltl_roadkill_line1.Text = roadkill_line[1];
	    this.ltl_roadkill_line2.Text = roadkill_line[2];
	    this.ltl_roadkill_line3.Text = roadkill_line[3];
	    this.ltl_roadkill_line4.Text = roadkill_line[4];
	    this.ltl_roadkill_line5.Text = roadkill_line[5];
	    this.ltl_roadkill_line6.Text = roadkill_line[6];
	    this.ltl_roadkill_line7.Text = roadkill_line[7];
	    this.ltl_roadkill_line8.Text = roadkill_line[8];
	    this.ltl_roadkill_line9.Text = roadkill_line[9];
	    this.ltl_roadkill_line10.Text = roadkill_line[10];
	    this.ltl_roadkill_line11.Text = roadkill_line[11];
	    this.ltl_roadkill_line12.Text = roadkill_line[12];
*/
	    this.ltl_roadkill_line1.Text = "2,2,0,2,3,1,3,1,5,1";
	    this.ltl_roadkill_line2.Text = "1,0,0,0,0,1,6,6,4,7";
	    this.ltl_roadkill_line3.Text = "2,3,3,2,1,2,2,1,3,1";
	    this.ltl_roadkill_line4.Text = "0,0,0,1,1,5,4,8,12,16";
	    this.ltl_roadkill_line5.Text = "0,1,0,3,2,2,3,9,4,3";
	    this.ltl_roadkill_line6.Text = "0,2,4,4,5,8,6,6,15,4";
	    this.ltl_roadkill_line7.Text = "2,0,1,3,2,6,15,7,6,74";
	    this.ltl_roadkill_line8.Text = "1,0,1,1,7,3,26,8,7,6";
	    this.ltl_roadkill_line9.Text = "18,48,19,9,9,3,8,13,6,6";
	    this.ltl_roadkill_line10.Text = "45,21,19,42,24,29,45,30,36,14";
	    this.ltl_roadkill_line11.Text = "41,49,28,56,46,33,58,51,55,22";
	    this.ltl_roadkill_line12.Text = "42,39,41,30,50,63,155,179,146,125";

	this.ltl_sitech_labels.Text = "";
	string sitech_uptable = "<table class=\"table\"><thead><tr><th scope=\"col\"></th>";
	for ( int j = 0; j < 13; j++ )
	{
	    roadkill_sitesum[j] = 0;
	    sitech_uptable += "<th scope=\"col\">"+site_ch[j]+"</th>";
	    if (this.ltl_sitech_labels.Text != "")
		this.ltl_sitech_labels.Text += ",";
	    this.ltl_sitech_labels.Text += "\""+site_ch[j]+"\"";
	}
	sitech_uptable += "<th scope=\"col\">總計</th></tr></thead><tbody>";
 
/*
	for ( int i = 1; i <= 6; i++ )
	{
	    roadkill_upcount[i] = "";
	    roadkill_monthsum[i] = 0;
	    sitech_uptable += "<tr><th scope=\"row\">"+i.ToString()+"月</th>";
	    for ( int j = 0; j < 13; j++ )
	    {
		if (roadkill_upcount[i] != "")
		    roadkill_upcount[i] += ",";
		using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where mm = @month and site_ch = '"+site_ch[j]+"'", conn))
		{
		    cmd.Parameters.Add("@month", i);
		    SqlDataReader sdr = cmd.ExecuteReader();
		    if (sdr.Read())
		    {
			roadkill_upcount[i] += sdr[0].ToString();
			roadkill_monthsum[i] += Convert.ToInt32(sdr[0]);
			roadkill_sitesum[j] += Convert.ToInt32(sdr[0]);
			sitech_uptable += "<td>"+sdr[0].ToString()+"</td>";
		    }
		    sdr.Close();
		}
	    }
	    sitech_uptable += "<td>"+roadkill_monthsum[i]+"</td></tr>";
	}
*/

            DataTable roadkill_upc = new DataTable();
	    apt = new SqlDataAdapter("select mm, site_ch, count(*) as count from v_roadkill_new group by mm, site_ch", conn);
	    apt.Fill(roadkill_upc);

	for ( int i = 1; i <= 6; i++ )
	{
	    roadkill_upcount[i] = "";
	    roadkill_monthsum[i] = 0;
	    sitech_uptable += "<tr><th scope=\"row\">"+i.ToString()+"月</th>";
	    for ( int j = 0; j < 13; j++ )
	    {
		if (roadkill_upcount[i] != "")
		    roadkill_upcount[i] += ",";
		DataRow[] dr = roadkill_upc.Select("mm = '"+i.ToString()+"' and site_ch = '"+site_ch[j]+"'");
		if (dr.Length > 0)
		{
		    roadkill_upcount[i] += dr[0][2].ToString();
		    roadkill_monthsum[i] += Convert.ToInt32(dr[0][2]);
		    roadkill_sitesum[j] += Convert.ToInt32(dr[0][2]);
		    sitech_uptable += "<td>"+dr[0][2].ToString()+"</td>";
		}
		else
		{
		    roadkill_upcount[i] += "0";
		    sitech_uptable += "<td>0</td>";
		}
	    }
	    sitech_uptable += "<td>"+roadkill_monthsum[i]+"</td></tr>";
	}

	roadkill_monthsum[0] = 0;
	sitech_uptable += "<tr><th scope=\"row\">全部</th>";
	for ( int j = 0; j < 13; j++ )
	{
	    sitech_uptable += "<td>"+roadkill_sitesum[j]+"</td>";
	    roadkill_monthsum[0] += roadkill_sitesum[j];
	}
	sitech_uptable += "<td>"+roadkill_monthsum[0]+"</td></tr></tbody></table>";

	this.ltl_sitech_uptable.Text = sitech_uptable;
	this.ltl_up_month1.Text = roadkill_upcount[1];
	this.ltl_up_month2.Text = roadkill_upcount[2];
	this.ltl_up_month3.Text = roadkill_upcount[3];
	this.ltl_up_month4.Text = roadkill_upcount[4];
	this.ltl_up_month5.Text = roadkill_upcount[5];
	this.ltl_up_month6.Text = roadkill_upcount[6];
	this.ltl_month1.Text = "1";
	this.ltl_month2.Text = "2";
	this.ltl_month3.Text = "3";
	this.ltl_month4.Text = "4";
	this.ltl_month5.Text = "5";
	this.ltl_month6.Text = "6";

	this.ltl_roadkill_year_site_months.Text = "全部";
	this.ltl_roadkill_year_animal2.Text = "全部";
	this.ltl_roadkill_month_animal2.Text = "全部";

            DataTable roadkill_year_a2 = new DataTable();
	    apt = new SqlDataAdapter("select animal2, count(animal2) as count from v_roadkill_new where animal2 in ('貓狗','大鳥','中小鳥','果子狸') group by animal2 union all select '其他' as animal2, count(*) as count from v_roadkill_new where animal2 not in ('貓狗','大鳥','中小鳥','果子狸') order by count desc", conn);
	    apt.Fill(roadkill_year_a2);
	    rptRoadkill_year_animal2.DataSource = roadkill_year_a2;
	    rptRoadkill_year_animal2.DataBind();

            DataTable roadkill_month_a2 = new DataTable();
	    apt = new SqlDataAdapter("select animal2, count(animal2) as count from v_roadkill_new where animal2 in ('貓狗','大鳥','中小鳥','果子狸') group by animal2 union all select '其他' as animal2, count(*) as count from v_roadkill_new where animal2 not in ('貓狗','大鳥','中小鳥','果子狸') order by count desc", conn);
	    apt.Fill(roadkill_month_a2);
	    rptRoadkill_month_animal2.DataSource = roadkill_month_a2;
	    rptRoadkill_month_animal2.DataBind();

	this.ltl_roadkill_year_site.Text = "全部";
	this.ltl_roadkill_month_site.Text = "全部";

            DataTable roadkill_year_site = new DataTable();
	    apt = new SqlDataAdapter("select site_ch, count(site_ch) as count from v_roadkill_new where site_ch <> '湖口' group by site_ch order by count desc", conn);
	    apt.Fill(roadkill_year_site);
	    rptRoadkill_year_site.DataSource = roadkill_year_site;
	    rptRoadkill_year_site.DataBind();

            DataTable roadkill_month_site = new DataTable();
	    apt = new SqlDataAdapter("select site_ch, count(site_ch) as count from v_roadkill_new where site_ch <> '湖口' group by site_ch order by count desc", conn);
	    apt.Fill(roadkill_month_site);
	    rptRoadkill_month_site.DataSource = roadkill_month_site;
	    rptRoadkill_month_site.DataBind();

	    conn.Close();
	}
    }



    private void SetContent2()
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataTable dt_highway = new DataTable();
	    SqlDataAdapter apt2 = new SqlDataAdapter("SELECT '0' AS highway_id, 'ALL' AS highway_name UNION ALL SELECT highway_id, highway_name FROM [Highway]", conn);
	    apt2.Fill(dt_highway);
	    highwayida.DataSource = dt_highway;
	    highwayida.DataBind();
	    highwayida.SelectedIndex = 1;

	    DataTable dt_part = new DataTable();
	    SqlDataAdapter apt3 = new SqlDataAdapter("SELECT 0 AS id , 'ALL' AS  class1 UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY id", conn);
	    apt3.Fill(dt_part);
	    DropDownList4a.DataSource = dt_part;
	    DropDownList4a.DataBind();
	    conn.Close();
	}
    }

    protected void btnquery_Click(object sender, EventArgs e)
    {
	Session["roadkill_statistic"] = "0";
	string[] roadkill_line = new string[13];
	string[] site_ch = {"中壢","內湖","木柵","頭城","關西","大甲","斗南","南投","苗栗","白河","岡山","屏東","新營"};
	string[] roadkill_upcount = new string[7];
	int[] roadkill_monthsum = new int[7];
	int[] roadkill_sitesum = new int[13];
	int year = Convert.ToInt32(Select_Year.SelectedValue);
	int month = Convert.ToInt32(Select_Month.SelectedValue);

	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

            DataTable roadkill_species = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("select top(9) deduce_species, count(deduce_species) as count from v_Roadkill_new where (@year = 0 or yy = @year) and (@month = 0 or mm = @month) and deduce_species <> '無法辨識' and deduce_species <> '' group by deduce_species order by count desc", conn);
	    apt.SelectCommand.Parameters.Add("@year", Select_Year.SelectedValue);
	    apt.SelectCommand.Parameters.Add("@month", Select_Month.SelectedValue);
	    apt.Fill(roadkill_species);
	    rptRoadkill_species.DataSource = roadkill_species;
	    rptRoadkill_species.DataBind();

	    string species_count = "";
	    foreach (DataRow dr in roadkill_species.Rows)
	    {
		if (species_count != "")
		    species_count += ",";
		species_count += "{name: \"" + dr[0].ToString() + "\", value: " + dr[1].ToString() + "}";
	    }
	    if (year == 0 && month == 0)
	    this.ltl_roadkill_time.Text = "(全部)";
	    else if (month == 0)
	    this.ltl_roadkill_time.Text = "("+Select_Year.SelectedValue+"年)";
	    else if (year == 0)
	    this.ltl_roadkill_time.Text = "("+Select_Month.SelectedValue+"月)";
	    else
	    this.ltl_roadkill_time.Text = "("+Select_Year.SelectedValue+"年"+Select_Month.SelectedValue+"月)";
	    this.ltl_roadkill_species.Text = species_count;

            DataTable roadkill_focus = new DataTable();
	    apt = new SqlDataAdapter("select top(9) deduce_species, count(deduce_species) as count from v_Roadkill_new where (@year = 0 or yy = @year) and (@month = 0 or mm = @month) and deduce_species in('白鼻心','台灣獼猴','鳳頭蒼鷹','台灣山羌','領角鴞','石虎','紅尾伯勞','麝香貓','台灣野兔','大赤鼯鼠','八哥','鼬獾','黑翅鳶','台灣畫眉','朱鸝','台灣藍鵲','黃嘴角鴞','大冠鷲','水鹿','穿山甲','環頸雉','梅花鹿') group by deduce_species order by count desc", conn);
	    apt.SelectCommand.Parameters.Add("@year", Select_Year.SelectedValue);
	    apt.SelectCommand.Parameters.Add("@month", Select_Month.SelectedValue);
	    apt.Fill(roadkill_focus);
	    rptRoadkill_focus.DataSource = roadkill_focus;
	    rptRoadkill_focus.DataBind();

	    string focus_name = "";
	    string focus_count = "";
	    foreach (DataRow dr in roadkill_focus.Rows)
	    {
		if (focus_name != "")
		    focus_name += ",";
		focus_name += "\"" + dr[0].ToString() + "\"";
		if (focus_count != "")
		    focus_count += ",";
		focus_count += dr[1].ToString();
	    }
	    if (year == 0 && month == 0)
	    this.ltl_roadkill_time2.Text = "(全部)";
	    else if (month == 0)
	    this.ltl_roadkill_time2.Text = "("+Select_Year.SelectedValue+"年)";
	    else if (year == 0)
	    this.ltl_roadkill_time2.Text = "("+Select_Month.SelectedValue+"月)";
	    else
	    this.ltl_roadkill_time2.Text = "("+Select_Year.SelectedValue+"年"+Select_Month.SelectedValue+"月)";
	    this.ltl_roadkill_focus_name.Text = focus_name;
	    this.ltl_roadkill_focus_count.Text = focus_count;

	this.ltl_sitech_labels.Text = "";
	string sitech_uptable = "<table class=\"table\"><thead><tr><th scope=\"col\"></th>";
	for ( int j = 0; j < 13; j++ )
	{
	    roadkill_sitesum[j] = 0;
	    sitech_uptable += "<th scope=\"col\">"+site_ch[j]+"</th>";
	    if (this.ltl_sitech_labels.Text != "")
		this.ltl_sitech_labels.Text += ",";
	    this.ltl_sitech_labels.Text += "\""+site_ch[j]+"\"";
	}
	sitech_uptable += "<th scope=\"col\">總計</th></tr></thead><tbody>";

	if ( month <= 6 )
	    month = 0;
	else
	    month = 6;
/*
	for ( int i = 1; i <= 6; i++ )
	{
	    roadkill_upcount[i] = "";
	    roadkill_monthsum[i] = 0;
	    sitech_uptable += "<tr><th scope=\"row\">"+(i+month).ToString()+"月</th>";
	    for ( int j = 0; j < 13; j++ )
	    {
		if (roadkill_upcount[i] != "")
		    roadkill_upcount[i] += ",";
		using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where (@year = 0 or yy = @year) and mm = @month and site_ch = '"+site_ch[j]+"'", conn))
		{
		    cmd.Parameters.Add("@year", year);
		    cmd.Parameters.Add("@month", i+month); 
		    SqlDataReader sdr = cmd.ExecuteReader();
		    if (sdr.Read())
		    {
			roadkill_upcount[i] += sdr[0].ToString();
			roadkill_monthsum[i] += Convert.ToInt32(sdr[0]);
			roadkill_sitesum[j] += Convert.ToInt32(sdr[0]);
			sitech_uptable += "<td>"+sdr[0].ToString()+"</td>";
		    }
		    sdr.Close();
		}
	    }
	    sitech_uptable += "<td>"+roadkill_monthsum[i]+"</td></tr>";
	}
*/
            DataTable roadkill_upc = new DataTable();
	    apt = new SqlDataAdapter("select mm, site_ch, count(*) as count from v_roadkill_new where "+year.ToString()+" = 0 or yy = '"+year.ToString()+"' group by mm, site_ch", conn);
	    apt.Fill(roadkill_upc);

	for ( int i = 1; i <= 6; i++ )
	{
	    roadkill_upcount[i] = "";
	    roadkill_monthsum[i] = 0;
	    sitech_uptable += "<tr><th scope=\"row\">"+i.ToString()+"月</th>";
	    for ( int j = 0; j < 13; j++ )
	    {
		if (roadkill_upcount[i] != "")
		    roadkill_upcount[i] += ",";
		DataRow[] dr = roadkill_upc.Select("mm = '"+i.ToString()+"' and site_ch = '"+site_ch[j]+"'");
		if (dr.Length > 0)
		{
		    roadkill_upcount[i] += dr[0][2].ToString();
		    roadkill_monthsum[i] += Convert.ToInt32(dr[0][2]);
		    roadkill_sitesum[j] += Convert.ToInt32(dr[0][2]);
		    sitech_uptable += "<td>"+dr[0][2].ToString()+"</td>";
		}
		else
		{
		    roadkill_upcount[i] += "0";
		    sitech_uptable += "<td>0</td>";
		}
	    }
	    sitech_uptable += "<td>"+roadkill_monthsum[i]+"</td></tr>";
	}

	roadkill_monthsum[0] = 0;
	if (year == 0)
	sitech_uptable += "<tr><th scope=\"row\">全部</th>";
	else
	sitech_uptable += "<tr><th scope=\"row\">"+(year-1911).ToString()+"年</th>";
	for ( int j = 0; j < 13; j++ )
	{
	    sitech_uptable += "<td>"+roadkill_sitesum[j]+"</td>";
	    roadkill_monthsum[0] += roadkill_sitesum[j];
	}
	sitech_uptable += "<td>"+roadkill_monthsum[0]+"</td></tr></tbody></table>";

	this.ltl_sitech_uptable.Text = sitech_uptable;
	this.ltl_up_month1.Text = roadkill_upcount[1];
	this.ltl_up_month2.Text = roadkill_upcount[2];
	this.ltl_up_month3.Text = roadkill_upcount[3];
	this.ltl_up_month4.Text = roadkill_upcount[4];
	this.ltl_up_month5.Text = roadkill_upcount[5];
	this.ltl_up_month6.Text = roadkill_upcount[6];
	this.ltl_month1.Text = (month+1).ToString();
	this.ltl_month2.Text = (month+2).ToString();
	this.ltl_month3.Text = (month+3).ToString();
	this.ltl_month4.Text = (month+4).ToString();
	this.ltl_month5.Text = (month+5).ToString();
	this.ltl_month6.Text = (month+6).ToString();

	month = Convert.ToInt32(Select_Month.SelectedValue);
	if (year == 0 && month == 0)
	{
	this.ltl_roadkill_year_site_months.Text = "全部";
	this.ltl_roadkill_year_animal2.Text = "全部";
	this.ltl_roadkill_month_animal2.Text = "全部";
	}
	else if (month == 0)
	{
	this.ltl_roadkill_year_site_months.Text = year.ToString();
	this.ltl_roadkill_year_animal2.Text = year+"年";
	this.ltl_roadkill_month_animal2.Text = year+"年";
	}
	else if (year == 0)
	{
	this.ltl_roadkill_year_site_months.Text = "全部";
	this.ltl_roadkill_year_animal2.Text = "全部";
	this.ltl_roadkill_month_animal2.Text = month+"月";
	}
	else
	{
	this.ltl_roadkill_year_site_months.Text = year.ToString();
	this.ltl_roadkill_year_animal2.Text = year+"年";
	this.ltl_roadkill_month_animal2.Text = year+"年"+month+"月";
	}

            DataTable roadkill_year_a2 = new DataTable();
	    apt = new SqlDataAdapter("select animal2, count(animal2) as count from v_roadkill_new where (@year = 0 or yy = @year) and animal2 in ('貓狗','大鳥','中小鳥','果子狸') group by animal2 union all select '其他' as animal2, count(*) as count from v_roadkill_new where (@year = 0 or yy = @year) and animal2 not in ('貓狗','大鳥','中小鳥','果子狸') order by count desc", conn);
	    apt.SelectCommand.Parameters.AddWithValue("@year", year);
	    apt.Fill(roadkill_year_a2);
	    rptRoadkill_year_animal2.DataSource = roadkill_year_a2;
	    rptRoadkill_year_animal2.DataBind();

            DataTable roadkill_month_a2 = new DataTable();
	    apt = new SqlDataAdapter("select animal2, count(animal2) as count from v_roadkill_new where (@year = 0 or yy = @year) and (@month = 0 or mm = @month) and animal2 in ('貓狗','大鳥','中小鳥','果子狸') group by animal2 union all select '其他' as animal2, count(*) as count from v_roadkill_new where (@year = 0 or yy = @year) and (@month = 0 or mm = @month) and animal2 not in ('貓狗','大鳥','中小鳥','果子狸') order by count desc", conn);
	    apt.SelectCommand.Parameters.AddWithValue("@year", year);
	    apt.SelectCommand.Parameters.AddWithValue("@month", month);
	    apt.Fill(roadkill_month_a2);
	    rptRoadkill_month_animal2.DataSource = roadkill_month_a2;
	    rptRoadkill_month_animal2.DataBind();

	if (year == 0 && month == 0)
	{
	this.ltl_roadkill_year_site.Text = "全部";
	this.ltl_roadkill_month_site.Text = "全部";
	}
	else if (month == 0)
	{
	this.ltl_roadkill_year_site.Text = year+"年";
	this.ltl_roadkill_month_site.Text = year+"年";
	}
	else if (year == 0)
	{
	this.ltl_roadkill_year_site.Text = "全部";
	this.ltl_roadkill_month_site.Text = month+"月";	
	}
	else
	{
	this.ltl_roadkill_year_site.Text = year+"年";
	this.ltl_roadkill_month_site.Text = year+"年"+month+"月";
	}

            DataTable roadkill_year_site = new DataTable();
	    apt = new SqlDataAdapter("select site_ch, count(site_ch) as count from v_roadkill_new where (@year = 0 or yy = @year) and site_ch <> '湖口' group by site_ch order by count desc", conn);
	    apt.SelectCommand.Parameters.AddWithValue("@year", year);
	    apt.Fill(roadkill_year_site);
	    rptRoadkill_year_site.DataSource = roadkill_year_site;
	    rptRoadkill_year_site.DataBind();

            DataTable roadkill_month_site = new DataTable();
	    apt = new SqlDataAdapter("select site_ch, count(site_ch) as count from v_roadkill_new where (@year = 0 or yy = @year) and (@month = 0 or mm = @month) and site_ch <> '湖口' group by site_ch order by count desc", conn);
	    apt.SelectCommand.Parameters.AddWithValue("@year", year);
	    apt.SelectCommand.Parameters.AddWithValue("@month", month);
	    apt.Fill(roadkill_month_site);
	    rptRoadkill_month_site.DataSource = roadkill_month_site;
	    rptRoadkill_month_site.DataBind();

	    conn.Close();
	}
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
	Session["roadkill_statistic"] = "1";
	Session["roadkill_qtype"] = "1";
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    SqlCommand cmd = new SqlCommand();
	    SqlCommand cmd1 = new SqlCommand();
	    SqlDataAdapter apt = new SqlDataAdapter();
	    SqlDataAdapter apt1 = new SqlDataAdapter();
	    apt.SelectCommand = cmd;
	    apt1.SelectCommand = cmd1;

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
	    string sql = "Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic, varify From v_Roadkill_new where 1 = 1";
	    string sql1 = "select  highway_id,mstart,mend, a.WGS84X as sx,a.WGS84Y as sy, b.WGS84X as ex, b.WGS84Y as ey,count from ";
	    sql1 += "(SELECT     highway_id,( mileage-100001) / 5000 * 5000 + 100000 as mstart, ( mileage-100001) / 5000 * 5000 + 105000 as mend, count(*) as count ";
	    sql1 += "from v_Roadkill_new where 1 = 1";
	    this.sds_roadkill.SelectParameters.Clear();
	    
	    if (searchtxt != "")
	    {
	        string[] searchtxts = searchtxt.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql += " and (site_ch like '%' + @kwd"+i+" + '%' or type like '%' + @kwd"+i+" + '%' or species like '%' + @kwd"+i+" + '%' or deduce_species  like '%' + @kwd"+i+" + '%')";
		    sql1 += " and (site_ch like '%' + @kwd"+i+" + '%' or type like '%' + @kwd"+i+" + '%' or species like '%' + @kwd"+i+" + '%' or deduce_species  like '%' + @kwd"+i+" + '%')";
		    this.sds_roadkill.SelectParameters.Add("kwd"+i, searchtxts[i]);
		    cmd.Parameters.AddWithValue("kwd"+i, searchtxts[i]);
		    cmd1.Parameters.AddWithValue("kwd"+i, searchtxts[i]);
		}
		qstr += searchtxt;
	    }

	    if (highwayid != "0")
	    {
		sql += " and highway_id = @highwayid";
		sql1 += " and highway_id = @highwayid";
		this.sds_roadkill.SelectParameters.Add("highwayid", highwayid);
		cmd.Parameters.AddWithValue("highwayid", highwayid);
		cmd1.Parameters.AddWithValue("highwayid", highwayid);
		if (qstr != "")
		    qstr += "、";
		qstr += "國道" + highwayid;
	    }

	    if (start_date != "")
	    {
		sql += " and date >= @start_date";
		sql1 += " and date >= @start_date";
		this.sds_roadkill.SelectParameters.Add("start_date", start_date);
		cmd.Parameters.AddWithValue("start_date", start_date);
		cmd1.Parameters.AddWithValue("start_date", start_date);
		if (qstr != "")
		    qstr += "、";
		qstr += "開始日期=" + start_date;
	    }

	    if (end_date != "")
	    {
		sql += " and date <= @end_date";
		sql1 += " and date <= @end_date";
		this.sds_roadkill.SelectParameters.Add("end_date", end_date);
		cmd.Parameters.AddWithValue("end_date", end_date);
		cmd1.Parameters.AddWithValue("end_date", end_date);
		if (qstr != "")
		    qstr += "、";
		qstr += "結束日期=" + end_date;
	    }

	    if (start != 0)
	    {
		sql += " and mileage >= @start";
		sql1 += " and mileage >= @start";
		this.sds_roadkill.SelectParameters.Add("start", start.ToString());
		cmd.Parameters.AddWithValue("start", start.ToString());
		cmd1.Parameters.AddWithValue("start", start.ToString());
		if (qstr != "")
		    qstr += "、";
		qstr += "起始里程=" + (start1 != "" ? start1 : "0") + "K+" + (start2 != "" ? start2 : "0");
	    }

	    if (end != 0)
	    {
		sql += " and mileage <= @end";
		sql1 += " and mileage <= @end";
		this.sds_roadkill.SelectParameters.Add("end", end.ToString());
		cmd.Parameters.AddWithValue("end", end.ToString());
		cmd1.Parameters.AddWithValue("end", end.ToString());
		if (qstr != "")
		    qstr += "、";
		qstr += "結束里程=" + (end1 != "" ? end1 : "0") + "K+" + (end2 != "" ? end2 : "0");
	    }

	    if (!value_Sensitive_Level.Equals("ALL"))
	    {
		sql += " and Sensitive_Level = @Sensitive_Level";
		sql1 += " and Sensitive_Level = @Sensitive_Level";
		this.sds_roadkill.SelectParameters.Add("Sensitive_Level", value_Sensitive_Level);
		cmd.Parameters.AddWithValue("Sensitive_Level", value_Sensitive_Level);
		cmd1.Parameters.AddWithValue("Sensitive_Level", value_Sensitive_Level);
		if (qstr != "")
		    qstr += "、";
		qstr += "敏感里程等級=" + value_Sensitive_Level;
	    }

            if (!value_group.Equals("ALL"))
            {
		sql += " and animal = @value_group";
		sql1 += " and animal = @value_group";
		this.sds_roadkill.SelectParameters.Add("value_group", value_group);
		cmd.Parameters.AddWithValue("value_group", value_group);
		cmd1.Parameters.AddWithValue("value_group", value_group);
		if (qstr != "")
		    qstr += "、";
		qstr += "物種類群=" + value_group;
            }

	    if (!value_species.Equals(""))
	    {
		if (value_species.Equals("coa_code"))
		{
		    sql += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
		    sql1 += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
		}
		if (value_species.Equals("is_endemic"))
		{
		    sql += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
		    sql1 += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
		}
		if (value_species.Equals("is_alien"))
		{
		    sql += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
		    sql1 += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
		}
		if (qstr != "")
		    qstr += "、";
		qstr += "關注物種=" + value_species;
	    }

	    sql1 += " group by highway_id,( mileage-100001) / 5000) as t inner join ";
	    sql1 += "(select free_id, mileage, WGS84X, WGS84Y from freeway_new where ";
	    if (highwayid != "0")
		sql1 += "free_id = @highwayid and ";
	    if (start != 0)
		sql1 += "mileage >= @start and ";
	    if (end != 0)
		sql1 += "mileage <= @end and ";
	    sql1 += "direction in ('S','E')) as a";
	    sql1 += " on t.mstart = a.mileage and highway_id = a.free_id inner join (select free_id, mileage, WGS84X, WGS84Y from freeway_new where ";
	    if (highwayid != "0")
		sql1 += "free_id = @highwayid and ";
	    if (start != 0)
		sql1 += "mileage >= @start and ";
	    if (end != 0)
		sql1 += "mileage <= @end and ";
	    sql1 += "direction in ('S','E')) as b";
	    sql1 += " on t.mend = b.mileage and highway_id = b.free_id";

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    this.sds_roadkill.SelectCommand = sql;
	    apt.SelectCommand.CommandText = sql;
	    apt.SelectCommand.Connection = conn;
	    DataTable dt = new DataTable();
	    apt.Fill(dt);
	    apt.Dispose();
	    string rks = "";
	    foreach(DataRow dr in dt.Rows)
	    {
		if (rks != "")
		    rks += ";";
		rks += dr["id"].ToString() + "," + dr["y"].ToString() + "," + dr["x"].ToString();
	    }
	    Session["roadkill_clustermap"] = rks;
	    string map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
	    map_url = map_url.Replace("roadkill_statistic.aspx", "roadkill_clustermap.aspx");
	    this.roadkill_clustermap.Src = map_url;

	    apt1.SelectCommand.CommandText = sql1;
	    apt1.SelectCommand.Connection = conn;
	    DataTable dt1 = new DataTable();
	    apt1.Fill(dt1);
	    apt1.Dispose();
	    string rkl = "";
	    foreach(DataRow dr in dt1.Rows)
	    {
		if (rkl != "")
		    rkl += ";";
		rkl += dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString() + "," + dr[6].ToString() + "," + dr[7].ToString();
	    }
	    Session["roadkill_heatmap"] = rkl;
	    map_url = map_url.Replace("roadkill_clustermap.aspx", "roadkill_heatmap.aspx");
	    this.roadkill_heatmap.Src = map_url;

	    this.gv_roadkill.DataBind();
	    this.gv_roadkill.UseAccessibleHeader = true;
	    if (this.gv_roadkill.HeaderRow != null)
	    this.gv_roadkill.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_roadkill.Visible = true;
	    this.dmap.Visible = true;
	    DataView view = (DataView)this.sds_roadkill.Select(DataSourceSelectArguments.Empty);
	    dt_excel = view.ToTable();
	    TotalCounts.Text = "總筆數：" + view.Count;
	    view.Dispose();

	    conn.Close();
	}
    }

    protected void btnsearch2_Click(object sender, EventArgs e)
    {
	Session["roadkill_statistic"] = "1";
	Session["roadkill_qtype"] = "2";
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string searchtxt2 = this.searchtxt2a.Text;
	    string start_date2 = this.start_date2a.Text;
	    string end_date2 = this.end_date2a.Text;
	    string value_class1 = this.DropDownList4a.SelectedValue;
	    string value_class2 = this.DropDownList5a.SelectedValue;
	    string value_Sensitive_Level = this.DropDownList32a.SelectedValue;
	    string value_group = this.DropDownList12a.SelectedValue;
	    string value_species = this.DropDownList22a.SelectedValue;

	    string qstr = "";
	    string sql = "Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic, varify From v_Roadkill_new where 1 = 1";
	    this.sds_roadkill.SelectParameters.Clear();
	    
	    if (searchtxt2 != "")
	    {
	        string[] searchtxts = searchtxt2.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql += " and (site_ch like '%' + @kwd"+i+" + '%' or type like '%' + @kwd"+i+" + '%' or species like '%' + @kwd"+i+" + '%' or deduce_species  like '%' + @kwd"+i+" + '%')";
		    this.sds_roadkill.SelectParameters.Add("kwd"+i, searchtxts[i]);
		}
		qstr += searchtxt2;
	    }

	    if (start_date2 != "")
	    {
		sql += " and date >= @start_date2";
		this.sds_roadkill.SelectParameters.Add("start_date2", start_date2);
		if (qstr != "")
		    qstr += "、";
		qstr += "開始日期=" + start_date2;
	    }

	    if (end_date2 != "")
	    {
		sql += " and date <= @end_date2";
		this.sds_roadkill.SelectParameters.Add("end_date2", end_date2);
		if (qstr != "")
		    qstr += "、";
		qstr += "結束日期=" + end_date2;
	    }

	    if (!value_class2.Equals("ALL"))
	    {
		sql += " and site_ch = @value_class2";
		this.sds_roadkill.SelectParameters.Add("value_class2", value_class2.Replace("工務段",""));
		if (qstr != "")
		    qstr += "、";
		qstr += value_class2;
	    }

	    else if (!value_class1.Equals("ALL"))
	    {
            sql += " and x <= (select max_x from Engineering_road_scope where start_end = @value_class1) ";
            sql += " and x >= (select min_x from Engineering_road_scope where start_end = @value_class1) ";
            sql += " and y <= (select max_y from Engineering_road_scope where start_end = @value_class1) ";
            sql += " and y >= (select min_y from Engineering_road_scope where start_end = @value_class1) ";
		this.sds_roadkill.SelectParameters.Add("value_class1", value_class1);
		if (qstr != "")
		    qstr += "、";
		qstr +=value_class1;
	    }

	    if (!value_Sensitive_Level.Equals("ALL"))
	    {
		sql += " and Sensitive_Level = @Sensitive_Level";
		this.sds_roadkill.SelectParameters.Add("Sensitive_Level", value_Sensitive_Level);
		if (qstr != "")
		    qstr += "、";
		qstr += "敏感里程等級=" + value_Sensitive_Level;
	    }

            if (!value_group.Equals("ALL"))
            {
		sql += " and animal = @value_group";
		this.sds_roadkill.SelectParameters.Add("value_group", value_group);
		if (qstr != "")
		    qstr += "、";
		qstr += "物種類群=" + value_group;
            }

	    if (!value_species.Equals(""))
	    {
		if (value_species.Equals("coa_code"))
		    sql += " and species in (select common_name_c from Endanger_species where coa_code<> '') ";
		if (value_species.Equals("is_endemic"))
		    sql += " and species in (select common_name_c from Endanger_species where is_endemic <> '0') ";
		if (value_species.Equals("is_alien"))
		    sql += " and species in (select common_name_c from Endanger_species where is_alien <> '0') ";
		if (qstr != "")
		    qstr += "、";
		qstr += "關注物種=" + value_species;
	    }

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    this.sds_roadkill.SelectCommand = sql;
	    this.dmap.Visible = false;

	    this.gv_roadkill.DataBind();
	    this.gv_roadkill.UseAccessibleHeader = true;
	    if (this.gv_roadkill.HeaderRow != null)
	    this.gv_roadkill.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_roadkill.Visible = true;
	    DataView view = (DataView)this.sds_roadkill.Select(DataSourceSelectArguments.Empty);
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
            u_sheet_Detail.SetColumnWidth(0, 10 * 256);
            u_sheet_Detail.SetColumnWidth(1, 10 * 256);
            u_sheet_Detail.SetColumnWidth(2, 10 * 256);
            u_sheet_Detail.SetColumnWidth(3, 10 * 256);
            u_sheet_Detail.SetColumnWidth(4, 15 * 256);
            u_sheet_Detail.SetColumnWidth(5, 10 * 256);
            u_sheet_Detail.SetColumnWidth(6, 20 * 256);
            u_sheet_Detail.SetColumnWidth(7, 20 * 256);
            u_sheet_Detail.SetColumnWidth(8, 10 * 256);

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

                }
            }

            workbook.Write(ms);

            string Excel_ShortTime = "roadkill_search_export.xls";
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

    public bool roadkill_editable(string id)
    {
	bool editable = false;
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = "Select id From v_Roadkill_new Where id = @id";
	    if (Session["role"].ToString() != "1")
		sql += " and uid = @uid";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("id", id);
	    cmd.Parameters.AddWithValue("uid", Session["uid"]);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
		editable = true;

	    conn.Close();
	}
	return editable;
    }

    protected void flexCheck_Changed(object sender, EventArgs e)
    {
	Session["roadkill_statistic"] = "1";
	if (flexCheck.Checked)
	{
	    this.gv_roadkill.Columns[12].Visible = true;
	    this.gv_roadkill.Columns[13].Visible = true;
	}
	else
	{
	    this.gv_roadkill.Columns[12].Visible = false;
	    this.gv_roadkill.Columns[13].Visible = false;
	}
	if (Session["roadkill_qtype"] == null || Session["roadkill_qtype"].ToString() == "1")
	    btnsearch_Click(sender, e);
	else if (Session["roadkill_qtype"] != null && Session["roadkill_qtype"].ToString() == "2")
	    btnsearch2_Click(sender, e);
    }

    protected void gv_roadkill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
	if (e.CommandName == "Delete")
	{
	    string id = e.CommandArgument.ToString();
	    string sql;
	    if (id[0] == 'N')
		sql = "delete from Roadkill where id = @id";
	    else
		sql = "delete from Roadkill_new where id = @id";

	    this.sds_roadkill.DeleteCommand = sql;
	    this.sds_roadkill.DeleteParameters.Add("id", id.Substring(1));
	}
    }


}
