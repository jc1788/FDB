using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Roadkill_add1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
    }

    protected void Get_XY(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_highway_id = highway_id.SelectedValue;
        String value_range = range1.Text;
        String value_direction = direction.Text;
        String value_milestone = milestone.Text;
        int query_mileage = (int.Parse(value_range) * 1000) + Convert.ToInt16(Math.Round((double.Parse(value_milestone) / 100) + 0.049, 0)) * 100;

        string value_direction1 = "";

        if ((value_highway_id.Equals("1") || value_highway_id.Equals("3") || value_highway_id.Equals("5") || value_highway_id.Equals("7") || value_highway_id.Equals("9")) && (value_direction.Equals("R") || value_direction.Equals("")))
        {
            value_direction1 = "N";
        }
        else if ((value_highway_id.Equals("2") || value_highway_id.Equals("4") || value_highway_id.Equals("6") || value_highway_id.Equals("8") || value_highway_id.Equals("10") || value_highway_id.Equals("3甲")) && (value_direction.Equals("R") || value_direction.Equals("")))
        {
            value_direction1 = "E";
        }
        else
        {
            value_direction1 = value_direction;
        }

        String sqlCommand_NameFull = " Select WGS84X x , WGS84Y y , Cast(Round(TMD67X,0) AS Int) AS TM2_X , Cast(Round(TMD67Y,0) AS Int) AS TM2_Y From Freeway_new Where 1=1 ";

        sqlCommand_NameFull += " and free_id = @value_highway_id  and direction = @value_direction1  and mileage = @query_mileage  ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction1",value_direction1);
	    cmd.Parameters.AddWithValue("query_mileage",query_mileage);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string str_x = reader["x"].ToString();
                    string str_y = reader["y"].ToString();
                    x.Text = reader["x"].ToString();
                    y.Text = reader["y"].ToString();
                    TM2_X.Text = reader["TM2_X"].ToString();
                    TM2_Y.Text = reader["TM2_Y"].ToString();

                    double value_x = 0;
                    double value_x_up = 0;
                    double value_x_down = 0;
                    try
                    {
                        value_x = double.Parse(str_x);
                        value_x_up = value_x + 0.15;
                        value_x_down = value_x - 0.15;
                    }
                    catch
                    {
                    }

                    double value_y = 0;
                    double value_y_up = 0;
                    double value_y_down = 0;
                    try
                    {
                        value_y = double.Parse(str_y);
                        value_y_up = value_y + 0.15;
                        value_y_down = value_y - 0.15;
                    }
                    catch
                    {
                    }


                    string sql = " Select Distinct animal , species From Roadkill_new Where 1=1 ";
                    sql = sql + " and x >= @value_x_down";
                    sql = sql + " and x <= @value_x_up";
                    sql = sql + " and y >= @value_y_down";
                    sql = sql + " and y <= @value_y_up";

                    float up_milestone = (float.Parse(query_mileage.ToString()) / 1000) + 1;
                    float down_milestone = (float.Parse(query_mileage.ToString()) / 1000) - 1;

                    //string history_sql = " Select Distinct Replace(Round(milestone,2),'0000','') As milestone , animal , detail_animal , animal2 , species , deduce_species From Roadkill_new Where highway_id = @value_highway_id  and milestone Between @down_milestone and @up_milestone Order By milestone , animal , detail_animal , animal2 , species , deduce_species ";
                    string history_sql = " Select Distinct Replace(Round(milestone,2),'0000','') As milestone , animal , detail_animal , animal2 , species , deduce_species From Roadkill_new Where highway_id = @value_highway_id  and milestone Between " + down_milestone + " and " + up_milestone + " Order By milestone , animal , detail_animal , animal2 , species , deduce_species ";

		    SDS_View.SelectParameters.Clear();
		    SDS_View.SelectParameters.Add("value_highway_id",value_highway_id);
		    //SDS_View.SelectParameters.Add("down_milestone",TypeCode.Single,down_milestone.ToString());
		    //SDS_View.SelectParameters.Add("up_milestone",TypeCode.Single,up_milestone.ToString());
                    SDS_View.SelectCommand = history_sql;
                    GridView_List.DataBind();
                    GridView_List.PageSize = 2000;
                    /*
                    GridView_List.PageSize = 500;
                    SDS_View.SelectCommand = sql.ToString();
                    GridView_List.DataBind();
                    */
                }
            }
            else
            {
                x.Text = "0.0";
                y.Text = "0.0";
                TM2_X.Text = "0";
                TM2_Y.Text = "0";
            }
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    //新增道路致死
    protected void Add_Roadkill_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand = "Insert Into Roadkill(site_ch,highway_id,direction,milestone,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,animal2,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid) values(";

        String value_site_ch = site_ch.Text;
        String value_highway_id = highway_id.SelectedValue;
        String value_direction = direction.Text;
        String value_range1 = range1.Text;
        String value_milestone = milestone.Text;
        String value_milestone1 = value_range1 + "." + value_milestone;
        int query_mileage1 = (int.Parse(value_range1) * 1000) + Convert.ToInt16(Math.Round((float.Parse(value_milestone) / 100) + 0.049, 0)) * 100;
        decimal value_x = decimal.Parse(x.Text);
        decimal value_y = decimal.Parse(y.Text);
        int value_TM2_X = int.Parse(TM2_X.Text.Replace(".0000", ""));
        int value_TM2_Y = int.Parse(TM2_Y.Text.Replace(".0000", ""));
        String value_date = date.Text;
        String value_range = range.Text;
        String value_type = type.Text;
        String value_weather = weather.Text;
        String value_animal = animal.SelectedValue; ;
        String value_detail_animal = detail_animal.SelectedValue;
        String value_animal2 = animal.SelectedValue;
        String value_Collector_ch = Collector_ch.Text;
        String value_species = species.Text;
        String value_deduce_species = deduce_species.Text;
        String value_transfer = transfer.Text;
        String value_note = note.Text;
        String value_uid = Session["uid"].ToString();
        //String value_path1 = "/App_Data/Roadkill/";
        //String filepath2 = "D:\\freeway2\\App_Data\\Roadkill\\";
        String value_path1 = "/Attachments/Roadkill/";
        String filepath2 = "D:\\freeway2\\Attachments\\Roadkill\\";
        //String value_file1 = file1.FileName;
        string value_file1 = "";
        string value_file2 = "";
        string value_file3 = "";
        string value_file4 = "";
        string value_file5 = "";
        int filecount = 1;
        String File_file="";
        if (file1.HasFile)
        {
            foreach (HttpPostedFile postedFile in file1.PostedFiles)
            {
                if (filecount == 1)
                    value_file1 = postedFile.FileName;
                else if (filecount == 2)
                    value_file2 = postedFile.FileName;
                else if (filecount == 3)
                    value_file3 = postedFile.FileName;
                else if (filecount == 4)
                    value_file4 = postedFile.FileName;
                else if (filecount == 5)
                    value_file5 = postedFile.FileName;
                else
                    break;
                File_file = filepath2 + postedFile.FileName;
                postedFile.SaveAs(File_file);
                filecount++;
            }
        }

        sqlCommand += "@value_site_ch,";
        sqlCommand += "@value_highway_id,";
        sqlCommand += "@value_direction,";
        sqlCommand += "@value_milestone1,";

        //若經緯度皆為0的話代表抓里程判斷
        if (value_x.Equals(0) && value_y.Equals(0) && value_TM2_X.Equals(0) && value_TM2_Y.Equals(0))
        {
            sqlCommand += " (Select Top 1 WGS84X From Freeway_new Where free_id = @value_highway_id  and direction = @value_direction  and mileage = @query_mileage1 ) ,";
            sqlCommand += " (Select Top 1 WGS84Y From Freeway_new Where free_id = @value_highway_id  and direction = @value_direction  and mileage = @query_mileage1 ) ,";
            sqlCommand += " (Select Top 1 TMD67X From Freeway_new Where free_id = @value_highway_id  and direction = @value_direction  and mileage = @query_mileage1 ) ,";
            sqlCommand += " (Select Top 1 TMD67Y From Freeway_new Where free_id = @value_highway_id  and direction = @value_direction  and mileage = @query_mileage1 ) ,";
        }
        else
        {
            sqlCommand += "@value_x,";
            sqlCommand += "@value_y,";
            sqlCommand += "@value_TM2_X,";
            sqlCommand += "@value_TM2_Y,";
        }
        sqlCommand += "@value_date,";
        sqlCommand += "@value_range,";
        sqlCommand += "@value_type,";
        sqlCommand += "@value_weather,";
        sqlCommand += "@value_animal,";
        sqlCommand += "@value_detail_animal,";
        sqlCommand += "@value_animal2,";
        sqlCommand += "@value_Collector_ch,";
        sqlCommand += "@value_species,";
        sqlCommand += "@value_deduce_species,";
        sqlCommand += "@value_file1,";
        sqlCommand += "@value_file2,";
        sqlCommand += "@value_file3,";
        sqlCommand += "@value_file4,";
        sqlCommand += "@value_file5,";
        sqlCommand += "@value_path1,";
        sqlCommand += "@value_transfer,";
        sqlCommand += "@value_note,";
        sqlCommand += "@value_uid)";

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_site_ch",value_site_ch);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
	    cmd.Parameters.AddWithValue("value_milestone1",value_milestone1);
        if (value_x.Equals(0) && value_y.Equals(0) && value_TM2_X.Equals(0) && value_TM2_Y.Equals(0))
        {
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
	    cmd.Parameters.AddWithValue("query_mileage1",query_mileage1);
	} else {
	    cmd.Parameters.AddWithValue("value_x",value_x);
	    cmd.Parameters.AddWithValue("value_y",value_y);
	    cmd.Parameters.AddWithValue("value_TM2_X",value_TM2_X);
	    cmd.Parameters.AddWithValue("value_TM2_Y",value_TM2_Y);
	}
	    cmd.Parameters.AddWithValue("value_date",value_date);
	    cmd.Parameters.AddWithValue("value_range",value_range);
	    cmd.Parameters.AddWithValue("value_type",value_type);
	    cmd.Parameters.AddWithValue("value_weather",value_weather);
	    cmd.Parameters.AddWithValue("value_animal",value_animal);
	    cmd.Parameters.AddWithValue("value_detail_animal",value_detail_animal);
	    cmd.Parameters.AddWithValue("value_animal2",value_animal2);
	    cmd.Parameters.AddWithValue("value_Collector_ch",value_Collector_ch);
	    cmd.Parameters.AddWithValue("value_species",value_species);
	    cmd.Parameters.AddWithValue("value_deduce_species",value_deduce_species);
	    cmd.Parameters.AddWithValue("value_file1",value_file1);
	    cmd.Parameters.AddWithValue("value_file2",value_file2);
	    cmd.Parameters.AddWithValue("value_file3",value_file3);
	    cmd.Parameters.AddWithValue("value_file4",value_file4);
	    cmd.Parameters.AddWithValue("value_file5",value_file5);
	    cmd.Parameters.AddWithValue("value_path1",value_path1);
	    cmd.Parameters.AddWithValue("value_transfer",value_transfer);
	    cmd.Parameters.AddWithValue("value_note",value_note);
	    cmd.Parameters.AddWithValue("value_uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //處理檔案上傳
            //if (file1.HasFile)
            //{
            //    String File_file1 = filepath2 + value_file1;
            //    file1.SaveAs(File_file1);
            //}
            Response.Write("<script>alert('新增道路致死資料完成!');location.href='Roadkill_add1_.aspx'; </script>");
            //Response.Write("<script>alert('新增道路致死資料完成!!')</script>");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
	    Response.Write("<script>alert('"+ex.Message+"')</script>");
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

    }
}