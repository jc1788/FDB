using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;


public partial class roadkill_bulletin : System.Web.UI.Page
{
    public DataTable dt_excel = new DataTable();

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

        string sql = " Select a.id , CONVERT (char(10), a.date, 126) AS date , CONVERT(CHAR(12), a.time, 114) AS time , a.highway_id , a.mileage  , a.reporter , a.institution , a.species , a.status , Case When a.file1 <> '' then a.path1+a.file1 Else file1 End file1 , a.path1 , a.owner From Roadkill_bulletin a Where 1=1 ";
        
        string value_KeyWords = KeyWords.Text;

        if (!value_KeyWords.Equals(""))
        {
            sql += " And ( a.date Like '%" + value_KeyWords + "%' ";
            sql += "    or a.time Like '%" + value_KeyWords + "%' ";
            sql += "    or a.mileage Like '%" + value_KeyWords + "%' ";
            sql += "    or a.reporter Like '%" + value_KeyWords + "%' ";
            sql += "    or a.institution Like '%" + value_KeyWords + "%' ";
            sql += "    or a.species Like '%" + value_KeyWords + "%' ";
            sql += "    or a.owner Like '%" + value_KeyWords + "%' ";
            sql += "    or a.status Like '%" + value_KeyWords + "%' )";
        }

        SDS_View.SelectCommand = sql;
        //GridView1.DataSource = SDS_View1;
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

        btnShowGVCount_Click1();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;
        // 建立 SqlCommand 
        cmd = new SqlCommand(sql, conn);
        using (SqlDataAdapter a = new SqlDataAdapter(cmd))
        {
            a.Fill(dt_excel);
        }
        int aa = dt_excel.Rows.Count;
        // 執行命令
        //dt_excel = cmd.ExecuteReader().;

        // 清理命令和連線物件。
        cmd.Dispose();
        conn.Dispose();
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
    }

    protected void btnShowGVCount_Click1()
    {
        DataView view = (DataView)SDS_View.Select(DataSourceSelectArguments.Empty);
        int count = view.Count;
        TotalCounts.Text = "總筆數：" + count;
        view.Dispose();
    }
}
