<%@ page language="C#" autoeventwireup="true" inherits="View_QA, App_Web_qgy0chct" %>

<!DOCTYPE html>

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

.PagerCss TD A:hover { FONT-SIZE: 20px;WIDTH: 20px; COLOR: black; padding-left: 4px; padding-right:4px; }
.PagerCss TD A:active { FONT-SIZE: 20px; WIDTH: 20px; COLOR: black; padding-left: 4px; padding-right:4px; }
.PagerCss TD A:link { FONT-SIZE: 20px; WIDTH: 20px; COLOR: black; padding-left: 4px; padding-right:4px; }
.PagerCss TD A:visited { FONT-SIZE: 20px; WIDTH: 20px; COLOR: black;padding-left: 4px; padding-right:4px; }
.PagerCss TD SPAN { FONT-WEIGHT: bold; FONT-SIZE: 24px; WIDTH: 20px; COLOR: red; padding-left: 4px; padding-right:4px;} 
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
    //-->
</script>

</head>
<body>
    
    <form id="form1" runat="server">
    <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
                <a href="../default.cshtml" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/frame/top1.jpg" BorderStyle="None" />
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
                                 <a href="QA.aspx" class="menu4">常見問題</a> 
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

       
        <asp:TableRow ID="Row_GridView">
            <asp:TableCell VerticalAlign="Top" >
                <asp:Table ID="Table3" runat="server" Width="1000" >
                    

                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                            <asp:Table ID="Table4" runat="server">
                                

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="1000">
                                        <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True" ForeColor="Red" HorizontalAlign="Center" Font-Size="X-Large"></asp:Label>
                                        <br/><br/>
                                        
                                        <asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label4" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label6" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label8" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label10" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label12" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label14" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                        <asp:Label ID="Label16" runat="server" Text="Label" Font-Bold="True"></asp:Label>
                                        <br/>
                                        <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                        <br/><br/>

                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow >
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999" >
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>


                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                
                <asp:Table ID="Table5" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
                    <asp:TableRow HorizontalAlign="Center" >
                        <asp:TableCell ColumnSpan="2" Font-Size="10" HorizontalAlign="Center" ForeColor="White">
                            <br/>
                            <div >
                                            總機：(02)2909-6141 (02)2909-6141 地址：24303臺北縣泰山鄉黎明村半山雅70號<br/>
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
