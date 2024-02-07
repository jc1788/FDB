<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gmap1.aspx.cs" Inherits="View_gmap1" %>

<!DOCTYPE html>


<html>
  <head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <title></title>

    <style>
        html, body, #map-canvas {
            height: 100%; 
            margin: 0px;  
            padding: 0px  
        }
    </style>

    <%--<script type="text/javascript" src="https://gist-geo.motc.gov.tw/webAPI/maps/api/js?clientId=tccg-a2luZ3dhbmdAZnJlZXdheS5nb3YudHc=0"></script>--%>
    <%--<script type="text/javascript" src="https://gist-gees.motc.gov.tw/thsr_map/query?request=Json&var=geeServerDefs"></script>--%>
	<%--<script type="text/javascript" src="../Scripts/markerclusterer.js"></script>--%>
        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCqbDyzkJIxoRAvez7VwmQMOdcOhgMAPto"></script>


    <script type="text/javascript">
        //New Map
        var gmap;
        var geeFusionMap;

        var kmlLayerOptions = {
            preserveViewport: true,
            suppressInfoWindows: true
        };

        var map;
        var url = window.location.toString(); //取得當前網址
        var str = ""; //參數中等號左邊的值
        var str_value = ""; //參數中等號右邊的值
        var x = "";//緯度
        var y = "";//經度
        var x1 = "";
        var y1 = "";
        var lo = "";//照片


        //抓取URL中的x與y
        if (url.indexOf("?") != -1) {
            //如果網址有"?"符號
            var ary = url.split("?")[1].split("&");
            //取得"?"右邊網址後利用"&"分割字串存入ary陣列 ["a=1","b=2","c=3"]
            for (var i in ary) {
                //取得陣列長度去跑迴圈，如:網址有三個參數，則會跑三次
                str = ary[i].split("=")[0];
                //取得參數"="左邊的值存入str變數中
                if (str == "x") {
                    //若str等於想要抓取參數 如:b
                    x = decodeURI(ary[i].split("=")[1]);
                    //取得b等號右邊的值並經過中文轉碼後存入str_value
                }

                if (str == "y") {
                    //若str等於想要抓取參數 如:b
                    y = decodeURI(ary[i].split("=")[1]);
                    //取得b等號右邊的值並經過中文轉碼後存入str_value
                }

                if (str == "lo") {
                    //若str等於想要抓取參數 如:b
                    lo = decodeURI(ary[i].split("=")[1]);
                    //取得b等號右邊的值並經過中文轉碼後存入str_value
                }
            }
        }

        //New Map
        //var kmlLayerOptions = {
        //    preserveViewport: true,
        //    suppressInfoWindows: true
        //};

        function LoadMap() {
            if (x != "" && y != "") {
                var mapOpts = {
                    zoom: 8,
                    center: new google.maps.LatLng(y, x),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                    //navigationControl: true,
                    //scaleControl: true
                };
            } else {
                var mapOpts = {
                    zoom: 8,
                    center: new google.maps.LatLng(23.7, 121.1),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                    //navigationControl: true,
                    //scaleControl: true
                };
            }
            geeFusionMap = new google.maps.Map(document.getElementById("map-canvas"), mapOpts);
            //geeFusionMap = new gee.GEFusionMap("map-canvas", mapOpts);
            //gmap = geeFusionMap.map;

            //initialize_data();
            //新增map marker
            //var beachMarker = new google.maps.Marker({
            //    map: gmap,
            //});

            //var infowindow = new google.maps.InfoWindow({
            //    content: "Test"
            //});
            //alert(lo);
            if (x != "" & y != "") {
                var contentString = '<div id="content">' +
                    '<div id="siteNotice">' +
                    '</div>' +
                    '<div id="bodyContent">' +
                    '<p><img src="' + lo + '" width=240 heigh=240>' +
                    '</div>' +
                    '</div>';
                var title = "經度 : " + x + "\n" + "緯度 : " + y;
                var url = lo;

                var markers = [];
                var latLng = new google.maps.LatLng(y, x);
                //var marker = new google.maps.Marker({ 'position': latLng, 'title': title, 'url': url });
                var marker = new google.maps.Marker({
                    position: latLng,
                    icon: "https://maps.google.com/mapfiles/ms/icons/red-dot.png",
                    //draggable: true,
                    //title: '起始里程'
                });
                marker.setMap(geeFusionMap);
                //var infowindow = new google.maps.InfoWindow({
                //    content: contentString
                //});

                //google.maps.event.addListener(marker, "click", function () {
                //    //window.open(this.url, '_blank');
                //    infowindow.open(gmap, marker);
                //});
                //markers.push(marker);

                ////var markerCluster = new MarkerClusterer(gmap, markers);
                //var markerCluster = new MarkerClusterer(map, markers, { imagePath: '../Images/m' });
            }
        }
        <!--
        function toggleLayer(id) {
            if (id == 2) {
                //geeFusionMap.hideFusionLayer(geeServerDefs.layers[1].glm_id + '-' + geeServerDefs.layers[1].id); 

                //geeFusionMap.hideFusionLayer('motc_plus_map-1046');   // 縣市/鄉鎮
                //geeFusionMap.hideFusionLayer('motc_plus_map-1187');   // 交流道
                //geeFusionMap.hideFusionLayer('motc_plus_map-1574');   // ETC 
                geeFusionMap.hideFusionLayer('VA0246V02');   // 縣市/鄉鎮
                geeFusionMap.hideFusionLayer('VA0308V02');   // 鄉鎮市界 
                geeFusionMap.hideFusionLayer('VA0686');      // 村里界
                geeFusionMap.hideFusionLayer('VL0699');      // 國道 
                geeFusionMap.hideFusionLayer('VL0694');      // 省道 
                document.getElementById('Button1').disabled = false;
                document.getElementById('Button3').disabled = false;
                document.getElementById('Button4').disabled = false;
                document.getElementById('Button5').disabled = false;
                document.getElementById('Button6').disabled = false;
            }
            else if (id == 1) {
                geeFusionMap.showFusionLayer(geeServerDefs.layers[1].glm_id + '-' + geeServerDefs.layers[1].id);
                //geeFusionMap.hideFusionLayer('motc_plus_map-1046');   // 縣市/鄉鎮
                geeFusionMap.hideFusionLayer('VA0246V02');   // 縣市/鄉鎮
                document.getElementById('Button1').disabled = true;

            }
            else if (id == 3) {
                geeFusionMap.showFusionLayer(geeServerDefs.layers[1].glm_id + '-' + geeServerDefs.layers[1].id);
                //geeFusionMap.showFusionLayer('motc_plus_map-1187');   // 交流道
                geeFusionMap.hideFusionLayer('VA0308V02');   // 鄉鎮市界 
                document.getElementById('Button3').disabled = true;
            }
            else if (id == 4) {
                geeFusionMap.showFusionLayer(geeServerDefs.layers[1].glm_id + '-' + geeServerDefs.layers[1].id);
                //geeFusionMap.showFusionLayer('motc_plus_map-1574');   // ETC 
                geeFusionMap.hideFusionLayer('VA0686');      // 村里界
                document.getElementById('Button4').disabled = true;
            }
            else if (id == 5) {
                geeFusionMap.showFusionLayer(geeServerDefs.layers[1].glm_id + '-' + geeServerDefs.layers[1].id);
                geeFusionMap.hideFusionLayer('VL0699');      // 國道 
                document.getElementById('Button5').disabled = true;
            }
            else if (id == 6) {
                geeFusionMap.showFusionLayer(geeServerDefs.layers[1].glm_id + '-' + geeServerDefs.layers[1].id);
                geeFusionMap.hideFusionLayer('VL0694');      // 省道 
                document.getElementById('Button6').disabled = true;
            }
            else {
                alert('error');
            }
        }
        -->
        window.onload = LoadMap;

        //New Map


    </script>

  </head>
  <body>
    <div id="map-canvas"></div>
	  
	<div class="gmnoprint" style="z-index: 0; position: absolute; right: 7px; top: 34px;">
        <!--
        <input id="Button1" type="button" value="縣市界" OnClick="toggleLayer(1);"/>
        <input id="Button3" type="button" value="鄉鎮市界" OnClick="toggleLayer(3);"/>
        <input id="Button4" type="button" value="村里界" OnClick="toggleLayer(4);"/>
        <input id="Button5" type="button" value="國道" OnClick="toggleLayer(5);"/>
        <input id="Button6" type="button" value="省道" OnClick="toggleLayer(6);"/>
	    <input id="Button2" type="button" value="關閉圖層" OnClick="toggleLayer(2);"/> 
        -->
        <!--
	    <input id="Button1" type="button" value="縣市/鄉鎮" OnClick="toggleLayer(1);"/>
        <input id="Button3" type="button" value="交流道" OnClick="toggleLayer(3);"/>
        <input id="Button4" type="button" value="ETC" OnClick="toggleLayer(4);"/>
	    <input id="Button2" type="button" value="關閉圖層" OnClick="toggleLayer(2);"/> 
        -->
        <br/>
    </div>

  </body>
</html>