﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plants_tmp_.aspx.cs" Inherits="Control_Plants_tmp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        <!--
        body {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 00px;
            background-image: url();
            background-repeat: repeat;
            background-color: #F7F7F7;
        }
        -->
    </style>

    <link href="../Content/style1.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
<!--
    function MM_preloadImages() { //v3.0
        var d = document; if (d.images) {
            if (!d.MM_p) d.MM_p = new Array();
            var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
        }
    }
    function MM_swapImgRestore() { //v3.0
        var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
    }
    function MM_findObj(n, d) { //v4.01
        var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
            d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
        }
        if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
        for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
        if (!x && d.getElementById) x = d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
            if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
    }

    function swap_data(rowid) { //v3.0
        if (rowid == 'data1') {
            document.data1.Visible = "true";
            document.data2.Visible = "false";
            document.data3.Visible = "false";
        }
    }
    //-->
    </script>
     <link href="fun01_011.aspx_files/style1.css" rel="stylesheet" type="text/css" />
    <script src="fun01_011.aspx_files/WebResource.js" type="text/javascript"></script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:Table ID="Table_Title" runat="server" Width="1000" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell>
                    <a href="../newdefault.aspx">
                        <asp:Image ID="Image1" runat="server" ImageUrl="fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
                    </a>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell BackColor="#F7F7F7">
                    <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                        <asp:TableRow HorizontalAlign="Right">
                            <asp:TableCell>
                             <div class="menu4">
                                 <a href="../newdefault.aspx" class="menu4">首頁</a> &gt;  
                                 <a href="../Control/Batch_Upload_Plants_.aspx" class="menu4">批次上傳</a>  
                                 
                             </div>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>



            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">

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

                                <asp:Button ID="Insert_RoadKill" runat="server" Text="確認上傳" OnClick="Insert_RoadKill_Click" AutoPostBack="false" />
                                <asp:Button ID="Delete_RoadKill_tmp" runat="server" Text="整批刪除" OnClick="Delete_RoadKill_tmp_Click" AutoPostBack="false" />
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
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow VerticalAlign="Top" BackColor="#a7a7a7">
                <asp:TableCell BackColor="#a7a7a7">
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">

                    <asp:Table ID="Table3" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
                        <asp:TableRow HorizontalAlign="Center">
                            <asp:TableCell ColumnSpan="2" Font-Size="10" HorizontalAlign="Center" ForeColor="White">
                            <br/>
                            <div >
                                            總機：(02)2909-6141 (02)2909-6141 地址：24303新北市泰山區黎明里半山雅70號<br/>
                                    交通部高速公路局 <br/>
                                版權所有 最佳瀏覽環境：IE 6.0、FireFox 2.0 以上版本．本網站最佳瀏覽解析度為 1024*768
                            </div>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>


        </asp:Table>


    </form>
</body>
</html>
