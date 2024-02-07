<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fun01_0131_.aspx.cs" Inherits="View_fun01_0131" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">


<!-- script type="text/javascript" src="template3/ReDir.js"></script>
<script type='text/javascript' src="template3/x_core.js"></script>
<script type='text/javascript' src="template3/x_event.js"></script>
<script type='text/javascript' src="template3/x_drag.js"></script>
<link rel="stylesheet" type="text/css" media="print" href="template3/print.css" />
<link rel="stylesheet" type="text/css" media="screen" href="template3/screen2.css" />
<link rel="stylesheet" type="text/css" media="screen" href="template3/gmapbounds.css" />
<link rel="SHORTCUT ICON" type="image/x-icon" href="template3/favicon.ico" />
<link href="default.css" rel="stylesheet" / -->


    <style type="text/css">
      #map-canva {
        width: 800px;
        height: 500px;
      }
    </style>



    <!--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>-->
    <script type="text/javascript" src="https://gist-gees.motc.gov.tw/thsr_map/query?request=Json&var=geeServerDefs"></script>
    <!--<script src="https://maps.googleapis.com/maps/api/js?sensor=false"></script> -->
    <script src="https://gist-gees.motc.gov.tw/thsr_map/query?request=Json&var=geeServerDefs"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://maps.google.com/maps/api/js?libraries=drawing&key=AIzaSyDRO6pUUDtvtd1AlVBsnUn4V7g4h8EcUn4"></script>

    <script type="text/javascript">
        var map;
        //var map = new google.maps.Map(document.getElementById("map-canvas"),mapOptions);
        var urls = {
            'forest': { link: 'https://dl.dropboxusercontent.com/u/153789/kmz/kmzforest.kmz' },
            'interchange': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/interchange.kmz' },
            'tollstations': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tollstations.kmz' },
            'tunnel': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz' }
        };

        var kmlLayerOptions = {
            preserveViewport: true,
            suppressInfoWindows: true
        };

        function initialize() {

            var mapOptions = {
                center: new google.maps.LatLng(23.16032, 120.349228),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            //alert(document.getElementById("y").value);
            //alert(document.getElementById("x").value);
            var map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
            //alert(document.getElementById("map-canvas").id);
            //map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

            //下面的座標可以換成要顯示的座標位置
            var myLatLng = new google.maps.LatLng(23.17, 120.35);
            var beachMarker = new google.maps.Marker({
                position: myLatLng,
                map: map,
            });

            //下面的字串換成點了地圖後的 marker後顯示的文字
            var infowindow = new google.maps.InfoWindow({
                //content: "Test"
                content: document.getElementById("Text_Location").value
            });

            google.maps.event.addListener(beachMarker, 'click', function () {
                infowindow.open(map, beachMarker);
            });
        }

        function toggleKMLs(checked, id) {

            var _id = (typeof id == 'undefined' ? '' : id);

            if (checked) {
                console.log('call addLayers ' + id);
                if (urls[_id]['link'].length > 0) {
                    var layer = new google.maps.KmlLayer({
                        url: urls[_id]['link']
                    });

                    urls[_id].obj = layer;
                    urls[_id].obj.setMap(map);
                    //console.log(urlsary[_id].obj)
                }
            } else {
                console.log('remove layers ' + id);
                if (urls[_id]['link'].length > 0) {

                    urls[_id].obj.setMap(null);
                    console.log('did to remove');

                }
            }
            return true;
        }
        google.maps.event.addDomListener(window, 'load', initialize);
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
      <link href="../Control/fun01_011.aspx_files/style1.css" rel="stylesheet" />
    <script src="../Control/fun01_011.aspx_files/WebResource.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
                <a href="../newdefault.aspx" >
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/control/fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
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
                                 <a href="fun01_011_.aspx" class="menu4">生態調查資料</a> &gt;  
                                 <a href="fun01_011_.aspx" class="menu4">物種瀏覽</a> 
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
                                        <asp:TextBox ID="Chinese_name" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="ScientificName" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="Density" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="Collector_ch" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="Way_ch" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
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
                                        <asp:TextBox ID="TM2_X" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="TM2_Y" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="inaccuracy" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="Group" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="sec_record" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="habit" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="400">
                                    <asp:TableCell CssClass="explain4">
                                        <asp:TextBox ID="siteid" runat="server" Text="" Width="400" BorderStyle="None" CssClass="explain4" Enabled="false" BackColor="White"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#e0e0e0" Width="400">
                                    <asp:TableCell></asp:TableCell>
                                </asp:TableRow>

                                
                            </asp:Table>

                        </asp:TableCell>

                        <asp:TableCell Width="600" Height="640" VerticalAlign="Top" HorizontalAlign="Center" BorderStyle="None">
                            
                              <iframe src="<%=map_url%>" style="height: 640px; width: 560px;" ></iframe>
                            <br/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>


        <asp:TableRow VerticalAlign="Top" BackColor="#a7a7a7">
            <asp:TableCell BackColor="#a7a7a7">
            </asp:TableCell>

        </asp:TableRow>

        <asp:TableRow VerticalAlign="Top" BackColor="#f7f7f7" Visible="False">
            <asp:TableCell>
                <asp:Table ID="Table1" runat="server" Width="1000">
                    <asp:TableRow>
                        <asp:TableCell>
                            <div>
                                <asp:Menu ID="Menu1" Width="1000" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
                                    <Items>
                                        <asp:MenuItem ImageUrl="~/Images/frame/e03.jpg" Text=" " Value="0"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Images/frame/e04.jpg" Text=" " Value="1"></asp:MenuItem>
                                        <asp:MenuItem ImageUrl="~/Images/frame/e05.jpg" Text=" " Value="2"></asp:MenuItem>
                                    </Items>
                                </asp:Menu>

                                <br />
                                <br />

                                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                       <asp:View ID="Tab1" runat="server"  >
                                            <table width="1000px" height="800px" cellpadding="0" cellspacing="0">
                                                <tr valign="top">
                                                    <td style="width: 800px">
                                                        <span class="menu13">
                                                            形態特徵
                                                        </span> 
                                                        <br/>
                                                        <br/>
                                                        <%
                                                            string accept_name_code = Request.QueryString[2].ToString();

                                                            //string url = "http://eoldata.taibif.tw/eol/endpoint/taxondesc/species/380267";
                                                            //string url_description = "http://eoldata.taibif.tw/eol/endpoint/taxondesc/species/";
                                                            string url_description = "http://api.taibif.tw/catalogue/description/taieol/";
                                                            url_description += accept_name_code;
                                                            if (!accept_name_code.Equals(""))
                                                            {
                                                                LoadDescription(url_description);
                                                            }
                                                            else
                                                            {
                                                                Response.Write("查無此物種說明資料");
                                                            }
                                                        %>
                                                    </td>
                                                </tr>
                                            </table>
                                         </asp:View>
                                        <asp:View ID="Tab2" runat="server">
                                            <table width="1000px" height="800px" cellpadding="0" cellspacing="0">
                                                <tr valign="top">
                                                    <td style="width: 800px">
                                                        <span class="menu13">
                                                            出現紀錄
                                                        </span>
                                                        <br/>
                                                        <br/>
                                                        <%
                                                            //string ScientificName = Request.QueryString[1].ToString();
                                                            //string ScientificName = "Ardea alba";
                                                            string ScientificName = Request.QueryString[1].ToString();
                                                            string table_service = "http://taibif.org.tw/taibif_search/table_service.php?asc=" + ScientificName;
                                                            string map_service = "http://taibif.org.tw/taibif_search/map_service.php?asc=" + ScientificName;
                                                        %>
                                                        <iframe src="<%Response.Write(table_service);%>" style="height: 300px; width: 1000px;" ></iframe>
                                                        <br/>
                                                        <iframe src="<%Response.Write(map_service);%>" style="height: 600px; width: 1000px;" ></iframe>
                                                        <br/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="Tab3" runat="server">
                                            <table width="1000px" height="800px" cellpadding=0 cellspacing=0>
                                                <tr valign="top">
                                                    <td style="width: 800px">
                                                    <span class="menu13">
                                                        多媒體資料
                                                    </span>
                                                    <br/>
                                                    <br/>
                                                    <%
                                                        //String url = "http://eoldata.taibif.tw/eol/endpoint/image/species/420998";
                                                        string accept_name_code = Request.QueryString[2].ToString();
                                                        //String url_image = "http://eoldata.taibif.tw/eol/endpoint/image/species/";
                                                        string url_image = " http://api.taibif.tw//catalogue/images/taieol/";
                                                        url_image += accept_name_code;
                                                        //String url = "http://eoldata.taibif.tw/eol/endpoint/image/species/" + name_code;

                                                        if (!accept_name_code.Equals(""))
                                                        {
                                                            LoadPict(url_image);
                                                        }
                                                        else
                                                        {
                                                            Response.Write("查無此物種圖片資料");
                                                        }
                                                    %>

                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
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

