using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.IO;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public partial class Control_DLFinishPlantsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }
    }
    protected void DLData_Click(object sender, EventArgs e)
    {
        string conns = WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString();
        string sql = "select po.id,po.highway_id,po.direction,po.start_mileage,po.end_mileage,po.chinese_name,po.amount,po.high,po.width,po.m_high,po.plant_class,po.location,po.plant_type,pe.work_date,pe.design_date from Plant_observation po join Plant_engineering pe on po.pid = pe.pid where plant_class = '喬木'";
        string highway_id_sql = highway_id.SelectedValue.ToString() != "" ? " and po.highway_id = @highway_id" : "";
        string direction_sql = direction.SelectedValue.ToString() != "" ? " and direction = @direction" : "";
        string start1_sql = start1.Text != "" && start1.Text != "0" ? " and start_mileage1 >= @start1" : "";
        string start2_sql = start2.Text != "" && start2.Text != "0" ? " and start_mileage2 >= @start2" : "";
        string end1_sql = End1.Text != "" && End1.Text != "0" ? " and end_mileage1 <= @End1" : "";
        string end2_sql = End2.Text != "" && End2.Text != "0" ? " and end_mileage2 <= @End2" : "";
        sql += highway_id_sql + direction_sql + start1_sql + start2_sql + end1_sql + end2_sql;

	string sql2 = "select PlantId,Office,Segment,Highway_Name,Direction,Start,[End],Plant_Name,Plant_Number,SpecificationTall,SpecificationCrown,SpecificationMeter,LifeStyle,florescence,FlowerColor,FruitPeriod,LeafColor,LeafPeriod,Plant_Loca,Date,Note,upload_date from Plants where 1=1";
        string highway_id_sql2 = highway_id.SelectedValue.ToString() != "" ? " and Highway_Name = @highway_id" : "";
        string direction_sql2 = direction.SelectedValue.ToString() != "" ? " and Direction = @direction" : "";
        string start1_sql2 = start1.Text != "" && start1.Text != "0" ? " and Start1 >= @start1" : "";
        string start2_sql2 = start2.Text != "" && start2.Text != "0" ? " and Start2 >= @start2" : "";
        string end1_sql2 = End1.Text != "" && End1.Text != "0" ? " and End1 <= @End1" : "";
        string end2_sql2 = End2.Text != "" && End2.Text != "0" ? " and End2 <= @End2" : "";
        sql2 += highway_id_sql2 + direction_sql2 + start1_sql2 + start2_sql2 + end1_sql2 + end2_sql2;

        using (SqlConnection cs = new SqlConnection(conns))
        {
            cs.Open();
            SqlCommand cmd = new SqlCommand(sql, cs);
	    if (highway_id.SelectedValue.ToString() != "") cmd.Parameters.AddWithValue("highway_id",highway_id.SelectedValue.ToString());
	    if (direction.SelectedValue.ToString() != "") cmd.Parameters.AddWithValue("direction",direction.SelectedValue.ToString());
	    if (start1.Text != "" && start1.Text != "0") cmd.Parameters.AddWithValue("start1",start1.Text);
	    if (start2.Text != "" && start2.Text != "0") cmd.Parameters.AddWithValue("start2",start2.Text);
	    if (End1.Text != "" && End1.Text != "0") cmd.Parameters.AddWithValue("End1",End1.Text);
	    if (End2.Text != "" && End2.Text != "0") cmd.Parameters.AddWithValue("End2",End2.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            SqlCommand cmd2 = new SqlCommand(sql2, cs);
	    if (highway_id.SelectedValue.ToString() != "") cmd2.Parameters.AddWithValue("highway_id",highway_id.SelectedValue.ToString());
	    if (direction.SelectedValue.ToString() != "") cmd2.Parameters.AddWithValue("direction",direction.SelectedValue.ToString());
	    if (start1.Text != "" && start1.Text != "0") cmd2.Parameters.AddWithValue("start1",start1.Text);
	    if (start2.Text != "" && start2.Text != "0") cmd2.Parameters.AddWithValue("start2",start2.Text);
	    if (End1.Text != "" && End1.Text != "0") cmd2.Parameters.AddWithValue("End1",End1.Text);
	    if (End2.Text != "" && End2.Text != "0") cmd2.Parameters.AddWithValue("End2",End2.Text);
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);

            if (dt.Rows.Count != 0 || dt2.Rows.Count != 0)
            {
                DataTable exportdt = new DataTable();
                exportdt.TableName = "Plants";
		exportdt.Columns.Add(new DataColumn("序號(流水號)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("養護工程分局(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("工務段(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("國道別(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("方向(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("起訖里程(起)(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("起訖里程(訖)(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("種類(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("數量", typeof(string)));
                exportdt.Columns.Add(new DataColumn("單位", typeof(string)));
                exportdt.Columns.Add(new DataColumn("高度(cm)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("冠徑(cm)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("米徑(cm)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("型態", typeof(string)));
                exportdt.Columns.Add(new DataColumn("花期(月份)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("花色", typeof(string)));
                exportdt.Columns.Add(new DataColumn("果期(月份)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("葉色", typeof(string)));
                exportdt.Columns.Add(new DataColumn("葉色轉變期(月份)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("位置", typeof(string)));
                exportdt.Columns.Add(new DataColumn("圖片名稱", typeof(string)));
                exportdt.Columns.Add(new DataColumn("竣工圖名稱", typeof(string)));
                exportdt.Columns.Add(new DataColumn("調查日期(必填)", typeof(string)));
                exportdt.Columns.Add(new DataColumn("備註", typeof(string)));
                exportdt.Columns.Add(new DataColumn("建檔日期", typeof(string)));

		int sn = 1;
		if (dt.Rows.Count != 0)
                foreach (DataRow row in dt.Rows)
                {
                    DataRow export_row = exportdt.NewRow();
                    //export_row[0] = sn.ToString();
		    export_row[0] = row["id"].ToString();
                    export_row[1] = "";
                    export_row[2] = "";
                    export_row[3] = row["highway_id"].ToString();
                    export_row[4] = row["direction"].ToString();
                    export_row[5] = row["start_mileage"].ToString();
                    export_row[6] = row["end_mileage"].ToString();
                    export_row[7] = row["chinese_name"].ToString();
                    export_row[8] = row["amount"].ToString();
                    export_row[9] = "";
                    export_row[10] = row["high"].ToString();
                    export_row[11] = row["width"].ToString();
                    export_row[12] = row["m_high"].ToString();
                    export_row[13] = row["plant_class"].ToString();
                    export_row[14] = "";
                    export_row[15] = "";
                    export_row[16] = "";
                    export_row[17] = "";
                    export_row[18] = "";
                    export_row[19] = row["location"].ToString();
                    export_row[20] = "";
                    export_row[21] = "";
                    export_row[22] = "";
                    export_row[23] = row["plant_type"].ToString();
		    if (!String.IsNullOrWhiteSpace(row["work_date"].ToString()))
			export_row[24] = row["work_date"].ToString();
		    else if (!String.IsNullOrWhiteSpace(row["design_date"].ToString()))
			export_row[24] = row["design_date"].ToString();
		    else
			export_row[24] = "";
                    exportdt.Rows.Add(export_row);
		    sn++;
                }

		if (dt2.Rows.Count != 0)
                foreach (DataRow row in dt2.Rows)
                {
                    DataRow export_row = exportdt.NewRow();
                    //export_row[0] = sn.ToString();
		    export_row[0] = row["PlantId"].ToString();
                    export_row[1] = row["Office"].ToString();
                    export_row[2] = row["Segment"].ToString();
                    export_row[3] = row["Highway_Name"].ToString();
                    export_row[4] = row["Direction"].ToString();
                    export_row[5] = row["Start"].ToString();
                    export_row[6] = row["End"].ToString();
                    export_row[7] = row["Plant_Name"].ToString();
                    export_row[8] = row["Plant_Number"].ToString();
                    export_row[9] = "";
                    export_row[10] = row["SpecificationTall"].ToString();
                    export_row[11] = row["SpecificationCrown"].ToString();
                    export_row[12] = row["SpecificationMeter"].ToString();
                    export_row[13] = row["LifeStyle"].ToString();
                    export_row[14] = row["florescence"].ToString();
                    export_row[15] = row["FlowerColor"].ToString();
                    export_row[16] = row["FruitPeriod"].ToString();
                    export_row[17] = row["LeafColor"].ToString();
                    export_row[18] = row["LeafPeriod"].ToString();
                    export_row[19] = row["Plant_Loca"].ToString();
                    export_row[20] = "";
                    export_row[21] = "";
                    export_row[22] = row["Date"].ToString();
                    export_row[23] = row["Note"].ToString();
		    if (!String.IsNullOrWhiteSpace(row["upload_date"].ToString()))
			export_row[24] = row["upload_date"].ToString().Substring(0,8);
		    else
			export_row[24] = "";
                    exportdt.Rows.Add(export_row);
		    sn++;
                }

                DataSet ds_export = new DataSet();
                ds_export.Tables.Add(exportdt);
/*
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                foreach (DataTable mydt in ds_export.Tables)
                {
                    //Create a table
                    Table tbl = new Table();

                    //Create column header row
                    TableHeaderRow thr = new TableHeaderRow();
                    foreach (DataColumn col in mydt.Columns)
                    {
                        TableHeaderCell th = new TableHeaderCell();
                        th.Text = col.Caption;
                        thr.Controls.Add(th);
                    }
                    tbl.Controls.Add(thr);

                    //Create table rows
                    foreach (DataRow row in mydt.Rows)
                    {
                        TableRow tr = new TableRow();
                        foreach (var value in row.ItemArray)
                        {
                            TableCell td = new TableCell();
                            td.Text = value.ToString();
                            tr.Controls.Add(td);
                        }
                        tbl.Controls.Add(tr);
                    }

                    tbl.RenderControl(hw);

                }

                string excelFileName = "ExportData.xls";
                HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
                HttpContext.Current.Response.ContentType = "application/x-msexcel";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=\"" + Page.Server.UrlEncode(excelFileName) + "\"");

                HttpContext.Current.Response.Write(sw.ToString());

                sw.Close();
                hw.Close();
*/

		HSSFWorkbook workbook = new HSSFWorkbook();
		HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Plants");
		HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
		foreach(DataColumn dc in ds_export.Tables[0].Columns)
		{
		    headerRow.CreateCell(dc.Ordinal).SetCellValue(dc.ColumnName);
		}
		int rowIndex = 1;
		foreach(DataRow row in ds_export.Tables[0].Rows)
		{
		    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
		    foreach (DataColumn column in ds_export.Tables[0].Columns)
		    {
			dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
		    }
		    dataRow = null;
		    rowIndex++;
		}

		HttpContext.Current.Response.Clear();
		MemoryStream ms = new MemoryStream();
		workbook.Write(ms);
		headerRow = null;
		sheet = null;
		workbook = null;

		HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=ExportData.xls"));
		HttpContext.Current.Response.BinaryWrite(ms.ToArray());
		ms.Close();
		ms.Dispose();
                HttpContext.Current.Response.End();
            }
        }
    }
}