using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

using Microsoft.Security.Application;

/// <summary>
/// AntiXssClass 的摘要描述
/// </summary>
public class AntiXssClass
{
    HttpServerUtility Server;

    public AntiXssClass()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //

        Server = HttpContext.Current.Server;
    }

    ///<summary>Security安全性編碼</summary>
    public string GetHtmlEncode(string strData)
    {
        return Encoder.HtmlEncode(strData);
    }

    ///<summary>Security安全性編解碼</summary>
    public string GetHtmlEDncode(string strData)
    {
        return Server.HtmlDecode(Encoder.HtmlEncode(strData));
    }

    ///<summary>取得DataRow陣列裡的資料</summary>
    public DataRow ReadRowData(DataRow[] dRowS, int iFn) { return dRowS[iFn]; }
    ///<summary>取得DataRow裡的資料</summary>
    public string ReadRowData(DataRow dRow, string sFn)
    {
        return GetHtmlEDncode(dRow[sFn].ToString());
    }

    ///<summary>取得DataRowView裡的資料</summary>
    public string ReadRowData(DataRowView dRow, string sFn)
    {
        //string sData = "";
        //DataTable oDt = dRow.Row.Table;
        //if (oDt.Columns.Contains(sFn) == true)
        //{
        //    sData = dRow[sFn].ToString();
        //}
        //return sData;
        return GetHtmlEDncode(dRow[sFn].ToString());
    }

    ///<summary>取得Request裡QueryString、Form、ServerVariables的資料</summary>
    public NameValueCollection ReadRData(HttpRequest oRequest, string typeData)
    {
        NameValueCollection collData = null;
        switch (typeData)
        {
            case "QueryString":
                collData = oRequest.QueryString;
                break;
            case "Form":
                collData = oRequest.Form;
                break;
            case "ServerVariables":
                collData = oRequest.ServerVariables;
                break;
        }

        return collData;
    }

    ///<summary>取得Request裡Cookies的資料</summary>
    public HttpCookieCollection ReadCookie(HttpRequest oRequest)
    {
        return oRequest.Cookies;
    }

    ///<summary>取得Request裡Files的資料</summary>
    public HttpFileCollection ReadFiles(HttpRequest oRequest)
    {
        return oRequest.Files;
    }

    ///<summary>取得Request裡QueryString的資料</summary>
    public string ReadRQSData(HttpRequest oRequest, string sFn)
    {
        string backValue = "";
        if (oRequest.QueryString[sFn] != null) { backValue = oRequest.QueryString[sFn]; }

        return GetHtmlEDncode(backValue);
    }
    ///<summary>取得Request裡QueryString的資料</summary>
    public string ReadRQSData(HttpRequest oRequest, int iFn)
    {
        string backValue = "";
        if (oRequest.QueryString[iFn] != null) { backValue = oRequest.QueryString[iFn]; }

        return GetHtmlEDncode(backValue);
    }
    ///<summary>取得Request裡QueryString的AllKeys資料</summary>
    public string ReadRQSAllKeysData(HttpRequest oRequest, int iFn)
    {
        string backValue = "";
        if (oRequest.QueryString.AllKeys[iFn] != null) { backValue = oRequest.QueryString.AllKeys[iFn]; }

        return GetHtmlEDncode(backValue);
    }
    ///<summary>設定Request裡QueryString的資料給物件</summary>
    public void SetRQSData(WebControl oWebCtl, HttpRequest oRequest, string sFn) { SetCtlData(oWebCtl, ReadRQSData(oRequest, sFn)); }
    ///<summary>設定Request裡QueryString的資料給物件</summary>
    public void SetRQSData(HtmlControl oHtmlCtl, HttpRequest oRequest, string sFn) { SetCtlData(oHtmlCtl, ReadRQSData(oRequest, sFn)); }

    ///<summary>取得Request裡Form的資料</summary>
    public string ReadRQFData(HttpRequest oRequest, string sFn)
    {
        string backValue = "";
        if (oRequest.Form[sFn] != null) { backValue = oRequest.Form[sFn]; }

        return GetHtmlEDncode(backValue);
    }
    ///<summary>取得Request裡Form的資料</summary>
    public string ReadRQFData(HttpRequest oRequest, int iFn)
    {
        string backValue = "";
        if (oRequest.Form[iFn] != null) { backValue = oRequest.Form[iFn]; }

        return GetHtmlEDncode(backValue);
    }
    ///<summary>設定Request裡Form的資料給物件</summary>
    public void SetRQFData(WebControl oWebCtl, HttpRequest oRequest, string sFn) { SetCtlData(oWebCtl, ReadRQFData(oRequest, sFn)); }
    ///<summary>設定Request裡Form的資料給物件</summary>
    public void SetRQFData(HtmlControl oHtmlCtl, HttpRequest oRequest, string sFn) { SetCtlData(oHtmlCtl, ReadRQFData(oRequest, sFn)); }

    ///<summary>取得Request裡的資料</summary>
    public string ReadRQData(HttpRequest oRequest, string sFn) { return GetHtmlEDncode(oRequest[sFn]); }
    ///<summary>設定Request裡的資料給物件</summary>
    public void SetRQData(WebControl oWebCtl, HttpRequest oRequest, string sFn) { SetCtlData(oWebCtl, ReadRQData(oRequest, sFn)); }
    ///<summary>設定Request裡的資料給物件</summary>
    public void SetRQData(HtmlControl oHtmlCtl, HttpRequest oRequest, string sFn) { SetCtlData(oHtmlCtl, ReadRQData(oRequest, sFn)); }

    ///<summary>取得Session物件</summary>
    public HttpSessionState GetSessionCtl(HttpContext oContext)
    {
        return oContext.Session;
    }
    ///<summary>取得Session物件</summary>
    public HttpSessionState GetSessionCtl(Page oPage)
    {
        return oPage.Session;
    }

    ///<summary>取得Session裡的資料</summary>
    public string ReadSSData(HttpSessionState oSession, string sFn)
    {
        if (oSession[sFn] != null)
        {
            return GetHtmlEDncode(oSession[sFn].ToString());
        }
        else { return ""; }
    }
    ///<summary>設定Session裡的資料</summary>
    public void SetSSData(HttpSessionState oSession, string sFn, object setObj)
    {
        oSession[sFn] = setObj;
    }
    ///<summary>設定Session裡的資料給物件</summary>
    public void SetSSData(WebControl oWebCtl, HttpSessionState oSession, string sFn)
    {
        if (oSession[sFn] != null)
        {
            SetCtlData(oWebCtl, oSession[sFn].ToString());
        }
        else { SetCtlData(oWebCtl, ""); }
    }
    ///<summary>設定Session裡的資料給物件</summary>
    public void SetSSData(HtmlControl oHtmlCtl, HttpSessionState oSession, string sFn)
    {
        if (oSession[sFn] != null)
        {
            SetCtlData(oHtmlCtl, oSession[sFn].ToString());
        }
        else { SetCtlData(oHtmlCtl, ""); }
    }

    ///<summary>取得Cookie裡的資料</summary>
    public string ReadRCKata(HttpRequest oRequest, string sFn)
    {
        if (oRequest.Cookies[sFn] != null)
        {
            return GetHtmlEDncode(oRequest.Cookies[sFn].Value);
        }
        else { return ""; }
    }
    ///<summary>設定Cookie裡的資料給物件</summary>
    public void SetCKData(WebControl oWebCtl, HttpRequest oRequest, string sFn) { if (oRequest.Cookies[sFn] != null) { SetCtlData(oWebCtl, oRequest.Cookies[sFn].Value); } else { SetCtlData(oWebCtl, ""); } }
    ///<summary>設定Cookie裡的資料給物件</summary>
    public void SetCKData(HtmlControl oHtmlCtl, HttpRequest oRequest, string sFn) { if (oRequest.Cookies[sFn] != null) { SetCtlData(oHtmlCtl, oRequest.Cookies[sFn].Value); } else { SetCtlData(oHtmlCtl, ""); } }

    public string GetCboText(DropDownList oWebCtl)
    {
        return oWebCtl.SelectedItem.Text;
    }
    public string GetCboText(HtmlSelect oWebCtl)
    {
        return oWebCtl.Items[oWebCtl.SelectedIndex].Text;
    }

    /// <summary>讀取WebControl的資料</summary>
    public string GetCtlData(WebControl oWebCtl)
    {
        string backText = "";
        if (oWebCtl != null)
        {
            switch (oWebCtl.GetType().Name)
            {
                case "TextBox": backText = ((TextBox)oWebCtl).Text; break;
                case "TableCell": backText = ((TableCell)oWebCtl).Text; break;
                case "DataControlFieldCell": backText = ((DataControlFieldCell)oWebCtl).Text; break;
                case "Label": backText = ((Label)oWebCtl).Text; break;
                case "DropDownList":
                    DropDownList oCbo = (DropDownList)oWebCtl;
                    if (oCbo.SelectedItem != null) { backText = oCbo.SelectedValue; }
                    break;
                case "CheckBox": backText = ((CheckBox)oWebCtl).Text; break;
                case "RadioButton": backText = ((RadioButton)oWebCtl).Text; break;
                case "Button": backText = ((Button)oWebCtl).Text; break;
            }
        }

        return GetHtmlEDncode(backText);
    }

    ///<summary>設定WebControl的資料</summary>
    public void SetCtlData(WebControl oWebCtl, string sValue)
    {
        switch (oWebCtl.GetType().Name)
        {
            case "TextBox": ((TextBox)oWebCtl).Text = sValue; break;
            case "TableCell": ((TableCell)oWebCtl).Text = sValue; break;
            case "Label": ((Label)oWebCtl).Text = sValue; break;
            case "DropDownList": ((DropDownList)oWebCtl).SelectedValue = sValue; break;
            case "CheckBox": ((CheckBox)oWebCtl).Text = sValue; break;
            case "RadioButton": ((RadioButton)oWebCtl).Text = sValue; break;
            case "Button": ((Button)oWebCtl).Text = sValue; break;
        }
    }

    /// <summary>讀取HtmlControl的資料</summary>
    public string GetCtlData(HtmlControl oHtmlCtl)
    {
        string backText = "";
        if (oHtmlCtl != null)
        {
            switch (oHtmlCtl.GetType().Name)
            {
                case "HtmlInputText": backText = ((HtmlInputText)oHtmlCtl).Value; break;
                case "HtmlTextArea": backText = ((HtmlTextArea)oHtmlCtl).Value; break;
                case "HtmlInputRadioButton": backText = ((HtmlInputRadioButton)oHtmlCtl).Value; break;
                case "HtmlInputCheckBox": backText = ((HtmlInputCheckBox)oHtmlCtl).Value; break;
                case "HtmlTableCell": backText = ((HtmlTableCell)oHtmlCtl).InnerHtml; break;
                case "HtmlGenericControl": backText = ((HtmlGenericControl)oHtmlCtl).InnerHtml; break;
                case "HtmlInputButton": backText = ((HtmlInputButton)oHtmlCtl).Value; break;
                case "HtmlButton": backText = ((HtmlButton)oHtmlCtl).InnerHtml; break;
                case "HtmlSelect": backText = ((HtmlSelect)oHtmlCtl).Value; break;
            }
        }
        return GetHtmlEDncode(backText);
    }

    ///<summary>設定HtmlControl的資料</summary>
    public void SetCtlData(HtmlControl oHtmlCtl, string sValue)
    {
        switch (oHtmlCtl.GetType().Name)
        {
            case "HtmlInputText": ((HtmlInputText)oHtmlCtl).Value = sValue; break;
            case "HtmlTextArea": ((HtmlTextArea)oHtmlCtl).Value = sValue; break;
            case "HtmlInputRadioButton": ((HtmlInputRadioButton)oHtmlCtl).Value = sValue; break;
            case "HtmlInputCheckBox": ((HtmlInputCheckBox)oHtmlCtl).Value = sValue; break;
            case "HtmlTableCell": ((HtmlTableCell)oHtmlCtl).InnerHtml = sValue; break;
            case "HtmlGenericControl": ((HtmlGenericControl)oHtmlCtl).InnerHtml = sValue; break;
            case "HtmlInputButton": ((HtmlInputButton)oHtmlCtl).Value = sValue; break;
            case "HtmlButton": ((HtmlButton)oHtmlCtl).InnerHtml = sValue; break;
        }
    }

    ///<summary>設定WebControl的資料</summary>
    public void SetCtlData(WebControl oWebCtl, DataRow dRow, string sFn) { SetCtlData(oWebCtl, dRow[sFn].ToString()); }
    ///<summary>設定WebControl的資料</summary>
    public void SetCtlData(WebControl oWebCtl, DataRowView dRow, string sFn) { SetCtlData(oWebCtl, dRow[sFn].ToString()); }
    ///<summary>設定HtmlControl的資料</summary>
    public void SetCtlData(HtmlControl oHtmlCtl, DataRow dRow, string sFn) { SetCtlData(oHtmlCtl, dRow[sFn].ToString()); }
    ///<summary>設定HtmlControl的資料</summary>
    public void SetCtlData(HtmlControl oHtmlCtl, DataRowView dRow, string sFn) { SetCtlData(oHtmlCtl, dRow[sFn].ToString()); }
}