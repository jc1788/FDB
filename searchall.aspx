<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="searchall.aspx.cs" Inherits="searchall" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">查詢結果</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
			<li class="breadcrumb-item active" aria-current="page">查詢結果</li>
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
                        role="tab" aria-controls="nav-home" aria-selected="true">查詢條件</a>
                    <!-- <a class="nav-item nav-link" id="nav-profile-tab" data-toggle="tab" href="#nav-search02"
                role="tab" aria-controls="nav-profile" aria-selected="false">進階搜尋</a> -->
                </div>
            </nav>
        </div>
        <p class="my-3"><asp:Literal ID="ltl_query" runat="server" /></p>
        <!-- 生態調查title -->
	<div id="div_occurrence" runat="server">
        <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
            <div class="pt-2">
                <h5 class="d-inline-block">生態調查資料
                </h5>
                <div class="form-check d-inline-block ms-2">
                    <asp:CheckBox id="flexCheck3" runat="server" OnCheckedChanged="flexCheck3_Changed" AutoPostBack="true" />
                    <asp:Label Cssclass="form-check-label" id="lbfor_flexCheck3" runat="server" AssociatedControlId="flexCheck3">
                        資料管理
                    </asp:Label>
                </div>
            </div>
            <div><asp:Label ID="TotalCounts_occurrence" runat="server" Text=""></asp:Label>
	    <asp:LinkButton ID="btn_excel_occurrence" runat="server" OnClick="Export_Excel_occurrence" Cssclass="whiteLink d-none d-md-inline-block d-lg-inline-block d-xl-inline-block ms-2"><i
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
        <!-- div class="d-flex flex-column flex-md-row flex-lg-row flex-xl-row justify-content-center my-4">
            <div class="p-2"><img src="./assets/images/map01.png" alt=""></div>
            <div class="p-2"><img src="./assets/images/map02.png" alt=""></div>
        </div -->
	</div>
        <!-- 道路致死title -->
	<div id="div_roadkill" runat="server">
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
	    <div><asp:Label ID="TotalCounts_roadkill" runat="server" Text=""></asp:Label>
	    <asp:LinkButton ID="btn_excel_roadkill" runat="server" OnClick="Export_Excel_roadkill" Cssclass="whiteLink d-none d-md-inline-block d-lg-inline-block d-xl-inline-block ms-2"><i
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
        <div class="d-flex flex-column flex-md-row flex-lg-row flex-xl-row justify-content-center my-4" id="dmap" runat="server">
            <div class="p-2"><iframe id="roadkill_clustermap" width="500" height="800" frameborder="0" runat="server" scrolling="no"></iframe></div>
            <div class="p-2"><iframe id="roadkill_heatmap" width="500" height="800" frameborder="0" runat="server" scrolling="no"></iframe></div>
        </div>
	</div>
        <!-- 現況植栽title -->
	<div id="div_plant" runat="server">
        <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
            <div class="pt-2">
                <h5 class="d-inline-block">現況植栽資料
                </h5>
                <div class="form-check d-inline-block ms-2">
                    <asp:CheckBox id="flexCheck2" runat="server" OnCheckedChanged="flexCheck2_Changed" AutoPostBack="true" />
                    <asp:Label Cssclass="form-check-label" id="lbfor_flexCheck2" runat="server" AssociatedControlId="flexCheck2">
                        資料管理
                    </asp:Label>
                </div>
            </div>
            <div><asp:Label ID="TotalCounts_plant" runat="server" Text=""></asp:Label>
	    <asp:LinkButton ID="btn_excel_plant" runat="server" OnClick="Export_Excel_plant" Cssclass="whiteLink d-none d-md-inline-block d-lg-inline-block d-xl-inline-block ms-2"><i
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
        </div>
        <!-- icon按鈕 -->
        <!-- p>一般資料的檢視/編輯/刪除
            <a href="page2.html"><i class="bi bi-eye-fill"></i></a>
            <a class="text-primary" href="page3.html"><i class="bi bi-pencil-fill"></i></a>
            <a class="text-primary" data-bs-toggle="modal" data-bs-target="#Modal2"><i class="bi bi-trash-fill"></i></a>
        </p>
<p>植栽的檢視（modal）<a class="text-primary" data-bs-toggle="modal" data-bs-target="#searchModal"><i class="bi bi-eye-fill"></i></a>
</p -->

        <!-- /.icon按鈕 -->
        <!-- div class="p-2 bg-light"><img src="./assets/images/map01.png" alt=""></div -->
        <!-- Modal -->
        <div class="modal fade" id="searchModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-fullscreen modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <section id="tabs">
                            <div class="container g-0">
                                <div class="row">
                                    <div class="col">
                                        <div id="underlineTabs">
                                            <nav>
                                                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                                    <a class="nav-item nav-link active" id="nav-home-tab"
                                                        data-toggle="tab" href="#nav-search01" role="tab"
                                                        aria-controls="nav-home" aria-selected="true">七里香(植物標題名稱)</a>
                                                </div>
                                            </nav>
                                        </div>
                                        <div class="tab-content" id="nav-tabContent">
                                            <div class="tab-pane fade show active mt-2 py-4" id="nav-search01"
                                                role="tabpanel" aria-labelledby="nav-search01-tab">
                                                <div class="container">
                                                    <h4>(這裡放植物細節的表格)</h4>
                                                    <table id="table2" data-toggle="table" data-flat="true"
                                                        data-pagination="true" data-search="false"
                                                        data-url="table2.json">
                                                        <thead>
                                                            <tr class="bg-primary text-white">
                                                                <th data-field="name">物種名稱</th>
                                                                <th data-field="國道編號" data-sortable="true">國道編號</th>
                                                                <th data-field="國道方向" data-sortable="true">國道方向</th>
                                                                <th data-field="參考里程" data-sortable="true">參考里程</th>
                                                                <th data-field="工務段">工務段</th>
                                                                <th data-field="調查日期">調查日期</th>
                                                                <th data-field="檢視" data-formatter="nameFormatter">檢視
                                                                </th>
                                                                <th data-field="編輯">編輯</th>
                                                                <th data-field="刪除">刪除</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                    <hr>
                                                    <div>
                                                        <img src="./assets/images/map02.png" alt="">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">取消</button>
                        <button type="button" class="btn btn-primary">搜尋</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.Modal -->
        <!-- Modal2 -->
        <div id="Modal2" class="modal" tabindex="-1">
            <div class="modal-dialog">
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                  <p>您確定要刪除此筆資料？</p>
                </div>
                <div class="modal-footer ">
                  <button type="button" class="btn btn-outline-secondary" data-bs-dismiss="modal">取消</button>
                  <button type="button" class="btn btn-danger">刪除</button>
                </div>
              </div>
            </div>
          </div>
        <!-- /.Modal2 -->
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
    <!-- bootstrap table -->
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
</asp:Content>
