<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_observation.aspx.cs" Inherits="plant_observation" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">植栽工程資料明細</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">植栽工程資料明細</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <!-- /.標題+麵包屑 -->
    <!-- 主要內容區塊 -->
    <main class="container" id="page">
		<br>
		<p class="my-3" style="display:none"><asp:Literal ID="ltl_query" runat="server" /></p>
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">植栽工程資料明細
                        </h5>
                    </div>
                </div>
                <!-- 生態調查table -->
	<asp:GridView ID="gv_plant_observation" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="SqlDataSource1" ShowHeaderWhenEmpty="true"
	OnRowCommand="gv_plant_observation_RowCommand">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
              <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                                                
              <asp:BoundField DataField="chinese_name" HeaderText="植物中文名" SortExpression="chinese_name" />
              <asp:BoundField DataField="ScientificName" HeaderText="植物學名" SortExpression="ScientificName" />
              <asp:BoundField DataField="high" HeaderText="高度" SortExpression="high" />
              <asp:BoundField DataField="width" HeaderText="幅寬" SortExpression="width" />
              <asp:BoundField DataField="m_high" HeaderText="米高徑" SortExpression="m_high" />
              <asp:BoundField DataField="amount" HeaderText="數量" SortExpression="amount" />
                
              <asp:BoundField DataField="location" HeaderText="位置" SortExpression="location" />
              <asp:BoundField DataField="direction" HeaderText="方向" SortExpression="direction" />
              <asp:BoundField DataField="range" HeaderText="里程範圍" SortExpression="range" />
              <asp:BoundField DataField="highway_id" HeaderText="國道編號" SortExpression="highway_id" />
              <asp:BoundField DataField="end_mileage1" HeaderText="開始公里" SortExpression="end_mileage1" />
              <asp:BoundField DataField="end_mileage2" HeaderText="開始公尺" SortExpression="end_mileage2" />
              <asp:BoundField DataField="start_mileage1" HeaderText="結束公里" SortExpression="start_mileage1" />
              <asp:BoundField DataField="start_mileage2" HeaderText="結束公尺" SortExpression="start_mileage2" />
              <asp:HyperLinkField DataNavigateUrlFields="id,pid" DataNavigateUrlFormatString="Plant_observation_edit.aspx?id={0}&pid={1}" HeaderText="編輯" Target="_self" Text="<i class='bi bi-pencil-fill'></i>" />
	      <asp:TemplateField HeaderText="刪除">
		<ItemTemplate>
		    <asp:LinkButton runat="server" Visible='<%# plant_observation_editable(Eval("id").ToString()) %>' CommandName='Delete' CommandArgument='<%# Eval("id") %>' OnClientClick="if (confirm('是否刪除?') == false) return false;"><i class="bi bi-trash-fill"></i></asp:LinkButton>
		</ItemTemplate>
	      </asp:TemplateField>
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT [id], [chinese_name], [ScientificName], [high], [width], [m_high], [amount], [pid], [location], [direction], [range], [highway_id], [end_mileage1], [end_mileage2], [start_mileage1], [start_mileage2] FROM [Plant_observation] WHERE ([pid] = @pid)">
	    <SelectParameters>
		<asp:QueryStringParameter Name="pid" QueryStringField="pid" Type="Int32" />
	    </SelectParameters>
	</asp:SqlDataSource>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
