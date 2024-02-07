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

public partial class View_fun07_03 : System.Web.UI.Page
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        //string Json_Data = "[{\"articles\":[[2008,100],[2009,242],[2010,709],[2011,1061],[2012,874],[2013,673],[2014,64],[2015,0]],\"total\":3623,\"name\":\"國1\"},{\"articles\":[[2008,20],[2009,13],[2010,28],[2011,49],[2012,33],[2013,47],[2014,5],[2015,0]],\"total\":175,\"name\":\"國10\"},{\"articles\":[[2008,30],[2009,11],[2010,20],[2011,22],[2012,32],[2013,21],[2014,0],[2015,0]],\"total\":106,\"name\":\"國2\"},{\"articles\":[[2008,40],[2009,342],[2010,712],[2011,751],[2012,958],[2013,947],[2014,200],[2015,0]],\"total\":3910,\"name\":\"國3\"},{\"articles\":[[2008,50],[2009,4],[2010,5],[2011,8],[2012,23],[2013,21],[2014,2],[2015,0]],\"total\":63,\"name\":\"國3甲\"},{\"articles\":[[2008,60],[2009,5],[2010,28],[2011,32],[2012,9],[2013,12],[2014,0],[2015,0]],\"total\":86,\"name\":\"國4\"},{\"articles\":[[2008,0],[2009,2],[2010,2],[2011,40],[2012,40],[2013,36],[2014,4],[2015,0]],\"total\":124,\"name\":\"國5\"},{\"articles\":[[2008,70],[2009,0],[2010,0],[2011,2],[2012,0],[2013,0],[2014,0],[2015,0]],\"total\":2,\"name\":\"國6\"},{\"articles\":[[2008,80],[2009,3],[2010,1],[2011,6],[2012,21],[2013,19],[2014,10],[2015,0]],\"total\":60,\"name\":\"國8\"}]";
        
        //json_data.Text = "[{\"articles\": [[1980,46],[1985,0],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":46,\"name\":\"1979－2003年間之植群調查資料\"},{\"articles\": [[1980,0],[1985,0],[1990,0],[1995,0],[2000,3],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":3,\"name\":\"中央研究院植物標本館標本資料庫\"},{\"articles\": [[1980,0],[1985,0],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,106],[2008,2],[2010,0],[2012,0]],\"total\":108,\"name\":\"國家植群多樣性調查及製圖計畫\"},{\"articles\": [[1980,0],[1985,0],[1990,0],[1995,0],[2000,1],[2002,5],[2004,36],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":42,\"name\":\"林務局生物資源資料庫\"},{\"articles\": [[1980,54],[1985,3],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":57,\"name\":\"林試所植物標本館資料集\"},{\"articles\": [[1980,16],[1985,0],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":16,\"name\":\"森林永久樣區調查工作\"},{\"articles\": [[1980,0],[1985,0],[1990,0],[1995,2],[2000,2],[2002,26],[2004,22],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":52,\"name\":\"特生中植物資料庫\"},{\"articles\": [[1980,0],[1985,19],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],\"total\":19,\"name\":\"臺大植物標本館資料集\"}]";
        //using (StreamWriter sw = new StreamWriter(@"C:\freeway2\Scripts\journals\data.json"))
        /*
        string Json_Data = "";
        using (StreamWriter sw = new StreamWriter(@"C:\Projects\freeway2(old)\Scripts\journals\data.json"))
        {
            sw.Write(Json_Data);
        }
        */
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
        //DropDownList_Page_SelectedIndexChanged(sender, e);
        //GridView_List_DataBound(sender, e);
    }

    protected void Query()
    {

        string value_animal = DropDownList1.SelectedValue;
        string sql = "  ";

        sql += " Select highway_id ";
        sql += "        , Sum(Year_2008) AS Year_2008 ";
        sql += "        , Sum(Year_2009) AS Year_2009 ";
        sql += "        , Sum(Year_2010) AS Year_2010 ";
        sql += "        , Sum(Year_2010) AS Year_2010 ";
        sql += "        , Sum(Year_2011) AS Year_2011 ";
        sql += "        , Sum(Year_2012) AS Year_2012 ";
        sql += "        , Sum(Year_2013) AS Year_2013 ";
        sql += "        , Sum(Year_2014) AS Year_2014 ";
        sql += "        , Sum(Year_2015) AS Year_2015 ";
        sql += "        , Sum(Year_2016) AS Year_2016 ";
        sql += "        , Sum(Year_2008)+Sum(Year_2009)+Sum(Year_2010)+Sum(Year_2011)+Sum(Year_2012)+Sum(Year_2013)+Sum(Year_2014)+Sum(Year_2015)+Sum(Year_2016) AS Year_total ";
        sql += " From ( ";
        sql += "        Select  highway_id";
        sql += "                , Case When Year(date) = 2008 Then 1 Else 0 End AS Year_2008 ";
        sql += "                , Case When Year(date) = 2009 Then 1 Else 0 End AS Year_2009 ";
        sql += "                , Case When Year(date) = 2010 Then 1 Else 0 End AS Year_2010 ";
        sql += "                , Case When Year(date) = 2011 Then 1 Else 0 End AS Year_2011 ";
        sql += "                , Case When Year(date) = 2012 Then 1 Else 0 End AS Year_2012 ";
        sql += "                , Case When Year(date) = 2013 Then 1 Else 0 End AS Year_2013 ";
        sql += "                , Case When Year(date) = 2014 Then 1 Else 0 End AS Year_2014 ";
        sql += "                , Case When Year(date) = 2015 Then 1 Else 0 End AS Year_2015 ";
        sql += "                , Case When Year(date) = 2016 Then 1 Else 0 End AS Year_2016 ";
        sql += "        From Roadkill_new ";

        if (!value_animal.Equals("ALL"))
        {
            sql += " Where animal = '" + value_animal + "' ";
        }
        
        sql += " ) tmp ";
        sql += " Group By highway_id ";
        sql += " Order By highway_id ";
        

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand = sql;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            //string Json_Data = "[{\"articles\":[[2008,100],[2009,242],[2010,709],[2011,1061],[2012,874],[2013,673],[2014,64],[2015,0]],\"total\":3623,\"name\":\"國1\"},{\"articles\":[[2008,20],[2009,13],[2010,28],[2011,49],[2012,33],[2013,47],[2014,5],[2015,0]],\"total\":175,\"name\":\"國10\"},{\"articles\":[[2008,30],[2009,11],[2010,20],[2011,22],[2012,32],[2013,21],[2014,0],[2015,0]],\"total\":106,\"name\":\"國2\"},{\"articles\":[[2008,40],[2009,342],[2010,712],[2011,751],[2012,958],[2013,947],[2014,200],[2015,0]],\"total\":3910,\"name\":\"國3\"},{\"articles\":[[2008,50],[2009,4],[2010,5],[2011,8],[2012,23],[2013,21],[2014,2],[2015,0]],\"total\":63,\"name\":\"國3甲\"},{\"articles\":[[2008,60],[2009,5],[2010,28],[2011,32],[2012,9],[2013,12],[2014,0],[2015,0]],\"total\":86,\"name\":\"國4\"},{\"articles\":[[2008,0],[2009,2],[2010,2],[2011,40],[2012,40],[2013,36],[2014,4],[2015,0]],\"total\":124,\"name\":\"國5\"},{\"articles\":[[2008,70],[2009,0],[2010,0],[2011,2],[2012,0],[2013,0],[2014,0],[2015,0]],\"total\":2,\"name\":\"國6\"},{\"articles\":[[2008,80],[2009,3],[2010,1],[2011,6],[2012,21],[2013,19],[2014,10],[2015,0]],\"total\":60,\"name\":\"國8\"}]";
            string Json_Data = "";
            Json_Data = "[";
            while (reader.Read())
            {
                Json_Data += "{\"articles\":[[2008," + reader["Year_2008"].ToString()+"],";
                Json_Data += "[2009," + reader["Year_2009"].ToString() + "],";
                Json_Data += "[2010," + reader["Year_2010"].ToString() + "],";
                Json_Data += "[2011," + reader["Year_2011"].ToString() + "],";
                Json_Data += "[2012," + reader["Year_2012"].ToString() + "],";
                Json_Data += "[2013," + reader["Year_2013"].ToString() + "],";
                Json_Data += "[2014," + reader["Year_2014"].ToString() + "],";
                Json_Data += "[2015," + reader["Year_2015"].ToString() + "],";
                Json_Data += "[2016," + reader["Year_2016"].ToString() + "]],";
                Json_Data += "\"total\":" + reader["Year_total"].ToString() + ",";
                //Json_Data += "\"name\":\"" + reader["highway_id"].ToString() + "\"},";
                Json_Data += "\"name\":\"國道"+ reader["highway_id"].ToString()+"號\"},";
                
            }
            Json_Data += "]";
            Json_Data = Json_Data.Replace(",]","]");

            //using (StreamWriter sw = new StreamWriter(@"C:\freeway2\Scripts\journals\data.json"))
            using (StreamWriter sw = new StreamWriter(@"C:\freeway2\Scripts\journals\data.json"))
            {
                sw.Write(Json_Data);
            }

        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }
}