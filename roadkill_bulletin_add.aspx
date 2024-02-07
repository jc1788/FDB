<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" Inherits="Control_Roadkill_bulletin_add1, App_Web_0o05krds" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar-setup.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/lang/calendar-tc.js"></script>
<link type="text/css" href="../Scripts/jscalendar-1.0/calendar-blue.css" rel="Stylesheet" />

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
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">受傷動物通報</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">受傷動物通報</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <!-- /.標題+麵包屑 -->
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-search01"
                        role="tab" aria-controls="nav-home" aria-selected="true">資料新增</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="index.aspx">取消通報</a></p>

    <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
         <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7" VerticalAlign="Top">
                <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">
                            <asp:Table ID="Table_Detail" runat="server" Width="900" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow BackColor="#FFFFFF" Width="900" Height="20">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_id" runat="server" Text="案件編號："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="id" runat="server" Text="" BorderStyle="NotSet" CssClass="explain4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label3" runat="server" Text="工程處 : "></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:DropDownList ID="owner" runat="server" DataSourceID="SqlDataSource3" DataTextField="class1" DataValueField="class1"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Top 1 id , class1  FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY id"></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_date" runat="server" Text="發現日期："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="date" runat="server" ReadOnly="false" Enabled="true" CausesValidation="true" BackColor="silver"></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar1" runat="server" title="小日曆"  ImageUrl="~/Images/icon_date.jpg" Width="15" Height="15"/> 
                                        <script ="javascript" type="text/javascript">
                                            Calendar.setup({
                                                inputField: "MainContentPlaceHolder_date",
                                                ifFormat: "%Y-%m-%d",
                                                button: "imgCalendar1",
                                                align: "Tl",
                                                singleClick: true
                                            });
                                        </script>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_time" runat="server" Text="發現時間："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:DropDownList ID="time" runat="server" DataSourceID="SqlDataSource2" DataTextField="hours_duration" DataValueField="hours_duration"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT Hours_duration FROM Hours_duration"></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label4" runat="server" Text="發現地點："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:DropDownList ID="highway_id" runat="server" DataSourceID="SqlDataSource1" DataTextField="highway_name" DataValueField="highway_id"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT * FROM [Highway]"></asp:SqlDataSource>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="range" runat="server" Width="50" autopostback="false" Text="0"></asp:TextBox>
                                        &nbsp;k+&nbsp;
                                        <asp:TextBox ID="milestone" runat="server" Width="50" autopostback="false" Text="0"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_reporter" runat="server" Text="通報人員："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="reporter" runat="server" Text="" BorderStyle="NotSet" CssClass="explain4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_institution" runat="server" Text="通報單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="institution" runat="server" Text="" BorderStyle="NotSet" CssClass="explain4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_species" runat="server" Text="可能種類："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="species" runat="server" Text="" BorderStyle="NotSet" CssClass="explain4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_status" runat="server" Text="後續安置："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:TextBox ID="status" runat="server" Text="" BorderStyle="NotSet" CssClass="explain4"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label9" runat="server" Text="照片上傳："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:FileUpload ID="file1" runat="server" Width="500"/><asp:Label ID="Label2" runat="server" ></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300"/>
                                    <asp:TableCell Width="600">
                                        <asp:Button ID="Add_Roadkill_bulletin" runat="server" Text="新增資料" Onclick="Add_Roadkill_bulletin_Click"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow >
            <asp:TableCell VerticalAlign="Middle" HorizontalAlign="Center">
                <table width="0"  border="1">
                <thead>
                <tr>
                <td colspan="5">
                <p style="text-align: center;"><strong>各工務段野生動物救傷單位建議</strong></p>
                </td>
                </tr>
                <tr>
                <td width="49">
                <p><strong>工程處</strong></p>
                </td>
                <td width="49">
                <p><strong>工務段</strong></p>
                </td>
                <td>
                <p><strong>國道</strong></p>
                </td>
                <td width="333">
                <p><strong>起點</strong></p>
                </td>
                <td width="540">
                <p><strong>建議救傷單位(公家機關)</strong></p>
                </td>
                </tr>
                </thead>
                <tbody>
                <tr>
                <td rowspan="11" width="47">
                <p>北區</p>
                </td>
                <td rowspan="2" width="49">
                <p>內湖</p>
                </td>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>基隆端（0k+000）南迄林口交流道（40k+850）</p>
                </td>
                <td rowspan="2" width="514">
                <p>基隆市動物保護防疫所、臺北市動物保護處、臺北市動物衛生檢驗所、新北市動物保護防疫處、新北市農業局林務科，及基隆、臺北市、新北市之其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>汐五高架段（20.28公里）</p>
                </td>
                </tr>
                <tr>
                <td rowspan="2" width="49">
                <p>中壢</p>
                </td>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>林口交流道(40k+850)南迄公道五交流道(93k+500)</p>
                </td>
                <td rowspan="2" width="514">
                <p>新竹縣農業處森林暨自然保育科、桃園縣動物保護防疫處，及桃園、新竹其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>2</p>
                </td>
                <td width="333">
                <p>中正機場支線 （0k+000）至大湳交流道（18k+200）</p>
                </td>
                </tr>
                <tr>
                <td rowspan="3" width="49">
                <p>木柵</p>
                </td>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>基隆港西岸碼頭至國道3號基金交流道</p>
                </td>
                <td rowspan="2" width="514">
                <p>基隆市動物保護防疫所、臺北市動物保護處、臺北市動物衛生檢驗所、新北市動物保護防疫處、新北市農業局林務科，及基隆、臺北市、新北市之其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>基金交流道（0k+000）南迄土城交流道北端（42k+000）</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>3甲</p>
                </td>
                <td width="333">
                <p>臺北聯絡道（0k+000~5k+600），南港聯絡道（1.4公里）</p>
                </td>
                <td width="514">
                <p>臺北市動物保護處、臺北市動物衛生檢驗所</p>
                </td>
                </tr>
                <tr>
                <td rowspan="3" width="49">
                <p>關西</p>
                </td>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>土城交流道北端（42k+000）南迄香山交流道南端(110K+703)</p>
                </td>
                <td width="514">
                <p>新北市動物保護防疫處、新北市農業局林務科、新竹市農業局林務科、新竹縣農業處森林暨自然保育科，及新北市、新竹之其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>2</p>
                </td>
                <td width="333">
                <p>大湳交流道（18K+200）至鶯歌系統交流道 (20K+358.8)</p>
                </td>
                <td width="514">
                <p>桃園縣動物保護防疫處，及桃園其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>公道五交流道(93k+500)南迄新竹系統交流道南端(100k+800)</p>
                </td>
                <td width="514">
                <p>新竹市農業局林務科、新竹縣農業處森林暨自然保育科，及新竹其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td width="49">
                <p>頭城</p>
                </td>
                <td>
                <p>5</p>
                </td>
                <td width="333">
                <p>南港系統交流道 (0k+000)至蘇澳交流道(54k+200)</p>
                </td>
                <td width="514">
                <p>宜蘭縣動植物防疫所野生動物急救站、宜蘭縣農業處畜產科及宜蘭其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td rowspan="7" width="47">
                <p>中區</p>
                </td>
                <td rowspan="2" width="49">
                <p>苗栗</p>
                </td>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>新竹至大雅（100k+800～173k+500）</p>
                </td>
                <td width="514">
                <p>苗栗縣農業處自然生態保育科、臺中市農業局林務自然保育科、臺中市動物防疫處，及苗栗、臺中其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>4</p>
                </td>
                <td width="333">
                <p>清水至豐原（0k+000～17k+160）</p>
                </td>
                <td width="514">
                <p>臺中市農業局林務自然保育科、臺中市動物防疫處，及臺中其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td width="49">
                <p>大甲</p>
                </td>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>香山至彰化系統（110k+703～195k+462）</p>
                </td>
                <td width="514">
                <p>苗栗縣農業處自然生態保育科、臺中市農業局林務自然保育科、臺中市動物防疫處，及苗栗、臺中其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td rowspan="3" width="49">
                <p>南投</p>
                </td>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>彰化系統至古坑系統（195k+462～270k+000)</p>
                </td>
                <td width="514">
                <p>彰化縣農業處林務暨野生動物保護科、雲林縣農業處森林及保育科、特有生物研究保育中心野生動物急救站、南投縣農業處林務保育科，及彰化、南投、雲林其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>6</p>
                </td>
                <td width="333">
                <p>霧峰系統至埔里端（0k+000~37k+500）</p>
                </td>
                <td rowspan="2" width="514">
                <p>特有生物研究保育中心野生動物急救站、南投縣農業處林務保育科</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>6</p>
                </td>
                <td width="333">
                <p>東草屯交流道聯絡道（0k~1k+900)</p>
                </td>
                </tr>
                <tr>
                <td width="49">
                <p>斗南</p>
                </td>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>大雅至大林（173k+500～251k+100）</p>
                </td>
                <td width="514">
                <p>彰化縣農業處林務暨野生動物保護科、雲林縣農業處森林及保育科，及彰化、雲林其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td rowspan="6" width="47">
                <p>南區</p>
                </td>
                <td width="49">
                <p>白河</p>
                </td>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>古坑至關廟（270k+000～358k+000）</p>
                </td>
                <td width="514">
                <p>嘉義市建設處農林畜牧科、嘉義縣農業處綠化保育科、臺南市農業局森林及自然保育科、臺南市動物防疫保護處，及嘉義、臺南其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td rowspan="2" width="49">
                <p>新營</p>
                </td>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>大林至永康（250k+100～320k+000）</p>
                </td>
                <td rowspan="2" width="514">
                <p>嘉義市建設處農林畜牧科、嘉義縣農業處綠化保育科、臺南市農業局森林及自然保育科、臺南市動物防疫保護處，及嘉義、臺南其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>8</p>
                </td>
                <td width="333">
                <p>臺南至新化（0k+000～15k+510）</p>
                </td>
                </tr>
                <tr>
                <td rowspan="2" width="49">
                <p>岡山</p>
                </td>
                <td>
                <p>1</p>
                </td>
                <td width="333">
                <p>永康至高雄（320k+000～372k+760）</p>
                </td>
                <td rowspan="2" width="514">
                <p>高雄市植物防疫及生態保育科、高雄市動物保護處、高雄市立壽山動物園，及高雄其他救傷單位</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>10</p>
                </td>
                <td width="333">
                <p>高雄至燕巢（0k+000～18k+400）</p>
                </td>
                </tr>
                <tr>
                <td width="49">
                <p>屏東</p>
                </td>
                <td>
                <p>3</p>
                </td>
                <td width="333">
                <p>關廟至大鵬灣（358k+000～431k+520）</p>
                </td>
                <td width="514">
                <p>屏東縣農業處林業及保育科、屏東縣動物防疫所、屏東科技大學保育類野生動物收容中心、國立海洋生物博物館，及屏東其他救傷單位</p>
                </td>
                </tr>
                </tbody>
                </table>

                <p style="text-align: center;">&nbsp;</p>
                <table width="0" border="1">
                <thead>
                <tr>
                <td colspan="5">
                <p style="text-align: center;"><strong>各縣市動物救傷單位資料</strong></p>
                </td>
                </tr>
                <tr>
                <td>
                <p><strong>縣市</strong></p>
                </td>
                <td width="239">
                <p><strong>單位名稱</strong></p>
                </td>
                <td width="248">
                <p><strong>電話</strong></p>
                </td>
                <td width="310">
                <p><strong>地址</strong></p>
                </td>
                <td width="161">
                <p><strong>備註</strong></p>
                </td>
                </tr>
                </thead>
                <tbody>
                <tr>
                <td rowspan="3">
                <p>基隆市</p>
                </td>
                <td width="239">
                <p>產業發展處-農林行政科</p>
                </td>
                <td width="248">
                <p>02-2423-8660<br /> 02-2425-8389轉152 ~ 164</p>
                </td>
                <td width="310">
                <p>基隆市中正區信二路301號3樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>動物保護防疫所</p>
                </td>
                <td width="248">
                <p>02-2428-0677</p>
                </td>
                <td width="310">
                <p>基隆市信義區信二路241號1樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>基隆市野鳥學會</p>
                </td>
                <td width="248">
                <p>02-2427-4100</p>
                </td>
                <td width="310">
                <p>基隆市孝一路82號之2</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="8">
                <p>臺北市</p>
                </td>
                <td width="239">
                <p>臺北市動物保護處</p>
                </td>
                <td width="248">
                <p>02-8789-7158</p>
                </td>
                <td width="310">
                <p>臺北市信義區吳興街600巷109號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺北市動物衛生檢驗所</p>
                </td>
                <td width="248">
                <p>02-8789-7158</p>
                </td>
                <td width="310">
                <p>臺北市吳興街600巷109號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺北市野鳥學會</p>
                </td>
                <td width="248">
                <p>02-2325-9190</p>
                </td>
                <td width="310">
                <p>臺北市復興南路二段160巷3號1樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺北市野鳥學會救傷中心</p>
                </td>
                <td width="248">
                <p>02-8732-8891</p>
                </td>
                <td width="310">
                <p>臺北市信義區和平東路三段463巷5號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺灣大學附設動物醫院</p>
                </td>
                <td width="248">
                <p>02-2739-6828轉1162</p>
                </td>
                <td width="310">
                <p>臺北市大安區基隆路三段三段153號</p>
                </td>
                <td width="161">
                <p>一律收500元</p>
                </td>
                </tr>
                <tr>
                <td width="239">
                <p>全陽犬貓動物醫院</p>
                </td>
                <td width="248">
                <p>02-2762-7945</p>
                </td>
                <td width="310">
                <p>臺北市松山區八德路四段305號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>凡賽爾賽鴿寵物鳥醫院</p>
                </td>
                <td width="248">
                <p>02-2586-9933</p>
                </td>
                <td width="310">
                <p>臺北市大同區民族西路53號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>澄毅動物醫院</p>
                </td>
                <td width="248">
                <p>02-2733-4341</p>
                </td>
                <td width="310">
                <p>臺北市大安區安和路二段171巷13號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="3">
                <p>新北市</p>
                </td>
                <td width="239">
                <p>農業局林務科</p>
                </td>
                <td width="248">
                <p>02-2960-3456轉3099~3113</p>
                </td>
                <td width="310">
                <p>新北市板橋區中山路1段161號22樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>動物保護防疫處</p>
                </td>
                <td width="248">
                <p>02-2959-6353</p>
                </td>
                <td width="310">
                <p>新北市板橋區四川路一段</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>崇恩寵物醫院</p>
                </td>
                <td width="248">
                <p>02-2231-2531</p>
                </td>
                <td width="310">
                <p>新北市永和區永利路95號</p>
                </td>
                <td width="161">
                <p>臺北市野鳥學會協助醫師</p>
                </td>
                </tr>
                <tr>
                <td rowspan="4">
                <p>桃園縣</p>
                </td>
                <td width="239">
                <p>動物保護防疫處</p>
                </td>
                <td width="248">
                <p>03-3326742</p>
                </td>
                <td width="310">
                <p>桃園市桃園區縣府路1號4樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>桃園市野鳥學會</p>
                </td>
                <td width="248">
                <p>0978-103371、0978-103315</p>
                </td>
                <td width="310">
                <p>桃園市桃園區宏昌十二街504號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>普羅動物醫院</p>
                </td>
                <td width="248">
                <p>03-3789900</p>
                </td>
                <td width="310">
                <p>桃園市桃園區中山路928號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>高生動物醫院-曾宏之院長</p>
                </td>
                <td width="248">
                <p>03-3341159</p>
                </td>
                <td width="310">
                <p>桃園市中山路419號</p>
                </td>
                <td width="161">
                <p>桃園縣野鳥學會協助醫師</p>
                </td>
                </tr>
                <tr>
                <td>
                <p>新竹縣</p>
                </td>
                <td width="239">
                <p>農業處森林暨自然保育科</p>
                </td>
                <td width="248">
                <p>03-5518101轉2920~2929</p>
                </td>
                <td width="310">
                <p>新竹縣竹北市光明六路10號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="4">
                <p>新竹市</p>
                </td>
                <td width="239">
                <p>產業發展處生態保育科</p>
                </td>
                <td width="248">
                <p>03-5216121轉480、530、401。03-5242070</p>
                </td>
                <td width="310">
                <p>新竹市中正路120號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>新竹市野鳥學會</p>
                </td>
                <td width="248">
                <p>03-5728675</p>
                </td>
                <td width="310">
                <p>新竹市光復路2段246號4樓之1</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>協和動物醫院-林鈺章醫師</p>
                </td>
                <td width="248">
                <p>03-5215380</p>
                </td>
                <td width="310">
                <p>新竹市南大路48號1樓</p>
                </td>
                <td width="161">
                <p>新竹市野鳥學會協助醫師、不收寵物鳥</p>
                </td>
                </tr>
                <tr>
                <td width="239">
                <p>新竹市立動物園</p>
                </td>
                <td width="248">
                <p>03-5222194</p>
                </td>
                <td width="310">
                <p>新竹市東區博愛街111號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="3">
                <p>苗栗縣</p>
                </td>
                <td width="239">
                <p>農業處自然生態保育科</p>
                </td>
                <td width="248">
                <p>037-558216</p>
                </td>
                <td width="310">
                <p>苗栗縣苗栗市縣府路100號3樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>苗栗縣自然生態學會</p>
                </td>
                <td width="248">
                <p>037-265387</p>
                </td>
                <td width="310">
                <p>苗栗市中山路76號2樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>後龍動物醫院</p>
                </td>
                <td width="248">
                <p>037-727803</p>
                </td>
                <td width="310">
                <p>苗栗縣後龍鎮光華路432號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="5">
                <p>臺中市</p>
                </td>
                <td width="239">
                <p>農業局林務自然保育科</p>
                </td>
                <td width="248">
                <p>04-2526-1823、2526-0609<br /> 04-2228-9111轉56202</p>
                </td>
                <td width="310">
                <p>臺中市豐原區陽明街36號5樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺中市動物防疫處</p>
                </td>
                <td width="248">
                <p>04-2386-9420、2386-9425</p>
                </td>
                <td width="310">
                <p>臺中市南屯區萬和路一段28之18號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺中市野生動物保育學會<br /> -林文隆</p>
                </td>
                <td width="248">
                <p>0988-374150</p>
                </td>
                <td width="310">
                <p>臺中市太平區光興路1086號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺灣省野鳥學會</p>
                </td>
                <td width="248">
                <p>04-2260-0518</p>
                </td>
                <td width="310">
                <p>臺中市南區建國南路2段218號1樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>中興大學獸醫院-高如柏醫師</p>
                </td>
                <td width="248">
                <p>04-2284-0405、2287-0180<br /> 0922-238405</p>
                </td>
                <td width="310">
                <p>臺中市南區國光路250-1號</p>
                </td>
                <td width="161">
                <p>臺灣省野鳥學會協助醫師</p>
                </td>
                </tr>
                <tr>
                <td rowspan="4">
                <p>彰化縣</p>
                </td>
                <td width="239">
                <p>農業處林務暨野生動物保護科</p>
                </td>
                <td width="248">
                <p>04-7531621轉1621</p>
                </td>
                <td width="310">
                <p>彰化市中山路二段416號6樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>彰化縣動物防疫所</p>
                </td>
                <td width="248">
                <p>04-7620774</p>
                </td>
                <td width="310">
                <p>彰化市中央路2號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>彰化縣野鳥學會</p>
                </td>
                <td width="248">
                <p>04-7110306</p>
                </td>
                <td width="310">
                <p>彰化市大埔路492號5樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>生偉動物醫院</p>
                </td>
                <td width="248">
                <p>04-8335769</p>
                </td>
                <td width="310">
                <p>員林鎮中山路二段131巷54弄24號</p>
                </td>
                <td width="161">
                <p>彰化縣野鳥學會協助單位</p>
                </td>
                </tr>
                <tr>
                <td rowspan="2">
                <p>南投縣</p>
                </td>
                <td width="239">
                <p>農業處林務保育科</p>
                </td>
                <td width="248">
                <p>049-2222340</p>
                </td>
                <td width="310">
                <p>南投市中興路660號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>特有生物研究保育中心<br /> 野生動物急救站</p>
                </td>
                <td width="248">
                <p>049-2761331-309</p>
                </td>
                <td width="310">
                <p>南投縣集集鎮民生東路1號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="3">
                <p>雲林縣</p>
                </td>
                <td width="239">
                <p>農業處森林及保育科</p>
                </td>
                <td width="248">
                <p>05-5522513</p>
                </td>
                <td width="310">
                <p>雲林縣斗六市雲林路二段515號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>雲林縣野鳥學會</p>
                </td>
                <td width="248">
                <p>05-5966970</p>
                </td>
                <td width="310">
                <p>雲林縣斗南鎮信義路242巷2號1樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>國際犬貓診所</p>
                </td>
                <td width="248">
                <p>05-6324100</p>
                </td>
                <td width="310">
                <p>雲林縣虎尾鎮林森路二段180號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="4">
                <p>嘉義市</p>
                </td>
                <td width="239">
                <p>建設處農林畜牧科</p>
                </td>
                <td width="248">
                <p>05-2254321轉234<br /> 05-2290357</p>
                </td>
                <td width="310">
                <p>嘉義市中山路199號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>嘉義市野鳥學會</p>
                </td>
                <td width="248">
                <p>05-2750667</p>
                </td>
                <td width="310">
                <p>嘉義市保成路195號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>仁愛動物醫院-詹成張醫師</p>
                </td>
                <td width="248">
                <p>05-2788067</p>
                </td>
                <td width="310">
                <p>嘉義市新生路214號</p>
                </td>
                <td width="161">
                <p>嘉義市野鳥學會協助醫師</p>
                </td>
                </tr>
                <tr>
                <td width="239">
                <p>上哲動物醫院-黃于哲醫師</p>
                </td>
                <td width="248">
                <p>05-2231500</p>
                </td>
                <td width="310">
                <p>嘉義市吳鳳北路259號</p>
                </td>
                <td width="161">
                <p>嘉義市野鳥學會協助醫師</p>
                </td>
                </tr>
                <tr>
                <td rowspan="2">
                <p>嘉義縣</p>
                </td>
                <td width="239">
                <p>農業處綠化保育科</p>
                </td>
                <td width="248">
                <p>05-3620123轉335、336、394、450</p>
                </td>
                <td width="310">
                <p>嘉義縣太保市祥和一路東段一號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>嘉義縣野鳥學會</p>
                </td>
                <td width="248">
                <p>05-3625372、0916-717696</p>
                </td>
                <td width="310">
                <p>嘉義縣太保市信義二路157之167號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="4">
                <p>臺南市</p>
                </td>
                <td width="239">
                <p>農業局森林及自然保育科</p>
                </td>
                <td width="248">
                <p>06-6321731</p>
                </td>
                <td width="310">
                <p>臺南市新營區民治路36號南瀛大樓4樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺南市動物防疫保護處</p>
                </td>
                <td width="248">
                <p>06-6323039</p>
                </td>
                <td width="310">
                <p>新營市長榮路一段501號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺南市野鳥學會</p>
                </td>
                <td width="248">
                <p>06-2138310</p>
                </td>
                <td width="310">
                <p>臺南市南門路237巷10號3樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>慈愛動物醫院（金華院）</p>
                </td>
                <td width="248">
                <p>06-2641220</p>
                </td>
                <td width="310">
                <p>臺南市金華路二段39巷3號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="5">
                <p>高雄市</p>
                </td>
                <td width="239">
                <p>植物防疫及生態保育科</p>
                </td>
                <td width="248">
                <p>07-7995678轉6155~6157</p>
                </td>
                <td width="310">
                <p>高雄縣鳳山市光復路二段132號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>高雄市動物保護處</p>
                </td>
                <td width="248">
                <p>07-7462368</p>
                </td>
                <td width="310">
                <p>鳳山市忠義街166號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>高雄市立壽山動物園</p>
                </td>
                <td width="248">
                <p>07-5215187</p>
                </td>
                <td width="310">
                <p>高雄市鼓山區萬壽路350號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>高雄市野鳥學會</p>
                </td>
                <td width="248">
                <p>07-2152525</p>
                </td>
                <td width="310">
                <p>高雄市前金區中華四路282號6樓</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>吉盈動物醫院-陳福利醫師</p>
                </td>
                <td width="248">
                <p>07-2267559</p>
                </td>
                <td width="310">
                <p>高雄市民生一路34號</p>
                </td>
                <td width="161">
                <p>高雄市野鳥學會協助醫師、不收寵物鳥</p>
                </td>
                </tr>
                <tr>
                <td rowspan="4">
                <p>屏東縣</p>
                </td>
                <td width="239">
                <p>農業處林業及保育科</p>
                </td>
                <td width="248">
                <p>08-7320415轉3770~3779、3713、08-7653860</p>
                </td>
                <td width="310">
                <p>屏東市自由路527號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>屏東縣動物防疫所</p>
                </td>
                <td width="248">
                <p>08-7224109</p>
                </td>
                <td width="310">
                <p>屏東市民學路58巷23號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>屏東科技大學保育類<br /> 野生動物收容中心</p>
                </td>
                <td width="248">
                <p>08-7740413</p>
                </td>
                <td width="310">
                <p>屏東縣內埔鄉老埤村學府路1號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>國立海洋生物博物館</p>
                </td>
                <td width="248">
                <p>08-8825001</p>
                </td>
                <td width="310">
                <p>屏東縣車城鄉後灣村後灣路2號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td rowspan="2">
                <p>屏東市</p>
                </td>
                <td width="239">
                <p>屏東縣野鳥學會</p>
                </td>
                <td width="248">
                <p>08-7351581</p>
                </td>
                <td width="310">
                <p>屏東市大連路62-15號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>百齡動物醫院-蕭恩沛醫師</p>
                </td>
                <td width="248">
                <p>08-7377545</p>
                </td>
                <td width="310">
                <p>屏東市大連路62-15號</p>
                </td>
                <td width="161">
                <p>屏東市野鳥學會協助醫師</p>
                </td>
                </tr>
                <tr>
                <td rowspan="5">
                <p>宜蘭縣</p>
                </td>
                <td width="239">
                <p>農業處畜產科</p>
                </td>
                <td width="248">
                <p>03-9251000轉1541、1542<br /> 03-9253174</p>
                </td>
                <td width="310">
                <p>宜蘭市南津里13鄰縣政北路一號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>宜蘭縣動植物防疫所<br /> 野生動物急救站</p>
                </td>
                <td width="248">
                <p>03-9602350</p>
                </td>
                <td width="310">
                <p>宜蘭縣五結鄉成興村利寶路60號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>國立宜蘭大學動物科技學系</p>
                </td>
                <td width="248">
                <p>03-9357400轉815</p>
                </td>
                <td width="310">
                <p>宜蘭市神農路一段一號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>宜蘭縣野鳥學會</p>
                </td>
                <td width="248">
                <p>0912-905929</p>
                </td>
                <td width="310">
                <p>宜蘭縣員山鄉石頭厝路200號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                <tr>
                <td width="239">
                <p>臺大動物醫院-魏昭文醫師</p>
                </td>
                <td width="248">
                <p>03-9548581</p>
                </td>
                <td width="310">
                <p>宜蘭縣羅東鎮愛國路86號</p>
                </td>
                <td width="161">&nbsp;</td>
                </tr>
                </tbody>
                </table>
                <p style="text-align: left;"><strong>107年1月更新。觀察家製作。勘誤資訊回報：許永暉、02-23648581-10、obyunghui@gmail.com.</strong></p>

            </asp:TableCell>

        </asp:TableRow>

        <asp:TableRow VerticalAlign="Top" BackColor="#a7a7a7">
            <asp:TableCell BackColor="#a7a7a7">
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table>

    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
