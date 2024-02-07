using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Control_Plants_edit2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        int value_id = int.Parse(Request.QueryString["Plantid"]);

        if (!IsPostBack)
        {
            Get_Data();
        }
    }

    protected void Get_Data()
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_id = Request.QueryString["Plantid"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = @value_id ";
        string sqlCommand_NameFull = "Select * From Plants Where Plantid = @value_id";
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
                    string db_highway_id = reader["Highway_Name"].ToString();
                    string db_direction = reader["Direction"].ToString();
                    string sel_direction = "";

                    Office.Text = reader["Office"].ToString();
                    Segment.Text = reader["Segment"].ToString();
                    Highway_Name.SelectedValue = db_highway_id;
                    //直向國道預設N
                    if ((db_highway_id.Equals("1") || db_highway_id.Equals("3") || db_highway_id.Equals("5") || db_highway_id.Equals("7") || db_highway_id.Equals("9")) && (!db_direction.Equals("N") && !db_direction.Equals("S") && !db_direction.Equals("R")))
                        sel_direction = "N";
                    //橫向國道預設E
                    else if ((db_highway_id.Equals("2") || db_highway_id.Equals("4") || db_highway_id.Equals("6") || db_highway_id.Equals("8") || db_highway_id.Equals("10") || db_highway_id.Equals("3甲")) && (!db_direction.Equals("E") && !db_direction.Equals("W") && !db_direction.Equals("R")))
                        sel_direction = "E";
                    else
                        sel_direction = db_direction.ToString();

                    Direction.Text = sel_direction;
                    start1.Text = reader["Start1"].ToString();
                    start2.Text = reader["Start2"].ToString();
                    End1.Text = reader["End1"].ToString();
                    End2.Text = reader["End2"].ToString();
                    PointX.Text = reader["PointX"].ToString();
                    PointY.Text = reader["PointY"].ToString();
		    if (String.IsNullOrWhiteSpace(PointX.Text) || String.IsNullOrWhiteSpace(PointY.Text) || PointX.Text == "0.0" || PointY.Text == "0.0")
			Get_XY2(db_highway_id);
                    PointXE.Text = reader["PointXE"].ToString();
                    PointYE.Text = reader["PointYE"].ToString();
		    if (String.IsNullOrWhiteSpace(PointXE.Text) || String.IsNullOrWhiteSpace(PointYE.Text) || PointXE.Text == "0.0" || PointYE.Text == "0.0")
			Get_XYE2(db_highway_id);
                    date.Text = reader["Date"].ToString();
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
                    Plant_Loca.Text = reader["Plant_Loca"].ToString();
		    Note.Text = reader["note"].ToString();

                    string url = reader["Img"].ToString();
                    if (!url.Equals(""))
                    {
                        Img.ImageUrl = "../Attachments/Plants/" + reader["Img"].ToString();
                        Img.Visible = true;
                    }
                    else
                        Img.Visible = false;

                    string ur2 = reader["FinishImg"].ToString();
                    if (!ur2.Equals(""))
                    {
                        FinishImg.ImageUrl = "../Attachments/Plants/" + reader["FinishImg"].ToString();
                        FinishImg.Visible = true;
                    }
                    else
                        FinishImg.Visible = false;

                    //float up_milestone = float.Parse(reader["m1"].ToString()) + 1;
                    //float down_milestone = float.Parse(reader["m1"].ToString()) - 1;
                    //string history_sql = " Select Distinct Replace(Round(milestone,2),'0000','') As milestone , animal , detail_animal , animal2 , species , deduce_species From Roadkill_new Where highway_id = @db_highway_id and milestone Between @down_milestone and @up_milestone";
                    //SDS_View.SelectCommand = history_sql;
                    //GridView_List.DataBind();
                    //GridView_List.PageSize = 2000;
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

        string value_Office = Office.Text;
        string value_Segment = Segment.Text;
        string value_Highway_Name = Highway_Name.SelectedValue;
        string value_start1 = start1.Text;
        string value_start2 = start2.Text;
        string value_End1 = End1.Text;
        string value_End2 = End2.Text;
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

        string value_id = Request.QueryString["Plantid"];
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
	string filename1 = "";
	string filename2 = "";
	if (file1.HasFile)
	{
	    string filepath = "D:\\freeway2\\Attachments\\Plants\\Plants_edit2\\";
	    string filename = "I" + System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + file1.FileName;
	    file1.SaveAs(filepath + filename);
	    filename1 = "Plants_edit2/" + filename;
	    sqlCommand += " Img = @filename1,";
	    filepath = "D:\\freeway2\\Attachments\\Plants\\";
	    if (File.Exists(filepath + Img.ImageUrl.Substring(21, Img.ImageUrl.Length-21).Replace('/','\\')))
		File.Delete(filepath + Img.ImageUrl.Substring(21, Img.ImageUrl.Length-21).Replace('/','\\'));
	}
	if (file2.HasFile)
	{
	    string filepath = "D:\\freeway2\\Attachments\\Plants\\Plants_edit2\\";
	    string filename = "F" + System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "") + file2.FileName;
	    file2.SaveAs(filepath + filename);
	    filename2 = "Plants_edit2/" + filename;
	    sqlCommand += " FinishImg = @filename2,";
	    filepath = "D:\\freeway2\\Attachments\\Plants\\";
	    if (File.Exists(filepath + FinishImg.ImageUrl.Substring(21, FinishImg.ImageUrl.Length-21).Replace('/','\\')))
		File.Delete(filepath + FinishImg.ImageUrl.Substring(21, FinishImg.ImageUrl.Length-21).Replace('/','\\'));
	}
        sqlCommand += " Note = @value_Note";

        sqlCommand += " Where Plantid = @value_id";
        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

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
	    if (filename1 != "")
	    cmd.Parameters.AddWithValue("filename1",filename1);
	    if (filename2 != "")
	    cmd.Parameters.AddWithValue("filename2",filename2);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //Response.Write("<script>alert('編輯資料完成!!')</script>");
            //Response.Redirect("Roadkill_edit1.aspx");
            Response.Write("<script>alert('修改資料完成!');location.href='Plants_edit1_.aspx'; </script>");

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

    protected void Get_XY2(string value_highway_id)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
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

    protected void Get_XYE(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_highway_id = Highway_Name.SelectedValue;
        String value_range = End1.Text;
        String value_direction = Direction.Text;
        string value_direction1 = "";
        String value_milestone = End2.Text;
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

    protected void Get_XYE2(string value_highway_id)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        String value_range = End1.Text;
        String value_direction = Direction.Text;
        string value_direction1 = "";
        String value_milestone = End2.Text;
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
}