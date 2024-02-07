using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.Xml;
using System.Text;
using Newtonsoft.Json;

public partial class Api_occurrence : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        string value_version = Request.QueryString["version"];
        string value_format = Request.QueryString["format"];
        string value_chinese_name = Request.QueryString["sn"];
        string value_Family = Request.QueryString["type"];
        string value_start = Request.QueryString["start"] + "-01";
        string value_end = Request.QueryString["end"] + "-01";
        string value_highway_id = Request.QueryString["no"];
        string value_startkm = Request.QueryString["startkm"];
        string value_endkm = Request.QueryString["endkm"];
        string value_top = Request.QueryString["top"];
        //string value_skip = Request.QueryString["skip"];

        XmlDocument xdoc = new XmlDocument();
        xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));

        // 建立根節點物件並加入 XmlDocument 中 (第 0 層)
        XmlElement rootElement = xdoc.CreateElement("xml");
        xdoc.AppendChild(rootElement);

        

        //string sql = "Select TOP " + value_top  + "  deduce_species AS SN , animal AS TYPE , Convert(varchar(10),date,111) AS DATE , highway_id AS NO , range AS RANGE , mileage AS MILEAGE	, x AS LAT	, y AS LON From Roadkill_new Where 1 = 1 ";
        string sql = "Select TOP " + value_top + " chinese_name as sn_zh , ScientificName as sn , Family as type , Convert(varchar(10),invest_date,111) as date , highway_id as NO , site_ch as loc , x as lat , y as lon from OCCURRENCE_API  Where 1 = 1 ";
        if (!value_chinese_name.Equals(""))
        {
            sql += " and chinese_name = '" + value_chinese_name + "'";
        }

        if (!value_Family.Equals(""))
        {
            sql += " and Family = '" + value_Family + "'";
        }

        if (!value_start.Equals(""))
        {
            sql += " and Replace(Convert(Varchar(10),date,111),'/','-') >= '" + value_start + "'";
        }

        if (!value_end.Equals(""))
        {
            sql += " and Replace(Convert(Varchar(10),date,111),'/','-') <= '" + value_end + "'";
        }

        if (!value_highway_id.Equals(""))
        {
            sql += " and highway_id = '" + value_highway_id + "'";
        }
        /*
        if (!value_startkm.Equals(""))
        {
            sql += " and milestone >= '" + value_startkm + "'";
        }

        if (!value_endkm.Equals(""))
        {
            sql += " and milestone <= '" + value_endkm + "'";
        }
        */
        string Json = "";
        StringWriter sw = new StringWriter();
        //建立JsonTextWriter
        JsonTextWriter writer = new JsonTextWriter(sw);

        try
        {
            conn.Open();
            cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.HasRows)
            {
                Json += "[";
                //要輸出的變數
                
                writer.WriteStartObject();

                while (reader.Read())
                {
                    Json += "{";
                    //writer.WriteStartObject();
                    
                    XmlElement eleChild1 = xdoc.CreateElement("Data");
                    XmlAttribute attChild1 = xdoc.CreateAttribute("sn_zh");
                    attChild1.Value = reader["sn_zh"].ToString();
                    eleChild1.Attributes.Append(attChild1);

                    Json += "sn_zh:\"" + reader["sn_zh"].ToString() + "\",";
                    writer.WritePropertyName("sn_zh");
                    writer.WriteValue(reader["sn_zh"].ToString());

                    XmlAttribute attChild2 = xdoc.CreateAttribute("sn");
                    attChild2.Value = reader["sn"].ToString();
                    eleChild1.Attributes.Append(attChild2);

                    Json += "sn:\"" + reader["sn"].ToString() + "\",";
                    writer.WritePropertyName("sn");
                    writer.WriteValue(reader["sn"].ToString());

                    XmlAttribute attChild3 = xdoc.CreateAttribute("type");
                    attChild3.Value = reader["type"].ToString();
                    eleChild1.Attributes.Append(attChild3);

                    Json += "type:\"" + reader["type"].ToString() + "\",";
                    writer.WritePropertyName("type");
                    writer.WriteValue(reader["type"].ToString());

                    XmlAttribute attChild4 = xdoc.CreateAttribute("date");
                    attChild4.Value = reader["date"].ToString();
                    eleChild1.Attributes.Append(attChild4);

                    Json += "date:\"" + reader["date"].ToString() + "\",";
                    writer.WritePropertyName("date");
                    writer.WriteValue(reader["date"].ToString());

                    XmlAttribute attChild5 = xdoc.CreateAttribute("NO");
                    attChild5.Value = reader["NO"].ToString();
                    eleChild1.Attributes.Append(attChild5);

                    Json += "NO:\"" + reader["NO"].ToString() + "\",";
                    writer.WritePropertyName("NO");
                    writer.WriteValue(reader["NO"].ToString());

                    XmlAttribute attChild6 = xdoc.CreateAttribute("loc");
                    attChild6.Value = reader["loc"].ToString();
                    eleChild1.Attributes.Append(attChild6);

                    Json += "loc:\"" + reader["loc"].ToString() + "\",";
                    writer.WritePropertyName("loc");
                    writer.WriteValue(reader["loc"].ToString());

                    XmlAttribute attChild7 = xdoc.CreateAttribute("lat");
                    attChild7.Value = reader["lat"].ToString();
                    eleChild1.Attributes.Append(attChild7);

                    Json += "lat:\"" + reader["lat"].ToString() + "\"},";
                    writer.WritePropertyName("lat");
                    writer.WriteValue(reader["lat"].ToString());


                    rootElement.AppendChild(eleChild1);
                    //writer.WriteEndObject();
                }

                Json += "]";
                Json = Json.Replace("},]", "}]");
                writer.WriteEndObject();
                
            }

            if (value_format.Equals("xml"))
            {
                xdoc.Save(MapPath("apio.xml"));
                Response.Redirect("apio.xml");
            }
            else
            {
                Response.Write(Json.ToString());
                //String FilePath = "apir.json";
                // lab_Json.Text = Json.ToString();
                /*
                StreamWriter outfile = new StreamWriter(MapPath("apir.json"), true);
                outfile.WriteAsync(sw.ToString());
                outfile = null;
                sw = null;
                Response.Redirect("apir.json");
                */
                /*
                StringBuilder sb = new StringBuilder();
                sb.Append(Json);
                StreamWriter outfile = new StreamWriter(MapPath("apir.json"), true);
                outfile.WriteAsync(sb.ToString());
                outfile = null;
                sb = null;
                Response.Redirect("apir.json");
                */

            }

        }

        catch (Exception ex)
        {
            // 在這裡新增錯誤處理方式以便進行偵錯。
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
        }
        conn.Close();

        
        /*
        XmlElement eleChild1 = xdoc.CreateElement("NO");
        XmlAttribute attChild1 = xdoc.CreateAttribute("NO");
        attChild1.Value = "1";
        eleChild1.Attributes.Append(attChild1);
        rootElement.AppendChild(eleChild1);
        XmlElement eleChild2 = xdoc.CreateElement("NO");
        XmlAttribute attChild2 = xdoc.CreateAttribute("NO");
        attChild2.Value = "2";
        eleChild2.Attributes.Append(attChild2);
        rootElement.AppendChild(eleChild2);
        */
        /*
        for (int i = 0; i < p_DataTable.Rows.Count; i++)
        {
            XmlElement eleChild1 = xdoc.CreateElement("NO");
            XmlAttribute attChild1 = xdoc.CreateAttribute("NO");
            attChild1.Value = p_DataTable.Rows[i]["NO"].ToString();
            eleChild1.Attributes.Append(attChild1);
            rootElement.AppendChild(eleChild1);

            XmlElement eleChild2 = xdoc.CreateElement("WFS_SEQ");
            XmlAttribute attChild2 = xdoc.CreateAttribute("WFS_SEQ");
            attChild2.Value = p_DataTable.Rows[i]["WFS_SEQ"].ToString();
            eleChild2.Attributes.Append(attChild2);
            rootElement.AppendChild(eleChild2);

        }
        */
        
    }
}