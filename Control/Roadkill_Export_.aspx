<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation = "false" CodeFile="Roadkill_Export_.aspx.cs" Inherits="Control_Roadkill_Export" %>

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
                    <asp:Image ID="Image1" runat="server" ImageUrl="fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
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
                                 <a href="../Control/Roadkill_Export_.aspx" class="menu4">道路致死匯出</a>
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
                                        <span class="menu13">道路致死資料匯出</span> 
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell>&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel_new" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button1" runat="server" Text="匯出道路致死新增資料(Excel)" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel" OnClick="Export_Excel_new" Title="匯出道路致死新增資料" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <%-- asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif"/ --%>
                                             <!-- a href="../Attachments/Batch_Upload/Roadkill_2015-1-26.zip" class="menu6">匯出道路致死歷史資料</a -->
                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel_History_new2" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button4" runat="server" Text="匯出道路致死歷史資料(Excel)" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel_History" OnClick="Export_Excel_History_new2" Title="匯出道路致死歷史資料" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell>&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Csv_new" Text="匯出CSV" Title="匯出CSV"/>
                                        <asp:Button ID="Button3" runat="server" Text="匯出道路致死新增資料(csv)" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Csv_new" OnClick="Export_Csv_new" Title="匯出道路致死新增資料" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel_History_new" Text="匯出CSV" Title="匯出CSV"/>
                                        <asp:Button ID="Button5" runat="server" Text="匯出道路致死歷史資料(csv for open data)" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel_History" OnClick="Export_Excel_History_new" Title="匯出道路致死歷史資料" />
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow HorizontalAlign="Left">
                                    <asp:TableCell>&nbsp;&nbsp;
                                       <%-- <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/icon/icon_XLS.gif" OnClick="Export_Excel" Text="匯出EXCEL" Title="匯出EXCEL"/>
                                        <asp:Button ID="Button2" runat="server" Text="匯出道路致死新增資料(csv)" Class="menu6" BackColor="#ECFFEC" BorderWidth="0px" CommandName="Export_Excel" OnClick="Export_Excel_new" Title="匯出道路致死新增資料" />--%>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                    </asp:TableCell>
                                    <asp:TableCell>&nbsp;
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                
                                        <asp:GridView ID="GridView_List" runat="server" DataSourceID="SqlDataSource1" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2"  ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver" DataKeyNames="id">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" InsertVisible="False" ReadOnly="True"></asp:BoundField>
                                                <asp:BoundField DataField="site_ch" HeaderText="工務段" SortExpression="site_ch" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                <asp:BoundField DataField="milestone" HeaderText="里程" SortExpression="milestone" />
                                                <asp:BoundField DataField="x" HeaderText="經度" SortExpression="x" />
                                                <asp:BoundField DataField="y" HeaderText="緯度" SortExpression="y" />
                                                <asp:BoundField DataField="date" HeaderText="日期" SortExpression="date" />
                                                <asp:BoundField DataField="range" HeaderText="範圍" SortExpression="range" />
                                                <asp:BoundField DataField="type" HeaderText="工作類別" SortExpression="type" />
                                                <asp:BoundField DataField="weather" HeaderText="天氣" SortExpression="weather" />
                                                <asp:BoundField DataField="animal" HeaderText="動物類群" SortExpression="animal" />
                                                <asp:BoundField DataField="detail_animal" HeaderText="大類" SortExpression="animal" />
                                                <asp:BoundField DataField="animal2" HeaderText="詳細類群" SortExpression="animal2" />
                                                <asp:BoundField DataField="collecter_ch" HeaderText="調查者" SortExpression="collecter_ch" />
                                                <asp:BoundField DataField="species" HeaderText="可能種類" SortExpression="species" />
                                                <asp:BoundField DataField="deduce_species" HeaderText="可能種類推測" SortExpression="deduce_species" />
                                                <asp:BoundField DataField="upload_file" HeaderText="上傳檔名" SortExpression="upload_file" />
                                                <asp:BoundField DataField="upload_date" HeaderText="上傳日期" SortExpression="upload_date" />
                                                <asp:BoundField DataField="transfer" HeaderText="後送" SortExpression="transfer" />
                                                <asp:BoundField DataField="note" HeaderText="備註" SortExpression="note" />
                                                <asp:BoundField DataField="file1" HeaderText="照片1" SortExpression="file1" />
                                                <asp:BoundField DataField="file1_url" HeaderText="照片1路徑" SortExpression="file1_url" />
                                                <asp:BoundField DataField="file2" HeaderText="照片2" SortExpression="file1" />
                                                <asp:BoundField DataField="file2_url" HeaderText="照片2路徑" SortExpression="file1_url" />
                                                <asp:BoundField DataField="file3" HeaderText="照片3" SortExpression="file1" />
                                                <asp:BoundField DataField="file3_url" HeaderText="照片3路徑" SortExpression="file1_url" />
                                                <asp:BoundField DataField="file4" HeaderText="照片4" SortExpression="file1" />
                                                <asp:BoundField DataField="file4_url" HeaderText="照片4路徑" SortExpression="file1_url" />
                                                <asp:BoundField DataField="file5" HeaderText="照片5" SortExpression="file1" />
                                                <asp:BoundField DataField="file5_url" HeaderText="照片5路徑" SortExpression="file1_url" />
                                                <asp:BoundField DataField="image_file" HeaderText="照片壓縮檔名" SortExpression="file1" />
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
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>"  SelectCommand=" " >
                                            <DeleteParameters>
                                                <asp:Parameter Name="id" Type="Int32" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
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
