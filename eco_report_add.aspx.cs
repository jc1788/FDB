using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class eco_report_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("/Account/logout.cshtml");
        }
    }
    protected void Add_Eco_report_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        //String sqlCommand = "Insert Into Eco_report(plan_name,plan_id,type,start_date,end_date,owner,provider,location,keywords,abstracts,file1,path1,file2,path2,file3,path3,file4,path4,file5,path5,file6,path6,file7,path7,file8,path8,file9,path9,file10,path10,file11,path11,file12,path12,file13,path13,file14,path14,file15,path15) values(";
        String sqlCommand = "Insert Into Eco_report(plan_name,plan_id,type,start_date,end_date,owner,provider,location,keywords,abstracts,file1,path1,uid) values(";
        String filepath = "/Attachments/Eco_report/";
        String filepath2 = "D:\\freeway2\\Attachments\\Eco_report\\";

        String value_plan_name = plan_name.Text;
        String value_plan_id = plan_id.Text;
        String value_type = type.SelectedValue;
        //String value_start_date = start_date.SelectedDate.ToString("yyyy-MM-dd");
        //String value_end_date = end_date.SelectedDate.ToString("yyyy-MM-dd");
        String value_start_date = start_date.Text;
        String value_end_date = end_date.Text;
        String value_owner = owner.Text;
        String value_provider = provider.Text;
        String value_location = location.Text;
        String value_keywords = keywords.Text;
        String value_abstracts = abstracts.Text;
        String value_file1 = file1.FileName;
        String value_path1 = filepath;
        String value_uid = Session["uid"].ToString();
        


        sqlCommand += "@value_plan_name,";
        sqlCommand += "@value_plan_id,";
        sqlCommand += "@value_type,";
        sqlCommand += "@value_start_date,";
        sqlCommand += "@value_end_date,";
        sqlCommand += "@value_owner,";
        sqlCommand += "@value_provider,";
        sqlCommand += "@value_location,";
        sqlCommand += "@value_keywords,";
        sqlCommand += "@value_abstracts,";
        sqlCommand += "@value_file1,";
        sqlCommand += "@value_path1,";
        sqlCommand += "@value_uid)";
        //sqlCommand += "@value_path1,";
        

        

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_plan_name",value_plan_name);
	    cmd.Parameters.AddWithValue("value_plan_id",value_plan_id);
	    cmd.Parameters.AddWithValue("value_type",value_type);
	    cmd.Parameters.AddWithValue("value_start_date",value_start_date);
	    cmd.Parameters.AddWithValue("value_end_date",value_end_date);
	    cmd.Parameters.AddWithValue("value_owner",value_owner);
	    cmd.Parameters.AddWithValue("value_provider",value_provider);
	    cmd.Parameters.AddWithValue("value_location",value_location);
	    cmd.Parameters.AddWithValue("value_keywords",value_keywords);
	    cmd.Parameters.AddWithValue("value_abstracts",value_abstracts);
	    cmd.Parameters.AddWithValue("value_file1",value_file1);
	    cmd.Parameters.AddWithValue("value_path1",value_path1);
	    cmd.Parameters.AddWithValue("value_uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            //處理檔案上傳
            if (file1.HasFile)
            {
                file1.SaveAs(filepath2 + value_file1);
            }
            

            Response.Write("<script>alert('新增生態調查報告完成!!');location.href='eco_report.aspx';</script>");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
    }
}
