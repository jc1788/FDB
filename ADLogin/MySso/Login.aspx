﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function setSsoUrl(argUser, argOpen) {
            parent.setSsoType("Yes");
            top.window.location.href = "../SSOLogin.aspx?argUser=" + argUser + "&argOpen=" + argOpen;
        }
    </script>
    <form id="form1" runat="server">
    </form>
</body>
</html>
