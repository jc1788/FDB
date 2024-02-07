using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI;

public partial class View_fun07_04 : System.Web.UI.Page
{
    public DataTable dt_excel1 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }

        Query();
    }

    protected void Query()
    {
        string value_Select_Year = Select_Year.SelectedValue.ToString();
        string value_Select_Month = Select_Month.SelectedValue.ToString();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        conn.Open();
        SqlCommand cmd;

        if (!IsPostBack)
        {
            value_Select_Year = "2009";
            value_Select_Month = "1";

            Label2.Text = "";
            string sql_2 = "";
            SDS_View2.SelectCommand = sql_2.ToString();
            GridView2.DataBind();
            ImageButton2.Visible = false;
            Button2.Visible = false;

            Label3.Text = "";
            string sql_3 = "";
            SDS_View3.SelectCommand = sql_3.ToString();
            GridView3.DataBind();
            ImageButton3.Visible = false;
            Button3.Visible = false;

        }
        else
        {

            //選擇年度
            if (!value_Select_Year.Equals("0") && !value_Select_Year.Equals(""))
            {
                Label2.Text = value_Select_Year.ToString() + "年各工務段道路致死總數及累積數量";
                string sql_2 = " Select site_ch , count(site_ch) As cnts From Roadkill Where year(date) = '" + value_Select_Year + "' Group By site_ch Order By site_ch ";
                SDS_View2.SelectCommand = sql_2.ToString();
                GridView2.DataBind();
                ImageButton2.Visible = true;
                Button2.Visible = true;
                cmd = new SqlCommand(sql_2, conn);
                using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                {
                    a.Fill(dt_excel1);
                }

            }
            //選擇月份
            if (!value_Select_Month.Equals("0") && !value_Select_Month.Equals(""))
            {

                Label3.Text = value_Select_Year.ToString() + "年" + value_Select_Month.ToString() + "月各工務段道路致死總數及累積數量";
                string sql_3 = " ";
                sql_3 += " Select site_ch , Cast(count(site_ch) AS varchar(10)) As cnts From Roadkill Where year(date) = '" + value_Select_Year + "' and month(date) = '" + value_Select_Month + "' Group By site_ch ";
                sql_3 += " Union Select user_institution , '無資料' As cnts From Roadkill_Month_NoRecord Where years = '" + value_Select_Year + "' and months = '" + value_Select_Month + "' Order By site_ch ";

                SDS_View3.SelectCommand = sql_3.ToString();
                GridView3.DataBind();
                ImageButton3.Visible = true;
                Button3.Visible = true;

                cmd = new SqlCommand(sql_3, conn);
                using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                {
                    a.Fill(dt_excel1);
                }
            }
            else
            {
                Label3.Text = "";
            }
        }

    }

    protected void Export_Excel2(object sender, EventArgs e)
    {
        GridView2.PageSize = 100000;
        GridView2.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=Caul2_" + Excel_ShortTime + ".xls");
        //Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        Response.ContentEncoding = Encoding.GetEncoding("BIG5");
        //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentType = "application/ms-excel";
        GridView2.EnableViewState = false;
        GridView2.AllowPaging = false;
        GridView2.AllowSorting = false;


        StringWriter objStringWriter = new StringWriter();
        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        GridView2.RenderControl(objHtmlTextWriter);

        Response.Write(objStringWriter.ToString());
        Response.End();

    }

    protected void Export_Excel3(object sender, EventArgs e)
    {
        GridView3.PageSize = 100000;
        GridView3.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=Caul3_" + Excel_ShortTime + ".xls");
        //Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        Response.ContentEncoding = Encoding.GetEncoding("BIG5");
        //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentType = "application/ms-excel";
        GridView3.EnableViewState = false;
        GridView3.AllowPaging = false;
        GridView3.AllowSorting = false;
        StringWriter objStringWriter = new StringWriter();
        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        GridView3.RenderControl(objHtmlTextWriter);

        Response.Write(objStringWriter.ToString());
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }


    protected void Export_Excel_new(object sender, EventArgs e)
    {
        System.DateTime now = new System.DateTime();
        now = System.DateTime.Now;
        //ExcelExporter export = new ExcelExporter();
        try
        {

            // 產生EXCEL Sheet 設定
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            // Detail Sheet
            ISheet u_sheet_Detail = workbook.CreateSheet("Caul");

            // CellStyle
            IDataFormat num_format = workbook.CreateDataFormat();
            IFont content_font = workbook.CreateFont();
            content_font.FontName = "新細明體";
            content_font.FontHeightInPoints = 12;

            ICellStyle style_wrap_right = workbook.CreateCellStyle();
            //style_wrap_right.DataFormat = num_format.GetFormat("###,###,##0.00");
            style_wrap_right.Alignment = HorizontalAlignment.Right;
            style_wrap_right.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_right.WrapText = true;
            style_wrap_right.SetFont(content_font);

            ICellStyle style_wrap_left = workbook.CreateCellStyle();
            style_wrap_left.Alignment = HorizontalAlignment.Left;
            style_wrap_left.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_left.WrapText = true;
            style_wrap_left.SetFont(content_font);

            ICellStyle style_wrap_center = workbook.CreateCellStyle();
            style_wrap_center.Alignment = HorizontalAlignment.Center;
            style_wrap_center.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_center.WrapText = true;
            style_wrap_center.SetFont(content_font);

            ICellStyle style_wrap_right_unit = workbook.CreateCellStyle();
            //style_wrap_right_unit.DataFormat = num_format.GetFormat("###,##0.000000");
            style_wrap_right_unit.Alignment = HorizontalAlignment.Right;
            style_wrap_right_unit.VerticalAlignment = VerticalAlignment.Center;
            style_wrap_right_unit.WrapText = true;
            style_wrap_right_unit.SetFont(content_font);

            // 差異表工作表 欄寬
            u_sheet_Detail.SetColumnWidth(0, 20 * 256);
            u_sheet_Detail.SetColumnWidth(1, 10 * 256);

            u_sheet_Detail.CreateRow(0);
            u_sheet_Detail.GetRow(0).CreateCell(0).SetCellValue("工務段");
            u_sheet_Detail.GetRow(0).CreateCell(1).SetCellValue("數量");

            //設定CellStyle
            u_sheet_Detail.GetRow(0).GetCell(0).CellStyle = style_wrap_center;
            u_sheet_Detail.GetRow(0).GetCell(1).CellStyle = style_wrap_center;

            int k = 0;

            // 比較表 資料
            foreach (DataRow pDR_FSData in dt_excel1.Rows)
            {
                if (pDR_FSData[0] != DBNull.Value)
                {
                    k++;
                    u_sheet_Detail.CreateRow(k);
                    u_sheet_Detail.GetRow(k).CreateCell(0).SetCellValue(pDR_FSData["site_ch"].ToString());
                    u_sheet_Detail.GetRow(k).CreateCell(1).SetCellValue(pDR_FSData["cnts"].ToString());


                    //設定CellStyle
                    u_sheet_Detail.GetRow(k).GetCell(0).CellStyle = style_wrap_left;
                    u_sheet_Detail.GetRow(k).GetCell(1).CellStyle = style_wrap_left;

                }
            }

            workbook.Write(ms);

            //將EXCEL檔案匯出到系統資料儲存目錄
            //string Excel_ShortTime = "roadkill_01.xls" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
            string Excel_ShortTime = "Caul2_.xls";
            string folderPath = "D:\\Down_Excel\\" + Excel_ShortTime;
            FileStream fs = new FileStream(folderPath, FileMode.Create, FileAccess.Write);
            fs.Write(ms.ToArray(), 0, ms.ToArray().Length);
            fs.Flush();
            fs.Close();
            workbook = null;
            ms.Close();
            ms.Dispose();

            System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
            byte[] xfile = null;
            //string docupath = Request.PhysicalApplicationPath;
            xfile = wc.DownloadData(folderPath);
            string xfileName = System.IO.Path.GetFileName(folderPath);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
            HttpContext.Current.Response.ContentType = "application/octet-stream"; //二進位方式
            HttpContext.Current.Response.BinaryWrite(xfile); //內容轉出作檔案下載
            HttpContext.Current.Response.End();

            //pDT_Account_Payables_Different_Detail = null;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

    }
}