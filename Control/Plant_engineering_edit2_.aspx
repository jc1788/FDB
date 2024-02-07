<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plant_engineering_edit2_.aspx.cs" Inherits="Control_Plant_engineering_edit2" %>

<!DOCTYPE html>

<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar-setup.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/lang/calendar-tc.js"></script>

<link type="text/css" href="../Scripts/jscalendar-1.0/calendar-blue.css" rel="Stylesheet" />


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7">
                <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                     <asp:TableRow HorizontalAlign="Right">
                         <asp:TableCell>
                             <div class="menu4">
                                 <a href="../newdefault.aspx" class="menu4">首頁</a> &gt;  
                                 <a href="../newdefault.aspx" class="menu4">單筆修改</a> &gt;  
                                 <a href="../Control/Plant_engineering_edit1_.aspx" class="menu4">植栽工程資料</a>
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7" VerticalAlign="Top">
                <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">
                            <asp:Table ID="Table_Detail" runat="server" Width="900" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2">
                                        植栽工程資料
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#FFFFFF" Width="900" Height="20">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_plant_id" runat="server" Text="編號："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="plant_id" runat="server" ></asp:TextBox>
                                        <asp:TextBox ID="pid" runat="server" Visible="False"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_number" runat="server" Text="標別："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="number" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_plant_name" runat="server" Text="計畫名稱："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="plant_name" runat="server" Width="400"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_institution" runat="server" Text="設計單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="institution" runat="server" Width="400"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_design_date" runat="server" Text="設計定稿日期："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="design_date" runat="server" ReadOnly="false" Enabled="true" CausesValidation="true" BackColor="silver"></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar1" runat="server" title="小日曆"  ImageUrl="~/Images/icon_date.jpg" />
                                        <script ="javascript" type="text/javascript">
                                            Calendar.setup({
                                                inputField: "design_date",
                                                ifFormat: "%Y-%m-%d",
                                                button: "imgCalendar1",
                                                align: "Tl",
                                                singleClick: true
                                            });
                                        </script>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_work_date" runat="server" Text="竣工日期："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="work_date" runat="server" ReadOnly="false" Enabled="true" CausesValidation="true" BackColor="silver"></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar2" runat="server" alt="小日曆"  ImageUrl="~/Images/icon_date.jpg" />
                                        <script ="javascript" type="text/javascript">
                                            Calendar.setup({
                                                inputField: "work_date",
                                                ifFormat: "%Y-%m-%d",
                                                button: "imgCalendar2",
                                                align: "Tl",
                                                singleClick: true
                                            });
                                        </script>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_maintain" runat="server" Text="維護單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="maintain" runat="server" Width="400"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                
                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300"/>
                                    <asp:TableCell Width="600">
                                        <asp:Button ID="Edit_Plant_engineering" runat="server" Text="修改植栽工程資料" OnClick="Edit_Plant_engineering_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
                <br/>

                
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow VerticalAlign="Top" BackColor="#a7a7a7">
            <asp:TableCell BackColor="#a7a7a7">
            </asp:TableCell>
        </asp:TableRow>

        

        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                
                <asp:Table ID="Table3" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
                    <asp:TableRow HorizontalAlign="Center" >
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
