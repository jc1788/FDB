﻿<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="occurrence_search.aspx.cs" Inherits="occurrence_search" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">生態調查資料</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">生態調查資料</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <!-- /.標題+麵包屑 -->
    <!-- 主要內容區塊 -->
    <main class="container" id="page">
                <!-- a href="#" class="btn btn-primary d-inline-block me-7" data-bs-toggle="modal"
                data-bs-target="#searchModal2">資料查詢</a -->
                <div class="btn-group my-4" role="group" aria-label="Basic mixed styles example">
                    <a href="occurrence_add.aspx" class="btn btn-outline-primary" target="_blank">新增一筆</a>
                    <a href="occurrence_batch.aspx" class="btn btn-outline-primary" target="_blank">批次上傳</a>
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
                      <a class="nav-item nav-link <%= Session["occurrence_qtype"] == null || Session["occurrence_qtype"].ToString() == "1" ? "active" : "" %>" id="nav_home_tab" data-toggle="tab" href="#nav-search01a"
                        role="tab" aria-controls="nav-home" aria-selected="true">依國道公里數查詢</a>
                      <a class="nav-item nav-link <%= Session["occurrence_qtype"] != null && Session["occurrence_qtype"].ToString() == "2" ? "active" : "" %>" id="nav_profile_tab" data-toggle="tab" href="#nav-search02a"
                        role="tab" aria-controls="nav-profile" aria-selected="false">依工程分局查詢</a>
                    </div>
                  </nav>
                </div>
                  <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show bg-light mt-2 py-4 <%= Session["occurrence_qtype"] == null || Session["occurrence_qtype"].ToString() == "1" ? "active" : "" %>" id="nav-search01a" role="tabpanel"
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
          <asp:Button ID="btn_searcha" Cssclass="btn btn-secondary" runat="server" OnClick="btnsearch_Click" Text="搜尋" />
        </div>
                              </div>
                    </div>
                    </div>
                    <div class="tab-pane fade show bg-light mt-2 py-4 <%= Session["occurrence_qtype"] != null && Session["occurrence_qtype"].ToString() == "2" ? "active" : "" %>" id="nav-search02a" role="tabpanel"
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
                <!-- 生態調查title -->
		<div id="div_occurrence" runat="server" Visible="false">
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">生態調查資料
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
                <!-- 生態調查table -->
	<asp:GridView ID="gv_occurrence" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="sds_occurrence" ShowHeaderWhenEmpty="true"
	OnRowCommand="gv_occurrence_RowCommand">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
	      <asp:BoundField DataField="siteid" Visible="False" ReadOnly="True" SortExpression="siteid" />
	      <asp:BoundField HeaderText="中文名" DataField="Chinese_Name" SortExpression="Chinese_Name" />
	      <asp:BoundField HeaderText="調查地點" DataField="site_ch" SortExpression="site_ch" />
	      <asp:BoundField HeaderText="國道編號" DataField="highway_id" SortExpression="highway_id" />
	      <asp:BoundField HeaderText="調查日期" DataField="invest_date" SortExpression="invest_date" />
	      <asp:BoundField HeaderText="經度" DataField="x" SortExpression="x" />
	      <asp:BoundField HeaderText="緯度" DataField="y" SortExpression="y" />
	      <asp:HyperLinkField DataNavigateUrlFields="sid,Short_Name" DataNavigateUrlFormatString="occurrence_detail.aspx?sid={0}&amp;Short_Name={1}" HeaderText="詳細資料" Target="occurrence_detail" Text="<i class='bi bi-eye-fill'></i>" />
	      <asp:TemplateField Visible="False" HeaderText="編輯">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" Visible='<%# occurrence_editable(Eval("sid").ToString()) %>' NavigateUrl='<%# Eval("sid","occurrence_edit.aspx?id={0}") %>' Target="occurrence_edit"><i class="bi bi-pencil-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="刪除">
		<ItemTemplate>
		    <asp:LinkButton runat="server" Visible='<%# occurrence_editable(Eval("sid").ToString()) %>' CommandName='Delete' CommandArgument='<%# Eval("sid") %>' OnClientClick="if (confirm('是否刪除?') == false) return false;"><i class="bi bi-trash-fill"></i></asp:LinkButton>
		</ItemTemplate>
	      </asp:TemplateField>
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="sds_occurrence" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="">
	</asp:SqlDataSource>

        <div class="d-flex flex-column flex-md-row flex-lg-row flex-xl-row justify-content-center my-4" id="dmap" runat="server" Visible="false">
		<iframe id="occurence_clustermap" width="500" height="800" frameborder="0" runat="server" scrolling="no"></iframe>
        </div>
	</div>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
