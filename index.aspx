<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="_index" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
  <!-- Link Swiper's CSS -->
  <link rel="stylesheet" href="https://unpkg.com/swiper/swiper-bundle.min.css" />
  <!-- chartjs cdn -->
  <script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.4"></script>
  <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
  <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.7.0"></script>
  <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-style@0.5.0/dist/chartjs-plugin-style.min.js"></script>

  <!-- jQ range slider  -->
  <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css">
  <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
  <script src="https://code.jquery.com/ui/1.13.0/jquery-ui.js"></script>

  <!-- production version, optimized for size and speed -->
  <script src="https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js"></script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
  <!-- Swiper -->
  <div class="swiper mySwiper swiperHeight">
    <div class="swiper-wrapper">
      <div class="swiper-slide">
        <img src="./assets/images/freewayBanner01.png" alt="">
        <div class="swiperTextWrap">
          <h2 class="text-white h1 text-shadow-sm">國道生態資料庫</h2>
          <p class="text">
            為瞭解國道設施與景觀綠化對沿線生態環境之影響，展開沿線沿線動植物資源調查，與建立道路致死資料蒐集機制，作為環境復育背景資料，及快速掌握國道生態資源豐富，以供研擬永續利用發展策略，使道路建設與環境更為融合。
          </p>
        </div>
      </div>
      <div class="swiper-slide">
        <img src="./assets/images/freewayBanner02.png" alt="">
        <div class="swiperTextWrap">
          <h2 class="text-white h1 text-shadow-sm">國道生態友善環境復育</h2>
          <p class="text">
            在國道兩側1公里範圍內的17處動物樣區的四季調查共記錄到鳥類148種、哺乳類32種、兩棲類25種、爬蟲類24種、蝶類155種和蜻蛉類72種，包含各級保育類43種和特有(亞)種85種。調查成果除納入資料庫做為長期監測之用外，亦可找出應優先關注的生態課題，以及作為選擇原生適生植栽的參考。
          </p>
        </div>
      </div>
      <div class="swiper-slide">
        <img src="./assets/images/freewayBanner03.png" alt="">
        <div class="swiperTextWrap">
          <h2 class="text-white h1 text-shadow-sm">植物生態調查</h2>
          <p class="text">
            利用衛星影像判釋與現場勘查，篩選國道沿線大面積森林性棲地優先做為敏感里程樣區，並針對一級與二級敏感里程樣區進行陸域植物資源調查。目前共完成24處樣區，包含一級敏感里程樣區14處與二級敏感里程樣區8處，共累積維管束植物173科628屬1097種。
          </p>
        </div>
      </div>
      <div class="swiper-slide">
        <img src="./assets/images/freewayBanner04.png" alt="">
        <div class="swiperTextWrap">
          <h2 class="text-white h1 text-shadow-sm">動物生態調查</h2>
          <p class="text">
            利用遙測影像判釋與現場勘查，優先選出10處大面積森林型棲地，另選出重要濕地棲地類型合計共17處樣區，各進行一年4季次的各類群動物調查，共記錄6類群動物456種(包含鳥類148種、哺乳類32種、兩棲類25種、爬行類24種、蝶類155種和蜻蛉類72種)，其中含特有種85種，各級保育類43種。
          </p>
        </div>
      </div>
    </div>
    <div class="swiper-button-next"></div>
    <div class="swiper-button-prev"></div>
    <div class="swiper-pagination"></div>
  </div>
  <!-- /.Swiper -->
  <!-- section01 這裡用vue-->
  <section class="bg-light mt-7 py-4" id="app01">
    <div class="container">
      <h2 class="h2 text-primary text-center fw-bold">生態資料庫數據</h2>
      <h5 class="h5 text-gray-600 text-center latoEng fw-bold">Ecology Database Record</h5>
      <div class="row p-4 gx-5 gy-2">
        <div class="col-12 col-md-4">
          <div class="p-3 pb-0 bg-gradient-green">
            <p class="text-white h5">生態調查資料</p>
            <p class="text-end text-white">截至<asp:Literal ID="ltl_month1" runat="server"></asp:Literal>月新增<span class="latoEng textNumber text-shadow-xs"><asp:Literal ID="ltl_occurence" runat="server"></asp:Literal></span>筆</p>
          </div>
        </div>
        <div class="col-12 col-md-4">
          <div class="p-3 pb-0 bg-gradient-orange">
            <p class="text-white h5">道路致死資料</p>
            <p class="text-end text-white">截至<asp:Literal ID="ltl_month2" runat="server"></asp:Literal>月新增<span class="latoEng textNumber text-shadow-xs"><asp:Literal ID="ltl_roadkill" runat="server"></asp:Literal></span>筆</p>
          </div>
        </div>
        <div class="col-12 col-md-4">
          <div class="p-3 pb-0 bg-gradient-blue">
            <p class="text-white h5">現況植栽資料</p>
            <p class="text-end text-white">截至<asp:Literal ID="ltl_month3" runat="server"></asp:Literal>月新增<span class="latoEng textNumber text-shadow-xs"><asp:Literal ID="ltl_plants" runat="server"></asp:Literal></span>筆</p>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- /.section01 -->
  <!-- section02 -->
  <section class="mt-7 py-4">
    <div class="container">
      <h2 class="h2 text-primary text-center fw-bold">國道道路致死動物各類群種類數與數量比例
      </h2>
      <h5 class="h5 text-gray-600 text-center latoEng fw-bold">Freeway Rollkill Statistic Pie Chart</h5>
      <div class="row p-4 gx-5 gy-2">
        <div class="col-12 col-md-4 ">
          <div class="p-3 border bg-light mt-md-6">
            <div class="text-center border-bottom pb-3">
              <h5 class="h5">累積動物路殺數量</h5>
              <p><span class="latoEng h1 fw-bold"><asp:Literal ID="ltl_roadkill_total" runat="server"></asp:Literal></span>隻</p>
            </div>
            <ul class="mt-4">
              <li class="d-flex justify-content-between align-items-baseline">
                <p class="d-inline-block">貓狗</p>
                <p class="d-inline-block"><span class="latoEng h3 fw-bold"><asp:Literal ID="ltl_roadkill_num1" runat="server"></asp:Literal></span>隻</p>
              </li>
              <li class="d-flex justify-content-between align-items-baseline">
                <p class="d-inline-block">大鳥</p>
                <p class="d-inline-block"><span class="latoEng h3 fw-bold"><asp:Literal ID="ltl_roadkill_num2" runat="server"></asp:Literal></span>隻</p>
              </li>
              <li class="d-flex justify-content-between align-items-baseline">
                <p class="d-inline-block">中小鳥</p>
                <p class="d-inline-block"><span class="latoEng h3 fw-bold"><asp:Literal ID="ltl_roadkill_num3" runat="server"></asp:Literal></span>隻</p>
              </li>
              <li class="d-flex justify-content-between align-items-baseline">
                <p class="d-inline-block">果子狸</p>
                <p class="d-inline-block"><span class="latoEng h3 fw-bold"><asp:Literal ID="ltl_roadkill_num4" runat="server"></asp:Literal></span>隻</p>
              </li>
              <li class="d-flex justify-content-between align-items-baseline">
                <p class="d-inline-block">其他</p>
                <p class="d-inline-block"><span class="latoEng h3 fw-bold"><asp:Literal ID="ltl_roadkill_num5" runat="server"></asp:Literal></span>隻</p>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-12 col-md-8">
          <div class="p-3">
            <div class="chart-container" style="height:40vh; width:50vw">
              <canvas id="pieChart" width="400" height="300"></canvas>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- /.section02 -->
  <!-- section03 -->
  <section class="mt-7 py-4">
    <div class="container">
      <h2 class="h2 text-primary text-center fw-bold">北、中、南分局轄區全物種路殺數量歷年變化</h2>
      <h5 class="h5 text-gray-600 text-center latoEng fw-bold">Freeway Branch Rollkill Amount Bar Chart</h5>
    </div>
    <!-- style="height:40vh; width:80vw" -->
    <div class="row p-4">
      <div class="col">
        <div class="chart-container" style="height:40vh;">
          <canvas id="barChart"></canvas>
        </div>
      </div>
    </div>
  </section>
  <!-- /.section03 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
  <!-- Swiper JS -->
  <script src="https://unpkg.com/swiper/swiper-bundle.min.js"></script>
  <!-- Initialize Swiper -->
  <script>
    // swiper js
    var swiper = new Swiper(".mySwiper", {
      slidesPerView: 1,
      spaceBetween: 30,
      loop: true,
      pagination: {
        el: ".swiper-pagination",
        clickable: true,
      },
      navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
      },
    });

    // chart js
    // 第一張圖-圓餅圖
    const ctx1 = document.getElementById('pieChart').getContext("2d");
    const elPieChart = new Chart(ctx1, {
      type: 'pie',
      data: {
        labels: ['貓狗', '大鳥', '中小鳥', '果子狸', '其他'],
        datasets: [{
          label: '# of Votes',
          data: [<asp:Literal ID="ltl_roadkill_nums" runat="server"></asp:Literal>],
          backgroundColor: [
            'rgba(255, 99, 132, 0.5)',
            'rgba(54, 162, 235, 0.5)',
            'rgba(255, 206, 86, 0.5)',
            'rgba(75, 192, 192, 0.5)',
            'rgba(153, 102, 255, 0.5)'
          ]
        }]
      },
      options: {
        maintainAspectRatio: false, //此行如果設定圖表寬高要加入，否則設定會出錯。
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
    // 第二張圖-長條bar圖
    // https://codepen.io/elisescolaro/pen/YaGyMW
    // https://github.com/reactchartjs/react-chartjs-2/issues/388
    var ctx2 = document.getElementById("barChart").getContext('2d');
    var elBarChart = new Chart(ctx2, {
      type: 'bar',
      data: {
        labels: [<asp:Literal ID="ltl_year_labels" runat="server"></asp:Literal>],
        datasets: [{
          label: '北分局',
          backgroundColor: "#caf270",
          data: [<asp:Literal ID="ltl_north_kills" runat="server"></asp:Literal>],
        }, {
          label: '中分局',
          backgroundColor: "#45c490",
          data: [<asp:Literal ID="ltl_center_kills" runat="server"></asp:Literal>],
        }, {
          label: '南分局',
          backgroundColor: "#008d93",
          data: [<asp:Literal ID="ltl_south_kills" runat="server"></asp:Literal>],
        }],
      },
      options: {
        tooltips: {
          displayColors: true,
          callbacks: {
            mode: 'x',
          },
        },
        scales: {
          xAxes: [{
            stacked: true,
            gridLines: {
              display: false,
            }
          }],
          yAxes: [{
            stacked: true,
            ticks: {
              beginAtZero: true,
            },
            type: 'linear',
          }]
        },

        // scales: {
        //     x: {
        //         stacked: true
        //     },
        //     y: {
        //         stacked: true
        //     }
        // },

        legend: {
          position: 'bottom'
        },
        responsive: true,
        maintainAspectRatio: false,
      }
    });
  

    // jQ範圍元件
    $(function () {
      $("#slider-range").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
          $("#amount").val(ui.values[0] + " - " + ui.values[1]);
        }
      });
      $("#amount").val($("#slider-range").slider("values", 0) +
        " - " + $("#slider-range").slider("values", 1));
    });
    $(function () {
      $("#slider-range2").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
          $("#amount2").val(ui.values[0] + " - " + ui.values[1]);
        }
      });
      $("#amount2").val($("#slider-range2").slider("values", 0) +
        " - " + $("#slider-range2").slider("values", 1));
    });
    // jQ-tab切換
    $(".tab").each(function (index) {
      $(this).click(function (e) {
        triggletab();
        triigletabcontent();
        $(this).toggleClass("active");
        $(".tab-c")
          .eq(index)
          .toggleClass("active");
      });
    });
    //to remove all tab headers
    function triggletab() {
      $(".tab").each(function () {
        $(this).removeClass("active");
      });
    }

    //triggle the tab content
    function triigletabcontent() {
      $(".tab-c").each(function () {
        $(this).removeClass("active");
      });
    }

  </script>
</asp:Content>
