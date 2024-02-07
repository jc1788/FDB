using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class roadkill_detail : System.Web.UI.Page
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
	    Response.Redirect("searchall.aspx");
	}

	if (!IsPostBack) SetContent();
    }

    private void SetContent()
    {
	string value_id = Request.QueryString["id"];
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = "SELECT id, site_ch, highway_id, direction, milestone, x, y, detail_animal, animal , animal2 ,weather, type, range , CONVERT (char(10), date, 126) AS date, TM2_Y, TM2_X, collecter_ch, species, deduce_species, transfer, note , varify, path1 , file1 , file2, file3 , file4 , file5 FROM v_Roadkill_new WHERE id = @id";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("id", value_id);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
	    {
		site_ch.Text = reader["site_ch"].ToString();
		highway_id.Text = reader["highway_id"].ToString();
		direction.Text = reader["direction"].ToString();
		milestone.Text = reader["milestone"].ToString().Replace("0000", "");
		x.Text = reader["x"].ToString();
		y.Text = reader["y"].ToString();
		date.Text = reader["date"].ToString();
		range.Text = reader["range"].ToString();
		type.Text = reader["type"].ToString();
		weather.Text = reader["weather"].ToString();
		animal.Text = reader["animal"].ToString();
		animal2.Text = reader["animal2"].ToString();
		detail_animal.Text = reader["detail_animal"].ToString();
		collecter_ch.Text = reader["collecter_ch"].ToString();
		species.Text = reader["species"].ToString();
		deduce_species.Text = reader["deduce_species"].ToString();
		transfer.Text = reader["transfer"].ToString();
		note.Text = reader["note"].ToString();
		varify.Text = reader["varify"].ToString();

                    string value_path1 = reader["path1"].ToString();
                    string value_file1 = reader["file1"].ToString();
                    string value_file2 = reader["file2"].ToString();
                    string value_file3 = reader["file3"].ToString();
                    string value_file4 = reader["file4"].ToString();
                    string value_file5 = reader["file5"].ToString();

                    if (!value_file1.Equals(""))
                    {
                        //file1.ImageUrl = "~" + value_path1 + value_file1;
                        file1.ImageUrl = "https://ecodata.freeway.gov.tw" + value_path1 + value_file1;
                        file1.OnClientClick = "window.open('https://ecodata.freeway.gov.tw" + value_path1 + value_file1 + "')";
                        file1.Visible = true;
                    }
                    else {
                        file1.Visible = false;
                    }

                    if (!value_file2.Equals(""))
                    {
                        //file2.ImageUrl = "~" + value_path1 + value_file2;
                        file2.ImageUrl = "https://ecodata.freeway.gov.tw" + value_path1 + value_file2;
                        file2.OnClientClick = "window.open('https://ecodata.freeway.gov.tw" + value_path1 + value_file2 + "')";
                        file2.Visible = true;
                    }
                    else
                    {
                        file2.Visible = false;
                    }

                    if (!value_file3.Equals(""))
                    {
                        //file3.ImageUrl = "~" + value_path1 + value_file3;
                        file3.ImageUrl = "https://ecodata.freeway.gov.tw" + value_path1 + value_file3;
                        file3.OnClientClick = "window.open('https://ecodata.freeway.gov.tw" + value_path1 + value_file3 + "')";
                        file3.Visible = true;
                    }
                    else
                    {
                        file3.Visible = false;
                    }

                    if (!value_file4.Equals(""))
                    {
                        //file4.ImageUrl = "~" + value_path1 + value_file4;
                        file4.ImageUrl = "https://ecodata.freeway.gov.tw" + value_path1 + value_file4;
                        file4.OnClientClick = "window.open('https://ecodata.freeway.gov.tw" + value_path1 + value_file4 + "')";
                        file1.Visible = true;
                    }
                    else
                    {
                        file4.Visible = false;
                    }

                    if (!value_file5.Equals(""))
                    {
                        //file5.ImageUrl = "~" + value_path1 + value_file5;
                        file5.ImageUrl = "https://ecodata.freeway.gov.tw" + value_path1 + value_file5;
                        file5.OnClientClick = "window.open('https://ecodata.freeway.gov.tw" + value_path1 + value_file5 + "')";
                        file5.Visible = true;
                    }
                    else
                    {
                        file5.Visible = false;
                    }

	    	conn.Close();
	    }
	    else
	    {
	    	conn.Close();
		Response.Redirect("searchall.aspx");
	    }
	}
    }
}
