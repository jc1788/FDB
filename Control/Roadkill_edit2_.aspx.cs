using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Control_Roadkill_edit2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        int value_id = int.Parse(Request.QueryString["id"]);

        if (!IsPostBack)
        {
            Get_Data();
        }
    }

    protected void Get_Data()
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_id = Request.QueryString["id"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = @value_id ";

        string sqlCommand_NameFull = "Select * , case when milestone >=0 and milestone <1000 then 0 when milestone >=1000 and milestone < 10000 then left(milestone,1) when milestone >=10000 and milestone < 100000 then left(milestone,2) when milestone >=100000 and milestone < 1000000 then left(milestone,3) end as m1  ,  right(milestone,3) as n1 From (Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , animal2, collecter_ch , species , deduce_species , transfer , note , Case When file1 <> '' then path1+file1 Else '' End As origan_file1 , Case When file2 <> '' then path1+file2 Else '' End As origan_file2 , Case When file3 <> '' then path1+file3 Else '' End As origan_file3 , Case When file4 <> '' then path1+file4 Else '' End As origan_file4 , Case When file5 <> '' then path1+file5 Else '' End As origan_file5 From Roadkill Where id = @value_id) a ";
        id.Text = value_id;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
	    cmd.Parameters.AddWithValue("value_id",value_id);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string db_highway_id = reader["highway_id"].ToString();
                    string db_direction = reader["direction"].ToString();
                    string sel_direction = "";

                    site_ch.Text = reader["site_ch"].ToString();
                    //highway_id.Text = reader["highway_id"].ToString();
                    highway_id.Text = db_highway_id;
                    //直向國道預設N
                    if ((db_highway_id.Equals("1") || db_highway_id.Equals("3") || db_highway_id.Equals("5") || db_highway_id.Equals("7") || db_highway_id.Equals("9")) && (!db_direction.Equals("N") && !db_direction.Equals("S") && !db_direction.Equals("R")))
                    {
                        sel_direction = "N";
                    }
                    //橫向國道預設E
                    else if ((db_highway_id.Equals("2") || db_highway_id.Equals("4") || db_highway_id.Equals("6") || db_highway_id.Equals("8") || db_highway_id.Equals("10") || db_highway_id.Equals("3甲")) && (!db_direction.Equals("E") && !db_direction.Equals("W") && !db_direction.Equals("R")))
                    {
                        sel_direction = "E";
                    }
                    else
                    {
                        sel_direction = db_direction.ToString();
                    }

                    direction.Text = sel_direction;


                    //direction.Text = reader["direction"].ToString();



                    //milestone.Text = reader["milestone"].ToString();
                    range1.Text = reader["m1"].ToString();
                    milestone.Text = reader["n1"].ToString();
                    x.Text = reader["x"].ToString();
                    y.Text = reader["y"].ToString();
                    date.Text = reader["date"].ToString();
                    range.Text = reader["range"].ToString();
                    type.Text = reader["type"].ToString();
                    weather.Text = reader["weather"].ToString();
                    animal.Text = reader["animal"].ToString();
                    detail_animal.Text = reader["detail_animal"].ToString();
                    animal2.Text = reader["animal2"].ToString();
                    Collector_ch.Text = reader["collecter_ch"].ToString();
                    species.Text = reader["species"].ToString();
                    deduce_species.Text = reader["deduce_species"].ToString();
                    transfer.Text = reader["transfer"].ToString();
                    note.Text = reader["note"].ToString();

                    string url = reader["origan_file1"].ToString();
                    if (!url.Equals(""))
                    {
                        origan_file1.ImageUrl = "~" + reader["origan_file1"].ToString();
                        origan_file1.Visible = true;
                    }
                    else
                    {
                        origan_file1.Visible = false;
                    }

                    string ur2 = reader["origan_file2"].ToString();
                    if (!ur2.Equals(""))
                    {
                        origan_file2.ImageUrl = "~" + reader["origan_file2"].ToString();
                        origan_file2.Visible = true;
                    }
                    else
                    {
                        origan_file2.Visible = false;
                    }

                    string ur3 = reader["origan_file3"].ToString();
                    if (!ur3.Equals(""))
                    {
                        origan_file3.ImageUrl = "~" + reader["origan_file3"].ToString();
                        origan_file3.Visible = true;
                    }
                    else
                    {
                        origan_file3.Visible = false;
                    }

                    string ur4 = reader["origan_file4"].ToString();
                    if (!ur4.Equals(""))
                    {
                        origan_file4.ImageUrl = "~" + reader["origan_file4"].ToString();
                        origan_file4.Visible = true;
                    }
                    else
                    {
                        origan_file4.Visible = false;
                    }

                    string ur5 = reader["origan_file5"].ToString();
                    if (!ur5.Equals(""))
                    {
                        origan_file5.ImageUrl = "~" + reader["origan_file5"].ToString();
                        origan_file5.Visible = true;
                    }
                    else
                    {
                        origan_file5.Visible = false;
                    }


                    //origan_file1.ImageUrl = "~" + reader["origan_file1"].ToString();

                    float up_milestone = float.Parse(reader["m1"].ToString()) + 1;
                    float down_milestone = float.Parse(reader["m1"].ToString()) - 1;

                    string history_sql = " Select Distinct Replace(Round(milestone,2),'0000','') As milestone , animal , detail_animal , animal2 , species , deduce_species From Roadkill_new Where highway_id = '" + db_highway_id + "' and milestone Between " + down_milestone + " and " + up_milestone;
                    SDS_View.SelectCommand = history_sql;
                    GridView_List.DataBind();
                    GridView_List.PageSize = 2000;
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


    protected void Edit_Roadkill_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_site_ch = site_ch.Text;
        string value_highway_id = highway_id.Text;
        string value_direction = direction.Text;
        //string value_milestone = milestone.Text;
        decimal value_x = (x.Text == "") ? 0 : decimal.Parse(x.Text);
        decimal value_y = (y.Text == "") ? 0 : decimal.Parse(y.Text);
        string value_date = date.Text;
        //string value_range = range.Text;
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
        string value_range = range.Text;
        string value_range1 = range1.Text;
        string value_milestone = milestone.Text;
        string value_milestone1 = value_range1 + "." + value_milestone;
        int query_mileage1 = (int.Parse(value_range1) * 1000) + Convert.ToInt16(Math.Round((float.Parse(value_milestone) / 100) + 0.049, 0)) * 100;

        string value_id = Request.QueryString["id"];

        string sqlCommand = " Update Roadkill Set ";

        sqlCommand += " site_ch = @value_site_ch ,";
        sqlCommand += " highway_id = @value_highway_id ,";
        sqlCommand += " direction = @value_direction ,";
        sqlCommand += " milestone = @value_milestone1 ,";
        sqlCommand += (value_x == 0) ? " x = null, " : " x = @value_x ,";
        sqlCommand += (value_y == 0) ? " y = null, " : " y = @value_y ,";
        sqlCommand += " date = @value_date ,";
        sqlCommand += " range = @value_range ,";
        sqlCommand += " type = @value_type ,";
        sqlCommand += " weather = @value_weather ,";
        sqlCommand += " animal = @value_animal ,";
        sqlCommand += " detail_animal = @value_detail_animal ,";
        sqlCommand += " animal2 = @value_animal2 ,";
        sqlCommand += " Collecter_ch = @value_Collector_ch ,";
        sqlCommand += " species = @value_species ,";
        sqlCommand += " deduce_species = @value_deduce_species ,";
        sqlCommand += " transfer = @value_transfer ,";
        sqlCommand += " note = @value_note ";
        sqlCommand += " Where id = @value_id ";


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
	    if (value_x != 0)
	    cmd.Parameters.AddWithValue("value_x",value_x);
	    if (value_y != 0)
	    cmd.Parameters.AddWithValue("value_y",value_y);
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
	    cmd.Parameters.AddWithValue("value_transfer",value_transfer);
	    cmd.Parameters.AddWithValue("value_note",value_note);
	    cmd.Parameters.AddWithValue("value_id",value_id);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //Response.Write("<script>alert('編輯資料完成!!')</script>");
            //Response.Redirect("Roadkill_edit1.aspx");
            Response.Write("<script>alert('修改資料完成!');location.href='Roadkill_edit1_.aspx'; </script>");

            //string msg = ("<script>alert('上傳完成'); location.href='Roadkill_edit1.aspx'; </script>");
            //Response.Write("<script>alert('上傳完成'); location.href='Default.aspx'; </script>");
            //Response.Redirect(msg);
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
    }

    protected void Get_XY(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
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

            if (reader.HasRows)
            {
                while (reader.Read())
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


                    string sql = " Select Distinct animal , species From Roadkill_new Where 1=1 ";
                    sql = sql + " and x >= " + value_x_down;
                    sql = sql + " and x <= " + value_x_up;
                    sql = sql + " and y >= " + value_y_down;
                    sql = sql + " and y <= " + value_y_up;

                    GridView_List.PageSize = 500;
                    SDS_View.SelectCommand = sql.ToString();
                    GridView_List.DataBind();

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
}