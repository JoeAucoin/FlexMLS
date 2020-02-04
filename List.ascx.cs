using System;
using System.Web;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using GIBS.Modules.FlexMLS.Components;

namespace GIBS.Modules.FlexMLS
{
    public partial class List : PortalModuleBase
    {

        string _village = "";
        string _town = "";
        string _beds = "0";
        string _baths = "0";
        string _waterfrontYN = "";
        string _waterviewYN = "";
        string _type = "";
        int _pricelow = 0;
        int _pricehigh = 50000000;
    //    string _returnURL = "";
        string _listingOfficeMLSID = "";
        string _complex = "";
        string _complexName = "";
        string _dom = "";
        bool _emailSearch = false;
        bool _condoSearch = false;
        string _lastModified = "01-01-1980";

        int _CurrentPage = 1;

        static string _maxThumbSize = "80";
        static string _MLSImagesURL = "";
        public bool _ShowListViewMap = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
           
            if (_ShowListViewMap == true)
            {
                string myAIPkey = Settings["GoogleMapAPIKey"].ToString();
                        
            }


        }



        protected void Page_Load(object sender, EventArgs e)
        {

            //  RegisterStyleSheet("", "");
            //      ClientResourceManager.RegisterStyleSheet(this.Page, this.Page.ResolveUrl("~/DesktopModules/GIBS/FlexMLS/CustomStyleSheet.css"), FileOrder.Css.ModuleCss);



            if (!IsPostBack)
            {
                LoadSettings();
                //  GetFavoritesModule();

                ModuleConfiguration.ModuleTitle = "Search Results";






                HyperLinkNewSearch.NavigateUrl = Globals.NavigateURL().ToString();

            }

            CheckQueryString();
            SearchMLS();


        }

