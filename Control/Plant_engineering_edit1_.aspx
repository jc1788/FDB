<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plant_engineering_edit1_.aspx.cs" Inherits="Control_Plant_engineering_edit1" %>

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
     <link href="fun01_011.aspx_files/style1.css" rel="stylesheet" type="text/css" />
    <script src="fun01_011.aspx_files/WebResource.js" type="text/javascript"></script>
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
                                 <a href="../newdefault.aspx" class="menu4">單筆修改</a> &gt;
                                 <a href="../Control/Plant_engineering_edit1_.aspx" class="menu4">植栽工程資料</a>
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

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:Label ID="Label_Keywords" runat="server" Text="查詢關鍵字 : " Visible="true"></asp:Label>
                                        <asp:TextBox ID="Keywords" runat="server" Visible="true"></asp:TextBox>
                                        <asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData"/>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                                        <asp:GridView ID="GridView_List" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2"  ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="XX-Large" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver" DataKeyNames ="pid">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="pid" DataNavigateUrlFormatString="Plant_engineering_edit2_.aspx?pid={0}&mode=e" HeaderText="編輯" Target="_self" Text="編輯"  />

                                                <asp:TemplateField HeaderText="刪除">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btnlink" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('是否刪除?') == false) return false;" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:HyperLinkField DataNavigateUrlFields="pid" DataNavigateUrlFormatString="Plant_observation_edit1_.aspx?pid={0}" HeaderText="編輯明細" Text="點我" Target="_blank" DataTextField="pid" SortExpression="pid" />
                                                <asp:BoundField DataField="plant_id" HeaderText="編號" SortExpression="plant_id" />
                                                <asp:BoundField DataField="number" HeaderText="標別" SortExpression="number" />
                                                <asp:BoundField DataField="plant_name" HeaderText="計畫名稱" SortExpression="plant_name" />
                                                <asp:BoundField DataField="institution" HeaderText="設計單位" SortExpression="institution" />
                                                <asp:BoundField DataField="design_date" HeaderText="設計定稿日期" SortExpression="design_date" />
                                                <asp:BoundField DataField="work_date" HeaderText="竣工日期" SortExpression="work_date" />
                                                <asp:BoundField DataField="maintain" HeaderText="維護單位" SortExpression="maintain" />
                                                <asp:BoundField DataField="cnts" HeaderText="明細筆數" SortExpression="cnts" />
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
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" DeleteCommand="DELETE FROM [Plant_engineering] WHERE [pid] = @pid" SelectCommand=" SELECT a.[pid], [plant_id], [number], [plant_name], [institution], CONVERT (char(10), [design_date], 126) AS [design_date], CONVERT (char(10), [work_date], 126) AS [work_date], [maintain] , Case when b.cnts is null then 0 else b.cnts End As cnts From [Plant_engineering] a Left Join (Select pid , count(*) cnts From Plant_observation GROUP BY pid) b on a.pid = b.pid" UpdateCommand="UPDATE [Plant_engineering] SET [plant_id] = @plant_id, [number] = @number, [plant_name] = @plant_name, [institution] = @institution, [design_date] = @design_date, [work_date] = @work_date, [maintain] = @maintain WHERE [pid] = @pid">
                                            <DeleteParameters>
                                                <asp:Parameter Name="pid" Type="Int32" />
                                            </DeleteParameters>
            
                                            <UpdateParameters>
                                                <asp:Parameter Name="plant_id" Type="String" />
                                                <asp:Parameter Name="number" Type="String" />
                                                <asp:Parameter Name="plant_name" Type="String" />
                                                <asp:Parameter Name="institution" Type="String" />
                                                <asp:Parameter DbType="Date" Name="design_date" />
                                                <asp:Parameter DbType="Date" Name="work_date" />
                                                <asp:Parameter Name="maintain" Type="String" />
                                                <asp:Parameter Name="pid" Type="Int32" />
                                            </UpdateParameters>
                                        </asp:SqlDataSource>
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
