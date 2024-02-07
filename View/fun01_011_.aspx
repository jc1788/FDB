<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fun01_011_.aspx.cs" Inherits="View_fun_011" %>

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
                                 <a href="fun01_011_.aspx" class="menu4">生態調查資料</a> &gt;  
                                 <a href="fun01_011_.aspx" class="menu4">物種瀏覽</a> 
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
                                                    <span class="menu13">
                                                        <a href="fun01_011_.aspx" class="menu13">依物種查詢</a>
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
                                                        <a href="fun01_021a_.aspx" class="menu12">依國道公里數查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="fun01_021b_.aspx" class="menu12">依工程分局查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="fun01_021e_.aspx" class="menu12">空間查詢</a>
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
                                                    <asp:TextBox ID="chinese_name" runat="server" ></asp:TextBox>
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
                                                <asp:DropDownList ID="Group" runat="server" DataSourceID="SqlDataSource2" DataTextField="title" DataValueField="title">
                                                    <asp:ListItem Value="0">全部物種</asp:ListItem>
                                                </asp:DropDownList>
                    
                                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' title UNION ALL SELECT title FROM [Animal]"></asp:SqlDataSource>
                                                
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
                                                    資料來源 :
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <select id="DataSource" runat="server" name="DataSource">
                                                        <option value ="0">ALL</option>
                                                        <option value ="1">高公局</option>
                                                        <option value ="2">特生</option>
                                                    </select>
                                              <%--  <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SDS_species" DataTextField="species_Name" DataValueField="species">
                                                    <asp:ListItem Value="0">全部物種</asp:ListItem>
                                                </asp:DropDownList>--%>
                                                <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT '' AS species , 'ALL' AS species_Name  UNION ALL Select species , species_name From  Focus_Species "></asp:SqlDataSource>--%>
                                                
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    調查地點 :
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="tb_site_ch" runat="server" ></asp:TextBox>
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
                                        <%
                                            string start_ym = "";
                                            start_ym = StartYear.Text + StartMonth.Text;

                                            string end_ym = "";
                                            end_ym = EndYear.Text + EndMonth.Text;
                                        %>
                                        <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="url" DataNavigateUrlFormatString="fun01_012_.aspx?Short_Name={0}"  HeaderText="中文名" Text="點我" Target="_blank" DataTextField="Chinese_Name" SortExpression="Chinese_Name" />
                                                <asp:BoundField DataField="Short_Name" HeaderText="學名" SortExpression="Short_Name"  />
                                                <asp:BoundField DataField="Group" HeaderText="類群" SortExpression="Group" />
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
                                        <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select DISTINCT a.Chinese_name , a.Short_Name , a.[Group] ,a.Short_Name + '&StartDate=&EndDate=' AS url  From occurrence_new a , site_new b Where a.siteid= b.siteid "></asp:SqlDataSource>

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
