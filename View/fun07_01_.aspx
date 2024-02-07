<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="fun07_01_.aspx.cs" Inherits="View_fun07_01" %>

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
                                 <a href="fun07_01_.aspx" class="menu4">統計資料概要</a>
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
                                    <asp:TableCell Width="150" CSSClass="title12">
                                        統計資料概要
                                    </asp:TableCell>
                                    <asp:TableCell Width="700" CSSClass="menu12a">
                                        <span class="menu13">
                                            <a href="fun07_01_.aspx" class="menu13">統計資料概要</a>
                                        </span> | 
                                            <a href="fun07_03_.aspx" class="menu12">統計資料綱要</a>
                                    </asp:TableCell>

                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell Width="17">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="150" CSSClass="title12">
                                        統計資料圖表
                                    </asp:TableCell>
                                    <asp:TableCell Width="700" CSSClass="menu12a">
                                        <span class="menu12">
                                            <a href="fun07_02_.aspx" class="menu12">工務段道路致死數量比較</a>
                                        </span> | 
                                        
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
                                            
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '0' AS Select_Year , '請選擇' AS Select_Year_Show Union All (Select Distinct Year(Date) AS  Select_Year ,  CONVERT(VARCHAR(10),Year(Date)) AS Select_Year_Show From Roadkill_New ) Order By Select_Year "></asp:SqlDataSource>
                                        月份：
                                        <asp:DropDownList ID="Select_Month" runat="server" DataSourceID="SqlDataSource2" DataTextField="Select_Month_Show" DataValueField="Select_Month" AutoPostBack="True">
                                            
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '0' AS Select_Month , '全年' AS Select_Month_Show Union All (Select Distinct Month(Date) AS  Select_Month ,  CONVERT(VARCHAR(10),Month(Date)) AS Select_Month_Show From Roadkill_New Where Date IS NOT NULL) Order By Select_Month "></asp:SqlDataSource>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="500" >
                                        資料類型：
                                        <asp:DropDownList ID="select_table" runat="server" AutoPostBack="True">
                                            <asp:ListItem Text="歷史資料" Value="roadkill_new" />
                                            <asp:ListItem Text="新增資料" Value="roadkill" />                                            
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                                
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00" ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" ColumnSpan="2">
                                        <br/><br/>

                                        <asp:Label ID="Label1" runat="server" Text="歷年道路致死調查累積數量" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View1" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="animal" HeaderText="類群" SortExpression="animal" />
                                                <asp:BoundField DataField="1" HeaderText="一" SortExpression="1" />
                                                <asp:BoundField DataField="2" HeaderText="二" SortExpression="2" />
                                                <asp:BoundField DataField="3" HeaderText="三" SortExpression="3" />
                                                <asp:BoundField DataField="5" HeaderText="五" SortExpression="5" />
                                                <asp:BoundField DataField="6" HeaderText="六" SortExpression="6" />
                                                <asp:BoundField DataField="7" HeaderText="七" SortExpression="7" />
                                                <asp:BoundField DataField="8" HeaderText="八" SortExpression="8" />
                                                <asp:BoundField DataField="9" HeaderText="九" SortExpression="9" />
                                                <asp:BoundField DataField="10" HeaderText="十" SortExpression="10" />
                                                <asp:BoundField DataField="3甲" HeaderText="三甲" SortExpression="3甲" />
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
                                        <asp:SqlDataSource ID="SDS_View1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select animal , [1]  , [2]  , [3]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10] , [3甲] From ( Select animal , highway_id , count(*) cnts From RoadKill_New  Group By animal , highway_id ) tmp PIVOT ( Sum(cnts) For tmp.highway_id In ([1]  , [2]  , [3]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10] , [3甲])  ) as pvt">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right"  ColumnSpan="2">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel1_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button1" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel1_new" OnClick="Export_Excel1_new" Title="匯出EXCEL"/>

                                        <br/><br/>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#000000">
                                    <asp:TableCell  ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label2" runat="server" Text="2013年道路致死前20名類群" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View2" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="animal" HeaderText="類群" SortExpression="animal" />
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
                                        <asp:SqlDataSource ID="SDS_View2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Top 20 a.animal ,  count(*) cnts From RoadKill_observation_new a , Roadkill_site_new b Where a.siteid = b.siteid  And DATEPART(yyyy,b.date1) = '2013' Group By a.animal Order By count(*) Desc">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label3" runat="server" Text="年月道路致死前10名類群" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View3" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="animal" HeaderText="類群" SortExpression="animal" />
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
                                        <asp:SqlDataSource ID="SDS_View3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Top 20 a.animal ,  count(*) cnts From RoadKill_observation_new a , Roadkill_site_new b Where a.siteid = b.siteid  And DATEPART(yyyy,b.date1) = '2010' And DATEPART(mm,b.date1)='06' Group By a.animal Order By count(*) Desc">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel2_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button2" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel2_new" OnClick="Export_Excel2_new" Title="匯出EXCEL"/>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel3_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button3" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel3_new" OnClick="Export_Excel3_new" Title="匯出EXCEL"/>

                                        <br/><br/>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#000000">
                                    <asp:TableCell  ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label9" runat="server" Text="年各工務段道路致死總數及累積數量" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView9" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View9" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
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
                                        <asp:SqlDataSource ID="SDS_View9" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select  SUBSTRING(site_ch,1,2) + '工務段' AS site_ch, count(*) cnts From Roadkill_site_new Where DATEPART(yyyy,date1) = '2010' And DATEPART(mm,date1)='06' Group by SUBSTRING(site_ch,1,2) Order By count(*) Desc">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label4" runat="server" Text="年月各工務段道路致死總數及累積數量" Font-Size="X-Large"></asp:Label>
                                        <br/>

                                        <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View4" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
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
                                        <asp:SqlDataSource ID="SDS_View4" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select  SUBSTRING(site_ch,1,2) + '工務段' AS site_ch, count(*) cnts From Roadkill_site_new Where DATEPART(yyyy,date1) = '2010' And DATEPART(mm,date1)='06' Group by SUBSTRING(site_ch,1,2) Order By count(*) Desc">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel9_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button7" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel9_new" OnClick="Export_Excel9_new" Title="匯出EXCEL"/>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel4_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button8" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel4_new" OnClick="Export_Excel4_new" Title="匯出EXCEL"/>

                                        <br/><br/>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#000000">
                                    <asp:TableCell  ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label5" runat="server" Text="年道路致死觀察上傳數量前10名" Font-Size="X-Large"></asp:Label>
                                        
                                        <br/>

                                        <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View5" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="collecter_ch" HeaderText="觀察者" SortExpression="collecter_ch" />
                                                <asp:BoundField DataField="cnts" HeaderText="上傳數量" SortExpression="cnts" />

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
                                        <asp:SqlDataSource ID="SDS_View5" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Top 10 a.collector_ch ,  count(*) cnts From RoadKill_observation_new a , Roadkill_site_new b Where a.siteid = b.siteid  And DATEPART(yyyy,b.date1) = '2010' Group By a.collector_ch Order By count(*) Desc">
                                        </asp:SqlDataSource>

                                        
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="450">
                                        <br/><br/>

                                        <asp:Label ID="Label8" runat="server" Text="年月道路致死觀察上傳數量前10名" Font-Size="X-Large"></asp:Label>
                                        
                                        <br/>

                                        <asp:GridView ID="GridView8" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View8" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="collecter_ch" HeaderText="觀察者" SortExpression="collecter_ch" />
                                                <asp:BoundField DataField="cnts" HeaderText="上傳數量" SortExpression="cnts" />

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
                                        <asp:SqlDataSource ID="SDS_View8" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Top 10 a.collector_ch ,  count(*) cnts From RoadKill_observation_new a , Roadkill_site_new b Where a.siteid = b.siteid  And DATEPART(yyyy,b.date1) = '2010' Group By a.collector_ch Order By count(*) Desc">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>

                                </asp:TableRow>

                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel5_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button4" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel5_new" OnClick="Export_Excel5_new" Title="匯出EXCEL"/>
                                    </asp:TableCell>

                                    <asp:TableCell HorizontalAlign="Right" >
                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel8_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button5" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel8_new" OnClick="Export_Excel8_new" Title="匯出EXCEL"/>

                                        <br/><br/>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#000000">
                                    <asp:TableCell  ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" ColumnSpan="2">
                                        <br/><br/>

                                        <asp:Label ID="Label6" runat="server" Text="常見種類統計" Font-Size="X-Large"></asp:Label>
                                        
                                        <br/>

                                        <asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View6" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="species" HeaderText="常見種類" SortExpression="species" />
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
                                        <asp:SqlDataSource ID="SDS_View6" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select a.species , count(*) cnts From RoadKill_observation_new a , Roadkill_site_new b  Where a.siteid = b.siteid Group By a.species Order By count(*) Desc">
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow BackColor="#ECFFEC">
                                    <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel6_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button6" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel6_new" OnClick="Export_Excel6_new" Title="匯出EXCEL"/>

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

