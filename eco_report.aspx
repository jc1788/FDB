<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="eco_report.aspx.cs" Inherits="eco_report" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">生態調查報告</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">生態調查報告</li>
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
                    <a href="eco_report_add.aspx" class="btn btn-outline-primary">新增一筆</a>
                </div>

		<asp:Label ID="Label1" runat="server" Text="查詢關鍵字 : " AssociatedControlId="TextBox1" Visible="false"></asp:Label>
		<asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True"  Visible="false"></asp:TextBox>

		<p class="my-3" style="display:none"><asp:Literal ID="ltl_query" runat="server" /></p>
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">生態調查報告
                        </h5>
                        <div class="form-check d-inline-block ms-2">
                            <asp:CheckBox id="flexCheck" runat="server" OnCheckedChanged="flexCheck_Changed" AutoPostBack="true" />
                            <asp:Label Cssclass="form-check-label" id="lbfor_flexCheck" runat="server" AssociatedControlId="flexCheck">
                                資料管理
                            </asp:Label>
                        </div>
                    </div>
                </div>
                <!-- 生態調查table -->
	<asp:GridView ID="gv_eco_report" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="true"
	OnRowCommand="gv_eco_report_RowCommand">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
              <asp:BoundField DataField="id" HeaderText="流水號" SortExpression="id" />
              <asp:BoundField DataField="plan_name" HeaderText="計畫名稱" SortExpression="plan_name" />
              <asp:BoundField DataField="plan_id" HeaderText="計畫編號" SortExpression="plan_id" />
              <asp:BoundField DataField="start_date" HeaderText="調查開始日期" SortExpression="start_date" />
              <asp:BoundField DataField="end_date" HeaderText="調查結束日期" SortExpression="end_date" />
              <asp:BoundField DataField="owner" HeaderText="計畫主持人" SortExpression="owner" />
              <asp:BoundField DataField="provider" HeaderText="資料提供者" SortExpression="provider" />
              <asp:BoundField DataField="location" HeaderText="調查地點" SortExpression="location" />
              <asp:BoundField DataField="keywords" HeaderText="關鍵字" SortExpression="keywords" />
	      <asp:TemplateField Visible="False" HeaderText="編輯">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" Visible='<%# eco_report_editable(Eval("id").ToString()) %>' NavigateUrl='<%# Eval("id","eco_report_edit.aspx?id={0}") %>'><i class="bi bi-pencil-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="刪除">
		<ItemTemplate>
		    <asp:LinkButton runat="server" Visible='<%# eco_report_editable(Eval("id").ToString()) %>' CommandName='Delete' CommandArgument='<%# Eval("id") %>' OnClientClick="if (confirm('是否刪除?') == false) return false;"><i class="bi bi-trash-fill"></i></asp:LinkButton>
		</ItemTemplate>
	      </asp:TemplateField>
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT [id], [plan_name], [plan_id], [type], CONVERT (char(10), [start_date], 126) AS [start_date], CONVERT (char(10), [end_date], 126) AS [end_date], [owner], [provider], [location], [keywords], [abstracts] FROM [Eco_report]">
	</asp:SqlDataSource>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
