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


public partial class searchall : System.Web.UI.Page
{
    public DataTable dt_excel_occurrence = new DataTable();
    public DataTable dt_excel_roadkill = new DataTable();
    public DataTable dt_excel_plant = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false) || Session["role"] == null)
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	Session["qtype"] = Request["q"] ?? "1";

	if (Session["qtype"].ToString() == "1"
	 && (Request.Form["ctl00$cb1"] == null || ((CheckBox)Page.Master.FindControl("cb1")).Checked == false)
	 && (Request.Form["ctl00$cb2"] == null || ((CheckBox)Page.Master.FindControl("cb2")).Checked == false)
	 && (Request.Form["ctl00$cb3"] == null || ((CheckBox)Page.Master.FindControl("cb3")).Checked == false))
	{
	    Response.Write("<script>alert('請至少勾選一種資料類型!');</script>");
	}
	if (Session["qtype"].ToString() == "2"
	 && (Request.Form["ctl00$cb12"] == null || ((CheckBox)Page.Master.FindControl("cb12")).Checked == false)
	 && (Request.Form["ctl00$cb22"] == null || ((CheckBox)Page.Master.FindControl("cb22")).Checked == false)
	 && (Request.Form["ctl00$cb32"] == null || ((CheckBox)Page.Master.FindControl("cb32")).Checked == false))
	{
	    Response.Write("<script>alert('請至少勾選一種資料類型!');</script>");
	}

	if (!IsPostBack)
	{
	if (Session["flexcheck1"] != null)
	    flexCheck.Checked = Session["flexcheck1"].ToString() == "1" ? true : false;
	flexCheck_Changed(sender, e);
	if (Session["flexcheck2"] != null)
	    flexCheck2.Checked = Session["flexcheck2"].ToString() == "1" ? true : false;
	flexCheck2_Changed(sender, e);
	if (Session["flexcheck3"] != null)
	    flexCheck3.Checked = Session["flexcheck3"].ToString() == "1" ? true : false;
	flexCheck3_Changed(sender, e);
	}

	if (Session["qtype"].ToString() == "2")
	    SetContent2();
	else
	    SetContent();
    }

    private void SetContent()
    {
	this.flexCheck.InputAttributes["class"] = "form-check-input";
	this.flexCheck2.InputAttributes["class"] = "form-check-input";
	this.flexCheck3.InputAttributes["class"] = "form-check-input";
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    SqlCommand cmd = new SqlCommand();
	    SqlCommand cmd1 = new SqlCommand();
	    SqlDataAdapter apt = new SqlDataAdapter();
	    SqlDataAdapter apt1 = new SqlDataAdapter();
	    apt.SelectCommand = cmd;
	    apt1.SelectCommand = cmd1;

	    DataView view;
	    //string searchtxt = Request.Form["ctl00$searchtxt"] ?? ((TextBox)Page.Master.FindControl("searchtxt")).Text;
	    string searchtxt = "";
	    if (Request.Form["ctl00$searchtxt"] != null)
		searchtxt = Request.Form["ctl00$searchtxt"];
	    else if (((TextBox)Page.Master.FindControl("searchtxt")).Text != "")
		searchtxt = ((TextBox)Page.Master.FindControl("searchtxt")).Text;
	    else if (Session["searchtxt"] != null)
		searchtxt = Session["searchtxt"].ToString();
	    Session["searchtxt"] = ((TextBox)Page.Master.FindControl("searchtxt")).Text = searchtxt;

	    //string start_date = Request.Form["ctl00$start_date"] ?? ((TextBox)Page.Master.FindControl("start_date")).Text;
	    string start_date = "";
	    if (Request.Form["ctl00$start_date"] != null)
		start_date = Request.Form["ctl00$start_date"];
	    else if (((TextBox)Page.Master.FindControl("start_date")).Text != "")
		start_date = ((TextBox)Page.Master.FindControl("start_date")).Text;
	    else if (Session["start_date"] != null)
		start_date = Session["start_date"].ToString();
	    Session["start_date"] = start_date;

	    //string end_date = Request.Form["ctl00$end_date"] ?? ((TextBox)Page.Master.FindControl("end_date")).Text;
	    string end_date = "";
	    if (Request.Form["ctl00$end_date"] != null)
		end_date = Request.Form["ctl00$end_date"];
	    else if (((TextBox)Page.Master.FindControl("end_date")).Text != "")
		end_date = ((TextBox)Page.Master.FindControl("end_date")).Text;
	    else if (Session["end_date"] != null)
		end_date = Session["end_date"].ToString();
	    Session["end_date"] = end_date;

	    //string highwayid = Request.Form["ctl00$highwayid"] ?? ((DropDownList)Page.Master.FindControl("highwayid")).SelectedValue;
	    string highwayid = "";
	    if (Request.Form["ctl00$highwayid"] != null)
		highwayid = Request.Form["ctl00$highwayid"];
	    else if (((DropDownList)Page.Master.FindControl("highwayid")).SelectedValue != "")
		highwayid = ((DropDownList)Page.Master.FindControl("highwayid")).SelectedValue;
	    else if (Session["highwayid"] != null)
		highwayid = Session["highwayid"].ToString();
	    Session["highwayid"] = highwayid;

	    //string start1 = Request.Form["ctl00$start1"] ?? ((TextBox)Page.Master.FindControl("start1")).Text;
	    //string start2 = Request.Form["ctl00$start2"] ?? ((TextBox)Page.Master.FindControl("start2")).Text;
	    string start1 = "";
	    if (Request.Form["ctl00$start1"] != null)
		start1 = Request.Form["ctl00$start1"];
	    else if (((TextBox)Page.Master.FindControl("start1")).Text != "")
		start1 = ((TextBox)Page.Master.FindControl("start1")).Text;
	    else if (Session["start1"] != null)
		start1 = Session["start1"].ToString();
	    string start2 = "";
	    if (Request.Form["ctl00$start2"] != null)
		start2 = Request.Form["ctl00$start2"];
	    else if (((TextBox)Page.Master.FindControl("start2")).Text != "")
		start2 = ((TextBox)Page.Master.FindControl("start2")).Text;
	    else if (Session["start2"] != null)
		start2 = Session["start2"].ToString();
	    Session["start1"] = start1;
	    Session["start2"] = start2;

	    //string end1 = Request.Form["ctl00$end1"] ?? ((TextBox)Page.Master.FindControl("end1")).Text;
	    //string end2 = Request.Form["ctl00$end2"] ?? ((TextBox)Page.Master.FindControl("end2")).Text;
	    string end1 = "";
	    if (Request.Form["ctl00$end1"] != null)
		end1 = Request.Form["ctl00$end1"];
	    else if (((TextBox)Page.Master.FindControl("end1")).Text != "")
		end1 = ((TextBox)Page.Master.FindControl("end1")).Text;
	    else if (Session["end1"] != null)
		end1 = Session["end1"].ToString();
	    string end2 = "";
	    if (Request.Form["ctl00$end2"] != null)
		end2 = Request.Form["ctl00$end2"];
	    else if (((TextBox)Page.Master.FindControl("end2")).Text != "")
		end2 = ((TextBox)Page.Master.FindControl("end2")).Text;
	    else if (Session["end2"] != null)
		end2 = Session["end2"].ToString();
	    Session["end1"] = end1;
	    Session["end2"] = end2;

	    string value_Sensitive_Level = "";
	    if (Request.Form["ctl00$DropDownList3"] != null)
		value_Sensitive_Level = Request.Form["ctl00$DropDownList3"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList3")).SelectedValue != "")
		value_Sensitive_Level = ((DropDownList)Page.Master.FindControl("DropDownList3")).SelectedValue;
	    else if (Session["DropDownList3"] != null)
		value_Sensitive_Level = Session["DropDownList3"].ToString();
	    string value_group = "";
	    if (Request.Form["ctl00$DropDownList1"] != null)
		value_group = Request.Form["ctl00$DropDownList1"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList1")).SelectedValue != "")
		value_group = ((DropDownList)Page.Master.FindControl("DropDownList1")).SelectedValue;
	    else if (Session["DropDownList1"] != null)
		value_group = Session["DropDownList1"].ToString();
	    string value_species = "";
	    if (Request.Form["ctl00$DropDownList2"] != null)
		value_species = Request.Form["ctl00$DropDownList2"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList2")).SelectedValue != "")
		value_species = ((DropDownList)Page.Master.FindControl("DropDownList2")).SelectedValue;
	    else if (Session["DropDownList2"] != null)
		value_species = Session["DropDownList2"].ToString();
	    Session["DropDownList3"] = value_Sensitive_Level;
	    Session["DropDownList1"] = value_group;
	    Session["DropDownList2"] = value_species;

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
	    sql1 += "(SELECT     highway_id,( mileage-100001) / 5000 * 5000 + 95000 as mstart, ( mileage-100001) / 5000 * 5000 + 100000 as mend, count(*) as count ";
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

	    if (highwayid != "")
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
		Session["end_date"] = end_date;
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
	    if (highwayid != "")
		sql1 += "free_id = @highwayid and ";
	    if (start != 0)
		sql1 += "mileage >= @start and ";
	    if (end != 0)
		sql1 += "mileage <= @end and ";
	    sql1 += "direction in ('S','E')) as a";
	    sql1 += " on t.mstart = a.mileage and highway_id = a.free_id inner join (select free_id, mileage, WGS84X, WGS84Y from freeway_new where ";
	    if (highwayid != "")
		sql1 += "free_id = @highwayid and ";
	    if (start != 0)
		sql1 += "mileage >= @start and ";
	    if (end != 0)
		sql1 += "mileage <= @end and ";
	    sql1 += "direction in ('S','E')) as b";
	    sql1 += " on t.mend = b.mileage and highway_id = b.free_id";

	    ltl_query.Text = qstr != "" ? "您查詢的條件為：" + qstr : "";
	    if (Request.Form["ctl00$cb2"] != null && ((CheckBox)Page.Master.FindControl("cb2")).Checked)
	    {
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
/*	    int i = 0;
	    string[,] roadkill_clustermap = new string[50000, 3];
	    while (reader.Read())
	    {
		roadkill_clustermap[i, 0] = reader["id"].ToString();
		roadkill_clustermap[i, 1] = reader["y"].ToString();
		roadkill_clustermap[i, 2] = reader["x"].ToString();
		i++;
	    }
	    reader.Close();
	    Session["roadkill_clustermap"] = roadkill_clustermap;
*/	    Session["roadkill_clustermap"] = rks;
	    string map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
	    map_url = map_url.Replace("searchall.aspx", "roadkill_clustermap.aspx");
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
	    this.div_roadkill.Visible = true;
	    this.dmap.Visible = true;
	    view = (DataView)this.sds_roadkill.Select(DataSourceSelectArguments.Empty);
	    dt_excel_roadkill = view.ToTable();
	    TotalCounts_roadkill.Text = "總筆數：" + view.Count;
	    view.Dispose();
	    }
	    else
	    {
	    this.sds_roadkill.SelectCommand = sql + " and 1 = 0";
	    this.div_roadkill.Visible = false;
	    }

	    this.gv_roadkill.DataBind();
	    this.gv_roadkill.UseAccessibleHeader = true;
	    if (this.gv_roadkill.HeaderRow != null)
	    this.gv_roadkill.HeaderRow.TableSection = TableRowSection.TableHeader;

	    string sql2 = "Select plantid,pointx,pointy,Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo,plant_loca,date,plant_number From Plants Where 1=1";
	    if (Request.Form["ctl00$cb3"] != null)
	    {
	    sql2 = "Select plantid,pointx,pointy,Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo,plant_loca,date,plant_number From Plants Where 1=1";
	    this.sds_plant.SelectParameters.Clear();
	    if (searchtxt != "")
	    {
	        string[] searchtxts = searchtxt.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql2 += " and (segment like '%' + @kwd"+i+" + '%' or plant_name like '%' + @kwd"+i+" + '%' or plant_loca like '%' + @kwd"+i+" + '%')";
		    this.sds_plant.SelectParameters.Add("kwd"+i, searchtxts[i]);
		}
	    }
	    if (highwayid != "")
	    {
		sql2 += " and highway_name = @highwayid";
		this.sds_plant.SelectParameters.Add("highwayid", highwayid);
	    }

	    if (start_date != "")
	    {
		sql2 += " and date >= @start_date";
		this.sds_plant.SelectParameters.Add("start_date", start_date.Replace("-",""));
	    }
	    if (end_date != "")
	    {
		sql2 += " and date <= @end_date";
		this.sds_plant.SelectParameters.Add("end_date", end_date.Replace("-",""));
	    }
	    if (start != 0)
	    {
		sql2 += " and (start1*1000)+start2 >= @start";
		this.sds_plant.SelectParameters.Add("start", start.ToString());
	    }
	    if (end != 0)
	    {
		sql2 += " and (end1*1000)+end2 <= @end";
		this.sds_plant.SelectParameters.Add("end", end.ToString());
	    }
	    this.sds_plant.SelectCommand = sql2;
	    this.gv_plant.DataBind();
	    this.gv_plant.UseAccessibleHeader = true;
	    if (this.gv_plant.HeaderRow != null)
	    this.gv_plant.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_plant.Visible = true;
	    view = (DataView)this.sds_plant.Select(DataSourceSelectArguments.Empty);
	    dt_excel_plant = view.ToTable();
	    TotalCounts_plant.Text = "總筆數：" + view.Count;
	    view.Dispose();
	    }
	    else
	    {
	    this.sds_plant.SelectCommand = sql2 + " and 1 = 0";
	    this.gv_plant.DataBind();
	    this.gv_plant.UseAccessibleHeader = true;
	    this.div_plant.Visible = false;
 	    }

	    string sql3 = "";
	    if (Request.Form["ctl00$cb1"] != null)
	    {
	    sql3 = "SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 Where 1=1 ";
	    this.sds_occurrence.SelectParameters.Clear();
	    if (searchtxt != "")
	    {
	        string[] searchtxts = searchtxt.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql3 += " and (Chinese_Name like '%' + @kwd"+i+" + '%' or Short_Name like '%' + @kwd"+i+" + '%')";
		    this.sds_occurrence.SelectParameters.Add("kwd"+i, searchtxts[i]);
		}
	    }
	    if (highwayid != "")
	    {
		sql3 += " and highway_id = @highwayid";
		this.sds_occurrence.SelectParameters.Add("highwayid", highwayid);
	    }
	    if (start_date != "")
	    {
		sql3 += " and Convert(Varchar(10),date1) >= @start_date";
		this.sds_occurrence.SelectParameters.Add("start_date", start_date);
	    }
	    if (end_date != "")
	    {
		sql3 += " and Convert(Varchar(10),date1) <= @end_date";
		this.sds_occurrence.SelectParameters.Add("end_date", end_date);
	    }
	    if (start != 0)
	    {
		sql3 += " and x <= (Select Top 1 WGS84X From Freeway_new Where free_id = @highwayid and mileage = @start) ";
		sql3 += " and y <= (Select Top 1 WGS84Y From Freeway_new Where free_id = @highwayid and mileage = @start) ";
		this.sds_occurrence.SelectParameters.Add("start", start.ToString());
	    }
	    if (end != 0)
	    {
		sql3 += " and x >= (Select Top 1 WGS84X From Freeway_new Where free_id = @highwayid and mileage = @end) ";
		sql3 += " and y >= (Select Top 1 WGS84Y From Freeway_new Where free_id = @highwayid and mileage = @end) ";
		this.sds_occurrence.SelectParameters.Add("end", end.ToString());
	    }

	    if (!value_Sensitive_Level.Equals("ALL"))
	    {
		sql3 += " and Sensitive_Level = @Sensitive_Level";
		this.sds_occurrence.SelectParameters.Add("Sensitive_Level", value_Sensitive_Level);
	    }

	    if (!value_group.Equals("ALL"))
	    {
		sql3 += " and [group] = @value_group";
		this.sds_occurrence.SelectParameters.Add("value_group", value_group);
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

	    this.sds_occurrence.SelectCommand = sql3;
	    this.gv_occurrence.DataBind();
	    this.gv_occurrence.UseAccessibleHeader = true;
	    if (this.gv_occurrence.HeaderRow != null)
	    this.gv_occurrence.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_occurrence.Visible = true;
	    view = (DataView)this.sds_occurrence.Select(DataSourceSelectArguments.Empty);
	    dt_excel_occurrence = view.ToTable();
	    TotalCounts_occurrence.Text = "總筆數：" + view.Count;
	    view.Dispose();
	    }
	    else
	    {
	    this.sds_occurrence.SelectCommand = "SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 where 1 = 0";
	    this.gv_occurrence.DataBind();
	    this.gv_occurrence.UseAccessibleHeader = true;
	    this.div_occurrence.Visible = false;
	    }


	    conn.Close();
	}
    }

    private void SetContent2()
    {
	this.flexCheck.InputAttributes["class"] = "form-check-input";
	this.flexCheck2.InputAttributes["class"] = "form-check-input";
	this.flexCheck3.InputAttributes["class"] = "form-check-input";
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataView view;
	    //string searchtxt2 = Request.Form["ctl00$searchtxt2"] ?? ((TextBox)Page.Master.FindControl("searchtxt2")).Text;
	    string searchtxt2 = "";
	    if (Request.Form["ctl00$searchtxt2"] != null)
		searchtxt2 = Request.Form["ctl00$searchtxt2"];
	    else if (((TextBox)Page.Master.FindControl("searchtxt2")).Text != "")
		searchtxt2 = ((TextBox)Page.Master.FindControl("searchtxt2")).Text;
	    else if (Session["searchtxt2"] != null)
		searchtxt2 = Session["searchtxt2"].ToString();
	    Session["searchtxt2"] = ((TextBox)Page.Master.FindControl("searchtxt2")).Text = searchtxt2;

	    //string start_date2 = Request.Form["ctl00$start_date2"] ?? ((TextBox)Page.Master.FindControl("start_date2")).Text;
	    string start_date2 = "";
	    if (Request.Form["ctl00$start_date2"] != null)
		start_date2 = Request.Form["ctl00$start_date2"];
	    else if (((TextBox)Page.Master.FindControl("start_date2")).Text != "")
		start_date2 = ((TextBox)Page.Master.FindControl("start_date2")).Text;
	    else if (Session["start_date2"] != null)
		start_date2 = Session["start_date2"].ToString();
	    Session["start_date2"] = start_date2;

	    //string end_date2 = Request.Form["ctl00$end_date2"] ?? ((TextBox)Page.Master.FindControl("end_date2")).Text;
	    string end_date2 = "";
	    if (Request.Form["ctl00$end_date2"] != null)
		end_date2 = Request.Form["ctl00$end_date2"];
	    else if (((TextBox)Page.Master.FindControl("end_date2")).Text != "")
		end_date2 = ((TextBox)Page.Master.FindControl("end_date2")).Text;
	    else if (Session["end_date2"] != null)
		end_date2 = Session["end_date2"].ToString();
	    Session["end_date2"] = end_date2;

	    string value_class1 = "";
	    string value_class2 = "";
	    if (Request.Form["ctl00$DropDownList4"] != null)
		value_class1 = Request.Form["ctl00$DropDownList4"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList4")).SelectedValue != "")
		value_class1 = ((DropDownList)Page.Master.FindControl("DropDownList4")).SelectedValue;
	    else if (Session["DropDownList4"] != null)
		value_class1 = Session["DropDownList4"].ToString();
	    if (Request.Form["ctl00$DropDownList5"] != null)
		value_class2 = Request.Form["ctl00$DropDownList5"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList5")).SelectedValue != "")
		value_class2 = ((DropDownList)Page.Master.FindControl("DropDownList5")).SelectedValue;
	    else if (Session["DropDownList5"] != null)
		value_class2 = Session["DropDownList5"].ToString();
	    Session["value_class1"] = value_class1;
	    Session["value_class2"] = value_class2;

	    string value_Sensitive_Level = "";
	    if (Request.Form["ctl00$DropDownList32"] != null)
		value_Sensitive_Level = Request.Form["ctl00$DropDownList32"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList32")).SelectedValue != "")
		value_Sensitive_Level = ((DropDownList)Page.Master.FindControl("DropDownList32")).SelectedValue;
	    else if (Session["DropDownList32"] != null)
		value_Sensitive_Level = Session["DropDownList32"].ToString();
	    string value_group = "";
	    if (Request.Form["ctl00$DropDownList12"] != null)
		value_group = Request.Form["ctl00$DropDownList12"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList12")).SelectedValue != "")
		value_group = ((DropDownList)Page.Master.FindControl("DropDownList12")).SelectedValue;
	    else if (Session["DropDownList12"] != null)
		value_group = Session["DropDownList12"].ToString();
	    string value_species = "";
	    if (Request.Form["ctl00$DropDownList22"] != null)
		value_species = Request.Form["ctl00$DropDownList22"];
	    else if (((DropDownList)Page.Master.FindControl("DropDownList22")).SelectedValue != "")
		value_species = ((DropDownList)Page.Master.FindControl("DropDownList22")).SelectedValue;
	    else if (Session["DropDownList22"] != null)
		value_species = Session["DropDownList22"].ToString();
	    Session["DropDownList32"] = value_Sensitive_Level;
	    Session["DropDownList12"] = value_group;
	    Session["DropDownList22"] = value_species;


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
		Session["end_date2"] = end_date2;
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
	    if (Request.Form["ctl00$cb22"] != null && ((CheckBox)Page.Master.FindControl("cb22")).Checked)
	    {
	    this.sds_roadkill.SelectCommand = sql;
	    this.div_roadkill.Visible = true;
	    this.dmap.Visible = false;
	    }
	    else
	    {
	    this.sds_roadkill.SelectCommand = sql + " and 1 = 0";
	    this.div_roadkill.Visible = false;
	    }

	    this.gv_roadkill.DataBind();
	    this.gv_roadkill.UseAccessibleHeader = true;
	    if (this.gv_roadkill.HeaderRow != null)
	    {
	    this.gv_roadkill.HeaderRow.TableSection = TableRowSection.TableHeader;
	    view = (DataView)this.sds_roadkill.Select(DataSourceSelectArguments.Empty);
	    dt_excel_roadkill = view.ToTable();
	    TotalCounts_roadkill.Text = "總筆數：" + view.Count;
	    view.Dispose();
	    }

	    string sql2 = "Select plantid,pointx,pointy,Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo,plant_loca,date,plant_number From Plants Where 1=1";
	    if (Request.Form["ctl00$cb32"] != null)
	    {
	    sql2 = "Select plantid,pointx,pointy,Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo,plant_loca,date,plant_number From Plants Where 1=1";
	    this.sds_plant.SelectParameters.Clear();
	    if (searchtxt2 != "")
	    {
	        string[] searchtxts = searchtxt2.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql2 += " and (segment like '%' + @kwd"+i+" + '%' or plant_name like '%' + @kwd"+i+" + '%' or plant_loca like '%' + @kwd"+i+" + '%')";
		    this.sds_plant.SelectParameters.Add("kwd"+i, searchtxts[i]);
		}
	    }
	    if (start_date2 != "")
	    {
		sql2 += " and date >= @start_date2";
		this.sds_plant.SelectParameters.Add("start_date2", start_date2.Replace("-",""));
	    }
	    if (end_date2 != "")
	    {
		sql2 += " and date <= @end_date2";
		this.sds_plant.SelectParameters.Add("end_date2", end_date2.Replace("-",""));
	    }

        if (!value_class2.Equals("ALL"))
        {
	    string vc2 = value_class2.Substring(0,2);
	    sql2 += " and (Segment = @vc2 or ((Segment is NULL or Segment = '')";
	    this.sds_plant.SelectParameters.Add("vc2", vc2);
            sql2 += " and PointX <= (select max_x from Engineering_road_scope where start_end = @value_class2) ";
            sql2 += " and PointX >= (select min_x from Engineering_road_scope where start_end = @value_class2) ";
            sql2 += " and PointY <= (select max_y from Engineering_road_scope where start_end = @value_class2) ";
            sql2 += " and PointY >= (select min_y from Engineering_road_scope where start_end = @value_class2))) ";
	    this.sds_plant.SelectParameters.Add("value_class2", value_class2);
        } else if (!value_class1.Equals("ALL"))
	{
	    sql2 += " and (Office = @value_class1 or ((Office is NULL or Office = '')";
            sql2 += " and PointX <= (select max_x from Engineering_road_scope where start_end = @value_class1) ";
            sql2 += " and PointX >= (select min_x from Engineering_road_scope where start_end = @value_class1) ";
            sql2 += " and PointY <= (select max_y from Engineering_road_scope where start_end = @value_class1) ";
            sql2 += " and PointY >= (select min_y from Engineering_road_scope where start_end = @value_class1))) ";
	    this.sds_plant.SelectParameters.Add("value_class1", value_class1);
	}
	    this.sds_plant.SelectCommand = sql2;
	    this.gv_plant.DataBind();
	    this.gv_plant.UseAccessibleHeader = true;
	    if (this.gv_plant.HeaderRow != null)
	    this.gv_plant.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_plant.Visible = true;
	    view = (DataView)this.sds_plant.Select(DataSourceSelectArguments.Empty);
	    dt_excel_plant = view.ToTable();
	    TotalCounts_plant.Text = "總筆數：" + view.Count;
	    view.Dispose();
	    }
	    else
	    {
	    this.sds_plant.SelectCommand = sql2 + " and 1 = 0";
	    this.gv_plant.DataBind();
	    this.gv_plant.UseAccessibleHeader = true;
	    this.div_plant.Visible = false;
 	    }

	    string sql3 = "";
	    if (Request.Form["ctl00$cb12"] != null)
	    {
	    sql3 = "SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 Where 1=1 ";
	    this.sds_occurrence.SelectParameters.Clear();
	    if (searchtxt2 != "")
	    {
	        string[] searchtxts = searchtxt2.Split(' ');
	        for ( int i = 0; i < searchtxts.Length; i++ )
		{
		    sql3 += " and (Chinese_Name like '%' + @kwd"+i+" + '%' or Short_Name like '%' + @kwd"+i+" + '%')";
		    this.sds_occurrence.SelectParameters.Add("kwd"+i, searchtxts[i]);
		}
	    }

	    if (start_date2 != "")
	    {
		sql3 += " and CONVERT (char(10), date1, 126) >= @start_date2";
		this.sds_occurrence.SelectParameters.Add("start_date2", start_date2);
	    }
	    if (end_date2 != "")
	    {
		sql3 += " and CONVERT (char(10), date1, 126) <= @end_date2";
		this.sds_occurrence.SelectParameters.Add("end_date2", end_date2);
	    }

        if (!value_class2.Equals("ALL"))
        {
            sql3 += " and x <= (select max_x from Engineering_road_scope where start_end = @value_class2) ";
            sql3 += " and x >= (select min_x from Engineering_road_scope where start_end = @value_class2) ";
            sql3 += " and y <= (select max_y from Engineering_road_scope where start_end = @value_class2) ";
            sql3 += " and y >= (select min_y from Engineering_road_scope where start_end = @value_class2) ";
	    this.sds_occurrence.SelectParameters.Add("value_class2", value_class2);
        }
        else if (!value_class1.Equals("ALL"))
        {
            sql3 += " and x <= (select max_x from Engineering_road_scope where start_end = @value_class1) ";
            sql3 += " and x >= (select min_x from Engineering_road_scope where start_end = @value_class1) ";
            sql3 += " and y <= (select max_y from Engineering_road_scope where start_end = @value_class1) ";
            sql3 += " and y >= (select min_y from Engineering_road_scope where start_end = @value_class1) ";
	    this.sds_occurrence.SelectParameters.Add("value_class1", value_class1);
        }

	    if (!value_Sensitive_Level.Equals("ALL"))
	    {
		sql3 += " and Sensitive_Level = @Sensitive_Level";
		this.sds_occurrence.SelectParameters.Add("Sensitive_Level", value_Sensitive_Level);
	    }

	    if (!value_group.Equals("ALL"))
	    {
		sql3 += " and [group] = @value_group";
		this.sds_occurrence.SelectParameters.Add("value_group", value_group);
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

	    this.sds_occurrence.SelectCommand = sql3;
	    this.gv_occurrence.DataBind();
	    this.gv_occurrence.UseAccessibleHeader = true;
	    if (this.gv_occurrence.HeaderRow != null)
	    this.gv_occurrence.HeaderRow.TableSection = TableRowSection.TableHeader;
	    this.div_occurrence.Visible = true;
	    view = (DataView)this.sds_occurrence.Select(DataSourceSelectArguments.Empty);
	    dt_excel_occurrence = view.ToTable();
	    TotalCounts_occurrence.Text = "總筆數：" + view.Count;
	    view.Dispose();
	    }
	    else
	    {
	    this.sds_occurrence.SelectCommand = "SELECT distinct  sid, CONVERT (char(10), date1, 126) AS invest_date, highway_id, site_ch, Round(x,2) as x , Round(y,2) as y, TM2_X, TM2_Y, Short_Name , Chinese_Name , accepted_name_code , '../Attachments/Occurrence/'+file1 AS lo FROM table_1 where 1 = 0";
	    this.gv_occurrence.DataBind();
	    this.gv_occurrence.UseAccessibleHeader = true;
	    this.div_occurrence.Visible = false;
	    }

	    conn.Close();
	}
    }

    protected void Export_Excel_occurrence(object sender, EventArgs e)
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
            foreach (DataRow pDR_FSData in dt_excel_occurrence.Rows)
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


    protected void Export_Excel_roadkill(object sender, EventArgs e)
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
            foreach (DataRow pDR_FSData in dt_excel_roadkill.Rows)
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

    protected void Export_Excel_plant(object sender, EventArgs e)
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
            foreach (DataRow pDR_FSData in dt_excel_plant.Rows)
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
	    this.gv_roadkill.Columns[12].Visible = true;
	    this.gv_roadkill.Columns[13].Visible = true;
	    Session["flexcheck1"] = 1;
	}
	else
	{
	    this.gv_roadkill.Columns[12].Visible = false;
	    this.gv_roadkill.Columns[13].Visible = false;
	    Session["flexcheck1"] = 0;
	}
    }

    protected void flexCheck2_Changed(object sender, EventArgs e)
    {
	if (flexCheck2.Checked)
	{
	    this.gv_plant.Columns[10].Visible = true;
	    this.gv_plant.Columns[11].Visible = true;
	    Session["flexcheck2"] = 1;
	}
	else
	{
	    this.gv_plant.Columns[10].Visible = false;
	    this.gv_plant.Columns[11].Visible = false;
	    Session["flexcheck2"] = 0;
	}
    }

    protected void flexCheck3_Changed(object sender, EventArgs e)
    {
	if (flexCheck3.Checked)
	{
	    this.gv_occurrence.Columns[8].Visible = true;
	    this.gv_occurrence.Columns[9].Visible = true;
	    Session["flexcheck3"] = 1;
	}
	else
	{
	    this.gv_occurrence.Columns[8].Visible = false;
	    this.gv_occurrence.Columns[9].Visible = false;
	    Session["flexcheck3"] = 0;
	}
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
