<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="occurrence_edit.aspx.cs" Inherits="occurrence_edit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">生態調查資料編輯</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">生態調查資料編輯</li>
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
        <p class="my-3"><a href="javascript:window.close()">取消編輯</a></p>
        <div class="row">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Chinese_name" Cssclass="form-label" AssociatedControlId="Chinese_name" runat="server">物種中文名：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="Chinese_name" Cssclass="form-control" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_ScientificName" Cssclass="form-label" AssociatedControlId="ScientificName" runat="server">學名：</asp:Label>
			    </th>
                            <td><asp:TextBox ID="ScientificName" Cssclass="form-control" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Group" Cssclass="form-label" AssociatedControlId="Group" runat="server">物種類群：</asp:Label>
			    </th>
                            <td><asp:DropDownList ID="Group" runat="server" DataSourceID="SqlDataSource2" DataTextField="title" DataValueField="title"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT title FROM [Animal]"></asp:SqlDataSource>
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Density" Cssclass="form-label" AssociatedControlId="Density" runat="server">個體數(面積/密度)：</asp:Label></th>
                            <td><asp:TextBox ID="Density" Cssclass="form-control" runat="server" />
    				<asp:Label ID="lb_note1" runat="server" Text="(例如：穿越線)" Forecolor="Blue"/>
			    </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Way_ch" Cssclass="form-label" AssociatedControlId="Way_ch" runat="server">調查方法：</asp:Label></th>
                            <td><asp:TextBox ID="Way_ch" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_sec_record" Cssclass="form-label" AssociatedControlId="sec_record" runat="server">間接記錄：</asp:Label></th>
                            <td><asp:TextBox ID="sec_record" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Date" Cssclass="form-label" AssociatedControlId="date" runat="server">調查日期：</asp:Label></th>
                            <td><asp:TextBox ID="date" Cssclass="form-control" runat="server" TextMode="date"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_x" Cssclass="form-label" AssociatedControlId="x" runat="server">經度：</asp:Label></th>
                            <td><asp:TextBox ID="x" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_y" Cssclass="form-label" AssociatedControlId="y" runat="server">緯度：</asp:Label></th>
                            <td><asp:TextBox ID="y" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_inaccuracy" Cssclass="form-label" AssociatedControlId="inaccuracy" runat="server">座標誤差(公尺)：</asp:Label></th>
                            <td><asp:TextBox ID="inaccuracy" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_site_city" Cssclass="form-label" AssociatedControlId="site_city" runat="server">調查地(縣市)：</asp:Label></th>
                            <td><asp:TextBox ID="site_city" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_site_area" Cssclass="form-label" AssociatedControlId="site_area" runat="server">調查地(鄉鎮)：</asp:Label></th>
                            <td><asp:TextBox ID="site_area" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_site_ch" Cssclass="form-label" AssociatedControlId="site_ch" runat="server">調查地(中文)：</asp:Label></th>
                            <td><asp:TextBox ID="site_ch" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_highway_id" Cssclass="form-label" AssociatedControlId="highway_id" runat="server">調查路段：</asp:Label>
			    </th>
                            <td><asp:DropDownList ID="highway_id" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource1" DataTextField="highway_name" DataValueField="highway_id"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT * FROM [Highway]"></asp:SqlDataSource>
				</td>
                        </tr>

                        <tr>
                            <th scope="row"><asp:Label ID="Label_Direction" Cssclass="form-label" AssociatedControlId="Direction" runat="server">方向：</asp:Label></th>
                            <td><asp:DropDownList ID="Direction" runat="server">
                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="E" Value="E"></asp:ListItem>
                                            <asp:ListItem Text="W" Value="W"></asp:ListItem>
                                            <asp:ListItem Text="M" Value="M"></asp:ListItem>
                                     </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_mileage" Cssclass="form-label" AssociatedControlId="mileage" runat="server">參考里程(k)：</asp:Label></th>
                            <td><asp:TextBox ID="mileage" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_habit" Cssclass="form-label" AssociatedControlId="habit" runat="server">棲地類型：</asp:Label></th>
                            <td><asp:TextBox ID="habit" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Collector_ch" Cssclass="form-label" AssociatedControlId="Collector_ch" runat="server">調查者中文名：</asp:Label></th>
                            <td><asp:TextBox ID="Collector_ch" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_plan_name" Cssclass="form-label" AssociatedControlId="plan_name" runat="server">計畫名稱：</asp:Label></th>
                            <td><asp:TextBox ID="plan_name" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_notes" Cssclass="form-label" AssociatedControlId="notes" runat="server">備註：</asp:Label></th>
                            <td><asp:TextBox ID="notes" Cssclass="form-control" runat="server" TextMode="MultiLine" Rows="3" /></td>
                        </tr> 
                </table>
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
	    <a href="javascript:window.close()" class="btn btn-outline-primary me-md-2">取消</a>
	    <asp:Button ID="Edit_Occurrence" Cssclass="btn btn-primary" runat="server" OnClick="Edit_Occurrence_Click" Text="修改" />
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
