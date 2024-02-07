<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="roadkill_bulletin_detail.aspx.cs" Inherits="roadkill_bulletin_detail" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
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
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-search01"
                        role="tab" aria-controls="nav-home" aria-selected="true">詳細資料</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="javascript:window.close()">回上一頁</a></p>
        <div class="row">
            <div class="col-12 col-lg-12">
                <table class="mb-4 itemTable">
                    <tbody>
                        <tr>
                            <th scope="row">國道編號：</th>
                            <td><asp:Literal ID="highway_id" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">里程：</th>
                            <td><asp:Literal ID="mileage" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">發現日期：</th>
                            <td><asp:Literal ID="date" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">發現時間：</th>
                            <td><asp:Literal ID="time" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">可能種類：</th>
                            <td><asp:Literal ID="species" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">通報單位：</th>
                            <td><asp:Literal ID="institution" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">通報者：</th>
                            <td><asp:Literal ID="reporter" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">狀態：</th>
                            <td><asp:Literal ID="status" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <th scope="row">照片：</th>
                            <td><asp:image id="file1" runat="server" ></asp:image></td>
                        </tr>
                </table>
            </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4">
            <a href="javascript:window.close()" class="btn btn-outline-secondary">回上一頁</a>
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
