using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Plant_observation_edit5 : System.Web.UI.Page
{
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
        string value_direction = direction.SelectedValue;
        string value_highway_id = highway_id.SelectedValue;

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

        string value_KeyWords = KeyWords.Text;
        //不統計里程
        //sql = "Select chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage , sum(amount) as amount From Plant_observation  Where chinese_name <> '' "; 
        //sql = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage , amount From Plant_observation  Where chinese_name <> '' "; 

        //填入里程或者關鍵字將數量加總，不填入抓id當KEY值傳到明細頁
        //sql_ori = "Select id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        sql_ori = "Select id ,pid, chinese_name , highway_id , direction , plant_class , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range , start_mileage , end_mileage ,  amount From Plant_observation  Where chinese_name <> '' and Delete_Flag = '' ";
        sql = "Select chinese_name , highway_id , direction , plant_class , MIN(Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10))) + '～' + MAX(Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10))) AS Range , sum(amount) as amount , MIN(start_mileage) AS start_mileage , MAX(end_mileage) AS  end_mileage From Plant_observation  Where chinese_name <> '' and delete_flag = '' ";
        sql_where += " Where delete_flag='' ";

        if (!value_KeyWords.Equals(""))
        {
            if (QueryType.Checked)
            {
                sql_ori += " And chinese_name Like '%" + value_KeyWords + "%' ";
                sql += " And chinese_name Like '%" + value_KeyWords + "%' ";
                sql_where += " And chinese_name Like '%" + value_KeyWords + "%' ";
            }
            else
            {
                sql_ori += " And chinese_name = '" + value_KeyWords + "' ";
                sql += " And chinese_name = '" + value_KeyWords + "' ";
                sql_where += " And chinese_name = '" + value_KeyWords + "' ";
            }

        }

        if (!value_highway_id.Equals("0") && !value_highway_id.Equals(""))
        {
            sql_ori += " and highway_id = '" + value_highway_id + "'";
            sql += " and highway_id = '" + value_highway_id + "'";
            sql_where += " and highway_id = '" + value_highway_id + "'";
        }

        if (!value_direction.Equals("0") && !value_direction.Equals(""))
        {
            sql_ori += " and direction = '" + value_direction + "'";
            sql += " and direction = '" + value_direction + "'";
            sql_where += " and direction = '" + value_direction + "'";
        }

        //有填入起始與結束的里程或者關鍵字
        if (!value_up_mileage.Equals(0) || !value_down_mileage.Equals(0))
        {
            sql += " and ( ";
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
        }

        //未填入里程
        sql_ori += "Order by  id , chinese_name , highway_id , direction , plant_class , range , start_mileage , end_mileage ";
        sql += " Group By chinese_name , highway_id , direction , plant_class ,center_mileage";
        /*
        SDS_View_Ori.SelectCommand = sql_ori;
        GridView_Ori.DataBind();
        GridView_Ori.PageSize = 20;
        */
        
        SDS_View.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

        GridView_Ori.Visible = false;
        GridView_List.Visible = true;
        btnShowGVCount_Click1();

        /*
        if (value_up_mileage.Equals(0) && value_down_mileage.Equals(0) && value_KeyWords.Equals(""))
        {
            //GridView_Ori.EnableViewState = true;
            GridView_Ori.Visible = true;
            GridView_List.Visible = false;
            btnShowGVCount_Ori();
        }
        else
        {
            GridView_Ori.Visible = false;
            GridView_List.Visible = true;
            btnShowGVCount_Click1();
        }
        */

        //組googlemap所需的資料

        //先拿掉統計數據
        //sql_xy += " Select Distinct (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y ,  ";
        //sql_xy += " Select (start_x +end_x)/2 AS x , (start_y+end_y)/2 AS y , sum(amount) AS amount , ";
        //改為抓Center_x與Center_y
        sql_xy += " Select center_x AS x , center_y AS y , sum(amount) AS amount , chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) AS Range ,";
        sql_xy += " (Select Max(amount) From (Select Sum(amount) As amount From  Plant_observation " + sql_where + " and start_x is not null Group By Center_Mileage,chinese_Name) tmp) AS max_amount ,";
        sql_xy += " (Select (Max(start_x) + Min(start_x)) /2 From  Plant_observation " + sql_where + " And start_x is not null) AS center_x ,";
        sql_xy += " (Select (Max(start_y) + Min(start_y)) /2 From  Plant_observation " + sql_where + " And start_x is not null) AS center_y ";
        sql_xy += " From Plant_observation ";
        sql_xy += sql_where;
        sql_xy += " Group By center_x  , center_y ,chinese_name , Cast(start_mileage1 as varchar(10)) + 'K＋' + Cast(start_mileage2 as varchar(10)) + '～' + Cast(end_mileage1 as varchar(10)) + 'K＋' + Cast(end_mileage2 as varchar(10)) ";
        ;
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

    protected void btnShowGVCount_Ori()
    {
        DataView view = (DataView)SDS_View_Ori.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }

}