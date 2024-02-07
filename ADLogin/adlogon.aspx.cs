using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;

public partial class adlogon : System.Web.UI.Page
{
    AntiXssClass oMySet = new AntiXssClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        string clientData = "";
        string clientIP = LdapClass.ChkUser.GetClientIP(Request);
        string[] xClientIP = clientIP.Split('.');
        if (xClientIP.Length > 2)
        {
            if (xClientIP[0] == "10")
            {
                switch (xClientIP[1])
                {
                    case "50": clientData = "總局"; break;
                    case "51": clientData = "北工處"; break;
                    case "52": clientData = "中工處"; break;
                    case "53": clientData = "南工處"; break;
                    case "54": clientData = "一工處"; break; //第一新建工程處
                    case "55": clientData = "二工處"; break; //第二新建工程處
                }
            }
        }
/*
	if (clientData == "")
	    Response.Redirect("../newdefault.aspx");
*/
        string sDomain = "";
        string sUserId = HttpContext.Current.User.Identity.Name;
        if (sUserId == "") { sUserId = Request.ServerVariables["LOGON_USER"]; }
        if (sUserId == "") { sUserId = Request.ServerVariables["AUTH_USER"]; }

        if (sUserId == null) { sUserId = ""; }
        if (sUserId == "") { return; }

        //string sUserData = @"FREEWAY\使用者登入帳號";
        string sUserData = sUserId;
        string[] xUserData = sUserData.Split('\\');
        if (xUserData.Length == 2)
        {
            sDomain = xUserData[0];
            sUserData = xUserData[1];
        }
        else
        {
            xUserData = sUserData.Split('/');
            if (xUserData.Length == 2)
            {
                sDomain = xUserData[0];
                sUserData = xUserData[1];
            }
        }

        string argData = oMySet.ReadRQSData(Request, "ArgData");
        if (argData == "") { argData = "Idx"; }

	/*
        Response.Write("<br />");
        Response.Write("來源位置：" + clientData);
        Response.Write("<br />");
        Response.Write("網域資訊：" + sDomain);
        Response.Write("<br />");
        Response.Write("帳號資訊：" + sUserData);
	*/

	if (sUserData != "")
	{
        string value_user_login_name = sUserData;

	string uid = "";
	string login_time = "";
        using(SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString()))
	{
	    string sql = "SELECT uid, role, user_name, user_email, user_institution FROM Userdata Where user_login_name = @value_user_login_name";
	    SqlCommand cmd;

            int login_count = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("value_user_login_name",value_user_login_name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   uid = reader["uid"].ToString();
                   string role = reader["role"].ToString();
		   string user_name = reader["user_name"].ToString();
		   string user_email = reader["user_email"].ToString();
		   string user_institution = reader["user_institution"].ToString();
		   reader.Close();
		   login_time = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "");
		   sql = "insert into adlogin (uid, user_login_name, role, user_name, user_email, user_institution, login_time) values (@uid,@value_user_login_name,@role,@user_name,@user_email,@user_institution,@login_time)";
		   cmd = new SqlCommand(sql, conn);
		   cmd.Parameters.AddWithValue("uid",uid);
		   cmd.Parameters.AddWithValue("value_user_login_name",value_user_login_name);
		   cmd.Parameters.AddWithValue("role",role);
		   cmd.Parameters.AddWithValue("user_name",user_name);
		   cmd.Parameters.AddWithValue("user_email",user_email);
		   cmd.Parameters.AddWithValue("user_institution",user_institution);
		   cmd.Parameters.AddWithValue("login_time",login_time);
		   cmd.ExecuteNonQuery();
                   login_count++;
		}
		else
		{
		   reader.Close();
		   sql = "insert Into userdata (user_login_name,password,role,user_name,user_email,user_institution) ";
		   sql += " values ( @value_user_login_name , '' , '2' , @value_user_login_name , '' , '' )";
		   cmd = new SqlCommand(sql, conn);
		   cmd.Parameters.AddWithValue("value_user_login_name",value_user_login_name);
		   cmd.ExecuteNonQuery();

		   sql = "SELECT uid, role, user_name, user_email, user_institution FROM Userdata Where user_login_name = @value_user_login_name";
		   cmd = new SqlCommand(sql, conn);
		   cmd.Parameters.AddWithValue("value_user_login_name",value_user_login_name);
		   reader = cmd.ExecuteReader();
		   if (reader.Read())
		   {
                   uid = reader["uid"].ToString();
                   string role = reader["role"].ToString();
		   string user_name = reader["user_name"].ToString();
		   string user_email = reader["user_email"].ToString();
		   string user_institution = reader["user_institution"].ToString();
		   reader.Close();
		   login_time = System.DateTime.Now.ToString("s").Replace("-", "").Replace(":", "").Replace("T", "");
		   sql = "insert into adlogin (uid, user_login_name, role, user_name, user_email, user_institution, login_time) values (@uid,@value_user_login_name,@role,@user_name,@user_email,@user_institution,@login_time)";
		   cmd = new SqlCommand(sql, conn);
		   cmd.Parameters.AddWithValue("uid",uid);
		   cmd.Parameters.AddWithValue("value_user_login_name",value_user_login_name);
		   cmd.Parameters.AddWithValue("role",role);
		   cmd.Parameters.AddWithValue("user_name",user_name);
		   cmd.Parameters.AddWithValue("user_email",user_email);
		   cmd.Parameters.AddWithValue("user_institution",user_institution);
		   cmd.Parameters.AddWithValue("login_time",login_time);
		   cmd.ExecuteNonQuery();
                   login_count++;
		   }
		}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
            conn.Close();
        }
	if (uid != "")
	    ClientScript.RegisterStartupScript(this.GetType(), "callSetSsoUrl", "setSsoUrl('" + uid + "','" + login_time + "');", true);
/*	    Response.Redirect("../adlogin.aspx?uid="+uid+"&logintime="+login_time);
	else
	    Response.Redirect("../newdefault.aspx");
*/	}
    }

}
