<%@ Page Language="C#" CodeFile="Road_status_add1_.aspx.cs" Inherits="Control_Road_status_add1" %>

<!DOCTYPE html>


<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar-setup.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/lang/calendar-tc.js"></script>

<link type="text/css" href="../Scripts/jscalendar-1.0/calendar-blue.css" rel="Stylesheet" />


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
    function MM_swapImgRestore() { //v3.0
        var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
    }
    function MM_findObj(n, d) { //v4.01
        var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
            d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
        }
        if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
        for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
        if (!x && d.getElementById) x = d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
            if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
    }

    function swap_data(rowid) { //v3.0
        if (rowid == 'data1') {
            document.data1.Visible = "true";
            document.data2.Visible = "false";
            document.data3.Visible = "false";
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
                                 <a href="../newdefault.aspx" class="menu4">資料上傳</a> &gt;  
                                 <a href="../Control/Road_status_add1_.aspx" class="menu4">路權空間使用情形</a>
                                 
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7" VerticalAlign="Top" >
                <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top" >
                            <asp:Table ID="Table_Detail" runat="server" Width="900" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2" HorizontalAlign="Left">
                                        路權空間使用情形
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#FFFFFF" Width="900" Height="20">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell Width="300" HorizontalAlign="Right">
                                        <asp:Label ID="Label16" runat="server" Text="國道里程："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:DropDownList ID="highway_id" runat="server" DataSourceID="SqlDataSource1" DataTextField="highway_name" DataValueField="highway_id" autopostback="true"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT * FROM [Highway]"></asp:SqlDataSource>
                                        &nbsp;&nbsp;
                    
                                        <asp:DropDownList ID="direction" runat="server" DataSourceID="SqlDataSource3" DataTextField="direction" DataValueField="direction" autopostback="True"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT [direction] FROM [Highway_direction] WHERE ([highway_id] = @highway_id)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="highway_id" Name="highway_id" PropertyName="SelectedValue" Type="string" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        &nbsp;&nbsp;

                                        <asp:TextBox ID="range" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                        &nbsp;k+&nbsp;
                                        <asp:TextBox ID="milestone" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                        至
                                        <asp:TextBox ID="range2" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                        &nbsp;k+&nbsp;
                                        <asp:TextBox ID="milestone2" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_maintain" runat="server" Text="維護單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:DropDownList ID="maintain1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource4" DataTextField="class1" DataValueField="class1"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 0 AS id , 'ALL' AS  class1 UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY id"></asp:SqlDataSource>
                                        <asp:DropDownList ID="maintain2" runat="server" AppendDataBoundItems="False" AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="class2" DataValueField="class2">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT DISTINCT class2 FROM [Engineering_road] Where (class1=@class1) ORDER BY class2">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="maintain1" Name="class1" PropertyName="SelectedValue" Type="string" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_doc_no" runat="server" Text="公文文號："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="doc_no" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_location" runat="server" Text="位置："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="location" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_area" runat="server" Text="出租使用面積(m2)："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="area" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_plan_use" runat="server" Text="計劃用途項目："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="plan_use" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_sensitive" runat="server" Text="敏感等級："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="sensitive" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_status" runat="server" Text="使用情形："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:DropDownList ID="status" runat="server" DataSourceID="SqlDataSource2" DataTextField="status" DataValueField="status"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT status FROM [Use_Status] "></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_institution" runat="server" Text="使用單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="institution" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_year" runat="server" Text="年度："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="year" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_Date" runat="server" Text="契約期間："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="start_date" runat="server" ReadOnly="false" Enabled="true" CausesValidation="true" BackColor="silver"></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar1" runat="server" title="小日曆"  ImageUrl="~/Images/icon_date.jpg" />　－　
                                        <script ="javascript" type="text/javascript">
                                            Calendar.setup({
                                                inputField: "start_date",
                                                ifFormat: "%Y-%m-%d",
                                                button: "imgCalendar1",
                                                align: "Tl",
                                                singleClick: true
                                            });
                                        </script>

                                        <asp:TextBox ID="end_date" runat="server" ReadOnly="false" Enabled="true" CausesValidation="true" BackColor="silver"></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar2" runat="server" title="小日曆"  ImageUrl="~/Images/icon_date.jpg" />
                                        <script ="javascript" type="text/javascript">
                                            Calendar.setup({
                                                inputField: "end_date",
                                                ifFormat: "%Y-%m-%d",
                                                button: "imgCalendar2",
                                                align: "Tl",
                                                singleClick: true
                                            });
                                        </script>
                                        
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_owner" runat="server" Text="轄管單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="owner" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_land1" runat="server" Text="縣市："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="land1" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_land2" runat="server" Text="鄉鎮："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="land2" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_land3" runat="server" Text="段："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="land3" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_land4" runat="server" Text="地號："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="land4" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_notes" runat="server" Text="備註："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="notes" runat="server" TextMode="MultiLine" Width="500" Rows="3"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right">
                                        <asp:Label ID="Label_file1" runat="server" Text="相關文件(契約書)："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:FileUpload ID="file1" runat="server" Width="400"/><asp:Label ID="Label2" runat="server" Text="(檔案類型：PDF)"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="300">
                                    <asp:TableCell HorizontalAlign="Right"/>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:Button ID="Add_Road_status" runat="server" Text="新增資料" OnClick="Add_Road_status_Click" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow VerticalAlign="Top" BackColor="#a7a7a7">
            <asp:TableCell BackColor="#a7a7a7">
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                
                <asp:Table ID="Table3" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
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
