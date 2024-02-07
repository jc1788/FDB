<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fun07_03_.aspx.cs" Inherits="View_fun07_03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

<script src="../Scripts/journals/jquery.min.js"></script>

<script src="../Scripts/journals/scripts.js"></script>

<style type="text/css">
body{font-family: Arial, sans-serif;font-size:10px;}
.axis path,.axis line {fill: none;stroke:#b6b6b6;shape-rendering: crispEdges;}
/*.tick line{fill:none;stroke:none;}*/
.tick text{fill:#999;}
g.journal.active{cursor:pointer;}
text.label{font-size:12px;font-weight:bold;cursor:pointer;}
text.value{font-size:12px;font-weight:bold;}
</style>

<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 00px;
	background-image: url();
	background-repeat: repeat;
	background-color: #F7F7F7;
}
-->

.PagerCss TD A:hover { FONT-SIZE: 20px;WIDTH: 20px; COLOR: black; padding-left: 4px; padding-right:4px; }
.PagerCss TD A:active { FONT-SIZE: 20px; WIDTH: 20px; COLOR: black; padding-left: 4px; padding-right:4px; }
.PagerCss TD A:link { FONT-SIZE: 20px; WIDTH: 20px; COLOR: black; padding-left: 4px; padding-right:4px; }
.PagerCss TD A:visited { FONT-SIZE: 20px; WIDTH: 20px; COLOR: black;padding-left: 4px; padding-right:4px; }
.PagerCss TD SPAN { FONT-WEIGHT: bold; FONT-SIZE: 24px; WIDTH: 20px; COLOR: red; padding-left: 4px; padding-right:4px;} 
</style>

<link href="../Content/style1.css" rel="stylesheet" type="text/css" />
     <link href="../Control/fun01_011.aspx_files/style1.css" rel="stylesheet" />
    <script src="../Control/fun01_011.aspx_files/WebResource.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
        <asp:TableRow>
            <asp:TableCell>
                <a href="../newdefault.aspx" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/control/fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
                </a>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7">
                <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                     <asp:TableRow HorizontalAlign="Right">
                         <asp:TableCell>
                             <div class="menu4">
                                 <a href="../newdefault.aspx" class="menu4">首頁</a> &gt;  
                                 <a href="fun07_01_.aspx" class="menu4">統計資料概要</a>
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">
                            <asp:Table ID="Table_Function" runat="server" Width="640" HorizontalAlign="Center" >
                                            
                                <asp:TableRow>
                                    <asp:TableCell Width="17">
                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="150" CSSClass="title12">
                                        統計資料概要
                                    </asp:TableCell>
                                    <asp:TableCell Width="700" CSSClass="menu12a">
                                        <span class="">
                                            <a href="fun07_01_.aspx" class="menu12">統計資料概要</a>
                                        </span> | 
                                        <span class="menu12">
                                            <a href="fun07_03_.aspx" class="menu13">統計資料綱要</a>
                                        </span>
                                    </asp:TableCell>

                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell Width="17">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/frame/10.jpg" Width="15" Height="32" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="150" CSSClass="title12">
                                        統計資料圖表
                                    </asp:TableCell>
                                    <asp:TableCell Width="700" CSSClass="menu13a">
                                        <span class="menu12">
                                            <a href="fun07_02_.aspx" class="menu12">工務段道路致死數量比較</a>
                                        </span> | 
                                        
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow ID="Row_GridView">
            <asp:TableCell VerticalAlign="Top" >
                <asp:Table ID="Table3" runat="server" Width="1000" >
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                            <asp:Table ID="Table4" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="800" >

                                        請選擇物種類群 : <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="title" DataValueField="title" ></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select title From (SELECT 'ALL' title UNION ALL Select Distinct Animal From Roadkill_New) tmp Order By title "></asp:SqlDataSource>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData"/>
                                        
                                    </asp:TableCell>
                                </asp:TableRow> 

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" BackColor="#666666">
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                         <script type="text/javascript">
                                             function truncate(str, maxLength, suffix) {
                                                 if (str.length > maxLength) {
                                                     str = str.substring(0, maxLength + 1);
                                                     str = str.substring(0, Math.min(str.length, str.lastIndexOf(" ")));
                                                     str = str + suffix;
                                                 }
                                                 return str;
                                             }

                                             /*
                                             var url = window.location.toString();
                                             var accept_name_code = "";
                                             var wd = "";
                                             if (url.indexOf("?") != -1) { //url裡有"?"符號
                                                 var ary = url.split("?")[1].split("&");
                                                 for (var i in ary) {
                                                     accept_name_code = ary[i].split("=")[1] + "\n"; //變數值
                                                     if (i == 1) {
                                                         wd = ary[i].split("=")[1];
                                                     }
                                                 }
                                             }
                                             */

                                             var margin = { top: 20, right: 200, bottom: 0, left: 200 },
                                                 width = 600,
                                                 height = 800;

                                             var start_year = 2008,
                                                 end_year = 2016;

                                             var c = d3.scale.category20c();

                                             var x = d3.scale.linear()
                                                 .range([0, width]);

                                             var xAxis = d3.svg.axis()
                                                 .scale(x)
                                                 .orient("top");

                                             var formatYears = d3.format("0000");
                                             xAxis.tickFormat(formatYears);

                                             var svg = d3.select("body").append("svg")
                                                 .attr("width", width + margin.left + margin.right)
                                                 .attr("height", height + margin.top + margin.bottom)
                                                 .style("margin-left", margin.left + "px")
                                                 .append("g")
                                                 .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

                                                //var json_url = "http://140.109.28.78:8081/journals_optogenetic.json.php?accept_name_code=" + accept_name_code;
                                                //var json_url = "http://140.109.28.78:8081/journals_optogenetic.json.php?accept_name_code=203271";

                                                //d3.json(json_url , function (data) {
                                                //var json_url = '[{"articles": [[1980,46],[1985,0],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],"total":46,"name":"1979－2003年間之植群調查資料"},{"articles": [[1980,0],[1985,0],[1990,0],[1995,0],[2000,3],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],"total":3,"name":"中央研究院植物標本館標本資料庫"},{"articles": [[1980,0],[1985,0],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,106],[2008,2],[2010,0],[2012,0]],"total":108,"name":"國家植群多樣性調查及製圖計畫"},{"articles": [[1980,0],[1985,0],[1990,0],[1995,0],[2000,1],[2002,5],[2004,36],[2006,0],[2008,0],[2010,0],[2012,0]],"total":42,"name":"林務局生物資源資料庫"},{"articles": [[1980,54],[1985,3],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],"total":57,"name":"林試所植物標本館資料集"},{"articles": [[1980,16],[1985,0],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],"total":16,"name":"森林永久樣區調查工作"},{"articles": [[1980,0],[1985,0],[1990,0],[1995,2],[2000,2],[2002,26],[2004,22],[2006,0],[2008,0],[2010,0],[2012,0]],"total":52,"name":"特生中植物資料庫"},{"articles": [[1980,0],[1985,19],[1990,0],[1995,0],[2000,0],[2002,0],[2004,0],[2006,0],[2008,0],[2010,0],[2012,0]],"total":19,"name":"臺大植物標本館資料集"}]';

                                                 d3.json("../Scripts/journals/data.json", function (data) {
                                            
                                                 //d3.json(jsondata , function (data) {


                                                 x.domain([start_year, end_year]);
                                                 var xScale = d3.scale.linear()
                                                     .domain([start_year, end_year])
                                                     .range([0, width]);

                                                 svg.append("g")
                                                     .attr("class", "x axis")
                                                     .attr("transform", "translate(0," + 0 + ")")
                                                     .call(xAxis);

                                                 for (var j = 0; j < data.length; j++) {
                                                     var g = svg.append("g").attr("class", "journal");

                                                     var circles = g.selectAll("circle")
                                                         .data(data[j]['articles'])
                                                         .enter()
                                                         .append("circle");

                                                     var text = g.selectAll("text")
                                                         .data(data[j]['articles'])
                                                         .enter()
                                                         .append("text");

                                                     var rScale = d3.scale.linear()
                                                         .domain([0, d3.max(data[j]['articles'], function (d) { return d[1]; })])
                                                         .range([0, 6]);

                                                     circles
                                                         .attr("cx", function (d, i) { return xScale(d[0]); })
                                                         .attr("cy", j * 20 + 20)
                                                         .attr("r", function (d) { return rScale(d[1]); })
                                                         .style("fill", function (d) { return c(j); });

                                                     text
                                                         .attr("y", j * 20 + 25)
                                                         .attr("x", function (d, i) { return xScale(d[0]) - 5; })
                                                         .attr("class", "value")
                                                         .text(function (d) { return d[1]; })
                                                         .style("fill", function (d) { return c(j); })
                                                         .style("display", "none");

                                                     g.append("text")
                                                         .attr("y", j * 20 + 25)
                                                         .attr("x", width + 20)
                                                         .attr("class", "label")
                                                         .text(truncate(data[j]['name'], 30, "..."))
                                                         .style("fill", function (d) { return c(j); })
                                                         .on("mouseover", mouseover)
                                                         .on("mouseout", mouseout);
                                                 };

                                                 function mouseover(p) {
                                                     var g = d3.select(this).node().parentNode;
                                                     d3.select(g).selectAll("circle").style("display", "none");
                                                     d3.select(g).selectAll("text.value").style("display", "block");
                                                 }

                                                 function mouseout(p) {
                                                     var g = d3.select(this).node().parentNode;
                                                     d3.select(g).selectAll("circle").style("display", "block");
                                                     d3.select(g).selectAll("text.value").style("display", "none");
                                                 }
                                             });

                                        </script>
                                        

                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

        
    </asp:Table>

    </form>
</body>

   
</html>