        public string GetComplexName(string complexCode)
        {

            try
            {

                FlexMLSController controller = new FlexMLSController();

                FlexMLSInfo item = controller.FlexMLS_Get_CondoComplexName(complexCode);

                if (item != null)
                {
                    return item.Complex.ToString();
                }




                else
                {
                    //Response.Redirect(Globals.NavigateURL(), true);
                    return "NO RECORD FOUND";
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return "ERROR";
            }

        }

        public void CheckQueryString()
        {

            try
            {

                if (Request.QueryString["Complex"] != null)
                {
                    _complex = Request.QueryString["Complex"].ToString();
                    _complexName = GetComplexName(_complex.ToString());
                    _complex = _complex.ToString().Replace("_", " ").ToString().Replace("~", "/").ToString().Replace("^", "'").ToString();
                    _condoSearch = true;
                }

                if (Request.QueryString["Town"] != null)
                {
                    _town = Request.QueryString["Town"].ToString();
                }

                if (Request.QueryString["Village"] != null)
                {
                    _village = Request.QueryString["Village"].ToString();
                }

                if (Request.QueryString["Beds"] != null)
                {
                    _beds = Request.QueryString["Beds"].ToString();
                }

                if (Request.QueryString["Baths"] != null)
                {
                    _baths = Request.QueryString["Baths"].ToString();
                }

                if (Request.QueryString["WaterFront"] != null)
                {
                    _waterfrontYN = Request.QueryString["WaterFront"].ToString();
                }

                if (Request.QueryString["WaterView"] != null)
                {
                    _waterviewYN = Request.QueryString["WaterView"].ToString();
                }

                if (Request.QueryString["Type"] != null)
                {
                    _type = Request.QueryString["Type"].ToString();
                }

                if (Request.QueryString["Low"] != null)
                {
                    _pricelow = Int32.Parse(Request.QueryString["Low"].ToString());
                }

                if (Request.QueryString["High"] != null)
                {
                    _pricehigh = Int32.Parse(Request.QueryString["High"].ToString());
                }

                if (Request.QueryString["LOID"] != null)
                {
                    _listingOfficeMLSID = Request.QueryString["LOID"].ToString();
                }

                if (Request.QueryString["DOM"] != null)
                {
                    _dom = Request.QueryString["DOM"].ToString();
                }
                // listingOfficeMLSID
                if (Request.QueryString["e"] != null)
                {
                    _emailSearch = true;
                    _lastModified = Request.QueryString["e"].ToString();
                    string MyTitle = "";

                    // CHECK IF AUTHENTICATED
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        MyTitle = "Daily E-Mail Search Results for " + this.UserInfo.DisplayName.ToString();
                    }
                    else
                    {
                        MyTitle = "Daily E-Mail Search Results";
                    }

                    ModuleConfiguration.ModuleTitle = MyTitle.ToString();
                    linkButtonFavorites.Visible = false;


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

                if (Settings.Contains("MaxThumbSize"))
                {
                    _maxThumbSize = Settings["MaxThumbSize"].ToString();
                }
                if (Settings.Contains("MLSImagesUrl"))
                {
                    _MLSImagesURL = Settings["MLSImagesUrl"].ToString();
                }


            //    lblPopupContent.Text = Globals.NavigateURL(PortalSettings.LoginTabId) + " - " + Globals.NavigateURL(PortalSettings.RegisterTabId);


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public string GetPropertyTypeDesc(string PropType)
        {

            try
            {
                string myRetValue = "";
                switch (PropType)
                {
                    case "RESI":
                        myRetValue = "Residential";
                        break;
                    case "COND":
                        myRetValue = "Condominium";
                        break;
                    case "HOTL":
                        myRetValue = "Hotel/Motel";
                        break;
                    case "LOTL":
                        myRetValue = "Vacant Land";
                        break;
                    case "MULT":
                        myRetValue = "Multi-Family";
                        break;
                    case "COMM":
                        myRetValue = "Commercial";
                        break;
                    default:
                        myRetValue = "";
                        break;
                }
                return myRetValue.ToString();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return "Error";
            }

        }

        //public string GetStatusDesc(string Status)
        //{

        //    try
        //    {
        //        string myRetValue = "";
        //        switch (Status)
        //        {
        //            case "A":
        //                myRetValue = "Active";
        //                break;
        //            case "C":
        //                myRetValue = "Pending with Contingencies";
        //                break;

        //            case "R":
        //                myRetValue = "Listing Removed. Call agent for details!";
        //                break;

        //            default:
        //                myRetValue = "";
        //                break;
        //        }
        //        return myRetValue.ToString();


        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //        return "Error";
        //    }

        //}


        //public void GetFavoritesModule()
        //{

        //    try
        //    {


        //        DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
        //        ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, "GIBS - MLS Connect - Favorites");

        //        foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
        //        {
        //            if (!mi.IsDeleted)
        //            {
        //                linkButtonFavorites.Text = mi.ModuleID.ToString();
        //                // get module title 
        //                //mi.ModuleTitle;
        //                // additionally, you can find out what tab it is on //mi.TabID;
        //                //mi.ModuleID;
        //                lblDebug.Text = mi.ModuleID.ToString();
        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}




        public void SearchMLS()
        {

            try
            {

                int PageSize = 20;
                //Display 20 items per page
                //Get the currentpage index from the url parameter
                if (Request.QueryString["currentpage"] != null)
                {
                    _CurrentPage = Convert.ToInt32(Request.QueryString["currentpage"].ToString());
                }
                else
                {
                    _CurrentPage = 1;
                }




                //int _bedRooms = Convert.ToInt32(_beds.ToString());
                //int _bathRooms = Convert.ToInt32(_baths.ToString());
                //string _SearchWaterFront = "";
                //string _SearchWaterView = "";
                //int _priceLow = Convert.ToInt32(_pricelow.ToString());
                //int _priceHigh = Convert.ToInt32(_pricehigh.ToString());

                if (_waterfrontYN.ToString() == "True")
                {
                    _waterfrontYN = "Yes";
                }
                if (_waterviewYN.ToString() == "True")
                {
                    _waterviewYN = "Yes";
                }


                StringBuilder _SearchCriteria = new StringBuilder();
                _SearchCriteria.Capacity = 500;

                if (_type.ToString().Trim().Length > 0)
                {
                    _SearchCriteria.Append("Listing Type: <b>" + GetPropertyTypeDesc(_type.ToString()) + "</b> &nbsp;");
                }
                else
                {
                    _type = "";
                }


                if (_town.ToString().Trim().Length > 0)
                {
                    _SearchCriteria.Append(" Town: <b>" + _town.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _town = "";
                }

                if (_village.ToString().Trim().Length > 0)
                {
                    _SearchCriteria.Append(" Village: <b>" + _village.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _village = "";
                }

                if (_complex.ToString().Length > 0)
                {
                    _SearchCriteria.Append(" Complex: <b>" + _complexName.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _complex = "";
                }

                if (Int32.Parse(_beds.ToString()) > 0)
                {
                    _SearchCriteria.Append(" Bedrooms: <b>" + _beds.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _beds = "";
                }
                if (Int32.Parse(_baths.ToString()) > 0)
                {
                    _SearchCriteria.Append(" Bathrooms: <b>" + _baths.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _baths = "";
                }
                if (_waterfrontYN.ToString() == "Yes")
                {
                    _SearchCriteria.Append(" Waterfront: <b>" + _waterfrontYN.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _waterfrontYN = "";
                }

                if (_waterviewYN.ToString() == "Yes")
                {
                    _SearchCriteria.Append(" Waterview: <b>" + _waterviewYN.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _waterviewYN = "";
                }

                if (_pricelow > 0)
                {
                    _SearchCriteria.Append(" Min. Price: <b>" + _pricelow.ToString("C0") + "</b> &nbsp;");
                }

                if (_pricehigh < 50000000)
                {
                    _SearchCriteria.Append(" Max Price: <b>" + _pricehigh.ToString("C0") + "</b> &nbsp;");
                }


                if (_listingOfficeMLSID.ToString().Length > 0)
                {
                    _SearchCriteria.Append(" Office: <b>" + _listingOfficeMLSID.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _listingOfficeMLSID = "";
                }

                if (_dom.ToString().Trim().Length > 0)
                {
                    _SearchCriteria.Append(" Days On Market: <b>" + _dom.ToString() + " day or less</b> &nbsp;");
                }
                else
                {
                    _dom = "";
                }

                lblSearchCriteria.Text = _SearchCriteria.ToString();
                // DEBUG
                //lblDebug.Visible = true;
                //lblDebug.Text += "<br /> type:" + _type.ToString() + " town:" +
                //    _town.ToString() + " village:" +
                //    _village.ToString() + " beds:" +
                //    _beds.ToString() + " baths:" +
                //    _baths.ToString() + " waterfront:" +
                //    _waterfrontYN.ToString() + " waterview:" +
                //    _waterviewYN.ToString() + " pricelow:" +
                //    _pricelow.ToString() + " pricehigh:" +
                //    _pricehigh + " listingOff:" + _listingOfficeMLSID.ToString();

                List<FlexMLSInfo> items;
                FlexMLSController controller = new FlexMLSController();

                if (_condoSearch == true)
                {
                    items = controller.FlexMLS_Search_Condo(_type.ToString(),
                    _town.ToString(), _village.ToString(),
                    _beds.ToString(), _baths.ToString(),
                    _waterfrontYN.ToString(),
                    _waterviewYN.ToString(),
                    _pricelow.ToString(),
                    _pricehigh.ToString(),
                    _listingOfficeMLSID, _dom.ToString(), _complexName.ToString().Replace("'", "''").ToString());

                    //    lblDebug.Text = " Searching Condos";
                    GetSeoValues(_complexName.ToString() + " Condominium in " + _town.ToString(), _town.ToString() + ", " + _complexName.ToString() + ", condo, condominium");

                    string MyTitle = "";
                    MyTitle = _complexName.ToString() + " Condominium";
                    ModuleConfiguration.ModuleTitle = MyTitle.ToString();
                }

                else if (_emailSearch == true)
                {
                    // controller.FlexMLS_Search_LastModified
                    items = controller.FlexMLS_Search_LastModified(_type.ToString(),
                    _town.ToString(),
                    _village.ToString(),
                    _beds.ToString(),
                    _baths.ToString(),
                    _waterfrontYN.ToString(),
                    _waterviewYN.ToString(),
                    _pricelow.ToString(),
                    _pricehigh.ToString(), _listingOfficeMLSID.ToString(), _dom.ToString(), _lastModified.ToString(), _complex.ToString());
                }
                else
                {
                    items = controller.FlexMLS_Search(_type.ToString(),
                    _town.ToString(),
                    _village.ToString(),
                    _beds.ToString(),
                    _baths.ToString(),
                    _waterfrontYN.ToString(),
                    _waterviewYN.ToString(),
                    _pricelow.ToString(),
                    _pricehigh.ToString(), _listingOfficeMLSID.ToString(), _dom.ToString());
                }


                PagedDataSource objPagedDataSource = new PagedDataSource();
                objPagedDataSource.DataSource = items;

                if (items.Count == 0)
                {
                    //GMap1.Visible = false;
                }

                if (objPagedDataSource.PageCount > 0)
                {
                    objPagedDataSource.PageSize = PageSize;
                    objPagedDataSource.CurrentPageIndex = _CurrentPage - 1;
                    objPagedDataSource.AllowPaging = true;
                }

                lstSearchResults.DataSource = objPagedDataSource;
                lstSearchResults.DataBind();

                lblSearchSummary.Text = "Total Listings Found: " + items.Count.ToString();

                if (items.Count == 200)
                {
                    lblSearchSummary.Text = "Over 200 Listing Found! Search results are limited to 200 records. Please refine your criteria.";
                    lblSearchSummary.ForeColor = System.Drawing.Color.Red;
                }


                if (PageSize == 0 || items.Count <= PageSize)
                {
                    PagingControl1.Visible = false;
                }
                else
                {
                    PagingControl1.Visible = true;
                    PagingControl1.TotalRecords = items.Count;
                    PagingControl1.PageSize = PageSize;
                    PagingControl1.CurrentPage = _CurrentPage;
                    PagingControl1.TabID = TabId;
                    PagingControl1.QuerystringParams = "pg=List&" + GenerateQueryStringParameters(this.Request, "Town", "Village", "Beds", "Baths", "WaterFront", "WaterView", "Type", "Low", "High", "LOID", "DOM", "e");

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetSeoValues(string _pagetitle, string _keywords)
        {

            try
            {

                DotNetNuke.Framework.CDefault GIBSpage = (DotNetNuke.Framework.CDefault)this.Page;
                GIBSpage.Title = _pagetitle.ToString();
                GIBSpage.KeyWords = _pagetitle.ToString() + ", " + _keywords.ToString() + ", " + GIBSpage.KeyWords.ToString();
                GIBSpage.Description = _pagetitle.ToString() + ". " + GIBSpage.Description.ToString();
                GIBSpage.Author = "Joseph M Aucoin, GIBS";

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected static string GenerateQueryStringParameters(HttpRequest request, params string[] queryStringKeys)
        {
            StringBuilder queryString = new StringBuilder(64);
            foreach (string key in queryStringKeys)
            {
                if (request.QueryString[key] != null)
                {
                    if (queryString.Length > 0)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append(key).Append("=").Append(request.QueryString[key]);
                }
            }

            return queryString.ToString();
        }

        private string GetAdditionalQueryStringParams()
        {
            throw new NotImplementedException();
        }

        protected void cmdAddFavorite_Click(object sender, EventArgs e)
        {

        }


        //public void BuildGoogleMap(double Latitude, double Longitude, string BubbleText)
        //{

        //    try
        //    {

        //        GMap1.setCenter(new GLatLng(Latitude, Longitude), 14);


        //        GLatLng latlng = new GLatLng(Latitude, Longitude);

        //        string vBubbleText = BubbleText.ToString();    // lblListingAddress.Text.ToString() + "<br />" + lblSummary.Text.ToString();
              
        //        //  vBubbleText = "<p>JOE</p>";
        //        // https://chart.apis.google.com/chart?chst=d_map_xpin_letter&chld=pin_star|+|FF0000|FFFFFF|FFD700
        //        // GMarker marker = new GMarker(latlng, new GMarkerOptions(new GIcon(xPinLetter.ToString(), xPinLetter.Shadow())));

        //        GMarker marker = new GMarker(latlng, new GMarkerOptions(new GIcon("https://chart.apis.google.com/chart?chst=d_map_xpin_letter&chld=pin_star|+|FF0000|FFFFFF|FFD700")));
        //        GInfoWindowOptions windowOptions = new GInfoWindowOptions();
        //        GInfoWindow commonInfoWindow = new GInfoWindow(marker, vBubbleText.ToString(), false);
        //        GMap1.Add(commonInfoWindow);



        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}


        protected void lstSearchResults_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {

            try
            {

                if (_ShowListViewMap == true)
                {
                    //GMap1.mapType = GMapType.GTypes.Hybrid;
                    //GMap1.Add(GMapType.GTypes.Normal);      //.addMapType(GMapType.GTypes.Physical);
                    //GMap1.Add(GMapType.GTypes.Physical);
                }
                string _ListingNumber = "";
                string _PropertyType = "";


                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    _ListingNumber = DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString();
                    _PropertyType = DataBinder.Eval(e.Item.DataItem, "PropertyType").ToString();

                    // Retrieve the Hyperlink control in the current DataListItem.
                    HyperLink eLink = (HyperLink)e.Item.FindControl("hyperlinkListingDetail");
                    string _pageName = DataBinder.Eval(e.Item.DataItem, "Address").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + "_" + DataBinder.Eval(e.Item.DataItem, "Village").ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + ".aspx";
                    //string vLink = ""; // Globals.NavigateURL(this.TabId, "ViewListing", "mid", this.ModuleId.ToString(), "MLS", DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString());
                    //vLink = "/TabID/" + _ViewListingPage.ToString() + "/MLS/" + _ListingNumber.ToString() + "/Default.aspx";

                    string vLink = Globals.NavigateURL("View", "pg", "v", "MLS", _ListingNumber.ToString());
                    vLink = vLink.ToString().Replace("ctl/View/", "");
                    vLink = vLink.ToString().Replace("Default.aspx", _pageName.ToString());
                    eLink.NavigateUrl = vLink.ToString();


                    //   HyperLinkVirtualTourLink
                    HyperLink VirtualTourLink = (HyperLink)e.Item.FindControl("HyperLinkVirtualTourLink");
                    Image VirtualTourImage = (Image)e.Item.FindControl("ImageVirtualTour1");
                    if (DataBinder.Eval(e.Item.DataItem, "VirtualTourLink").ToString().Trim() != "")
                    {
                        VirtualTourImage.Visible = true;
                        VirtualTourLink.Visible = true;
                        VirtualTourLink.NavigateUrl = DataBinder.Eval(e.Item.DataItem, "VirtualTourLink").ToString();
                        VirtualTourLink.ToolTip = "Virtual Tour for MLS# " + _ListingNumber.ToString();
                    }
                    else
                    {
                        VirtualTourImage.Visible = false;
                        VirtualTourLink.Visible = false;
                    }
                    // END //HyperLinkVirtualTourLink

                    Label MLS = (Label)e.Item.FindControl("lblListingNumber");
                    //           MLS.Text = "MLS " + _ListingNumber.ToString();

                    // lblLotSquareFootage
                    Label LotSquareFootage = (Label)e.Item.FindControl("lblLotSquareFootage");
                    double sqft = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "LotSizeSqFt"));
                    if (sqft > 0)
                    {
                        LotSquareFootage.Text = Math.Round(sqft / 43560, 2).ToString() + " Acres";
                    }




                    if (_PropertyType.ToString().ToUpper() == "COND" || _PropertyType.ToString().ToUpper() == "COMM")
                    {
                        LotSquareFootage.Text = DataBinder.Eval(e.Item.DataItem, "Complex").ToString();    //CONDO COMPLEX NAME
                    }

                    Label ListingStatus = (Label)e.Item.FindControl("lblListingStatus");
                    string _listingstatus = DataBinder.Eval(e.Item.DataItem, "StatusCode").ToString();
                    ListingStatus.Text = _listingstatus.ToString();

                    Label MLNumber = (Label)e.Item.FindControl("lblMLNumber");
                    MLNumber.Text = "MLS # " + _ListingNumber.ToString();

                    //lblBedsBaths
                    string _S_baths = "";
                    string _S_beds = "";
                    string _S_halfbaths = "";
                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString()) > 1)
                    {
                        _S_beds = "s";
                    }
                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString()) > 1)
                    {
                        _S_baths = "s";
                    }

                    Label BedsBaths = (Label)e.Item.FindControl("lblBedsBaths");




                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString()) > 0)
                    {
                        BedsBaths.Text = DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString() + " Bed" + _S_beds.ToString();
                    }
                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString()) > 0)
                    {
                        BedsBaths.Text = BedsBaths.Text.ToString() + " - " + DataBinder.Eval(e.Item.DataItem, "FullBaths").ToString() + " Bath" + _S_baths.ToString();
                    }

                    if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "HalfBaths").ToString()) > 0)
                    {
                        BedsBaths.Text = BedsBaths.Text.ToString() + ", " + DataBinder.Eval(e.Item.DataItem, "HalfBaths").ToString() + " Half" + _S_halfbaths.ToString();
                    }
                    if (_PropertyType.ToString() == "COMM" || _PropertyType.ToString() == "MULT")
                    {
                        BedsBaths.Text = DataBinder.Eval(e.Item.DataItem, "Style").ToString();
                    }


                    // lblLivingSpace  
                    Label SquareFootage = (Label)e.Item.FindControl("lblLivingSpace");
                    int livingspace = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "LivingSpace").ToString());
                    if (livingspace > 0)
                    {
                        SquareFootage.Text = livingspace.ToString("##,###") + " Sqft.";
                    }
                    

                    // lblAddress

                    HyperLink Address = (HyperLink)e.Item.FindControl("hyperlinkListingAddress");
                    HyperLink City = (HyperLink)e.Item.FindControl("hyperlinkListingAddressCity");
                    //HyperLinkImage
                    HyperLink ImageLink = (HyperLink)e.Item.FindControl("HyperLinkImage");

                    string _UnitNumber = "";
                    if (DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString().Trim().Length >= 1 && DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString() != "0")
                    {
                        _UnitNumber = " #" + DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString() + " ";
                    }
                    City.Text = DataBinder.Eval(e.Item.DataItem, "Village").ToString() + ", MA";
                    City.NavigateUrl = vLink.ToString();
                    Address.Text = DataBinder.Eval(e.Item.DataItem, "Address").ToString() + _UnitNumber.ToString();
                    string BubbleAddress = Address.Text.ToString() + ", " + City.Text;
                    Address.NavigateUrl = vLink.ToString();
                    ImageLink.NavigateUrl = vLink.ToString();
                    // lblListingPrice
                    Label ListingPrice = (Label)e.Item.FindControl("lblListingPrice");
                    //  ListingPrice.Text = String.Format("{0:C0}",Int32.Parse(DataBinder.Eval(e.Item.DataItem, "ListingPrice").ToString()));
                    string vListingPrice = DataBinder.Eval(e.Item.DataItem, "ListingPrice").ToString();
                    string vOriginalListingPrice = DataBinder.Eval(e.Item.DataItem, "originalListPrice").ToString();
                    ListingPrice.Text = String.Format("{0:C0}", double.Parse(vListingPrice.ToString()));

                    //imgPriceChange
                    //   double.Parse
                    //   Double.Parse
                    Image PriceChangeImage = (Image)e.Item.FindControl("imgPriceChange");
                    if (double.Parse(vListingPrice.ToString()) < double.Parse(vOriginalListingPrice.ToString()))
                    {
                        double _priceChange = double.Parse(vOriginalListingPrice.ToString()) - double.Parse(vListingPrice.ToString());
                        string _priceChangeAmt = String.Format("{0:C0}", _priceChange);
                        PriceChangeImage.Visible = true;
                        PriceChangeImage.ToolTip = _priceChangeAmt.ToString() + " Price Reduction";
                    }
                    else
                    {
                        PriceChangeImage.Visible = false;
                    }


                    //lblYearBuilt
                    Label YearBuilt = (Label)e.Item.FindControl("lblYearBuilt");
                    YearBuilt.Text = "Built In " + DataBinder.Eval(e.Item.DataItem, "YearBuilt").ToString();

                    // PropType
                    Label PropType = (Label)e.Item.FindControl("lblPropType");
                    PropType.Text = DataBinder.Eval(e.Item.DataItem, "PropertySubType1").ToString();


                    // CHECK FOR LAND LISTING    ---     || _PropertyType.ToString() == "MULT"
                    if (_PropertyType.ToString() == "LOTL")
                    {
                        YearBuilt.Text = "";    // DataBinder.Eval(e.Item.DataItem, "PropertySubType1").ToString();
                    }


                    // FIND fotorama DIV

                    HtmlGenericControl DIVfotorama = e.Item.FindControl("fotorama") as HtmlGenericControl;
                   // ListingStatus

                //    DIVfotorama.Attributes.Add("data-minwidth", _maxThumbSize.ToString());
                //    DIVfotorama.Attributes.Add("data-maxwidth", _maxThumbSize.ToString());
                    DIVfotorama.Attributes.Add("data-caption", _listingstatus.ToString());

                    // IMAGE
                    Image ListingImage = (Image)e.Item.FindControl("imgListingImage");


                    ListingImage.Attributes.Add("data-caption", _listingstatus.ToString()); 

                    // ListingImage.ImageUrl = "~/DesktopModules/GIBS/FlexMLS/ImageHandler.ashx?MlsNumber=" + _ListingNumber.ToString() + "&MaxSize=" + _maxThumbSize.ToString();

                    //       ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";



                    string checkImage = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";

                    if (UrlExists(checkImage.ToString()) == true)
                    {
                        // ListingImage.ImageUrl = checkImage.ToString();
                        ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";

                    }
                    else if (UrlExists(_MLSImagesURL.ToString() + _ListingNumber.ToString() + "_1.jpg") == true)
                    {
                        //
                        ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + "_1.jpg";

                    }
                    else
                    {

                        ListingImage.ImageUrl = _MLSImagesURL.ToString() + "NoImage.jpg";

                        ImageNeeded(_ListingNumber.ToString());
                    }

                    ListingImage.ToolTip = "MLS Listing " + _ListingNumber.ToString();
                    ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString();


                    //  ListingImage.Width = 175;

                    // CHECK IF AUTHENTICATED
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        // lblMarketingRemarks
                        Label MarketingRemarks = (Label)e.Item.FindControl("lblMarketingRemarks");
                        MarketingRemarks.Text = DataBinder.Eval(e.Item.DataItem, "PublicRemarks").ToString();

                        ////PlaceHolderMarketingRemarks
                        //PlaceHolder MktRemarks = (PlaceHolder)e.Item.FindControl("PlaceHolderMarketingRemarks");
                        //Literal _MktRemarks = new Literal();
                        //_MktRemarks.Text = DataBinder.Eval(e.Item.DataItem, "PublicRemarks").ToString().Substring(0,100);
                       
                        //MktRemarks.Controls.Add(_MktRemarks);



                        ////tblRegisteredUsersContent
                      //  string _dom = (DateTime.Today - DateTime.Parse(DataBinder.Eval(e.Item.DataItem, "InsertDate").ToString())).TotalDays.ToString();

                        string _dom = DataBinder.Eval(e.Item.DataItem, "DOM").ToString();

                        PlaceHolder RegisteredUsersContent = (PlaceHolder)e.Item.FindControl("PlaceHolder_RegisterUserContent_DOM");
                        Literal RegUserContent = new Literal();
                        RegUserContent.Text = "DOM: " + _dom.ToString();

                        // + "</li><li>Original List Price: " + String.Format("{0:C0}", double.Parse(DataBinder.Eval(e.Item.DataItem, "OriginalListPrice").ToString())) + "</li></ul></div>";


                        RegisteredUsersContent.Controls.Add(RegUserContent);

                        ////if (item.InteriorFeatures.Length > 1)
                        ////{

                        //HtmlTableRow tRow = new HtmlTableRow();
                        //HtmlTableCell tb_l = new HtmlTableCell();
                        //HtmlTableCell tb_r = new HtmlTableCell();
                        //tb_l.Attributes.Add("class", "featurelabel");
                        //tb_r.Attributes.Add("class", "featuredata");
                        //tb_l.InnerHtml = "Days on Market";
                        //tb_r.InnerHtml = DataBinder.Eval(e.Item.DataItem, "DOM").ToString();
                        //tRow.Controls.Add(tb_l);
                        //tRow.Controls.Add(tb_r);

                        //RegisteredUsersContent.Rows.Add(tRow);

                        //AddToTableRegisteredUsersContent("Days on Market", DataBinder.Eval(e.Item.DataItem, "DOM").ToString(), RegisteredUsersContent);
                        ////}
                    }

