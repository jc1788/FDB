<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" CodeFile="fun11_01_.aspx.cs" Inherits="View_fun11_01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">



<script type="text/javascript">

    function Navigate(url) {

        javascript: window.open("http://localhost:49989/freeway2(old)/"+url);

    }

</script>

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
                                 <a href="fun11_01_.aspx" class="menu4">受傷動物通報查詢</a>
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
                                        <asp:Table ID="Table_Query" runat="server" Width="590" HorizontalAlign="Right" >
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    關鍵字 :
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
                                                <asp:BoundField DataField="highway_id" HeaderText=" 國道編號 " SortExpression="highway_id" />
                                                <asp:BoundField DataField="mileage" HeaderText=" 里程 " SortExpression="mileage" />
                                                <asp:BoundField DataField="date" HeaderText=" 發現日期 " SortExpression="date" />
                                                <asp:BoundField DataField="time" HeaderText=" 發現時間 " SortExpression="time" />
                                                <asp:BoundField DataField="species" HeaderText=" 可能種類 " SortExpression="species" />
                                                <asp:BoundField DataField="owner" HeaderText=" 工程處 " SortExpression="owner" />
                                                <asp:BoundField DataField="reporter" HeaderText=" 通報單位 " SortExpression="reporter" />
                                                <asp:BoundField DataField="status" HeaderText=" 狀態 " SortExpression="institution" />

                                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="fun11_02_.aspx?id={0}" HeaderText="詳細資料" Target="_blank" Text="詳細資料" ItemStyle-Width="10%" />

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
                                        <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select a.id , CONVERT (char(10), a.date, 126) AS date , CONVERT(CHAR(12), a.time, 114) AS time , a.highway_id , a.mileage  , a.reporter , a.institution , a.species , a.status , Case When a.file1 <> '' then a.path1+a.file1 Else file1 End file1 , a.path1 , a.owner From Roadkill_bulletin a Where 1=1">
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
