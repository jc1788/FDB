using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_Roadkill_edit1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        Query();
        //uxGoToPageDropDownList_DataBinding(sender,e);
    }

    protected void Query()
    {
        string sql = " SELECT [id] , [site_ch], [highway_id],  [direction],Replace(Replace([milestone],'00000',''),'0000','') AS [milestone],  [x], [y], [detail_animal], [animal], [weather], [range], [type], [TM2_Y], [TM2_X], CONVERT (char(10), date, 126) AS [date], [collecter_ch], [species], [deduce_species], [transfer], [note] FROM [Roadkill] Where 1=1 ";
        string role = Session["role"].ToString();
        string uid = Session["uid"].ToString();
        string value_keywords = Keywords.Text;

        SqlDataSource1.SelectParameters.Clear();
        if (!role.Equals("1"))
        {
            sql += " and uid = @uid";
	    SqlDataSource1.SelectParameters.Add("uid",uid);
        }

        if (!value_keywords.Equals("")) {
            sql += " and ( site_ch Like '%' + @value_keywords + '%' ";
            sql += "    or highway_id Like '%' + @value_keywords + '%' ";
            sql += "    or direction Like '%' + @value_keywords + '%' ";
            sql += "    or milestone Like '%' + @value_keywords + '%' ";
            sql += "    or x Like '%' + @value_keywords + '%' ";
            sql += "    or y Like '%' + @value_keywords + '%' ";
            sql += "    or detail_animal Like '%' + @value_keywords + '%' ";
            sql += "    or animal Like '%' + @value_keywords + '%' ";
            sql += "    or weather Like '%' + @value_keywords + '%' ";
            sql += "    or range Like '%' + @value_keywords + '%' ";
            sql += "    or type Like '%' + @value_keywords + '%' ";
            sql += "    or collecter_ch Like '%' + @value_keywords + '%' ";
            sql += "    or species Like '%' + @value_keywords + '%' ";
            sql += "    or deduce_species Like '%' + @value_keywords + '%' ";
            sql += "    or transfer Like '%' + @value_keywords + '%' ";
            sql += "    or note Like '%' + @value_keywords + '%' ";
            sql += " ) ";
            SqlDataSource1.SelectParameters.Add("value_keywords",value_keywords);
        }
        SqlDataSource1.SelectCommand = sql;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;
        int d = 0;
        try
        {
            d = Convert.ToInt32(uxGoToPageDropDownList.SelectedValue);
        }
        catch { 
        
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
        GridView_List.PageIndex = d;
        uxGoToPageDropDownList_DataBinding2();
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
    }
    /*
    public GridView SourceGridView
    {
        get { return (GridView)Parent.Parent.NamingContainer; }
    }
    */
    protected void uxGoToPageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView_List.PageIndex = int.Parse(((DropDownList)sender).SelectedValue);
    }

    protected void uxGoToPageDropDownList_DataBinding(object sender, EventArgs e)
    {
        for (int i = 1; i <= GridView_List.PageCount; i++)
        {
            uxGoToPageDropDownList.Items.Add(new ListItem(i.ToString(), (i - 1).ToString()));
        }
        uxGoToPageDropDownList.SelectedIndex = GridView_List.PageIndex;
    }

    protected void uxGoToPageDropDownList_DataBinding2()
    {
        uxGoToPageDropDownList.Items.Clear();
        for (int i =1; i <= GridView_List.PageCount; i++)
        {
            uxGoToPageDropDownList.Items.Add(new ListItem(i.ToString(), (i - 1).ToString()));
        }
	if (GridView_List.PageCount > 0)
        uxGoToPageDropDownList.SelectedIndex = GridView_List.PageIndex;
    }
}