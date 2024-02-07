using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_Plants_edit1 : System.Web.UI.Page
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
        string sql = " SELECT [PlantId], [Segment], [Highway_Name], [Direction], [Start], [End], [Plant_Name], [Plant_Number], [Plant_Loca], [Date], Case When Img <> '' Then '../Attachments/Plants/'+Img Else '' End As Img, Case When Img='' Then 'no data' Else '' End As image_memo FROM [Plants] where 1=1 ";
        string role = Session["role"].ToString();
        string uid = Session["uid"].ToString();
        string value_keywords = Keywords.Text;

	SqlDataSource1.SelectParameters.Clear();
        if (role.Equals("2"))
        {
            sql += " and uid = @uid";
	    SqlDataSource1.SelectParameters.Add("uid",uid);
        }

        if (!value_keywords.Equals(""))
        {
            sql += " and ( Office Like '%' + @value_keywords + '%' ";
            sql += "    or Segment Like '%' + @value_keywords + '%' ";
            sql += "    or Date Like '%' + @value_keywords + '%' ";
            sql += "    or Highway_Name Like '%' + @value_keywords + '%' ";
            sql += "    or Direction Like '%' + @value_keywords + '%' ";
            sql += "    or Start Like '%' + @value_keywords + '%' ";
            sql += "    or [End] Like '%' + @value_keywords + '%' ";
            sql += "    or Plant_Name Like '%' + @value_keywords + '%' ";
            sql += "    or Plant_Loca Like '%' + @value_keywords + '%' ";
            sql += "    or Interchange Like '%' + @value_keywords + '%' ";
            sql += "    or Note Like '%' + @value_keywords + '%' ";
            sql += "    or Color Like '%' + @value_keywords + '%' ";
            sql += "    or LifeStyle Like '%' + @value_keywords + '%' ";
            //sql += "    or deduce_species Like '%' + @value_keywords + '%' ";
            //sql += "    or transfer Like '%' + @value_keywords + '%' ";
            //sql += "    or note Like '%' + @value_keywords + '%' ";
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
        for (int i = 1; i <= GridView_List.PageCount; i++)
        {
            uxGoToPageDropDownList.Items.Add(new ListItem(i.ToString(), (i - 1).ToString()));
        }
	if (GridView_List.PageCount > 0)
        uxGoToPageDropDownList.SelectedIndex = GridView_List.PageIndex;
    }
}