using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_Eco_report_edit1 : System.Web.UI.Page
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
        string sql = " SELECT [id], [plan_name], [plan_id], [type], CONVERT (char(10), [start_date], 126) AS [start_date], CONVERT (char(10), [end_date], 126) AS [end_date], [owner], [provider], [location], [keywords], [abstracts] FROM [Eco_report] ";
        string value_query = TextBox1.Text.ToString();

        if (!value_query.Equals(""))
        {
            sql += " Where plan_name like '%' + @value_query + '%'";
            sql += " or plan_id like '%' + @value_query + '%'";
            sql += " or type like '%' + @value_query + '%'";
            sql += " or start_date like '%' + @value_query + '%'";
            sql += " or end_date like '%' + @value_query + '%'";
            sql += " or owner like '%' + @value_query + '%'";
            sql += " or provider like '%' + @value_query + '%'";
            sql += " or location like '%' + @value_query + '%'";
            sql += " or keywords like '%' + @value_query + '%'";
            sql += " or abstracts like '%' + @value_query + '%'";
        }

        SqlDataSource1.SelectCommand = sql.ToString();
	SqlDataSource1.SelectParameters.Add("value_query",value_query);
        GridView_List.DataBind();
    }
}