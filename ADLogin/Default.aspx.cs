using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    AntiXssClass oMySet = new AntiXssClass();
    string argData = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        TxtArgP.Attributes["autocomplete"] = "off";
        argData = oMySet.ReadRQSData(Request, "ArgData");

        if (IsPostBack == false)
        {
            string clientIP = LdapClass.ChkUser.GetClientIP(Request);
            if (clientIP != "" && argData.Contains("AdError") == false)
            {
                string sGetNowScript = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

                //開發時測試
                //mySso.Attributes["src"] = "Login.aspx?GetNowScript=" + sGetNowScript;
                //SetLoginData("Auto");
                //ClientScript.RegisterClientScriptInclude("BasePageDefaultJS", "Default.js?GetNowScript=" + sGetNowScript); //動態載入Script

                string[] xClientIP = clientIP.Split('.');
                if (xClientIP.Length > 2)
                {
                    if (xClientIP[0] == "10")
                    {
                        //case "54": //第一新建工程處
                        //case "55": //第二新建工程處
                        //case "55": //拓建
                        switch (xClientIP[1])
                        {
                            case "50": //總局
                            case "51": //北工
                            case "52": //中工
                            case "53": //南工
                                mySso.Attributes["src"] = "MySso/Login.aspx?GetNowScript=" + sGetNowScript;
                                SetLoginData("Auto");
                                ClientScript.RegisterClientScriptInclude("BasePageDefaultJS", "Default.js?GetNowScript=" + sGetNowScript); //動態載入Script
                                break;
                        }
                    }
                }
            }
            else
            {
                Session["UserID"] = null;
                Session.Clear();
                Session["LogOutPage"] = "Default.aspx";

                TxtArgU.Focus();
            }
        }
    }

    private void SetLoginData(string sType)
    {
        switch (sType)
        {
            case "Auto":
                TableLogin.Style[HtmlTextWriterStyle.Display] = "none";
                TableLogin.Style[HtmlTextWriterStyle.Visibility] = "hidden";
                TableAuto.Style[HtmlTextWriterStyle.Display] = "";
                TableAuto.Style[HtmlTextWriterStyle.Visibility] = "";
                break;
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string sArgU = "";
        string sArgP = "";

        if (TxtArgU.Text.Length == 0)
        {
            sArgU = "";

            Session["UserID"] = null;
            Session.Clear();
            Session["LogOutPage"] = "Default.aspx";
            return;
        }
        else
        {
            sArgU = oMySet.GetCtlData(TxtArgU);
            sArgP = oMySet.GetCtlData(TxtArgP);

            if (sArgU.Trim() == "" || sArgP.Trim() == "")
            {
                Session["UserID"] = null;
                Session.Clear();
                Session["LogOutPage"] = "Default.aspx";
                ClientScript.RegisterStartupScript(Page.GetType(), "ShowErrorMsg", "alert('帳號 或 密碼 不得為空白');", true); //動態載入Script
                return;
            }
        }

        LoginSys(sArgU, sArgP);
    }

    private void LoginSys(string sArgU, string sArgP)
    {
        string sCPCPerson = "No";

        if (sArgU.ToLower() != "admin")
        {
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

            bool bChk = LdapClass.ReadAD.CheckUser(sArgU, sArgP, deptType); //密碼是否正確
            if (bChk == true)
            {
                string errData = LdapClass.ChkUser.ChkLDAP_UID(sArgU, sArgP, deptType);
                if (errData == "")
                {
                    sCPCPerson = "Yes";
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "ShowErrorMsg", "alert('" + errData + "');", true); //動態載入Script
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "ShowErrorMsg", "alert('帳號 或 密碼 驗証錯誤');", true); //動態載入Script
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
            ClientScript.RegisterStartupScript(Page.GetType(), "ShowErrorMsg", "alert('帳號 或 密碼 登入錯誤');", true); //動態載入Script
        }
    }
}