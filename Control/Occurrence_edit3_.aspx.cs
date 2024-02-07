using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_Occurrence_edit3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        Query();
    }
    protected void Query()
    {
        string value_sid = Request.QueryString["sid"];

        string sql = " Select sid,sid as id,chinese_name,short_name,[Group],Density,Way_ch,sec_record,Convert(Varchar(10),[date1],126) AS [date1],x,y,inaccuracy,site_city,site_area,site_ch,highway_id,direction,mileage,habit,file1,Collector_ch,plan_name,invest_company,notes From table_1 Where sid = @sid";

        SDS_View.SelectCommand = sql;
	SDS_View.SelectParameters.Add("sid", value_sid);
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

    }
}