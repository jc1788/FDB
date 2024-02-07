<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adlogon.aspx.cs" Inherits="adlogon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function setSsoUrl(uid, logintime) {
            //parent.setSsoType("Yes");
            top.window.location.href = "../adlogin.aspx?uid=" + uid + "&logintime=" + logintime;
        }
    </script>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="debug" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
