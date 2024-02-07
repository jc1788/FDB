using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;


public partial class Control_Environment_Restore_edit2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        if (!IsPostBack)
        {
            Get_Data();
        }



    }


    protected void Edit_Environment_Restore_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String sqlCommand = " Update Environment_Restore Set ";

        String value_id = id.Text;
        String value_plan_name = plan_name.Text;
        String value_plan_id = plan_id.Text;
        String value_owner = owner.Text;
        String value_instruction = instruction.Text;
        String value_plant_duration = plant_duration.Text;
        String value_plan_area = plan_area.Text;
        String value_work_item = work_item.Text;
        String value_status = status.Text;
        String value_finish_date = finish_date.Text;
        
        String value_uid = Session["uid"].ToString();



        sqlCommand += " plan_name = '" + value_plan_name + "' , ";
        sqlCommand += " plan_id = '" + value_plan_id + "' , ";
        sqlCommand += " owner = '" + value_owner + "' , ";
        sqlCommand += " instruction = '" + value_instruction + "' , ";
        sqlCommand += " plant_duration = '" + value_plant_duration + "' , ";
        sqlCommand += " plan_area = '" + value_plan_area + "' , ";
        sqlCommand += " work_item = '" + value_work_item + "' , ";
        sqlCommand += " status = '" + value_status + "' , ";
        sqlCommand += " finish_date = '" + value_finish_date + "' , ";
        sqlCommand += " uid = '" + value_uid + "'  ";

        sqlCommand += " Where id = '" + value_id + "' ";


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


            Response.Write("<script>alert('修改生態相關資料完成!!')</script>");
            Response.Redirect("Environment_Restore_edit1_.aspx");
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            // 不應將此錯誤訊息傳回給呼叫者。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
    }

    protected void Get_Data()
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;
        string value_id = Request.QueryString["id"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = '" + value_id + "' ";

        string sql_GetData = " Select plan_name , plan_id , owner , instruction , plant_duration , plan_area , work_item , status , finish_date  From Environment_Restore Where id = '" + value_id + "' ";
        id.Text = value_id;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sql_GetData, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    plan_name.Text = reader["plan_name"].ToString();
                    plan_id.Text = reader["plan_id"].ToString();
                    owner.Text = reader["owner"].ToString();
                    instruction.Text = reader["instruction"].ToString();
                    plant_duration.Text = reader["plant_duration"].ToString();
                    plan_area.Text = reader["plan_area"].ToString();
                    work_item.Text = reader["work_item"].ToString();
                    status.Text = reader["status"].ToString();
                    finish_date.Text = reader["finish_date"].ToString();

                }
            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();
    }
}