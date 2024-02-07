<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_observation_edit.aspx.cs" Inherits="plant_observation_edit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
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
    <main class="container" id="page">
        <div id="underlineTabs" class="mt-4">
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-search01"
                        role="tab" aria-controls="nav-home" aria-selected="true">資料修改</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="plant_observation.aspx">取消修改</a></p>
        <div class="row" ID="Table1" runat="server">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_chinese_name" Cssclass="form-label" AssociatedControlId="chinese_name" runat="server">中文名稱：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="chinese_name" Cssclass="form-control" runat="server" />
				<asp:TextBox ID="pid" runat="server" Visible="false" />
				<asp:TextBox ID="id" runat="server" Visible="false" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_ScientificName" Cssclass="form-label" AssociatedControlId="ScientificName" runat="server">學名：</asp:Label></th>
                            <td><asp:TextBox ID="ScientificName" Cssclass="form-control" runat="server" />
				<asp:TextBox ID="accepted_name_code" runat="server" Visible="False" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_high" Cssclass="form-label" AssociatedControlId="high" runat="server">高度(H)：</asp:Label></th>
                            <td><asp:TextBox ID="high" Cssclass="form-control" runat="server" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_width" Cssclass="form-label" AssociatedControlId="width" runat="server">幅寬(W)：</asp:Label></th>
                            <td><asp:TextBox ID="width" Cssclass="form-control" runat="server" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_m_high" Cssclass="form-label" AssociatedControlId="m_high" runat="server">米高徑：</asp:Label></th>
                            <td><asp:TextBox ID="m_high" Cssclass="form-control" runat="server" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_amount" Cssclass="form-label" AssociatedControlId="amount" runat="server">數量(株)：</asp:Label></th>
                            <td><asp:TextBox ID="amount" Cssclass="form-control" runat="server" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_highway_id" Cssclass="form-label" AssociatedControlId="highway_id" runat="server">國道編號：</asp:Label></th>
                            <td><asp:DropDownList ID="highway_id" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource3" DataTextField="highway_name" DataValueField="highway_id" AutoPostBack="true"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT highway_id , highway_name FROM [Highway] UNION Select Top 1 '' , '' From [Highway]"></asp:SqlDataSource></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_location" Cssclass="form-label" AssociatedControlId="location" runat="server">位置：</asp:Label></th>
                            <td><asp:DropDownList ID="location" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SDS_Location" DataTextField="Location" DataValueField="Location"></asp:DropDownList>
				<asp:SqlDataSource ID="SDS_Location" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Distinct Location From Plant_observation"></asp:SqlDataSource></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_direction" Cssclass="form-label" AssociatedControlId="direction" runat="server">方向：</asp:Label></th>
                            <td><asp:DropDownList ID="direction" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SDS_Direction" DataTextField="direction" DataValueField="direction"></asp:DropDownList>
				<asp:SqlDataSource ID="SDS_Direction" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="Select Distinct direction From Highway_direction UNION Select TOP 1 '' From Highway_direction Order By direction"></asp:SqlDataSource>
				<asp:Label ID="lab_direction" runat="server" Text="（直向國道方向請填N、S、R，橫向國道請方向填E、W、R）" Forecolor="Red"/></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_range1" Cssclass="form-label" AssociatedControlId="range1" runat="server">里程：</asp:Label></th>
                            <td>
                               <asp:TextBox ID="range1" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
                               &nbsp;K+&nbsp;
                               <asp:TextBox ID="milestone1" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
                               &nbsp;至&nbsp;
                               &nbsp;&nbsp;
                               <asp:TextBox ID="range2" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
                               &nbsp;K+&nbsp;
                               <asp:TextBox ID="milestone2" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
			    </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_plant_class" Cssclass="form-label" AssociatedControlId="plant_class" runat="server">植栽類別：</asp:Label></th>
                            <td><asp:TextBox ID="plant_class" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_plant_type" Cssclass="form-label" AssociatedControlId="plant_type" runat="server">植栽方式：</asp:Label></th>
                            <td><asp:TextBox ID="plant_type" Cssclass="form-control" runat="server" /></td>
                        </tr>
                </table>
	    <asp:Button ID="Edit_Plant_observation" Cssclass="btn btn-primary" runat="server" OnClick="Edit_Plant_observation_Click" Text="編輯植栽工程資料" />
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
        </div>

    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
