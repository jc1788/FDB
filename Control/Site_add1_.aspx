



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Site_add1_.aspx.cs" Inherits="Control_Site_add" %>

<!DOCTYPE html>


<link href="~/Content/base.css" rel="stylesheet" />
<link href="~/Content/admin.css" rel="stylesheet" type="text/css" />
<link href="~/Content/style.css" rel="stylesheet" type="text/css" />


<script type="text/javascript" src="../Scripts/proj4js.js"></script>
<script src="../Scripts/proj4js-combined.js"></script>

 <script type="text/javascript">

     //function TransCoord(cal_type) {
     function TransCoord() {
         //alert(document.getElementById('TM2_X').value);


         Proj4js.defs["EPSG:4326"] = "+proj=longlat +ellps=WGS84 +datum=WGS84 +no_defs";
         var EPSG4326 = new Proj4js.Proj('EPSG:4326');

         Proj4js.defs["EPSG:3821"] = "+proj=longlat  +towgs84=-752,-358,-179,-.0000011698,.0000018398,.0000009822,.00002329 +ellps=aust_SA +units=度 +no_defs";
         var EPSG3821 = new Proj4js.Proj('EPSG:3821');

         Proj4js.defs["EPSG:3824"] = "+proj=longlat +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +no_defs";
         var EPSG3824 = new Proj4js.Proj('EPSG:3824');

         Proj4js.defs["EPSG:3825"] = "+proj=tmerc +lat_0=0 +lon_0=119 +k=0.9999 +x_0=250000 +y_0=0 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs";
         var EPSG3825 = new Proj4js.Proj('EPSG:3825');

         Proj4js.defs["EPSG:3826"] = "+proj=tmerc  +lat_0=0 +lon_0=121 +k=0.9999 +x_0=250000 +y_0=0 +ellps=GRS80 +units=???? +no_defs";
         var EPSG3826 = new Proj4js.Proj('EPSG:3826');

         Proj4js.defs["EPSG:3827"] = "+proj=tmerc +lat_0=0 +lon_0=119 +k=0.9999 +x_0=250000 +y_0=0 +ellps=aust_SA +units=m +towgs84=-752,-358,-179,-0.0000011698,0.0000018398,0.0000009822,0.00002329 +no_defs";
         var EPSG3827 = new Proj4js.Proj('EPSG:3827');

         Proj4js.defs["EPSG:3828"] = "+proj=tmerc  +towgs84=-752,-358,-179,-.0000011698,.0000018398,.0000009822,.00002329 +lat_0=0 +lon_0=121 +x_0=250000 +y_0=0 +k=0.9999 +ellps=aust_SA  +units=公尺";
         var EPSG3828 = new Proj4js.Proj('EPSG:3828');
         
         //if (cal_type == 'TM2') {
             var xx = parseFloat(document.getElementById('TM2_X').value);
             var yy = parseFloat(document.getElementById('TM2_Y').value);
             var src = new Proj4js.Proj("EPSG:3826");
             var dst = new Proj4js.Proj("EPSG:4326");
             var point = new Proj4js.Point(xx, yy);  //Create a point object 
             Proj4js.transform(src, dst, point) //Do your conversion 
             document.getElementById('x').value = Math.floor(point.x * 100000) / 100000;
             document.getElementById('y').value = Math.floor(point.y * 100000) / 100000;
         //}
         //else{

         //}
         
        //var xx = parseFloat(document.getElementById('TM2_X').value);
        //var yy = parseFloat(document.getElementById('TM2_Y').value);
        //alert(xx);
        
        //var src = new Proj4js.Proj("EPSG:3826");
        //var dst = new Proj4js.Proj("EPSG:4326");
        //var point = new Proj4js.Point(xx, yy);  //Create a point object 
        //Proj4js.transform(src, dst, point) //Do your conversion 
        //document.getElementById('x').value=point.x;
        //document.getElementById('y').value=point.y;
     } //end TransCoord()
    
</script>

<script type="text/javascript">
    function fun1() {
        alert("Test");
        //alert(document.getElementById('siteid').value);
        
        //url = "http://taibif.org.tw/BDTools/proj4/convert.php?source=5&destination=1&x=" + document.getElementById('TM2_X').value + "&y=" + document.getElementById('TM2_Y').value;
        //window.open(url, '里程座標轉換', config = 'height=0,width=0')

        //var child = window.open(url, "child", "height=200,width=400,status=yes,toolbar=no,menubar=no,location=no");
        //alert(child.length);
        //var qs = URI(url.query(true)); // == { dest : 'aboutus.aspx' }
        //alert(qs.dest);
        //string aaa = child.toString;
        //alert(child.length);
        
        
        //var MMAry = new Array();
        //var MLAry = child.split("<br>");
        //alert(MLAry[0]);
        //alert(MLAry[1]);

        //child.split("<br>");
        //alert(child.split("<br>"));
        //child.close();
        
        
        //window.location.href = url;
        //alert(url);
        //document.getElementById('x').value = 121.1;
        //document.getElementById('y').value = 23.2323;
    }

</script>

