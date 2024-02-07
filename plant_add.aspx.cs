using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class plant_add : System.Web.UI.Page
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
	    Highway_Name.DataSource = dt_highway;
	    Highway_Name.DataBind();

	    conn.Close();
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

    protected void Add_Plant_Click(object sender, EventArgs e)
    {
//        string value_id = Request.QueryString["id"];
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

        string value_Office = Office.SelectedValue;
        string value_Segment = Segment.SelectedValue;
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

        string sqlCommand = " Insert into Plants (Office,Segment,Direction,Highway_Name,Start1,Start2,End1,End2,Start,[End],PointX,pointY,";
	sqlCommand += "PointXE,pointYE,Date,Plant_Name,Plant_Number,Unit2,SpecificationTall,SpecificationCrown,SpecificationMeter,";
	sqlCommand += "LifeStyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,Plant_Loca,Note) ";
	sqlCommand += "values (@Office,@Segment,@Direction,@Highway_Name,@Start1,@Start2,@End1,@End2,@Start,@End,@PointX,@pointY,";
	sqlCommand += "@PointXE,@pointYE,@Date,@Plant_Name,@Plant_Number,@Unit2,@SpecificationTall,@SpecificationCrown,@SpecificationMeter,";
	sqlCommand += "@LifeStyle,@florescence,@FlowerColor,@FruitPeriod,@LeafColor,@LeafPeriod,@Plant_Loca,@Note) ";
/*
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
*/
            // 建立 SqlCommand 
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("Office",value_Office);
	    cmd.Parameters.AddWithValue("Segment",value_Segment);
	    cmd.Parameters.AddWithValue("direction",value_direction);
	    cmd.Parameters.AddWithValue("Highway_Name",value_Highway_Name);
	    cmd.Parameters.AddWithValue("start1",value_start1);
	    cmd.Parameters.AddWithValue("start2",value_start2);
	    cmd.Parameters.AddWithValue("End1",value_End1);
	    cmd.Parameters.AddWithValue("End2",value_End2);
	    cmd.Parameters.AddWithValue("start",value_start);
	    cmd.Parameters.AddWithValue("End",value_End);
	    cmd.Parameters.AddWithValue("PointX",value_PointX);
	    cmd.Parameters.AddWithValue("pointY",value_pointY);
	    cmd.Parameters.AddWithValue("PointXE",value_PointXE);
	    cmd.Parameters.AddWithValue("pointYE",value_pointYE);
	    cmd.Parameters.AddWithValue("date",value_date);
	    cmd.Parameters.AddWithValue("Plant_Name",value_Plant_Name);
	    cmd.Parameters.AddWithValue("Plant_Number",value_Plant_Number);
	    cmd.Parameters.AddWithValue("Unit2",value_Unit2);
	    cmd.Parameters.AddWithValue("SpecificationTall",value_SpecificationTall);
	    cmd.Parameters.AddWithValue("SpecificationCrown",value_SpecificationCrown);
	    cmd.Parameters.AddWithValue("SpecificationMeter",value_SpecificationMeter);
	    cmd.Parameters.AddWithValue("LifeStyle",value_LifeStyle);
	    cmd.Parameters.AddWithValue("florescence",value_florescence);
	    cmd.Parameters.AddWithValue("FlowerColor",value_FlowerColor);
	    cmd.Parameters.AddWithValue("FruitPeriod",value_FruitPeriod);
	    cmd.Parameters.AddWithValue("LeafColor",value_LeafColor);
	    cmd.Parameters.AddWithValue("LeafPeriod",value_LeafPeriod);
	    cmd.Parameters.AddWithValue("Plant_Loca",value_Plant_Loca);
	    cmd.Parameters.AddWithValue("Note",value_Note);
//	    cmd.Parameters.AddWithValue("value_id",value_id);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();
	    conn.Close();

            Response.Write("<script>alert('修改新增完成!');window.close(); </script>");
	}
    }
}
