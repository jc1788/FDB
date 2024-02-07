using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Environment_Restore_add1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
    }

    protected void Add_Environment_Restore_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand = "Insert Into Environment_Restore( plan_id , plan_name , owner , instruction , plant_duration , plan_area , work_item , status , finish_date , uid) values(";
        String vaule_plan_id = plan_id.Text;
        String vaule_plan_name = plan_name.Text;
        String vaule_owner = owner.Text;
        String vaule_instruction = instruction.Text;
        String vaule_plant_duration = plant_duration.Text;
        String vaule_plan_area = plan_area.Text;
        String vaule_work_item = work_item.Text;
        String vaule_status = status.Text;
        String vaule_finish_date = finish_date.Text;
        String value_uid = Session["uid"].ToString();


        sqlCommand += "'" + vaule_plan_id + "',";
        sqlCommand += "'" + vaule_plan_name + "',";
        sqlCommand += "'" + vaule_owner + "',";
        sqlCommand += "'" + vaule_instruction + "',";
        sqlCommand += "'" + vaule_plant_duration + "',";
        sqlCommand += "'" + vaule_plan_area + "',";
        sqlCommand += "'" + vaule_work_item + "',";
        sqlCommand += "'" + vaule_status + "',";
        sqlCommand += "'" + vaule_finish_date + "',";
        sqlCommand += "'" + value_uid + "')";



        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            conn.Open();

            // 建立 SqlCommand 
            cmd = new SqlCommand(sqlCommand, conn);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            Response.Write("<script>alert('新增生態相關資料完成!!');location.href='/newdefault.aspx';</script>");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }

    }
}