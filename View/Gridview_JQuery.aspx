<%@ page language="C#" autoeventwireup="true" inherits="View_Gridview_JQuery, App_Web_qgy0chct" %>

<!DOCTYPE html>
<%@ Import Namespace="System.Data" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
    .altRow { background-color: #ddddff; }
    </style>
    <link href="../Scripts/superTables.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="../Scripts/superTables.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.superTable.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#GridView1").toSuperTable({ width: "640px", height: "480px", fixedCols: 2 })
            .find("tr:even").addClass("altRow");
        });
    </script>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable t = new DataTable();
        t.Columns.Add("序號", typeof(int));
        t.Columns.Add("料號", typeof(string));
        t.Columns.Add("單價", typeof(decimal));
        t.Columns.Add("數量", typeof(int));
        t.Columns.Add("金額", typeof(decimal), "單價*數量");
        Random rnd = new Random();
        for (int i = 0; i < 200; i++)
        {
            t.Rows.Add(
                i + 1, 
                Guid.NewGuid().ToString().Substring(0, 13).ToUpper(),
                rnd.NextDouble() * 100,
                rnd.Next() * 2000);
        }
        GridView1.AutoGenerateColumns = false;
        foreach (DataColumn c in t.Columns)
        {
            BoundField bf = new BoundField();
            bf.DataField = c.ColumnName;
            bf.HeaderText = c.ColumnName;
            if (c.DataType == typeof(decimal))
                bf.DataFormatString = "{0:#,0.00}";
            else if (c.DataType == typeof(int))
                bf.DataFormatString = "{0:#,0}";
            bf.ItemStyle.HorizontalAlign =
                (!string.IsNullOrEmpty(bf.DataFormatString)) ?
                HorizontalAlign.Right : HorizontalAlign.Center;
                
            GridView1.Columns.Add(bf);
        }
        GridView1.DataSource = t;
        GridView1.DataBind();
    }
</script>

    <script type="text/javascript">
        $(function () {
            //在表格裡塞入選取欄位
            $("#GridView1 th:nth-child(1)")
            .after("<th><input type='checkbox' id='cbxSelAll' /></th>");
            $("#GridView1 td:nth-child(1)")
            .after("<td><input type='checkbox' class='clsHideCbx' /></td>");
            //配合SuperTable的多Table同步處理，加上列號
            $("#GridView1 tr").each(function (i) {
                $(this).attr("rowidx", i)
                .find("td").each(function (j) {
                    $(this).attr("pos", i + "_" + j);
                });
            });
            //設定SuperTable
            $("#GridView1").toSuperTable({ width: "640px", height: "480px", fixedCols: 3 })
            .find("tr:even").addClass("altRow");
            //加上全選/全不選功能(.sFHeader為配合SuperTable才加)
            $(".sFHeader #cbxSelAll")
            .click(function () {
                if (this.checked)
                    $(".clsHideCbx").attr("checked", "checked");
                else
                    $(".clsHideCbx").removeAttr("checked");
            });
            //加上隱藏/顯示功能
            $("#btnHide,#btnShowAll").click(toggleRow);
            //隱藏/顯示共用一個事件，由evt.target判別按的是哪一顆
            function toggleRow(evt) {
                var show = (evt.target.id == "btnShowAll");
                var rowSet = (show) ?
                    $(".sFData tr:not(:visible)") : $(".sFData .clsHideCbx:checked").closest("tr");
                rowSet.toggle(show)
                //一般情況到hide()即可，以下這段是為了SuperTable而加的
                //把捲動區裡那一份<table>對應的資料列也同步藏起來
                .each(function () {
                    $(".sData tbody tr[rowidx=" + $(this).attr("rowidx") + "]").toggle(show);
                });
                $(".sData tbody").find(".altRow").removeClass("altRow")
                .end().find("tr:visible:even").addClass("altRow");
                //修正IE隱藏結尾會不齊的問題
                if ($.browser.msie) {
                    var fixedColZone = $(".sFDataInner");
                    var cellZone = $(".sData table");
                    var p1 = fixedColZone.offset().top;
                    var p2 = cellZone.offset().top;
                    if (p1 != p2) {
                        fixedColZone.css("top", (p2 - fixedColZone.parent().offset().top) + "px");
                    }
                }
            }

            function getPosVal(s) {
                return parseInt(s.replace("px", ""));
            }

            $("#btnScroll").click(function () {
                scrollToRow($("#rowIdx").val());
            });

            //捲動到指定列數
            function scrollToRow(rowIdx) {
                //IE下有一列的位移，鋸箭法校正
                if ($.browser.msie && !isNaN(rowIdx))
                    rowIdx = parseInt(rowIdx) - 1;
                var x = $(".sData tr[rowidx=" + rowIdx + "]");
                if (x.length > 0) {
                    //alert($(".sData").scrollTop() + "," + x.position().top);
                    $(".sData").scrollTop(
                        $(".sData").scrollTop() +
                        x.position().top);
                }
            }
            //捲動到指定的欄數
            function scrollToCol(colIdx) {
                var x = $(".sData td:eq(" + colIdx + ")");
                $(".sData").scrollLeft($(".sData").scrollLeft() + x.position().left);
            }

            //保留所有Cell的集合
            var allCells = $(".sData #GridView1 td");

            $("#btnFind").click(function () {
                var keywd = $("#keywd").val();
                //先找到上次的焦點
                var focusIdx = 0;
                var hasPrevFocus = false;
                allCells.filter("td[findfocus]").each(function () {
                    focusIdx = allCells.index(this);
                    //將focus移除
                    $(this).removeAttr("findfocus")
                    //去除Highlight
                    .html($(this).text());
                    hasPrevFocus = true;
                });
                //由焦點開始往後找
                allCells.filter("td:gt(" + focusIdx + ")")
                .each(function () {
                    var td = $(this);
                    if (td.text().indexOf(keywd) > -1) {
                        var p = td.attr("pos").split("_");
                        scrollToRow(p[0]);
                        var cell = allCells.filter("td[pos=" + td.attr("pos") + "]");
                        cell.attr("findfocus", "true")
                        .html(cell.text().replace(keywd,
                            "<span style='background-color:yellow;'>" + keywd + "</span>"));
                        scrollToCol(p[1]);
                        return false;
                    }
                });
                if (!allCells.is("td[findfocus]")) {
                    if (hasPrevFocus) {
                        if (confirm("已搜尋至結尾，要從頭開始再找一次嗎?"))
                            $("#btnFind").click();
                    } else
                        alert("找不到指定的關鍵字!");
                }
            });

        });
    </script>


   
    <style type="text/css">
    .clsLinkButton  
    { font-size: 9pt; cursor: pointer; text-decoration: underline; color: Blue; }
    #rowIdx { width: 20px; text-align: right; }
    #spnCmdBar { font-size: 9pt; margin-left: 15px; }
    </style>
</head>
<body>

<form id="form1" runat="server">
<span id="spnCmdBar">
關鍵字: <input type="text" id="keywd" style="width: 80px;" /> 
<input type="button" id="btnFind" value="尋找" />&nbsp;
移至第<input type="text" id="rowIdx" value="1" />列 
<input type="button" value="捲動吧! 表格" id="btnScroll" /></span>
    <asp:GridView ID="GridView1" runat="server" Font-Size="9pt">
    </asp:GridView>
<span id="btnHide" class="clsLinkButton" style="margin-left: 150px;">隱藏選取列</span>
<span id="btnShowAll" class="clsLinkButton">顯示全部</span>
    </form>
</body>
</html>
