using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_RoadKill_tmp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
        string counts_error = Request.QueryString[0].ToString();
        string counts_ok = Request.QueryString[1].ToString();
        //ok_string.Text = Request.QueryString[0].ToString();
        //string errorLog = Request.QueryString[2].ToString();
        ok_string.Text = "您所上傳資料顯示如下，如資料格式正確，請按下下方確認上傳按鈕後匯入資料庫，如要重新上傳資料，請按下整批刪除按鈕";
        
        if (counts_ok.Equals("0"))
        {
            ok_string.Visible = false;
            ok_title.Visible = false;
        }

        if (counts_error.Equals("0"))
        {
            error_string.Visible = false;
            error_title.Visible = false;
            GridView_Error.Visible = false;
            //ok_string.Text = "您所上傳資料顯示如下，如資料格式正確，請按下下方確認上傳按鈕後匯入資料庫，如要重新上傳資料，請按下整批刪除按鈕";
	    Insert_RoadKill.Text = "確認上傳";
        }
        else
        {
            //error_string.Text = "您所上傳資料顯示如表格，其中id為" + Request.QueryString[0].ToString() +"，其資料可能有誤，請重新再檢查。";
            error_string.Text = "您所上傳資料顯示如表格，其資料可能有誤，請重新再檢查。";
            //memo.Text = "如您欲匯入本次上傳的結果，請按下下方確認上傳按鈕後匯入資料庫，如要重新上傳資料，請按下整批刪除按鈕。" + "\r\n" + errorLog;
            //memo.Text = errorLog;
	    Insert_RoadKill.Text = "強制上傳可能有誤的資料";
        }
        
        Query();
    }

    protected void Query()
    {
        string sql = " Select id , site_ch , highway_id , direction , replace(replace(milestone,'00000',''),'0000','') as milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , collecter_ch , species ,  Case When file1 <> '' Then '..'+path1+file1 Else '' End As file1 , note , Case When file1='' Then 'no data' Else 'no file' End As image_memo From RoadKill_tmp Where 1=1 ";
        string role = Session["role"].ToString();
        string uid = Session["uid"].ToString();
        sql += " and uid = @uid";

        SDS_View.SelectCommand = sql;
	SDS_View.SelectParameters.Clear();
	SDS_View.SelectParameters.Add("uid",uid);
        GridView_List.DataBind();
        GridView_List.PageSize = 20;

        //for (int i = 0; i < GridView_List.Rows; i++) { 
        //}
    }

   

    protected void Insert_RoadKill_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_uid = Session["uid"].ToString();
        string sqlCommand = " Begin Delete Roadkill_Month_NoRecord Where uid = @uid and years = Year(getdate()) and months = Month(getdate()); Insert Into Roadkill (site_ch,highway_id,direction,milestone,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid,upload_file,upload_date,Sensitive_level,image_file,animal2) Select site_ch,highway_id,direction,milestone,x,y,TM2_X,TM2_Y,date,range,type,weather,animal,detail_animal,collecter_ch,species,deduce_species,file1,file2,file3,file4,file5,path1,transfer,note,uid,upload_file,upload_date,Sensitive_level,image_file,'' as animal2 From RoadKill_tmp Where uid = @uid; delete From RoadKill_tmp Where uid = @uid; delete From RoadKill_error Where uid = @uid; end;";

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();
            //Query();

	    userlog(Session["uid"].ToString(), "INSERT_INTO_ROADKILL_FROM_BATCH_UPLOAD");

            Response.Write("<script>alert('上傳批次資料完成!');location.href='Batch_Upload_Roadkill_.aspx'; </script>");
            //Response.Write("<script>alert('新增道路致死批次資料完成!!')</script>");
            //Response.Redirect("Batch_Upload_Roadkill_.aspx");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

    }


    protected void Delete_RoadKill_tmp_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_uid = Session["uid"].ToString();
        string sqlCommand = "Begin Delete Roadkill_tmp  Where uid = @uid; Delete Roadkill_error  Where uid = @uid; End;";

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            Response.Write("<script>alert('刪除批次資料完成!');location.href='Batch_Upload_Roadkill_.aspx'; </script>");
            //Response.Write("<script>alert('刪除道路致死批次資料完成!!')</script>");
            //Response.Redirect("Batch_Upload_Roadkill_.aspx");
            //Query();
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

    }

    private void userlog(string uid, string action)
    {
	string source_ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
	if (source_ip == null) source_ip = Request.ServerVariables["REMOTE_ADDR"];

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

	conn.Open();
        string sql = "insert into userlog(uid,action,atime,source_ip) values(@uid,@action,getdate(),@source_ip)";
	cmd = new SqlCommand(sql, conn);
	cmd.Parameters.AddWithValue("uid", uid);
	cmd.Parameters.AddWithValue("action", action);
	cmd.Parameters.AddWithValue("source_ip", source_ip);
	cmd.ExecuteNonQuery();

	cmd.Dispose();
	conn.Dispose();
    }
}