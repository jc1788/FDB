<%@ page language="C#" autoeventwireup="true" inherits="View_gmap01_021a, App_Web_qgy0chct" %>

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

  </head>
  <body>
        
        <div id="map-canvas"></div>
        <div class="gmnoprint" style="z-index: 0; position: absolute; right: 7px; top: 34px;">
            <br/>
            <!--
            <div style="color: rgb(0, 0, 204); background-color: white;" >
                大面積森林<input type="checkbox" id="forest" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                交流道<input type="checkbox" id="interchange" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                隧道<input type="checkbox" id="tunnel" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                一級敏感里程<input type="checkbox" id="sent1" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                二級敏感里程<input type="checkbox" id="sent2" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
                三級敏感里程<input type="checkbox" id="sent3" onchange="initialize_kml(this.checked,this.id);;return false;"><br/>
            </div>
            -->
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
            'tunnel': { link: 'https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz' },
            'sent1': { link: 'https://dl.dropboxusercontent.com/u/16007422/1.kmz' },
            'sent2': { link: 'https://dl.dropboxusercontent.com/u/16007422/2.kmz' },
            'sent3': { link: 'https://dl.dropboxusercontent.com/u/16007422/3.kmz' }
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
            for (var i = 0; i < xy_array.length - 1; i++) {
                var xy = xy_array[i].split(',');
                //alert(xy[1]);
                var url = "fun01_022.aspx?x=" + xy[1] + "&y=" + xy[0];
                var title = "經度 : " + xy[1] + "\n" + "緯度 : " + xy[0];
                var latLng = new google.maps.LatLng(xy[0], xy[1]);
                var marker = new google.maps.Marker({ 'position': latLng, 'title': title, 'url': url });
                //marker[i] = new google.maps.Marker({ 'position': latLng, 'title': url, 'url': url });
                //google.maps.event.addListener(marker[i], "click", function () {

                google.maps.event.addListener(marker, "click", function () {
                    //window.open('fun01_022.aspx?x=' + xy[0] + '&y=' + xy[1], '_self');
//                    window.open(this.url, '_blank');
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
            //var markerCluster = new MarkerClusterer(map, markers);
            var markerCluster = new MarkerClusterer(map, markers, { imagePath: '../Images/m' });

            //圖層顯示
            var _id = (typeof id == 'undefined' ? '' : id);

            //原始圖層
            /*
            if (checked) {
                console.log('call addLayers ' + id);
                if (urls[_id]['link'].length > 0) {
                    //var Layer = new google.maps.KmlLayer("https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz", { suppressInfoWindows: true });
                    var Layer = new google.maps.KmlLayer(urls[_id]['link'], { suppressInfoWindows: true });
                    Layer.setMap(map)
                }
            }

            else {
                console.log('remove layers ' + id);
                if (urls[_id]['link'].length > 0) {

                    urls[_id].obj.setMap(null);
                    console.log('did to remove');

                }
            }
            */

            //修改後可以套疊的圖層
	    /*
            if (document.getElementById('forest').checked) {
                var Layer_forest = new google.maps.KmlLayer('https://dl.dropboxusercontent.com/u/153789/maps/kmz/forest.kmz');
                Layer_forest.setMap(map)
            }

            if (document.getElementById('interchange').checked) {
                var Layer_interchange = new google.maps.KmlLayer('https://dl.dropboxusercontent.com/u/153789/maps/kmz/interchange.kmz');
                Layer_interchange.setMap(map)
            }

            if (document.getElementById('tunnel').checked) {
                var Layer_tunnel = new google.maps.KmlLayer('https://dl.dropboxusercontent.com/u/153789/maps/kmz/tunnel.kmz');
                Layer_tunnel.setMap(map)
            }

            if (document.getElementById('sent1').checked) {
                var Layer_sent1 = new google.maps.KmlLayer('https://dl.dropboxusercontent.com/u/16007422/1.kmz');
                Layer_sent1.setMap(map)
            }

            if (document.getElementById('sent2').checked) {
                var Layer_sent2 = new google.maps.KmlLayer('https://dl.dropboxusercontent.com/u/16007422/2.kmz');
                Layer_sent2.setMap(map)
            }

            if (document.getElementById('sent3').checked) {
                var Layer_sent3 = new google.maps.KmlLayer('https://dl.dropboxusercontent.com/u/16007422/3.kmz');
                Layer_sent3.setMap(map)
            }
	    */

            /*
            google.maps.event.addListener(beachMarker, 'click', function () {
                infowindow.open(map, beachMarker);
            });
            */
        }

        google.maps.event.addDomListener(window, 'load', initialize_kml);
    </script>
</html>