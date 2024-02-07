<%@ page language="C#" autoeventwireup="true" inherits="View_gmap03_011a, App_Web_qgy0chct" %>

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
    
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCqbDyzkJIxoRAvez7VwmQMOdcOhgMAPto"></script>
    <script type="text/javascript" src="../Scripts/markerclusterer.js"></script>
    <!--<script type="https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/markerclusterer.js"></script>-->

  </head>
  <body>
        <br/>
        <div id="map-canvas"></div>
        <div class="gmnoprint" style="z-index: 0; position: absolute; right: 7px; top: 34px;">
            <br/>
        </div>
        <form id="form1" runat="server">
            <asp:TextBox ID="mapxy" runat="server" Text="" Visible="True" width="600"/>
            
        </form>
  </body>

    <script type="text/javascript">
        var map;
        var url = window.location.toString(); //取得當前網址
        var str = ""; //參數中等號左邊的值
        var str_value = ""; //參數中等號右邊的值
        var x = "";
        var y = "";
        var highestZIndex = 0;
        var markers = [];
        //var mks;


        function initialize_kml() {
            //資料最大數量，用紅色表示
            var max_amount = "";
            //地圖中心點經緯度
            var center_x = "";
            var center_y = "";
            var xy_array = document.getElementById('mapxy').value.split(';');

            
            //抓取所需的資料
            for (var i = 0; i < xy_array.length - 1; i++) {
                //alert(i);
                var xy = xy_array[i].split(',');
                max_amount = xy[3];
                center_x = xy[4];
                center_y = xy[5];
                //中心點改為最大值那點
                
                if (xy[0] != '' && xy[1] != '') {
                    
                    if (xy[2] == xy[3]) {
                        
                        //alert(xy[1]);
                        center_x = xy[1];
                        center_y = xy[0];
                    }
                }
                
            }

            //alert(center_y);
            //alert(center_x);
            var mapOptions = {
                center: new google.maps.LatLng(center_y, center_x),
                zoom: 7,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

            //下面的字串換成點了地圖後的 marker後顯示的文字
            var infowindow = new google.maps.InfoWindow({ content: "" });

            for (var i = 0; i < xy_array.length - 1 ; i++) {

                var xy = xy_array[i].split(',');
                var url_string = "fun03_011e.aspx?center_x=" + xy[1] + "&center_y=" + xy[0] + "&chinese_name=" + xy[6];
                var title_string = xy[6] + " : 里程 : " + xy[7];
                var latLng = new google.maps.LatLng(xy[0], xy[1]);
                var icon_string = "";
                //代表最大的圖
                if (xy[2] == xy[3]) {
                    icon_string = "../Images/marker_icon/Purple/icon_green_" + xy[2] + ".png";
                } else {
                    icon_string = "../Images/marker_icon/Green/icon_green_" + xy[2] + ".png";
                }

                var markerOptions = {
                    //position: {lat: xy[0], lng: xy[1]},
                    position: latLng,
                    map: map,
                    title: title_string,
                    //url: url_string,
                    icon: icon_string,
                    zIndex: i + 1 // important! ，由 1 開始 
                };

                //alert(markerOptions.url);
                //var marker = new google.maps.Marker(markerOptions);
                var markerCluster = new MarkerClusterer(map, markers, { imagePath: '../Images/m' });
                
                marker.set("myZIndex", i);
                markers.push(marker);
                /*
				google.maps.event.addListener(marker, 'click', function() {
					window.open(url_string, '_blank');
					//window.location.href = marker.url;
				});
				*/
                google.maps.event.addListener(marker, "mouseover", function () {
                    getHighestZIndex();
                    this.setOptions({ zIndex: highestZIndex });
                    marker.setIcon(icon_string);
                });
                google.maps.event.addListener(marker, "mouseout", function () {
                    this.setOptions({ zIndex: this.get("myZIndex") });
                    marker.setIcon(icon_string);
                });

                //alert(marker.markerOptions);



                //marker event click 改寫到裡面
                /*
                (function (marker, i) {
                    // add click event
                    google.maps.event.addListener(marker, "click", function () {
                        window.open(marker.url, '_blank');
                    });
					google.maps.event.addListener(marker, "mouseover", function() { 
						//alert(i);
						getHighestZIndex();  
						//alert(i);
						this.setOptions({zIndex:highestZIndex});  
						marker.setIcon(icon_string);
						
					});  
                })(marker, i);
				*/

                //var markerCluster = new MarkerClusterer(map, markers);

            }
        }

        google.maps.event.addDomListener(window, 'load', initialize_kml);

        function getHighestZIndex() {

            // if we haven't previously got the highest zIndex  
            // save it as no need to do it multiple times  
            if (highestZIndex == 0) {
                if (markers.length > 0) {
                    // loop through markers 
                    for (var i = 0; i < markers.length; i++) {
                        tempZIndex = markers[i].getZIndex();
                        //alert(tempZIndex);								
                        // if this zIndex is the highest so far  
                        if (tempZIndex > highestZIndex) {
                            highestZIndex = tempZIndex;
                        }
                    }
                }
            }
            return highestZIndex;
        }
    </script>
</html>