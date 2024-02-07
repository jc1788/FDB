<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<link href="/Content/style1.css" rel="stylesheet" type="text/css" />
<html xmlns="http://www.w3.org/1999/xhtml">
    
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width" />
        <title>登入</title>
        <link href="/Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
        <link href="/Content/base.css" rel="stylesheet" />
        <link href="/Content/admin.css" rel="stylesheet" type="text/css" />
        <link href="/Content/style.css" rel="stylesheet" type="text/css" />
        <link href="/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <script type="text/javascript" src="/Scripts/jquery-1.7.1.min.js"></script>
        <script type="text/javascript" src="/Scripts/jquery-ui-1.8.20.js"></script>
        <script type="text/javascript" src="/Scripts/modernizr-2.5.3.js"></script>
    </head>

<body style="margin: 0px;">
    <form id="form1" runat="server">
                                            <table id="TableAuto" border="0" cellpadding="0" cellspacing="0" style="display: none; visibility: hidden" runat="server">
                                                <tr>
                                                    <td id="TdAgrUMsg" align="center" style="color: #c06161; font-size: 13px">帳號偵測中，請稍後</td>
                                                </tr>
                                                <tr id="TrAuto">
                                                    <td align="center">
                                                        <img src="Images/LoadingS.gif" alt="等待圖示" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="TableLogin" border="0" cellpadding="0" cellspacing="0" runat="server">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" style="line-height: 25px">
                                                            <tr>
                                                                <td style="color: #5D5E5E; font-size: 15px; font-weight: bold">帳號：</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="TxtArgU" CssClass="input" runat="server" Width="82px" MaxLength="15"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="color: #5D5E5E; font-size: 15px; font-weight: bold">密碼：</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="TxtArgP" CssClass="input" runat="server" Width="82px" MaxLength="20" TextMode="Password"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 5px"></td>
                                                    <td>
                                                        <asp:Button ID="ImageButton1" runat="server" OnClick="btnLogin_Click" Text="登入"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
        <iframe id="mySso" frameborder="0" src="Error404.htm" style="display:none;visibility:hidden" runat="server"></iframe>
    </form>
</body>
</html>
