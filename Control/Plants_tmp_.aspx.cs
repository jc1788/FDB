using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Plants_tmp : System.Web.UI.Page
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

        if (counts_error.Equals("0"))
        {
            error_string.Visible = false;
            error_title.Visible = false;
            GridView_Error.Visible = false;
            //Delete_RoadKill_tmp.Enabled = false;
            //ok_string.Text = "您所上傳資料顯示如下，如資料格式正確，請按下下方確認上傳按鈕後匯入資料庫，如要重新上傳資料，請按下整批刪除按鈕";
        }
        else if (counts_ok.Equals("0"))
        {
            ok_string.Visible = false;
            ok_title.Visible = false;
            Insert_RoadKill.Enabled = false;
        }
        else
        {
            //error_string.Text = "您所上傳資料顯示如表格，其中id為" + Request.QueryString[0].ToString() +"，其資料可能有誤，請重新再檢查。";
            error_string.Text = "您所上傳資料顯示如表格，其資料可能有誤，請重新再檢查。";
            //memo.Text = "如您欲匯入本次上傳的結果，請按下下方確認上傳按鈕後匯入資料庫，如要重新上傳資料，請按下整批刪除按鈕。" + "\r\n" + errorLog;
            //memo.Text = errorLog;
            Insert_RoadKill.Enabled = false;
        }

        Query();
    }

    protected void Query()
    {
        string sql = "Select Plantid , sid, Segment , Highway_Name , Direction , convert(decimal(5,1),Start1+'.'+Start2) as Start  ,convert(decimal(5,1),End1+'.'+End2) as [End], Plant_Name , Plant_Number , Plant_Loca , Date ,  Case When Img <> '' Then '../Attachments/Plants/'+Img Else '' End As Img , note , Case When Img='' Then 'no data' Else '' End As image_memo From Plants_tmp";
        string role = Session["role"].ToString();
        string uid = Session["uid"].ToString();
        sql += " where uid = '" + uid + "'";

        SDS_View.SelectCommand = sql;
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
        string sqlCommand = " Begin Insert Into Plants (Office,Segment,Highway_Name,Direction,Start1,Start2,Start,End1,End2,[End],PointX,PointY,PointXE,PointYE,Plant_Name,Plant_Number,unit2,SpecificationTall,SpecificationCrown,SpecificationMeter,LifeStyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,Plant_Loca,Img,FinishImg,Date,Note,uid,user_institution,upload_date,upload_xls) Select Office,Segment,Highway_Name,Direction,Start1,Start2,Start,End1,End2,[End],PointX,PointY,PointXE,PointYE,Plant_Name,Plant_Number,unit2,SpecificationTall,SpecificationCrown,SpecificationMeter,LifeStyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,Plant_Loca,Img,FinishImg,Date,Note,uid,user_institution,upload_date,upload_xls From Plants_tmp Where uid = @value_uid; delete From Plants_tmp Where uid = @value_uid; delete From Plants_error Where uid = @value_uid; end;";

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();
            //Query();

	    userlog(Session["uid"].ToString(), "INSERT_INTO_PLANTS_FROM_BATCH_UPLOAD");

            Response.Write("<script>alert('上傳批次資料完成!');location.href='Batch_Upload_Plants_.aspx'; </script>");
            //Response.Write("<script>alert('新增道路致死批次資料完成!!')</script>");
            //Response.Redirect("Batch_Upload_Roadkill.aspx");
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
        string sqlCommand = "Begin Delete Plants_tmp  Where uid = @value_uid; Delete Plants_error  Where uid = @value_uid; End;";

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);
	    cmd.Parameters.AddWithValue("value_uid",value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();


            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            Response.Write("<script>alert('刪除批次資料完成!');location.href='Batch_Upload_Plants_.aspx'; </script>");
            //Response.Write("<script>alert('刪除道路致死批次資料完成!!')</script>");
            //Response.Redirect("Batch_Upload_Roadkill.aspx");
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