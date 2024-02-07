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

public partial class Login : Page
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

        Response.Write("<br />");
        Response.Write("<br />");
        Response.Write("來源位置：" + clientData);
        Response.Write("<br />");
        Response.Write("網域資訊：" + sDomain);
        Response.Write("<br />");
        Response.Write("帳號資訊：" + sUserData);

        if (sUserData != "")
        {
            if (clientData != "" && clientData != "無法判斷")
            {
                string argOpen = argData; //AdLogin
                string argUser = sUserData;
                ClientScript.RegisterStartupScript(this.GetType(), "callSetSsoUrl", "setSsoUrl('" + argUser + "','" + argOpen + "');", true);
            }
        }
    }
}