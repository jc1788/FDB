<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="occurrence_detail.aspx.cs" Inherits="occurrence_detail" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
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
    <link rel="stylesheet" href="https://maxcdn.icons8.com/fonts/line-awesome/1.1/css/line-awesome.min.css">
    <script type="text/javascript">
    let map;

    function init() {
	map = L.map('leafletmap', { fullscreenControl: true, scrollWheelZoom: false }).setView([23.65, 121.0], 8);

                let wmtsId = 'EMAP98';
                let tsUrl = 'https://wmts.nlsc.gov.tw/wmts/' + wmtsId + '/default/GoogleMapsCompatible/{z}/{y}/{x}';

                L.tileLayer(tsUrl, {
                    maxZoom: 19,
                    attribution: 'Tiles &copy; 內政部國土測繪中心',
                    id: wmtsId
                }).addTo(map);

	var Lat = <%=y.Text %>;
	var Lng = <%=x.Text %>;

	L.marker([Lat, Lng]).addTo(map);
    }

    window.onload = function () {
        init();
    };
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">生態調查資料詳情</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">生態調查資料詳情</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <!-- /.標題+麵包屑 -->
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-search01"
                        role="tab" aria-controls="nav-home" aria-selected="true">詳細資料</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="javascript:window.close()">回上一頁</a></p>
        <div class="row">
            <div class="col-12 col-lg-6">
                <table class="mb-4 itemTable">
                    <tbody>
                        <tr>
                            <th scope="row">物種中文名：</th>
                            <td><asp:Literal ID="Chinese_name" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">物種學名：</th>
                            <td><asp:Literal ID="ScientificName" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">個體數(面積/密度) ：</th>
                            <td><asp:Literal ID="Density" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">調查者中文名：</th>
                            <td><asp:Literal ID="Collector_ch" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">調查方法：</th>
                            <td><asp:Literal ID="Way_ch" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">調查日期：</th>
                            <td><asp:Literal ID="date" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">調查地(中文)：</th>
                            <td><asp:Literal ID="site_ch" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">國道編號：</th>
                            <td><asp:Literal ID="highway_id" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">經度：</th>
                            <td><asp:Literal ID="x" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">緯度：</th>
                            <td><asp:Literal ID="y" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">TM2_X：</th>
                            <td><asp:Literal ID="TM2_X" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">TM2_Y：</th>
                            <td><asp:Literal ID="TM2_Y" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">座標不確定值(公尺) ：</th>
                            <td><asp:Literal ID="inaccuracy" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">物種類群：</th>
                            <td><asp:Literal ID="Group" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">間接紀錄：</th>
                            <td><asp:Literal ID="sec_record" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">棲地類型：</th>
                            <td><asp:Literal ID="habit" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">時間地點代號：</th>
                            <td><asp:Literal ID="siteid" runat="server"></asp:Literal></td>
                        </tr>
                </table>
            </div>
            <div class="col-12 col-lg-6" id="leafletmap">
            </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4">
            <a href="javascript:window.close()" class="btn btn-outline-secondary">回上一頁</a>
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
