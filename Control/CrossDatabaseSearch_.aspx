﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrossDatabaseSearch_.aspx.cs" Inherits="Control_CrossDatabaseSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <a href="../newdefault.aspx">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/control/fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
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
                                 <a href="../control/CorssDatabaseSearch_.aspx" class="menu4">跨資料庫查詢</a>
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
                                <asp:Table ID="Table2" runat="server" Width="900" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell Width="649" VerticalAlign="Top">
                                            <asp:Table ID="Table_Function" runat="server" Width="640" HorizontalAlign="Center">

                                                <asp:TableRow>
                                                    <asp:TableCell Width="17">
                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="93" CssClass="title12">
                                                    空間瀏覽
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="482" CssClass="menu12a">
                                                    <span class="menu12">
                                                        <a href="CrossDatabaseSearch_.aspx" class="menu12">依國道公里數查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="CrossDatabaseSearch_02_.aspx" class="menu12">依工程處查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="CrossDatabaseSearch_03_.aspx" class="menu12">空間查詢</a>
                                                    </span> 
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            <br />

                                            <asp:Table ID="Table_Query" runat="server" Width="590" HorizontalAlign="Right">
                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4" ColumnSpan="2">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" Text="生態調查" ID="search1" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" Text="道路致死" ID="search2" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" Text="植栽工程" ID="search3" />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="1">
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4">
                                                    請輸入物種名稱 :
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="KeyWords" runat="server"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="2">
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4">
                                                    請選擇查詢國道 :
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="highway_name" DataValueField="highway_id">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="  SELECT highway_id , highway_name FROM [highway]"></asp:SqlDataSource>

                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="2">
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4">
                                                    起始里程(公里) :
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="Start_Mileston1" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50"></asp:TextBox>
                                                        <asp:Label ID="Label5" runat="server" Text="k + "></asp:Label>
                                                        <asp:TextBox ID="Start_Mileston2" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="2">
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4">
                                                    結束里程(公里) :
                                                    </asp:TableCell>
                                                    <asp:TableCell>

                                                        <asp:TextBox ID="End_Mileston1" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50"></asp:TextBox>
                                                        <asp:Label ID="Label7" runat="server" Text="k + "></asp:Label>
                                                        <asp:TextBox ID="End_Mileston2" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50"></asp:TextBox>

                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="2">
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

                                                        <asp:SqlDataSource ID="SDS_StartMonth" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '' AS MM UNION ALL Select MM From MM "></asp:SqlDataSource>
                                                        <asp:Label ID="Label2" runat="server" Text="  月至  "></asp:Label>

                                                        <asp:TextBox ID="EndYear" runat="server" Width="30"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="  年(西元)  " Width="70"></asp:Label>
                                                        <asp:DropDownList ID="EndMonth" runat="server" DataSourceID="SDS_EndMonth" DataTextField="MM" DataValueField="MM">
                                                            <asp:ListItem Value="0">無</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:SqlDataSource ID="SDS_EndMonth" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '' AS MM UNION ALL Select MM From MM "></asp:SqlDataSource>
                                                        <asp:Label ID="Label4" runat="server" Text="  月  "></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="2">
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4">
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData" />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="2">
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
                <asp:TableCell VerticalAlign="Top">
                    <asp:Table ID="Table3" runat="server" Width="1000">


                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                                <asp:Table ID="Table4" runat="server">
                                    <asp:TableRow>
                                          <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="100%">
                                            <asp:Label ID="Label6" runat="server" Text="生態調查資料"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" Width="1000">
                                            <asp:Label ID="TotalCounts" runat="server" Text=""></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" ColumnSpan="2">

                                            <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
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
                                                <PagerStyle HorizontalAlign="Center" CssClass="PagerCss" VerticalAlign="Middle" />
                                                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                                <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                                <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                                <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                            </asp:GridView>
                                            <%--<asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" SELECT distinct  a.siteid, CONVERT (char(10), a.date, 126) AS invest_date, a.highway_id, a.site_ch, Round(a.x,6) as x , Round(a.y,6) as y, a.TM2_X, a.TM2_Y, b.Short_Name , b.Chinese_Name , b.accepted_name_code , '..'+path1+file1 AS lo FROM site_new AS a INNER JOIN occurrence_new AS b ON a.siteid = b.siteid LEFT JOIN (Select Distinct free_id , Round(WGS84X,2) x , Round(WGS84Y,2) y ,Sensitive_Level From freeway_new) d ON a.highway_id = d.free_id and Round(a.x,2) = d.x and Round(a.y,2) = d.y Where 1=1 ">
                                         </asp:SqlDataSource>--%>
                                            <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=""></asp:SqlDataSource>

                                        </asp:TableCell>

                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                          <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="1000">
                                            <asp:Label ID="Label8" runat="server" Text="動物道路致死資料" ></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" Width="1000">
                                            <asp:Label ID="TotalCounts_02" runat="server" Text=""></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" ColumnSpan="2">

                                            <asp:GridView ID="GridView_List_02" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View_02" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
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
                                                    <asp:HyperLinkField DataNavigateUrlFields="id,x,y,lo" DataNavigateUrlFormatString="../view/fun02_012_.aspx?id={0}&amp;x={1}&amp;y={2}&amp;lo={3}" HeaderText="詳細資料" Target="_blank" Text="詳細資料" />

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
                                            <%--<asp:SqlDataSource ID="SDS_View_02" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select id , site_ch , highway_id , direction , replace(round(milestone,1),'00000','') as milestone  , CONVERT (char(10), date, 126) AS date , type , deduce_species , x , y  , species , Case When file1 <> '' Then  '..'+path1+file1 Else '' End AS lo , Case When file1 <> '' Then 'Y' Else '' End Pic From Roadkill_new Where 1=1 "></asp:SqlDataSource>--%>
                                            <asp:SqlDataSource ID="SDS_View_02" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                          <asp:TableCell HorizontalAlign="Left" VerticalAlign="Middle" Width="1000">
                                            <asp:Label ID="Label9" runat="server" Text="植栽工程"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle" Width="1000">
                                            <asp:Label ID="TotalCounts_03" runat="server" Text=""></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" ColumnSpan="2">

                                            <asp:GridView ID="GridView_List_03" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View_03" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="chinese_name,highway_id,plant_class,direction,start_mileage,end_mileage" DataNavigateUrlFormatString="../view/fun03_011c_.aspx?chinese_name={0}&highway_id={1}&plant_class={2}&direction={3}&start_mileage={4}&end_mileage={5}" HeaderText="中文名稱" Target="_blank" DataTextField="chinese_name" SortExpression="chinese_name" />
                                                    <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                    <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                    <asp:BoundField DataField="range" HeaderText="里程" SortExpression="range" />
                                                    <asp:BoundField DataField="plant_class" HeaderText="植栽類別" SortExpression="plant_class" />
                                                    <asp:BoundField DataField="amount" HeaderText="數量" SortExpression="amount" />
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
                                            <asp:SqlDataSource ID="SDS_View_03" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="  "></asp:SqlDataSource>

                                            <asp:GridView ID="GridView_Ori" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View_Ori" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="fun03_011d.aspx?id={0}" HeaderText="中文名稱" Target="_blank" DataTextField="chinese_name" SortExpression="chinese_name" />
                                                    <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                    <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                    <asp:BoundField DataField="range" HeaderText="里程" SortExpression="range" />
                                                    <asp:BoundField DataField="plant_class" HeaderText="植栽類別" SortExpression="plant_class" />
                                                    <asp:BoundField DataField="amount" HeaderText="數量" SortExpression="amount" />
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
                                            <asp:SqlDataSource ID="SDS_View_Ori" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="  "></asp:SqlDataSource>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#999999">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow BackColor="#ECFFEC">
                                        <asp:TableCell HorizontalAlign="Right" Visible="false">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel_new" Text="匯出EXCEL" Title="匯出EXCEL" />
                                            <asp:Button ID="Button1" runat="server" Text="匯出EXCEL" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel_new" OnClick="Export_Excel_new" Title="匯出EXCEL" />
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>


                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>

         <%--   <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                <iframe name="googlemap" src="<%=map_url%>" style="height: 800px; width: 900px;" scrolling="no" frameBorder="0"></iframe>
                </asp:TableCell>
            </asp:TableRow>--%>

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
