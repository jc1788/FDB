<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_eng_edit.aspx.cs" Inherits="plant_eng_edit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
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
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-search01"
                        role="tab" aria-controls="nav-home" aria-selected="true">資料修改</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="plant_eng.aspx">取消修改</a></p>
        <div class="row" ID="Table1" runat="server">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_plant_id" Cssclass="form-label" AssociatedControlId="plant_id" runat="server">編號：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="plant_id" Cssclass="form-control" runat="server" />
				<asp:TextBox ID="pid" runat="server" Visible="False"/>
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_number" Cssclass="form-label" AssociatedControlId="number" runat="server">標別：</asp:Label></th>
                            <td><asp:TextBox ID="number" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_plant_name" Cssclass="form-label" AssociatedControlId="plant_name" runat="server">計畫名稱：</asp:Label></th>
                            <td><asp:TextBox ID="plant_name" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_institution" Cssclass="form-label" AssociatedControlId="institution" runat="server">設計單位：</asp:Label></th>
                            <td><asp:TextBox ID="institution" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_design_date" Cssclass="form-label" AssociatedControlId="design_date" runat="server">設計定稿日期：</asp:Label></th>
                            <td><asp:TextBox ID="design_date" Cssclass="form-control" runat="server" TextMode="date" /></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_work_date" Cssclass="form-label" AssociatedControlId="work_date" runat="server">竣工日期：</asp:Label></th>
                            <td><asp:TextBox ID="work_date" Cssclass="form-control" runat="server" TextMode="date" /></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_maintain" Cssclass="form-label" AssociatedControlId="maintain" runat="server">維護單位：</asp:Label></th>
                            <td><asp:TextBox ID="maintain" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                </table>
	    <asp:Button ID="Edit_Plant_engineering" Cssclass="btn btn-primary" runat="server" OnClick="Edit_Plant_engineering_Click" Text="修改植栽工程資料" />
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
        </div>

    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
