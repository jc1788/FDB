<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plant_observation_edit5_.aspx.cs" Inherits="Control_Plant_observation_edit5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <link href="fun01_011.aspx_files/style1.css" rel="stylesheet" type="text/css" />
    <script src="fun01_011.aspx_files/WebResource.js" type="text/javascript"></script>
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
                                 <a href="../newdefault.aspx" class="menu4">單筆修改</a> &gt;
                                 <a href="../Control/Plant_observation_edit5_.aspx" class="menu4">植栽資料</a>
                                 
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
                                                    請輸入物種名稱 :
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="KeyWords" runat="server" Text="" Width="300"></asp:TextBox>
                                                    <asp:CheckBox ID="QueryType" runat="server" />
                                                    模糊查詢
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    請選擇查詢國道 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:DropDownList ID="highway_id" runat="server" DataSourceID="SqlDataSource1" DataTextField="highway_name" DataValueField="highway_id" autopostback="true">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" SELECT '' AS highway_id , '' AS highway_name UNION ALL SELECT highway_id , highway_name FROM [highway]"></asp:SqlDataSource>

                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    請選擇方向 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:DropDownList ID="direction" runat="server" DataSourceID="SqlDataSource3" DataTextField="direction" DataValueField="direction" autopostback="True"></asp:DropDownList>
                                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" SELECT '' AS direction UNION ALL SELECT  [direction] FROM [Highway_direction] WHERE ([highway_id] = @highway_id)">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="highway_id" Name="highway_id" PropertyName="SelectedValue" Type="string" />
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
                                                    起始里程(公里) :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:TextBox ID="Start_Mileston1" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50px"></asp:TextBox>
                                                    <asp:Label ID="Label5" runat="server" Text="k + "></asp:Label>
                                                    <asp:TextBox ID="Start_Mileston2" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50px"></asp:TextBox>

                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4">
                                                    結束里程(公里) :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:TextBox ID="End_Mileston1" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50px"></asp:TextBox>
                                                    <asp:Label ID="Label7" runat="server" Text="k + "></asp:Label>
                                                    <asp:TextBox ID="End_Mileston2" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Width="50px"></asp:TextBox>

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
                                                <asp:HyperLinkField DataNavigateUrlFields="chinese_name,highway_id,plant_class,direction,start_mileage,end_mileage" DataNavigateUrlFormatString="Plant_observation_edit6_.aspx?chinese_name={0}&highway_id={1}&plant_class={2}&direction={3}&start_mileage={4}&end_mileage={5}" HeaderText="中文名稱" Target="_self" DataTextField="chinese_name" SortExpression="chinese_name" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                <asp:BoundField DataField="range" HeaderText="里程" SortExpression="range" />
                                                <asp:BoundField DataField="plant_class" HeaderText="植栽類別" SortExpression="plant_class" />
                                                <asp:BoundField DataField="amount" HeaderText="數量" SortExpression="amount" />
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
                                        <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="  ">
                                        </asp:SqlDataSource>

                                         <asp:GridView ID="GridView_Ori" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View_Ori" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"  Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver" DataKeyNames="id">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="id,pid" DataNavigateUrlFormatString="Plant_observation_edit4.aspx?id={0}&pid={1}" HeaderText="編輯" Target="_blank" Text="編輯"  />

                                                <asp:TemplateField HeaderText="刪除">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btnlink" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('是否刪除?') == false) return false;" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="chinese_name" HeaderText="中文名稱" SortExpression="chinese_name" />
                                                <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
                                                <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
                                                <asp:BoundField DataField="range" HeaderText="里程" SortExpression="range" />
                                                <asp:BoundField DataField="plant_class" HeaderText="植栽類別" SortExpression="plant_class" />
                                                <asp:BoundField DataField="amount" HeaderText="數量" SortExpression="amount" />
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
                                        <asp:SqlDataSource ID="SDS_View_Ori" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" DeleteCommand="Begin Update Plant_observation Set Delete_Flag = 'Y'  WHERE [id] = @id; Insert Into Plant_observation_log (uid , plant_observation_id , update_date) Values(@uid,@id,getdate());End;">
                                            <DeleteParameters>
                                                <asp:Parameter Name="id" Type="Int32" />
                                                <asp:sessionparameter name="uid" sessionfield="uid" type="Int32" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
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
