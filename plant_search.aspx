<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_search.aspx.cs" Inherits="plant_search" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">現況植栽資料</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">現況植栽資料</li>
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
                    <a href="plant_add.aspx" class="btn btn-outline-primary" target="_blank">新增一筆</a>
                    <a href="plant_batch.aspx" class="btn btn-outline-primary" target="_blank">批次上傳</a>
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
                      <a class="nav-item nav-link <%= Session["plant_qtype"] == null || Session["plant_qtype"].ToString() == "1" ? "active" : "" %>" id="nav_home_tab" data-toggle="tab" href="#nav-search01a"
                        role="tab" aria-controls="nav-home" aria-selected="true">依國道公里數查詢</a>
                      <a class="nav-item nav-link <%= Session["plant_qtype"] != null && Session["plant_qtype"].ToString() == "2" ? "active" : "" %>" id="nav_profile_tab" data-toggle="tab" href="#nav-search02a"
                        role="tab" aria-controls="nav-profile" aria-selected="false">依工程分局查詢</a>
                    </div>
                  </nav>
                </div>
                  <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show bg-light mt-2 py-4 <%= Session["plant_qtype"] == null || Session["plant_qtype"].ToString() == "1" ? "active" : "" %>" id="nav-search01a" role="tabpanel"
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
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="florescenceS" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_florescenceS" AssociatedControlId="florescenceS" runat="server">花期(月份)</asp:Label>
			</div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="florescenceE" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			</div>
                        <div class="col-12 col-md-2 form-floating">
                                                        <asp:DropDownList ID="flowercolor" Cssclass="form-select" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>白</asp:ListItem>
                                                            <asp:ListItem>紅</asp:ListItem>
                                                            <asp:ListItem>粉紅</asp:ListItem>
                                                            <asp:ListItem>黃</asp:ListItem>
                                                            <asp:ListItem>綠</asp:ListItem>
                                                            <asp:ListItem>紫</asp:ListItem>
                                                            <asp:ListItem>藍</asp:ListItem>
                                                        </asp:DropDownList>
				<asp:Label ID="lb_flowercolor" AssociatedControlId="flowercolor" runat="server">花色</asp:Label>
			</div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="leafperiodS" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_leafperiodS" AssociatedControlId="leafperiodS" runat="server">葉色轉變期(月份)</asp:Label>
			</div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="leafperiodE" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			</div>
                        <div class="col-12 col-md-2 form-floating">
                                                        <asp:DropDownList ID="leafcolor" Cssclass="form-select" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>白</asp:ListItem>
                                                            <asp:ListItem>紅</asp:ListItem>
                                                            <asp:ListItem>紫</asp:ListItem>
                                                            <asp:ListItem>黃</asp:ListItem>
                                                            <asp:ListItem>綠</asp:ListItem>
                                                            <asp:ListItem>褐</asp:ListItem>
                                                        </asp:DropDownList>
				<asp:Label ID="lb_leafcolor" AssociatedControlId="leafcolor" runat="server">葉色</asp:Label>
			</div>
		      </div>
                      <div class="row g-2 my-2">
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" data-bs-dismiss="modal">取消</button>
          <asp:Button ID="btn_searcha" Cssclass="btn btn-secondary" runat="server" OnClick="btnsearch_Click" Text="搜尋" />
        </div>
                              </div>
                    </div>
                    </div>
                    <div class="tab-pane fade show bg-light mt-2 py-4 <%= Session["plant_qtype"] != null && Session["plant_qtype"].ToString() == "2" ? "active" : "" %>" id="nav-search02a" role="tabpanel"
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
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="florescenceS2" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_florescenceS2" AssociatedControlId="florescenceS2" runat="server">花期(月份)</asp:Label>
			</div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="florescenceE2" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			</div>
                        <div class="col-12 col-md-2 form-floating">
                                                        <asp:DropDownList ID="flowercolor2" Cssclass="form-select" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>白</asp:ListItem>
                                                            <asp:ListItem>紅</asp:ListItem>
                                                            <asp:ListItem>粉紅</asp:ListItem>
                                                            <asp:ListItem>黃</asp:ListItem>
                                                            <asp:ListItem>綠</asp:ListItem>
                                                            <asp:ListItem>紫</asp:ListItem>
                                                            <asp:ListItem>藍</asp:ListItem>
                                                        </asp:DropDownList>
				<asp:Label ID="lb_flowercolor2" AssociatedControlId="flowercolor2" runat="server">花色</asp:Label>
			</div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="leafperiodS2" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			    <asp:Label ID="lb_leafperiodS2" AssociatedControlId="leafperiodS2" runat="server">葉色轉變期(月份)</asp:Label>
			</div>
                        <div class="col-12 col-md-2 form-floating">
                            <asp:TextBox ID="leafperiodE2" Cssclass="form-control" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server" />
			</div>
                        <div class="col-12 col-md-2 form-floating">
                                                        <asp:DropDownList ID="leafcolor2" Cssclass="form-select" runat="server">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem>白</asp:ListItem>
                                                            <asp:ListItem>紅</asp:ListItem>
                                                            <asp:ListItem>紫</asp:ListItem>
                                                            <asp:ListItem>黃</asp:ListItem>
                                                            <asp:ListItem>綠</asp:ListItem>
                                                            <asp:ListItem>褐</asp:ListItem>
                                                        </asp:DropDownList>
				<asp:Label ID="lb_leafcolor2" AssociatedControlId="leafcolor2" runat="server">葉色</asp:Label>
			</div>
		      </div>
                      <div class="row g-2 my-2">
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
                <!-- 現況植栽title -->
		<div id="div_plant" runat="server" Visible="false">
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">現況植栽資料
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
                <!-- 現況植栽table -->
	<asp:GridView ID="gv_plant" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="sds_plant" ShowHeaderWhenEmpty="true"
	OnRowCommand="gv_plant_RowCommand">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
	      <asp:BoundField DataField="plantid" Visible="False" ReadOnly="True" SortExpression="id" />
	      <asp:BoundField HeaderText="物種名稱" DataField="plant_name" SortExpression="plant_name" />
	      <asp:BoundField HeaderText="工務段" DataField="segment" SortExpression="segment" />
	      <asp:BoundField HeaderText="國道編號" DataField="highway_name" SortExpression="highway_name" />
	      <asp:BoundField HeaderText="方向" DataField="direction" SortExpression="direction" />
	      <asp:BoundField HeaderText="里程" DataField="range" SortExpression="range" />
	      <asp:BoundField HeaderText="位置" DataField="plant_loca" SortExpression="plant_loca" />
	      <asp:BoundField HeaderText="日期" DataField="date" SortExpression="date" />
	      <asp:BoundField HeaderText="數量" DataField="plant_number" SortExpression="plant_number" />
	      <asp:TemplateField HeaderText="詳細資料">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("plantid","plant_detail.aspx?id={0}") %>' Target="plant_detail"><i class="bi bi-eye-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="編輯">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" Visible='<%# plant_editable(Eval("plantid").ToString()) %>' NavigateUrl='<%# Eval("plantid","plant_edit.aspx?id={0}") %>' Target="plant_edit"><i class="bi bi-pencil-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="刪除">
		<ItemTemplate>
		    <asp:LinkButton runat="server" Visible='<%# plant_editable(Eval("plantid").ToString()) %>' CommandName='Delete' CommandArgument='<%# Eval("plantid") %>' OnClientClick="if (confirm('是否刪除?') == false) return false;"><i class="bi bi-trash-fill"></i></asp:LinkButton>
		</ItemTemplate>
	      </asp:TemplateField>
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="sds_plant" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=""></asp:SqlDataSource>

        <div class="d-flex flex-column flex-md-row flex-lg-row flex-xl-row justify-content-center my-4" id="dmap" runat="server" Visible="false">
		<iframe id="plant_map" width="500" height="800" frameborder="0" runat="server" scrolling="no"></iframe>
        </div>
	</div>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
