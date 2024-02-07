﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrossDatabaseSearch_03_.aspx.cs" Inherits="Control_CrossDatabaseSearch_03" %>

<!DOCTYPE html>
<script type="text/javascript" src="../Scripts/template3/ReDir.js"></script>
<script type='text/javascript' src="../Scripts/template3/x_core.js"></script>
<script type='text/javascript' src="../Scripts/template3/x_event.js"></script>
<script type='text/javascript' src="../Scripts/template3/x_drag.js"></script>



<link rel="stylesheet" type="text/css" media="print" href="../Scripts/template3/print.css" />
<link rel="stylesheet" type="text/css" media="screen" href="../Scripts/template3/screen2.css" />
<link rel="stylesheet" type="text/css" media="screen" href="../Scripts/template3/gmapbounds.css" />


  
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    

    <style type="text/css">
      #map {
        width: 800px;
        height: 500px;
      }
    </style>

    <script type="text/javascript" src="https://maps.google.com/maps/api/js?sensor=false"></script>

    <script type="text/javascript">
        // Global variables
        var map;
        var marker1;
        var marker2;
        var rectangle;

        var map_02;
        var marker1_02;
        var marker2_02;
        var rectangle_02;

        /**
        * Called on the initial page load.
        */
        function init() {
            map = new google.maps.Map(document.getElementById('map'), {
                'zoom': 8,
                'center': new google.maps.LatLng(23.2, 120.6),
                'mapTypeId': google.maps.MapTypeId.ROADMAP
            });

            map_02 = new google.maps.Map(document.getElementById('map_02'), {
                'zoom': 8,
                'center': new google.maps.LatLng(23.4, 121.0),
                'mapTypeId': google.maps.MapTypeId.ROADMAP
            });

            // Plot two markers to represent the Rectangle's bounds.
            marker1 = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(23.1, 120.4),
                draggable: true,
                title: 'Drag me!'
            });
            marker2 = new google.maps.Marker({
                map: map,
                position: new google.maps.LatLng(23.4, 120.7),
                draggable: true,
                title: 'Drag me!'
            });

            marker1_02 = new google.maps.Marker({
                map: map_02,
                position: new google.maps.LatLng(23.8, 120.4),
                draggable: true,
                title: 'Drag me!'
            });
            marker2_02 = new google.maps.Marker({
                map: map_02,
                position: new google.maps.LatLng(24.2, 120.7),
                draggable: true,
                title: 'Drag me!'
            });

            // Allow user to drag each marker to resize the size of the Rectangle.
            google.maps.event.addListener(marker1, 'drag', redraw);
            google.maps.event.addListener(marker2, 'drag', redraw);
            google.maps.event.addListener(marker1_02, 'drag', redraw);
            google.maps.event.addListener(marker2_02, 'drag', redraw);

            // Create a new Rectangle overlay and place it on the map.  Size
            // will be determined by the LatLngBounds based on the two Marker
            // positions.
            rectangle = new google.maps.Rectangle({
                map: map
            });
            rectangle_02 = new google.maps.Rectangle({
                map: map_02
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
            var latLngBounds_02 = new google.maps.LatLngBounds(
         marker1_02.getPosition(),
         marker2_02.getPosition()
       );
            document.getElementById("box_marker1").value = marker1.getPosition().toUrlValue();
            document.getElementById("box_marker2").value = marker2.getPosition().toUrlValue();
            document.getElementById("box_marker1_02").value = marker1_02.getPosition().toUrlValue();
            document.getElementById("box_marker2_02").value = marker2_02.getPosition().toUrlValue();
            rectangle.setBounds(latLngBounds);
            rectangle_02.setBounds(latLngBounds_02);
        }

        // Register an event listener to fire when the page finishes loading.
        google.maps.event.addDomListener(window, 'load', init);
    </script>


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
    //function MM_preloadImages() { //v3.0
    //    var d = document; if (d.images) {
    //        if (!d.MM_p) d.MM_p = new Array();
    //        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
    //            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    //    }
    //}
    //-->
</script>
        <link href="../Control/fun01_011.aspx_files/style1.css" rel="stylesheet" />
    <script src="../Control/fun01_011.aspx_files/WebResource.js"></script>
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
                                 <a href="../control/CorssDatabaseSearch_.aspx" class="menu4">跨資料庫查詢</a>
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
                                        <asp:Table ID="Table_Function" runat="server" Width="640" HorizontalAlign="Center" >

                                            <asp:TableRow>
                                                <asp:TableCell Width="17">
                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                                </asp:TableCell>
                                                <asp:TableCell Width="93" CSSClass="title12">
                                                    空間瀏覽
                                                </asp:TableCell>
                                                <asp:TableCell Width="482" CSSClass="menu12a">
                                                    <span class="menu12">
                                                        <a href="CrossDatabaseSearch_.aspx" class="menu12">依國道公里數查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="CrossDatabaseSearch_02_.aspx" class="menu12">依工程處查詢</a>
                                                    </span> | 
                                                    <span class="menu12">
                                                        <a href="CrossDatabaseSearch_03_.aspx" class="menu12">空間查詢</a>
                                                    </span> 
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <br/>
                                        <asp:Table ID="Table_Query" runat="server" HorizontalAlign="Right" >

                                              <asp:TableRow>
                                                    <asp:TableCell CssClass="explain4" ColumnSpan="2">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" Text="生態調查" ID="search1" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" Text="道路致死" ID="search2" />
                                                      <%--  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                   <%-- <asp:CheckBox runat="server" Text="植栽工程" ID="search3" />--%>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow BackColor="#e0e0e0">
                                                    <asp:TableCell ColumnSpan="1">
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4" HorizontalAlign="Right">
                                                    請輸入物種名稱 :
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="explain4" >
                                                    
                                                    <asp:TextBox ID="KeyWords" runat="server" Text="" Width="400"></asp:TextBox>
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell CssClass="explain4" HorizontalAlign="Right">
                                                    資料期間 :
                                                </asp:TableCell>
                                                <asp:TableCell>

                                                    <asp:TextBox ID="StartYear" runat="server" Width="30"></asp:TextBox><asp:Label ID="Label1" runat="server" Text="  年(西元)  " Width="70"></asp:Label>
                                                    <asp:DropDownList ID="StartMonth" runat="server" DataSourceID="SDS_StartMonth" DataTextField="MM" DataValueField="MM">
                                                        <asp:ListItem Value="0">無</asp:ListItem>
                                                    </asp:DropDownList>
                    
                                                    <asp:SqlDataSource ID="SDS_StartMonth" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '' AS MM UNION ALL Select MM From MM "></asp:SqlDataSource>
                                                    <asp:Label ID="Label2" runat="server" Text="  月至  "></asp:Label>

                                                    <asp:TextBox ID="EndYear" runat="server" Width="30"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="  年(西元)  " Width="70"></asp:Label>
                                                    <asp:DropDownList ID="EndMonth" runat="server" DataSourceID="SDS_EndMonth" DataTextField="MM" DataValueField="MM">
                                                        <asp:ListItem Value="0">無</asp:ListItem>
                                                    </asp:DropDownList>
                    
                                                    <asp:SqlDataSource ID="SDS_EndMonth" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select '' AS MM UNION ALL Select MM From MM "></asp:SqlDataSource>
                                                    <asp:Label ID="Label4" runat="server" Text="  月  "></asp:Label>
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
                                                    <asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="Button_Query_Click"/>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="3" Visible="false">
                                                    <asp:Label runat="server" Text="生態調查資料"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                             <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Center"  ColumnSpan="3">
                                                    <div id="map">
                                                    </div>
                                                    <asp:TextBox ID="box_marker1" runat="server" Text="" Width="100" ></asp:TextBox>
                                                    <asp:TextBox ID="box_marker2" runat="server" Text="" Width="100" ></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="3" Visible="false">
                                                    <asp:Label runat="server" Text="動物道路致死資料"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                             <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Center"  ColumnSpan="3" Visible="false">
                                                    <div id="map_02" style="width:100%;height:500px">
                                                    </div>
                                                    <asp:TextBox ID="box_marker1_02" runat="server" Text="" Width="100" ></asp:TextBox>
                                                    <asp:TextBox ID="box_marker2_02" runat="server" Text="" Width="100" ></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow BackColor="#e0e0e0">
                                                <asp:TableCell  ColumnSpan="2">
                                                </asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
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