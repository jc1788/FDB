using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class plant_detail : System.Web.UI.Page
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
        string value_id = Request.QueryString[0].ToString();
	using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString))
	{
	    conn.Open();

	    string sql = " SELECT segment,highway_name,Direction,start,[end],pointx,pointy,plant_name,plant_number,plant_loca,interchange,note,lifestyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,case when Img <>'' then ('../Attachments/Plants/'+Img) else '' end as Img,case when FinishImg <>'' then ('../Attachments/Plants/'+FinishImg) else '' end as FinishImg from plants WHERE plantid = @id";
	    SqlCommand cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("id", value_id);
	    SqlDataReader reader = cmd.ExecuteReader();

	    if (reader.Read())
	    {
		plant_name.Text = reader["plant_name"].ToString();
		segment.Text = reader["segment"].ToString();
		highway_name.Text = reader["highway_name"].ToString();
		direction.Text = reader["direction"].ToString();
		start.Text = reader["start"].ToString();
		end.Text = reader["end"].ToString();
		x.Text = reader["pointx"].ToString();
		y.Text = reader["pointy"].ToString();
		plant_number.Text = reader["plant_number"].ToString();
		lifestyle.Text = reader["lifestyle"].ToString();
		florescence.Text = reader["florescence"].ToString();
		flowercolor.Text = reader["flowercolor"].ToString();
		fruitperiod.Text = reader["fruitperiod"].ToString();
		leafcolor.Text = reader["leafcolor"].ToString();
		leafperiod.Text = reader["leafperiod"].ToString();
		plant_loca.Text = reader["plant_loca"].ToString();
		note.Text = reader["note"].ToString();
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
