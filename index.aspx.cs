using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class _index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	if (IsPostBack) return;

	SetContent();
    }

    private void SetContent()
    {
	string month = DateTime.Now.Month.ToString();
	this.ltl_month1.Text = month;
	this.ltl_month2.Text = month;
	this.ltl_month3.Text = month;
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string date = (DateTime.Now.Year-1).ToString() + "-01-01";
	    using (SqlCommand cmd = new SqlCommand("select count(*) from Table_1 where date1 >= '" + date + "'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    this.ltl_occurence.Text = sdr[0].ToString();
		sdr.Close();
	    }

	    date = (DateTime.Now.Year).ToString() + "-01-01";
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_Roadkill_new where date >= '" + date + "'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    this.ltl_roadkill.Text = sdr[0].ToString();
		sdr.Close();
	    }

	    date = (DateTime.Now.Year).ToString() + "-01-01";
	    using (SqlCommand cmd = new SqlCommand("select count(*) from Plants where date >= '" + date + "'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    this.ltl_plants.Text = sdr[0].ToString();
		sdr.Close();
	    }

	    int[] roadkill_num = new int[6];
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_roadkill_new where animal2 = '貓狗'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_num[1] = Convert.ToInt32(sdr[0]);
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_roadkill_new where animal2 = '大鳥'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_num[2] = Convert.ToInt32(sdr[0]);
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_roadkill_new where animal2 = '中小鳥'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_num[3] = Convert.ToInt32(sdr[0]);
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_roadkill_new where animal2 = '果子狸'", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_num[4] = Convert.ToInt32(sdr[0]);
		sdr.Close();
	    }
	    using (SqlCommand cmd = new SqlCommand("select count(*) from v_roadkill_new where animal2 NOT IN ('貓狗','大鳥','中小鳥','果子狸')", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		if (sdr.Read())
		    roadkill_num[5] = Convert.ToInt32(sdr[0]);
		sdr.Close();
	    }
	    this.ltl_roadkill_total.Text = (roadkill_num[1] + roadkill_num[2] + roadkill_num[3] + roadkill_num[4] + roadkill_num[5]).ToString();
	    this.ltl_roadkill_num1.Text = roadkill_num[1].ToString();
	    this.ltl_roadkill_num2.Text = roadkill_num[2].ToString();
	    this.ltl_roadkill_num3.Text = roadkill_num[3].ToString();
	    this.ltl_roadkill_num4.Text = roadkill_num[4].ToString();
	    this.ltl_roadkill_num5.Text = roadkill_num[5].ToString();
	    this.ltl_roadkill_nums.Text = this.ltl_roadkill_num1.Text + "," + this.ltl_roadkill_num2.Text + "," + this.ltl_roadkill_num3.Text + "," + this.ltl_roadkill_num4.Text + "," + this.ltl_roadkill_num5.Text;

	    int year = DateTime.Now.Year-1;
	    string labels = "";
	    for (int i = year-1911-8; i <= year-1911; i++)
	    {
		if (labels != "")
		    labels += ",";
		labels += "\"" + i.ToString() + "年\"";
	    }
	    this.ltl_year_labels.Text = labels;

	    using (SqlCommand cmd = new SqlCommand("select yy, count(yy) from v_roadkill_new where yy >= "+(year-8).ToString()+" and yy <= "+year.ToString()+" and site_ch IN ('中壢','內湖','木柵','頭城','關西') group by yy", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		string kills = "";
		while (sdr.Read())
		{
		    if (kills != "")
			kills += ",";
		    kills += sdr[1].ToString();
		}
		sdr.Close();
		this.ltl_north_kills.Text = kills;
	    }
	    using (SqlCommand cmd = new SqlCommand("select yy, count(yy) from v_roadkill_new where yy >= "+(year-8).ToString()+" and yy <= "+year.ToString()+" and site_ch IN ('大甲','斗南','南投','苗栗') group by yy", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		string kills = "";
		while (sdr.Read())
		{
		    if (kills != "")
			kills += ",";
		    kills += sdr[1].ToString();
		}
		sdr.Close();
		this.ltl_center_kills.Text = kills;
	    }
	    using (SqlCommand cmd = new SqlCommand("select yy, count(yy) from v_roadkill_new where yy >= "+(year-8).ToString()+" and yy <= "+year.ToString()+" and site_ch IN ('白河','岡山','屏東','新營') group by yy", conn))
	    {
		SqlDataReader sdr = cmd.ExecuteReader();
		string kills = "";
		while (sdr.Read())
		{
		    if (kills != "")
			kills += ",";
		    kills += sdr[1].ToString();
		}
		sdr.Close();
		this.ltl_south_kills.Text = kills;
	    }

	    conn.Close();
	}
    }

    public static String More(String text, int length)
    {
        return text.Length > length ? text.Substring(0, length) + "..." : text;
    }
}
