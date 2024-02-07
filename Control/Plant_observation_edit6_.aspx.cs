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


public partial class Control_Plant_observation_edit6 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        Query();
    }

    protected void Query()
    {
        string value_chinese_name = Request.QueryString["chinese_name"];
        string value_highway_id = Request.QueryString["highway_id"];
        string value_plant_class = Request.QueryString["plant_class"];
        string value_direction = Request.QueryString["direction"];
        string value_start_mileage = Request.QueryString["start_mileage"];
        string value_end_mileage = Request.QueryString["end_mileage"];
        

        string sql = "Select id , pid , chinese_name , ScientificName , high , width , m_high , amount , highway_id , location , direction , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , plant_class ,plant_type , notes, photo_number , note_image , file1 , case when photo_number <> '' then '..' + path1 + photo_number end as url1 , case when note_image <> ''  then '..' + path1 + note_image end as url2 , case when file1 <> '' then '..' + path1 + file1 end as url3 , main1 , main2, main3 , case when main1 <> ''  then '..' + path1 + main1 end as main1_url , case when main2 <> ''  then '..' + path1 + main2 end as main2_url , case when main3 <> ''  then '..' + path1 + main3 end as main3_url From Plant_observation  Where delete_flag = ''   ";
	SDS_View.SelectParameters.Clear();
        if (!value_chinese_name.Equals(""))
        {
            sql += " and chinese_name = @value_chinese_name ";
	    SDS_View.SelectParameters.Add("value_chinese_name",value_chinese_name);
        }
        if (!value_highway_id.Equals(""))
        {
            sql += " and highway_id = @value_highway_id ";
	    SDS_View.SelectParameters.Add("value_highway_id",value_highway_id);
        }
        if (!value_plant_class.Equals(""))
        {
            sql += " and plant_class = @value_plant_class ";
	    SDS_View.SelectParameters.Add("value_plant_class",value_plant_class);
        }
        if (!value_direction.Equals(""))
        {
            sql += " and direction = @value_direction ";
	    SDS_View.SelectParameters.Add("value_direction",value_direction);
        }
        sql += " and start_mileage >= @value_start_mileage ";
        sql += " and end_mileage <= @value_end_mileage ";
        SDS_View.SelectCommand = sql;
	SDS_View.SelectParameters.Add("value_start_mileage",value_start_mileage);
	SDS_View.SelectParameters.Add("value_end_mileage",value_end_mileage);
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

    }
    
}