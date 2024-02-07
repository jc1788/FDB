<%@ page language="C#" autoeventwireup="true" inherits="View_gmap3, App_Web_qgy0chct" %>

<!DOCTYPE html>

<html>  
    <head>    
        <meta name="viewport" content="initial-scale=1.0, user-scalable=no">    
        <meta charset="utf-8">    
        <title>KML Layers</title>    
         
        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCqbDyzkJIxoRAvez7VwmQMOdcOhgMAPto"></script>    
        <script>
            function initialize() {
                //alert("aaaa");
                var chicago = new google.maps.LatLng(23.7048, 120.9594);
                var mapOptions = { zoom: 11, center: chicago, mapTypeId: google.maps.MapTypeId.ROADMAP }
                var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
                var ctaLayer = new google.maps.KmlLayer({ url: 'http://gmaps-samples.googlecode.com/svn/trunk/ggeoxml/cta.kml' });
                ctaLayer.setMap(map);
            }
            google.maps.event.addDomListener(window, 'load', initialize);

        </script>  

    </head>  
    <body>    
        aaa
        <div id="map-canvas"></div>  
        bbb
    </body>

</html>
