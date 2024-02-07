<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="roadkill_add.aspx.cs" Inherits="roadkill_add" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">道路致死資料新增</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">道路致死資料新增</li>
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
        <p class="my-3"><a href="javascript:window.close()">取消新增</a></p>
        <div class="row">
            <div class="col-12 col-lg-8 mx-auto ">
                <table class="mb-4 itemTable itemTableEdit">
                    <tbody>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_site_ch" Cssclass="form-label" AssociatedControlId="site_ch" runat="server">工務段：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="site_ch" Cssclass="form-control" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_highway_id" Cssclass="form-label" AssociatedControlId="highway_id" runat="server">調查路段：</asp:Label>
			    </th>
                            <td><asp:DropDownList ID="highway_id" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataTextField="highway_name" DataValueField="highway_id" OnSelectedIndexChanged="Get_XY" AutoPostBack="true"></asp:DropDownList>
				<asp:Label ID="lb_direction" Cssclass="form-label-inline" AssociatedControlId="direction" runat="server">方向：</asp:Label>
				<asp:TextBox ID="direction" Cssclass="form-control-inline" runat="server" Width="30" ></asp:TextBox>
				<asp:Label ID="lb_range1" Cssclass="form-label-inline" AssociatedControlId="range1" runat="server">里程：</asp:Label>
				<asp:TextBox ID="range1" Cssclass="form-control-inline" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" OnTextChanged="Get_XY" AutoPostBack="true"></asp:TextBox>
				&nbsp;k+&nbsp;
				<asp:TextBox ID="milestone" Cssclass="form-control-inline" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" OnTextChanged="Get_XY" AutoPostBack="true"></asp:TextBox><br>
				<asp:Label ID="lb_note1" runat="server" Text="（直向國道方向請填N、S、R，橫向國道請方向填E、W、R）" Forecolor="Blue"/>
				</td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_y" Cssclass="form-label" AssociatedControlId="y" runat="server">緯度：</asp:Label></th>
                            <td><asp:TextBox ID="y" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox><asp:TextBox ID="TM2_Y" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_x" Cssclass="form-label" AssociatedControlId="y" runat="server">經度：</asp:Label></th>
                            <td><asp:TextBox ID="x" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox><asp:TextBox ID="TM2_X" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_date" Cssclass="form-label" AssociatedControlId="date" runat="server">調查日期：</asp:Label></th>
                            <td><asp:TextBox ID="date" Cssclass="form-control" runat="server" TextMode="date"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_range" Cssclass="form-label" AssociatedControlId="range" runat="server">範圍：</asp:Label></th>
                            <td><asp:TextBox ID="range" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_type" Cssclass="form-label" AssociatedControlId="type" runat="server">工作類別：</asp:Label></th>
                            <td><asp:TextBox ID="type" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_weather" Cssclass="form-label" AssociatedControlId="weather" runat="server">天氣：</asp:Label></th>
                            <td><asp:TextBox ID="weather" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_animal" Cssclass="form-label" AssociatedControlId="animal" runat="server">大類：</asp:Label></th>
                            <td><asp:TextBox ID="animal" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_detail_animal" Cssclass="form-label" AssociatedControlId="detail_animal" runat="server">詳細類群：</asp:Label></th>
                            <td><asp:TextBox ID="detail_animal" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_animal2" Cssclass="form-label" AssociatedControlId="animal2" runat="server">動物類群：</asp:Label></th>
                            <td><asp:TextBox ID="animal2" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_Collector_ch" Cssclass="form-label" AssociatedControlId="Collector_ch" runat="server">調查者：</asp:Label></th>
                            <td><asp:TextBox ID="Collector_ch" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_species" Cssclass="form-label" AssociatedControlId="species" runat="server">可能種類：</asp:Label></th>
                            <td><asp:TextBox ID="species" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_deduce_species" Cssclass="form-label" AssociatedControlId="deduce_species" runat="server">可能種類推測：</asp:Label></th>
                            <td><asp:TextBox ID="deduce_species" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="lb_file1" Cssclass="form-label" AssociatedControlId="file1" runat="server">上傳照片：</asp:Label></th>
                            <td><asp:FileUpload ID="file1" Cssclass="form-control" runat="server" AllowMultiple="true"/><br>(至多五張照片，超過取前五張記錄)</td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_transfer" Cssclass="form-label" AssociatedControlId="transfer" runat="server">轉送：</asp:Label></th>
                            <td><asp:TextBox ID="transfer" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="lb_note" Cssclass="form-label" AssociatedControlId="note" runat="server">備註：</asp:Label></th>
                            <td><asp:TextBox ID="note" Cssclass="form-control" runat="server" TextMode="MultiLine" Rows="3" /></td>
                        </tr> 
                </table>
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
	    <a href="javascript:window.close()" class="btn btn-outline-primary me-md-2">取消</a>
	    <asp:Button ID="Add_Roadkill" Cssclass="btn btn-primary" runat="server" OnClick="Add_Roadkill_Click" Text="新增" />
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
