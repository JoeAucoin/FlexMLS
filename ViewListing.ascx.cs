using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Common;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web;

using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using DotNetNuke.Framework.JavaScriptLibraries;


namespace GIBS.Modules.FlexMLS
{
    public partial class ViewListing : PortalModuleBase
    {

        public int mlNumber = 0;
        //  public string listingType = "";
        public string _returnURL = "";
        public bool _addTrendingListing = true;
        public string _ZillowUserId = "";
        public string _ZillowWebServiceId = "";
        static bool _ZillowAutoRunData = false;

        public string _Z_address = "";
        public string _Z_city = "";
        public string _Z_state = "";
        public string _Z_zip = "";
        public string ListingAddressForGMap = "";
    
        public string _GoogleMapAPIKey = "";
        public string _GoogleLatLon = "";
        static string _MLSImagesURL = "";

        //Your Zillow Web Services Identification (ZWSID) is: X1-ZWz1cs7xazgk5n_6msnx




        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _GoogleMapAPIKey = Settings["GoogleMapAPIKey"].ToString();


            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            JavaScript.RequestRegistration(CommonJs.DnnPlugins);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "galleriffic", (this.TemplateSourceDirectory + "/js/jquery.galleriffic.js"));
                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "opacityrollover", (this.TemplateSourceDirectory + "/js/jquery.opacityrollover.js"));
                Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Style", ("https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css"));


                LoadSettings();

                if (Request.QueryString["MLS"] != null)
                {
                    mlNumber = Int32.Parse(Request.QueryString["MLS"].ToString());
                }

                if (!IsPostBack)
                {


                    if (Request.UrlReferrer == null)
                    {
                        HyperLinkReturnURL.Visible = false;
                    }
                    else
                    {
                        _returnURL = Request.UrlReferrer.ToString();
                        HyperLinkReturnURL.Visible = true;
                        HyperLinkReturnURL.NavigateUrl = _returnURL.ToString();

                    }
                    // NEW SEARCH HyperLink
                    HyperLinkNewSearch.NavigateUrl = Globals.NavigateURL().ToString();



                    // DISABLE ADDING OF NEW RECORD IF COMING FROM TRENDING MODULE BY QUERYSTRING ADDITION OF . . . /t/f
                    if (Request.QueryString["t"] != null)
                    {
                        _addTrendingListing = false;
                    }

                    GetListing(mlNumber);
                    GetSeoValues();
                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        public void LoadSettings()
        {

            try
            {

                if (Settings.Contains("ZillowUserId"))
                {
                    _ZillowUserId = Settings["ZillowUserId"].ToString();
                }
                if (Settings.Contains("ZillowWebServiceId"))
                {
                    _ZillowWebServiceId = Settings["ZillowWebServiceId"].ToString();
                }
                if (Settings.Contains("ZillowAutoRunData"))
                {
                    _ZillowAutoRunData = Convert.ToBoolean(Settings["ZillowAutoRunData"].ToString());
                }

                if (Settings.Contains("MLSImagesUrl"))
                {
                    _MLSImagesURL = Settings["MLSImagesUrl"].ToString();
                }

                //FlexMLSSettings settingsData = new FlexMLSSettings(this.TabModuleId);

                //if (settingsData.ZillowUserId != null)
                //{
                //    _ZillowUserId = settingsData.ZillowUserId.ToString();

                //}
                //if (settingsData.ZillowWebServiceId != null)
                //{
                //    _ZillowWebServiceId = settingsData.ZillowWebServiceId.ToString();

                //}
                //if (settingsData.ZillowAutoRunData != null)
                //{
                //    _ZillowAutoRunData = Convert.ToBoolean(settingsData.ZillowAutoRunData);
                //}

                //          lblPopupContent.Text = Globals.NavigateURL(PortalSettings.LoginTabId) + " - " + Globals.NavigateURL(PortalSettings.RegisterTabId);


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //public void GetZillow_zpid(string _address, string _city, string _state, string _zip)
        //{
        //    try
        //    {

        //        // Build URL

        //        string _CityStateZip = HttpUtility.UrlEncode(_city.ToString()) + "+" + HttpUtility.UrlEncode(_state.ToString()) + "+" + HttpUtility.UrlEncode(_zip.ToString());

        //        var zEstimate = FlexMLS.Components.ZillowAPI.GetZestimate(_ZillowWebServiceId.ToString(), HttpUtility.UrlEncode(_address.ToString()), _CityStateZip.ToString().Trim());


        //        if (zEstimate.LinktoComparables.ToString().Length > 0)
        //        {
        //            zLinkcomps.NavigateUrl = zEstimate.LinktoComparables.ToString();
        //        }
        //        else
        //        {
        //            zLinkcomps.Visible = false;
        //        }

        //        //See more details for [address] on Zillow
        //        if (zEstimate.LinktoHomeDetails.ToString().Length > 0)
        //        {
        //            HyperLinkZillowHomeDetails.NavigateUrl = zEstimate.LinktoHomeDetails.ToString();
        //            HyperLinkZillowHomeDetails.Text = "See more details for " + _address.ToString() + " on Zillow";
        //            HyperLinkZillowHomeDetails.Target = "_blank";

        //        }
        //        else
        //        {
        //            HyperLinkZillowHomeDetails.Visible = false;
        //        }

        //        if (zEstimate.Estimate > 0)
        //        {
        //            lblzEstimate.Text = zEstimate.Estimate.ToString("C0");
        //        }
        //        else
        //        {
        //            lblzEstimate.Text = "None Available";
        //        }



        //        lblZpid.Text = zEstimate.RequestURL.ToString() + "<br />" + zEstimate.ZillowId.ToString() + " Zestimate: " + zEstimate.Estimate.ToString("C") + " Compariables: " + zEstimate.LinktoComparables.ToString();


        //        if (zEstimate.ZillowId.ToString().Length > 2)
        //        {
        //            var zChart = FlexMLS.Components.ZillowAPI.ZillowGetValuationChart(_ZillowWebServiceId.ToString(), zEstimate.ZillowId.ToString());

        //            imgValuationChart.ImageUrl = zChart.ChartValuation.ToString();
        //            zGraphsanddata.NavigateUrl = zChart.ChartLink.ToString();
        //        }
        //        //       XmlDocument doc = new XmlDocument();
        //        //       doc.Load(ZillowRequestURL.ToString());
        //        //       //try
        //        //       //{
        //        //       //    GridView1.DataSource = dt;
        //        //       // //   GridView1.DataMember = "result";
        //        //       //    GridView1.DataBind();
        //        //       //}

        //        //       //catch (Exception ex)
        //        //       //{
        //        //       //    lblZpid.Text = ex.ToString();
        //        //       //}

        //        //       XmlNodeList xmlnode;
        //        //       xmlnode = doc.GetElementsByTagName("result");

        //        //  //     xmlnode = doc.SelectSingleNode("/result/address");

        //        ////       XmlNode props = doc.SelectSingleNode("/result/address");

        //        //  ////     XmlNode props = root.SelectSingleNode("/entry/m:properties");
        //        //  //     int i = 0;
        //        //  //     string str = "<br />";

        //        //  //     for (i = 0; i <= xmlnode.Count - 1; i++)
        //        //  //     {
        //        //  //         xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
        //        //  //         str = xmlnode[i].ChildNodes.Item(0).InnerText.Trim() + " <br /> " + xmlnode[i].ChildNodes.Item(1).InnerText.Trim() + " <br /> " + xmlnode[i].ChildNodes.Item(2).InnerText.Trim() + "<br /><br />";
        //        //  //         lblZpid.Text += str.ToString();
        //        //  //       //  MessageBox.Show(str);
        //        //  //     }

        //        //       lblZpid.Text += "<br />" + doc.SelectSingleNode("//response/results/result/zpid").InnerText;

        //        //       DataTable dt = ConvertXmlNodeListToDataTable(xmlnode);

        //        //       GridView1.DataSource = dt;
        //        //       //   GridView1.DataMember = "result";
        //        //       GridView1.DataBind();





        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}


        //—-Method for convert xmlnode list to data table
        public DataTable ConvertXmlNodeListToDataTable(XmlNodeList xnl)
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




        //public void GetZillowMortgageCalc(double HousePrice)
        //{
        //    try
        //    {
        //        string MorgCalc;   
        //        string RateChart;

        //        MorgCalc = "<div id=\"verticalWidget\" style=\"float:left;width:192px;overflow:hidden;text-align:center;font-family:verdana,arial,sans-serif;font-size:8pt;line-height:13x;background-color:#dbdbdb;letter-spacing:0;text-transform:none;border-radius: 5px;webkit-border-radius:5px;\"><div style=\"margin:6px 0;\"><a href=\"https://www.zillow.com/mortgage-calculator/\" target=\"_blank\" title=\"Mortgage Calculators on Zillow\" style=\"font-family:Arial;font-size:15px;text-decoration:none;font-weight:bold;color:#666666;cursor: pointer;display: block;text-align: center;text-shadow: 0 1px #fff;\">Estimate Payment</a></div><div style=\"width:176px;margin:0 auto;background-color:#f4f4f4;text-align:left; font-size:8pt;border-radius: 5px; border: 1px solid;border-color:#cccccc;webkit-border-radius: 5px;padding: 0 1px;\"><iframe title=\"Mortgage Calculator\" frameborder=\"0\" height=\"470px\" style=\"float:left;\" scrolling=\"no\" width=\"176px\" src=\"https://www.zillow.com/mortgage/SmallMortgageLoanCalculatorWidget.htm?price=" + HousePrice.ToString() + "&ezuid=" + _ZillowUserId.ToString() + "&wtype=spc&rid=102001&wsize=small&textcolor=666666&backgroundColor=f4f4f4&advTabColor=9b9b9b&bgcolor=dbdbdb&bgtextcolor=666666&headerTextShadow=fff&widgetOrientationType=verticalWidget\"> Your browser doesn't support frames. Visit <a href=\"https://www.zillow.com/mortgage-calculator/\" target=\"_blank\" style=\"text-decoration:none; font-size:9pt; font-weight:bold;\">Zillow Mortgage Calculators</a> to see this content. </iframe><div style=\"clear:both;\"></div></div><div style=\"height:20px;\"><span style=\"display:block;margin:0 auto;font-size:7pt;height:15px;width:178px;color:#666666;padding-top:2px;\"><a href=\"https://www.zillow.com/mortgage-rates/\" target=\"_blank\" title=\"Zillow Mortgage Marketplace\" style=\"text-decoration:none;color:#3366bb;font-weight:normal;font-family:verdana,arial,sans-serif;font-size:7pt;color:#666666;\">Zillow Mortgage Calculator</a></span></div></div>";
        //        RateChart = "<div id=\"mtg-rate-table\" style=\"float:left;margin-left:20px\"><div id=\"large-rate-table-iframe-widget-container\" style=\"width:308px;border: 1px solid #bfbfbf;padding:12px;margin:0;font:normal normal normal 8pt verdana,arial,sans-serif;text-transform:none;text-indent:0;line-height:13px;background-color:#fff;letter-spacing:0;color: #555;overflow:hidden;text-align:center;\"><div id=\"title\" style=\"margin:5px 0 12px;text-align:left;\"><h1 style=\"padding-top:0px;text-align:left;font-family: Arial,verdana,sans-serif;margin:0\"><a href=\"https://www.zillow.com/mortgage-rates/\" target=\"_blank\" style=\"color:#000;font-style:normal;font-weight:bold;font-size:20px;padding:0;text-decoration:none;\">Zillow Mortgage Rates</a></h1></div><iframe src=\"https://www.zillow.com/webtools/widgets/RateTableDistributionWidget.htm?mp=RD-FLNVMXZ&\" width=\"306\" height=\"215\" frameborder=\"0\" scrolling=\"no\" style=\"display:block;\"></iframe></div></div>";

        //        LiteralMortgageCalc.Text = MorgCalc.ToString() + RateChart.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}

        public void AddViewListingRecord(int listingNumber, double listingPrice, string content)
        {
            try
            {
                FlexMLSController controller = new FlexMLSController();

                FlexMLSInfo item = new FlexMLSInfo();
                // FBClientsInfo item = new FBClientsInfo();
                item.ListingNumber = listingNumber.ToString();
                item.ModuleId = this.ModuleId;
                item.Content = content.ToString();
                item.CreatedByUserID = this.UserId;
                item.ListingPrice = listingPrice;

                controller.FlexMLS_ListingViews_Add(item);

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void AddToTableCondo(string labelName, string labelValue)
        {
            try
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb_l = new HtmlTableCell();
                HtmlTableCell tb_r = new HtmlTableCell();
                tb_l.Attributes.Add("class", "featurelabel");
                tb_r.Attributes.Add("class", "featuredata");
                tb_l.InnerHtml = labelName.ToString();
                tb_r.InnerHtml = labelValue.ToString();
                tRow.Controls.Add(tb_l);
                tRow.Controls.Add(tb_r);

                tblCondoSpecific.Rows.Add(tRow);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void AddToTableGeneral(string labelName, string labelValue)
        {
            try
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb_l = new HtmlTableCell();
                HtmlTableCell tb_r = new HtmlTableCell();
                tb_l.Attributes.Add("class", "featurelabel");
                tb_r.Attributes.Add("class", "featuredata");
                tb_l.InnerHtml = labelName.ToString();
                tb_r.InnerHtml = labelValue.ToString();
                tRow.Controls.Add(tb_l);
                tRow.Controls.Add(tb_r);
                tblGeneral.Rows.Add(tRow);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void AddToTableInterior(string labelName, string labelValue)
        {
            try
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb_l = new HtmlTableCell();
                HtmlTableCell tb_r = new HtmlTableCell();
                tb_l.Attributes.Add("class", "featurelabel");
                tb_r.Attributes.Add("class", "featuredata");
                tb_l.InnerHtml = labelName.ToString();
                tb_r.InnerHtml = labelValue.ToString();
                tRow.Controls.Add(tb_l);
                tRow.Controls.Add(tb_r);
                tblInterior.Rows.Add(tRow);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        //tblExterior
        public void AddToTableExterior(string labelName, string labelValue)
        {
            try
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb_l = new HtmlTableCell();
                HtmlTableCell tb_r = new HtmlTableCell();
                tb_l.Attributes.Add("class", "featurelabel");
                tb_r.Attributes.Add("class", "featuredata");
                tb_l.InnerHtml = labelName.ToString();
                tb_r.InnerHtml = labelValue.ToString();
                tRow.Controls.Add(tb_l);
                tRow.Controls.Add(tb_r);
                tblExterior.Rows.Add(tRow);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void AddToTableNeighborhood(string labelName, string labelValue)
        {
            try
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb_l = new HtmlTableCell();
                HtmlTableCell tb_r = new HtmlTableCell();
                tb_l.Attributes.Add("class", "featurelabel");
                tb_r.Attributes.Add("class", "featuredata");
                tb_l.InnerHtml = labelName.ToString();
                tb_r.InnerHtml = labelValue.ToString();
                tRow.Controls.Add(tb_l);
                tRow.Controls.Add(tb_r);
                tblNeighborhood.Rows.Add(tRow);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        //public void AddToTableSchools(string labelName, string labelValue)
        //{
        //    try
        //    {
        //        HtmlTableRow tRow = new HtmlTableRow();
        //        HtmlTableCell tb_l = new HtmlTableCell();
        //        HtmlTableCell tb_r = new HtmlTableCell();
        //        tb_l.Attributes.Add("class", "featurelabel");
        //        tb_r.Attributes.Add("class", "featuredata");
        //        tb_l.InnerHtml = labelName.ToString();
        //        tb_r.InnerHtml = labelValue.ToString();
        //        tRow.Controls.Add(tb_l);
        //        tRow.Controls.Add(tb_r);
        //        tblSchools.Rows.Add(tRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}	

        public void AddToTableTaxAssessment(string labelName, string labelValue)
        {
            try
            {
                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb_l = new HtmlTableCell();
                HtmlTableCell tb_r = new HtmlTableCell();
                tb_l.Attributes.Add("class", "featurelabel");
                tb_r.Attributes.Add("class", "featuredata_alignright");
                tb_l.InnerHtml = labelName.ToString();
                tb_r.InnerHtml = labelValue.ToString();
                tRow.Controls.Add(tb_l);
                tRow.Controls.Add(tb_r);
                tblTaxAssessment.Rows.Add(tRow);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        //linkButtonFavoritesAddListing_Click
        protected void linkButtonFavoritesAddListing_Click(object sender, EventArgs e)
        {
            try
            {
                int MLSnumber = 0;

                if (Request.IsAuthenticated)
                {

                    LinkButton myButton = sender as LinkButton;

                    if (myButton != null)
                    {
                        MLSnumber = Convert.ToInt32(myButton.CommandArgument);
                    }

                    FlexMLSController controller = new FlexMLSController();
                    FlexMLSInfo item = new FlexMLSInfo();

                    item.Favorite = MLSnumber.ToString();
                    item.FavoriteType = "Listing";
                    item.ModuleId = this.ModuleId;
                    item.UserID = this.UserId;
                    item.EmailSearch = false;

                    controller.FlexMLS_Favorites_Add(item);

                    myButton.Text = "SAVED! - " + item.Favorite.ToString();
                    myButton.ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    LinkButton myButton = sender as LinkButton;
                    myButton.Text = "Register to Use This Feature";
                    myButton.ForeColor = System.Drawing.Color.Red;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowPopup();", true);
                }

                //RELOAD LISTING 
                GetListing(MLSnumber);
                GetSeoValues();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetListing(int mlsNumber)
        {

            try
            {

                FlexMLSController controller = new FlexMLSController();

                FlexMLSInfo item = controller.FlexMLS_Get_Listing(mlsNumber);

                if (item != null)
                {
                    string _UnitNumber = "";
                    if (item.UnitNumber.ToString().Length >= 1 && item.UnitNumber.ToString() != "0")
                    {
                        _UnitNumber = " " + item.UnitNumber.ToString();
                    }

                    lblListingAddress.Text = item.Address.ToString() + _UnitNumber.ToString() + ", " + item.Village.ToString() + ", " + item.State.ToString() + " " + item.ZipCode.ToString(); ;
                    lblListingPrice.Text = String.Format("{0:C0}", item.ListingPrice); //;
                    ListingAddressForGMap = (lblListingAddress.Text.ToString()).Replace(" ", "%20").ToString();
                    //41.674677,-69.990018
                    
                    _GoogleLatLon = item.Latitude.ToString() + "," + item.Longitude.ToString();

                    //         GetZillowMortgageCalc(Double.Parse(item.ListingPrice.ToString()));

                    ModuleConfiguration.ModuleTitle = "MLS# " + item.ListingNumber.ToString();  // + " - " + lblListingAddress.Text.ToString();

                    // TRENDING LISTINGS
                    if (_addTrendingListing)
                    {
                        string AddListingRecordContent = lblListingAddress.Text.ToString();
                        AddViewListingRecord(Int32.Parse(item.ListingNumber.ToString()), item.ListingPrice, AddListingRecordContent.ToString());
                    }
                    else
                    {
                        // DO NOTHING
                    }

                    // LOOK FOR PRICE REDUCTION
                    if (item.ListingPrice < item.OriginalListPrice)
                    {
                        int _priceChange = Int32.Parse(item.OriginalListPrice.ToString()) - Int32.Parse(item.ListingPrice.ToString());
                        string _priceChangeAmt = String.Format("{0:C0}", _priceChange);

                        imgPriceChange.Visible = true;
                        imgPriceChange.ToolTip = _priceChangeAmt.ToString() + " Price Reduction";
                    }
                    else
                    {
                        imgPriceChange.Visible = false;
                    }

                    // LOOK FOR VIRTUAL TOUR
                    if (item.VirtualTourLink.ToString().Trim() != "")
                    {
                        HyperLinkVirtualTourLink.Visible = true;
                        ImageVirtualTour.Visible = true;
                        HyperLinkVirtualTourLink.NavigateUrl = item.VirtualTourLink.ToString();
                        HyperLinkVirtualTourLink.ToolTip = "Virtual Tour for MLS# " + item.ListingNumber.ToString();
                    }
                    else
                    {
                        ImageVirtualTour.Visible = false;
                        HyperLinkVirtualTourLink.Visible = false;
                    }

                    if (_ZillowAutoRunData == true)
                    {
                        // ZILLOW DATA
                   //     ZillowData.Visible = true;
                   //     GetZillow_zpid(item.Address.ToString() + _UnitNumber.ToString(), item.Village.ToString(), "MA", item.ZipCode.ToString());

                    }
                    else
                    {
                        //    ZillowData.Visible = false;

                        _Z_address = item.Address.ToString() + _UnitNumber.ToString();
                        _Z_city = item.Village.ToString();
                        _Z_state = "MA";
                        _Z_zip = item.ZipCode.ToString();

                    }



                    //HyperLinkInquiry - CONTACT FORM

                    string InquiryLink = Globals.NavigateURL("View", "pg", "Contact", "MLS", item.ListingNumber.ToString());
                    InquiryLink = InquiryLink.ToString().Replace("ctl/View/", "");
                    HyperLinkInquiry.NavigateUrl = InquiryLink.ToString();

                    //HyperLinkShowing - SCHEDULE A SHOWING

                    string ShowingLink = Globals.NavigateURL("View", "pg", "Contact", "MLS", item.ListingNumber.ToString(), "Schedule", "Showing");
                    ShowingLink = ShowingLink.ToString().Replace("ctl/View/", "");
                    HyperLinkShowing.NavigateUrl = ShowingLink.ToString();

                    // linkButtonFavoritesAddListing - ADD TO FAVORITES

                    linkButtonFavoritesAddListing.CommandArgument = item.ListingNumber.ToString();

                    //HyperLinkInquiry - TELL A FRIEND FORM
                    
                    if (Request.IsAuthenticated)
                    {
                        //string TellAFrinedLink = "";

                        HyperLinkTellAFriend.NavigateUrl = Globals.NavigateURL("View", "pg", "TellAFriend", "MLS", item.ListingNumber.ToString()).Replace("ctl/View/", "");
                    }
                    else
                    {
                        HyperLinkTellAFriend.Visible = false;
                    }

                    // GENERAL TABLE

                    if (item.PropertySubType1.ToString() == "Condominium" || item.FeeAmount > 0)
                    {
                        PanelCondoSpecific.Visible = true;
                        
                        if (item.Complex.ToString().Length > 1)
                        {
                            AddToTableCondo("Complex Name", item.Complex.ToString());  //CONDO COMPLEX NAME
                        }

                        if (item.MaxNumberOfUnits.ToString().Length > 1)
                        {
                            AddToTableCondo("# of Units", item.MaxNumberOfUnits.ToString());
                        }
                        if (item.FeeAmount > 0)
                        {
                            AddToTableCondo("Fees", String.Format("{0:C0}", item.FeeAmount) + " " + item.FeeFrequency.ToString());
                        }
                        if (item.AssocFeeYear.ToString().Length > 1)
                        {
                            AddToTableCondo("Year", item.AssocFeeYear.ToString());
                        }
                        if (item.Amenities.ToString().Length > 1)
                        {
                            AddToTableCondo("Fee Includes", AddSpaceAfterComma(item.Amenities.ToString()));
                        }
                        if (item.UnitPlacement.ToString().Length > 1)
                        {
                            AddToTableCondo("Unit Placement", AddSpaceAfterComma(item.UnitPlacement.ToString()));
                        }

                        if (item.Restrictions.ToString().Length > 1)
                        {
                            AddToTableCondo("Restrictions", AddSpaceAfterComma(item.Restrictions.ToString()));
                        }


                    }



                    if (item.YearBuilt.ToString().Length > 1)
                    {
                        AddToTableGeneral("Year Built", item.YearBuilt.ToString());
                    }
                    if (item.Style.Length > 1)
                    {
                        AddToTableGeneral("Style", item.Style.ToString());
                    }

                    if (item.LivingSpace.ToString().Length > 1)
                    {
                        AddToTableGeneral("Living Area", String.Format("{0,000}", item.LivingSpace).ToString() + " Sq. Ft.");
                    }

                    if (item.Bedrooms > 0)
                    {
                        AddToTableGeneral("Bedrooms", item.Bedrooms.ToString());
                    }
                    if (item.FullBaths > 0)
                    {
                        AddToTableGeneral("Full Baths", item.FullBaths.ToString());
                    }
                    if (item.HalfBaths > 0)
                    {
                        AddToTableGeneral("Half Baths", item.HalfBaths.ToString());
                    }


                    double lot_sqft = item.Acres;
                    if (lot_sqft > 0)
                    {
                        AddToTableGeneral("Lot Size", Math.Round(lot_sqft, 2).ToString() + " Acres");
                        // lblLotSquareFootage.Text = Math.Round(sqft, 2).ToString() + " Acres";
                    }


                    if (item.Zoning.Length > 1)
                    {
                        AddToTableGeneral("Zoning", item.Zoning.ToString());
                    }


                    if (item.FloodInsuranceRequired.Length > 1)
                    {
                        AddToTableGeneral("Flood Insurance Required", item.FloodInsuranceRequired.ToString());
                    }

                    //[Subdivision]
                    if (item.Subdivision.Length > 1)
                    {
                        AddToTableNeighborhood("Subdivision", AddSpaceAfterComma(item.Subdivision.ToString()));
                    }


                    if (item.WaterfrontYN.Length > 1)
                    {
                        AddToTableGeneral("Waterfront", item.WaterfrontYN.ToString());
                    }

                    if (item.WaterViewYN.Length > 1)
                    {
                        AddToTableGeneral("Water Views", item.WaterViewYN.ToString());
                    }

                    if (item.BeachDescription.Length > 1)
                    {
                        AddToTableGeneral("Beach Description", AddSpaceAfterComma(item.BeachDescription.ToString()));
                    }
                    if (item.BeachLakePond.Length > 1)
                    {
                        AddToTableGeneral("Beach/Lake/Pond Name", item.BeachLakePond.ToString());
                    }
                    if (item.BeachOwnership.Length > 1)
                    {
                        AddToTableGeneral("Beach Ownership", item.BeachOwnership.ToString());
                    }

                    // Neighborhood TABLE
                    if (item.ConvenientTo.Length > 1)
                    {
                        AddToTableNeighborhood("Convenient To", AddSpaceAfterComma(item.ConvenientTo.ToString()));
                    }
                    //NeighborhoodAmenities
                    if (item.NeighborhoodAmenities.Length > 1)
                    {
                        AddToTableNeighborhood("Neighborhood Amenities", AddSpaceAfterComma(item.NeighborhoodAmenities.ToString()));
                    }
                    //SchoolDistrict
                    if (item.SchoolDistrict.Length > 1)
                    {
                        AddToTableNeighborhood("School District", item.SchoolDistrict.ToString());
                    }



                    //lblBedsBaths
                    string _S_baths = "";
                    string _S_halfbaths = "";
                    string _S_beds = "";

                    string _BedsAndBaths = "";
                    if (item.Bedrooms > 1)
                    {
                        _S_beds = "s";
                    }
                    if (item.FullBaths > 1)
                    {
                        _S_baths = "s";

                    }
                    if (item.HalfBaths > 1)
                    {
                        _S_halfbaths = "s";
                    }

                    if (item.Bedrooms > 0)
                    {
                        _BedsAndBaths = item.Bedrooms.ToString() + " Bedroom" + _S_beds.ToString();
                    }
                    if (item.FullBaths > 0)
                    {
                        _BedsAndBaths = _BedsAndBaths.ToString() + " - " + item.FullBaths.ToString() + " Full Bath" + _S_baths.ToString();
                    }
                    if (item.HalfBaths > 0)
                    {
                        _BedsAndBaths = _BedsAndBaths.ToString() + " & " + item.HalfBaths.ToString() + " Half Bath" + _S_halfbaths.ToString();
                    }


                    lblSummary.Text = "MLS #: " + item.ListingNumber.ToString() + " | " + item.PropertySubType1.ToString();

                    if (_BedsAndBaths.ToString().Length > 0)
                    {
                        lblSummary.Text = lblSummary.Text.ToString() + " | " + _BedsAndBaths.ToString();
                    }

                    lblMarketingRemarks.Text = item.PublicRemarks.ToString();

                    lblListingOfficeName.Text = item.ListingOfficeName.ToString();







                    // TAX ASSESSMENT TABLE
                    if (item.LandAssessment > 1)
                    {
                        AddToTableTaxAssessment("Land", String.Format("{0:C0}", item.LandAssessment));
                    }
                    if (item.ImprovementAssessment > 1)
                    {
                        AddToTableTaxAssessment("Improvement", String.Format("{0:C0}", item.ImprovementAssessment));
                    }
                    if (item.OtherAssessment > 1)
                    {
                        AddToTableTaxAssessment("Other", String.Format("{0:C0}", item.OtherAssessment));
                    }
                    if (item.TotalAssessments > 1)
                    {
                        AddToTableTaxAssessment("Total Assessments", String.Format("{0:C0}", item.TotalAssessments));
                    }

                    //if (item.TaxYear.Length > 1)
                    //{
                    //    AddToTableTaxAssessment("Tax Year", item.TaxYear.ToString());
                    //}

                    if (item.Taxes > 0)
                    {
                        AddToTableTaxAssessment(item.TaxYear.ToString() + " Taxes", String.Format("{0:C0}", item.Taxes));
                    }

                    //  item.TaxRate


                    if (item.PropertySubType1.ToString() == "Single Family Residence" || item.PropertySubType1.ToString() == "Condominium")
                    {

                        if (item.InteriorFeatures.Length > 1)
                        {
                            AddToTableInterior("Interior Features", AddSpaceAfterComma(item.InteriorFeatures.ToString()));
                        }




                        if (item.KitchenFeatures.Length > 1)
                        {
                            AddToTableInterior("Kitchen Features", AddSpaceAfterComma(item.KitchenFeatures.ToString()));
                        }
                        // INTERIOR FEATURES TABLE
                        if (item.Appliances.Length > 1)
                        {
                            AddToTableInterior("Appliances", AddSpaceAfterComma(item.Appliances.ToString()));
                        }
                        if (item.FamilyRoomFeatures.Length > 1)
                        {
                            AddToTableInterior("Family Room", AddSpaceAfterComma(item.FamilyRoomFeatures.ToString()));
                        }

                        //if (item.MasterBedroom.Length > 5)
                        //{
                        //    AddToTableInterior("Master Bedroom", item.MasterBedroom.ToString().Replace("0x0 ", ""));
                        //}
                        //MasterBedroomFeatures
                        if (item.MasterBedroomFeatures.Length > 1)
                        {
                            AddToTableInterior("Master Bedroom Features", AddSpaceAfterComma(item.MasterBedroomFeatures.ToString()));
                        }

                        if (item.Bedroom2Features.Length > 1)
                        {
                            AddToTableInterior("Bedroom 2 Features", item.Bedroom2Features.ToString());
                        }

                        if (item.Heating.Length > 1)
                        {
                            AddToTableInterior("Heating", item.Heating.ToString());
                        }
                        if (item.FuelType.Length > 1)
                        {
                            AddToTableInterior("Fuel Type", AddSpaceAfterComma(item.FuelType.ToString()));
                        }
                        if (item.Cooling.Length > 1)
                        {
                            AddToTableInterior("Cooling", item.Cooling.ToString());
                        }

                        if (item.HotWater.Length > 1)
                        {
                            AddToTableInterior("Hot Water", item.HotWater.ToString());
                        }

                        if (item.AsbestosYN.Length > 1)
                        {
                            AddToTableInterior("Asbestos", item.AsbestosYN.ToString());
                        }

                        if (item.LeadPaint.Length > 1)
                        {
                            AddToTableInterior("Lead Paint", item.LeadPaint.ToString());
                        }

                        // EXTERIOR FEATURES TABLE
                        if (item.ExteriorFeatures.Length > 1)
                        {
                            AddToTableExterior("Exterior Features", AddSpaceAfterComma(item.ExteriorFeatures.ToString()));
                        }
                        if (item.Foundation.Length > 1)
                        {
                            AddToTableExterior("Foundation", AddSpaceAfterComma(item.Foundation.ToString()));
                        }
                        //Roofing
                        if (item.Roofing.Length > 1)
                        {
                            AddToTableExterior("Roofing", AddSpaceAfterComma(item.Roofing.ToString()));
                        }


                        PanelResiInteriorFeatures.Visible = true;

                    }



                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        lblSummary.Text += " | Days on Market: " + item.DOM.ToString();
                        if (item.OriginalListPrice > 0)
                        {
                            lblSummary.Text += " | Original List Price: " + String.Format("{0:C0}", Convert.ToDouble(item.OriginalListPrice.ToString()));
                        }
                    
                    }
                    else
                    {
                        lblSummary.Text = lblSummary.Text.ToString();
                    }

                    //lblSummary.Text


          //          BuildGoogleMap(item.Latitude, item.Longitude);
                    BuildPictureGallery(item.PictureCount, item.ListingNumber.ToString());



                }
                else
                {
                    Response.Redirect(Globals.NavigateURL("","Error=Listing+Removed"), true);
                    lblListingAddress.Text = "Error! Listing cannot be found.";
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public string AddSpaceAfterComma(string myInput)
        {

            try
            {
                String text = myInput.ToString();
                text = Regex.Replace(text, @",(?!\s)", x => x + " ");
                return text;
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return "";
            }

        }


        //public void BuildGoogleMap(double Latitude, double Longitude)
        //{

        //    try
        //    {

        //        GMap1.setCenter(new GLatLng(Latitude, Longitude), 18);
        //        GMap1.mapType = GMapType.GTypes.Hybrid;
        //        GMap1.Add(GMapType.GTypes.Normal);      //.addMapType(GMapType.GTypes.Physical);
        //  //      GMap1.Add(GMapType.GTypes.Physical);

        //        GControl control = new GControl(GControl.preBuilt.LargeMapControl);
        //   //     GControl control2 = new GControl(GControl.preBuilt.MenuMapTypeControl, new GControlPosition(GControlPosition.position.Top_Left));
                
        //        GMap1.Add(control);
        //   //     GMap1.Add(control2);

        //        GLatLng latlng = new GLatLng(Latitude, Longitude);

        //        string vBubbleText = lblListingAddress.Text.ToString() + "<br />" + lblSummary.Text.ToString().Replace(" | ", "<br />");
        //        //XPinLetter xPinLetter = new XPinLetter(PinShapes.pin_star, "A", Color.Red, Color.White, Color.Black);
        //       // XPinLetter xPinLetter = new XPinLetter(PinShapes.pin_star, "+", Color.Red, Color.White, Color.Gold);


        //        GMarker marker = new GMarker(latlng, new GMarkerOptions(new GIcon("https://chart.apis.google.com/chart?chst=d_map_xpin_letter&chld=pin_star|+|FF0000|FFFFFF|FFD700")));

        //        //   GInfoWindowOptions windowOptions = new GInfoWindowOptions();
        //        GInfoWindow commonInfoWindow = new GInfoWindow(marker, vBubbleText.ToString(), true);
        //        GMap1.Add(commonInfoWindow);



        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}

        public void BuildPictureGallery(int PictureCount, string MLSNumber)
        {
            //
            try
            {
                string checkImage = _MLSImagesURL + MLSNumber.ToString() + ".jpg";

                string checkImage_1 = _MLSImagesURL + MLSNumber.ToString() + "_1.jpg";


                //   StringBuilder sb = new StringBuilder();
                StringBuilder photorama = new StringBuilder();


                if (UrlExists(checkImage.ToString()) == true)
                {

                    for (int i = 0; i < PictureCount; i++)
                    {

                        if (i > 0)
                        {
                            string s = String.Format("{0:00}", i);
                           
                            photorama.Append("<img src=\"" + _MLSImagesURL + MLSNumber.ToString() + "_" + s.ToString() + ".jpg\" alt=\"Cape Cod MLS " + MLSNumber.ToString() + " Listing Picture\" width=\"80\" />");
                        }
                        else
                        {
                           
                            photorama.Append("<img src=\"" + _MLSImagesURL + MLSNumber.ToString() + ".jpg\" alt=\"Cape Cod MLS " + MLSNumber.ToString() + " Listing Picture\" width=\"80\" />");
                        }


                    }



                }

                else if (UrlExists(checkImage_1.ToString()) == true)
                {

                    for (int i = 1; i < (PictureCount + 1); i++)
                    {

                        if (i > 0)
                        {
                            //   string s = String.Format("{0:00}", i);
                       
                            photorama.Append("<img src=\"" + _MLSImagesURL + MLSNumber.ToString() + "_" + i.ToString() + ".jpg\" alt=\"Cape Cod MLS " + MLSNumber.ToString() + " Listing Picture\" width=\"80\" />");
                        }
                        else
                        {
       
                            photorama.Append("<img src=\"" + _MLSImagesURL + MLSNumber.ToString() + ".jpg\" alt=\"Cape Cod MLS " + MLSNumber.ToString() + " Listing Picture\" width=\"80\" />");
                        }


                    }

                }

                else
                {

        
                    photorama.Append("<img src=\"" + _MLSImagesURL+ "NoImage.jpg\" alt=\"Cape Cod MLS " + MLSNumber.ToString() + " Listing Picture\" width=\"80\" />");

                    ImageNeeded(MLSNumber.ToString());
                }



                LiteralPhotorama.Text = photorama.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void ImageNeeded(string listingNumber)
        {
            try
            {

                FlexMLSController controller = new FlexMLSController();
                FlexMLSInfo item = new FlexMLSInfo();

                item.ListingNumber = listingNumber;

                controller.FlexMLS_ImagesNeeded_Insert(item);

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }




        }


        private static bool UrlExists(string url)
        {
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }


        public void GetSeoValues()
        {

            try
            {



                DotNetNuke.Framework.CDefault GIBSpage = (DotNetNuke.Framework.CDefault)this.Page;
                GIBSpage.Title = lblListingAddress.Text.ToString() + " MLS " + mlNumber.ToString();
                GIBSpage.KeyWords = lblListingAddress.Text.ToString() + ", " + GIBSpage.KeyWords.ToString();
                GIBSpage.Description = lblListingAddress.Text.ToString() + ", MLS " + mlNumber.ToString() + ". " + lblMarketingRemarks.Text.ToString();   // +" " + Settings["PageTitle"].ToString() + ". " + Settings["PageDescription"].ToString();
                GIBSpage.Author = "Joseph M Aucoin, GIBS";



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //protected void btnLoadZillowData_Click(object sender, EventArgs e)
        //{

        //    ZillowData.Visible = true;
        //    GetZillow_zpid(_Z_address.ToString(), _Z_city.ToString(), _Z_state.ToString(), _Z_zip.ToString());
        //    ZillowDataButton.Visible = false;

        //    GetListing(mlNumber);
        //    //GetSeoValues(); 
        //}


    }
}