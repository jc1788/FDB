<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="roadkill_batch.aspx.cs" Inherits="roadkill_batch" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">道路致死資料批次上傳</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">道路致死資料批次上傳</li>
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
                        role="tab" aria-controls="nav-home" aria-selected="true">批次上傳</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="javascript:window.close()">取消上傳</a></p>
        <div class="row">
            <div class="col-12 col-lg-10 mx-auto ">
                <asp:Table ID="Table1" runat="server" Width="100%" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="90%" VerticalAlign="Top">
                            <asp:Table ID="Table_Detail" runat="server" Width="90%" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="90%">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2">
                                        道路致死批次上傳
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="90%">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="90%">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table ID="Table2" runat="server" Width="80%" HorizontalAlign="Center" >

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="50%">
                                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Attachments/Sample/批次動物道路致死調查範例.xlsx" BorderStyle="None">批次動物道路致死調查範例</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell Width="50%">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Attachments/Sample/路殺批次上傳.pdf" BorderStyle="None">道路致死批次上傳教學</asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="50%">
                                                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Attachments/Sample/道路致死講習.pdf" BorderStyle="None">道路致死講習</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell Width="50%">
                                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Attachments/Sample/批次動物道路致死調查格式上傳說明.docx" BorderStyle="None">批次動物道路致死調查資料上傳說明</asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="50%">
                                                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/View/QA.aspx" BorderStyle="None">常見問題</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell Width="50%">
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="50%">
                                                    
                                                </asp:TableCell>
                                                <asp:TableCell Width="50%">
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="50%">
                                                    
                                                </asp:TableCell>
                                                <asp:TableCell Width="50%">
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>
                                             
                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                

                                
                                <asp:TableRow BackColor="#e0e0e0" Width="90%">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="30%">
                                        <asp:Label ID="Label1" runat="server" Text="新增調查資料："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="60%">
                                        <asp:FileUpload ID="file1" runat="server" Width="50%"/>
                                        <br/>
                                        檔案類型：excel
                                        <br/>
                                        說明：檔名請使用英文及數字，不接受空格及特殊符號。
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="90%">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="30%">
                                        <asp:Label ID="Label2" runat="server" Text="新增圖片檔案："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="60%">
                                        <asp:FileUpload ID="file2" runat="server" Width="50%"/>
                                        <br/>
                                        檔案類型：.zip, .rar
                                        <br/>
                                        說明：檔名請使用英文及數字，不接受空格及特殊符號，檔案大小勿超過100MB。
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="90%">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                
                                <asp:TableRow BackColor="#999999" Width="90%">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="30%">
                                    </asp:TableCell>
                                    <asp:TableCell Width="60%">
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

            </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
            <asp:Button ID="Upload_Data" runat="server" Cssclass="btn btn-primary" Text="上傳檔案" OnClick="Upload_Data_Click" AutoPostBack="false"/>
            <asp:Button ID="Month_NoRecord" runat="server" Cssclass="btn btn-primary" Text="本月無資料" OnClick="Upload_Month_NoRecord" AutoPostBack="false"/>
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
