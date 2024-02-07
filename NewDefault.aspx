<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewDefault.aspx.cs" Inherits="NewDefault" %>

<!DOCTYPE html>
<html lang="">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, shrink-to-fit=no">
    <title>國道生態資料庫</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.min.css">
    <!-- OwlCarousel輪播 -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/OwlCarousel2/2.3.4/assets/owl.carousel.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/OwlCarousel2/2.3.4/assets/owl.theme.default.min.css">
    <!-- hover動畫 -->
    <link rel="stylesheet" href="css/hover-min.css">
    <!-- 自訂義CSS -->
    <link rel="stylesheet" href="css/layout.css">
    <!--[if lt IE 9]>
			<script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.min.js"></script>
			<script src="https://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js"></script>
		<![endif]-->
    <style type="text/css">
        .type1 {
            line-height: normal;
            margin-left: 5px;
            margin-right: 5px;
        }

        .formmodel {
            color: #f5f5f5;
            height: 26px;
            background-color: rgba(255,255,255,0.7);
            border: 1px solid #fff;
        }
    </style>
</head>
<body>

    <div class="AllContent">

        <!-- SEARCH_start -->

        <div class="HW_Header">
            <div class="HW_Header_Btn">
		<div class="BannerBtn hvr-sweep-to-right"><a href="index.aspx">新版首頁</a></div>
                <a href='#modal-id' data-toggle="modal" class="SearchIcon">
                    <img src="images/magnifying-glass-search.png"></a>
            </div>
            <div class="LoginWords">
                <span>
                    <asp:Label ID="labuser" runat="server"></asp:Label></span>
                <ul>
                    <li style="display: inline; float: left"><a href="newdefault.aspx"><font color="black">首頁</font></a></li>
                    <li style="display: inline; float: left">|</li>
                    <li style="display: inline; float: left">
                        <form id="logoutForm" action="Account/Logout2" method="post" style="display: inline; float: left">
                            <a href="javascript:document.getElementById('logoutForm').submit()"><font color="black">登出</font></a>
                        </form>
                    </li>
                </ul>
            </div>
        </div>
        <scri
            function logout() {
                //window.location = "/freeway2(old)/account/logout2.cshtml";
            }
        </scri>
        <form runat="server">
            <div class="modal fade" id="modal-id">
                <div class="modal-dialog modal-lg">
                    <div>
                        <div class="modal-header">
                            <asp:Button runat="server" ID="searchdata" class="btn-default" Style="margin-left: 10px; margin-top: 3px; width: 50px; height: 35px" Text="搜尋" OnClick="searchdata_Click" />
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="input-group">
                                <asp:TextBox runat="server" CssClass="form-control" ID="searchtxt"></asp:TextBox>
                                <%--<input type="text" class="form-control" placeholder="Search..." aria-label="Search">--%>
                            </div>
                            <div >
                                <ul style="font-size:20px">
                                    <li style="display:inline;padding:5px">
                                        <asp:CheckBox ID="s1" runat="server" Text="生態調查" Style="color: white" /></li>
                                    <li style="display:inline;padding:5px">
                                        <asp:CheckBox ID="s2" runat="server" Text="道路致死" Style="color: white" /></li>
                                    <li style="display:inline;padding:5px">
                                        <asp:CheckBox ID="s3" runat="server" Text="植栽工程" Style="color: white" /></li>
                                    <li><br /></li>
                                    <li style="color: white">請選擇查詢國道：&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList runat="server" ID="highwayid" Style="color: black; height: 26px">
                                            <asp:ListItem Value="1">國道1號</asp:ListItem>
                                            <asp:ListItem Value="2">國道2號</asp:ListItem>
                                            <asp:ListItem Value="3">國道3號</asp:ListItem>
                                            <asp:ListItem Value="3甲">國道3甲</asp:ListItem>
                                            <asp:ListItem Value="4">國道4號</asp:ListItem>
                                            <asp:ListItem Value="5">國道5號</asp:ListItem>
                                            <asp:ListItem Value="6">國道6號</asp:ListItem>
                                            <asp:ListItem Value="7">國道7號</asp:ListItem>
                                            <asp:ListItem Value="8">國道8號</asp:ListItem>
                                            <asp:ListItem Value="10">國道10號</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li style="width: 200px" class="type1"></li>
                                    <li style="color: white" class="type1">起始里程﹝公里﹞：<asp:TextBox runat="server" ID="start1" Style="color: black; height: 26px;width:70px"></asp:TextBox>K+<asp:TextBox runat="server" ID="start2" Style="color: black; height: 26px;width:70px"></asp:TextBox></li>
                                    <li style="width: 200px" class="type1"></li>
                                    <li style="color: white" class="type1">結束里程﹝公里﹞：<asp:TextBox runat="server" ID="end1" Style="color: black; height: 26px;width:70px"></asp:TextBox>K+<asp:TextBox runat="server" ID="end2" Style="color: black; height: 26px;width:70px"></asp:TextBox></li>
                                    <li style="width: 200px" class="type1"></li>
                                    <li style="color: white" class="type1">資料期間：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="year1" Style="color: black; height: 26px;width:50px"></asp:TextBox>年﹝西元﹞
                                     <asp:DropDownList runat="server" ID="month1" Style="color: black; height: 26px">
                                         <asp:ListItem Value=""></asp:ListItem>
                                         <asp:ListItem Value="01">01</asp:ListItem>
                                         <asp:ListItem Value="02">02</asp:ListItem>
                                         <asp:ListItem Value="03">03</asp:ListItem>
                                         <asp:ListItem Value="04">04</asp:ListItem>
                                         <asp:ListItem Value="05">05</asp:ListItem>
                                         <asp:ListItem Value="06">06</asp:ListItem>
                                         <asp:ListItem Value="07">07</asp:ListItem>
                                         <asp:ListItem Value="08">08</asp:ListItem>
                                         <asp:ListItem Value="09">09</asp:ListItem>
                                         <asp:ListItem Value="10">10</asp:ListItem>
                                         <asp:ListItem Value="11">11</asp:ListItem>
                                         <asp:ListItem Value="12">12</asp:ListItem>
                                     </asp:DropDownList>
                                        月至<asp:TextBox runat="server" ID="year2" Style="color: black; height: 26px;width:50px"></asp:TextBox>年﹝西元﹞
                                    <asp:DropDownList runat="server" ID="month2" Style="color: black; height: 26px">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="01">01</asp:ListItem>
                                        <asp:ListItem Value="02">02</asp:ListItem>
                                        <asp:ListItem Value="03">03</asp:ListItem>
                                        <asp:ListItem Value="04">04</asp:ListItem>
                                        <asp:ListItem Value="05">05</asp:ListItem>
                                        <asp:ListItem Value="06">06</asp:ListItem>
                                        <asp:ListItem Value="07">07</asp:ListItem>
                                        <asp:ListItem Value="08">08</asp:ListItem>
                                        <asp:ListItem Value="09">09</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                    </asp:DropDownList>月
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <!-- SEARCH_end -->

        <!-- BANNER_start -->
        <div class="TitleBanner">
            <span class="ImgCover">
                <div class="CoverContent">
                    <div class="CoverContentCenter">
                        <h1>國道生態資料庫系統</h1>
                        <p>
                            為瞭解國道設施與景觀綠化對沿線生態環境之影響，展開沿線沿線動植物資源調查，與建立道路致死資料蒐集機制，作為環境復育背景資料，及快速掌握國道生態資源豐富，以供研擬永續利用發展策略，使道路建設與環境更為融合。
                        </p>
                        <div class="BannerBtn hvr-sweep-to-right"><a href="#">資料搜尋</a></div>
                    </div>
                </div>
            </span>
            <img src="images/titlebanner.jpg">
        </div>
        <!-- BANNER_end -->



        <!-- MENU_end -->

        <div class="MenuContent">
            <ul>
                <li id="MENU1"><a href="#" class="ListChange">單筆修改</a>
                    <ul id="SUB1">
                        <li class="SUBList"><a href="Control/Occurrence_edit2_.aspx">生態調查資料</a></li>
                        <li class="SUBList"><a href="Control/Roadkill_edit1_.aspx">道路致死資料</a></li>
                        <li class="SUBList"><a href="Control/Plant_engineering_edit1_.aspx">植栽工程資料</a></li>
                        <!-- li class="SUBList"><a href="Control/Plant_observation_edit5_.aspx">植栽工程物種</a></li -->
                        <!-- li class="SUBList"><a href="Control/Environment_edit1_.aspx">環評調查報告</a></li -->
                        <li class="SUBList"><a href="Control/Eco_report_edit1_.aspx">生態相關文件</a></li>
                        <li class="SUBList"><a href="Control/Road_status_edit1_.aspx">路權空間資料</a></li>
                        <!-- li class="SUBList"><a href="Control/Environment_Restore_edit1_.aspx">生態相關資料</a></li -->
                        <li class="SUBList"><a href="Control/Plants_edit1_.aspx">現況植栽調查</a></li>
                    </ul>
                </li>

                <li id="MENU2"><a href="#" class="ListIn">單筆新增</a>
                    <ul id="SUB2">
                        <li class="SUBList"><a href="Control/Site_add1_.aspx">生態調查資料</a></li>
                        <li class="SUBList"><a href="Control/Roadkill_add1_.aspx">道路致死資料</a></li>
                        <li class="SUBList"><a href="Control/Plant_engineering_add1_.aspx">植栽工程資料</a></li>
                        <!-- li class="SUBList"><a href="Control/Environment_add1_.aspx">環評調查報告</a></li -->
                        <li class="SUBList"><a href="Control/Eco_report_add1_.aspx">生態調查報告</a></li>
                        <li class="SUBList"><a href="Control/Road_status_add1_.aspx">路權空間資料</a></li>
                        <!-- li class="SUBList"><a href="Control/Environment_Restore_add1_.aspx">生態相關資料</a></li -->
                    </ul>
                </li>

                <li id="MENU3"><a href="#" class="UpLoad">批次上傳</a>
                    <ul id="SUB3">
                        <li class="SUBList"><a href="Control/Batch_Upload_Site_.aspx">生態調查資料</a></li>
                        <li class="SUBList"><a href="Control/Batch_Upload_Roadkill_.aspx">道路致死資料</a></li>
                        <li class="SUBList"><a href="Control/Batch_Upload_Plants_.aspx">現況植栽調查</a></li>
                    </ul>
                </li>

                <!-- li id="MENU4"><a href="View/fun12_01.aspx" class="DownLoad">文件下載</a>
                    <ul id="SUB4">
                        <li class="SUBList"><a href="#">請務必檢查車輛及輪胎</a></li>
                    </ul>
                </li -->

                <li id="MENU5"><a href="#" class="UserSystem">系統管理</a>
                    <ul id="SUB5">
                        <li class="SUBList"><a href="Control/Userdata_.aspx">系統帳號維護</a></li>
                        <li class="SUBList"><a href="Control/Roadkill_Export_.aspx">道路致死匯出</a></li>
                        <li class="SUBList"><a href="View/fun07_04_.aspx">上傳道路致死統計</a></li>
                        <li class="SUBList"><a href="View/fun12_01.aspx">文件下載</a></li>
                    </ul>
                </li>

                <li id="MENU6"><a href="Control/Roadkill_bulletin_add1_.aspx" class="ACPhone">受傷動物通報</a>
                    <%--  <ul id="SUB6">
                        <li class="SUBList"><a href="#">請務必檢查車輛及輪胎</a></li>
                    </ul>--%>
                </li>
            </ul>
        </div>

        <!-- MENU_end -->


        <!-- 輪播_start -->

        <div class="owl-carousel owl-theme Owl_Margin">
            <div class="item">
                <div class="ListItemText">
                    <h4>通霄一號跨越橋多功能動物通道</h4>
                    <p>
                        全長達144公尺的通霄一號跨越橋是國道上很有名的一座π型橋，橋梁所在路段兩側森林環境有包括石虎在內的豐富哺乳動物，且這座橋梁的車流量少、人為干擾程度低，7.5公尺寬的路面亦足夠分享給動物使用。在考量排水和荷重問題後，大甲工務段將橋面分出三分之一的寬度，設置土袋並進行植栽以營造可供動物通行區域，再配合水溝動物逃生坡道、解說牌、告示牌、監視器、水塔和滴灌等設施，完成了改善的試驗工程。                   
                    </p>
                </div>
                <div class="ListItemImg" width="">
                    <img src="Images/newdefault1.jpg" />
                </div>
            </div>

            <div class="item">
                <div class="ListItemText">
                    <h4>生態友善國道</h4>
                    <ul>
                        <li>起始計畫 97-100年<br />營運階段國道永續發展環境復育改善研究計畫</li>
                         <li>延續計畫 101-103年<br />國道沿線生態課題調查與友善措施評估計畫</li>
                         <li>淺山一期 103-105年<br />國道生態資源調查暨淺山環境復育研究計畫</li>
                         <li>淺山二期 106-108年<br />國道生態資源調查暨淺山環境復育研究計畫(第2期)</li>
                    </ul>
                </div>
                <div class="ListItemImg" width="474px" height="350px">
                    <img src="Images/photo2.jpg"  />
                </div>
            </div>

            <div class="item">
                <div class="ListItemText">
                    <h4>如何獲得政府資料開放平臺的資料集清單</h4>
                    <p>
                        資料集清單為集中列示於政府資料開放平台資料集之詮釋資料，主要欄位包含id、資料集名稱、檔案格式、下載連結、資料集類型、資料集描述、主要欄位說明、提供機關、更新頻率、授權方式、計費方式、編碼格式、資料集提供機關聯絡人、資料集提供機關聯絡人電話、備註等，並於每日更新一次。取得各資料集詮釋資料清單之方式如下
                    </p>
                </div>
                <div class="ListItemImg">
                    <img src="images/listitem01.jpg">
                </div>
            </div>
        </div>

        <!-- 輪播_end -->


        <!-- ICONS切換_start -->

        <div class="IfThis">
            <div class="IfThis_Left">
                <ul>
                    <li name="SS1" id="SS1">
                        <a href="View/fun01_011_.aspx">
                            <img src="images/penicons.png"><span>生態調查資料</span></a>
                    </li>
                    <li name="SS2" id="SS2">
                        <a href="View/fun02_011a_.aspx">
                            <img src="images/dangersing.png"><span>道路致死資料</span></a>
                    </li>
                    <li name="SS5" id="SS5">
                        <a href="View/fun03_011a_.aspx">
                            <img src="images/plant.png"><span>現況植栽調查</span></a>
                    </li>
                    <li name="SS4" id="SS4">
                        <a href="View/fun07_01_.aspx">
                            <img src="images/profits.png"><span>路殺統計資料</span></a>
                    </li>
                    <li name="SS3" id="SS3">
                        <a href="trans_">
                            <img src="images/compass.png"><span>里程座標轉換</span></a>
                    </li>
                    <li name="SS6" id="SS6">
                        <a href="View/fun11_01_.aspx">
                            <img src="images/phoneicons.png"><span>受傷動物通報</span></a>
                    </li>
                </ul>
            </div>

            <div class="IfThis_right">
                <div class="IfThisContent" name="PP1" id="PP1">
                    <h4>生態調查資料</h4>
                    <p>
                        國道沿線生態調查資料，於高速公路路權外1公里範圍內，針對可及之次生林林下環境及自然度較高的重要生物棲地，進行沿線生物調查
                    </p>
                    <p>
                        生態資料透過系統性資料蒐集，於系統中可針對物種名稱、國道別、國道里程等條件進行資料檢索。
                    </p>
                </div>
                <div class="IfThisContent" name="PP2" id="PP2">
                    <h4>道路致死資料</h4>
                    <p>
                        野生動物車禍，又稱路殺或道路致死，以標準方法進行系統性的路殺資料收集，為減輕路殺事件的第一步。後續再配合分析路殺事件的物種、熱點和時空變化，以進行改善成效評估。
                    </p>
                    <p>
                        於系統中可針對路殺物種(物種名稱與類群)、國道別、國道里程等條件進行資料檢索，除單筆資料呈現外，同時也提供路殺熱點視覺呈現。
                    </p>
                </div>
                <div class="IfThisContent" name="PP3" id="PP3">
                    <h4>現況植栽調查</h4>
                    <p>
                        提供現況植栽查詢，提供物種名稱、花期、葉色轉變期等條件進行查詢。
                   
                    </p>
                </div>
                <div class="IfThisContent" name="PP4" id="PP4">
                    <h4>道路致死統計</h4>
                    <p>
                        統計各工務段上傳數量
                    </p>
                    <p>
                        統計各種關注物種之路殺數量
                    </p>
                </div>
                <div class="IfThisContent" name="PP5" id="PP5">
                    <h4>里程座標轉換 </h4>
                    <p>
                        由各國道方向性與里程，轉換至WGS 84座標資訊
                    </p>
                </div>
                <div class="IfThisContent" name="PP6" id="PP6">
                    <h4>受傷動物通報查詢</h4>
                    <p>
                    </p>
                </div>
            </div>
        </div>

        <!-- ICONS切換_end -->


        <!-- footer_start -->

        <div class="Footer">
            <div class="FooterText">
                <span>總機：(02)2909-6141 (02)2909-6141 地址：24303新北市泰山區黎明里半山雅70號<br>
                    交通部高速公路局<br>
                    版權所有 最佳瀏覽環境：IE 6.0、FireFox 2.0 以上版本．本網站最佳瀏覽解析度為 1024*768
                </span>
            </div>
        </div>

        <!-- footer_end -->

    </div>




    <script src="https://code.jquery.com/jquery.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="js/setBstModalMaxHeight.js"></script>
    <!-- OwlCarousel輪播 -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/OwlCarousel2/2.3.4/owl.carousel.min.js"></script>
    <script>
        $('.owl-carousel').owlCarousel({
            loop: true,
            // margin:0,
            nav: true,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        })
    </script>
    <!-- 自訂義JQ -->
    <script src="js/layout.js"></script>

</body>
</html>
