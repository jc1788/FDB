using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SSOLogin : Page
{
    AntiXssClass oMySet = new AntiXssClass();

    string argErr = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        argErr = "AdError";
        Session["UserID"] = "";

        string sArgOpen = oMySet.ReadRQSData(Request, "argOpen");
        string sArgDept = oMySet.ReadRQSData(Request, "argDept");
        //南區工程處.岡山工務段D28 03M02X000019 te-7006 林寬鴻   te-7006@freeway.gov.tw
        //會把 - 去除 變成 te7006
        string sArgUser = oMySet.ReadRQSData(Request, "argUser");
        //string sArgUser = oMySet.ReadRQSData(Request, "argUser");

        if (IsPostBack == false && sArgUser != "")
        {
            string argO = sArgOpen;
            string argD = "";
            string argU = "";

            if (sArgDept != "") { argD = sArgDept; }
            if (sArgUser != "") { argU = sArgUser; }

            if (argU != "")
            {
                Response.Write("<br />");
                Response.Write("<br />");
                Response.Write("argD：" + argD);
                Response.Write("<br />");
                Response.Write("argU：" + argU);
                Response.Write("<br />");
                Response.Write("argO：" + argO);
                LoginSys(argD, argU, argO);
            }
            else
            {
                Response.Redirect("Default.aspx?ArgData=" + argErr + "_2");
            }
        }
        else
        {
            Response.Redirect("Default.aspx?ArgData=" + argErr + "_3");
        }
    }

    private void LoginSys(string sPosID, string sArgU, string sLoginType)
    {
        string sCPCPerson = "No";

        if (sArgU.ToLower() != "admin")
        {
            string sPassWord = "00123456789";

            string deptType = null;
            string clientIP = LdapClass.ChkUser.GetClientIP(Request);
            string[] xClientIP = clientIP.Split('.');
            if (xClientIP.Length > 2)
            {
                if (xClientIP[0] == "10")
                {
                    switch (xClientIP[1])
                    {
                        case "53": //南工
                            deptType = "南";
                            break;
                        case "50": //總局
                        case "51": //北工
                        case "52": //中工
                        case "54": //第一新建工程處
                        case "55": //第二新建工程處
                            deptType = "總";
                            break;
                    }
                }
            }

            string errData = LdapClass.ChkUser.ChkLDAP_UID(sArgU, sPassWord, deptType);
            if (errData == "")
            {
                sCPCPerson = "Yes";
            }
            else
            {
                if (sLoginType != "S" && sLoginType != "TD")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "callAlert", "<script>alert('" + errData + "')</script>");
                    Response.Redirect("Default.aspx?ArgData=" + argErr + "_4");
                }
            }
        }

        //======================以下程式為範例，請依各系統自行修正====================

        if (sCPCPerson == "Yes")
        {
            string sSql = "Select ID,NAME From USER Where ID='" + sArgU + "'";
            DataView oDv = null; //去資料庫驗証帳密
            if (oDv != null && oDv.Count > 0)
            {
                //登入成功處理相關程式碼
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "ShowErrorMsg", "alert('Yes');", true); //動態載入Script
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "ShowErrorMsg", "alert('登入錯誤，請聯絡系統管理者');", true); //動態載入Script
        }
    }
}