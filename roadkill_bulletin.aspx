<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="roadkill_bulletin.aspx.cs" Inherits="roadkill_bulletin" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">

    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">通報事件查詢</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">通報事件查詢</li>
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

		<br>
		<asp:Label ID="Label_Keywords" runat="server" Text="查詢關鍵字 : " AssociatedControlId="KeyWords"></asp:Label>
		<asp:TextBox ID="KeyWords" runat="server"></asp:TextBox>
		<asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData"/>
		<br><br>

		<p class="my-3" style="display:none"><asp:Literal ID="ltl_query" runat="server" /></p>
                <div class="d-flex justify-content-between align-items-center bg-secondary rounded-top py-2 px-3">
                    <div class="pt-2">
                        <h5 class="d-inline-block">受傷動物通報
                        </h5>
                    </div>
		    <div><asp:Label ID="TotalCounts" runat="server" Text=""></asp:Label>
		    </div>
                </div>	
                <!-- 受傷動物通報table -->
	<asp:GridView ID="GridView_List" Cssclass="table table-bordered table-condensed table-hover" runat="server"
	AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false" DataSourceID="SDS_View" ShowHeaderWhenEmpty="true">
	    <HeaderStyle CssClass="bg-primary text-white" ForeColor="#FFFFFF" />
	    <Columns>
              <asp:BoundField DataField="highway_id" HeaderText=" 國道編號 " SortExpression="highway_id" />
              <asp:BoundField DataField="mileage" HeaderText=" 里程 " SortExpression="mileage" />
              <asp:BoundField DataField="date" HeaderText=" 發現日期 " SortExpression="date" />
              <asp:BoundField DataField="time" HeaderText=" 發現時間 " SortExpression="time" />
              <asp:BoundField DataField="species" HeaderText=" 可能種類 " SortExpression="species" />
              <asp:BoundField DataField="institution" HeaderText=" 通報單位 " SortExpression="institution" />
              <asp:BoundField DataField="reporter" HeaderText=" 通報者 " SortExpression="reporter" />
              <asp:BoundField DataField="status" HeaderText=" 狀態 " SortExpression="institution" />
              <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="roadkill_bulletin_detail.aspx?id={0}" HeaderText="詳細資料" Text="<i class='bi bi-eye-fill'></i>" Target="_blank" ItemStyle-Width="10%" />
	    </Columns>
	</asp:GridView>
	<asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select a.id , CONVERT (char(10), a.date, 126) AS date , CONVERT(CHAR(12), a.time, 114) AS time , a.highway_id , a.mileage  , a.reporter , a.institution , a.species , a.status , Case When a.file1 <> '' then a.path1+a.file1 Else file1 End file1 , a.path1 , a.owner From Roadkill_bulletin a Where 1=1">
	</asp:SqlDataSource>
        </div>
    </main>
    <!-- /.主要內容區塊 -->
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
