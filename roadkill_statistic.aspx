<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="roadkill_statistic.aspx.cs" Inherits="roadkill_statistic" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
      <script scr="https://cdn.anychart.com/releases/v8/js/anychart-base.min.js"></script>
      <script src="https://cdn.anychart.com/releases/v8/js/anychart-bundle.min.js"></script>
      <script scr="https://cdn.anychart.com/releases/v8/js/anychart-core.min.js"></script>
      <script src="https://cdn.anychart.com/releases/v8/js/anychart-treemap.min.js"></script>
      <script>
	function check_highway()
	{
	    if ($('#<%=highwayida.ClientID%>').val() == "0")
		return confirm('您選擇要一次查詢所有國道，建議加上其他條件以免查詢時間過長。\n\r確定繼續查詢，或取消增加其他查詢條件');
	    else
		return true;
	}
      </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">道路致死資料</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">道路致死資料</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <!-- /.標題+麵包屑 -->
    <!-- 主要內容區塊 -->
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link <%= Session["roadkill_statistic"] == null || Session["roadkill_statistic"].ToString() == "0" ? "active" : "" %>" id="link01-tab" data-toggle="tab" href="#link01" role="tab"
                        aria-controls="link01" aria-selected="true">資料統計</a>
                    <a class="nav-item nav-link <%= Session["roadkill_statistic"] != null && Session["roadkill_statistic"].ToString() == "1" ? "active" : "" %>" id="link02-tab" data-toggle="tab" href="#link02" role="tab"
                        aria-controls="link02" aria-selected="true">資料管理</a>
                </div>
            </nav>
        </div>
        <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade <%= Session["roadkill_statistic"] == null || Session["roadkill_statistic"].ToString() == "0" ? "show mt-4 active" : "" %>" id="link01" role="tabpanel" aria-labelledby="link01-tab">
                <ul class="nav nav-tabs tabs-pages" id="myTab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="tab active" id="home-tab" data-bs-toggle="tab" data-bs-target="#home"
                            type="button" role="tab" aria-controls="home" aria-selected="true">統計條件</button>
                    </li>
                    <!-- <li class="nav-item" role="presentation">
                      <button class="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#profile" type="button" role="tab" aria-controls="profile" aria-selected="false">Profile</button>
                    </li> -->
                </ul>
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                        <div class="container bg-light -mt-8 pb-2">
                            <div class="row g-2 my-2">
                                <div class="col-12 col-md-4">
                                    <div class="form-floating">
					<asp:DropDownList ID="Select_Year" runat="server" DataSourceID="SqlDataSource1" DataTextField="Select_Year_Show" DataValueField="Select_Year"
					Cssclass="form-select" aria-label="Floating label select example">
					</asp:DropDownList>
					<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select 0 AS Select_Year, 'ALL' AS Select_Year_Show UNION Select Distinct Year(Date) AS Select_Year, CONVERT(VARCHAR(10),Year(Date)) AS Select_Year_Show From Roadkill_New where Year(Date) <> '' Order By Select_Year"></asp:SqlDataSource>
					<asp:Label id="lbfor_Select_Year" AssociatedControlId="Select_Year" Text="資料年份" runat="server" />
                                    </div>
                                </div>
                                <div class="col-12 col-md-4">
                                    <div class="form-floating">
                                        <asp:DropDownList ID="Select_Month" runat="server" DataSourceID="SqlDataSource2" DataTextField="Select_Month_Show" DataValueField="Select_Month"
					Cssclass="form-select" aria-label="Floating label select example">
                                        </asp:DropDownList>
					<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select 0 AS Select_Month, 'ALL' AS Select_Month_Show UNION Select Distinct Month(Date) AS Select_Month, CONVERT(VARCHAR(10),Month(Date)) AS Select_Month_Show From Roadkill_New where Month(Date) <> '' Order By Select_Month"></asp:SqlDataSource>
					<asp:Label id="lbfor_Select_Month" AssociatedControlId="Select_Month" Text="資料月份" runat="server" />
                                    </div>
                                </div>
                                <div class="d-grid gap-2 col-12 col-md-2 mx-auto">
                                    <asp:Button ID="btnquery" runat="server" Cssclass="btn btn-primary" Text="查詢" OnClick="btnquery_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <section class="my-4">
                            <h2 class="h2 text-primary text-center fw-bold my-4">常見物種路殺
                            </h2>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">#</th>
                                                    <th scope="col">物種名稱</th>
                                                    <th scope="col">路殺數量</th>
                                                </tr>
                                            </thead>
                                            <tbody>
						<asp:Repeater ID="rptRoadkill_species" runat="server">
						<ItemTemplate>
                                                <tr>
                                                    <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                                    <td><%# Eval("deduce_species") %></td>
                                                    <td><%# Eval("count") %></td>
                                                </tr>
						</ItemTemplate>
						</asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                                    <div id="container" style="height: 430px"></div>
                                </div>
                            </div>
                        </section>
                        <section class="my-4">
                            <h2 class="h2 text-primary text-center fw-bold my-4">關注物種路殺數量
                            </h2>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">#</th>
                                                    <th scope="col">物種名稱</th>
                                                    <th scope="col">路殺數量</th>
                                                </tr>
                                            </thead>
                                            <tbody>
						<asp:Repeater ID="rptRoadkill_focus" runat="server">
						<ItemTemplate>
                                                <tr>
                                                    <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                                    <td><%# Eval("deduce_species") %></td>
                                                    <td><%# Eval("count") %></td>
                                                </tr>
						</ItemTemplate>
						</asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                                    <canvas id="bar-chart-horizontal" height="230"></canvas>
                                </div>
                            </div>
                        </section>
                        <section class="my-4">
                            <h2 class="h2 text-primary text-center fw-bold my-4">關注物種路殺數量年變化
                            </h2>
                            <div class="row g-2">
                                <div class="col-12 col-lg-4 bg-info">
                                    <canvas id="line-chart" width="200" height="150"></canvas>
                                </div>
                                <div class="col-12 col-lg-4 bg-light">
                                    <canvas id="line-chart2" width="200" height="150"></canvas>
                                </div>
                                <div class="col-12 col-lg-4 bg-warning">
                                    <canvas id="line-chart3" width="200" height="150"></canvas>
                                </div>
                            </div>
                        </section>
                        <section class="my-4">
                            <h2 class="h2 text-primary text-center fw-bold my-4">各工務段路殺資料<asp:Literal ID="ltl_roadkill_year_site_months" runat="server"></asp:Literal>年各月上傳狀況
                            </h2>
                            <div class="row">
                                <div class="col-16 col-md-8">
                                    <div class="table-responsive">
					<asp:Literal ID="ltl_sitech_uptable" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="col-8 col-md-4">
				    <canvas id="barChart"></canvas>
                                </div>
                            </div>
                        </section>
                        <section class="my-4">
                            <div class="row">
                                <div class="col-12 col-md-6">
                            <h2 class="h2 text-primary text-center fw-bold my-4"><asp:Literal ID="ltl_roadkill_year_animal2" runat="server"></asp:Literal>道路致死各類群統計
                            </h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
						    <th scope="col">#</th>
                                                    <th scope="col">類群</th>
                                                    <th scope="col">數量</th>
                                                </tr>
                                            </thead>
                                            <tbody>
						<asp:Repeater ID="rptRoadkill_year_animal2" runat="server">
						<ItemTemplate>
                                                <tr>
						    <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                                    <td><%# Eval("animal2") %></td>
                                                    <td><%# Eval("count") %></td>
                                                </tr>
						</ItemTemplate>
						</asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                            <h2 class="h2 text-primary text-center fw-bold my-4"><asp:Literal ID="ltl_roadkill_month_animal2" runat="server"></asp:Literal>道路致死各類群統計
                            </h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
						    <th scope="col">#</th>
                                                    <th scope="col">類群</th>
                                                    <th scope="col">數量</th>
                                                </tr>
                                            </thead>
                                            <tbody>
						<asp:Repeater ID="rptRoadkill_month_animal2" runat="server">
						<ItemTemplate>
                                                <tr>
						    <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                                    <td><%# Eval("animal2") %></td>
                                                    <td><%# Eval("count") %></td>
                                                </tr>
						</ItemTemplate>
						</asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <section class="my-4">
                            <div class="row">
                                <div class="col-12 col-md-6">
                            <h2 class="h2 text-primary text-center fw-bold my-4"><asp:Literal ID="ltl_roadkill_year_site" runat="server"></asp:Literal>各工務段道路致死總數
                            </h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
						    <th scope="col">#</th>
                                                    <th scope="col">工務段</th>
                                                    <th scope="col">數量</th>
                                                </tr>
                                            </thead>
                                            <tbody>
						<asp:Repeater ID="rptRoadkill_year_site" runat="server">
						<ItemTemplate>
                                                <tr>
						    <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                                    <td><%# Eval("site_ch") %>工務段</td>
                                                    <td><%# Eval("count") %></td>
                                                </tr>
						</ItemTemplate>
						</asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="col-12 col-md-6">
                            <h2 class="h2 text-primary text-center fw-bold my-4"><asp:Literal ID="ltl_roadkill_month_site" runat="server"></asp:Literal>各工務段道路致死總數
                            </h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
						    <th scope="col">#</th>
                                                    <th scope="col">工務段</th>
                                                    <th scope="col">數量</th>
                                                </tr>
                                            </thead>
                                            <tbody>
						<asp:Repeater ID="rptRoadkill_month_site" runat="server">
						<ItemTemplate>
                                                <tr>
						    <th scope="row"><%# Container.ItemIndex + 1 %></th>
                                                    <td><%# Eval("site_ch") %>工務段</td>
                                                    <td><%# Eval("count") %></td>
                                                </tr>
						</ItemTemplate>
						</asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <!-- <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">BBBBB...</div> -->
                </div>
            </div>
            <div class="tab-pane fade <%= Session["roadkill_statistic"] != null && Session["roadkill_statistic"].ToString() == "1" ? "show mt-4 active" : "" %>" id="link02" role="tabpanel" aria-labelledby="link02-tab">
                <!-- a href="#" class="btn btn-primary d-inline-block me-7" data-bs-toggle="modal"
                data-bs-target="#searchModal2">資料查詢</a -->
                <div class="btn-group my-4" role="group" aria-label="Basic mixed styles example">
                    <a href="roadkill_add.aspx" class="btn btn-outline-primary" target="_blank">新增一筆</a>
                    <a href="roadkill_batch.aspx" class="btn btn-outline-primary" target="_blank">批次上傳</a>
                </div>

  <!-- div class="modal fade" id="searchModal2" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" -->
    <div class="modal-dialog modal-fullscreen modal-dialog-scrollable">
      <div class="modal-content">
        <!-- div class="modal-header">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div -->
        <div class="modal-body">
          <section id="tabs">
            <div class="container g-0">
              <div class="row">
                <div class="col">
                  <div id="underlineTabs">
                  <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                      <a class="nav-item nav-link <%= Session["roadkill_qtype"] == null || Session["roadkill_qtype"].ToString() == "1" ? "active" : "" %>" id="nav_home_tab" data-toggle="tab" href="#nav-search01a"
                        role="tab" aria-controls="nav-home" aria-selected="true">依國道公里數查詢</a>
                      <a class="nav-item nav-link <%= Session["roadkill_qtype"] != null && Session["roadkill_qtype"].ToString() == "2" ? "active" : "" %>" id="nav_profile_tab" data-toggle="tab" href="#nav-search02a"
                        role="tab" aria-controls="nav-profile" aria-selected="false">依工程分局查詢</a>
                    </div>
                  </nav>
                </div>
                  <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show bg-light mt-2 py-4 <%= Session["roadkill_qtype"] == null || Session["roadkill_qtype"].ToString() == "1" ? "active" : "" %>" id="nav-search01a" role="tabpanel"
                      aria-labelledby="nav-search01-tab">
                      <div class="container">
                      <div class="row g-2 my-2">
                        <div class="col">
                          <asp:TextBox ID="searchtxta" Cssclass="form-control form-control-lg" runat="server" placeholder="請輸入物種名稱"
                            aria-label=".form-control-lg example" />
                        </div>
                      </div>
                      <div class="row g-2 my-2">
                        <div class="col-12 col-md-6">
                          <div class="form-floating">
                            <asp:TextBox ID="start_datea" Cssclass="form-control" TextMode="date" runat="server" />
			    <asp:Label ID="lb_start_datea" AssociatedControlId="start_datea" runat="server">資料開始日期</asp:Label>
                          </div>
                        </div>
                        <div class="col-12 col-md-6">
                          <div class="form-floating">
                            <asp:TextBox ID="end_datea" Cssclass="form-control" TextMode="date" runat="server" />
			    <asp:Label ID="lb_end_datea" AssociatedControlId="end_datea" runat="server">資料結束日期</asp:Label>
                          </div>
                        </div>
                      </div>
                      <div class="row g-2 my-2">
                        <div class="col-12 col-md-4">
                          <div class="form-floating">
                            <asp:DropDownList ID="highwayida" Cssclass="form-select" runat="server" DataTextField="highway_name" DataValueField="highway_id"
			    aria-label="Floating label select example">
                            </asp:DropDownList>
                            <asp:Label ID="lb_highwayida" AssociatedControlId="highwayida" runat="server">國道編號</asp:Label>
                          </div>
                        </div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="start1a" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_start1a" AssociatedControlId="start1a" runat="server">起始里程(公里)</asp:Label>
                        </div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="start2a" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_start2a" AssociatedControlId="start2a" runat="server">＋公尺</asp:Label>
                        </div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="end1a" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_end1a" AssociatedControlId="end1a" runat="server">結束里程(公里)</asp:Label>
                        </div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="end2a" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_end2a" AssociatedControlId="end2a" runat="server">＋公尺</asp:Label>
                        </div>
                      </div>
                              <div class="row g-2 my-2">
                                <div class="col-12 col-md-4">
                                  <div class="form-floating">
                                    <asp:DropDownList ID="DropDownList3a" Cssclass="form-select" runat="server" DataSourceID="SqlDataSource3a" DataTextField="Sensitive_level" DataValueField="Sensitive_level"
				    aria-label="Floating label select example">
				    <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource3a" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' Sensitive_level UNION ALL SELECT DISTINCT Sensitive_level FROM freeway"></asp:SqlDataSource>
				    <asp:Label ID="lb_DropDownList3a" AssociatedControlId="DropDownList3a" runat="server">敏感里程等級</asp:Label>
                                  </div>
                                </div>
                                <div class="col-12 col-md-4">
                                  <div class="form-floating">
				    <asp:DropDownList ID="DropDownList1a" Cssclass="form-select" runat="server" DataSourceID="SqlDataSource1a" DataTextField="title" DataValueField="title"
				    aria-label="Floating label select example">
				    <asp:ListItem Value="ALL">ALL</asp:ListItem>
				    </asp:DropDownList>
				    <asp:SqlDataSource ID="SqlDataSource1a" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' title UNION ALL SELECT title FROM [Animal]"></asp:SqlDataSource>
				    <asp:Label ID="lb_DropDownList1a" AssociatedControlId="DropDownList1a" runat="server">物種類群</asp:Label>
                                  </div>
                                </div>
                                <div class="col-12 col-md-4">
                                  <div class="form-floating">
                                    <asp:DropDownList ID="DropDownList2a" Cssclass="form-select" runat="server" DataSourceID="SqlDataSource2a" DataTextField="species_Name" DataValueField="species"
				    aria-label="Floating label select example">
				    <asp:ListItem Value="">ALL</asp:ListItem>
                                    </asp:DropDownList>
				    <asp:SqlDataSource ID="SqlDataSource2a" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT '' AS species , 'ALL' AS species_Name  UNION ALL Select species , species_name From  Focus_Species "></asp:SqlDataSource>
				    <asp:Label ID="lb_DropDownList2a" AssociatedControlId="DropDownList2a" runat="server">關注物種</asp:Label>
                                  </div>
                                </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal">取消</button>
          <asp:Button ID="btn_searcha" Cssclass="btn btn-secondary" runat="server" OnClick="btnsearch_Click" OnClientClick="return check_highway();" Text="搜尋" />
        </div>
                              </div>
                    </div>
                    </div>
                    <div class="tab-pane fade show bg-light mt-2 py-4 <%= Session["roadkill_qtype"] != null && Session["roadkill_qtype"].ToString() == "2" ? "active" : "" %>" id="nav-search02a" role="tabpanel"
                      aria-labelledby="nav-search02-tab">
                              <div class="container">
                              <div class="row g-2 my-2">
                                <div class="col">
                          <asp:TextBox ID="searchtxt2a" Cssclass="form-control form-control-lg" runat="server" placeholder="請輸入關鍵字"
                            aria-label=".form-control-lg example" />
                                </div>
                              </div>
                      <div class="row g-2 my-2">
                        <div class="col-12 col-md-6">
                          <div class="form-floating">
                            <asp:TextBox ID="start_date2a" Cssclass="form-control" TextMode="date" runat="server" />
			    <asp:Label ID="lb_start_date2a" AssociatedControlId="start_date2a" runat="server">資料開始日期</asp:Label>
                          </div>
                        </div>
                        <div class="col-12 col-md-6">
                          <div class="form-floating">
                            <asp:TextBox ID="end_date2a" Cssclass="form-control" TextMode="date" runat="server" />
			    <asp:Label ID="lb_end_date2a" AssociatedControlId="end_date2a" runat="server">資料結束日期</asp:Label>
                          </div>
                        </div>
                      </div>
			<asp:UpdatePanel ID="upsitea" runat="server" UpdateMode="Conditional">
		        <ContentTemplate>
                      <div class="row g-2 my-2">
                        <div class="col-12 col-md-6">
                          <div class="form-floating">
                            <asp:DropDownList ID="DropDownList4a" Cssclass="form-select" runat="server" DataTextField="class1" DataValueField="class1"
			    aria-label="Floating label select example" OnSelectedIndexChanged="DropDownList4a_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Label ID="lb_DropDownList4a" AssociatedControlId="DropDownList4a" runat="server">工程分局</asp:Label>
                          </div>
                        </div>
                        <div class="col-12 col-md-6 form-floating">
                            <asp:DropDownList ID="DropDownList5a" Cssclass="form-select" runat="server" DataTextField="class2" DataValueField="class2"
			    aria-label="Floating label select example">
			    <asp:ListItem Value="ALL">ALL</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lb_DropDownList5a" AssociatedControlId="DropDownList5a" runat="server">工務段</asp:Label>
                        </div>
                      </div>
		        </ContentTemplate>
			<Triggers>
			    <asp:AsyncPostBackTrigger ControlID="DropDownList4a" EventName="SelectedIndexChanged" />
			</Triggers>
		        </asp:UpdatePanel>
                              <div class="row g-2 my-2">
                                <div class="col-12 col-md-4">
                                  <div class="form-floating">
                                    <asp:DropDownList ID="DropDownList32a" Cssclass="form-select" runat="server" DataSourceID="SqlDataSource32a" DataTextField="Sensitive_level" DataValueField="Sensitive_level"
				    aria-label="Floating label select example">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource32a" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' Sensitive_level UNION ALL SELECT DISTINCT Sensitive_level FROM freeway"></asp:SqlDataSource>
				    <asp:Label ID="lb_DropDownList32a" AssociatedControlId="DropDownList32a" runat="server">敏感里程等級</asp:Label>
                                  </div>
                                </div>
                                <div class="col-12 col-md-4">
                                  <div class="form-floating">
				    <asp:DropDownList ID="DropDownList12a" Cssclass="form-select" runat="server" DataSourceID="SqlDataSource12a" DataTextField="title" DataValueField="title"
				    aria-label="Floating label select example">
				    </asp:DropDownList>
				    <asp:SqlDataSource ID="SqlDataSource12a" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 'ALL' title UNION ALL SELECT title FROM [Animal]"></asp:SqlDataSource>
				    <asp:Label ID="lb_DropDownList12a" AssociatedControlId="DropDownList12a" runat="server">物種類群</asp:Label>
                                  </div>
                                </div>
                                <div class="col-12 col-md-4">
                                  <div class="form-floating">
                                    <asp:DropDownList ID="DropDownList22a" Cssclass="form-select" runat="server" DataSourceID="SqlDataSource22a" DataTextField="species_Name" DataValueField="species"
				    aria-label="Floating label select example">
                                    </asp:DropDownList>
				    <asp:SqlDataSource ID="SqlDataSource22a" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT '' AS species , 'ALL' AS species_Name  UNION ALL Select species , species_name From  Focus_Species "></asp:SqlDataSource>
				    <asp:Label ID="lb_DropDownList22a" AssociatedControlId="DropDownList22a" runat="server">關注物種</asp:Label>
                                  </div>
                                </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal">取消</button>
          <asp:Button ID="btn_search2a" Cssclass="btn btn-secondary" OnClick="btnsearch2_Click" runat="server" Text="搜尋" />
        </div>
                              </div>
                            </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
              </div>
                </div>
            </div>
          </section>
        </div>
      </div>
    </div>
  <!-- /div -->
  <!-- /.搜尋Modal -->

		<p class="my-3" style="display:none"><asp:Literal ID="ltl_query" runat="server" /></p>
                <!-- 道路致死title -->
		<div id="div_roadkill" runat="server" Visible="false">
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">道路致死資料
                        </h5>
                        <div class="form-check d-inline-block ms-2">
                            <asp:CheckBox id="flexCheck" runat="server" OnCheckedChanged="flexCheck_Changed" AutoPostBack="true" />
                            <asp:Label Cssclass="form-check-label" id="lbfor_flexCheck" runat="server" AssociatedControlId="flexCheck">
                                資料管理
                            </asp:Label>
                        </div>
		    </div>
		    <div><asp:Label ID="TotalCounts" runat="server" Text=""></asp:Label>
		    <asp:LinkButton ID="btn_excel" runat="server" OnClick="Export_Excel" Cssclass="whiteLink d-none d-md-inline-block d-lg-inline-block d-xl-inline-block ms-2"><i
			class="bi bi-file-earmark-arrow-down"></i>匯出excel</asp:LinkButton>
		    </div>
                </div>
                <!-- 道路致死table -->
	<asp:GridView ID="gv_roadkill" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="sds_roadkill" ShowHeaderWhenEmpty="true"
	OnRowCommand="gv_roadkill_RowCommand">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
	      <asp:BoundField DataField="id" Visible="False" ReadOnly="True" SortExpression="id" />
	      <asp:BoundField HeaderText="工務段" DataField="site_ch" SortExpression="site_ch" />
	      <asp:BoundField HeaderText="國道編號" DataField="highway_id" SortExpression="highway_id" />
	      <asp:BoundField HeaderText="方向" DataField="direction" SortExpression="direction" />
	      <asp:BoundField HeaderText="國道里程" DataField="milestone" SortExpression="milestone" />
	      <asp:BoundField HeaderText="日期" DataField="date" SortExpression="date" />
	      <asp:BoundField HeaderText="工作類別" DataField="type" SortExpression="type" />
	      <asp:BoundField HeaderText="可能種類" DataField="species" SortExpression="species" />
	      <asp:BoundField HeaderText="可能種類推測" DataField="deduce_species" SortExpression="deduce_species" />
	      <asp:BoundField HeaderText="照片" DataField="Pic" SortExpression="Pic" />
	      <asp:BoundField HeaderText="校對" Visible="False" DataField="varify" SortExpression="varify" />
	      <asp:TemplateField HeaderText="詳細資料">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("id","roadkill_detail.aspx?id={0}") %>' Target="roadkill_detail"><i class="bi bi-eye-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="編輯">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" Visible='<%# roadkill_editable(Eval("id").ToString()) %>' NavigateUrl='<%# Eval("id","roadkill_edit.aspx?id={0}") %>' Target="roadkill_edit"><i class="bi bi-pencil-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="刪除">
		<ItemTemplate>
		    <asp:LinkButton runat="server" Visible='<%# roadkill_editable(Eval("id").ToString()) %>' CommandName='Delete' CommandArgument='<%# Eval("id") %>' OnClientClick="if (confirm('是否刪除?') == false) return false;"><i class="bi bi-trash-fill"></i></asp:LinkButton>
		</ItemTemplate>
	      </asp:TemplateField>
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="sds_roadkill" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=""></asp:SqlDataSource>
        <div class="d-flex flex-column flex-md-row flex-lg-row flex-xl-row justify-content-center my-4" id="dmap" runat="server" Visible="false">
            <div class="p-2"><iframe id="roadkill_clustermap" width="500" height="800" frameborder="0" runat="server" scrolling="no"></iframe></div>
            <div class="p-2"><iframe id="roadkill_heatmap" width="500" height="800" frameborder="0" runat="server" scrolling="no"></iframe></div>
        </div>
	</div>
            </div>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
    <!-- bootstrap table -->
    <script>
	anychart.onDocumentReady(function() {

	var data = [{name: "常見物種路殺<asp:Literal ID="ltl_roadkill_time" runat="server"></asp:Literal>", children: [<asp:Literal ID="ltl_roadkill_species" runat="server"></asp:Literal>]}];
	var treeData = anychart.data.tree(data, "as-tree");
	var chart = anychart.treeMap(treeData);
	chart.hovered().fill("silver", 0.2);
	var customColorScale = anychart.scales.linearColor();
	customColorScale.colors(["Yellow", "MediumPurple"]);
	chart.colorScale(customColorScale);
	chart.container("container");
	chart.draw();
	});
    </script>
    <script>
        function nameFormatter(value, row) {
            console.log(row);
            if ((row.mode & 4) == 0) {
                return "有資料";
            }
            return "無資料";
        }
    </script>
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-table/1.16.0/locale/bootstrap-table-zh-TW.min.js">
    </script>
    <!-- chartjs cdn 這段放前面table會掛掉 -->
    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.4"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@0.7.0"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-style@0.5.0/dist/chartjs-plugin-style.min.js"></script>
    <!-- chart -->
    <script>
        // 第二張圖-bar chart(H)
        // https://tobiasahlin.com/blog/chartjs-charts-to-get-you-started/
        new Chart(document.getElementById("bar-chart-horizontal"), {
            type: 'horizontalBar',
            data: {
                labels: [<asp:Literal ID="ltl_roadkill_focus_name" runat="server"></asp:Literal>],
                datasets: [{
                    label: "路殺數",
                    backgroundColor: ["#3e95cd","#3e95cd","#3e95cd","#3e95cd","#3e95cd","#3e95cd","#3e95cd","#3e95cd","#3e95cd"],
                    data: [<asp:Literal ID="ltl_roadkill_focus_count" runat="server"></asp:Literal>]
                }]
            },
            options: {
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: '關注物種路殺<asp:Literal ID="ltl_roadkill_time2" runat="server"></asp:Literal>'
                }
            }
        });


        // 三個折線圖
        new Chart(document.getElementById("line-chart"), {
            type: 'line',
            data: {
                labels: [<asp:Literal ID="ltl_roadkill_year1" runat="server"></asp:Literal>],
                datasets: [{
                    data: [<asp:Literal ID="ltl_roadkill_line1" runat="server"></asp:Literal>],
                    label: "石虎",
                    borderColor: "#3e95cd",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line2" runat="server"></asp:Literal>],
                    label: "麝香貓",
                    borderColor: "#8e5ea2",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line3" runat="server"></asp:Literal>],
                    label: "環頸雉",
                    borderColor: "#3cba9f",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line4" runat="server"></asp:Literal>],
                    label: "台灣山羌",
                    borderColor: "#c45850",
                    fill: false
                }]
            },
            options: {
                title: {
                    display: false,
                }
            }
        });
        new Chart(document.getElementById("line-chart2"), {
            type: 'line',
            data: {
                labels: [<asp:Literal ID="ltl_roadkill_year2" runat="server"></asp:Literal>],
                datasets: [{
                    data: [<asp:Literal ID="ltl_roadkill_line5" runat="server"></asp:Literal>],
                    label: "台灣獼猴",
                    borderColor: "#3e95cd",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line6" runat="server"></asp:Literal>],
                    label: "大冠鷲",
                    borderColor: "#8e5ea2",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line7" runat="server"></asp:Literal>],
                    label: "穿山甲",
                    borderColor: "#3cba9f",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line8" runat="server"></asp:Literal>],
                    label: "鼬獾",
                    borderColor: "#c45850",
                    fill: false
                }]
            },
            options: {
                title: {
                    display: false,
                }
            }
        });
        new Chart(document.getElementById("line-chart3"), {
            type: 'line',
            data: {
                labels: [<asp:Literal ID="ltl_roadkill_year3" runat="server"></asp:Literal>],
                datasets: [{
                    data: [<asp:Literal ID="ltl_roadkill_line9" runat="server"></asp:Literal>],
                    label: "台灣野兔",
                    borderColor: "#3e95cd",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line10" runat="server"></asp:Literal>],
                    label: "領角鴞",
                    borderColor: "#8e5ea2",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line11" runat="server"></asp:Literal>],
                    label: "鳳頭蒼鷹",
                    borderColor: "#3cba9f",
                    fill: false
                }, {
                    data: [<asp:Literal ID="ltl_roadkill_line12" runat="server"></asp:Literal>],
                    label: "白鼻心",
                    borderColor: "#c45850",
                    fill: false
                }]
            },
            options: {
                title: {
                    display: false,
                }
            }
        });

    var ctx2 = document.getElementById("barChart").getContext('2d');
    var elBarChart = new Chart(ctx2, {
      type: 'bar',
      data: {
        labels: [<asp:Literal ID="ltl_sitech_labels" runat="server"></asp:Literal>],
        datasets: [{
          label: '<asp:Literal ID="ltl_month1" runat="server"></asp:Literal>月',
          backgroundColor: "#caf270",
          data: [<asp:Literal ID="ltl_up_month1" runat="server"></asp:Literal>],
        }, {
          label: '<asp:Literal ID="ltl_month2" runat="server"></asp:Literal>月',
          backgroundColor: "#45c490",
          data: [<asp:Literal ID="ltl_up_month2" runat="server"></asp:Literal>],
        }, {
          label: '<asp:Literal ID="ltl_month3" runat="server"></asp:Literal>月',
          backgroundColor: "#008d93",
          data: [<asp:Literal ID="ltl_up_month3" runat="server"></asp:Literal>],
        }, {
          label: '<asp:Literal ID="ltl_month4" runat="server"></asp:Literal>月',
          backgroundColor: "#caf270",
          data: [<asp:Literal ID="ltl_up_month4" runat="server"></asp:Literal>],
        }, {
          label: '<asp:Literal ID="ltl_month5" runat="server"></asp:Literal>月',
          backgroundColor: "#45c490",
          data: [<asp:Literal ID="ltl_up_month5" runat="server"></asp:Literal>],
        }, {
          label: '<asp:Literal ID="ltl_month6" runat="server"></asp:Literal>月',
          backgroundColor: "#008d93",
          data: [<asp:Literal ID="ltl_up_month6" runat="server"></asp:Literal>],
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
        legend: {
          position: 'bottom'
        },
        responsive: true,
        maintainAspectRatio: false,
      }
    });

    </script>
</asp:Content>
