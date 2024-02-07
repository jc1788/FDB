<%@ page language="C#" autoeventwireup="true" inherits="View_gmap2, App_Web_qgy0chct" %>

<!DOCTYPE html>

<html>
  <head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
      <link href="https://code.google.com/apis/maps/documentation/javascript/examples/standard.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      html { height: 100% }
      body { height: 100%; margin: 0; padding: 0 }
      #map-canvas { height: 100% }
    </style>

      <title></title>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCqbDyzkJIxoRAvez7VwmQMOdcOhgMAPto">
    </script>
      
    <script type="text/javascript" src="../Scripts/markerclusterer.js"></script>

    

    
      <script type="text/javascript">
        var map;
        var url = window.location.toString(); //取得當前網址
        var str = ""; //參數中等號左邊的值
        var str_value = ""; //參數中等號右邊的值
        var x = "";
        var y = "";


        //var map = new google.maps.Map(document.getElementById("map-canvas"),mapOptions);
        var urls = {
            'forest': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/kmzforest.kmz' },
            'interchange': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/interchange.kmz' },
            'tollstations': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tollstations.kmz' },
            'tunnel': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz' },
            'smap': { link: 'http://smap.swcb.gov.tw/kml/kml2009123171140.kml?tmp=201382119148' },
            'taipei': { link: 'http://www.taipei.gov.tw/public/Attachment/042715285592.kml' }
        };

        var kmlLayerOptions = {
            preserveViewport: true,
            suppressInfoWindows: true
        };

        function initialize() {
            var mapOptions = {
                center: new google.maps.LatLng(23.7048, 120.9594),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            //map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);

            //下面的座標可以換成要顯示的座標位置
            
            var myLatLng = new google.maps.LatLng(22.983333333333, 120.81666666667);
            var beachMarker = new google.maps.Marker({
            //    position: myLatLng,
                map: map,
            });
            

            //下面的字串換成點了地圖後的 marker後顯示的文字
		    var infowindow = new google.maps.InfoWindow({
                content:""});
          
		        google.maps.event.addListener(beachMarker, 'click', function() {
                infowindow.open(map,beachMarker);
            });
		    
		    var markers = [];
            var xy_array = document.getElementById('mapxy').value.split(';');
            //alert(xy_array.length);

            for (var i = 0; i < xy_array.length; i++) {
                var xy = xy_array[i].split(',');
                
                var latLng = new google.maps.LatLng(xy[0], xy[1]);
                
                //var marker = new google.maps.Marker({ 'position': latLng });
                
                /*
                for (var j = 0; j < xy.length; j++) {
                    var latLng = new google.maps.LatLng(xy[0],xy[1]);
                    var marker = new google.maps.Marker({ 'position': latLng });
                    
                }*/
                //alert(latLng);
                //alert(markers.push(marker));
                
                //markers.push(marker);
                

                //var latLng = new google.maps.LatLng(23.123,123.123);
                /*
                var xy = xy_array[i].split(',');
                for (var j = 0; j < xy.length; j++) {
                    var latLng = new google.maps.LatLng(xy[1],xy[0]);
                    var marker = new google.maps.Marker({'position': latLng });
                }
                */

                //var latLng = new google.maps.LatLng(xy_array[i]);
                //var marker = new google.maps.Marker({ 'position': latLng });

                //alert(xy_array[i]);
                //var marker = new google.maps.Marker({
                //    position: latLng,
                //    map: map
                //});
                //markersArray.push(marker);
                
                //
            }
            //var markerCluster = new MarkerClusterer(map, markers);
            
            var Layer = new google.maps.KmlLayer("https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz", { suppressInfoWindows: true });
            //alert(Layer.url);
            //将KML设置到地图
            Layer.setMap(map)
            //toggleKMLs();
            /*
            //抓取session資料產生mark
		    var markers = [];
            for (var i = 0; i < 100; i++) {
                var latLng = new google.maps.LatLng(data.photos[i].latitude,
                                                    data.photos[i].longitude);
                var marker = new google.maps.Marker({'position': latLng});
                markers.push(marker);
                }
                var markerCluster = new MarkerClusterer(map, markers);
            }
            */

            google.maps.event.addListener(beachMarker, 'click', function () {
                infowindow.open(map, beachMarker);
            });
        }




        function toggleKMLs(checked, id) {

            var _id = (typeof id == 'undefined' ? '' : id);

            if (checked) {
                console.log('call addLayers ' + id);
                //alert(urls[_id]['link'].length);
                if (urls[_id]['link'].length > 0) {

                    //var layer = new google.maps.KmlLayer({
                    //    url: urls[_id]['link']
                    //});

                    var layer = new google.maps.KmlLayer("http://gemvg.com/ge/kml/20060831wulie.kmz", { suppressInfoWindows: true });
                    //将KML设置到地图
                    //layer.setMap(map);
                    //alert(layer.url);
                    
                    //alert(urls[_id]['link']);
                    urls[_id].obj = layer;
                    urls[_id].obj.setMap(map);
                    //alert(layer.url);
                    //layer.setMap(map);
                    //window.location.reload();
                    //layer.setMap(map);
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



      
  </head>
  <body>
        
            <div id="map-canvas"></div>
            <div class="gmnoprint" style="z-index: 0; position: absolute; right: 7px; top: 34px;">
              <br/>
              <div style="color: rgb(0, 0, 204); background-color: white;" >
                    森林 Layer<input type="checkbox" id="forest" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                    交流道 Layer<input type="checkbox" id="interchange" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
       
                    收費站 Layer<input type="checkbox" id="tollstations" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                    隧道 Layer<input type="checkbox" id="tunnel" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
              </div>
          
              <!--green Layer<input type="checkbox" id="green" onchange="toggleKMLs(this.checked,this.id);;return false;"><br>-->
              <!--<div style="color: rgb(0, 0, 204); background-color: white; font-style: normal; font-variant: normal; font-weight: normal; font-size: small; line-height: normal; font-family: Arial; border: 1px solid black; padding: 2px; margin-bottom: 3px; text-align: center; width: 6em; cursor: pointer;">顯示/隱藏森林圖層</div>-->
            </div>
        <form id="form1" runat="server">
            <asp:TextBox ID="mapxy" runat="server" Text="" Visible="True" width="800"/>
        </form>
  </body>

    <script type="text/javascript">
        var map;
        var url = window.location.toString(); //取得當前網址
        var str = ""; //參數中等號左邊的值
        var str_value = ""; //參數中等號右邊的值
        var x = "";
        var y = "";


        //var map = new google.maps.Map(document.getElementById("map-canvas"),mapOptions);
        var urls = {
            'forest': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/forest.kmz' },
            'interchange': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/interchange.kmz' },
            'tollstations': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tollstations.kmz' },
            'tunnel': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz' }
        };

        var kmlLayerOptions = {
            preserveViewport: true,
            suppressInfoWindows: true
        };

        function initialize_kml(checked, id) {
            var mapOptions = {
                center: new google.maps.LatLng(23.7048, 120.9594),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);


            var myLatLng = new google.maps.LatLng(22.983333333333, 120.81666666667);
            var beachMarker = new google.maps.Marker({
                map: map,
            });

            //下面的字串換成點了地圖後的 marker後顯示的文字
            var infowindow = new google.maps.InfoWindow({
                content: "Test"
            });

            /*
            google.maps.event.addListener(beachMarker, 'click', function () {
                infowindow.open(map, beachMarker);
            });
            */
            var markers = [];
            //var marker = [];
            //var marker  = new google.maps.Marker({})[];
            var xy_array = document.getElementById('mapxy').value.split(';');
            for (var i = 0; i < xy_array.length-1; i++) {
                var xy = xy_array[i].split(',');
                //alert(xy[1]);
                var url = "fun01_022.aspx?x=" + xy[1] + "&y=" + xy[0];
                var title = "經度 : " + xy[1] + "\n" + "緯度 : " + xy[0];
                var latLng = new google.maps.LatLng(xy[0], xy[1]);
                var marker = new google.maps.Marker({ 'position': latLng, 'title': title , 'url':url });
                //marker[i] = new google.maps.Marker({ 'position': latLng, 'title': url, 'url': url });
                //google.maps.event.addListener(marker[i], "click", function () {
                
                google.maps.event.addListener(marker, "click", function () {
                    //window.open('fun01_022.aspx?x=' + xy[0] + '&y=' + xy[1], '_self');
                    window.open(this.url, '_blank');
                    //window.open(marker.url, '_blank');
                    //alert(this.title);
                });
                markers.push(marker);
            }
            //markers.push(marker);
            
            
            /*
            var marker = new GMarker(location);
            GEvent.addListener(marker, "click", function () {
                window.location = theURL;
            });
            map.addOverlay(marker);
            */
            var markerCluster = new MarkerClusterer(map, markers);

            //圖層顯示
            var _id = (typeof id == 'undefined' ? '' : id);

            if (checked) {
                console.log('call addLayers ' + id);
                if (urls[_id]['link'].length > 0) {
                    //var Layer = new google.maps.KmlLayer("https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz", { suppressInfoWindows: true });
                    var Layer = new google.maps.KmlLayer(urls[_id]['link'], { suppressInfoWindows: true });
                    Layer.setMap(map)
                }
            } else {
                console.log('remove layers ' + id);
                if (urls[_id]['link'].length > 0) {

                    urls[_id].obj.setMap(null);
                    console.log('did to remove');

                }
            }
            
            /*
            google.maps.event.addListener(beachMarker, 'click', function () {
                infowindow.open(map, beachMarker);
            });
            */
        }

        google.maps.event.addDomListener(window, 'load', initialize_kml);
    </script>
</html>