                    //HyperLinkInquiry - CONTACT FORM
                    HyperLink InquiryHyperLink = (HyperLink)e.Item.FindControl("HyperLinkInquiry");
                    string InquiryLink = Globals.NavigateURL("View", "pg", "Contact", "MLS", _ListingNumber.ToString());
                    InquiryLink = InquiryLink.ToString().Replace("ctl/View/", "");
                    InquiryHyperLink.NavigateUrl = InquiryLink.ToString();

                    //HyperLinkShowing - SCHEDULE A SHOWING
                    HyperLink ShowingHyperLink = (HyperLink)e.Item.FindControl("HyperLinkShowing");
                    string ShowingLink = Globals.NavigateURL("View", "pg", "Contact", "MLS", _ListingNumber.ToString(), "Schedule", "Showing");
                    ShowingLink = ShowingLink.ToString().Replace("ctl/View/", "");
                    ShowingHyperLink.NavigateUrl = ShowingLink.ToString();

                    //HyperLinkInquiry - TELL A FRIEND FORM
                    HyperLink TellFriendHyperLink = (HyperLink)e.Item.FindControl("HyperLinkTellAFriend");
                   
                    string MailTovLink = Globals.NavigateURL("View", "pg", "v", "MLS", _ListingNumber.ToString());
                    MailTovLink = MailTovLink.ToString().Replace("ctl/View/", "");
                    MailTovLink = MailTovLink.ToString().Replace("Default.aspx", _pageName.ToString());
                  
