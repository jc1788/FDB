using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_Road_status_edit1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        //Query();
    }

    protected void Query()
    {
        string sql = " SELECT [id], [highway_id], [direction], [mileage], [location], [sensitive], [status], [institution], CONVERT (char(10), [start_date], 126) AS [start_date] , CONVERT (char(10), [end_date], 126) AS  [end_date], [owner], [mileage1], [mileage2] FROM [Road_status] ";
        string value_query = TextBox1.Text.ToString();

        if (!value_query.Equals(""))
        {
            sql += " Where highway_id like '%' + @value_query + '%'";
            sql += " or direction like '%' + @value_query + '%'";
            sql += " or mileage1 like '%' + @value_query + '%'";
            sql += " or mileage2 like '%' + @value_query + '%'";
            sql += " or location like '%' + @value_query + '%'";
            sql += " or sensitive like '%' + @value_query + '%'";
            sql += " or status like '%' + @value_query + '%'";
            sql += " or institution like '%' + @value_query + '%'";
            sql += " or start_date like '%' + @value_query + '%'";
            sql += " or end_date like '%' + @value_query + '%'";
            sql += " or owner like '%' + @value_query + '%'";
	    SqlDataSource1.SelectParameters.Clear();
	    SqlDataSource1.SelectParameters.Add("value_query",value_query);
        }

        SqlDataSource1.SelectCommand = sql.ToString();
        GridView_List.DataBind();
    }
}