<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/calendar-setup.js"></script>
<script type="text/JavaScript" src="../Scripts/jscalendar-1.0/lang/calendar-tc.js"></script>

<link type="text/css" href="../Scripts/jscalendar-1.0/calendar-blue.css" rel="Stylesheet" />

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
<body style="text-align:center">
    
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
                                 <a href="../Control/Site_add1_.aspx" class="menu4">生態調查資料</a>
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7" VerticalAlign="Top">
                <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">
                            <asp:Table ID="Table_Detail" runat="server" Width="900" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2">
                                        生態調查資料
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#FFFFFF" Width="900" Height="20">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                
                                <%--start--%>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_Chinese_name" runat="server" Text="物種中文名："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="Chinese_name" runat="server" OnTextChanged="Get_NameFull" autopostback="true"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_ScientificName" runat="server" Text="學名："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="ScientificName" runat="server" Width="300"></asp:TextBox>
                                     <asp:TextBox ID="accepted_name_code" runat="server" Visible="False" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_Group" runat="server" Text="物種類群："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:DropDownList ID="Group" runat="server" DataSourceID="SqlDataSource2" DataTextField="title" DataValueField="title"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT title FROM [Animal]"></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                   <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_Density" runat="server" Text="個體數(面積/密度)："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="Density" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                   <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_Way_ch" runat="server" Text="調查方法："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="Way_ch" runat="server"></asp:TextBox>
                                        <asp:Label ID="Note1" runat="server" Text="(例如：穿越線)"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_sec_record" runat="server" Text="間接記錄："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="sec_record" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_date" runat="server" Text="調查日期："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="date" runat="server" ReadOnly="false" Enabled="true" CausesValidation="true" BackColor="silver"></asp:TextBox>
                                        <asp:ImageButton ID="imgCalendar1" runat="server" title="小日曆"  ImageUrl="~/Images/icon_date.jpg" />
                                        <script ="javascript" type="text/javascript">
                                            Calendar.setup({
                                                inputField: "date",
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
                                        <asp:Label ID="Label_x" runat="server" Text="經度："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="x" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_y" runat="server" Text="緯度："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="y" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_inaccuracy" runat="server" Text="座標誤差(公尺)："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="inaccuracy" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_site_city" runat="server" Text="調查地(縣市)"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="site_city" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_site_area" runat="server" Text="調查地(鄉鎮)"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="site_area" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_site_ch" runat="server" Text="調查地(中文)：" ></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="site_ch" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label1" runat="server" Text="調查路段："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:DropDownList ID="highway_id" runat="server" DataSourceID="SqlDataSource1" DataTextField="highway_name" DataValueField="highway_id"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT * FROM [Highway]"></asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label2" runat="server" Text="方向："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:DropDownList ID="Direction" runat="server">
                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                            <asp:ListItem Text="W" Value="W"></asp:ListItem>
                                            <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                     </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_mileage" runat="server" Text="參考里程(k)" ></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="mileage" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_habit" runat="server" Text="棲地類型："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="habit" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow >
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label13" runat="server" Text="上傳影音資料："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:FileUpload ID="file1" runat="server" Width="500" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_Collector_ch" runat="server" Text="調查者中文名："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="Collector_ch" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_plan_name" runat="server" Text="計畫名稱："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="plan_name" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                  <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_invest_company" runat="server" Text="調查單位："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="invest_company" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" >
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>


                                   <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_note" runat="server" Text="備註："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="note" runat="server" Rows="5" Width="500" TextMode="MultiLine"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Visible="false">
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_siteid" runat="server" Text="時間地點代號："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="siteid" runat="server" OnTextChanged="Get_SiteId" autopostback="true"></asp:TextBox>
                                        <asp:Image ID="check_ok" runat="server" ImageUrl="~/Images/check_ok.ico" Width="0" Height="0"/>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="http://taibif.org.tw/freeway/rule.php" fontcolor="red">編碼原則</asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Visible="false">
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_TM2_X" runat="server" Text="TM2_X："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="TM2_X" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" oninput="javascript:TransCoord();"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Visible="false">
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_TM2_Y" runat="server" Text="TM2_Y："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="TM2_Y" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" oninput="javascript:TransCoord();"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Visible="false">
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_site_en" runat="server" Text="調查地(英文)："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="site_en" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300"/>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:Button ID="Add_Site" runat="server" Text="新增生態調查資料" OnClick="Add_Site_Click" Enabled="true"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
                <br/>

                <asp:Table ID="Table2" runat="server" Width="1000" HorizontalAlign="Center" Visible="false">
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">

                            <asp:Table ID="Table4" runat="server" Width="900" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="900" Visible="false">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2">
                                        生態調查出現記錄
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#FFFFFF" Width="900" Height="20" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Visible="false">
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label_Collector" runat="server" Text="調查者英文名："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:TextBox ID="Collector" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="900" Visible="false">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900" Visible="false">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300"/>
                                    <asp:TableCell Width="600" HorizontalAlign="Left">
                                        <asp:Button ID="Add_Occurrence" runat="server" Text="新增生態調查出現紀錄" OnClick="Add_Occurrence_Click" />
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
