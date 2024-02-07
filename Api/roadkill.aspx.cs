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

public partial class Api_roadkill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["freewayDbConnectionString"].ConnectionString.ToString());
        SqlCommand cmd;

        string value_version = Request.QueryString["version"];
        string value_format = Request.QueryString["format"];
        string value_deduce_species = Request.QueryString["sn"];
        string value_animal = Request.QueryString["type"];
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



        string sql = "Select TOP " + value_top  + "  deduce_species AS SN , animal AS TYPE , Convert(varchar(10),date,111) AS DATE , highway_id AS NO , range AS RANGE , mileage AS MILEAGE	, x AS LAT	, y AS LON From Roadkill_new Where 1 = 1 ";

        if (!value_deduce_species.Equals("") )
        {
            sql += " and deduce_species = '" + value_deduce_species + "'";
        }

        if (!value_animal.Equals(""))
        {
            sql += " and animal = '" + value_animal + "'";
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

        if (!value_startkm.Equals(""))
        {
            sql += " and milestone >= '" + value_startkm + "'";
        }

        if (!value_endkm.Equals(""))
        {
            sql += " and milestone <= '" + value_endkm + "'";
        }

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
                    XmlAttribute attChild1 = xdoc.CreateAttribute("SN");
                    attChild1.Value = reader["SN"].ToString();
                    eleChild1.Attributes.Append(attChild1);

                    Json += "SN:\"" + reader["SN"].ToString() + "\",";
                    writer.WritePropertyName("SN");
                    writer.WriteValue(reader["SN"].ToString());

                    XmlAttribute attChild2 = xdoc.CreateAttribute("DATE");
                    attChild2.Value = reader["DATE"].ToString();
                    eleChild1.Attributes.Append(attChild2);

                    Json += "DATE:\"" + reader["DATE"].ToString() + "\",";
                    writer.WritePropertyName("DATE");
                    writer.WriteValue(reader["DATE"].ToString());

                    XmlAttribute attChild3 = xdoc.CreateAttribute("NO");
                    attChild3.Value = reader["NO"].ToString();
                    eleChild1.Attributes.Append(attChild3);

                    Json += "NO:\"" + reader["NO"].ToString() + "\",";
                    writer.WritePropertyName("NO");
                    writer.WriteValue(reader["NO"].ToString());

                    XmlAttribute attChild4 = xdoc.CreateAttribute("RANGE");
                    attChild4.Value = reader["RANGE"].ToString();
                    eleChild1.Attributes.Append(attChild4);

                    Json += "RANGE:\"" + reader["RANGE"].ToString() + "\",";
                    writer.WritePropertyName("RANGE");
                    writer.WriteValue(reader["RANGE"].ToString());

                    XmlAttribute attChild5 = xdoc.CreateAttribute("MILEAGE");
                    attChild5.Value = reader["MILEAGE"].ToString();
                    eleChild1.Attributes.Append(attChild5);

                    Json += "MILEAGE:\"" + reader["MILEAGE"].ToString() + "\",";
                    writer.WritePropertyName("MILEAGE");
                    writer.WriteValue(reader["MILEAGE"].ToString());

                    XmlAttribute attChild6 = xdoc.CreateAttribute("LAT");
                    attChild6.Value = reader["LAT"].ToString();
                    eleChild1.Attributes.Append(attChild6);

                    Json += "LAT:\"" + reader["LAT"].ToString() + "\",";
                    writer.WritePropertyName("LAT");
                    writer.WriteValue(reader["LAT"].ToString());

                    XmlAttribute attChild7 = xdoc.CreateAttribute("LON");
                    attChild7.Value = reader["LON"].ToString();
                    eleChild1.Attributes.Append(attChild7);

                    Json += "LON:\"" + reader["LON"].ToString() + "\"},";
                    writer.WritePropertyName("LON");
                    writer.WriteValue(reader["LON"].ToString());

                    rootElement.AppendChild(eleChild1);
                    //writer.WriteEndObject();
                }

                Json += "]";
                Json = Json.Replace("},]", "}]");
                writer.WriteEndObject();
                
            }

            if (value_format.Equals("xml"))
            {
                //xdoc.Save(MapPath("apir.xml"));
                //Response.Redirect("apir.xml");
		Response.Clear();
		Response.ContentType = "text/xml; charset=utf-8";
		using (var stringWriter = new StringWriter())
		using (var xmlTextWriter = XmlWriter.Create(stringWriter))
		{
		    xdoc.WriteTo(xmlTextWriter);
		    xmlTextWriter.Flush();
		    Response.Write(stringWriter.GetStringBuilder().ToString());
		}
		Response.End();
            }
            else
            {
		Response.Clear();
		Response.ContentType = "application/json; charset=utf-8";
                Response.Write(Json.ToString());
		Response.End();
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