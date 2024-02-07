<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="fun07_04_.aspx.cs" Inherits="View_fun07_04" %>

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
                                 <a href="fun07_04_.aspx" class="menu4">上傳道路致死統計</a>
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>

                    
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">
                            <asp:Table ID="Table_Function" runat="server" Width="640" HorizontalAlign="Center" >
                                            
                                <asp:TableRow>
                                    <asp:TableCell Width="17">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="10" CSSClass="title12">
                                        
                                    </asp:TableCell>
                                    <asp:TableCell Width="700" CSSClass="menu12a">
                                        <span class="menu13">
                                            <a href="fun07_04.aspx" class="menu13">上傳道路致死統計</a>
                                        </span> 
                                    </asp:TableCell>

                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
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
                                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" Width="500" >
                                        年度：
                                        <asp:DropDownList ID="Select_Year" runat="server" DataSourceID="SqlDataSource1" DataTextField="Select_Year_Show" DataValueField="Select_Year" AutoPostBack="True">
                                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '0' AS Select_Year , '請選擇' AS Select_Year_Show Union All (Select Distinct Year(Date) AS  Select_Year ,  CONVERT(VARCHAR(10),Year(Date)) AS Select_Year_Show From Roadkill ) Order By Select_Year "></asp:SqlDataSource>

                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="500" >
                                        月份：
                                        <asp:DropDownList ID="Select_Month" runat="server" DataSourceID="SqlDataSource2" DataTextField="Select_Month_Show" DataValueField="Select_Month" AutoPostBack="True">
                                            
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '0' AS Select_Month , '全年' AS Select_Month_Show Union All (Select Distinct Month(Date) AS  Select_Month ,  CONVERT(VARCHAR(10),Month(Date)) AS Select_Month_Show From Roadkill Where Date IS NOT NULL) Order By Select_Month "></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>

                                
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00" ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>

                                

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label2" runat="server" Text="年各工務段道路致死" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View2" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="site_ch" HeaderText="工務段" SortExpression="site_ch" />
                                                <asp:BoundField DataField="cnts" HeaderText="數量" SortExpression="cnts" />

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
                                        <asp:SqlDataSource ID="SDS_View2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select site_ch , count(site_ch) As cnts From Roadkill_new Where year(date) = 2009  Group By site_ch Order By site_ch">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Top" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label3" runat="server" Text="年月各工務段道路致死" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View3" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="site_ch" HeaderText="工務段" SortExpression="site_ch" />
                                                <asp:BoundField DataField="cnts" HeaderText="數量" SortExpression="cnts" />

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
                                        <asp:SqlDataSource ID="SDS_View3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select site_ch , Cast(count(site_ch) AS varchar(10)) As cnts From Roadkill_new Where year(date) = 2009 and month(date) = 4 Group By site_ch  Union Select user_institution , '無資料' As cnts From Roadkill_Month_NoRecord Where years = 2009 and months = 4 Order By site_ch">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel2" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button2" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel" OnClick="Export_Excel_new" Title="匯出EXCEL"/>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel3" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button3" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel" OnClick="Export_Excel_new" Title="匯出EXCEL"/>

                                        <br/><br/>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#000000">
                                    <asp:TableCell  ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>

                                

                                <asp:TableRow >
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999"  ColumnSpan="2">
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
