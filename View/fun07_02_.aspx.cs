using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.Configuration;



public partial class View_fun07_02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1.Equals(false))
        {
            Response.Redirect("../Account/logout.cshtml");
        }


        //Query();
    }

    protected void QueryData(object sender, EventArgs e)
    {
        Query();
    }


    protected void Query()
    {
        string value_work1 = Work1.SelectedValue.ToString();
        string value_work2 = Work2.SelectedValue.ToString();

        int value_year = int.Parse(Select_Year.SelectedValue.ToString());

        //產生GridView資料
        string sql_1 = " Select site_ch , Month(date) AS MM , Count(*) AS CNTS From Roadkill_new Where Year(date) = '" + value_year + "' and site_ch in ('" + value_work1 + "','" + value_work2 + "') Group By Month(date) , site_ch ";
        SDS_View1.SelectCommand = sql_1.ToString();
        GridView1.DataBind();

        string sql_w1 = " Select Month(date) AS MM , Count(*) AS CNTS From Roadkill_new Where Year(date) = '" + value_year + "' and site_ch = '" + value_work1 + "' Group By Month(date) ";
        string sql_w2 = " Select Month(date) AS MM , Count(*) AS CNTS From Roadkill_new Where Year(date) = '" + value_year + "' and site_ch = '" + value_work2 + "' Group By Month(date) ";

        try
        {
            #region Step1. 設定 Chart Title
            Title ChartTitle = new Title();
            System.Drawing.Font font = new System.Drawing.Font("標楷體", 20);
            ChartTitle.Font = font;
            ChartTitle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            ChartTitle.Text = "工務段道路致死數量變化及比較";

            //新增至Chart Control
            Chart1.Titles.Add(ChartTitle);
            #endregion

            #region Step2. 產生工作區塊(Area1)
            ChartArea cArea1 = new ChartArea("Area1");
            //設定Area1的X,Y軸標題
            cArea1.AxisX.Title = "月份";
            cArea1.AxisY.Title = "道路致死件數";
            //X,Y軸刻度區間
            cArea1.AxisX.Interval = 1;
            cArea1.AxisY.Interval = 20;

            #region Step2.1 產生Area1的Series


            #region Series1填入資料
            List<string> lstX1 = new List<string>();
            List<string> lstY1 = new List<string>();


            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());

            
            try
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand(sql_w1, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstX1.Add(reader["MM"].ToString());
                        lstY1.Add(reader["CNTS"].ToString());
                    }
                }
 
            }

            catch (Exception ex)
            {
                // 在這裡新增錯誤處理方式以便進行偵錯。
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
            conn.Close();


            /*
            Random r1 = new Random((int)DateTime.Now.Ticks);
            for (int i = 1; i < 24; i++)
            {
                lstX1.Add(i.ToString());
            }
            for (int i = 1; i < 24; i++)
            {
                int x = r1.Next(0, 100);
                lstY1.Add(x.ToString());
            }
            */
            #endregion

            #region series1
            Series series1 = new Series(value_work1);
            //設定要顯示在哪一個ChartArea
            series1.ChartArea = "Area1";
            //設定圖表種類
            //series1.ChartType = SeriesChartType.Column;
            series1.ChartType = SeriesChartType.Column;
            //是否將值show在value label上
            series1.IsValueShownAsLabel = true;
            //填入資料
            series1.Points.DataBindXY(lstX1, lstY1);
            #endregion


            #region Series1填入資料
            List<string> lstX2 = new List<string>();
            List<string> lstY2 = new List<string>();


            try
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand(sql_w2, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lstX2.Add(reader["MM"].ToString());
                        lstY2.Add(reader["CNTS"].ToString());
                    }
                }

            }

            catch (Exception ex)
            {
                // 在這裡新增錯誤處理方式以便進行偵錯。
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }
            conn.Close();


            /*
            Random r2 = new Random(((int)DateTime.Now.Ticks) + 1);
            for (int i = 1; i < 24; i++)
            {
                lstX2.Add(i.ToString());
            }
            for (int i = 1; i < 24; i++)
            {
                int x = r2.Next(0, 100);
                lstY2.Add(x.ToString());
            }
            */


            #endregion



            #region series2
            //string kind2 = "木柵";
            Series series2 = new Series(value_work2);
            //設定要顯示在哪一個ChartArea
            series2.ChartArea = "Area1";
            //設定圖表種類
            //series2.ChartType = SeriesChartType.Spline;
            series2.ChartType = SeriesChartType.Column;
            //是否將值show在value label上
            series2.IsValueShownAsLabel = true;
            //填入資料
            series2.Points.DataBindXY(lstX2, lstY2);
            series2.BorderWidth = 3;
            #endregion

            #endregion

            #endregion

            #region Step3. 設定Legend
            Legend leg = new Legend("分類");
            leg.Docking = Docking.Right;
            Chart1.Legends.Add(leg);
            #endregion

            Chart1.ChartAreas.Add(cArea1);
            Chart1.Series.Add(series1);
            Chart1.Series.Add(series2);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void Export_Excel1(object sender, EventArgs e)
    {
        GridView1.PageSize = 100000;
        //GridView1.DataBind();

        //匯出EXCEL檔
        Response.Clear();
        Response.Buffer = true;

        //Response.Charset = "UTF-8";
        Response.Charset = "BIG5";
        string Excel_ShortTime = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        Response.AppendHeader("Content-Disposition", "attachment;filename=Caul1_" + Excel_ShortTime + ".xls");
        //Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        Response.ContentEncoding = Encoding.GetEncoding("BIG5");
        //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
        Response.ContentType = "application/ms-excel";
        GridView1.EnableViewState = false;
        GridView1.AllowPaging = false;
        GridView1.AllowSorting = false;
        StringWriter objStringWriter = new StringWriter();
        HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
        GridView1.RenderControl(objHtmlTextWriter);

        Response.Write(objStringWriter.ToString());
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // '處理'GridView' 的控制項 'GridView' 必須置於有 runat=server 的表單標記之中   
    }

}

