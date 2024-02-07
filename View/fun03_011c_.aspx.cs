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


public partial class View_fun03_011c : System.Web.UI.Page
{
        
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }        
        //if (!IsPostBack)
        //{
            Query();
        //}
    }


    protected void Query()
    {
        
        string value_chinese_name = Request.QueryString["chinese_name"];
        string value_highway_id = Request.QueryString["highway_id"];
        string value_plant_class = Request.QueryString["plant_class"];
        string value_direction = Request.QueryString["direction"];
        string value_start_mileage = Request.QueryString["start_mileage"];
        string value_end_mileage = Request.QueryString["end_mileage"];
        string sql_where = "";
        string sql_xy = "";

        //string sql = "Select chinese_name , ScientificName , high , width , m_high , amount , highway_id , location , direction , range , plant_class ,plant_type , notes, photo_number ,start_x AS x , start_y AS y  From Plant_observation  Where 1= 1 ";
        //string sql = "Select chinese_name , ScientificName , high , width , m_high , amount , highway_id , location , direction , range , plant_class ,plant_type , notes, photo_number , note_image , file1 ,  case when photo_number <> '' and note_image = '' then '..' + path1 + photo_number end as url1 , case when note_image <> ''  then '..' + path1 + note_image end as url2 , case when file1 <> '' and photo_number = '' and note_image = '' then '..' + path1 + file1 end as url3  From Plant_observation  Where 1=1  " ;
        //string sql = "Select chinese_name , ScientificName , high , width , m_high , amount , highway_id , location , direction , Cast(start_mileage as nvarchar) + '-' + Cast(end_mileage as nvarchar) as range , plant_class ,plant_type , notes, photo_number , note_image , file1 , case when photo_number <> '' then '..' + path1 + photo_number end as url1 , case when note_image <> ''  then '..' + path1 + note_image end as url2 , case when file1 <> '' then '..' + path1 + file1 end as url3 , main1 , main2, main3 , case when main1 <> ''  then '..' + path1 + main1 end as main1_url , case when main2 <> ''  then '..' + path1 + main2 end as main2_url , case when main3 <> ''  then '..' + path1 + main3 end as main3_url From Plant_observation  Where 1=1  ";
        //string sql = "Select chinese_name , ScientificName , high , width , m_high , amount , highway_id , location , direction , Cast(start_mileage as nvarchar) + '-' + Cast(end_mileage as nvarchar) as range , plant_class ,plant_type , notes, photo_number , note_image , file1 , case when photo_number <> '' then '..' + path1 + photo_number end as url1 , case when note_image <> ''  then '..' + path1 + note_image end as url2 , case when file1 <> '' then '..' + path1 + file1 end as url3 , main1 , main2, main3 , case when main1 <> ''  then '..' + path1 + main1 end as main1_url , case when main2 <> ''  then '..' + path1 + main2 end as main2_url , case when main3 <> ''  then '..' + path1 + main3 end as main3_url From Plant_observation  Where delete_flag = ''   ";
        string sql = "Select chinese_name , ScientificName , high , width , m_high , amount , highway_id , location , direction , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , plant_class ,plant_type , notes, photo_number , note_image , file1 , case when photo_number <> '' then '..' + path1 + photo_number end as url1 , case when note_image <> ''  then '..' + path1 + note_image end as url2 , case when file1 <> '' then '..' + path1 + file1 end as url3 , main1 , main2, main3 , case when main1 <> ''  then '..' + path1 + main1 end as main1_url , case when main2 <> ''  then '..' + path1 + main2 end as main2_url , case when main3 <> ''  then '..' + path1 + main3 end as main3_url From Plant_observation  Where delete_flag = ''   "; 
        sql += " and chinese_name = '" + value_chinese_name + "' ";
        sql += " and highway_id = '" + value_highway_id + "' ";
        sql += " and plant_class = '" + value_plant_class + "' ";
        sql += " and direction = '" + value_direction + "' ";
        sql += " and start_mileage >= " + value_start_mileage + " ";
        sql += " and end_mileage <= " + value_end_mileage + " ";
        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

        

        //組googlemap所需的資料
        //sql_where += " Where 1=1 ";
        sql_where += " Where  delete_flag = '' ";
        sql_where += " and chinese_name = '" + value_chinese_name + "' ";
        sql_where += " and highway_id = '" + value_highway_id + "' ";
        sql_where += " and plant_class = '" + value_plant_class + "' ";
        sql_where += " and direction = '" + value_direction + "' ";
        sql_where += " and start_mileage >= '" + value_start_mileage + "' ";
        sql_where += " and end_mileage <= '" + value_end_mileage + "' ";



        //sql_xy += " Select x , y , max_amount , center_x , center_y , Sum(amount) AS amount From ( ";
        sql_xy += " Select center_x as x , center_y as y ,  center_x , center_y , Sum(amount) AS amount , Case When Count(x) > 1 Then 'G' Else 'P' End AS Icon_Color , chinese_name , Range";
        sql_xy += " From ( ";
        sql_xy += " Select (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y , amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , ";
        //sql_xy += " (Select Max(amount) From  Plant_observation " + sql_where + " ) AS max_amount ,";
        sql_xy += " (Select (Max(start_x) + Min(start_x)) /2 From  Plant_observation " + sql_where + " ) AS center_x ,";
        sql_xy += " (Select (Max(start_y) + Min(start_y)) /2 From  Plant_observation " + sql_where + " ) AS center_y ";
        sql_xy += " From Plant_observation ";
        sql_xy += sql_where;
        sql_xy += " ) tmp ";
        sql_xy += " Group By center_x , center_y , chinese_name , Range ";

        Get_XY(sql_xy);
        
    }
    protected void Get_XY(String sql)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand_NameFull = sql;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            string[,] xy03_011c = new string[1000, 8];
            //string[,] xy03_011c = new string[1000, 2];
            while (reader.Read())
            {
                xy03_011c[i, 0] = reader["y"].ToString();
                xy03_011c[i, 1] = reader["x"].ToString();
                xy03_011c[i, 2] = reader["amount"].ToString();
                xy03_011c[i, 3] = reader["Icon_Color"].ToString();
                //xy03_011c[i, 3] = reader["max_amount"].ToString();
                xy03_011c[i, 4] = reader["center_x"].ToString();
                xy03_011c[i, 5] = reader["center_y"].ToString();
                xy03_011c[i, 6] = reader["chinese_name"].ToString();
                xy03_011c[i, 7] = reader["Range"].ToString();
                
                i += 1;
            }
            //產生Session
            Session["xy03_011c"] = xy03_011c;
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        //if (!IsPostBack){
            String map_url = "";
            map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
            map_url = map_url.Replace("fun03_011c_.aspx", "gmap03_011c.aspx");
            googlemap.Src = map_url;
            //googlemap.Visible = false;
        //}

        
    }


}