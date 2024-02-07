<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_eng_add.aspx.cs" Inherits="plant_eng_add" %>

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
                        role="tab" aria-controls="nav-home" aria-selected="true">資料新增</a>
                </div>
            </nav>
        </div>
        <p class="my-3"><a href="plant_eng.aspx">取消新增</a></p>
        <div class="row" ID="Table1" runat="server">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_plant_id" Cssclass="form-label" AssociatedControlId="plant_id" runat="server">編號：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="plant_id" Cssclass="form-control" runat="server" OnTextChanged="Get_Plant_Id" autopostback="true" />
				<asp:TextBox ID="pid" runat="server" Visible="False"/>
				<asp:Image ID="check_ok" runat="server" ImageUrl="~/Images/check_ok.ico" Width="0" Height="0"/>
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
                            <th scope="row"><asp:Label ID="lb_maintain" Cssclass="form-label" AssociatedControlId="DropDownList1" runat="server">維護單位：</asp:Label></th>
                            <td><asp:DropDownList ID="DropDownList1" Cssclass="form-select-inline" aria-label="Default select example" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="class1" DataValueField="class1"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT 0 AS id , 'ALL' AS  class1 UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '北區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '中區養護工程分局' UNION ALL Select Top 1 id , class1 FROM Engineering_road Where class1 = '南區養護工程分局' ORDER BY id"></asp:SqlDataSource>
				<asp:DropDownList ID="DropDownList2" Cssclass="form-select-inline" aria-label="Default select example" runat="server" AppendDataBoundItems="False" AutoPostBack="True" DataSourceID="SqlDataSource2" DataTextField="class2" DataValueField="class2"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT DISTINCT class2 FROM [Engineering_road] Where (class1=@class1) ORDER BY class2">
				    <SelectParameters><asp:ControlParameter ControlID="DropDownList1" Name="class1" PropertyName="SelectedValue" Type="string" /></SelectParameters>
				</asp:SqlDataSource>
			    </td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_file1" Cssclass="form-label" AssociatedControlId="file1" runat="server">竣工圖：</asp:Label></th>
                            <td><asp:FileUpload ID="file1" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                </table>
	    <asp:Button ID="Add_Plant_engineering" Cssclass="btn btn-primary" runat="server" OnClick="Add_Plant_engineering_Click" Text="新增植栽工程資料" />
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
        </div>

        <div class="row" id="Table2" runat="server" Visible="false">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_chinese_name" Cssclass="form-label" AssociatedControlId="Chinese_name" runat="server">植物中文名：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="Chinese_name" Cssclass="form-control" runat="server" OnTextChanged="Get_NameFull" autopostback="true" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_ScientificName" Cssclass="form-label" AssociatedControlId="ScientificName" runat="server">植物學名：</asp:Label></th>
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
                            <th scope="row"><asp:Label ID="lb_m_high" Cssclass="form-label" AssociatedControlId="m_high" runat="server">米高徑(∮)：</asp:Label></th>
                            <td><asp:TextBox ID="m_high" Cssclass="form-control" runat="server" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_amount" Cssclass="form-label" AssociatedControlId="amount" runat="server">數量(株)：</asp:Label></th>
                            <td><asp:TextBox ID="amount" Cssclass="form-control" runat="server" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_highway_id" Cssclass="form-label" AssociatedControlId="highway_id" runat="server">栽種里程：</asp:Label></th>
                            <td><asp:DropDownList ID="highway_id" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource3" DataTextField="highway_name" DataValueField="highway_id" AutoPostBack="true"></asp:DropDownList>
				<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT * FROM [Highway]"></asp:SqlDataSource>
				&nbsp;&nbsp;
				<asp:DropDownList ID="direction" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataSourceID="SqlDataSource4" DataTextField="direction" DataValueField="direction"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand="SELECT [direction] FROM [Highway_direction] WHERE ([highway_id] = @highway_id)">
                                    <SelectParameters><asp:ControlParameter ControlID="highway_id" Name="highway_id" PropertyName="SelectedValue" Type="string" /></SelectParameters>
                                </asp:SqlDataSource>
				&nbsp;&nbsp;
				<asp:TextBox ID="range1" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
				&nbsp;K+&nbsp;
				<asp:TextBox ID="milestone1" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
				&nbsp;至&nbsp;
				<asp:TextBox ID="range2" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
				 &nbsp;K+&nbsp;
				<asp:TextBox ID="milestone2" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')"></asp:TextBox>
			    </td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_location" Cssclass="form-label" AssociatedControlId="location" runat="server">位置：</asp:Label></th>
                            <td><asp:TextBox ID="location" Cssclass="form-control" runat="server" /></td>
                        </tr>
                </table>
	    <asp:Button ID="Add_Plant_observation" Cssclass="btn btn-primary" runat="server" OnClick="Add_Plant_observation_Click" Text="新增植栽資料" />
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
