using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Control_Environment_edit2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        if (!IsPostBack)
        {
            Get_Data();
        }


        /*
        if (IsPostBack)
        {
            //string value_start_date = start_date.Text;
            //start_date.Text = Request.Form["start_date"].ToString();
            //end_date.Text = Request.Form["end_date"].ToString();
            //string value_end_date = end_date.Text;
            string value_type = type.SelectedValue;
            if (value_type.Equals("環境監測報告"))
            {
                Label_file2.Visible = false;
                Label_file3.Visible = false;
                Label_file4.Visible = false;
                Label_file5.Visible = false;
                Label_file6.Visible = false;
                Label_file7.Visible = false;
                Label_file8.Visible = false;
                Label_file9.Visible = false;
                Label_file10.Visible = false;
                Label_file11.Visible = false;
                Label_file12.Visible = false;
                Label_file13.Visible = false;
                Label_file14.Visible = false;
                Label_file15.Visible = false;

                file2.Visible = false;
                file3.Visible = false;
                file4.Visible = false;
                file5.Visible = false;
                file6.Visible = false;
                file7.Visible = false;
                file8.Visible = false;
                file9.Visible = false;
                file10.Visible = false;
                file11.Visible = false;
                file12.Visible = false;
                file13.Visible = false;
                file14.Visible = false;
                file15.Visible = false;
            }
            else
            {
                Label_file2.Visible = true;
                Label_file3.Visible = true;
                Label_file4.Visible = true;
                Label_file5.Visible = true;
                Label_file6.Visible = true;
                Label_file7.Visible = true;
                Label_file8.Visible = true;
                Label_file9.Visible = true;
                Label_file10.Visible = true;
                Label_file11.Visible = true;
                Label_file12.Visible = true;
                Label_file13.Visible = true;
                Label_file14.Visible = true;
                Label_file15.Visible = true;

                file2.Visible = true;
                file3.Visible = true;
                file4.Visible = true;
                file5.Visible = true;
                file6.Visible = true;
                file7.Visible = true;
                file8.Visible = true;
                file9.Visible = true;
                file10.Visible = true;
                file11.Visible = true;
                file12.Visible = true;
                file13.Visible = true;
                file14.Visible = true;
                file15.Visible = true;
            }
        }
        */

    }

    public void Type_Change(object sender, EventArgs e)
    {
        /*
        string value_type = type.SelectedValue;
        if (value_type.Equals("環境監測報告"))
        {
            Label_file2.Visible = false;
            Label_file3.Visible = false;
            Label_file4.Visible = false;
            Label_file5.Visible = false;
            Label_file6.Visible = false;
            Label_file7.Visible = false;
            Label_file8.Visible = false;
            Label_file9.Visible = false;
            Label_file10.Visible = false;
            Label_file11.Visible = false;
            Label_file12.Visible = false;
            Label_file13.Visible = false;
            Label_file14.Visible = false;
            Label_file15.Visible = false;

            file2.Visible = false;
            file3.Visible = false;
            file4.Visible = false;
            file5.Visible = false;
            file6.Visible = false;
            file7.Visible = false;
            file8.Visible = false;
            file9.Visible = false;
            file10.Visible = false;
            file11.Visible = false;
            file12.Visible = false;
            file13.Visible = false;
            file14.Visible = false;
            file15.Visible = false;
        }
        else
        {
            Label_file2.Visible = true;
            Label_file3.Visible = true;
            Label_file4.Visible = true;
            Label_file5.Visible = true;
            Label_file6.Visible = true;
            Label_file7.Visible = true;
            Label_file8.Visible = true;
            Label_file9.Visible = true;
            Label_file10.Visible = true;
            Label_file11.Visible = true;
            Label_file12.Visible = true;
            Label_file13.Visible = true;
            Label_file14.Visible = true;
            Label_file15.Visible = true;

            file2.Visible = true;
            file3.Visible = true;
            file4.Visible = true;
            file5.Visible = true;
            file6.Visible = true;
            file7.Visible = true;
            file8.Visible = true;
            file9.Visible = true;
            file10.Visible = true;
            file11.Visible = true;
            file12.Visible = true;
            file13.Visible = true;
            file14.Visible = true;
            file15.Visible = true;
        }
        */
    }


    protected void Edit_Environment_Click(object sender, EventArgs e)
    {
        //String filepath = "/Attachments/Environment/";
        //String filepath2 = "D:\\freeway2\\Attachments\\Environment\\";

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand = " Update Environment Set ";

        String value_id = id.Text;
        String value_plan_name = plan_name.Text;
        String value_plan_id = plan_id.Text;
        String value_type = type.SelectedValue;
        //String value_start_date = start_date.SelectedDate.ToString("yyyy-MM-dd");
        String value_start_date = start_date.Text;
        //String value_end_date = end_date.SelectedDate.ToString("yyyy-MM-dd");
        String value_end_date = end_date.Text;
        String value_owner = owner.Text;
        String value_provider = provider.Text;
        String value_location = location.Text;
        String value_keywords = keywords.Text;
        String value_abstracts = abstracts.Text;
        String value_uid = Session["uid"].ToString();

        sqlCommand += " plan_name = '" + value_plan_name + "' , ";
        sqlCommand += " plan_id = '" + value_plan_id + "' , ";
        sqlCommand += " type = '" + value_type + "' , ";
        sqlCommand += " start_date = '" + value_start_date + "' , ";
        sqlCommand += " end_date = '" + value_end_date + "' , ";
        sqlCommand += " owner = '" + value_owner + "' , ";
        sqlCommand += " provider = '" + value_provider + "' , ";
        sqlCommand += " location = '" + value_location + "' , ";
        sqlCommand += " keywords = '" + value_keywords + "' , ";
        sqlCommand += " abstracts = '" + value_abstracts + "' , ";
        sqlCommand += " uid = '" +value_uid + "'  ";


        sqlCommand += " Where id = '" + value_id + "' ";


        /*
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        String value_file2 = file2.FileName;
        String value_path2 = filepath;
        String value_file3 = file3.FileName;
        String value_path3 = filepath;
        String value_file4 = file4.FileName;
        String value_path4 = filepath;
        String value_file5 = file5.FileName;
        String value_path5 = filepath;
        String value_file6 = file6.FileName;
        String value_path6 = filepath;
        String value_file7 = file7.FileName;
        String value_path7 = filepath;
        String value_file8 = file8.FileName;
        String value_path8 = filepath;
        String value_file9 = file9.FileName;
        String value_path9 = filepath;
        String value_file10 = file10.FileName;
        String value_path10 = filepath;
        String value_file11 = file11.FileName;
        String value_path11 = filepath;
        String value_file12 = file12.FileName;
        String value_path12 = filepath;
        String value_file13 = file13.FileName;
        String value_path13 = filepath;
        String value_file14 = file14.FileName;
        String value_path14 = filepath;
        String value_file15 = file15.FileName;
        String value_path15 = filepath;
        */



        


        /*
        if (value_type.Equals("環境監測報告"))
        {
            sqlCommand = "Insert Into Environment(plan_name,plan_id,type,start_date,end_date,owner,provider,location,keywords,abstracts,file1,path1,uid) values(";
        }
        sqlCommand += "'" + value_plan_name + "',";
        sqlCommand += "'" + value_plan_id + "',";
        sqlCommand += "'" + value_type + "',";
        sqlCommand += "'" + value_start_date + "',";
        sqlCommand += "'" + value_end_date + "',";
        sqlCommand += "'" + value_owner + "',";
        sqlCommand += "'" + value_provider + "',";
        sqlCommand += "'" + value_location + "',";
        sqlCommand += "'" + value_keywords + "',";
        sqlCommand += "'" + value_abstracts + "',";
        sqlCommand += "'" + value_file1 + "',";
        if (value_type.Equals("環境監測報告"))
        {
            sqlCommand += "'" + value_path1 + "',";
            sqlCommand += "'" + value_uid + "')";
        }
        else
        {

            sqlCommand += "'" + value_path1 + "',";
            sqlCommand += "'" + value_file2 + "',";
            sqlCommand += "'" + value_path2 + "',";
            sqlCommand += "'" + value_file3 + "',";
            sqlCommand += "'" + value_path3 + "',";
            sqlCommand += "'" + value_file4 + "',";
            sqlCommand += "'" + value_path4 + "',";
            sqlCommand += "'" + value_file5 + "',";
            sqlCommand += "'" + value_path5 + "',";
            sqlCommand += "'" + value_file6 + "',";
            sqlCommand += "'" + value_path6 + "',";
            sqlCommand += "'" + value_file7 + "',";
            sqlCommand += "'" + value_path7 + "',";
            sqlCommand += "'" + value_file8 + "',";
            sqlCommand += "'" + value_path8 + "',";
            sqlCommand += "'" + value_file9 + "',";
            sqlCommand += "'" + value_path9 + "',";
            sqlCommand += "'" + value_file10 + "',";
            sqlCommand += "'" + value_path10 + "',";
            sqlCommand += "'" + value_file11 + "',";
            sqlCommand += "'" + value_path11 + "',";
            sqlCommand += "'" + value_file12 + "',";
            sqlCommand += "'" + value_path12 + "',";
            sqlCommand += "'" + value_file13 + "',";
            sqlCommand += "'" + value_path13 + "',";
            sqlCommand += "'" + value_file14 + "',";
            sqlCommand += "'" + value_path14 + "',";
            sqlCommand += "'" + value_file15 + "',";
            sqlCommand += "'" + value_path15 + "',";
            sqlCommand += "'" + value_uid + "')";
        }
        */


        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            /*
            //處理檔案上傳
            if (file1.HasFile)
            {
                file1.SaveAs(filepath2 + value_file1);
            }
            if (file2.HasFile)
            {
                file2.SaveAs(filepath2 + value_file2);
            }
            if (file3.HasFile)
            {
                file3.SaveAs(filepath2 + value_file3);
            }
            if (file4.HasFile)
            {
                file4.SaveAs(filepath2 + value_file4);
            }
            if (file5.HasFile)
            {
                file5.SaveAs(filepath2 + value_file5);
            }
            if (file6.HasFile)
            {
                file6.SaveAs(filepath2 + value_file6);
            }
            if (file7.HasFile)
            {
                file7.SaveAs(filepath2 + value_file7);
            }
            if (file8.HasFile)
            {
                file8.SaveAs(filepath2 + value_file8);
            }
            if (file9.HasFile)
            {
                file9.SaveAs(filepath2 + value_file9);
            }
            if (file10.HasFile)
            {
                file10.SaveAs(filepath2 + value_file10);
            }
            if (file11.HasFile)
            {
                file11.SaveAs(filepath2 + value_file11);
            }
            if (file12.HasFile)
            {
                file12.SaveAs(filepath2 + value_file12);
            }
            if (file13.HasFile)
            {
                file13.SaveAs(filepath2 + value_file13);
            }
            if (file14.HasFile)
            {
                file14.SaveAs(filepath2 + value_file14);
            }
            if (file15.HasFile)
            {
                file15.SaveAs(filepath2 + value_file15);
            }
            */
            Response.Write("<script>alert('修改環評調查報告完成!!')</script>");
            Response.Redirect("Environment_edit1_.aspx");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
    }

    protected void Get_Data()
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_id = Request.QueryString["id"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = '" + value_id + "' ";

        string sql_GetData = " Select plan_name , plan_id , type ,  CONVERT(CHAR(10), start_date, 120) AS start_date , CONVERT(CHAR(10), end_date, 120) AS end_date , owner , provider , location , keywords , abstracts From Environment Where id = '" + value_id + "' ";
        id.Text = value_id;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sql_GetData, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    plan_name.Text = reader["plan_name"].ToString();
                    plan_id.Text = reader["plan_id"].ToString();
                    type.Text = reader["type"].ToString();
                    start_date.Text = reader["start_date"].ToString();
                    end_date.Text = reader["end_date"].ToString();
                    owner.Text = reader["owner"].ToString();
                    provider.Text = reader["provider"].ToString();
                    location.Text = reader["location"].ToString();
                    keywords.Text = reader["keywords"].ToString();
                    abstracts.Text = reader["abstracts"].ToString();
 
                    /*
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
                    */

                    //direction.Text = reader["direction"].ToString();



                    
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
}