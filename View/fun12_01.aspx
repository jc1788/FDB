<%@ page language="C#" autoeventwireup="true" inherits="View_fun12_01, App_Web_qgy0chct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <a href="/newdefault.aspx" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/control/fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
                </a>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7">
                <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                     <asp:TableRow HorizontalAlign="Right">
                         <asp:TableCell>
                             <div class="menu4">
                                 <a href="/newdefault.aspx" class="menu4">首頁</a> &gt;  
                                 <a href="fun12_01.aspx" class="menu4">文件下載</a>
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
                                <asp:TableRow BorderStyle="None" HorizontalAlign="Left">
                                    <asp:TableCell ColumnSpan="3">&nbsp;&nbsp;
                                        <span class="menu13">EML 下載</span> 
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell Width="33%">&nbsp;&nbsp;
                                        <asp:Image ID="Image2" runat="server" ImageUrl="http://taibif.org.tw/freeway/images/zip.png" Height="30" Width="30" />
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://taibif.org.tw/freeway/download/AR.zip">動物調查(道路致死)</asp:HyperLink>
                                    </asp:TableCell>
                                    <asp:TableCell Width="33%">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="http://taibif.org.tw/freeway/images/zip.png" Height="30" Width="30"/>
                                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="http://taibif.org.tw/freeway/download/AA.zip">動物調查(除鳥類)</asp:HyperLink>
                                    </asp:TableCell>
                                    <asp:TableCell Width="33%">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="http://taibif.org.tw/freeway/images/zip.png" Height="30" Width="30"/>
                                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="http://taibif.org.tw/freeway/download/AB.zip">動物調查(鳥類)</asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell>&nbsp;&nbsp;
                                        <asp:Image ID="Image5" runat="server" ImageUrl="http://taibif.org.tw/freeway/images/zip.png" Height="30" Width="30" />
                                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="http://taibif.org.tw/freeway/download/PP.zip">植物調查</asp:HyperLink>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Image ID="Image6" runat="server" ImageUrl="http://taibif.org.tw/freeway/images/zip.png" Height="30" Width="30"/>
                                        <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="http://taibif.org.tw/freeway/download/AC.zip">道路致死</asp:HyperLink>
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell ColumnSpan="3">
                                        &nbsp;&nbsp;補充說明：<br/><br/>

                                        &nbsp;&nbsp;1. 本EML文件為使用Morpho 1.8.0版本所編制，若為舊版morpho請升級，否則無發開啟檔案<br/><br/>

                                        &nbsp;&nbsp;2. 下載後的檔案，請解壓縮至您電腦中預設的morpho 的「data」資料夾<br/><br/>
                                    </asp:TableCell>
                                </asp:TableRow>
        
                                <asp:TableRow BorderStyle="None" HorizontalAlign="Left">
                                    <asp:TableCell ColumnSpan="3">&nbsp;&nbsp;
                                        <span class="menu13">說明檔案下載</span>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell>&nbsp;&nbsp;
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" Height="20" Width="20"/>
                                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Attachments/Batch_Upload/敏感里程管理原則與辦法.doc" BorderStyle="None">敏感里程管理原則與辦法</asp:HyperLink>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        
                                        <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Attachments/Batch_Upload/批次動物道路致死調查格式.xlsx" BorderStyle="None"></asp:HyperLink>
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00" ColumnSpan="3">
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
       