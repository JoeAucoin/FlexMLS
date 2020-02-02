﻿using System;
using System.Net;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace GIBS.Modules.FlexMLS.Components
{
    /// <summary>
    /// ZillowAPI implementation for .Net
    /// </summary>
    public class ZillowAPI
    {
        public class ZillowEstimate
        {
            public int ReturnCode;
            public string ReturnCodeMessage;
            public int ZillowId;
            public string LinktoMap;
            public string LinktoHomeDetails;
            public string LinktoGraphsAndData;
            public string LinktoComparables;
            public decimal Estimate;
            public DateTime LastUpdated;
            public decimal ValueChange;
            public int ValueChangeDurationInDays;
            public decimal ValueRangeLow;
            public decimal ValueRangeHigh;
            public decimal Latitude;
            public decimal Longitude;
            public string Street;
            public string City;
            public string State;
            public string ZipCode;
            public string RequestURL;
            public DataTable dt;
            public string ChartValuation;
            public string ChartLink;
        }


        public static ZillowEstimate GetZestimate(string zillowWebServiceId, string Address, string ZipCode)
        {
            //http://www.zillow.com/howto/api/GetSearchResults.htm

            var z = new ZillowEstimate();
            // Construct the url
            //       var zEstimate = new decimal();
            ///// http://www.zillow.com/webservice/GetSearchResults.htm?zws-id=X1-ZWz1cs7xazgk5n_6msnx&address=84+Queen+Anne+Rd&citystatezip=Harwich+MA+02645
            ///////////////////////////

            var url = String.Format("http://www.zillow.com/webservice/GetSearchResults.htm?zws-id={0}&address={1}&citystatezip={2}", zillowWebServiceId, Address, ZipCode);


            // Make the HTTP request / get the response
            var Request = (System.Net.HttpWebRequest)WebRequest.Create(url);
            var Response = (HttpWebResponse)Request.GetResponse();

            // Parse the HTTP response into an XML document
            XmlDocument xml = new XmlDocument();
            xml.Load(Response.GetResponseStream());
            XmlElement root = xml.DocumentElement;

            //Return Code
            z.ReturnCode = int.Parse(root.SelectSingleNode("//message/code").InnerText);
            z.ReturnCodeMessage = root.SelectSingleNode("//message/text").InnerText;


            if (z.ReturnCode == 0)
            {
                z.ZillowId = int.Parse(root.SelectSingleNode("//response/results/result/zpid").InnerText);
                z.LinktoMap = root.SelectSingleNode("//response/results/result/links/mapthishome").InnerText;
                z.LinktoHomeDetails = root.SelectSingleNode("//response/results/result/links/homedetails").InnerText;


                var graphsanddata = root.SelectSingleNode("//response/results/result/links/graphsanddata");
                if (graphsanddata != null)
                {
                    z.LinktoGraphsAndData = root.SelectSingleNode("//response/results/result/links/graphsanddata").InnerText;
                }


                var comparables = root.SelectSingleNode("//response/results/result/links/comparables");
                if (comparables != null)
                {
                    z.LinktoComparables = root.SelectSingleNode("//response/results/result/links/comparables").InnerText;
                }
                else
                {
                    z.LinktoComparables = "";
                }

                //    var zestimate = root.SelectSingleNode("//response/results/result/zestimate/amount");
                if (root.SelectSingleNode("//response/results/result/zestimate/amount").InnerText.ToString().Length > 0)
                {
                    z.Estimate = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/amount").InnerText);
                    z.LastUpdated = DateTime.Parse(root.SelectSingleNode("//response/results/result/zestimate/last-updated").InnerText);


                }
                else
                {
                    z.Estimate = 0;
                }
                if (root.SelectSingleNode("//response/results/result/zestimate/valueChange").InnerText.ToString().Length > 0)
                {
                    z.ValueChange = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/valueChange").InnerText);
                    z.ValueChangeDurationInDays = int.Parse(root.SelectSingleNode("//response/results/result/zestimate/valueChange").Attributes["duration"].Value);
                    z.ValueRangeLow = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/valuationRange/low").InnerText);
                    z.ValueRangeHigh = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/valuationRange/high").InnerText);
                }
                else
                {
                    z.ValueChange = 0;
                }


                z.Street = root.SelectSingleNode("//response/results/result/address/street").InnerText;
                z.City = root.SelectSingleNode("//response/results/result/address/city").InnerText;
                z.State = root.SelectSingleNode("//response/results/result/address/state").InnerText;
                z.ZipCode = root.SelectSingleNode("//response/results/result/address/zipcode").InnerText;
                z.Latitude = decimal.Parse(root.SelectSingleNode("//response/results/result/address/latitude").InnerText);
                z.Longitude = decimal.Parse(root.SelectSingleNode("//response/results/result/address/longitude").InnerText);
                z.RequestURL = url.ToString();
            }
            else
            {
                z.ZillowId = 0;
                z.LinktoMap = "";
                z.LinktoHomeDetails = "";
                z.LinktoGraphsAndData = "";
                z.LinktoComparables = "";
                z.Estimate = 0;
                z.Street = "";
                z.City = "";
                z.State = "";
                z.ZipCode = "";
                z.RequestURL = "";

            }
            Response.Close();

            return z;
        }


        public static ZillowEstimate ZillowGetValuationChart(string zillowWebServiceId, string zpid)
        {
            //http://www.zillow.com/webservice/GetChart.htm?zws-id=X1-ZWz1cs7xazgk5n_6msnx&unit-type=percent&zpid=56777343&width=300&height=150

            var z = new ZillowEstimate();
            // Construct the url
            //    var zEstimate = new decimal();
            var url = String.Format("http://www.zillow.com/webservice/GetChart.htm?zws-id={0}&zpid={1}&unit-type=dollar&width=600&height=300&chartDuration=5years", zillowWebServiceId, zpid);


            // Make the HTTP request / get the response
            var Request = (System.Net.HttpWebRequest)WebRequest.Create(url);
            var Response = (HttpWebResponse)Request.GetResponse();

            // Parse the HTTP response into an XML document
            XmlDocument xml = new XmlDocument();
            xml.Load(Response.GetResponseStream());
            XmlElement root = xml.DocumentElement;

            //Return Code
            z.ReturnCode = int.Parse(root.SelectSingleNode("//message/code").InnerText);
            z.ReturnCodeMessage = root.SelectSingleNode("//message/text").InnerText;

            //NoDataAvailable.png
            //C:\inetpub\DNN55\Website\DesktopModules\GIBS\FlexMLS\Images
            if (z.ReturnCode == 0)
            {
                z.ChartValuation = root.SelectSingleNode("//response/url").InnerText;

                var graphsanddata = root.SelectSingleNode("//response/results/result/links/graphsanddata");
                if (graphsanddata != null)
                {
                    z.ChartLink = root.SelectSingleNode("//response/graphsanddata").InnerText;
                }
                else
                {
                    z.ChartLink = "";
                }


            }
            else
            {
                z.ChartValuation = "~/DesktopModules/GIBS/FlexMLS/Images/NoDataAvailable.png";
                z.ChartLink = "";
            }
            Response.Close();

            return z;
        }


        public static DataTable ZillowGetComps(string zillowWebServiceId, string zpid)
        {
            //http://www.zillow.com/webservice/GetDeepComps.htm?zws-id=X1-ZWz1cs7xazgk5n_6msnx&zpid=2107292608&count=5

            var z = new ZillowEstimate();

            // Construct the url
            //    var zEstimate = new decimal();
            //http://www.zillow.com/webservice/GetComps.htm?zws-id=X1-ZWz1cs7xazgk5n_6msnx&zpid=55871386&count=5
            ////////////////////////////////////////////////////////////////////////////////////////////////////

            var url = String.Format("http://www.zillow.com/webservice/GetDeepComps.htm?zws-id={0}&zpid={1}&count=5", zillowWebServiceId, zpid);


            // Make the HTTP request / get the response
            var Request = (System.Net.HttpWebRequest)WebRequest.Create(url);
            var Response = (HttpWebResponse)Request.GetResponse();

            // Parse the HTTP response into an XML document
            XmlDocument xml = new XmlDocument();

            xml.Load(Response.GetResponseStream());

            XmlElement root = xml.DocumentElement;
            //   root.read
            //Return Code
            z.ReturnCode = int.Parse(root.SelectSingleNode("//message/code").InnerText);
            z.ReturnCodeMessage = root.SelectSingleNode("//message/text").InnerText;

            DataTable dt;
            ////   dt.ReadXmlSchema
            //   dt.ReadXmlSchema(
            //   dt.ReadXmlSchema(HttpContext.Current.Server.MapPath(".") + "\\DesktopModules\\GIBS\\FlexMLS\\ZillowSchemas\\GetComps.xsd");

            if (z.ReturnCode == 0)
            {

                //xml.GetElementsByTagName("comparables");
                XmlNodeList xmlnode;
                xmlnode = xml.GetElementsByTagName("comparables");


                //     xmlnode = doc.SelectSingleNode("/result/address");

                //       XmlNode props = doc.SelectSingleNode("/result/address");

                ////     XmlNode props = root.SelectSingleNode("/entry/m:properties");
                //     int i = 0;
                //     string str = "<br />";

                //     for (i = 0; i <= xmlnode.Count - 1; i++)
                //     {
                //         xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
                //         str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + " <br /> " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + " <br /> " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim() + "<br /><br />";
                //         lblZpid.Text += str.ToString();
                //       //  MessageBox.Show(str);
                //     }

                //   lblZpid.Text += "<br />" + xml.SelectSingleNode("//response/properties/comparables/comp").InnerText;

                dt = ConvertXmlNodeListToDataTable(xmlnode);

                //       GridView1.DataSource = dt;
                //       //   GridView1.DataMember = "result";
                //       GridView1.DataBind();

                Response.Close();
                return dt;


            }

            else
            {
                dt = new DataTable("ResultTable");
                dt.Columns.Add("Result", typeof(string));

                DataRow dr = dt.NewRow();
                dr["Result"] = z.ReturnCodeMessage.ToString();
                dt.Rows.Add(dr);

                return dt;
            }



        }

        public static DataTable ConvertXmlNodeListToDataTable(XmlNodeList xnl)
        {
            DataTable dt = new DataTable();
            int TempColumn = 0;

            foreach (XmlNode node in xnl.Item(0).ChildNodes)
            {
                TempColumn++;
                DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));
                if (dt.Columns.Contains(node.Name))
                {
                    dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
                }
                else
                {
                    dt.Columns.Add(dc);
                }
            }

            int ColumnsCount = dt.Columns.Count;
            for (int i = 0; i < xnl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnsCount; j++)
                {
                    dr[j] = xnl.Item(i).ChildNodes[j].InnerText;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


        public static ZillowEstimate GetComps(string zillowWebServiceId, string zpid)
        {
            //http://www.zillow.com/webservice/GetComps.htm?zws-id=X1-ZWz1cs7xazgk5n_6msnx&zpid=2107292608&count=5

            var z = new ZillowEstimate();
            // Construct the url
            //    var zEstimate = new decimal();
            var url = String.Format("http://www.zillow.com/webservice/GetSearchResults.htm?zws-id={0}&zpid={1}", zillowWebServiceId, zpid);


            // Make the HTTP request / get the response
            var Request = (System.Net.HttpWebRequest)WebRequest.Create(url);
            var Response = (HttpWebResponse)Request.GetResponse();

            // Parse the HTTP response into an XML document
            XmlDocument xml = new XmlDocument();
            xml.Load(Response.GetResponseStream());
            XmlElement root = xml.DocumentElement;

            //Return Code
            z.ReturnCode = int.Parse(root.SelectSingleNode("//message/code").InnerText);
            z.ReturnCodeMessage = root.SelectSingleNode("//message/text").InnerText;


            if (z.ReturnCode == 0)
            {
                z.ZillowId = int.Parse(root.SelectSingleNode("//response/results/result/zpid").InnerText);
                z.LinktoMap = root.SelectSingleNode("//response/results/result/links/mapthishome").InnerText;
                z.LinktoHomeDetails = root.SelectSingleNode("//response/results/result/links/homedetails").InnerText;
                z.LinktoGraphsAndData = root.SelectSingleNode("//response/results/result/links/graphsanddata").InnerText;
                z.LinktoComparables = root.SelectSingleNode("//response/results/result/links/comparables").InnerText;
                z.Estimate = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/amount").InnerText);
                z.LastUpdated = DateTime.Parse(root.SelectSingleNode("//response/results/result/zestimate/last-updated").InnerText);
                z.ValueChange = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/valueChange").InnerText);
                z.ValueChangeDurationInDays = int.Parse(root.SelectSingleNode("//response/results/result/zestimate/valueChange").Attributes["duration"].Value);
                z.ValueRangeLow = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/valuationRange/low").InnerText);
                z.ValueRangeHigh = decimal.Parse(root.SelectSingleNode("//response/results/result/zestimate/valuationRange/high").InnerText);

                z.Street = root.SelectSingleNode("//response/results/result/address/street").InnerText;
                z.City = root.SelectSingleNode("//response/results/result/address/city").InnerText;
                z.State = root.SelectSingleNode("//response/results/result/address/state").InnerText;
                z.ZipCode = root.SelectSingleNode("//response/results/result/address/zipcode").InnerText;
                z.Latitude = decimal.Parse(root.SelectSingleNode("//response/results/result/address/latitude").InnerText);
                z.Longitude = decimal.Parse(root.SelectSingleNode("//response/results/result/address/longitude").InnerText);
            }
            Response.Close();

            return z;
        }

    }

}