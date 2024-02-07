using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Control_Userdata_edit1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        string role = Session["role"].ToString();
        if (!role.Equals("1"))
        {
	    Response.Redirect("../newdefault.aspx");
	}

        if (!IsPostBack){
            Get_Data();
        }
        
    }

    protected void Edit_Userdata_Click(object sender, EventArgs e)
    {
        string value_uid = uid.Text;
        string value_user_login_name = user_login_name.Text;
        string value_password = password.Text;
        string value_role = role.Text;
        string value_user_name = user_name.Text;
        string value_user_email = user_email.Text;
        string value_user_institution = user_institution.Text;

        string sqlCommand = " Update Userdata Set ";
        sqlCommand += " user_login_name = @value_user_login_name,";
        sqlCommand += " password = @value_password,";
        sqlCommand += " role = @value_role,";
        sqlCommand += " user_name = @value_user_name,";
        sqlCommand += " user_email = @value_user_email,";
        sqlCommand += " user_institution = @value_user_institution ";
        sqlCommand += " Where uid = @value_uid ";

        //Response.Write(sqlCommand);
        //Response.Write("<script>alert('"+sqlCommand+"')</script>");

        try
        {
            // 用來連線本機 SQL Server 的適當連線字串。
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
            conn.Open();

            // 建立 SqlCommand 
            SqlCommand cmd = new SqlCommand(sqlCommand, conn);
            cmd.Parameters.AddWithValue("value_user_login_name", value_user_login_name);
            cmd.Parameters.AddWithValue("value_password", value_password);
            cmd.Parameters.AddWithValue("value_role", value_role);
            cmd.Parameters.AddWithValue("value_user_name", value_user_name);
            cmd.Parameters.AddWithValue("value_user_email", value_user_email);
            cmd.Parameters.AddWithValue("value_user_institution", value_user_institution);
            cmd.Parameters.AddWithValue("value_uid", value_uid);

            // 執行命令
            cmd.ExecuteNonQuery();

            // 清理命令和連線物件。
            cmd.Dispose();
            conn.Dispose();

            Response.Write("<script>alert('修改帳號資料完成!!')</script>");
            Response.Redirect("Userdata_.aspx");
            //Response.Write("<script>'javascript:window.close()';</script>");

            
            
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
        String value_uid = Request.QueryString["uid"];
        String sqlCommand_NameFull = " Select uid , user_login_name , password , role , user_name , user_email , user_institution From Userdata Where uid = @value_uid ";

        try
        {
            conn.Open();
            cmd = new SqlCommand(sqlCommand_NameFull, conn);
            cmd.Parameters.AddWithValue("value_uid", value_uid);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    uid.Text = reader["uid"].ToString();
                    user_login_name.Text = reader["user_login_name"].ToString();
                    password.Text = reader["password"].ToString();
                    role.SelectedValue = reader["role"].ToString();
                    user_name.Text = reader["user_name"].ToString();
                    user_email.Text = reader["user_email"].ToString();
                    user_institution.Text = reader["user_institution"].ToString();
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