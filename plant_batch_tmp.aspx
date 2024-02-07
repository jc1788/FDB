<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_batch_tmp.aspx.cs" Inherits="plant_batch_tmp" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">現況植栽調查批次上傳</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">現況植栽調查批次上傳</li>
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

                                <asp:Label ID="ok_title" runat="server" Text="植栽調查上傳成功資料"></asp:Label><br />

                                <asp:TextBox ID="ok_string" runat="server" Width="900" ForeColor="Red" BorderStyle="None" Rows="3"></asp:TextBox><br />

                                <asp:GridView ID="GridView_List" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_View" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="sid" HeaderText="流水號" ReadOnly="True" SortExpression="sid" />
                                        <asp:BoundField DataField="Segment" HeaderText="工務段" ReadOnly="True" SortExpression="Segment" />
                                        <asp:BoundField DataField="Highway_Name" HeaderText="國道編號" SortExpression="Highway_Name" />
                                        <asp:BoundField DataField="Direction" HeaderText="方向" SortExpression="Direction" />
                                        <asp:BoundField DataField="Start" HeaderText="里程(起)" SortExpression="Start" />
                                        <asp:BoundField DataField="End" HeaderText="里程(迄)" SortExpression="End" />
                                        <asp:BoundField DataField="Plant_Name" HeaderText="種類" SortExpression="Plant_Name" />
                                        <asp:BoundField DataField="Plant_Number" HeaderText="數量" SortExpression="Plant_Number" />
                                        <asp:BoundField DataField="Plant_Loca" HeaderText="位置" SortExpression="Plant_Loca" />
                                        <asp:BoundField DataField="Date" HeaderText="調查日期" SortExpression="Date" />
                                        <%-- <asp:BoundField DataField="weather" HeaderText="天氣" SortExpression="weather" />
                                <asp:BoundField DataField="animal" HeaderText="動物類群" SortExpression="animal" />
                                <asp:BoundField DataField="collecter_ch" HeaderText="紀錄者" SortExpression="collecter_ch" />
                                <asp:BoundField DataField="species" HeaderText="可能種類" SortExpression="species" />--%>
                                        <asp:TemplateField HeaderText="照片">
                                            <ItemTemplate>
                                                <asp:Image ID="img1" ImageUrl='<%#Eval("Img") %>' runat="server" AlternateText='<%#Eval("image_memo") %>' Width="64" Height="48" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="note" HeaderText="備註" SortExpression="note" />--%>
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
                                <asp:SqlDataSource ID="SDS_View" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select Plant_id , sid , Segment , Highway_Name , Direction , Start , End , Plant_Name , Plant_Number , Plant_Loca , Date ,  Case When Img <> '' Then '..'+path1+Img Else '' End As Img, Case When Img='' Then 'no data' Else '' End As image_memo From Plants_tmp Where uid = @uid">
                                   <SelectParameters>
                              <asp:SessionParameter Name="uid" SessionField="uid" DefaultValue="0" />
                             </SelectParameters>
                                </asp:SqlDataSource>
                                <br />

                                <asp:Button ID="Insert_RoadKill" runat="server" Cssclass="btn btn-primary" Text="確認上傳" OnClick="Insert_RoadKill_Click" AutoPostBack="false" />&nbsp;
                                <asp:Button ID="Delete_RoadKill_tmp" runat="server" Cssclass="btn btn-primary" Text="整批刪除" OnClick="Delete_RoadKill_tmp_Click" AutoPostBack="false" />
                                <br />
                                <br />

                                <asp:Label ID="error_title" runat="server" Text="植栽調查上傳問題資料"></asp:Label>

                                <br />
                                <asp:TextBox ID="error_string" runat="server" Width="900" ForeColor="Red" BorderStyle="None" Rows="3"></asp:TextBox>

                                <br />


                                <asp:GridView ID="GridView_Error" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" CellPadding="2" DataSourceID="SDS_Error" ForeColor="Black" GridLines="None" Width="100%" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" Font-Size="Medium" RowStyle-Height="30px" HeaderStyle-Height="30px" PageSize="20" BorderWidth="1px" BorderColor="Silver">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundField DataField="sid" HeaderText="流水號" ReadOnly="True" SortExpression="sid" />
                                        <asp:BoundField DataField="Segment" HeaderText="工務段" ReadOnly="True" SortExpression="Segment" />
                                        <asp:BoundField DataField="Highway_Name" HeaderText="國道編號" SortExpression="Highway_Name" />
                                        <asp:BoundField DataField="Direction" HeaderText="方向" SortExpression="Direction" />
                                        <asp:BoundField DataField="Start" HeaderText="里程(起)" SortExpression="Start" />
                                        <asp:BoundField DataField="End" HeaderText="里程(迄)" SortExpression="End" />
                                        <asp:BoundField DataField="Plant_Name" HeaderText="種類" SortExpression="Plant_Name" />
                                        <asp:BoundField DataField="Plant_Number" HeaderText="數量" SortExpression="Plant_Number" />
                                        <asp:BoundField DataField="Plant_Loca" HeaderText="位置" SortExpression="Plant_Loca" />
                                        <asp:BoundField DataField="Date" HeaderText="調查日期" SortExpression="Date" />
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
                                <asp:SqlDataSource ID="SDS_Error" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" Select Plantid , sid, Segment , Highway_Name , Direction ,  Start , [End] , Plant_Name ,  Plant_Number , Plant_Loca , Date , errornote  From Plants_error Where uid = @uid">
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
