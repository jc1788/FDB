<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false"  CodeFile="fun01_021f_.aspx.cs" Inherits="View_fun01_021f" %>

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
         <link href="../Control/fun01_011.aspx_files/style1.css" rel="stylesheet" />
    <script src="../Control/fun01_011.aspx_files/WebResource.js"></script>
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
                                 <a href="../view/fun01_011_.aspx" class="menu4">生態調查資料</a> &gt;  
                                 <a href="../view/fun01_021a_.aspx" class="menu4">空間瀏覽</a> 
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7">
                <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/frame/c01.jpg" Width="1000" Height="47" />
                        </asp:TableCell>
                    </asp:TableRow>

                     <asp:TableRow>
                        <asp:TableCell>
                             <asp:Table ID="Table2" runat="server" Width="900" HorizontalAlign="Center" >
                                 <asp:TableRow>
                                    <asp:TableCell Width="649" VerticalAlign="Top">
                                        <asp:Table ID="Table_Function" runat="server" Width="640" HorizontalAlign="Center" >
                                            <asp:TableRow>
                                                <asp:TableCell Width="17">
                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                                </asp:TableCell>
                                                <asp:TableCell Width="93" CSSClass="title12">
                                                    物種瀏覽
                                                </asp:TableCell>
                                                <asp:TableCell Width="482" CSSClass="menu12a">
                                                    <span class="menu12">
                                                        <a href="../view/fun01_011_.aspx" class="menu12">依物種查詢</a>
                                                    </span> 
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell Width="17">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                                </asp:TableCell>
                                                <asp:TableCell Width="93" CSSClass="title12">
                                                    空間瀏覽
                                                </asp:TableCell>
                                                <asp:TableCell Width="482" CSSClass="menu12a">
                                                    <span class="menu12">
                                                        <a href="../view/fun01_021a_.aspx" class="menu12">依國道公里數查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="../view/fun01_021b_.aspx" class="menu12">依國道工程處查詢</a>
                                                    </span> | 
                                                    <span class="menu13">
                                                        <a href="../view/fun01_021e_.aspx" class="menu13">空間查詢</a>
                                                    </span> 
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br/>
                                        
                                     </asp:TableCell>

                                    <asp:TableCell Width="245" HorizontalAlign="Center">
                                         <div>
                                             <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/frame/c04.jpg" Width="189" Height="204" />
                                         </div>
                                     </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
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
            <asp:TableCell VerticalAlign="Top" >
                <asp:Table ID="Table3" runat="server" Width="1000" >
                    

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

                                        <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="Chinese_Name" HeaderText="中文名" SortExpression="Chinese_Name" />
                                                <asp:BoundField DataField="site_ch" HeaderText="調查地點" SortExpression="site_ch" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="invest_date" HeaderText="調查日期" ReadOnly="True" SortExpression="invest_date" />
                                                <asp:BoundField DataField="x" HeaderText="經度" SortExpression="x" />
                                                <asp:BoundField DataField="y" HeaderText="緯度" SortExpression="y" />
                                                <asp:HyperLinkField DataNavigateUrlFields="siteid,Short_Name,accepted_name_code,x,y,lo" DataNavigateUrlFormatString="../view/fun01_0131_.aspx?siteid={0}&amp;Short_Name={1}&amp;accepted_name_code={2}&amp;x={3}&amp;y={4}&amp;lo={5}" HeaderText="詳細資料" Target="_blank" Text="詳細資料" />

                                            </Columns>
                                            <FooterStyle BackColor="Tan" />
                                            <HeaderStyle BackColor="#ECFFEC" Font-Bold="True" CssClass="menu6" ForeColor="#035D00" />
                                            <PagerStyle  HorizontalAlign="Center" CssClass="PagerCss" VerticalAlign="Middle" />
                                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                            <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                            <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" SELECT distinct  a.siteid, CONVERT (char(10), a.date, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,6) as x , Round(a.y,6) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow >
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999" >
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button1" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel_new" OnClick="Export_Excel_new" Title="匯出EXCEL" />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>


                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center">
                <iframe src="<%=map_url%>" style="height: 800px; width: 900px;" scrolling="no" frameBorder="0"></iframe>
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
