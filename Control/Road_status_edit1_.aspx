<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Road_status_edit1_.aspx.cs" Inherits="Control_Road_status_edit1" %>

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
</head><body>
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
                                 <a href="../Control/Road_status_edit1_.aspx" class="menu4">路權空間</a>
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
                                        <asp:Label ID="Label1" runat="server" Text="查詢關鍵字 : " Visible="false"></asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True"  Visible="false"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                                        <asp:GridView ID="GridView_List" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2"  ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver" DataKeyNames="id">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>

                                                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="Road_status_edit2_.aspx?id={0}&mode=e" HeaderText="編輯" Target="_self" Text="編輯"  />

                                                <asp:TemplateField HeaderText="刪除">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btnlink" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('是否刪除?') == false) return false;" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="id" HeaderText="流水號" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                <asp:BoundField DataField="doc_no" HeaderText="公文文號" SortExpression="doc_no" />
                                                <asp:BoundField DataField="area" HeaderText="出租使用面積(m2)" SortExpression="area" />
                                                <asp:BoundField DataField="plan_use" HeaderText="計畫用途項目" SortExpression="plan_use" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                <asp:BoundField DataField="mileage1" HeaderText="公里＋" SortExpression="mileage1" />
                                                <asp:BoundField DataField="mileage2" HeaderText="公尺" SortExpression="mileage2" />
                                                <asp:BoundField DataField="mileage" HeaderText="里程" SortExpression="mileage" />
                                                <asp:BoundField DataField="location" HeaderText="位置" SortExpression="location" />
                                                <asp:BoundField DataField="sensitive" HeaderText="敏感等級" SortExpression="sensitive" />
                                                <asp:BoundField DataField="status" HeaderText="使用情形" SortExpression="status" />
                                                <asp:BoundField DataField="institution" HeaderText="使用單位" SortExpression="institution" />
                                                <asp:BoundField DataField="start_date" HeaderText="契約開始日期" SortExpression="start_date" />
                                                <asp:BoundField DataField="end_date" HeaderText="契約結束日期" SortExpression="end_date" />
                                                <asp:BoundField DataField="owner" HeaderText="轄管單位" SortExpression="owner" />
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
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" DeleteCommand=" DELETE FROM [Road_status] WHERE [id] = @original_id " OldValuesParameterFormatString="original_{0}" SelectCommand=" SELECT [id], [highway_id], [direction], [mileage], [location], [sensitive], [status], [institution], CONVERT (char(10), [start_date], 126) AS [start_date] , CONVERT (char(10), [end_date], 126) AS  [end_date], [owner], [mileage1], [mileage2],[doc_no],[plan_use],[area] FROM [Road_status] " UpdateCommand="　UPDATE [Road_status] SET [highway_id] = @highway_id, [direction] = @direction, [mileage] = @mileage, [location] = @location, [sensitive] = @sensitive, [status] = @status, [institution] = @institution, [start_date] = @start_date, [end_date] = @end_date, [owner] = @owner, [mileage1] = @mileage1, [mileage2] = @mileage2 , [doc_no] = @doc_no , [plan_use] = @plan_use , [area] = @area , [x] = (Select Top 1 WGS84X From Freeway Where free_id = @highway_id and direction = @direction and mileage = @mileage1*1000+(@mileage2/100*100)) , [y] = (Select Top 1 WGS84Y From Freeway Where free_id = @highway_id and direction = @direction and mileage = @mileage1*1000+(@mileage2/100*100)) WHERE [id] = @original_id ">
                                            <DeleteParameters>
                                                <asp:Parameter Name="original_id" Type="Int32" />
                                                <asp:Parameter Name="original_highway_id" Type="String" />
                                                <asp:Parameter Name="original_direction" Type="String" />
                                                <asp:Parameter Name="original_mileage" Type="String" />
                                                <asp:Parameter Name="original_location" Type="String" />
                                                <asp:Parameter Name="original_sensitive" Type="String" />
                                                <asp:Parameter Name="original_status" Type="String" />
                                                <asp:Parameter Name="original_institution" Type="String" />
                                                <asp:Parameter DbType="Date" Name="original_start_date" />
                                                <asp:Parameter DbType="Date" Name="original_end_date" />
                                                <asp:Parameter Name="original_owner" Type="String" />
                                                <asp:Parameter Name="original_mileage1" Type="Int32" />
                                                <asp:Parameter Name="original_mileage2" Type="Int32" />
                                                <asp:Parameter Name="original_area" Type="Decimal" />
                                                <asp:Parameter Name="original_plan_use" Type="String" />
                                                <asp:Parameter Name="original_doc_no" Type="String" />
                                            </DeleteParameters>
            
                                            <UpdateParameters>
                                                <asp:Parameter Name="highway_id" Type="String" />
                                                <asp:Parameter Name="direction" Type="String" />
                                                <asp:Parameter Name="mileage" Type="String" />
                                                <asp:Parameter Name="location" Type="String" />
                                                <asp:Parameter Name="sensitive" Type="String" />
                                                <asp:Parameter Name="status" Type="String" />
                                                <asp:Parameter Name="institution" Type="String" />
                                                <asp:Parameter DbType="Date" Name="start_date" />
                                                <asp:Parameter DbType="Date" Name="end_date" />
                                                <asp:Parameter Name="owner" Type="String" />
                                                <asp:Parameter Name="mileage1" Type="Int32" />
                                                <asp:Parameter Name="mileage2" Type="Int32" />
                                                <asp:Parameter Name="area" Type="Decimal" />
                                                <asp:Parameter Name="plan_use" Type="String" />
                                                <asp:Parameter Name="doc_no" Type="String" />
                                                <asp:Parameter Name="original_id" Type="Int32" />
                                                <asp:Parameter Name="original_highway_id" Type="String" />
                                                <asp:Parameter Name="original_direction" Type="String" />
                                                <asp:Parameter Name="original_mileage" Type="String" />
                                                <asp:Parameter Name="original_location" Type="String" />
                                                <asp:Parameter Name="original_sensitive" Type="String" />
                                                <asp:Parameter Name="original_status" Type="String" />
                                                <asp:Parameter Name="original_institution" Type="String" />
                                                <asp:Parameter DbType="Date" Name="original_start_date" />
                                                <asp:Parameter DbType="Date" Name="original_end_date" />
                                                <asp:Parameter Name="original_owner" Type="String" />
                                                <asp:Parameter Name="original_mileage1" Type="Int32" />
                                                <asp:Parameter Name="original_mileage2" Type="Int32" />
                                                <asp:Parameter Name="original_area" Type="Decimal" />
                                                <asp:Parameter Name="original_plan_use" Type="String" />
                                                <asp:Parameter Name="original_doc_no" Type="String" />
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
