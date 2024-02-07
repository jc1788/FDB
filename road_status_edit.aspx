<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="road_status_edit.aspx.cs" Inherits="road_status_edit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">路權空間資料</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">>路權空間資料</li>
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
                        role="tab" aria-controls="nav-home" aria-selected="true">資料編輯</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="road_status.aspx">取消編輯</a></p>
        <div class="row" ID="Table1" runat="server">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_highway_id" Cssclass="form-label" AssociatedControlId="highway_id" runat="server">國道里程：</asp:Label>
                            </th>
                            <td><asp:DropDownList ID="highway_id" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource1" DataTextField="highway_name" DataValueField="highway_id" autopostback="true"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT * FROM [Highway]"></asp:SqlDataSource>
				&nbsp;&nbsp;
				<asp:DropDownList ID="direction" runat="server" DataSourceID="SqlDataSource3" DataTextField="direction" DataValueField="direction" autopostback="True"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT [direction] FROM [Highway_direction] WHERE ([highway_id] = @highway_id)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="highway_id" Name="highway_id" PropertyName="SelectedValue" Type="string" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
				&nbsp;&nbsp;
                                        <asp:TextBox ID="range" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                        &nbsp;k+&nbsp;
                                        <asp:TextBox ID="milestone" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                        至
                                        <asp:TextBox ID="range2" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                        &nbsp;k+&nbsp;
                                        <asp:TextBox ID="milestone2" runat="server" Width="50" OnTextChanged="Get_Sensitive_level" autopostback="true" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_maintain" Cssclass="form-label" AssociatedControlId="maintain1" runat="server">維護單位：</asp:Label></th>
                            <td><asp:TextBox ID="maintain1" runat="server" Width="40%" />　
				<asp:TextBox ID="maintain2" runat="server" Width="40%" />
			    </td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_doc_no" Cssclass="form-label" AssociatedControlId="doc_no" runat="server">公文文號：</asp:Label></th>
                            <td><asp:TextBox ID="doc_no" Cssclass="form-control" runat="server" />
				<asp:TextBox ID="id" runat="server" Visible="false"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_location" Cssclass="form-label" AssociatedControlId="location" runat="server">位置：</asp:Label></th>
                            <td><asp:TextBox ID="location" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_area" Cssclass="form-label" AssociatedControlId="area" runat="server">出租使用面積(m2)：</asp:Label></th>
                            <td><asp:TextBox ID="area" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_plan_use" Cssclass="form-label" AssociatedControlId="plan_use" runat="server">計劃用途項目：</asp:Label></th>
                            <td><asp:TextBox ID="plan_use" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_sensitive" Cssclass="form-label" AssociatedControlId="sensitive" runat="server">敏感等級：</asp:Label></th>
                            <td><asp:TextBox ID="sensitive" Cssclass="form-control" runat="server" ReadOnly="true" Enabled="false" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_status" Cssclass="form-label" AssociatedControlId="status" runat="server">使用情形：</asp:Label></th>
                            <td><asp:DropDownList ID="status" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource2" DataTextField="status" DataValueField="status"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT status FROM [Use_Status] "></asp:SqlDataSource></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_institution" Cssclass="form-label" AssociatedControlId="institution" runat="server">使用單位：</asp:Label></th>
                            <td><asp:TextBox ID="institution" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_year" Cssclass="form-label" AssociatedControlId="year" runat="server">年度：</asp:Label></th>
                            <td><asp:TextBox ID="year" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_start_date" Cssclass="form-label" AssociatedControlId="start_date" runat="server">契約期間：</asp:Label></th>
                            <td><asp:TextBox ID="start_date" Cssclass="form-control" runat="server" TextMode="date" /></asp:TextBox>
				　－　<asp:TextBox ID="end_date" Cssclass="form-control" runat="server" TextMode="date" /></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_owner" Cssclass="form-label" AssociatedControlId="owner" runat="server">轄管單位：</asp:Label></th>
                            <td><asp:TextBox ID="owner" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_land1" Cssclass="form-label" AssociatedControlId="land1" runat="server">縣市：</asp:Label></th>
                            <td><asp:TextBox ID="land1" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_land2" Cssclass="form-label" AssociatedControlId="land2" runat="server">鄉鎮：</asp:Label></th>
                            <td><asp:TextBox ID="land2" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_land3" Cssclass="form-label" AssociatedControlId="land3" runat="server">段：</asp:Label></th>
                            <td><asp:TextBox ID="land3" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_land4" Cssclass="form-label" AssociatedControlId="land4" runat="server">地號：</asp:Label></th>
                            <td><asp:TextBox ID="land4" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_notes" Cssclass="form-label" AssociatedControlId="notes" runat="server">備註：</asp:Label></th>
                            <td><asp:TextBox ID="notes" Cssclass="form-control" runat="server" TextMode="MultiLine" Rows="3" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_file1" Cssclass="form-label" AssociatedControlId="file1" runat="server">相關文件(契約書)：</asp:Label></th>
                            <td><asp:FileUpload ID="file1" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                </table>
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
	    <asp:Button ID="Edit_Road_status" Cssclass="btn btn-primary" runat="server" OnClick="Edit_Road_status_Click" Text="修改資料" />
        </div>

    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
