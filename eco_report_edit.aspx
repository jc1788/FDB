<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="eco_report_edit.aspx.cs" Inherits="eco_report_edit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
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
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-search01"
                        role="tab" aria-controls="nav-home" aria-selected="true">資料修改</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="eco_report.aspx">取消修改</a></p>
        <div class="row">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_plan_name" Cssclass="form-label" AssociatedControlId="plan_name" runat="server">計畫名稱：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="plan_name" Cssclass="form-control" runat="server" />
                                <asp:TextBox ID="id" runat="server" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_plan_id" Cssclass="form-label" AssociatedControlId="plan_id" runat="server">計畫編號：</asp:Label></th>
                            <td><asp:TextBox ID="plan_id" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_type" Cssclass="form-label" AssociatedControlId="type" runat="server">設計單位：</asp:Label></th>
                            <td><asp:DropDownList ID="type" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource1" DataTextField="Report_Type" DataValueField="Report_Type"></asp:DropDownList>
			    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" SELECT * FROM [Report_Type] WHERE ID = 3 "></asp:SqlDataSource></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Date" Cssclass="form-label" AssociatedControlId="start_date" runat="server">調查日期：</asp:Label></th>
                            <td><asp:TextBox ID="start_date" Cssclass="form-control" runat="server" TextMode="date" />
				　－　<asp:TextBox ID="end_date" Cssclass="form-control" runat="server" TextMode="date" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_owner" Cssclass="form-label" AssociatedControlId="owner" runat="server">計畫主持人：</asp:Label></th>
                            <td><asp:TextBox ID="owner" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_provider" Cssclass="form-label" AssociatedControlId="provider" runat="server">資料提供者：</asp:Label></th>
                            <td><asp:TextBox ID="provider" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_location" Cssclass="form-label" AssociatedControlId="location" runat="server">調查地點：</asp:Label></th>
                            <td><asp:TextBox ID="location" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_keywords" Cssclass="form-label" AssociatedControlId="keywords" runat="server">關鍵字：</asp:Label></th>
                            <td><asp:TextBox ID="keywords" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_abstracts" Cssclass="form-label" AssociatedControlId="abstracts" runat="server">摘要：</asp:Label></th>
                            <td><asp:TextBox ID="abstracts" Cssclass="form-control" TextMode="MultiLine" Rows="5" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_file1" Cssclass="form-label" AssociatedControlId="file1" runat="server">全文：</asp:Label></th>
                            <td><asp:FileUpload ID="file1" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                </table>
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
	    <asp:Button ID="Edit_Eco_report" Cssclass="btn btn-primary" runat="server" OnClick="Edit_Eco_report_Click" Text="修改資料" />
        </div>

    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
