<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewListing.ascx.cs" Inherits="GIBS.Modules.FlexMLS.ViewListing" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>


<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FlexMLS/css/Style.css?1=8" />


	<script src="//code.listtrac.com/monitor.ashx?acct=gibs_100489" type="text/javascript"></script>
	<script type="text/javascript">
	    if (typeof (_LT) != 'undefined' && _LT != null) {
	        _LT._trackEvent(_eventType.view, '<%=  mlNumber.ToString() %>', '<%=  _Z_zip.ToString() %>');
	    }
	    //'0913456677' is the listing ID 
	    //'92127' is the zipcode
	    //'agentID_123' is the the ID assigned by the MLS associated to the Agent/Broker receiving the lead. 
	    //         This parameter is only required for the 'Lead Generated' event but may be included for other events. 
	    //         (NOTE: if there is no value, please pass NULL)
	    //'loggedIn_agentID' is only required for MLS systems; this ID represents the member currently logged into the MLS system; 
	    //         if the event occurs in a client-facing component of the MLS system, 
	    //         e.g., report sent to a customer, then the value for this parameter would be ‘Client'
	    //'Single Photo Report' is the name of the report in the MLS system; 
	    //         this parameter is only for MLS systems and provides the name of the report being viewed
	    //
	    //All parameters are string values and should include single quotes, e.g., 'value'

	    //examples:
	    /*
	    _LT._trackEvent(_eventType.view, '0913456677', '92127');
	    _LT._trackEvent(_eventType.view, '0913456677', '92127','agentID_123');
	    _LT._trackEvent(_eventType.view, '0913456677', '92127',NULL,'agentID_456','Single Photo Report');
	    */
    	</script>


<script type="text/javascript" >




    // BIND DNN Tabs
   // jQuery(function ($) { $('#tabs-client').dnnTabs(); });




    function ShowPopup() {
        $.dnnAlert({
            okText: 'CLOSE',
            text: '<div style=\"text-align: center\"><h3>Premium Feature</h3><p>Register on the site to access this feature.</p></div>',
            position: ["center", "center"],
            width: 400,
            height: 400,
            title: 'Register Today!'
        });
       }
    //     //   ShowPopup;
    //          setTimeout(ShowPopup, 1);
    //    });
    //     }
</script>		



<!-- fotorama.css & fotorama.js. -->
<link  href="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.css" rel="stylesheet" /> <!-- 3 KB -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js" type="text/javascript"></script> <!-- 16 KB -->

<!-- 2. Add images to <div class="fotorama"></div>. -->





<div class="returntosearch"><asp:HyperLink ID="HyperLinkReturnURL" runat="server" Visible="False">Return to Previous Page</asp:HyperLink> 
 | <asp:HyperLink ID="HyperLinkNewSearch" runat="server">New Search</asp:HyperLink></div>

<div class="row">
    <div class="col-sm-8"><asp:Label ID="lblListingAddress" runat="server" CssClass="ListingAddress"  /></div>
        <div class="col-sm-4 text-right"><asp:Label ID="lblListingPrice" runat="server" CssClass="ListingPrice"  />
        <asp:Image ID="imgPriceChange" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/arrow_down.png" /></div> 

</div>




<div><p class="summary"><asp:Label ID="lblSummary" runat="server" /></p></div>

<div class="ListingActions">
        <asp:HyperLink ID="HyperLinkVirtualTourLink" runat="server" Target="_blank" ><asp:Image ID="ImageVirtualTour" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/VirtualTour.png" ToolTip="Virtual Tour" /></asp:HyperLink>
          <asp:HyperLink ID="HyperLinkShowing" runat="server" CssClass="btn btn-sm btn-default" Text="Schedule Showing" /> 
        &nbsp; <asp:HyperLink ID="HyperLinkInquiry" runat="server" CssClass="btn btn-sm btn-default" Text="Inquiry" />
        &nbsp; <asp:LinkButton ID="linkButtonFavoritesAddListing" runat="server"  
         onclick="linkButtonFavoritesAddListing_Click" CssClass="btn btn-sm btn-default" Text="Add to Favorites" /> 
         &nbsp; <asp:HyperLink ID="HyperLinkTellAFriend" runat="server" CssClass="btn btn-sm btn-default" Text="E-Mail A Friend" /> 
         
         </div>

