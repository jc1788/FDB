using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class View_fun02_012 : System.Web.UI.Page
{
    public string map_url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string x = Request.QueryString[1].ToString();
        string y = Request.QueryString[2].ToString();
        string lo = Request.QueryString[3].ToString();

        //if (!x.Equals("") & !y.Equals(""))
        //{
        map_url = "https://" + Request.Url.Authority + Request.Url.AbsolutePath.ToString();
        map_url = map_url.Replace("fun03_011c__.aspx", "gmap1.aspx");
        map_url += "?x=" + x + "&y=" + y + "&lo=" + lo;
        //}
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        //string id = Request.QueryString[0].ToString();
        //aaa = "資料編號 : 5 " + "\r\n" + "工務段 : 中壢";
        //TextBox1.Text = id;
        Get_DetailData();
    }

    protected void Get_DetailData()
    {
        string value_id = Request.QueryString[0].ToString();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        //string detail_data = "";
        string sqlCommand_NameFull = " SELECT segment,highway_name,Direction,start,[end],pointx,pointy,plant_name,plant_number,plant_loca,interchange,note,lifestyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,case when Img <>'' then ('../Attachments/Plants/'+Img) else '' end as Img,case when FinishImg <>'' then ('../Attachments/Plants/'+FinishImg) else '' end as FinishImg from plants WHERE plantid =  '" + value_id + "'";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    plant_name.Text    = "物種名稱 :  " + reader["plant_name"].ToString();
                    segment.Text       = "工務段 :  " + reader["segment"].ToString();
                    highway_id.Text    = "國道編號 : " + reader["highway_name"].ToString();
                    direction.Text     = "方向 : " + reader["Direction"].ToString();
                    start.Text         = "起始里程 :  " + getFormat(reader["start"].ToString(), "f1") + " K";
                    end.Text           = "結束里程 : " + getFormat(reader["end"].ToString(), "f1") + " K";
                    x.Text             = "經度X : " + reader["pointx"].ToString();
                    y.Text             = "緯度Y : " + reader["pointy"].ToString();
                    plant_number.Text  = "數量 : " + reader["plant_number"].ToString();
                    //interchange.Text   = "交流道 : " + reader["interchange"].ToString();
                    lifestyle.Text     = "型態 : " + reader["lifestyle"].ToString();
                    florescence.Text   = "花期 : " + reader["florescence"].ToString();
		    flowercolor.Text   = "花色 : " + reader["FlowerColor"].ToString();
		    fruitperiod.Text   = "果期 : " + reader["FruitPeriod"].ToString();
		    leafcolor.Text     = "葉色 : " + reader["LeafColor"].ToString();
		    leafperiod.Text    = "葉色轉變期 : " + reader["LeafPeriod"].ToString();
		    plant_loca.Text    = "位置 : " + reader["plant_loca"].ToString();
                    note.Text          = "備註 : " + reader["note"].ToString();
                    //species.Text        = "可能種類 : " + reader["species"].ToString();
                    //deduce_species.Text = "可能種類推測 : " + reader["deduce_species"].ToString();
                    //transfer.Text       = "轉送 : " + reader["transfer"].ToString();
                    //TM2_X.Text          = "TM2_X : " + reader["TM2_X"].ToString();
                    //TM2_Y.Text          = "TM2_Y : " + reader["TM2_Y"].ToString();

                    //string value_path1 = reader["path1"].ToString();
                    //string value_file1 = reader["file1"].ToString();
                    //string value_file2 = reader["file2"].ToString();
                    //string value_file3 = reader["file3"].ToString();
                    string value_file4 = reader["finishimg"].ToString();
                    string value_file5 = reader["img"].ToString();

                    //if (!value_file1.Equals(""))
                    //{
                    //    file1.ImageUrl = "~" + value_path1 + value_file1;
                    //    file1.OnClientClick = "window.open('.." + value_path1 + value_file1 + "')";
                    //    file1.Visible = true;
                    //    //ImageButton1.ImageUrl = "~" + value_path1 + value_file1;
                    //}
                    //else
                    //{
                    //    file1.Visible = false;
                    //}

                    //if (!value_file2.Equals(""))
                    //{
                    //    file2.ImageUrl = ".." + value_path1 + value_file2;
                    //    file2.OnClientClick = "window.open('.." + value_path1 + value_file2 + "')";
                    //    file2.Visible = true;
                    //}
                    //else
                    //{
                    //    file2.Visible = false;
                    //}

                    //if (!value_file3.Equals(""))
                    //{
                    //    file3.ImageUrl = "~" + value_path1 + value_file3;
                    //    file3.OnClientClick = "window.open('.." + value_path1 + value_file3 + "')";
                    //    file3.Visible = true;
                    //}
                    //else
                    //{
                    //    file3.Visible = false;
                    //}

                    //if (!value_file4.Equals(""))
                    //{
                    //    file4.ImageUrl = "~" + value_path1 + value_file4;
                    //    file4.OnClientClick = "window.open('.." + value_path1 + value_file4 + "')";
                    //    file1.Visible = true;
                    //}
                    //else
                    //{
                    //    file4.Visible = false;
                    //}

                    if (!value_file5.Equals(""))
                    {
                        file5.ImageUrl = value_file5;
                        file5.OnClientClick = "window.open('" + value_file5 + "')";
                        file5.Visible = true;
                    }
                    else
                    {
                        file5.Visible = false;
                    }
                    if (!value_file4.Equals(""))
                    {
                        file4.ImageUrl = value_file4;
                        file4.OnClientClick = "window.open('" + value_file5 + "')";
                        file4.Visible = true;
                    }
                    else
                    {
                        file4.Visible = false;
                    }
                    /*
                    detail_data = detail_data + "資料編號 : " + reader["id"].ToString() + "\r\n";
                    detail_data = detail_data + "工務段 :  " + reader["site_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "國道編號 : " + reader["highway_id"].ToString() + "\r\n";
                    detail_data = detail_data + "方向 : " + reader["direction"].ToString() + "\r\n";
                    detail_data = detail_data + "里程 : " + reader["milestone"].ToString().Replace("0000","") + "\r\n";
                    detail_data = detail_data + "經度X : " + reader["x"].ToString() + "\r\n";
                    detail_data = detail_data + "緯度Y : " + reader["y"].ToString() + "\r\n";
                    detail_data = detail_data + "日期 : " + reader["date"].ToString() + "\r\n";
                    detail_data = detail_data + "範圍 : " + reader["range"].ToString() + "\r\n";
                    detail_data = detail_data + "工作類別 : " + reader["type"].ToString() + "\r\n";
                    detail_data = detail_data + "天氣 : " + reader["weather"].ToString() + "\r\n";
                    detail_data = detail_data + "大類 : " + reader["animal"].ToString() + "\r\n";
                    detail_data = detail_data + "詳細類群 : " + reader["detail_animal"].ToString() + "\r\n";
                    detail_data = detail_data + "調查者 : " + reader["collecter_ch"].ToString() + "\r\n";
                    detail_data = detail_data + "可能種類 : " + reader["species"].ToString() + "\r\n";
                    detail_data = detail_data + "可能種類推測 : " + reader["deduce_species"].ToString() + "\r\n";
                    detail_data = detail_data + "轉送 : " + reader["transfer"].ToString() + "\r\n";
                    detail_data = detail_data + "備註 : " + reader["note"].ToString() + "\r\n";
                    detail_data = detail_data + "TM2_X : " + reader["TM2_X"].ToString() + "\r\n";
                    detail_data = detail_data + "TM2_Y : " + reader["TM2_Y"].ToString() + "\r\n";
                    */
                }
            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();


    }

    private string getFormat(string text, string foramt)
    {
        try
        {
            double d = double.Parse(text);
            return d.ToString(foramt);
        }
        catch
        {
            return text;
        }
    }
}