<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Occurrence_edit2_.aspx.cs" Inherits="Control_Occurrence_edit2" %>

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
        .PagerCss TD A:hover {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD A:active {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD A:link {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD A:visited {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD SPAN {
            FONT-WEIGHT: bold;
            FONT-SIZE: 24px;
            WIDTH: 20px;
            COLOR: red;
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>
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
     <link href="fun01_011.aspx_files/style1.css" rel="stylesheet" type="text/css" />
    <script src="fun01_011.aspx_files/WebResource.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
        <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <a href="../newdefault.aspx">
                        <asp:Image ID="Image1" runat="server" ImageUrl="fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
                    </a>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell BackColor="#F7F7F7">
                    <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                        <asp:TableRow HorizontalAlign="Right">
                            <asp:TableCell>
                             <div class="menu4">
                                 <a href="../newdefault.aspx" class="menu4">首頁</a> &gt;
                                 <a href="../newdefault.aspx" class="menu4">單筆修改</a> &gt;
                                 <a href="../Control/Occurrence_edit2_.aspx" class="menu4">生態資料</a>
                                 
                             </div>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell BackColor="#F7F7F7">
                    <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/frame/c01.jpg" Width="1000" Height="47" />
                            </asp:TableCell>
                        </asp:TableRow>


                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/frame/c03.jpg" Width="1000" Height="25" />
                            </asp:TableCell>
                        </asp:TableRow>

                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableRow ID="Row_GridView">
                <asp:TableCell VerticalAlign="Top">
                    <asp:Table ID="Table3" runat="server" Width="1000">


                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                                <asp:Table ID="Table4" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" Width="1000">
                                            <asp:Label ID="TotalCounts" runat="server" Text=""></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">

                                            <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="sid" DataNavigateUrlFormatString="Occurrence_edit3_.aspx?sid={0}" HeaderText="物種名" Target="_blank" DataTextField="chinese_name" SortExpression="chinese_name" />
                                                    <asp:BoundField DataField="short_name" HeaderText="拉丁學名" SortExpression="short_name" />
                                                    <asp:BoundField DataField="date1" HeaderText="調查日期" SortExpression="date1" />
                                                    <asp:BoundField DataField="Site_city" HeaderText="調查地(縣市) " SortExpression="Site_city" />
                                                    <asp:BoundField DataField="Site_area" HeaderText="調查地(鄉鎮)" SortExpression="Site_area" />
                                                    <asp:BoundField DataField="Site_ch" HeaderText="調查地(地名)" SortExpression="Site_ch" />
                                                    <asp:BoundField DataField="highway_id" HeaderText="國道編號 " SortExpression="highway_id" />
                                                    <asp:BoundField DataField="Direction" HeaderText="方向" SortExpression="Direction" />
                                                    <asp:BoundField DataField="mileage" HeaderText="參考里程" SortExpression="mileage" />
                                                </Columns>
                                                <FooterStyle BackColor="Tan" />
                                                <HeaderStyle BackColor="#ECFFEC" Font-Bold="True" CssClass="menu6" ForeColor="#035D00" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="PagerCss" VerticalAlign="Middle" />
                                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select sid,Chinese_Name , short_name , Convert(varchar(10),date1,126) AS date1 , Site_city , Site_area , Site_ch , highway_id ,  mileage,Direction  From table_1 "></asp:SqlDataSource>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999">
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
