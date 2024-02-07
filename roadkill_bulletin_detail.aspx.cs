using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class roadkill_bulletin_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }

        Query();
    }

    protected void Query()
    {
        int value_id = int.Parse(Request.QueryString[0]);


        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand_NameFull = " Select a.id , CONVERT (char(10), a.date, 126) AS date , CONVERT(CHAR(12), a.time, 114) AS time , a.highway_id , a.mileage  , a.reporter , a.institution , a.species , a.status , Case When a.file1 <> '' then a.path1+a.file1 Else file1 End file1 , a.path1 , a.owner From Roadkill_bulletin a  Where a.id = '" + value_id + "'";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    date.Text = reader["date"].ToString();
                    time.Text = reader["time"].ToString();
                    highway_id.Text = reader["highway_id"].ToString();
                    mileage.Text = reader["mileage"].ToString();
                    reporter.Text = reader["reporter"].ToString();
                    institution.Text = reader["institution"].ToString();
                    species.Text = reader["species"].ToString();
                    status.Text = reader["status"].ToString();


                    string string_before_img = reader["file1"].ToString();
                    if (string_before_img.Equals(""))
                    {
                        file1.Visible = false;
                    }
                    else
                    {
                        file1.Visible = true;
                        file1.ImageUrl = "~" + reader["file1"].ToString();
                        file1.Height = 600;
                        file1.Width = 800;
                    }


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
