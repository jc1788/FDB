<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DLFinishPlantsData.aspx.cs" Inherits="Control_DLFinishPlantsData" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        <!--
        body {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 00px;
            background-image: url();
            background-repeat: repeat;
            background-color: #F7F7F7;
        }
        -->
    </style>

    <link href="../Content/style1.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
<!--
    function MM_preloadImages() { //v3.0
        var d = document; if (d.images) {
            if (!d.MM_p) d.MM_p = new Array();
            var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
        }
    }
    function MM_swapImgRestore() { //v3.0
        var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
    }
    function MM_findObj(n, d) { //v4.01
        var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
            d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
        }
        if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
        for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
        if (!x && d.getElementById) x = d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
            if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
    }

    function swap_data(rowid) { //v3.0
        if (rowid == 'data1') {
            document.data1.Visible = "true";
            document.data2.Visible = "false";
            document.data3.Visible = "false";
        }
    }
    //-->
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                <a href="../newdefault.aspx" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
                </a>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow Width="900">
                <asp:TableCell ColumnSpan="2">
                    <asp:Table ID="Table2" runat="server" Width="800" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="300">
                                國道編號：
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="highway_id" runat="server" DataSourceID="SqlDataSource3" DataTextField="highway_name" DataValueField="highway_id" AutoPostBack="true"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT highway_id , highway_name FROM [Highway] UNION Select Top 1 '' , '' From [Highway]"></asp:SqlDataSource>
                                &nbsp;&nbsp;
                            </asp:TableCell>
                            <asp:TableCell Width="300"></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="300">
                                方向：
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="direction" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>E</asp:ListItem>
                                    <asp:ListItem>S</asp:ListItem>
                                    <asp:ListItem>W</asp:ListItem>
                                    <asp:ListItem>N</asp:ListItem>
                                    <asp:ListItem>M</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell Width="300"></asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="300">
                                起始里程：
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="start1" runat="server" Width="30" Text="0" AutoPostBack="true"></asp:TextBox>
                                &nbsp;k+&nbsp;
                                <asp:TextBox ID="start2" runat="server" Width="30" Text="0" AutoPostBack="true"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell Width="300"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="300">
                                結束里程：
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="End1" runat="server" Width="30" Text="0" AutoPostBack="true"></asp:TextBox>
                                &nbsp;k+&nbsp;
                                <asp:TextBox ID="End2" runat="server" Width="30" Text="0" AutoPostBack="true"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell Width="300"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="300"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="Center" Width="300">
                                <asp:Button runat="server" ID="DLData" OnClick="DLData_Click" Text="下載資料" />
                            </asp:TableCell>
                            <asp:TableCell Width="300"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">

                    <asp:Table ID="Table3" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
                        <asp:TableRow HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2" Font-Size="10" HorizontalAlign="Center" ForeColor="White">
                            <br/>
                            <div >
                                            總機：(02)2909-6141 (02)2909-6141 地址：24303新北市泰山區黎明里半山雅70號<br/>
                                    交通部高速公路局 <br/>
                                版權所有 最佳瀏覽環境：IE 6.0、FireFox 2.0 以上版本．本網站最佳瀏覽解析度為 1024*768
                            </div>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
