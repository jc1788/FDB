<%@ page language="C#" autoeventwireup="true" inherits="View_gmap02_011a, App_Web_qgy0chct" %>

<!DOCTYPE html>

<html>
  <head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
      html { height: 100% }
      body { height: 100%; margin: 0; padding: 0 }
      #map-canvas { height: 100% }
    </style>

    <title></title>
    <!--leafletjs-->
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"
    integrity="sha512-xodZBNTC5n17Xt2atTPuE1HxjVMSvLVW9ocqUKLsCC5CXdbqCmblAshOMAS6/keqq/sMZMZ19scR4PsZChSR7A=="
    crossorigin=""/>
     <!-- Make sure you put this AFTER Leaflet's CSS -->
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"
    integrity="sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA=="
    crossorigin=""></script>
    <!-- Leaflet.markercluster -->
    <link rel="stylesheet" href="https://unpkg.com/leaflet.markercluster@1.4.1/dist/MarkerCluster.Default.css" />
    <script src="https://unpkg.com/leaflet.markercluster@1.4.1/dist/leaflet.markercluster.js"></script>
    <!-- Leaflet.fullscreen -->
    <script src="https://unpkg.com/leaflet-fullscreen@1.0.2/dist/Leaflet.fullscreen.min.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/leaflet-fullscreen@1.0.2/dist/leaflet.fullscreen.css" />
    <link rel="stylesheet" href="https://maxcdn.icons8.com/fonts/line-awesome/1.1/css/line-awesome.min.css">

    <style type="text/css">
        .map-icon {
            background: rgba(231, 19, 87, 0.8);
            border: 2px solid rgba(255, 255, 255, 1);
            font-weight: bold;
            text-align: center;
            border-radius: 50%;
            line-height: 30px;
        }
    </style>

    <script type="text/javascript">
    let map;

    function init() {
	map = L.map('map-canvas', { fullscreenControl: true, scrollWheelZoom: false }).setView([23.4, 121.0], 8);

                let wmtsId = 'EMAP98';
                let tsUrl = 'https://wmts.nlsc.gov.tw/wmts/' + wmtsId + '/default/GoogleMapsCompatible/{z}/{y}/{x}';

                L.tileLayer(tsUrl, {
                    maxZoom: 19,
                    attribution: 'Tiles &copy; 內政部國土測繪中心',
                    id: wmtsId
                }).addTo(map);

	let markers = L.markerClusterGroup();
	var xy_array = document.getElementById('mapxy').value.split(';');
	for (var i = 0; i < xy_array.length - 1; i++) {
		var xy = xy_array[i].split(',');
		var url = "fun02_022.aspx?x=" + xy[1] + "&y=" + xy[0];
		var title = "經度 : " + xy[1] + "\n" + "緯度 : " + xy[0];
		markers.addLayer(createMarker(xy[0],xy[1],url,title));
	}

	map.addLayer(markers);
    }

    function createMarker(y,x,url,title) {
	let m_icon = createIcon();
	return L.marker([y, x], { title: title, icon: m_icon }).on('click', function(e) {
		window.open(url,'roadkill_detail');
	});
    }

    function createIcon() {
	return L.divIcon({ html: '', className: 'map-icon', iconSize: L.point(16, 16) });
    }

    function createContent(url) {
	return document.getElementById('awt').innerHTML.replace(/{{url}}/g, url);
    }
    </script>

    
  </head>
  <body onload="init()">
        <br/>
        <div id="map-canvas"></div>
        <div class="gmnoprint" style="z-index: 0; position: absolute; right: 7px; top: 34px;">
            <br/>
        </div>
        <form id="form1" runat="server">
            <asp:TextBox ID="mapxy" runat="server" Text="" Visible="True" width="800"/>
        </form>
  </body>

</html>