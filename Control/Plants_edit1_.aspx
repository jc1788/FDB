﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plants_edit1_.aspx.cs" Inherits="Control_Plants_edit1" %>

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

        .PagerCss TD A:hover {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD A:active {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD A:link {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD A:visited {
            FONT-SIZE: 20px;
            WIDTH: 20px;
            COLOR: black;
            padding-left: 4px;
            padding-right: 4px;
        }

        .PagerCss TD SPAN {
            FONT-WEIGHT: bold;
            FONT-SIZE: 24px;
            WIDTH: 20px;
            COLOR: red;
            padding-left: 4px;
            padding-right: 4px;
        }
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
                                 <a href="../newdefault.aspx" class="menu4">單筆修改</a> &gt;
                                 <a href="../Control/Plants_edit1.aspx" class="menu4">植栽調查</a>
                             </div>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow ID="Row_GridView">
                <asp:TableCell VerticalAlign="Top">
                    <asp:Table ID="Table3" runat="server" Width="1000">

                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                                <asp:Table ID="Table4" runat="server">

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000" BackColor="#035D00">
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label ID="Label_Keywords" runat="server" Text="查詢關鍵字 : " Visible="true"></asp:Label>
                                            <asp:TextBox ID="Keywords" runat="server" AutoPostBack="True" Visible="true"></asp:TextBox>
                                            <asp:Button ID="Button_Query" runat="server" Text="查詢" OnClick="QueryData" />
                                        </asp:TableCell>
                                    </asp:TableRow>

                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" Width="1000">
                                            <asp:GridView ID="GridView_List"
                                                runat="server"
                                                DataSourceID="SqlDataSource1"
                                                AllowPaging="True"
                                                AllowSorting="True"
                                                AutoGenerateColumns="False"
                                                BackColor="LightGoldenrodYellow"
                                                CellPadding="2"
                                                ForeColor="Black"
                                                GridLines="None"
                                                Width="100%"
                                                RowStyle-HorizontalAlign="Center"
                                                RowStyle-VerticalAlign="Middle"
                                                Font-Size="Medium"
                                                RowStyle-Height="30px"
                                                HeaderStyle-Height="30px"
                                                PageSize="20"
                                                BorderWidth="1px"
                                                BorderColor="Silver"
                                                DataKeyNames="Plantid">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>

                                                    <asp:HyperLinkField DataNavigateUrlFields="Plantid" DataNavigateUrlFormatString="Plants_edit2_.aspx?Plantid={0}&mode=e" HeaderText="編輯" Target="_self" Text="編輯" />

                                                    <asp:TemplateField HeaderText="刪除">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CssClass="btnlink" CommandName="Delete" Text="刪除" OnClientClick="if (confirm('是否刪除?') == false) return false;"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Plantid" HeaderText="流水號" ReadOnly="True" SortExpression="Plantid" />
                                                    <asp:BoundField DataField="Segment" HeaderText="工務段" ReadOnly="True" SortExpression="Segment" />
                                                    <asp:BoundField DataField="Highway_Name" HeaderText="國道編號" SortExpression="Highway_Name" />
                                                    <asp:BoundField DataField="Direction" HeaderText="方向" SortExpression="Direction" />
                                                    <asp:BoundField DataField="Start" HeaderText="里程(起)" SortExpression="Start" />
                                                    <asp:BoundField DataField="End" HeaderText="里程(迄)" SortExpression="End" />
                                                    <asp:BoundField DataField="Plant_Name" HeaderText="種類" SortExpression="Plant_Name" />
                                                    <asp:BoundField DataField="Plant_Number" HeaderText="數量" SortExpression="Plant_Number" />
                                                    <asp:BoundField DataField="Plant_Loca" HeaderText="位置" SortExpression="Plant_Loca" />
                                                    <asp:BoundField DataField="Date" HeaderText="調查日期" SortExpression="Date" />
                                                    <asp:TemplateField HeaderText="照片" SortExpression="Img">
                                                        <ItemTemplate>
                                                            <asp:Image ID="img1" ImageUrl='<%#Eval("Img") %>' runat="server" AlternateText='<%#Eval("image_memo") %>' Width="64" Height="48" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:freewayDbConnectionString %>" SelectCommand=" SELECT [PlantId], [Segment], [Highway_Name], [Direction], [Start], [End], [Plant_Name], [Plant_Number], [Plant_Loca], [Date], Case When Img <> '' Then '../Attachments/Plants/'+Img Else '' End As Img, Case When Img='' Then 'no data' Else '' End As image_memo FROM [Plants] " DeleteCommand="DELETE FROM [Plants] WHERE [PlantId] = @PlantId">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="PlantId" Type="Int32" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <br />
                                            <asp:DropDownList ID="uxGoToPageDropDownList" runat="server"
                                                OnSelectedIndexChanged="uxGoToPageDropDownList_SelectedIndexChanged"
                                                OnDataBinding="uxGoToPageDropDownList_DataBinding" AutoPostBack="True">
                                            </asp:DropDownList>

                                        </asp:TableCell>

                                    </asp:TableRow>


                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>


                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">

                    <asp:Table ID="Table5" runat="server" Width="1000" HorizontalAlign="Center" BackImageUrl="~/Images/frame/02.jpg" Height="70">
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

