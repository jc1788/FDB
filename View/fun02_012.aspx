<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fun02_012.aspx.cs" Inherits="View_fun02_012" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


<script type="text/javascript" src="template3/ReDir.js"></script>
<script type='text/javascript' src="template3/x_core.js"></script>
<script type='text/javascript' src="template3/x_event.js"></script>
<script type='text/javascript' src="template3/x_drag.js"></script>
<link rel="stylesheet" type="text/css" media="print" href="template3/print.css" />
<link rel="stylesheet" type="text/css" media="screen" href="template3/screen2.css" />
<link rel="stylesheet" type="text/css" media="screen" href="template3/gmapbounds.css" />
<link rel="SHORTCUT ICON" type="image/x-icon" href="template3/favicon.ico" />
    <style type="text/css">
      #map {
        width: 800px;
        height: 500px;
      }
    </style>


    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false"></script>
    <script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script> 

    <script type="text/javascript">
        // Global variables
        var map;
        var marker1;
        var marker2;
        var rectangle;
        //var google;

        /**
         * Called on the initial page load.
         */
        function init() {
            map = new google.maps.Map(document.getElementById('map'), {
                'zoom': 7,
                'center': new google.maps.LatLng(22, 121),
                'mapTypeId': google.maps.MapTypeId.ROADMAP
            });

            // Plot two markers to represent the Rectangle's bounds.
            marker1 = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(23.459999, 120.610001),
                draggable: true,
                title: 'Drag me!'
            });
            marker2 = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(24.790001, 121.129997),
                draggable: true,
                title: 'Drag me!'
            });

            // Allow user to drag each marker to resize the size of the Rectangle.
            google.maps.event.addListener(marker1, 'drag', redraw);
            google.maps.event.addListener(marker2, 'drag', redraw);

            // Create a new Rectangle overlay and place it on the map.  Size
            // will be determined by the LatLngBounds based on the two Marker
            // positions.
            rectangle = new google.maps.Rectangle({
                map: map
            });
            redraw();
        }

        /**
         * Updates the Rectangle's bounds to resize its dimensions.
         */
        function redraw() {
            var latLngBounds = new google.maps.LatLngBounds(
              marker1.getPosition(),
              marker2.getPosition()
            );
            document.getElementById("box_marker1").value = marker1.getPosition().toUrlValue();
            document.getElementById("box_marker2").value = marker2.getPosition().toUrlValue();
            rectangle.setBounds(latLngBounds);

            google.maps.event.addDomListener(window, 'load', init);
        }
        //google.maps.event.addDomListener(window, 'load', Demo.init);
        // Register an event listener to fire when the page finishes loading.
        google.maps.event.addDomListener(window, 'load', init);
        //google.maps.event.addDomListener(window, 'load', Initialize);


    </script>

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
                <a href="../default.cshtml" >
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/frame/top1.jpg" BorderStyle="None" />
                </a>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7">
                <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                     <asp:TableRow HorizontalAlign="Right">
                         <asp:TableCell>
                             <div class="menu4">
                                 <a href="../default.cshtml" class="menu4">首頁</a> &gt;  
                                 <a href="fun02_011a.aspx" class="menu4">道路致死資料</a> &gt;  
                                 <a href="fun02_011a.aspx" class="menu4">空間瀏覽</a> 
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7" VerticalAlign="Top">
                <asp:Table ID="Table2" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="400" VerticalAlign="Top">
                            <asp:Table ID="Table_Detail" runat="server" Width="400" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="menu13">
                                        物種資料
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="site_ch" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="highway_id" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="direction" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="milestone" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="x" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="y" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="date" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="range" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="type" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="weather" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                
                                
                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="animal" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="detail_animal" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="animal2" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="collecter_ch" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="species" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="deduce_species" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="transfer" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                 <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="note" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </asp:TableCell>

                        <asp:TableCell Width="600" VerticalAlign="Top" HorizontalAlign="Center" BorderStyle="None">
                            <iframe src="<%=map_url%>" style="height: 560px; width: 560px;" ></iframe>
                            <br/>

                            <asp:ImageButton ID="file1" runat="server" width="100" Height="100"/>&nbsp;
                            <asp:ImageButton ID="file2" runat="server" width="100" Height="100"/>&nbsp;
                            <asp:ImageButton ID="file3" runat="server" width="100" Height="100"/>&nbsp;
                            <asp:ImageButton ID="file4" runat="server" width="100" Height="100"/>&nbsp;
                            <asp:ImageButton ID="file5" runat="server" width="100" Height="100"/>&nbsp;

                            <br/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>


        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell ColumnSpan="2">
                
                <asp:Table ID="Table6" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
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
