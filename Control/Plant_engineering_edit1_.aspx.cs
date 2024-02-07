using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_Plant_engineering_edit1 : System.Web.UI.Page
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
        string sql = " SELECT a.[pid], [plant_id], [number], [plant_name], [institution], CONVERT (char(10), [design_date], 126) AS [design_date], CONVERT (char(10), [work_date], 126) AS [work_date], [maintain] , Case when b.cnts is null then 0 else b.cnts End As cnts From [Plant_engineering] a Left Join (Select pid , count(*) cnts From Plant_observation GROUP BY pid) b on a.pid = b.pid ";
        string uid = Session["uid"].ToString();
        string value_keywords = Keywords.Text;

        
        if (!value_keywords.Equals(""))
        {
            sql += " where  a.plant_id Like '%' + @value_keywords + '%' ";
            sql += "    or a.number Like '%' + @value_keywords + '%' ";
            sql += "    or a.plant_name Like '%' + @value_keywords + '%' ";
            sql += "    or a.institution Like '%' + @value_keywords + '%' ";
            sql += "    or a.design_date Like '%' + @value_keywords + '%' ";
            sql += "    or a.work_date Like '%' + @value_keywords + '%' ";
            sql += "    or a.maintain Like '%' + @value_keywords + '%' ";
	    SqlDataSource1.SelectParameters.Clear();            
	    SqlDataSource1.SelectParameters.Add("value_keywords",value_keywords);
        }
        SqlDataSource1.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        //int d = 0;
        try
        {
            //d = Convert.ToInt32(uxGoToPageDropDownList.SelectedValue);
        }
        catch
        {

        }
        //string d = uxGoToPageDropDownList.SelectedValue;

        /*
        if(d!=""){
            GridView_List.PageIndex = Convert.ToInt32(uxGoToPageDropDownList.SelectedValue);
        }
        
        if (!d.Equals(0))
        {
            GridView_List.PageIndex = d;
            uxGoToPageDropDownList_DataBinding2();
        }
        */
        //GridView_List.PageIndex = d;
        //uxGoToPageDropDownList_DataBinding2();
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
    }
}