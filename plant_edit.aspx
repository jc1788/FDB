<%@ Page Language="C#" MasterPageFile="front.master" AutoEventWireup="true" CodeFile="plant_edit.aspx.cs" Inherits="plant_edit" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!--/.主導覽 -->
    <!-- 標題+麵包屑 -->
    <div class="bg-gradient-primary">
        <div class="container">
            <div class="d-flex justify-content-between">
                <h4 class="py-2 h4 text-white">現況植栽資料編輯</h4>
                <nav class="breadcrumbNav  d-none d-md-block d-lg-block d-xl-block"
                    style="--bs-breadcrumb-divider: url(&#34;data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E&#34;);"
                    aria-label="breadcrumb">
                    <ol class="breadcrumb mt12">
                        <li class="breadcrumb-item"><a href="index.aspx" class="whiteLink">首頁</a></li>
                        <li class="breadcrumb-item active" aria-current="page">現況植栽資料編輯</li>
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
                            <th scope="row"><asp:Label ID="Label_Office" Cssclass="form-label" AssociatedControlId="Office" runat="server">養護工程分局：</asp:Label>
                            </th>
                            <td><asp:TextBox ID="Office" Cssclass="form-control" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Segment" Cssclass="form-label" AssociatedControlId="Segment" runat="server">工務段：</asp:Label>
			    </th>
                            <td><asp:TextBox ID="Segment" Cssclass="form-control" runat="server" />
                                </td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Highway_Name" Cssclass="form-label" AssociatedControlId="Highway_Name" runat="server">調查路段：</asp:Label>
			    </th>
                            <td><asp:DropDownList ID="Highway_Name" Cssclass="form-select-inline" aria-label="Default select example" runat="server" DataTextField="highway_name" DataValueField="highway_id" OnSelectedIndexChanged="Get_XY" AutoPostBack="true"></asp:DropDownList>
				<asp:Label ID="lb_Direction" Cssclass="form-label-inline" AssociatedControlId="Direction" runat="server">方向：</asp:Label>
				<asp:TextBox ID="Direction" Cssclass="form-control-inline" runat="server" Width="30" ></asp:TextBox>
				<br><asp:Label ID="lb_start1" Cssclass="form-label-inline" AssociatedControlId="start1" runat="server">起始里程：</asp:Label>
				<asp:TextBox ID="start1" Cssclass="form-control-inline" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" OnTextChanged="Get_XY" AutoPostBack="true"></asp:TextBox>
				&nbsp;k+&nbsp;
				<asp:TextBox ID="start2" Cssclass="form-control-inline" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" OnTextChanged="Get_XY" AutoPostBack="true"></asp:TextBox><br>
				<br><asp:Label ID="lb_end1" Cssclass="form-label-inline" AssociatedControlId="end1" runat="server">結束里程：</asp:Label>
				<asp:TextBox ID="end1" Cssclass="form-control-inline" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" OnTextChanged="Get_XY" AutoPostBack="true"></asp:TextBox>
				&nbsp;k+&nbsp;
				<asp:TextBox ID="end2" Cssclass="form-control-inline" runat="server" Width="50" Text="0" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" OnTextChanged="Get_XY" AutoPostBack="true"></asp:TextBox><br>
				<br><asp:Label ID="lb_note1" runat="server" Text="（直向國道方向請填N、S、R，橫向國道請方向填E、W、R）" Forecolor="Blue"/>
				</td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_PointY" Cssclass="form-label" AssociatedControlId="PointY" runat="server">起始緯度：</asp:Label></th>
                            <td><asp:TextBox ID="PointY" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_PointX" Cssclass="form-label" AssociatedControlId="PointX" runat="server">起始經度：</asp:Label></th>
                            <td><asp:TextBox ID="PointX" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_PointYE" Cssclass="form-label" AssociatedControlId="PointYE" runat="server">結束緯度：</asp:Label></th>
                            <td><asp:TextBox ID="PointYE" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_PointXE" Cssclass="form-label" AssociatedControlId="PointXE" runat="server">結束經度：</asp:Label></th>
                            <td><asp:TextBox ID="PointXE" Cssclass="form-control" runat="server" onkeyup="this.value=this.value.replace(/[^0-9\.]/g,'')" Text="0.0" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Date" Cssclass="form-label" AssociatedControlId="date" runat="server">調查日期：</asp:Label></th>
                            <td><asp:TextBox ID="date" Cssclass="form-control" runat="server" TextMode="date"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Plant_Name" Cssclass="form-label" AssociatedControlId="Plant_Name" runat="server">種類：</asp:Label></th>
                            <td><asp:TextBox ID="Plant_Name" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Plant_Number" Cssclass="form-label" AssociatedControlId="Plant_Number" runat="server">數量：</asp:Label></th>
                            <td><asp:TextBox ID="Plant_Number" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Unit2" Cssclass="form-label" AssociatedControlId="Unit2" runat="server">單位：</asp:Label></th>
                            <td><asp:TextBox ID="Unit2" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_SpecificationTall" Cssclass="form-label" AssociatedControlId="SpecificationTall" runat="server">高度(cm)：</asp:Label></th>
                            <td><asp:TextBox ID="SpecificationTall" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_SpecificationCrown" Cssclass="form-label" AssociatedControlId="SpecificationCrown" runat="server">冠徑(cm)：</asp:Label></th>
                            <td><asp:TextBox ID="SpecificationCrown" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_SpecificationMeter" Cssclass="form-label" AssociatedControlId="SpecificationMeter" runat="server">米徑(cm)：</asp:Label></th>
                            <td><asp:TextBox ID="SpecificationMeter" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_LifeStyle" Cssclass="form-label" AssociatedControlId="LifeStyle" runat="server">型態：</asp:Label></th>
                            <td><asp:TextBox ID="LifeStyle" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_florescence" Cssclass="form-label" AssociatedControlId="florescence" runat="server">花期(月份)：</asp:Label></th>
                            <td><asp:TextBox ID="florescence" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_FlowerColor" Cssclass="form-label" AssociatedControlId="FlowerColor" runat="server">花色：</asp:Label></th>
                            <td><asp:TextBox ID="FlowerColor" Cssclass="form-control" runat="server" /></td>
                        </tr>
                        <tr>
                            <th scope="row"><asp:Label ID="Label_FruitPeriod" Cssclass="form-label" AssociatedControlId="FruitPeriod" runat="server">果期(月份)：</asp:Label></th>
                            <td><asp:TextBox ID="FruitPeriod" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="Label_LeafColor" Cssclass="form-label" AssociatedControlId="LeafColor" runat="server">葉色：</asp:Label></th>
                            <td><asp:TextBox ID="LeafColor" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="Label_LeafPeriod" Cssclass="form-label" AssociatedControlId="LeafPeriod" runat="server">葉色轉變期(月份)：</asp:Label></th>
                            <td><asp:TextBox ID="LeafPeriod" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Plant_Loca" Cssclass="form-label" AssociatedControlId="Plant_Loca" runat="server">位置：</asp:Label></th>
                            <td><asp:TextBox ID="Plant_Loca" Cssclass="form-control" runat="server" /></td>
                        </tr> 
                        <tr>
                            <th scope="row"><asp:Label ID="Label_Note" Cssclass="form-label" AssociatedControlId="Note" runat="server">備註：</asp:Label></th>
                            <td><asp:TextBox ID="Note" Cssclass="form-control" runat="server" TextMode="MultiLine" Rows="3" /></td>
                        </tr> 
                </table>
               </div>
        </div>
        <div class="d-grid gap-2 col-12  col-md-2 mx-auto my-4 d-md-block">
	    <a href="javascript:window.close()" class="btn btn-outline-primary me-md-2">取消</a>
	    <asp:Button ID="Edit_Plant" Cssclass="btn btn-primary" runat="server" OnClick="Edit_Plant_Click" Text="修改" />
        </div>
    </main>
</asp:Content>

<asp:Content ID="FScript" ContentPlaceHolderID="FScriptContentPlaceHolder" runat="Server">
</asp:Content>
