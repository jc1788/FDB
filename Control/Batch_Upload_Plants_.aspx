<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Batch_Upload_Plants_.aspx.cs" Inherits="Control_Batch_Upload_Plants" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                <a href="../newdefault.aspx" >
                    <asp:Image ID="Image1" runat="server" ImageUrl="fun01_011.aspx_files/top1.jpg" BorderStyle="None" />
                </a>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7">
                <asp:Table ID="Table_Menu" runat="server" Width="900" HorizontalAlign="Center">
                     <asp:TableRow HorizontalAlign="Right">
                         <asp:TableCell>
                             <div class="menu4">
                                 <a href="../newdefault.aspx" class="menu4">首頁</a> &gt;  
                                 <a href="../Control/Batch_Upload_Plants_.aspx" class="menu4">植栽調查批次上傳</a>  
                                  
                             </div>
                         </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

         <asp:TableRow>
            <asp:TableCell backcolor="#F7F7F7" VerticalAlign="Top">
                <asp:Table ID="Table1" runat="server" Width="1000" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="900" VerticalAlign="Top">
                            <asp:Table ID="Table_Detail" runat="server" Width="900" HorizontalAlign="Center" BorderColor="#c0c0c0" BorderStyle="Solid">

                                <asp:TableRow Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2">
                                        植栽調查批次上傳
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="900">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table ID="Table2" runat="server" Width="800" HorizontalAlign="Center" >

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Attachments/Sample/現況植栽調查資料範例.xls" BorderStyle="None">現況植栽調查資料範例</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Attachments/Sample/高速公路局國道植栽資料庫操作手冊V2.pdf" BorderStyle="None">國道植栽資料庫操作手冊</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="DLFinishPlantsData.aspx" BorderStyle="None" Target="_blank">下載竣工植栽資料</asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>

<%--                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Attachments/Sample/道路致死講習.pdf" BorderStyle="None">道路致死講習</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell Width="400">
                                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Attachments/Sample/批次動物道路致死調查格式上傳說明.docx" BorderStyle="None">批次動物道路致死調查資料上傳說明</asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/View/QA.aspx" BorderStyle="None">常見問題</asp:HyperLink>
                                                </asp:TableCell>
                                                <asp:TableCell Width="400">
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>--%>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    
                                                </asp:TableCell>
                                                <asp:TableCell Width="400">
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Left" Width="400">
                                                    
                                                </asp:TableCell>
                                                <asp:TableCell Width="400">
                                                    
                                                </asp:TableCell>
                                            </asp:TableRow>
                                             
                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                

                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label1" runat="server" Text="新增調查資料："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:FileUpload ID="file1" runat="server" Width="500"/>
                                        <br/>
                                        檔案類型：excel
                                        <br/>
                                        說明：檔名請使用英文及數字，不接受空格及特殊符號。
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label2" runat="server" Text="新增圖片檔案："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:FileUpload ID="file2" runat="server" Width="500"/>
                                        <br/>
                                        檔案類型：.zip, .rar
                                        <br/>
                                        說明：檔名請使用英文及數字，不接受空格及特殊符號，檔案大小勿超過100MB。
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#e0e0e0" Width="900">
                                    <asp:TableCell ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                        <asp:Label ID="Label3" runat="server" Text="新增竣工圖檔案："></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:FileUpload ID="file3" runat="server" Width="500"/>
                                        <br/>
                                        檔案類型：.zip, .rar
                                        <br/>
                                        說明：檔名請使用英文及數字，不接受空格及特殊符號。
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow BackColor="#999999" Width="900">
                                    <asp:TableCell CssClass="menu13" ColumnSpan="2"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Right" Width="300">
                                    </asp:TableCell>
                                    <asp:TableCell Width="600">
                                        <asp:Button ID="Upload_Data" runat="server" Text="上傳檔案" OnClick="Upload_Data_Click" AutoPostBack="false"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <%-- <asp:Button ID="Month_NoRecord" runat="server" Text="本月無資料" OnClick="Upload_Month_NoRecord" AutoPostBack="false"/>--%>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
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
                    <asp:TableRow HorizontalAlign="Center" >
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
