<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="GIBS.Modules.FlexMLS.List" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>



<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FlexMLS/css/Style.css?1=2" />

<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" crossorigin="anonymous">

<script type="text/javascript">


    function ShowPopup() {
        var x = document.getElementById('<%= lblPopupContent.ClientID %>').innerText;
        $("html, body").animate({ scrollTop: 0 }, "slow");

        //   $('#<%= linkButtonFavorites.ClientID %>').focus();
        $.dnnAlert({
            okText: 'CLOSE',
            text: '<div id=\"mymessage\" style=\"text-align: center\"><h3>Premium Feature</h3><p>Register on the site to access this feature. ' + x.toString() + '  </p></div>',
            position: ["center", "center"],
            width: 400,
            height: 400,
            title: 'Register Today!'
        });


        //   $('body').scrollTop();
    }


</script>




<!-- fotorama.css & fotorama.js. -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.css" rel="stylesheet" />
<!-- 3 KB -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js" type="text/javascript"></script>
<!-- 16 KB -->

<!-- 2. Add images to <div class="fotorama"></div>. -->


<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>


<div style="display: inline;">
    <asp:Label ID="lblPopupContent" runat="server" Text=""></asp:Label></div>


<div style="position: relative; float: right; padding-right: 30px;">
    <asp:LinkButton ID="linkButtonFavorites" runat="server" CssClass="NormalRed"
        OnClick="linkButtonFavorites_Click">Save This Search</asp:LinkButton>
    | 
     <asp:HyperLink ID="HyperLinkNewSearch" runat="server">New Search</asp:HyperLink>


</div>



<div class="searchcriteria">
    </div>

<div class="searchcriteria">
    <asp:Label ID="lblSearchSummary" runat="server" Text="0 Records Found" /></div>

    <div class="well well-sm">
        <strong>Search Criteria</strong> 
        <asp:Label ID="lblSearchCriteria" runat="server" Text="" Visible="True"></asp:Label>

    </div>




<div  class="row">
    <div class="col-12">&nbsp;</div>
</div>

    
        <div class="container-fluid">
<div id="listings" class="row row-eq-height">

        <asp:DataList ID="lstSearchResults" DataKeyField="ListingNumber" runat="server" Width="100%" 
            OnItemDataBound="lstSearchResults_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <ItemStyle VerticalAlign="Top" />
            <ItemTemplate>
                <!----
          
          style="float:left; vertical-align:top; margin:6px; background-color:#fffee9;width:290px;"
          --->
                <div class="item col-md-4 col-sm-6 col-xs-6" style="background-color: azure;">
                    <div class="thumbnail clearfix">
                        <div class="piccontainer">
                            <div class="fotorama" id="fotorama" data-fit="cover" runat="server" data-ratio="800/600">
                                <asp:HyperLink ID="HyperLinkImage" runat="server">
                                    <asp:Image ID="imgListingImage" runat="server" AlternateText="Cape Cod MLS" />
                                </asp:HyperLink>

                            </div>
                            <div class="center-bottom">
                                <asp:Label ID="lblListingStatus" runat="server" /></div>
                            <div class="top-right">
                                <asp:HyperLink ID="HyperLinkVirtualTourLink" runat="server" CssClass="ActionLinks" Target="_blank">
                                    <asp:Image ID="ImageVirtualTour1" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/VirtualTour.png" AlternateText="Virtual Tour" />
                                </asp:HyperLink>
                            </div>
                        </div>
                        <div class="alert alert-warning ListListingPrice">
                            <asp:Label ID="lblListingPrice" runat="server" />
                            <asp:Image ID="imgPriceChange" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/arrow_down.png" />
                        </div>

                        <div>
                            <asp:HyperLink ID="hyperlinkListingAddress" Text="Listing Address" NavigateUrl="#" CssClass="ListingAddress" runat="server" />
                        </div>
                        <div>
                            <asp:HyperLink ID="hyperlinkListingAddressCity" Text="Listing Town" NavigateUrl="#" CssClass="ListingAddress" runat="server" />
                        </div>

                        <div><i class="fas fa-bed"></i> 
                            <asp:Label ID="lblBedsBaths" runat="server" CssClass="ListingDetails" />
                        </div>
                        <div>
                            <asp:Label ID="lblPropType" runat="server" CssClass="ListingDetails" />
                        </div>
                        <div>
                            <asp:Label ID="lblLivingSpace" runat="server" CssClass="ListingDetails" />
                            <asp:Label ID="lblYearBuilt" runat="server" CssClass="ListingDetails" />
                            
                            
                        </div>
                        <div>
                            <asp:PlaceHolder ID="PlaceHolder_RegisterUserContent_DOM" runat="server"></asp:PlaceHolder>
                        </div>
                        <div>
                            <asp:Label ID="lblLotSquareFootage" runat="server" CssClass="ListingDetails" />
                        </div>

                        <div></div>
                        <div></div>

                        <div>
                            <asp:Label ID="lblMarketingRemarks" runat="server" Visible="false"></asp:Label>
                        </div>



                        <div>
                            <asp:Label ID="lblMLNumber" runat="server" CssClass="listingstatus" /><asp:Image ID="imgBRLogo" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/BrokerReciprocity.gif" AlternateText="Broker Reciprocity (BR) of the Cape Cod & Islands MLS" Width="107px" Height="25px" />
                        </div>





                        <div>

                            <p class="ListingLinks">

                                <asp:HyperLink ID="hyperlinkListingDetail" Text="Details" runat="server" CssClass="btn btn-xs btn-default" Visible="false" />
                                <asp:HyperLink ID="HyperLinkShowing" runat="server" CssClass="btn btn-xs btn-default" Text="Schedule Showing" Visible="false" />
                                <asp:HyperLink ID="HyperLinkInquiry" runat="server" CssClass="btn btn-xs btn-default" Text="Inquire" Visible="false" />
                                <asp:LinkButton ID="linkButtonFavoritesAddListing" runat="server" CommandArgument='<%# Eval("ListingNumber") %>'
                                    OnClick="linkButtonFavoritesAddListing_Click" CssClass="btn btn-xs btn-default" Text="Add to Favorites" Visible="false" />
                                <asp:HyperLink ID="HyperLinkTellAFriend" runat="server" CssClass="btn btn-xs btn-default" Text="E-Mail It" Visible="false" />

                            </p>


                        </div>




                    </div>

                </div>




            </ItemTemplate>
        </asp:DataList>

    </div>
</div>

<dnn:PagingControl id="PagingControl1" runat="server" Visible="False" BackColor="#FFFFFF" BorderColor="#000000"></dnn:PagingControl>


<p class="disclaimer">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/DesktopModules/GIBS/FlexMLS/Images/BrokerReciprocity.gif" AlternateText="Broker Reciprocity (BR) of the Cape Cod & Islands MLS" ImageAlign="Left" Width="107px" Height="25px" />

    The data relating to real estate for sale on this site comes from the Broker Reciprocity (BR) of the Cape Cod &amp; Islands 
Multiple Listing Service, Inc. Summary or thumbnail real estate listings held by brokerage firms other than <b><%#   this.PortalSettings.PortalName.ToString() %></b>
    are marked with the BR Logo and detailed information about them includes the name of the listing broker.  Neither the 
listing broker nor <b><%#   this.PortalSettings.PortalName.ToString() %></b> shall be responsible for any typographical errors, misinformation, or misprints and 
shall be held totally harmless.
</p>
