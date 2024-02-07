<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_eng.aspx.cs" Inherits="plant_eng" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">植栽工程資料</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">植栽工程資料</li>
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
                    <a href="plant_eng_add.aspx" class="btn btn-outline-primary">新增一筆</a>
                </div>

		<br>
		<asp:Label ID="Label_Keywords" runat="server" Text="查詢關鍵字 : " AssociatedControlId="Keywords"></asp:Label>
		<asp:TextBox ID="Keywords" runat="server"></asp:TextBox>
		<asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData"/>
		<br><br>

		<p class="my-3" style="display:none"><asp:Literal ID="ltl_query" runat="server" /></p>
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">植栽工程資料
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
	<asp:GridView ID="gv_plant_eng" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="true"
	OnRowCommand="gv_plant_eng_RowCommand">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
              <asp:HyperLinkField DataNavigateUrlFields="pid" DataNavigateUrlFormatString="Plant_observation.aspx?pid={0}" HeaderText="編輯明細" Text="點我" Target="_blank" DataTextField="pid" SortExpression="pid" />
              <asp:BoundField DataField="plant_id" HeaderText="編號" SortExpression="plant_id" />
              <asp:BoundField DataField="number" HeaderText="標別" SortExpression="number" />
              <asp:BoundField DataField="plant_name" HeaderText="計畫名稱" SortExpression="plant_name" />
              <asp:BoundField DataField="institution" HeaderText="設計單位" SortExpression="institution" />
              <asp:BoundField DataField="design_date" HeaderText="設計定稿日期" SortExpression="design_date" />
              <asp:BoundField DataField="work_date" HeaderText="竣工日期" SortExpression="work_date" />
              <asp:BoundField DataField="maintain" HeaderText="維護單位" SortExpression="maintain" />
              <asp:BoundField DataField="cnts" HeaderText="明細筆數" SortExpression="cnts" />
	      <asp:TemplateField Visible="False" HeaderText="編輯">
	        <ItemTemplate>
		    <asp:HyperLink runat="server" Visible='<%# plant_eng_editable(Eval("pid").ToString()) %>' NavigateUrl='<%# Eval("pid","plant_eng_edit.aspx?pid={0}") %>'><i class="bi bi-pencil-fill"></i></asp:HyperLink>
		</ItemTemplate>
	      </asp:TemplateField>
	      <asp:TemplateField Visible="False" HeaderText="刪除">
		<ItemTemplate>
		    <asp:LinkButton runat="server" Visible='<%# plant_eng_editable(Eval("pid").ToString()) %>' CommandName='Delete' CommandArgument='<%# Eval("pid") %>' OnClientClick="if (confirm('是否刪除?') == false) return false;"><i class="bi bi-trash-fill"></i></asp:LinkButton>
		</ItemTemplate>
	      </asp:TemplateField>
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="">
	</asp:SqlDataSource>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
