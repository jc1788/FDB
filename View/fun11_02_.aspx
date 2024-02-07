<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fun11_02_.aspx.cs" Inherits="View_fun11_02" %>

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
      <link href="../Control/fun01_011.aspx_files/style1.css" rel="stylesheet" />
    <script src="../Control/fun01_011.aspx_files/WebResource.js"></script>
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
                <a href="../newdefault.aspx" >
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
                                 <a href="../newdefault.aspx" class="menu4">首頁</a> &gt;  
                                 <a href="fun11_01_.aspx" class="menu4">受傷動物通報查詢</a>
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
                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="1000">
                            <asp:Table ID="Table4" runat="server">

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">國道編號</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="highway_id" runat="server" Readonly="true" Text="" Width="90%" BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">里程</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="mileage" runat="server" Readonly="true" Text=""　Width="90%"  BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">發現日期</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="date" runat="server" Readonly="true" Text="" Width="90%" BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">發現時間</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="time" runat="server" Readonly="true" Text="" Width="90%"  BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">可能種類</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="species" runat="server" Readonly="true" Text="" Width="90%"  BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">工程處</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="institution" runat="server" Readonly="true" Text="" Width="90%"  BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">通報單位</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="reporter" runat="server" Readonly="true" Text="" Width="90%"  BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">狀態</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:TextBox ID="status" runat="server" Readonly="true" Text="" Width="90%"  BorderStyle="None" Font-Size="Medium"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="200" BackColor="#ECFFEC">
                                        <span class="menu13">照片</span>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="800" BackColor="#FFFFFF">
                                        <asp:image id="file1" runat="server" ></asp:image>
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
