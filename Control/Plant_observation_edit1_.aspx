<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plant_observation_edit1_.aspx.cs" Inherits="Control_Plant_observation_edit1" %>

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
                                 <a href="../Control/Plant_engineering_edit1_.aspx" class="menu4">植栽資料</a>
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
                                                <asp:HyperLinkField DataNavigateUrlFields="id,pid" DataNavigateUrlFormatString="Plant_observation_edit4_.aspx?id={0}&pid={1}" HeaderText="編輯" Target="_self" Text="編輯"  />

                                                <asp:TemplateField HeaderText="刪除">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btnlink" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('是否刪除?') == false) return false;" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                
                                                <asp:BoundField DataField="chinese_name" HeaderText="植物中文名" SortExpression="chinese_name" />
                                                <asp:BoundField DataField="ScientificName" HeaderText="植物學名" SortExpression="ScientificName" />
                                                <asp:BoundField DataField="high" HeaderText="高度" SortExpression="high" />
                                                <asp:BoundField DataField="width" HeaderText="幅寬" SortExpression="width" />
                                                <asp:BoundField DataField="m_high" HeaderText="米高徑" SortExpression="m_high" />
                                                <asp:BoundField DataField="amount" HeaderText="數量" SortExpression="amount" />
                
                                                <asp:BoundField DataField="location" HeaderText="位置" SortExpression="location" />
                                                <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                <asp:BoundField DataField="range" HeaderText="里程範圍" SortExpression="range" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="end_mileage1" HeaderText="開始公里" SortExpression="end_mileage1" />
                                                <asp:BoundField DataField="end_mileage2" HeaderText="開始公尺" SortExpression="end_mileage2" />
                                                <asp:BoundField DataField="start_mileage1" HeaderText="結束公里" SortExpression="start_mileage1" />
                                                <asp:BoundField DataField="start_mileage2" HeaderText="結束公尺" SortExpression="start_mileage2" />
                        
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
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" DeleteCommand="DELETE FROM [Plant_observation] WHERE [id] = @id"  SelectCommand="SELECT [id], [chinese_name], [ScientificName], [high], [width], [m_high], [amount], [pid], [location], [direction], [range], [highway_id], [end_mileage1], [end_mileage2], [start_mileage1], [start_mileage2] FROM [Plant_observation] WHERE ([pid] = @pid)" >
                                            <DeleteParameters>
                                                <asp:Parameter Name="id" Type="Int32" />
                                            </DeleteParameters>
            
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="pid" QueryStringField="pid" Type="Int32" />
                                            </SelectParameters>
                                            <UpdateParameters>
                                                <asp:Parameter Name="chinese_name" Type="String" />
                                                <asp:Parameter Name="ScientificName" Type="String" />
                                                <asp:Parameter Name="high" Type="Double" />
                                                <asp:Parameter Name="width" Type="Double" />
                                                <asp:Parameter Name="m_high" Type="Double" />
                                                <asp:Parameter Name="amount" Type="Int32" />
                                                <asp:Parameter Name="pid" Type="Int32" />
                                                <asp:Parameter Name="location" Type="String" />
                                                <asp:Parameter Name="direction" Type="String" />
                                                <asp:Parameter Name="range" Type="String" />
                                                <asp:Parameter Name="highway_id" Type="String" />
                                                <asp:Parameter Name="end_mileage1" Type="Int32" />
                                                <asp:Parameter Name="end_mileage2" Type="Int32" />
                                                <asp:Parameter Name="start_mileage1" Type="Int32" />
                                                <asp:Parameter Name="start_mileage2" Type="Int32" />
                                                <asp:Parameter Name="id" Type="Int32" />
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