<div style="clear:both"></div>



<div class="fotorama" data-nav="thumbs" data-width="100%"
     data-ratio="800/600" data-allowfullscreen="true">

    <asp:Literal ID="LiteralPhotorama" runat="server"></asp:Literal>
</div>



<div class="MarketingRemarks"><asp:Label ID="lblMarketingRemarks" runat="server" /></div>


    <h3>General</h3>
        <table id="tblGeneral" runat="server" class="table table-striped">

        </table>


    <asp:Panel ID="PanelCondoSpecific" runat="server" Visible="False">
    <h3>Details</h3>
        <table id="tblCondoSpecific" runat="server" class="table table-striped">
        </table>
     </asp:Panel>


    <asp:Panel ID="PanelResiInteriorFeatures" runat="server" Visible="False">
    <h3>Interior Features</h3>
        <table id="tblInterior" runat="server" class="table table-striped">
        </table>
     </asp:Panel>


<h3>Exterior</h3>
        <table id="tblExterior" runat="server" class="table table-striped">
        </table>

<h3>Neighborhood</h3>
        <table id="tblNeighborhood" runat="server" class="table table-striped">
        </table>

    


    <h3>Assessment and Taxes</h3>
        <table id="tblTaxAssessment" runat="server" class="table table-striped">
        </table>

<br clear="all" />











<div class="gmap_canvas"><iframe frameborder="0" height="500" id="gmap_canvas" marginheight="0" marginwidth="0" scrolling="no" 
    src='https://www.google.com/maps/embed/v1/place?key=<% = _GoogleMapAPIKey %>&q=<% = ListingAddressForGMap %>&center=<% = _GoogleLatLon %>&zoom=18&maptype=satellite' width="100%"></iframe></div>
<style type="text/css">.gmap_canvas {overflow:hidden;background:none!important;height:500px;width:100%;}
</style>				


<style type="text/css">
	#erateWidget{
		font-family:Arial, Helvetica, sans-serif;
		font-size:12px;
		width:300px;
	}
	#erateWidget table.ratesTable{
		font-size:12px;
		padding:2px;
		margin:0px auto;
		border-color:#b0c1d9;
		width:100%;
	}
	
	#erateWidget table.ratesTable td{
		border-color:#b0c1d9;
		font-size:12px;
	}
	
	#erateWidget table.ratesTable th.top{
		font-weight:bold;
		color: #255488;
		border-right: 1px solid #b0c1d9;
		border-bottom: 1px solid #b0c1d9;
		border-top: 1px solid #b0c1d9;
		text-align: left;
		padding: 2px;
		background: #e2ecf9;
		font-size:12px;
	}
	
	#erateWidget table.ratesTable .left{
		border-left: 1px solid #b0c1d9;
		width:50%;
		float:none;
	}
	
	#erateWidget table.ratesTable th.tick{
		color: #255488;
		border-right: 1px solid #b0c1d9;
		border-bottom: 1px solid #b0c1d9;
		border-left: 1px solid #b0c1d9;
		text-align: left;
		border-top: none;
		background: #ffffff;
		padding: 2px;
		color: #255488;
		font-weight: bold;
		font-size:12px;
	}
	
	#erateWidget table.ratesTable th.tick a{
		color: #255488;
		font-weight: bold;
		font-size:12px;
		text-decoration:none;
	}
	
	#erateWidget table.ratesTable td{
		border-right: 1px solid #b0c1d9;
		border-bottom: 1px solid #b0c1d9;
		background: #ffffff;
		padding: 2px;
		color: #333333;
		text-align:center;
	}
	
	#erateWidget table.ratesTable th.tdcenter{
		text-align:center;
		width:25%;
	}
	
	#erateWidget .caption{
		padding: 4px 0px;
		font-size: 12px;
		text-align: center;
		color: #333333;	
	}
	#erateWidget .caption a, #erateWidget .caption a:hover, #erateWidget .caption a:visited{
		text-decoration:none;
		color: #333333;
	}
