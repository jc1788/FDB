using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class View_fun01_0131 : System.Web.UI.Page
{
    //public string ScientificName = "";
    //public string accept_name_code = "";
    public string map_url = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string x = Request.QueryString[3].ToString();
        string y = Request.QueryString[4].ToString();
        string lo = Request.QueryString[5].ToString();
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun01_0131_.aspx", "gmap1.aspx");
        map_url += "?x=" + x + "&y=" + y + "&lo=" + lo;

        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        //Get_DetailData();
        Get_DetailData2();
    }


    protected void Get_DetailData()
    {
        string value_siteid = Request.QueryString[0].ToString();
        string value_Short_Name = Request.QueryString[1].ToString();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        //string detail_data = "";
        //string sqlCommand_NameFull = " Select TOP 1 b.siteid , b.x , b.y , b.inaccuracy , b.TM2_X , b.TM2_Y , CONVERT (char(10), b.date1, 126) AS date , b.site_en , b.site_ch , b.highway_id , a.Chinese_name , a.full_name as ScientificName , a.Density , a.Collector , a.Collector_ch , a.Way_ch , a.[Group] , a.sec_record , a.habit From occurrence_new a , site_new b Where a.siteid = b.siteid  and b.siteid = '" + value_siteid + "' and a.Short_Name  = '" + value_Short_Name + "'";
        string sqlCommand_NameFull = " Select TOP 1 sid , x , y , inaccuracy , TM2_X , TM2_Y , CONVERT (char(10), date1, 126) AS date , site_ch , highway_id , Chinese_name , full_name as ScientificName , Density , Collector_ch , Way_ch , [Group] , sec_record , habit From table_1 where (siteid = '" + value_siteid + "' or sid = '" + value_siteid + "') and Short_Name  = '" + value_Short_Name + "'";
        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    siteid.Text = "時間地點代號 : " + reader["siteid"].ToString();
                    x.Text = "經度 :  " + reader["x"].ToString();
                    y.Text = "緯度 : " + reader["y"].ToString();
                    inaccuracy.Text = "座標不確定值(公尺) : " + reader["inaccuracy"].ToString();
                    TM2_X.Text = "TM2_X : " + reader["TM2_X"].ToString().Replace("0000", "");
                    TM2_Y.Text = "TM2_Y : " + reader["TM2_Y"].ToString().Replace("0000", "");
                    date.Text = "調查日期 : " + reader["date"].ToString();
                    //site_en.Text = "調查地(英文) : " + reader["site_en"].ToString();
                    site_ch.Text = "調查地(中文) : " + reader["site_ch"].ToString();
                    highway_id.Text = "國道編號 : " + reader["highway_id"].ToString();
                    Chinese_name.Text = "物種中文名 : " + reader["Chinese_name"].ToString();
                    ScientificName.Text = "物種學名 : " + reader["ScientificName"].ToString();
                    Density.Text = "個體數(面積/密度) : " + reader["Density"].ToString();
                    //Collector.Text = "調查者英文名 : " + reader["Collector"].ToString();
                    Collector_ch.Text = "調查者中文名 : " + reader["Collector_ch"].ToString();
                    Way_ch.Text = "調查方法 : " + reader["Way_ch"].ToString();
                    Group.Text = "物種類群 : " + reader["Group"].ToString();
                    sec_record.Text = "間接紀錄 : " + reader["sec_record"].ToString();
                    habit.Text = "棲地類型 : " + reader["habit"].ToString();

                    
                    /*
                    detail_data = detail_data + "時間地點代號 : " + reader["siteid"].ToString() + "\r\n";
                    detail_data = detail_data + "經度 :  " + reader["x"].ToString() + "\r\n";
                    detail_data = detail_data + "緯度 : " + reader["y"].ToString() + "\r\n";
                    detail_data = detail_data + "座標不確定值(公尺) : " + reader["inaccuracy"].ToString() + "\r\n";
                    detail_data = detail_data + "TM2_X : " + reader["TM2_X"].ToString().Replace("0000", "") + "\r\n";
                    detail_data = detail_data + "TM2_Y : " + reader["TM2_Y"].ToString() + "\r\n";
                    detail_data = detail_data + "調查日期 : " + reader["date"].ToString() + "\r\n";
                    detail_data = detail_data + "調查地(英文) : " + reader["site_en"].ToString() + "\r\n";
                    detail_data = detail_data + "調查地(中文) : " + reader["site_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "國道編號 : " + reader["highway_id"].ToString() + "\r\n";
                    detail_data = detail_data + "物種中文名 : " + reader["Chinese_name"].ToString() + "\r\n";
                    detail_data = detail_data + "物種學名 : " + reader["ScientificName"].ToString() + "\r\n";
                    detail_data = detail_data + "個體數(面積/密度) : " + reader["Density"].ToString() + "\r\n";
                    detail_data = detail_data + "調查者英文名 : " + reader["Collector"].ToString() + "\r\n";
                    detail_data = detail_data + "調查者中文名 : " + reader["Collector_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "調查方法 : " + reader["Way_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "物種類群 : " + reader["Group"].ToString() + "\r\n";
                    detail_data = detail_data + "間接紀錄 : " + reader["sec_record"].ToString() + "\r\n";
                    detail_data = detail_data + "棲地類型 : " + reader["habit"].ToString() + "\r\n";
                    */
                }
            }
            //TextBox1.Text = detail_data;
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
        //TextBox1.Text = detail_data;
        //TextBox2.Text = detail_data;
    }

    protected void Get_DetailData2()
    {
        string value_siteid = Request.QueryString[0].ToString();
        string value_Short_Name = Request.QueryString[1].ToString();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        //string detail_data = "";
        string sqlCommand_NameFull = " Select TOP 1 sid , x , y , inaccuracy , TM2_X , TM2_Y , CONVERT (char(10), date1, 126) AS date , site_ch , highway_id , Chinese_name , full_name as ScientificName , Density , Collector_ch , Way_ch , [Group] , sec_record , habit From table_1 where (siteid = '" + value_siteid + "' or sid = '" + value_siteid + "') and Short_Name  = '" + value_Short_Name + "'";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    siteid.Text = "時間地點代號 : " + reader["sid"].ToString();
                    x.Text = "經度 :  " + reader["x"].ToString();
                    y.Text = "緯度 : " + reader["y"].ToString();
                    inaccuracy.Text = "座標不確定值(公尺) : " + reader["inaccuracy"].ToString();
                    TM2_X.Text = "TM2_X : " + reader["TM2_X"].ToString().Replace("0000", "");
                    TM2_Y.Text = "TM2_Y : " + reader["TM2_Y"].ToString().Replace("0000", "");
                    date.Text = "調查日期 : " + reader["date"].ToString();
                    //site_en.Text = "調查地(英文) : " + reader["site_en"].ToString();
                    site_ch.Text = "調查地(中文) : " + reader["site_ch"].ToString();
                    highway_id.Text = "國道編號 : " + reader["highway_id"].ToString();
                    Chinese_name.Text = "物種中文名 : " + reader["Chinese_name"].ToString();
                    ScientificName.Text = "物種學名 : " + reader["ScientificName"].ToString();
                    Density.Text = "個體數(面積/密度) : " + reader["Density"].ToString();
                    //Collector.Text = "調查者英文名 : " + reader["Collector"].ToString();
                    Collector_ch.Text = "調查者中文名 : " + reader["Collector_ch"].ToString();
                    Way_ch.Text = "調查方法 : " + reader["Way_ch"].ToString();
                    Group.Text = "物種類群 : " + reader["Group"].ToString();
                    sec_record.Text = "間接紀錄 : " + reader["sec_record"].ToString();
                    habit.Text = "棲地類型 : " + reader["habit"].ToString();


                    /*
                    detail_data = detail_data + "時間地點代號 : " + reader["siteid"].ToString() + "\r\n";
                    detail_data = detail_data + "經度 :  " + reader["x"].ToString() + "\r\n";
                    detail_data = detail_data + "緯度 : " + reader["y"].ToString() + "\r\n";
                    detail_data = detail_data + "座標不確定值(公尺) : " + reader["inaccuracy"].ToString() + "\r\n";
                    detail_data = detail_data + "TM2_X : " + reader["TM2_X"].ToString().Replace("0000", "") + "\r\n";
                    detail_data = detail_data + "TM2_Y : " + reader["TM2_Y"].ToString() + "\r\n";
                    detail_data = detail_data + "調查日期 : " + reader["date"].ToString() + "\r\n";
                    detail_data = detail_data + "調查地(英文) : " + reader["site_en"].ToString() + "\r\n";
                    detail_data = detail_data + "調查地(中文) : " + reader["site_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "國道編號 : " + reader["highway_id"].ToString() + "\r\n";
                    detail_data = detail_data + "物種中文名 : " + reader["Chinese_name"].ToString() + "\r\n";
                    detail_data = detail_data + "物種學名 : " + reader["ScientificName"].ToString() + "\r\n";
                    detail_data = detail_data + "個體數(面積/密度) : " + reader["Density"].ToString() + "\r\n";
                    detail_data = detail_data + "調查者英文名 : " + reader["Collector"].ToString() + "\r\n";
                    detail_data = detail_data + "調查者中文名 : " + reader["Collector_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "調查方法 : " + reader["Way_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "物種類群 : " + reader["Group"].ToString() + "\r\n";
                    detail_data = detail_data + "間接紀錄 : " + reader["sec_record"].ToString() + "\r\n";
                    detail_data = detail_data + "棲地類型 : " + reader["habit"].ToString() + "\r\n";
                    */
                }
            }
            //TextBox1.Text = detail_data;
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
        //TextBox1.Text = detail_data;
        //TextBox2.Text = detail_data;
    }

    protected void LoadPict(string url)
    {
        try
        {
            string html = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
            if (isSuccess)
            {
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    html = reader.ReadToEnd();
                    html = html.Replace(@"""", "");
                    string[] parts = html.Split('{');
                    if (parts.Length > 1)
                    {
                        Response.Write("<table>");
                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (i % 3 == 1)
                            {
                                Response.Write("<tr>");
                            }

                            string s = parts[i].ToString();
                            string[] s1 = s.Split(',');
                            Response.Write("<td>");
                            for (int j = 0; j < s1.Length; j++)
                            {

                                string[] s2 = s1[j].Split(':');
                                for (int m = 0; m < s2.Length; m++)
                                {

                                    if (s2[m].ToString().Equals("url"))
                                    {
                                        string htmstring = s1[j];
                                        htmstring = htmstring.Replace("url:", "<img src='");
                                        htmstring = htmstring.Replace(".jpg", ".jpg'>");
                                        htmstring = htmstring.Replace("}", "");
                                        htmstring = htmstring.Replace("{", "");
                                        htmstring = htmstring.Replace("]", "");
                                        htmstring = htmstring.Replace("[", "");
                                        htmstring = htmstring.Replace("\\", "");
                                        Response.Write(htmstring + "<br/>");
                                    }
                                    if (s2[m].ToString().Equals("author") || s2[m].ToString().Equals("license"))
                                    {
                                        string htmstring = s1[j];
                                        htmstring = htmstring.Replace("}", "");
                                        htmstring = htmstring.Replace(",", "'>");
                                        htmstring = htmstring.Replace("]", "");
                                        htmstring = htmstring.Replace("\\", "");
                                        Response.Write(htmstring + "<br/>");
                                    }
                                }

                            }
                            Response.Write("</td>");
                            if ((i == 0) || (i % 3 == 0))
                            {
                                Response.Write("</tr>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("查無此物種圖檔資料!");
                    }
                    Response.Write("</table>");
                }
            }
        }
        catch (Exception)
        {
        }
    }


    protected void LoadDescription(string url)
    {
        try
        {
            //string html = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
            if (isSuccess)
            {
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {
                    string html = reader.ReadToEnd();


                    html = html.Replace(@"""", "");
                    string[] parts = html.Split('{');
                    if (parts.Length > 1)
                    {
                        for (int i = 0; i < parts.Length; i++)
                        {
                            string s = parts[i].ToString();
                            string[] s1 = s.Split(',');

                            for (int j = 0; j < s1.Length; j++)
                            {
                                string[] s2 = s1[j].Split(':');
                                for (int m = 0; m < s2.Length; m++)
                                {
                                    if (s2[m].ToString().Equals("description") || s2[m].ToString().Equals("author") || s2[m].ToString().Equals("license"))
                                    {
                                        string ss = s1[j].Replace("}", "");
                                        //ss = ss.Replace("author", "作者");
                                        //ss = ss.Replace("description", "描述");
                                        //ss = ss.Replace("license", "版權");
                                        Response.Write(ss + "<br/><br/>");

                                        //if (j == 8)
                                        //{
                                        //string htmstring = s2[m+1].ToString();
                                        //htmstring = htmstring.Replace("description:", "");
                                        //htmstring = htmstring.Replace(",", "");
                                        //htmstring = htmstring.Replace("\\", @"\");

                                        /*取消unicode
                                        string dst = "";
                                        string src = htmstring;
                                        int len = htmstring.Length / 6;

                                        for (int k = 0; k <= len - 1; k++)
                                        {
                                            string str = "";
                                            str = src.Substring(0, 6).Substring(2);
                                            src = src.Substring(6);
                                            byte[] bytes = new byte[2];
                                            bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
                                            bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
                                            dst += Encoding.Unicode.GetString(bytes);
                                        }

                                        Response.Write("" + dst + "&nbsp;");
                                         */


                                        //Response.Write("" + htmstring + "<br/>");
                                        //Response.Write("圖片來源：TaiBNET"+"<br/>");

                                        //}
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Write("查無此物種說明資料!");
                    }
                    //Response.Write(html);
                    //PrintCPBytes(html, 1252);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value);
        int i = 0;
        //Make the selected menu item reflect the correct imageurl
        for (i = 0; i <= Menu1.Items.Count - 1; i++)
        {
            /*
            if (i == e.Item.Value)
            {
                Menu1.Items(i).ImageUrl = "selectedtab.gif";
            }
            else
            {
                Menu1.Items(i).ImageUrl = "unselectedtab.gif";
            }
             */
        }
    }
}