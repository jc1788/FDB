using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class plant_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

	if (Request.QueryString["id"] == null || Request.QueryString["id"] == "")
	{
	    Response.Redirect("plant_search.aspx");
	}

	if (!IsPostBack) SetContent();
    }

    private void SetContent()
    {
	string value_id = Request.QueryString["id"];
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    DataTable dt_highway = new DataTable();
	    SqlDataAdapter apt = new SqlDataAdapter("SELECT * FROM [Highway]", conn);
	    apt.Fill(dt_highway);
	    Highway_Name.DataSource = dt_highway;
	    Highway_Name.DataBind();

	    string sql = "Select * From Plants Where Plantid = @value_id";
	    if (Session["role"].ToString() != "1")
		sql += " and uid = @uid";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("value_id", value_id);
	    cmd.Parameters.AddWithValue("uid", Session["uid"]);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
	    {
		    Office.Text = reader["Office"].ToString();
                    Segment.Text = reader["Segment"].ToString();

                    string db_highway_id = reader["Highway_Name"].ToString();
                    string db_direction = reader["Direction"].ToString();
                    string sel_direction = "";
                    
                    Highway_Name.SelectedValue = db_highway_id;
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

                    Direction.Text = sel_direction;
                    start1.Text = reader["Start1"].ToString();
                    start2.Text = reader["Start2"].ToString();
                    end1.Text = reader["End1"].ToString();
                    end2.Text = reader["End2"].ToString();
                    PointY.Text = reader["PointY"].ToString();
                    PointX.Text = reader["PointX"].ToString();
                    PointYE.Text = reader["PointYE"].ToString();
                    PointXE.Text = reader["PointXE"].ToString();
                    date.Text = reader["date"].ToString();
                    Plant_Name.Text = reader["Plant_Name"].ToString();
                    Plant_Number.Text = reader["Plant_Number"].ToString();
                    Unit2.Text = reader["Unit2"].ToString();
                    SpecificationTall.Text = reader["SpecificationTall"].ToString();
                    SpecificationCrown.Text = reader["SpecificationCrown"].ToString();
                    SpecificationMeter.Text = reader["SpecificationMeter"].ToString();
                    LifeStyle.Text = reader["LifeStyle"].ToString();
                    florescence.Text = reader["florescence"].ToString();
                    FlowerColor.Text = reader["FlowerColor"].ToString();
                    FruitPeriod.Text = reader["FruitPeriod"].ToString();
                    LeafColor.Text = reader["LeafColor"].ToString();
                    LeafPeriod.Text = reader["LeafPeriod"].ToString();
                    FruitPeriod.Text = reader["FruitPeriod"].ToString();
                    Plant_Loca.Text = reader["Plant_Loca"].ToString();
                    Note.Text = reader["Note"].ToString();

	    	conn.Close();
		if (PointY.Text == "" || PointX.Text == "")
		    Get_XY2();
		if (PointYE.Text == "" || PointXE.Text == "")
		    Get_XYE();
	    }
	    else
	    {
	    	conn.Close();
		Response.Redirect("plant_search.aspx");
	    }
	}
    }

    private void Get_XY2()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
	String value_highway_id = Highway_Name.SelectedValue;
        String value_range = start1.Text;
        String value_direction = Direction.Text;
        string value_direction1 = "";
        String value_milestone = start2.Text;
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

        sqlCommand_NameFull += " and free_id = @value_highway_id and direction = @value_direction1 and mileage = @query_mileage ";

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
                    PointX.Text = reader["x"].ToString();
                    PointY.Text = reader["y"].ToString();

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


                    //string sql = " Select Distinct animal , species From Roadkill_new Where 1=1 ";
                    //sql = sql + " and x >= " + value_x_down;
                    //sql = sql + " and x <= " + value_x_up;
                    //sql = sql + " and y >= " + value_y_down;
                    //sql = sql + " and y <= " + value_y_up;

                    //GridView_List.PageSize = 500;
                    //SDS_View.SelectCommand = sql.ToString();
                    //GridView_List.DataBind();

                }
            }
            else
            {
                PointX.Text = "0.0";
                PointY.Text = "0.0";
            }
        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }

    protected void Get_XYE()
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_highway_id = Highway_Name.SelectedValue;
        String value_range = end1.Text;
        String value_direction = Direction.Text;
        string value_direction1 = "";
        String value_milestone = end2.Text;
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

        sqlCommand_NameFull += " and free_id = @value_highway_id and direction = @value_direction1 and mileage = @query_mileage ";

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
                    PointXE.Text = reader["x"].ToString();
                    PointYE.Text = reader["y"].ToString();

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


                    //string sql = " Select Distinct animal , species From Roadkill_new Where 1=1 ";
                    //sql = sql + " and x >= " + value_x_down;
                    //sql = sql + " and x <= " + value_x_up;
                    //sql = sql + " and y >= " + value_y_down;
                    //sql = sql + " and y <= " + value_y_up;

                    //GridView_List.PageSize = 500;
                    //SDS_View.SelectCommand = sql.ToString();
                    //GridView_List.DataBind();

                }
            }
            else
            {
                PointXE.Text = sqlCommand_NameFull;
                PointYE.Text = "0.0";
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
	Get_XYE();
    }

    protected void Edit_Plant_Click(object sender, EventArgs e)
    {
        string value_id = Request.QueryString["id"];
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = "Select plantid From plants Where plantid = @id";
	    if (Session["role"].ToString() != "1")
		sql += " and uid = @uid";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("id", value_id);
	    cmd.Parameters.AddWithValue("uid", Session["uid"]);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (!reader.HasRows)
	    {
		Response.Redirect("plant_search.aspx");
		conn.Close();
	    }

	    reader.Close();

        string value_Office = Office.Text;
        string value_Segment = Segment.Text;
        string value_Highway_Name = Highway_Name.SelectedValue;
        string value_start1 = start1.Text;
        string value_start2 = start2.Text;
        string value_End1 = end1.Text;
        string value_End2 = end2.Text;
        string value_start = value_start1 + "." + value_start2;
        string value_End = value_End1 + "." + value_End2;
        string value_direction = Direction.Text;
        string value_PointX = PointX.Text;
        string value_pointY = PointY.Text;
        string value_PointXE = PointXE.Text;
        string value_pointYE = PointYE.Text;
        string value_date = date.Text;
        string value_Plant_Name = Plant_Name.Text;
        string value_Plant_Number = Plant_Number.Text;
        string value_Unit2 = Unit2.Text;
        string value_SpecificationTall = SpecificationTall.Text;
        string value_SpecificationCrown = SpecificationCrown.Text;
        string value_SpecificationMeter = SpecificationMeter.Text;
        string value_LifeStyle = LifeStyle.Text;
        string value_florescence = florescence.Text;
        string value_FlowerColor = FlowerColor.Text;
        string value_FruitPeriod = FruitPeriod.Text;
        string value_LeafColor = LeafColor.Text;
        string value_LeafPeriod = LeafPeriod.Text;
        string value_Plant_Loca = Plant_Loca.Text;
        string value_Note = Note.Text;

        string sqlCommand = " Update Plants Set ";

        sqlCommand += " Office = @value_Office,";
        sqlCommand += " Segment = @value_Segment,";
        sqlCommand += " Direction = @value_direction,";
        sqlCommand += " Highway_Name = @value_Highway_Name,";
        sqlCommand += " Start1 = @value_start1,";
        sqlCommand += " Start2 = @value_start2,";
        sqlCommand += " End1 = @value_End1,";
        sqlCommand += " End2 = @value_End2,";
        sqlCommand += " Start = @value_start,";
        sqlCommand += " [End] = @value_End,";
        sqlCommand += " PointX = @value_PointX,";
        sqlCommand += " pointY = @value_pointY,";
        sqlCommand += " PointXE = @value_PointXE,";
        sqlCommand += " pointYE = @value_pointYE,";
        sqlCommand += " Date = @value_date,";
        sqlCommand += " Plant_Name = @value_Plant_Name,";
        sqlCommand += " Plant_Number = @value_Plant_Number,";
        sqlCommand += " Unit2 = @value_Unit2,";
        sqlCommand += " SpecificationTall = @value_SpecificationTall,";
        sqlCommand += " SpecificationCrown = @value_SpecificationCrown,";
        sqlCommand += " SpecificationMeter = @value_SpecificationMeter,";
        sqlCommand += " LifeStyle = @value_LifeStyle,";
        sqlCommand += " florescence = @value_florescence,";
        sqlCommand += " FlowerColor = @value_FlowerColor,";
        sqlCommand += " FruitPeriod = @value_FruitPeriod,";
        sqlCommand += " LeafColor = @value_LeafColor,";
        sqlCommand += " LeafPeriod = @value_LeafPeriod,";
        sqlCommand += " Plant_Loca = @value_Plant_Loca,";

        sqlCommand += " Note = @value_Note";
        sqlCommand += " Where Plantid = @value_id";

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_Office",value_Office);
	    cmd.Parameters.AddWithValue("value_Segment",value_Segment);
	    cmd.Parameters.AddWithValue("value_direction",value_direction);
	    cmd.Parameters.AddWithValue("value_Highway_Name",value_Highway_Name);
	    cmd.Parameters.AddWithValue("value_start1",value_start1);
	    cmd.Parameters.AddWithValue("value_start2",value_start2);
	    cmd.Parameters.AddWithValue("value_End1",value_End1);
	    cmd.Parameters.AddWithValue("value_End2",value_End2);
	    cmd.Parameters.AddWithValue("value_start",value_start);
	    cmd.Parameters.AddWithValue("value_End",value_End);
	    cmd.Parameters.AddWithValue("value_PointX",value_PointX);
	    cmd.Parameters.AddWithValue("value_pointY",value_pointY);
	    cmd.Parameters.AddWithValue("value_PointXE",value_PointXE);
	    cmd.Parameters.AddWithValue("value_pointYE",value_pointYE);
	    cmd.Parameters.AddWithValue("value_date",value_date);
	    cmd.Parameters.AddWithValue("value_Plant_Name",value_Plant_Name);
	    cmd.Parameters.AddWithValue("value_Plant_Number",value_Plant_Number);
	    cmd.Parameters.AddWithValue("value_Unit2",value_Unit2);
	    cmd.Parameters.AddWithValue("value_SpecificationTall",value_SpecificationTall);
	    cmd.Parameters.AddWithValue("value_SpecificationCrown",value_SpecificationCrown);
	    cmd.Parameters.AddWithValue("value_SpecificationMeter",value_SpecificationMeter);
	    cmd.Parameters.AddWithValue("value_LifeStyle",value_LifeStyle);
	    cmd.Parameters.AddWithValue("value_florescence",value_florescence);
	    cmd.Parameters.AddWithValue("value_FlowerColor",value_FlowerColor);
	    cmd.Parameters.AddWithValue("value_FruitPeriod",value_FruitPeriod);
	    cmd.Parameters.AddWithValue("value_LeafColor",value_LeafColor);
	    cmd.Parameters.AddWithValue("value_LeafPeriod",value_LeafPeriod);
	    cmd.Parameters.AddWithValue("value_Plant_Loca",value_Plant_Loca);
	    cmd.Parameters.AddWithValue("value_Note",value_Note);
	    cmd.Parameters.AddWithValue("value_id",value_id);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();
	    conn.Close();

            Response.Write("<script>alert('修改資料完成!');window.close(); </script>");
	}
    }
}
