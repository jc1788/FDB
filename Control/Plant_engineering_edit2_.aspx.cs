using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Control_Plant_engineering_edit2 : System.Web.UI.Page
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


    protected void Edit_Plant_engineering_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        String value_pid = pid.Text;
        String value_plant_id = plant_id.Text;
        String value_number = number.Text;
        String value_plant_name = plant_name.Text;
        String value_institution = institution.Text;
        String value_design_date = design_date.Text;
        String value_work_date = work_date.Text;
        String value_maintain = maintain.Text;
        String value_uid = Session["uid"].ToString();

        String sqlCommand = " Update Plant_engineering Set ";

        sqlCommand += " plant_id = '" + value_plant_id + "',";
        sqlCommand += " number = '" + value_number + "',";
        sqlCommand += " plant_name = '" + value_plant_name + "',";
        sqlCommand += " institution = '" + value_institution + "',";
        sqlCommand += " design_date = '" + value_design_date + "',";
        sqlCommand += " work_date = '" + value_work_date + "',";
        sqlCommand += " maintain = '" + value_maintain + "',";
        sqlCommand += " uid = '" + value_uid + "' ";

        sqlCommand += "Where pid = '" + value_pid + "' ";

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

                //Response.Write("<script>alert('修改植栽工程資料完成!!')</script>");
                //Response.Redirect("Plant_Engineering_edit1_.aspx");
		Response.Write("<script>alert('修改資料完成!');location.href='Plant_Engineering_edit1_.aspx'; </script>");
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
        string value_pid = Request.QueryString["pid"];
        //string sqlCommand_NameFull = " Select site_ch , highway_id , direction , Cast(milestone*1000 AS Integer) AS milestone , x , y , CONVERT (char(10), date, 126) AS date , range , type , weather , animal , detail_animal , collecter_ch , species , deduce_species , transfer , note From Roadkill Where id = '" + value_id + "' ";

        string sql_GetData = " Select pid,  plant_id , number , plant_name , institution , CONVERT (char(10), design_date, 126) AS design_date , CONVERT (char(10), work_date, 126) AS work_date , maintain From Plant_Engineering Where pid = '" + value_pid + "' ";
        pid.Text = value_pid;

        try
        {
            conn.Open();
            cmd = new SqlCommand(sql_GetData, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    plant_id.Text = reader["plant_id"].ToString();
                    number.Text = reader["number"].ToString();
                    plant_name.Text = reader["plant_name"].ToString();
                    institution.Text = reader["institution"].ToString();
                    design_date.Text = reader["design_date"].ToString();
                    work_date.Text = reader["work_date"].ToString();
                    maintain.Text = reader["maintain"].ToString();


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