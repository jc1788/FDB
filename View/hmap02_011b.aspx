<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hmap02_011b.aspx.cs" Inherits="View_hmap02_011b" %>

<!DOCTYPE html>

<html xmlns="https://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>熱點圖</title>
    <style type="text/css">
        body, html {
            margin:0;
            padding:0;
            font-family:Arial;
        }
        h1 {
            margin-bottom:10px;
        }
        #main {
            position:relative;
            width:520px;
            padding:20px;
            margin:auto;
        }
        #heatmapArea {
            position:relative;
            float:left;
            width:500px;
            height:900px;
            border:1px dashed black;
        }
        #configArea {
            position:relative;
            float:left;
            width:200px;
            padding:15px;
            padding-top:0;
            padding-right:0;
        }
        .btn {
            margin-top:25px;
            padding:10px 20px 10px 20px;
            -moz-border-radius:15px;
            -o-border-radius:15px;
            -webkit-border-radius:15px;
            border-radius:15px;
            border:2px solid black;
            cursor:pointer;
            color:white;
            background-color:black;
        }
        #gen:hover{
            background-color:grey;
            color:black;
        }
        textarea{
            width:260px;
            padding:10px;
            height:200px;
        }
        h2{
            margin-top:0;
        }
 </style>
    <!--leafletjs-->
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" />
     <!-- Make sure you put this AFTER Leaflet's CSS -->
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"></script>
    <!-- Leaflet.markercluster -->
    <link rel="stylesheet" href="https://unpkg.com/leaflet.markercluster@1.4.1/dist/MarkerCluster.Default.css" />
    <script src="https://unpkg.com/leaflet.markercluster@1.4.1/dist/leaflet.markercluster.js"></script>
    <!-- Leaflet.fullscreen -->
    <script src="https://unpkg.com/leaflet-fullscreen@1.0.2/dist/Leaflet.fullscreen.min.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/leaflet-fullscreen@1.0.2/dist/leaflet.fullscreen.css" />
    <link rel="stylesheet" href="https://maxcdn.icons8.com/fonts/line-awesome/1.1/css/line-awesome.min.css"
</head>

<body onload = "init()">
    
    
    <div id="main">
        <div id="heatmapArea">
        </div>
    </div>
    <form id="form1" runat="server">
        <asp:TextBox ID="hmapxy" runat="server" Text="" Visible="True" width="400"/>
        
    </form>
    
</body>


<script type="text/javascript">
    let map;

    function init() {
	map = L.map('heatmapArea', { fullscreenControl: true, scrollWheelZoom: false }).setView([23.4, 121.0], 8);

                let wmtsId = 'EMAP98';
                let tsUrl = 'https://wmts.nlsc.gov.tw/wmts/' + wmtsId + '/default/GoogleMapsCompatible/{z}/{y}/{x}';

                L.tileLayer(tsUrl, {
                    maxZoom: 19,
                    attribution: 'Tiles &copy; 內政部國土測繪中心',
                    id: wmtsId
                }).addTo(map);

        var hxy_array = document.getElementById('hmapxy').value.split(';');
	var max = Number(hxy_array[0].split(',')[4]);
	var min = Number(hxy_array[0].split(',')[4]);
        for (var i = 1; i < hxy_array.length - 1; i++) {
            var hxy = hxy_array[i].split(',');

	    if (Number(hxy[4]) > max)
		max = Number(hxy[4]);
	    else if (Number(hxy[4]) < min)
		min = Number(hxy[4]);
	}

	var step = ( max - min ) / 8;
        for (var i = 0; i < hxy_array.length - 1; i++) {
            var hxy = hxy_array[i].split(',');

	    var coordinates = [[hxy[1],hxy[0]],[hxy[3],hxy[2]]];
	    var color;
	    if (Number(hxy[4])>max-step)
		color = '#C00000';
	    else if (Number(hxy[4])>max-step*2)
		color = '#FF6464';
	    else if (Number(hxy[4])>max-step*3)
		color = '#FFC000';
	    else if (Number(hxy[4])>max-step*4)
		color = '#FFE699';
	    else if (Number(hxy[4])>max-step*5)
		color = '#385723';
	    else if (Number(hxy[4])>max-step*6)
		color = '#548235';
	    else if (Number(hxy[4])>max-step*7)
		color = '#2F5597';
	    else
		color = '#5B9BD5';

	    var line = L.polyline(coordinates,{color:color,weight:8}).addTo(map);
	}
    }

    window.onload = function () {
        init();
    };
</script>
</html>
