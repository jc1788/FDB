using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;



public partial class View_fun03_011a : System.Web.UI.Page
{
    public string map_url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        if (IsPostBack)
        {
            Query();
        }
    }


    protected void Query()
    {
        string sql = "";
        string sql_ori = "";
        string sql_xy = "";
        string sql_where = "";
        string value_highway_id = highway_id.SelectedValue;
        String value_Start_Mileston1 = Start_Mileston1.Text;
        String value_Start_Mileston2 = Start_Mileston2.Text;
        String value_End_Mileston1 = End_Mileston1.Text;
        String value_End_Mileston2 = End_Mileston2.Text;
        string value_florescenceS = florescenceS.Text;
        string value_florescenceE = florescenceE.Text;
        string value_flowercolor = flowercolor.SelectedValue;
        string value_leafperiodS = leafperiodS.Text;
        string value_leafperiodE = leafperiodE.Text;
        string value_leafcolor = leafcolor.SelectedValue;
        string value_florescence = "";
        string value_leafperiod = "";

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
        string tmp_chinese_name = KeyWords.Text.ToString();
        string[] tmp_chinese_name_array = (KeyWords.Text.ToString().IndexOf(' ') > 0) ? tmp_chinese_name.Split(' ') : null;
        string value_KeyWords = KeyWords.Text;

        sql_ori = "Select plantid,pointx,pointy, Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo From Plants Where 1=1 ";
        sql = "Select plantid,pointx,pointy, Plant_name,segment,highway_name,start,[end],direction,cast(convert(decimal(18,1),start) as varchar)+'~'+cast(convert(decimal(18,1),[end]) as varchar) as [range],Plant_number,florescence,FlowerColor,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as Img,case when Img <>'' then ('..\\Attachments\\Plants\\'+Img) else '' end as lo,Case When Img='' Then '' Else '' End As image_memo From Plants Where 1=1 ";
        sql_where += " Where plant_name <> '' ";

        //if (!value_KeyWords.Equals(""))
        //{
        //    sql_ori += " And Plant_name = '" + value_KeyWords + "' ";
        //    sql += " And Plant_name = '" + value_KeyWords + "' ";
        //    sql_where += " And Plant_name = '" + value_KeyWords + "' ";
        //}

        if (tmp_chinese_name_array != null && tmp_chinese_name_array[1] != "")
        {
            sql += " and ( ";
            sql_ori += " and ( ";
            sql_where += " and ( ";
            int i = 1;
            int j = tmp_chinese_name_array.Length;
            foreach (var item in tmp_chinese_name_array)
            {
                sql += " (Plant_name like '%" + item + "%') ";
                sql_ori += " (Plant_name like '%" + item + "%') ";
                sql_where += " (Plant_name like '%" + item + "%') ";
                if (i != j)
                {
                    sql += " or ";
                    sql_ori += " or ";
                    sql_where += " or ";
                }
                i++;
            }
            sql += " ) ";
            sql_ori += " ) ";
            sql_where += " ) ";
        }
        else if (!value_KeyWords.Equals("ALL") && !value_KeyWords.Equals(""))
        {
            sql += " and (Plant_name like '%" + value_KeyWords + "%') ";
            sql_ori += " and (Plant_name like '%" + value_KeyWords + "%') ";
            sql_where += " and (Plant_name like '%" + value_KeyWords + "%') ";
        }

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql_ori += " and highway_name = '" + value_highway_id + "'";
            sql += " and highway_name = '" + value_highway_id + "'";
            sql_where += " and highway_name = '" + value_highway_id + "'";
        }
        //有填入起始與結束的里程或者關鍵字
        if (!value_up_mileage.Equals(0))
        {
            sql_ori += " and start >= " + value_Start_Mileston1 + "." + value_Start_Mileston2 + " ";
            sql += " and start >= " + value_Start_Mileston1 + "." + value_Start_Mileston2 + " ";
            sql_where += " and start >= " + value_Start_Mileston1 + "." + value_Start_Mileston2 + " ";

        }
        if (!value_down_mileage.Equals(0))
        {
            sql_ori += " and [end] <= " + value_End_Mileston1 + "." + value_End_Mileston2 + " ";
            sql += " and [end] <= " + value_End_Mileston1 + "." + value_End_Mileston2 + " ";
            sql_where += " and [end] <= " + value_End_Mileston1 + "." + value_End_Mileston2 + " ";
        }

        if (value_flowercolor != "")
        {
            sql_ori += " and flowercolor = '" + value_flowercolor + "'";
            sql += " and flowercolor = '" + value_flowercolor + "'";
            sql_where += " and flowercolor = '" + value_flowercolor + "'";
        }

        if (value_leafcolor != "")
        {
            sql_ori += " and leafcolor = '" + value_leafcolor + "'";
            sql += " and leafcolor = '" + value_leafcolor + "'";
            sql_where += " and leafcolor = '" + value_leafcolor + "'";
        }
       
        if (value_florescenceS != "" & value_florescenceE != "")
        {
            sql_ori += " and (','+florescence like '%," + value_florescenceS + ",%' or ','+florescence like '%," + value_florescenceE + ",%')";
            sql += " and (','+florescence like '%," + value_florescenceS + ",%' or ','+florescence like '%," + value_florescenceE + ",%')";
            sql_where += " and (','+florescence like '%," + value_florescenceS + ",%' or ','+florescence like '%," + value_florescenceE + ",%')";
        }
        else if (value_florescenceS != "" & value_florescenceE == "")
        {
            sql_ori += " and (','+florescence like '%," + value_florescenceS + ",%')";
            sql += " and (','+florescence like '%," + value_florescenceS + ",%')";
            sql_where += " and (','+florescence like '%," + value_florescenceS + ",%')";
        }
        else if (value_florescenceE != "" & value_florescenceS == "")
        {
            sql_ori += " and (','+florescence like '%," + value_florescenceE + ",%')";
            sql += " and (','+florescence like '%," + value_florescenceE + ",%')";
            sql_where += " and (','+florescence like '%," + value_florescenceE + ",%')";
        }

        if (value_leafperiodS != "" & value_leafperiodE != "")
        {
            sql_ori += " and (','+LeafPeriod like '%," + value_leafperiodS + ",%' or ','+LeafPeriod like '%," + value_leafperiodE + ",%')";
            sql += " and (','+LeafPeriod like '%," + value_leafperiodS + ",%' or ','+LeafPeriod like '%," + value_leafperiodE + ",%')";
            sql_where += " and (','+LeafPeriod like '%," + value_leafperiodS + ",%' or ','+LeafPeriod like '%," + value_leafperiodE + ",%')";
        }
        else if (value_leafperiodS != "" & value_leafperiodE == "")
        {
            sql_ori += " and (','+LeafPeriod like '%," + value_leafperiodS + ",%')";
            sql += " and (','+LeafPeriod like '%," + value_leafperiodS + ",%')";
            sql_where += " and (','+LeafPeriod like '%," + value_leafperiodS + ",%')";
        }
        else if (value_leafperiodE != "" & value_leafperiodS == "")
        {
            sql_ori += " and (','+LeafPeriod like '%," + value_leafperiodE + ",%')";
            sql += " and (','+LeafPeriod like '%," + value_leafperiodE + ",%')";
            sql_where += " and ','+(LeafPeriod like '%," + value_leafperiodE + ",%')";
        }
       

        //填入物種名稱
        //sql_ori += "Order by  id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ";
        sql_ori += " Group By Plant_name , highway_name , direction ,Segment,start,[end],Plant_Number,florescence,FlowerColor,Img,plantid,pointx,pointy";
        sql += " Group By Plant_name , highway_name , direction,Segment,start,[end],Plant_Number,florescence,FlowerColor,Img,plantid,pointx,pointy ";
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
            SDS_View.SelectCommand = sql_ori;
            GridView_List.DataBind();
            GridView_List.PageSize = 20;
            GridView_Ori.Visible = false;
            GridView_List.Visible = true;
            // btnShowGVCount_Ori();
            btnShowGVCount_Click1();
        }
        else
        {
            SDS_View.SelectCommand = sql;
            GridView_List.DataBind();
            GridView_List.PageSize = 20;
            GridView_Ori.Visible = false;
            GridView_List.Visible = true;
            btnShowGVCount_Click1();
        }
        //組googlemap所需的資料
        //先拿掉統計數據
        //改為抓Center_x與Center_y
        if (!value_KeyWords.Equals(""))
        {
            sql_xy = "";
            sql_xy = "select plantid,Flowercolor,highway_name,plant_name, pointx as x,pointy as y from plants " + sql_where;
            sql_xy += " and pointx is not null and pointy is not null order by x,y ";
            //sql_xy += " Select center_x AS x , center_y AS y , center_x , center_y , sum(amount) as amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , highway_id , direction ";
            //sql_xy += "        , (Select Max(amount) AS max_amount From (select sum(amount) AS amount From Plant_observation " + sql_where + " and start_x is not null  Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage , center_x , center_y ) tmp ) AS max_amount";
            //sql_xy += " From Plant_observation " + sql_where;
            //sql_xy += " Group By chinese_name , highway_id , direction , plant_class , start_mileage1 , start_mileage2 , end_mileage1 , end_mileage2 , start_mileage , end_mileage , center_x , center_y ";
        }
        else
        {
            sql_xy = "";
            sql_xy = "select plantid,Flowercolor,highway_name,plant_name, pointx as x,pointy as y from plants " + sql_where;
            sql_xy += " and pointx is not null and pointy is not null order by x,y ";
            //sql_xy += " Select a.* , b.x , b.y , b.x AS center_x , b.y AS center_y  ";
            //sql_xy += "  From ( ";
            //sql_xy += "          Select chinese_name , highway_id  , plant_class, sum(amount) AS amount , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , Round((MIN(start_mileage)+MAX(end_mileage))/2,1)*1000 AS center_mileage ";
            //sql_xy += "                , Case When highway_id in ('1','3','5','7','9') And Direction Not In ('N','S') Then 'N' When highway_id not in ('1','3','5','7','9') And Direction Not In  ('E','W') Then 'E' Else Direction End AS direction ";
            //sql_xy += "                , (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " Group By chinese_name , highway_id , direction , plant_class ) tmp ) AS max_amount  ";
            //sql_xy += "          From plant_observation ";
            //sql_xy += sql_where;
            //sql_xy += "          Group By chinese_name , highway_id , direction , plant_class ";
            //sql_xy += "         ) a , freeway_2016 b ";
            //sql_xy += " Where a.highway_id = b.free_id and a.direction = b.direction and a.center_mileage = b.mileage ";
        }

        Get_XY(sql_xy);
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
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
            int j = 0;
            string[,] xy03_011a = new string[20000, 8];
            string x_ori = "";
            string y_ori = "";


            while (reader.Read())
            {
                string highway_direction = "";
                string highway_id = "";
                highway_id = reader["highway_name"].ToString();
                if (highway_id.Equals("2") || highway_id.Equals("4") || highway_id.Equals("6") || highway_id.Equals("8") || highway_id.Equals("10") || highway_id.Equals("3甲"))
                {
                    highway_direction = "2";
                }
                else
                {
                    highway_direction = "1";
                }

                //相同x,y
                if (x_ori.Equals(reader["x"].ToString()) && y_ori.Equals(reader["y"].ToString()))
                {
                    j++;
                    //橫向國道加緯度
                    if (highway_direction.Equals("2"))
                    {
                        xy03_011a[i, 0] = reader["y"].ToString();
                        xy03_011a[i, 1] = (Convert.ToDouble(reader["x"].ToString()) + (0.000005 * j)).ToString();
                    }
                    //直向國道加經度
                    else
                    {
                        xy03_011a[i, 0] = (Convert.ToDouble(reader["y"].ToString()) + (0.00001 * j)).ToString();
                        xy03_011a[i, 1] = reader["x"].ToString();
                    }
                }
                else
                {
                    j = 0;
                    xy03_011a[i, 0] = reader["y"].ToString();
                    xy03_011a[i, 1] = reader["x"].ToString();
                    x_ori = reader["x"].ToString();
                    y_ori = reader["y"].ToString();
                }

                //xy03_011a[i, 2] = reader["amount"].ToString();
                //xy03_011a[i, 3] = reader["max_amount"].ToString();
                //xy03_011a[i, 4] = reader["center_x"].ToString();
                //xy03_011a[i, 5] = reader["center_y"].ToString();
                //xy03_011a[i, 6] = reader["Plant_name"].ToString();
                //xy03_011a[i, 7] = reader["range"].ToString();
                xy03_011a[i, 2] = "";
                xy03_011a[i, 3] = "";
                xy03_011a[i, 4] = "";
                xy03_011a[i, 5] = reader["PlantId"].ToString();
                xy03_011a[i, 6] = reader["Plant_name"].ToString();
                xy03_011a[i, 7] = reader["Flowercolor"].ToString();
                //第一筆
                if (i == 0)
                {
                    x_ori = reader["x"].ToString();
                    y_ori = reader["y"].ToString();
                }
                i += 1;
            }
            //產生Session
            Session["xy02_011a"] = null;
	    if (i > 0)
            Session["xy02_011a"] = xy03_011a;
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        //String map_url = "";
	if (Session["xy02_011a"] != null) {
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun03_011a_.aspx", "gmap02_011a_.aspx");
	}
        //googlemap.Src = map_url;
    }

    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
	if (count > 0)
        TotalCounts.Text = "總筆數：" + count;
	else
	TotalCounts.Text = "查無資料";
        view.Dispose();
    }

    protected void btnShowGVCount_Ori()
    {
        DataView view = (DataView)SDS_View_Ori.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
	if (count > 0)
        TotalCounts.Text = "總筆數：" + count;
	else
	TotalCounts.Text = "查無資料";
        view.Dispose();
    }
}
