<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="fun02_011b_.aspx.cs" Inherits="View_fun02_011b" %>

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
                                 <a href="fun02_011a_.aspx" class="menu4">道路致死資料</a> &gt;  
                                 <a href="fun02_011a_.aspx" class="menu4">空間瀏覽</a> 
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
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                                </asp:TableCell>
                                                <asp:TableCell Width="93" CSSClass="title12">
                                                    空間瀏覽
                                                </asp:TableCell>
                                                <asp:TableCell Width="482" CSSClass="menu12a">
                                                    <span class="menu12">
                                                        <a href="fun02_011a_.aspx" class="menu12">依國道公里數查詢</a>
                                                    </span> | 
                                                    <span class="menu13">
                                                        <a href="fun02_011b_.aspx" class="menu13">依工程分局查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="fun02_011e_.aspx" class="menu12">空間查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="http://tmap.geospatial.org.tw/tie/index1.html" class="menu12">圖台查詢</a>
                                                    </span> 
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br/>
                                        <asp:Table ID="Table_Query" runat="server" Width="590" HorizontalAlign="Right" >
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    請輸入物種名稱 :
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="KeyWords" runat="server" Text="" Width="400"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    請選擇物種類群 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="TITLE_TEXT" DataValueField="TITLE_VALUE" >
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' AS TITLE_VALUE , 'ALL' AS TITLE_TEXT UNION ALL Select Distinct Animal AS TITLE_VALUE , CASE WHEN ANIMAL = '' THEN '無法辨識' ELSE ANIMAL END AS TITLE_TEXT From Roadkill_New "></asp:SqlDataSource>

                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    請選擇查詢工程分局 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="class1" DataValueField="class1">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 0 AS id , 'ALL' AS  class1 UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY class1"></asp:SqlDataSource>

                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    請選擇查詢工務段 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource4" DataTextField="class2" DataValueField="class2">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' as class2, 0 as position UNION ALL (SELECT DISTINCT class2, position FROM [Engineering_road] Where (class1=@class1)) order by position">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="DropDownList2" Name="class1" PropertyName="SelectedValue" Type="string" />
                                                         </SelectParameters>
                                                    </asp:SqlDataSource>

                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    敏感里程等級 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource3" DataTextField="Sensitive_level" DataValueField="Sensitive_level">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' Sensitive_level UNION ALL SELECT DISTINCT Sensitive_level FROM freeway"></asp:SqlDataSource>

                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    關注物種 :
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                <asp:DropDownList ID="species" runat="server" DataSourceID="SDS_species" DataTextField="species_Name" DataValueField="species">
                                                    <asp:ListItem Value="0">全部物種</asp:ListItem>
                                                </asp:DropDownList>
                    
                                                <asp:SqlDataSource ID="SDS_species" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT '' AS species , 'ALL' AS species_Name  UNION ALL Select species , species_name From  Focus_Species "></asp:SqlDataSource>
                                                
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    資料期間 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:TextBox ID="StartYear" runat="server" Width="30"></asp:TextBox><asp:Label ID="Label1" runat="server" Text="  年(西元)  " Width="70"></asp:Label>
                                                    <asp:DropDownList ID="StartMonth" runat="server" DataSourceID="SDS_StartMonth" DataTextField="MM" DataValueField="MM">
                                                        <asp:ListItem Value="0">無</asp:ListItem>
                                                    </asp:DropDownList>
                    
                                                    <asp:SqlDataSource ID="SDS_StartMonth" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select * From MM "></asp:SqlDataSource>
                                                    <asp:Label ID="Label2" runat="server" Text="  月至  "></asp:Label>

                                                    <asp:TextBox ID="EndYear" runat="server" Width="30"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="  年(西元)  " Width="70"></asp:Label>
                                                    <asp:DropDownList ID="EndMonth" runat="server" DataSourceID="SDS_EndMonth" DataTextField="MM" DataValueField="MM">
                                                        <asp:ListItem Value="0">無</asp:ListItem>
                                                    </asp:DropDownList>
                    
                                                    <asp:SqlDataSource ID="SDS_EndMonth" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select * From MM "></asp:SqlDataSource>
                                                    <asp:Label ID="Label4" runat="server" Text="  月  "></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData"/>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
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
                                                <asp:BoundField DataField="site_ch" HeaderText="工務段" ReadOnly="True" SortExpression="site_ch" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                <asp:BoundField DataField="milestone" HeaderText="國道里程" SortExpression="milestone" />
                                                <asp:BoundField DataField="date" HeaderText="日期" SortExpression="date" />
                                                <asp:BoundField DataField="type" HeaderText="工作類別" SortExpression="type" />
                                                <asp:BoundField DataField="species" HeaderText="可能種類" SortExpression="species" />
                                                <asp:BoundField DataField="deduce_species" HeaderText="可能種類推測" SortExpression="deduce_species" />
                                                <asp:BoundField DataField="Pic" HeaderText="照片" SortExpression="Pic" />
                                                <asp:BoundField DataField="varify" HeaderText="校對" SortExpression="varify" />
                                                <asp:HyperLinkField DataNavigateUrlFields="id,x,y,lo" DataNavigateUrlFormatString="fun02_012_.aspx?id={0}&amp;x={1}&amp;y={2}&amp;lo={3}" HeaderText="詳細資料" Target="_blank" Text="詳細資料" />

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
                                        <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic, varify From v_Roadkill_new Where 1=1 ">
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
                                        <asp:Button ID="Button1" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel_new" OnClick="Export_Excel_new" Title="匯出EXCEL"/>
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
                <iframe name="googlemap" src="<%=map_url%>" style="height: 800px; width: 900px;" scrolling="no" frameBorder="0"></iframe>
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