</style>
<div id="erateWidget"><div class="caption"><a href="https://www.erate.com/mortgage_rates/Massachusetts/conforming/30_year_fixed.html" target="_blank">Massachusetts  Mortgage Rates</a></div><script type="text/javascript" src="https://www.erate.com/widgets/getRates?state=MA"></script></div>



<hr />



<p class="disclaimer"><asp:Image ID="Image1" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/BrokerReciprocity.gif" AlternateText="Broker Reciprocity (BR) of the Cape Cod & Islands MLS" ImageAlign="Left" Width="107px" Height="25px" />

The data relating to real estate for sale on this site comes from the Broker Reciprocity (BR) of the Cape Cod &amp; Islands 
Multiple Listing Service, Inc. Summary or thumbnail real estate listings held by brokerage firms other than <b><%=  this.PortalSettings.PortalName.ToString() %></b> 
are marked with the BR Logo and detailed information about them includes the name of the listing broker.  Neither the 
listing broker nor <b><%=  this.PortalSettings.PortalName.ToString() %></b> shall be responsible for any typographical errors, misinformation, or misprints and 
shall be held totally harmless.
</p>   

<p class="ListingOfficeName">Listing Courtesy of <asp:Label ID="lblListingOfficeName" runat="server" Text="Label"></asp:Label></p>







<script type="text/javascript">
    jQuery(document).ready(function ($) {
        // We only want these styles applied when javascript is enabled
        $('div.navigation').css({ 'width': '200px', 'float': 'left' });
        $('div.czontent').css('display', 'block');

        // Initially set opacity on thumbs and add
        // additional styling for hover effect on thumbs
        var onMouseOutOpacity = 0.67;
        $('#thumbs ul.thumbs li').opacityrollover({
            mouseOutOpacity: onMouseOutOpacity,
            mouseOverOpacity: 1.0,
            fadeSpeed: 'fast',
            exemptionSelector: '.selected'
        });

        // Initialize Advanced Galleriffic Gallery
        var gallery = $('#thumbs').galleriffic({
            delay: 2500,
            numThumbs: 8,
            preloadAhead: 8,
            enableTopPager: true,
            enableBottomPager: false,
            maxPagesToShow: 7,
            imageContainerSel: '#slideshow',
            controlsContainerSel: '#controls',
            captionContainerSel: '#caption',
            loadingContainerSel: '#loading',
            renderSSControls: true,
            renderNavControls: true,
            playLinkText: 'Play Slideshow',
            pauseLinkText: 'Pause Slideshow',
            prevLinkText: '&lsaquo; Previous Photo',
            nextLinkText: 'Next Photo &rsaquo;',
            nextPageLinkText: 'Next &rsaquo;',
            prevPageLinkText: '&lsaquo; Prev',
            enableHistory: false,
            autoStart: false,
            syncTransitions: true,
            defaultTransitionDuration: 900,
            onSlideChange: function (prevIndex, nextIndex) {
                // 'this' refers to the gallery, which is an extension of $('#thumbs')
                this.find('ul.thumbs').children()
							.eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
							.eq(nextIndex).fadeTo('fast', 1.0);
            },
            onPageTransitionOut: function (callback) {
                this.fadeTo('fast', 0.0, callback);
            },
            onPageTransitionIn: function () {
                this.fadeTo('fast', 1.0);
            }
        });
    });
		</script>



