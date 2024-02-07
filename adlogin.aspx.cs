using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class adlogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string value_uid = "";
	string value_logintime = "";

        if (Request.QueryString["uid"] != null)
        {
	    value_uid = Request.QueryString["uid"].ToString();
        }

	if (Request.QueryString["logintime"] != null)
        {
	    value_logintime = Request.QueryString["logintime"].ToString();
        }
         
	if (value_uid == "" || value_logintime == "")
	    Response.Redirect("newdefault.aspx");

        string sql = "SELECT id, uid, user_login_name, role, user_name, user_email, user_institution, login_time FROM adlogin WHERE uid = @uid and login_time = @logintime";

        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        int login_count = 0;
        try
        {
            conn.Open();
            cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("uid", value_uid);
	    cmd.Parameters.AddWithValue("logintime", value_logintime);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                login_count++;
                Context.Session["uid"] = reader["uid"].ToString();
                Context.Session["role"] = reader["role"].ToString();
		Context.Session["user_institution"] = reader["user_institution"].ToString();
		string value_user_login_name = reader["user_login_name"].ToString();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        value_user_login_name,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        true,
                        "",
                        FormsAuthentication.FormsCookiePath);

                string encTicket = FormsAuthentication.Encrypt(ticket);

                // Create the cookie.
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
	    }
		reader.Close();
		sql = "DELETE from adlogin WHERE uid = @uid";
		cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("uid", value_uid);
		cmd.ExecuteNonQuery();

	    string source_ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
	    if (source_ip == null) source_ip = Request.ServerVariables["REMOTE_ADDR"];

	    sql = "insert into userlog(uid,action,atime,source_ip) values(@uid,@action,getdate(),@source_ip)";
	    cmd = new SqlCommand(sql, conn);
	    cmd.Parameters.AddWithValue("uid", value_uid);
	    cmd.Parameters.AddWithValue("action", "AD_LOGIN_SUCCESS");
	    cmd.Parameters.AddWithValue("source_ip", source_ip);
	    cmd.ExecuteNonQuery();

	    cmd.Dispose();
	    conn.Dispose();
        }
        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

	Response.Redirect("newdefault.aspx");
    }
}