                    string TellAFriendLink = "mailto:?subject=Information about a property: MLS " + _ListingNumber.ToString() + "&body=" + BubbleAddress.ToString()
                      + HttpUtility.UrlEncode(Environment.NewLine + Environment.NewLine) + MailTovLink.ToString() 
                      + HttpUtility.UrlEncode(Environment.NewLine + Environment.NewLine) + PortalSettings.PortalName.ToString();
                 
                    TellFriendHyperLink.NavigateUrl = TellAFriendLink.ToString();


                    // ADD TO MAP
                    double _lat = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Latitude").ToString());
                    double _log = Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "Longitude").ToString());
                    string _bubbleText = "<div style='width:270px;height:120px;'><img src='" + "/DesktopModules/GIBS/FlexMLS/ImageHandler.ashx?MlsNumber=" + _ListingNumber.ToString() + "&MaxSize=140' id='" + _ListingNumber.ToString() + "' align='right' alt='" + BubbleAddress.ToString() + "' style='border: 1px solid #000000;'>"
                        + Address.Text.ToString() + "<br/>" + ListingPrice.Text.ToString()
                        + "<br/><a href='" + vLink.ToString() + "'>MLS " + _ListingNumber.ToString() + "<br/>View Listing</a></div>";
                    //  _bubbleText = "";

                    //if (_ShowListViewMap == true)
                    //{
                    //    if (_lat > 0)
                    //    {
                    //        BuildGoogleMap(_lat, _log, _bubbleText.ToString());
                    //    }
                    //}
                    //else 
                    //{
                    //    GMap1.Visible = false;
                        
                    //}




                }

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


        //public void AddToTableRegisteredUsersContent(string labelName, string labelValue, Table tableName)
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

        //        tableName.Rows.Add(tRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}

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





        // SAVE A LISTING
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
                    //       linkButtonFavorites.Focus();

                    //       Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowPopup();", true);

                }




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        // SAVE A SEARCH
        protected void linkButtonFavorites_Click(object sender, EventArgs e)
        {
            try
            {


                if (Request.IsAuthenticated)
                {

                    FlexMLSController controller = new FlexMLSController();
                    FlexMLSInfo item = new FlexMLSInfo();

                    item.Favorite = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                    item.FavoriteType = "Search";
                    item.ModuleId = this.ModuleId;
                    item.EmailSearch = true;
                    item.UserID = this.UserId;
                    controller.FlexMLS_Favorites_Add(item);
                    linkButtonFavorites.ForeColor = System.Drawing.Color.Red;
                    linkButtonFavorites.Text = "SEARCH SAVED!";

                }

                else
                {
                    LinkButton myButton = sender as LinkButton;
                    myButton.Text = "Register to Use This Feature";
                    myButton.ForeColor = System.Drawing.Color.Red;
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowPopup();", true);
                }

                ////determine if we are adding or updating
                //if (Null.IsNull(item.ItemId))
                //    controller.AddFlexMLS(item);
                //else
                //    controller.UpdateFlexMLS(item);

                //Response.Redirect(Globals.NavigateURL(), true);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }




    }
}
