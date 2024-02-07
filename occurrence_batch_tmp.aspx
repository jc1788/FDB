<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="occurrence_batch_tmp.aspx.cs" Inherits="occurrence_batch_tmp" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">生態調查資料批次上傳</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">生態調查資料批次上傳</li>
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
        <!-- p class="my-3"><a href="javascript:window.close()">取消上傳</a></p -->
        <div class="row">
            <div class="col-12 col-lg-10 mx-auto ">

                    <asp:Table ID="Table4" runat="server" Width="1000">
                        <asp:TableRow HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2" Font-Size="10" HorizontalAlign="Center">

                                <asp:Label ID="ok_title" runat="server" Text="生態調查上傳成功資料"></asp:Label><br />

                                <asp:TextBox ID="ok_string" runat="server" Width="900" ForeColor="Red" BorderStyle="None" Rows="3"></asp:TextBox><br />

                                <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Chinese_name" HeaderText="物種名" SortExpression="Chinese_name" />
                                        <asp:BoundField DataField="short_name" HeaderText="拉丁學名" SortExpression="short_name" />
                                        <asp:BoundField DataField="Density" HeaderText="數量" SortExpression="Density" />
                                        <asp:BoundField DataField="date1" HeaderText="調查日期" SortExpression="date1" />
                                        <asp:BoundField DataField="x" HeaderText="經度" SortExpression="x" />
                                        <asp:BoundField DataField="y" HeaderText="緯度" SortExpression="y" />
                                        <asp:BoundField DataField="site_ch" HeaderText="調查地(地名)" SortExpression="site_ch" />
                                        <asp:BoundField DataField="Collector_ch" HeaderText="調查者中文名" SortExpression="Collector_ch" />
                                    </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="#ECFFEC" Font-Bold="True" CssClass="menu6" ForeColor="#035D00" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="PagerCss" VerticalAlign="Middle" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select * From table_2 Where uid = @uid">
                                   <SelectParameters>
                              <asp:SessionParameter Name="uid" SessionField="uid" DefaultValue="0" />
                             </SelectParameters>
                                </asp:SqlDataSource>
                                <br />

                                <asp:Button ID="Insert_RoadKill" runat="server" Cssclass="btn btn-primary" Text="確認上傳" OnClick="Insert_RoadKill_Click" AutoPostBack="false" />&nbsp;
                                <asp:Button ID="Delete_RoadKill_tmp" runat="server" Cssclass="btn btn-primary" Text="整批刪除" OnClick="Delete_RoadKill_tmp_Click" AutoPostBack="false" />
                                <br />
                                <br />

                                <asp:Label ID="error_title" runat="server" Text="生態調查上傳問題資料"></asp:Label>

                                <br />
                                <asp:TextBox ID="error_string" runat="server" Width="900" ForeColor="Red" BorderStyle="None" Rows="3"></asp:TextBox>

                                <br />


                                <asp:GridView ID="GridView_Error" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_Error" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="Chinese_name" HeaderText="物種名" SortExpression="Chinese_name" />
                                        <asp:BoundField DataField="short_name" HeaderText="拉丁學名" SortExpression="short_name" />
                                        <asp:BoundField DataField="Density" HeaderText="數量" SortExpression="Density" />
                                        <asp:BoundField DataField="date1" HeaderText="調查日期" SortExpression="date1" />
                                        <asp:BoundField DataField="x" HeaderText="經度" SortExpression="x" />
                                        <asp:BoundField DataField="y" HeaderText="緯度" SortExpression="y" />
                                        <asp:BoundField DataField="site_ch" HeaderText="調查地(地名)" SortExpression="site_ch" />
                                        <asp:BoundField DataField="Collector_ch" HeaderText="調查者中文名" SortExpression="Collector_ch" />
					<asp:BoundField DataField="errornote" HeaderText="錯誤訊息" SortExpression="errornote" />
                                    </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <HeaderStyle BackColor="#ECFFEC" Font-Bold="True" CssClass="menu6" ForeColor="#035D00" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="PagerCss" VerticalAlign="Middle" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <SortedAscendingCellStyle BackColor="#FAFAE7" />
                                    <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                                    <SortedDescendingCellStyle BackColor="#E1DB9C" />
                                    <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SDS_Error" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select * From table_3 Where uid = @uid">
                                    <SelectParameters>
                              <asp:SessionParameter Name="uid" SessionField="uid" DefaultValue="0" />
                             </SelectParameters>
                                </asp:SqlDataSource>

                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
            </div>
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
