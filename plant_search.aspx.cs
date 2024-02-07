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

public partial class plant_search : System.Web.UI.Page
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
	else if (Session["plant_qtype"] != null && Session["plant_qtype"].ToString() == "1")
	    btnsearch_Click(sender, e);
	else if (Session["plant_qtype"] != null && Session["plant_qtype"].ToString() == "2")
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
	Session["plant_qtype"] = "1";
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
        string value_florescenceS = florescenceS.Text;
        string value_florescenceE = florescenceE.Text;
        string value_flowercolor = flowercolor.SelectedValue;
        string value_leafperiodS = leafperiodS.Text;
        string value_leafperiodE = leafperiodE.Text;
        string value_leafcolor = leafcolor.SelectedValue;

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
	    string sql = "Select plantid,pointx,pointy,Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo,plant_loca,date,plant_number From Plants Where 1=1";
	    string sql2 = "";
	    this.sds_plant.SelectParameters.Clear();
	    
	    if (searchtxt != "")
	    {
	        string[] searchtxts = searchtxt.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql2 += " and (segment like '%' + @kwd"+i+" + '%' or plant_name like '%' + @kwd"+i+" + '%' or plant_loca like '%' + @kwd"+i+" + '%')";
		    this.sds_plant.SelectParameters.Add("kwd"+i, searchtxts[i]);
		    cmd.Parameters.AddWithValue("kwd"+i, searchtxts[i]);
		}
		qstr += searchtxt;
	    }
	    if (highwayid != "")
	    {
		sql2 += " and highway_name = @highwayid";
		this.sds_plant.SelectParameters.Add("highwayid", highwayid);
		cmd.Parameters.AddWithValue("highwayid", highwayid);
		if (qstr != "")
		    qstr += "、";
		qstr += "國道" + highwayid;
	    }
	    if (start_date != "")
	    {
		sql2 += " and date >= @start_date";
		this.sds_plant.SelectParameters.Add("start_date", start_date.Replace("-",""));
		cmd.Parameters.AddWithValue("start_date", start_date.Replace("-",""));
		if (qstr != "")
		    qstr += "、";
		qstr += "開始日期=" + start_date;
	    }
	    if (end_date != "")
	    {
		sql2 += " and date <= @end_date";
		this.sds_plant.SelectParameters.Add("end_date", end_date.Replace("-",""));
		cmd.Parameters.AddWithValue("end_date", end_date.Replace("-",""));
		if (qstr != "")
		    qstr += "、";
		qstr += "結束日期=" + end_date;
	    }
	    if (start != 0)
	    {
		sql2 += " and (start1*1000)+start2 >= @start";
		this.sds_plant.SelectParameters.Add("start", start.ToString());
		cmd.Parameters.AddWithValue("start", start.ToString());
		if (qstr != "")
		    qstr += "、";
		qstr += "起始里程=" + (start1 != "" ? start1 : "0") + "K+" + (start2 != "" ? start2 : "0");
	    }
	    if (end != 0)
	    {
		sql2 += " and (end1*1000)+end2 <= @end";
		this.sds_plant.SelectParameters.Add("end", end.ToString());
		cmd.Parameters.AddWithValue("end", end.ToString());
		if (qstr != "")
		    qstr += "、";
		qstr += "結束里程=" + (end1 != "" ? end1 : "0") + "K+" + (end2 != "" ? end2 : "0");
	    }

        if (value_florescenceS != "" & value_florescenceE != "")
        {
            sql2 += " and (','+florescence like '%,' + @value_florescenceS + ',%' or ','+florescence like '%,' + @value_florescenceE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_florescenceS", value_florescenceS);
	    cmd.Parameters.AddWithValue("value_florescenceS", value_florescenceS);
	    this.sds_plant.SelectParameters.Add("value_florescenceE", value_florescenceE);
	    cmd.Parameters.AddWithValue("value_florescenceE", value_florescenceE);
		if (qstr != "")
		    qstr += "、";
		qstr += "花期="+value_florescenceS+"~"+value_florescenceE;
        }
        else if (value_florescenceS != "" & value_florescenceE == "")
        {
            sql2 += " and (','+florescence like '%,' + @value_florescenceS + ',%')";
	    this.sds_plant.SelectParameters.Add("value_florescenceS", value_florescenceS);
	    cmd.Parameters.AddWithValue("value_florescenceS", value_florescenceS);
		if (qstr != "")
		    qstr += "、";
		qstr += "花期="+value_florescenceS+"~";
        }
        else if (value_florescenceE != "" & value_florescenceS == "")
        {
            sql2 += " and (','+florescence like '%,' + @value_florescenceE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_florescenceE", value_florescenceE);
	    cmd.Parameters.AddWithValue("value_florescenceE", value_florescenceE);
		if (qstr != "")
		    qstr += "、";
		qstr += "花期=~"+value_florescenceE;
        }

        if (value_flowercolor != "")
        {
            sql2 += " and flowercolor = @value_flowercolor";
	    this.sds_plant.SelectParameters.Add("value_flowercolor", value_flowercolor);
	    cmd.Parameters.AddWithValue("value_flowercolor", value_flowercolor);
		if (qstr != "")
		    qstr += "、";
		qstr += "花色="+value_flowercolor;
        }

        if (value_leafperiodS != "" & value_leafperiodE != "")
        {
            sql2 += " and (','+LeafPeriod like '%,' + @value_leafperiodS + ',%' or ','+LeafPeriod like '%,' + @value_leafperiodE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_leafperiodS",value_leafperiodS);
	    cmd.Parameters.AddWithValue("value_leafperiodS",value_leafperiodS);
	    this.sds_plant.SelectParameters.Add("value_leafperiodE",value_leafperiodE);
	    cmd.Parameters.AddWithValue("value_leafperiodE",value_leafperiodE);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色轉變期="+value_leafperiodS+"~"+value_leafperiodE;
        }
        else if (value_leafperiodS != "" & value_leafperiodE == "")
        {
            sql2 += " and (','+LeafPeriod like '%,' + @value_leafperiodS + ',%')";
	    this.sds_plant.SelectParameters.Add("value_leafperiodS",value_leafperiodS);
	    cmd.Parameters.AddWithValue("value_leafperiodS",value_leafperiodS);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色轉變期="+value_leafperiodS+"~";
        }
        else if (value_leafperiodE != "" & value_leafperiodS == "")
        {
            sql2 += " and (','+LeafPeriod like '%,' + @value_leafperiodE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_leafperiodE",value_leafperiodE);
	    cmd.Parameters.AddWithValue("value_leafperiodE",value_leafperiodE);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色轉變期=~"+value_leafperiodE;
        }

        if (value_leafcolor != "")
        {
            sql2 += " and leafcolor = @value_leafcolor";
	    this.sds_plant.SelectParameters.Add("value_leafcolor", value_leafcolor);
	    cmd.Parameters.AddWithValue("value_leafcolor", value_leafcolor);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色="+value_leafcolor;
        }

	    sql += sql2;
	    string sql_xy = "select plantid,Flowercolor,highway_name,plant_name, pointx as x,pointy as y from plants where 1 = 1 " + sql2;
	    sql_xy += " and pointx is not null and pointy is not null order by x,y ";

	    apt.SelectCommand.CommandText = sql_xy;
	    apt.SelectCommand.Connection = conn;
	    DataTable dt = new DataTable();
	    apt.Fill(dt);
	    apt.Dispose();
	    string pm = "";
	    string x_ori = "";
	    string y_ori = "";
	    int k = 0;
	    int j = 0;

	    foreach(DataRow dr in dt.Rows)
	    {
		if (pm != "")
		    pm += ";";

                string highway_direction = "";
                string highway_id = "";
                highway_id = dr["highway_name"].ToString();
                if (highway_id.Equals("2") || highway_id.Equals("4") || highway_id.Equals("6") || highway_id.Equals("8") || highway_id.Equals("10") || highway_id.Equals("3甲"))
                {
                    highway_direction = "2";
                }
                else
                {
                    highway_direction = "1";
                }

                if (x_ori.Equals(dr["x"].ToString()) && y_ori.Equals(dr["y"].ToString()))
                {
                    j++;
                    //橫向國道加緯度
                    if (highway_direction.Equals("2"))
                    {
                        pm += dr["y"].ToString() + ",";
                        pm += (Convert.ToDouble(dr["x"].ToString()) + (0.000005 * j)).ToString() + ",";
                    }
                    //直向國道加經度
                    else
                    {
                        pm += (Convert.ToDouble(dr["y"].ToString()) + (0.00001 * j)).ToString() + ",";
                        pm += dr["x"].ToString() + ",";
                    }
                }
                else
                {
                    j = 0;
                    pm += dr["y"].ToString() + ",";
                    pm += dr["x"].ToString() + ",";
                    x_ori = dr["x"].ToString();
                    y_ori = dr["y"].ToString();
                }
		pm += dr["PlantId"].ToString() + ",";
	        pm += dr["Plant_name"].ToString() + ",";
		pm += dr["Flowercolor"].ToString();
		if (k == 0)
		{
		    x_ori = dr["x"].ToString();
		    y_ori = dr["y"].ToString();
		}
		k += 1;
	    }
	    Session["plant_map"] = pm;
	    this.plant_map.Src = "plant_map.aspx";
	    this.dmap.Visible = true;

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    this.sds_plant.SelectCommand = sql;
	    this.gv_plant.DataBind();
	    this.gv_plant.UseAccessibleHeader = true;
	    if (this.gv_plant.HeaderRow != null)
	    this.gv_plant.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_plant.Visible = true;
	    DataView view = (DataView)this.sds_plant.Select(DataSourceSelectArguments.Empty);
	    dt_excel = view.ToTable();
	    TotalCounts.Text = "總筆數：" + view.Count;
	    view.Dispose();

	    conn.Close();
	}
    }

    protected void btnsearch2_Click(object sender, EventArgs e)
    {
	Session["plant_qtype"] = "2";
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
        string value_florescenceS = florescenceS2.Text;
        string value_florescenceE = florescenceE2.Text;
        string value_flowercolor = flowercolor2.SelectedValue;
        string value_leafperiodS = leafperiodS2.Text;
        string value_leafperiodE = leafperiodE2.Text;
        string value_leafcolor = leafcolor2.SelectedValue;

	    string qstr = "";
	    string sql = "Select plantid,pointx,pointy,Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo,plant_loca,date,plant_number From Plants Where 1=1";
	    string sql2 = "";
	    this.sds_plant.SelectParameters.Clear();
	    if (searchtxt2 != "")
	    {
	        string[] searchtxts = searchtxt2.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql2 += " and (segment like '%' + @kwd"+i+" + '%' or plant_name like '%' + @kwd"+i+" + '%' or plant_loca like '%' + @kwd"+i+" + '%')";
		    this.sds_plant.SelectParameters.Add("kwd"+i, searchtxts[i]);
		    cmd.Parameters.AddWithValue("kwd"+i, searchtxts[i]);
		}
		qstr += searchtxt2;
	    }

	    if (start_date2 != "")
	    {
		sql2 += " and date >= @start_date2";
		this.sds_plant.SelectParameters.Add("start_date2", start_date2.Replace("-",""));
		cmd.Parameters.AddWithValue("start_date2", start_date2.Replace("-",""));
		if (qstr != "")
		    qstr += "、";
		qstr += "開始日期=" + start_date2;
	    }
	    if (end_date2 != "")
	    {
		sql2 += " and date <= @end_date2";
		this.sds_plant.SelectParameters.Add("end_date2", end_date2.Replace("-",""));
		cmd.Parameters.AddWithValue("end_date2", end_date2.Replace("-",""));
		if (qstr != "")
		    qstr += "、";
		qstr += "結束日期=" + end_date2;
	    }

        if (!value_class2.Equals("ALL"))
        {
	    string vc2 = value_class2.Substring(0,2);
	    sql2 += " and (Segment = @vc2 or ((Segment is NULL or Segment = '')";
	    this.sds_plant.SelectParameters.Add("vc2", vc2);
	    cmd.Parameters.AddWithValue("vc2", vc2);
            sql2 += " and PointX <= (select max_x from Engineering_road_scope where start_end = @value_class2) ";
            sql2 += " and PointX >= (select min_x from Engineering_road_scope where start_end = @value_class2) ";
            sql2 += " and PointY <= (select max_y from Engineering_road_scope where start_end = @value_class2) ";
            sql2 += " and PointY >= (select min_y from Engineering_road_scope where start_end = @value_class2))) ";
	    this.sds_plant.SelectParameters.Add("value_class2", value_class2);
	    cmd.Parameters.AddWithValue("value_class2", value_class2);
		if (qstr != "")
		    qstr += "、";
		qstr += value_class2;
        }
        else if (!value_class1.Equals("ALL"))
        {
	    sql2 += " and (Office = @value_class1 or ((Office is NULL or Office = '')";
            sql2 += " and PointX <= (select max_x from Engineering_road_scope where start_end = @value_class1) ";
            sql2 += " and PointX >= (select min_x from Engineering_road_scope where start_end = @value_class1) ";
            sql2 += " and PointY <= (select max_y from Engineering_road_scope where start_end = @value_class1) ";
            sql2 += " and PointY >= (select min_y from Engineering_road_scope where start_end = @value_class1))) ";
	    this.sds_plant.SelectParameters.Add("value_class1", value_class1);
	    cmd.Parameters.AddWithValue("value_class1", value_class1);
		if (qstr != "")
		    qstr += "、";
		qstr +=value_class1;
        }

        if (value_florescenceS != "" & value_florescenceE != "")
        {
            sql2 += " and (','+florescence like '%,' + @value_florescenceS + ',%' or ','+florescence like '%,' + @value_florescenceE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_florescenceS", value_florescenceS);
	    cmd.Parameters.AddWithValue("value_florescenceS", value_florescenceS);
	    this.sds_plant.SelectParameters.Add("value_florescenceE", value_florescenceE);
	    cmd.Parameters.AddWithValue("value_florescenceE", value_florescenceE);
		if (qstr != "")
		    qstr += "、";
		qstr += "花期="+value_florescenceS+"~"+value_florescenceE;
        }
        else if (value_florescenceS != "" & value_florescenceE == "")
        {
            sql2 += " and (','+florescence like '%,' + @value_florescenceS + ',%')";
	    this.sds_plant.SelectParameters.Add("value_florescenceS", value_florescenceS);
	    cmd.Parameters.AddWithValue("value_florescenceS", value_florescenceS);
		if (qstr != "")
		    qstr += "、";
		qstr += "花期="+value_florescenceS+"~";
        }
        else if (value_florescenceE != "" & value_florescenceS == "")
        {
            sql2 += " and (','+florescence like '%,' + @value_florescenceE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_florescenceE", value_florescenceE);
	    cmd.Parameters.AddWithValue("value_florescenceE", value_florescenceE);
		if (qstr != "")
		    qstr += "、";
		qstr += "花期=~"+value_florescenceE;
        }

        if (value_flowercolor != "")
        {
            sql2 += " and flowercolor = @value_flowercolor";
	    this.sds_plant.SelectParameters.Add("value_flowercolor", value_flowercolor);
	    cmd.Parameters.AddWithValue("value_flowercolor", value_flowercolor);
		if (qstr != "")
		    qstr += "、";
		qstr += "花色="+value_flowercolor;
        }

        if (value_leafperiodS != "" & value_leafperiodE != "")
        {
            sql2 += " and (','+LeafPeriod like '%,' + @value_leafperiodS + ',%' or ','+LeafPeriod like '%,' + @value_leafperiodE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_leafperiodS",value_leafperiodS);
	    cmd.Parameters.AddWithValue("value_leafperiodS",value_leafperiodS);
	    this.sds_plant.SelectParameters.Add("value_leafperiodE",value_leafperiodE);
	    cmd.Parameters.AddWithValue("value_leafperiodE",value_leafperiodE);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色轉變期="+value_leafperiodS+"~"+value_leafperiodE;
        }
        else if (value_leafperiodS != "" & value_leafperiodE == "")
        {
            sql2 += " and (','+LeafPeriod like '%,' + @value_leafperiodS + ',%')";
	    this.sds_plant.SelectParameters.Add("value_leafperiodS",value_leafperiodS);
	    cmd.Parameters.AddWithValue("value_leafperiodS",value_leafperiodS);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色轉變期="+value_leafperiodS+"~";
        }
        else if (value_leafperiodE != "" & value_leafperiodS == "")
        {
            sql2 += " and (','+LeafPeriod like '%,' + @value_leafperiodE + ',%')";
	    this.sds_plant.SelectParameters.Add("value_leafperiodE",value_leafperiodE);
	    cmd.Parameters.AddWithValue("value_leafperiodE",value_leafperiodE);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色轉變期=~"+value_leafperiodE;
        }

        if (value_leafcolor != "")
        {
            sql2 += " and leafcolor = @value_leafcolor";
	    this.sds_plant.SelectParameters.Add("value_leafcolor", value_leafcolor);
	    cmd.Parameters.AddWithValue("value_leafcolor", value_leafcolor);
		if (qstr != "")
		    qstr += "、";
		qstr += "葉色="+value_leafcolor;
        }

	    sql += sql2;
	    string sql_xy = "select plantid,Flowercolor,highway_name,plant_name, pointx as x,pointy as y from plants where 1 = 1 " + sql2;
	    sql_xy += " and pointx is not null and pointy is not null order by x,y ";

	    apt.SelectCommand.CommandText = sql_xy;
	    apt.SelectCommand.Connection = conn;
	    DataTable dt = new DataTable();
	    apt.Fill(dt);
	    apt.Dispose();
	    string pm = "";
	    string x_ori = "";
	    string y_ori = "";
	    int k = 0;
	    int j = 0;

	    foreach(DataRow dr in dt.Rows)
	    {
		if (pm != "")
		    pm += ";";

                string highway_direction = "";
                string highway_id = "";
                highway_id = dr["highway_name"].ToString();
                if (highway_id.Equals("2") || highway_id.Equals("4") || highway_id.Equals("6") || highway_id.Equals("8") || highway_id.Equals("10") || highway_id.Equals("3甲"))
                {
                    highway_direction = "2";
                }
                else
                {
                    highway_direction = "1";
                }

                if (x_ori.Equals(dr["x"].ToString()) && y_ori.Equals(dr["y"].ToString()))
                {
                    j++;
                    //橫向國道加緯度
                    if (highway_direction.Equals("2"))
                    {
                        pm += dr["y"].ToString() + ",";
                        pm += (Convert.ToDouble(dr["x"].ToString()) + (0.000005 * j)).ToString() + ",";
                    }
                    //直向國道加經度
                    else
                    {
                        pm += (Convert.ToDouble(dr["y"].ToString()) + (0.00001 * j)).ToString() + ",";
                        pm += dr["x"].ToString() + ",";
                    }
                }
                else
                {
                    j = 0;
                    pm += dr["y"].ToString() + ",";
                    pm += dr["x"].ToString() + ",";
                    x_ori = dr["x"].ToString();
                    y_ori = dr["y"].ToString();
                }
		pm += dr["PlantId"].ToString() + ",";
	        pm += dr["Plant_name"].ToString() + ",";
		pm += dr["Flowercolor"].ToString();
		if (k == 0)
		{
		    x_ori = dr["x"].ToString();
		    y_ori = dr["y"].ToString();
		}
		k += 1;
	    }
	    Session["plant_map"] = pm;
	    this.plant_map.Src = "plant_map.aspx";
	    this.dmap.Visible = true;

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    this.sds_plant.SelectCommand = sql;
	    this.gv_plant.DataBind();
	    this.gv_plant.UseAccessibleHeader = true;
	    if (this.gv_plant.HeaderRow != null)
	    this.gv_plant.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_plant.Visible = true;
	    DataView view = (DataView)this.sds_plant.Select(DataSourceSelectArguments.Empty);
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
            u_sheet_Detail.SetColumnWidth(1, 10 * 256);
            u_sheet_Detail.SetColumnWidth(2, 10 * 256);
            u_sheet_Detail.SetColumnWidth(3, 10 * 256);
            u_sheet_Detail.SetColumnWidth(4, 20 * 256);
            u_sheet_Detail.SetColumnWidth(5, 20 * 256);
            u_sheet_Detail.SetColumnWidth(6, 20 * 256);
            u_sheet_Detail.SetColumnWidth(7, 10 * 256);


            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("物種名稱");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("工務段");
            u_sheet_Detail.GetRow(0).CreateCell(2).SetCellValue("國道編號");
            u_sheet_Detail.GetRow(0).CreateCell(3).SetCellValue("方向");
            u_sheet_Detail.GetRow(0).CreateCell(4).SetCellValue("里程");
            u_sheet_Detail.GetRow(0).CreateCell(5).SetCellValue("位置");
            u_sheet_Detail.GetRow(0).CreateCell(6).SetCellValue("日期");
            u_sheet_Detail.GetRow(0).CreateCell(7).SetCellValue("數量");


            //設定CellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(2).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(3).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(4).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(5).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(6).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(7).CellStyle = style_wrap_center;

            int k = 0;

            // 比較表 資料
            foreach (DataRow pDR_FSData in dt_excel.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;

                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["plant_name"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["segment"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(2).SetCellValue(pDR_FSData["highway_name"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(3).SetCellValue(pDR_FSData["direction"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(4).SetCellValue(pDR_FSData["range"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(5).SetCellValue(pDR_FSData["plant_loca"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(6).SetCellValue(pDR_FSData["date"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(7).SetCellValue(pDR_FSData["plant_number"].ToString());

                    //設定CellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(2).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(3).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(4).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(5).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(6).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(7).CellStyle = style_wrap_left;

                }
            }

            workbook.Write(ms);

            //將EXCEL檔案匯出到系統資料儲存目錄
            string Excel_ShortTime = "plant_search_export.xls";
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

    public bool plant_editable(string id)
    {
	bool editable = false;
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = "Select plantid From plants Where plantid = @id";
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
	if (flexCheck.Checked)
	{
	    this.gv_plant.Columns[10].Visible = true;
	    this.gv_plant.Columns[11].Visible = true;
	}
	else
	{
	    this.gv_plant.Columns[10].Visible = false;
	    this.gv_plant.Columns[11].Visible = false;
	}
	if (Session["plant_qtype"] == null || Session["plant_qtype"].ToString() == "1")
	    btnsearch_Click(sender, e);
	else if (Session["plant_qtype"] != null && Session["plant_qtype"].ToString() == "2")
	    btnsearch2_Click(sender, e);
    }

    protected void gv_plant_RowCommand(object sender, GridViewCommandEventArgs e)
    {
	if (e.CommandName == "Delete")
	{
	    string plantid = e.CommandArgument.ToString();
	    string sql = "DELETE FROM [Plants] WHERE [PlantId] = @PlantId";

	    this.sds_plant.DeleteCommand = sql;
	    this.sds_plant.DeleteParameters.Add("PlantId", plantid);
	}
    }
}
