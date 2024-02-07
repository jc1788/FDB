using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class roadkill_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	if (!IsPostBack) SetContent();
    }

    private void SetContent()
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataTable dt_highway = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM [Highway]", conn);
	    apt.Fill(dt_highway);
	    highway_id.DataSource = dt_highway;
	    highway_id.DataBind();

	    conn.Close();
	}
    }

    private void Get_XY2()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_highway_id = highway_id.SelectedValue;
        String value_range = range1.Text;
        String value_direction = direction.Text;
        string value_direction1 = "";
        String value_milestone = milestone.Text;
        int query_mileage = (int.Parse(value_range) * 1000) + Convert.ToInt16(Math.Round((double.Parse(value_milestone) / 100) + 0.049, 0)) * 100;

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

        sqlCommand_NameFull += " and free_id = @value_highway_id and direction = @value_direction1 and mileage = @query_mileage";

       try
       {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_highway_id",value_highway_id);
	    cmd.Parameters.AddWithValue("value_direction1",value_direction1);
	    cmd.Parameters.AddWithValue("query_mileage",query_mileage);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                    string str_x = reader["x"].ToString();
                    string str_y = reader["y"].ToString();
                    x.Text = reader["x"].ToString();
                    y.Text = reader["y"].ToString();

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
            }
            else
            {
                x.Text = "0.0";
                y.Text = "0.0";
            }
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    protected void Get_XY(object sender, EventArgs e)
    {
	Get_XY2();
    }

    protected void Add_Roadkill_Click(object sender, EventArgs e)
    {
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

        string value_site_ch = site_ch.Text;
        string value_highway_id = highway_id.Text;
        string value_direction = direction.Text;
        string value_range1 = range1.Text;
        string value_milestone = milestone.Text;
        string value_milestone1 = value_range1 + "." + value_milestone;
        int query_mileage1 = (int.Parse(value_range1) * 1000) + Convert.ToInt16(Math.Round((float.Parse(value_milestone) / 100) + 0.049, 0)) * 100;
        decimal value_x = (x.Text == "") ? 0 : decimal.Parse(x.Text);
        decimal value_y = (y.Text == "") ? 0 : decimal.Parse(y.Text);
        int value_TM2_X = int.Parse(TM2_X.Text.Replace(".0000", ""));
        int value_TM2_Y = int.Parse(TM2_Y.Text.Replace(".0000", ""));
        string value_date = date.Text;
        string value_range = range.Text;
        string value_type = type.Text;
        string value_weather = weather.Text;
        string value_animal = animal.Text; ;
        string value_detail_animal = detail_animal.Text;
        string value_animal2 = animal2.Text; ;
        string value_Collector_ch = Collector_ch.Text;
        string value_species = species.Text;
        string value_deduce_species = deduce_species.Text;
        string value_transfer = transfer.Text;
        string value_note = note.Text;
	string value_uid = Session["uid"].ToString();
        string value_path1 = "/Attachments/Roadkill/";
	string filepath2 = "D:\\freeway2\\Attachments\\Roadkill\\";
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

	string sqlCommand = "Insert Into Roadkill(site_ch,highway_id,direction,milestone,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,animal2,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid) values(";

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
            // 建立 SqlCommand 
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
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

            Response.Write("<script>alert('新增道路致死資料完成!');window.close(); </script>");
	}
	catch (Exception ex)
	{
	    Response.Write("<script>alert('"+ex.Message+"')</script>");
	    System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
	}

	    conn.Close();
	}
    }
}
