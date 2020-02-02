using System;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace GIBS.Modules.FlexMLS.Components
{
    public class FlexMLSInfo
    {
        //private vars exposed thro the
        //properties
        private int moduleId;
        private int itemId;
        private string content;
        private int createdByUser;
        private DateTime createdDate = DateTime.Today;
        private string createdByUserName = null;

        private string propertyType = "";
        
        // USERS
        private int userID;
        private int clientID;
        private int createdByUserID;
       
        private int clientPortalID;

        // TAXRATES
        
        private double taxRate;

        // AGENT MODULE
        private string _ListingStatus;



        // FAVORTIES

        private string favorite;
        private string favoriteType;
        private bool emailSearch;
        private string clientEmail;
        private string clientName;

        private string description;
        private int itemCount;
        private string address;

        private double _Acres = 0;
        private string _agentboardCode = "";
        private string _agentboardID = "";
        private string _AgentEmail = "";
        private string _AgentID = "";
        private string _AgentRemarks = "";
        private string _Amenities = "";
        private double _AnnualAssocFee;
        private string _Appliances = "";
        private string _AsbestosYN = "";
        private string _AssocFeeYear = "";
        private string _AssociationYN = "";
        private double _BasementBaths = 0;
        private string _BasementDescription = "";
        private double _BasementSqFt = 0;
        private string _BasementYN = "";
        private string _BathDescription = "";
        private double _BathsLevel1 = 0;
        private double _BathsLevel2 = 0;
        private double _BathsLevel3 = 0;
        private string _BeachDescription = "";
        private string _BeachLakePond = "";
        private string _BeachOwnership = "";
        private string _Bedroom2Features = "";
        private string _Bedroom2Length = "";
        private string _Bedroom2Level = "";
        private string _Bedroom2Width = "";
        private string _Bedroom3Features = "";
        private string _Bedroom3Length = "";
        private string _Bedroom3Level = "";
        private string _Bedroom3Width = "";
        private string _Bedroom4Features = "";
        private string _Bedroom4Length = "";
        private string _Bedroom4Level = "";
        private string _Bedroom4Width = "";
        private int _Bedrooms = 0;
        private double _Betterment = 0;
        private string _BuildingConstruction = "";
        private string _BuildingName = "";
        private string _BusinessIncludes = "";
        private string _BusinessIncludesComments = "";
        private string _BusinessName = "";
        private string _BusinessOperatingYN = "";
        private string _BusinessWithRealEstateYN = "";
        private string _CommUnit1Description = "";
        private string _Complex = "";
        private string _ComplexCompleted = "";
        private string _CondoAttachedOrDetached = "";
        private string _CondoManagement = "";
        private string _CondoOwnership = "";
        private string _CondoType = "";
        private string _ConformingUseYN = "";
        private string _ConvenientTo = "";
        private string _Cooling = "";
        private string _County = "";
        private string _CumulativeDOM = "";
        private string _DenFeatures = "";
        private string _DenLength = "";
        private string _DenLevel = "";
        private string _DenWidth = "";
        private string _DiningRoomFeatures = "";
        private string _DiningRoomLength = "";
        private string _DiningRoomLevel = "";
        private string _DiningRoomWidth = "";
        private string _Directional = "";
        private string _Directions = "";
        private string _DockDescription = "";
        private string _DockYN = "";
        private string _DOM = "";
        private string _elementary = "";
        private string _elementary1 = "";
        private string _ElevationCert = "";
        private string _EntertainmentFeatures = "";
        private string _ExerciseRoomFeatures = "";
        private string _ExerciseRoomLength = "";
        private string _ExerciseRoomLevel = "";
        private string _ExerciseRoomWidth = "";
        private string _ExpirationDate = "";
        private string _ExteriorFeatures = "";
        private string _FamilyRoomFeatures = "";
        private string _FamilyRoomLength = "";
        private string _FamilyRoomLevel = "";
        private string _FamilyRoomWidth = "";
        private string _FinancingAvailable = "";
        private double _FinishedBasementSqFt = 0;
        private string _Fireplace = "";
        private int _FireplaceNumber = 0;
        private string _FirstMonthRequired = "";
        private string _FloodInsuranceRequired = "";
        private string _Floors = "";
        private string _ForSaleYN = "";
        private string _Foundation = "";
        private double _FoundationDepth = 0;
        private double _FoundationWidth = 0;
        private double _FoundationWingDepth = 0;
        private double _FoundationWingWidth = 0;
        private string _FoyerFeatures = "";
        private string _FoyerLength = "";
        private string _FoyerLevel = "";
        private string _FoyerWidth = "";
        private string _FuelType = "";
        private int _FullBaths = 0;
        private string _GalleryFeatures = "";
        private string _GameRoomFeatures = "";
        private string _GarageDescription = "";
        private string _GarageYN = "";
        private string _GreatRoomFeatures = "";
        private int _HalfBaths = 0;
        private string _Heating = "";
        private string _HomeOfficeFeatures = "";
        private string _HotWater = "";
        private string _HotWaterSource = "";
        private double _ImprovementAssessment;
        private string _InLawAptFeatures = "";
        private DateTime _InsertDate = DateTime.Today;
        private string _InteriorFeatures = "";
        private string _IrregularYN = "";
        private string _KitchenDiningCombo = "";
        private string _KitchenFeatures = "";
        private double _LandAssessment = 0;
        private string _LandCourtCert = "";
        private string _LandlordPays = "";
        private DateTime _LastModifiedDate = DateTime.Today;
        private double _Latitude = 0;
        private string _LaundryRoomFeatures = "";
        private string _LaundryRoomLength = "";
        private string _LaundryRoomLevel = "";
        private string _LaundryRoomWidth = "";
        private string _LeadPaint = "";
        private string _LibraryFeatures = "";
        private string _ListingAgentID = "";
        private string _ListingAgentName = "";
        private string _ListingNumber;
        private string _ListingOfficeName = "";
        private double _ListingPrice;
        private string _ListingRid = "";
        private string _ListingTable = "";
        private string _ListOffUrl = "";
        private double _ListPricePerSqFt;
        private string _LivingDiningComboLevel = "";
        private string _LivingDiningComboYN = "";
        private string _LivingRoomFeatures = "";
        private string _LivingRoomLength = "";
        private string _LivingRoomWidth = "";
        private double _LivingSpace = 0;
        private string _LocationDescription = "";
        private string _LoftFeatures = "";
        private double _Longitude = 0;
        private double _LotDepth = 0;
        private string _LotDescription = "";
        private string _LotNumber = "";
        private string _LotSizeSource = "";
        private double _LotSizeSqFt = 0;
        private double _LotWidth = 0;
        private string _MallParkName = "";
        private string _MassUseCode = "";
        private string _MasterBathFetaures = "";
        private string _MasterBedroomFeatures = "";
        private string _MaxNumberOfUnits = "";
        private string _MediaRoomFeatures = "";
        private string _MembershipRequired = "";
        private string _MilesToBeach = "";
        private int _MLNumber = 0;
        private string _MLSApproved = "";
        private string _MLSID = "";
        private double _MonthlyFeeAmount;
        private string _MudRoomFeatures = "";
        private string _NeighborhoodAmenities = "";
        private string _NumberOfCars = "";
        private int _NumRooms = 0;
        private string _OccupancyComments = "";
        private string _ListingOfficeID = "";
        private string _OfficeID = "";
        private DateTime _offmarkett = DateTime.Today;
        private DateTime _onmarketti = DateTime.Today;
        private double _OriginalListPrice = 0;
        private double _OtherAssessment = 0;
        private string _OtherRoom1Features = "";
        private string _OtherRoom2Features = "";
        private string _OtherRoom3Features = "";
        private string _OwnerName = "";
        private string _ParkingDescription = "";
        private string _ParkingSpaces = "";
        private string _PetsAllowed = "";
        private DateTime _PicTimestamp = DateTime.Today;
        private int _PictureCount = 0;
        private string _Plan = "";
        private string _PlayRoomFeatures = "";
        private string _Pool = "";
        private string _PoolDescription = "";
        private string _PresentUse = "";
        private double _previousli = 0;
        private DateTime _pricechange = DateTime.Today;
        private string _PropertySubType1 = "";
        private string _PublicRemarks = "";
        private string _PublishToInternet = "";
        private string _Renovated = "";
        private string _RentalType = "";
        private string _Restrictions = "";
        private string _RoadFrontage = "";
        private string _Roofing = "";
        private string _SchoolDistrict = "";
        private string _SecondKitchen = "";
        private string _Security = "";
        private string _SeparateLivingQtrsDescription = "";
        private string _SeparateLivingQtrsYN = "";
        private string _SepticTankGrade = "";
        private string _Sewer = "";
        private string _ShowingInstructions = "";
        private string _SidingDescription = "";
        private string _SittingRoomFeatures = "";
        private string _SolarFeatures = "";
        private DateTime _SoldDate = DateTime.Today;
        private double _SoldPrice = 0;
        private double _SoldPricePerSqFt = 0;
        private string _SpecialListingCond = "";
        private string _SqFtSource = "";
        private string _State = "";
        private DateTime _StatusChangeDate = DateTime.Today;
        private string _StatusCode = "";
        private string _Stories = "";
        private string _StreetDescription = "";
        private string _StreetMod = "";
        private string _StreetName = "";
        private string _StreetNumber = "";
        private string _StreetType = "";
        private string _StudioFeatures = "";
        private string _Style = "";
        private string _Subdivision = "";
        private string _SunRoomFeatures = "";
        private double _Taxes = 0;
        private string _TaxID = "";
        private int _TaxYear = 0;
        private string _TitleReferenceBook = "";
        private string _TitleReferencePage = "";
        private double _TotalAssessments = 0;
        private double _TotalBaths = 0;
        private string _Town = "";
        private DateTime _UnderContractDate = DateTime.Today;
        private string _UndergroundFuelTank = "";
        private int _Unit1Bedrooms = 0;
        private string _Unit1Features = "";
        private int _Unit1FloorLevel = 0;
        private string _Unit1FloorLvlDes = "";
        private int _Unit1FullBaths = 0;
        private int _Unit1HalfBaths = 0;
        private string _Unit1Leased = "";
        private string _Unit1LeaseExpires = "";
        private double _Unit1MonthlyRent = 0;
        private int _Unit1Rooms = 0;
        private int _Unit2Bedrooms = 0;
        private string _Unit2Features = "";
        private int _Unit2FloorLevel = 0;
        private string _Unit2FloorLvlDes = "";
        private int _Unit2FullBaths = 0;
        private int _Unit2HalfBaths = 0;
        private string _Unit2Leased = "";
        private string _Unit2LeaseExpires = "";
        private double _Unit2MonthlyRent;
        private int _Unit2Rooms = 0;
        private int _Unit3Bedrooms = 0;
        private string _Unit3Features = "";
        private int _Unit3FloorLevel = 0;
        private string _Unit3FloorLvlDes = "";
        private int _Unit3FullBaths = 0;
        private int _Unit3HalfBaths = 0;
        private string _Unit3Leased = "";
        private string _Unit3LeaseExpires = "";
        private double _Unit3MonthlyRent = 0;
        private int _Unit3Rooms = 0;
        private int _Unit4Bedrooms = 0;
        private string _Unit4Features = "";
        private int _Unit4FloorLevel = 0;
        private string _Unit4FloorLvlDes = "";
        private int _Unit4FullBaths = 0;
        private int _Unit4HalfBaths = 0;
        private string _Unit4Leased = "";
        private string _Unit4LeaseExpires = "";
        private double _Unit4MonthlyRent = 0;
        private int _Unit4Rooms = 0;
        private string _UnitNumber = "";
        private string _UnitPlacement = "";
        private string _UtilityRoomFeatures = "";
        private string _Village = "";
        private string _VirtualTourLink = "";
        private string _WarrantyAvailable = "";
        private string _WaterAccess = "";
        private string _WaterFrontDesc = "";
        private string _WaterfrontYN = "";
        private string _WaterSource = "";
        private string _WaterViewDesc = "";
        private string _WaterViewYN = "";
        private DateTime _WithdrawlDate = DateTime.Today;
        private string _WorkShopFeatures = "";
        private int _YearBuilt = 0;
        private string _YearBuiltDesc = "";
        private string _YearRoundYN = "";
        private string _YearsEstablished = "";
        private string _ZipCode = "";
        private string _Zoning = "";


        /// <summary>
        /// empty cstor
        /// </summary>
        public FlexMLSInfo()
        {
        }


        protected bool CheckDate(String date)
        {

            try
            {

                DateTime dt = DateTime.Parse(date);

                return true;
            }
            catch
            {

                return false;

            }

        }



        #region properties

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public string CreatedByUserName
        {
            get
            {
                if (createdByUserName == null)
                {
                    int portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
                    //UserInfo user = UserController.GetUser(portalId, createdByUser, false);
                    UserInfo user = UserController.GetUserById(portalId, createdByUser);
                    createdByUserName = user.DisplayName;
                }

                return createdByUserName;
            }
        }

        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        // FOR LOOKUPS
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // CONDO SPECIFIC

        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        public string Address
        {

            get
            {
                if (address == null)
                {

                    address = _StreetNumber + " " + _StreetName + " " + _StreetType;
                }

                return address;
            }

        }

        // USERS
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public int ClientID
        {
            get { return clientID; }
            set { clientID = value; }
        }

        public int CreatedByUserID
        {
            get { return createdByUserID; }
            set { createdByUserID = value; }
        }

        //ClientPortalID
        public int ClientPortalID
        {
            get { return clientPortalID; }
            set { clientPortalID = value; }
        }


        // TAXRATES

        public double TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }


        // Agent Module
        //_ListingStatus
        public string ListingStatus
        {
            get { return _ListingStatus; }
            set { _ListingStatus = value; }
        }

        // FAVORITES
        public string Favorite
        {
            get { return favorite; }
            set { favorite = value; }
        }

        public string FavoriteType
        {
            get { return favoriteType; }
            set { favoriteType = value; }
        }

        public bool EmailSearch
        {
            get { return emailSearch; }
            set { emailSearch = value; }
        }

        public string ClientEmail
        {
            get { return clientEmail; }
            set { clientEmail = value; }
        }

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }







        public double Acres
        {
            get { return _Acres; }
            set { _Acres = value; }
        }

        public string agentboardCode
        {
            get { return _agentboardCode; }
            set { _agentboardCode = value; }
        }

        public string agentboardID
        {
            get { return _agentboardID; }
            set { _agentboardID = value; }
        }

        public string AgentEmail
        {
            get { return _AgentEmail; }
            set { _AgentEmail = value; }
        }

        public string AgentID
        {
            get { return _AgentID; }
            set { _AgentID = value; }
        }

        public string AgentRemarks
        {
            get { return _AgentRemarks; }
            set { _AgentRemarks = value; }
        }

        public string Amenities
        {
            get { return _Amenities; }
            set { _Amenities = value; }
        }

        public double AnnualAssocFee
        {
            get { return _AnnualAssocFee; }
            set { _AnnualAssocFee = value; }
        }

        public string Appliances
        {
            get { return _Appliances; }
            set { _Appliances = value; }
        }

        public string AsbestosYN
        {
            get { return _AsbestosYN; }
            set { _AsbestosYN = value; }
        }

        public string AssocFeeYear
        {
            get { return _AssocFeeYear; }
            set { _AssocFeeYear = value; }
        }

        public string AssociationYN
        {
            get { return _AssociationYN; }
            set { _AssociationYN = value; }
        }

        public double BasementBaths
        {
            get { return _BasementBaths; }
            set { _BasementBaths = value; }
        }

        public string BasementDescription
        {
            get { return _BasementDescription; }
            set { _BasementDescription = value; }
        }

        public double BasementSqFt
        {
            get { return _BasementSqFt; }
            set { _BasementSqFt = value; }
        }

        public string BasementYN
        {
            get { return _BasementYN; }
            set { _BasementYN = value; }
        }

        public string BathDescription
        {
            get { return _BathDescription; }
            set { _BathDescription = value; }
        }

        public double BathsLevel1
        {
            get { return _BathsLevel1; }
            set { _BathsLevel1 = value; }
        }

        public double BathsLevel2
        {
            get { return _BathsLevel2; }
            set { _BathsLevel2 = value; }
        }

        public double BathsLevel3
        {
            get { return _BathsLevel3; }
            set { _BathsLevel3 = value; }
        }

        public string BeachDescription
        {
            get { return _BeachDescription; }
            set { _BeachDescription = value; }
        }

        public string BeachLakePond
        {
            get { return _BeachLakePond; }
            set { _BeachLakePond = value; }
        }

        public string BeachOwnership
        {
            get { return _BeachOwnership; }
            set { _BeachOwnership = value; }
        }

        public string Bedroom2Features
        {
            get { return _Bedroom2Features; }
            set { _Bedroom2Features = value; }
        }

        public string Bedroom2Length
        {
            get { return _Bedroom2Length; }
            set { _Bedroom2Length = value; }
        }

        public string Bedroom2Level
        {
            get { return _Bedroom2Level; }
            set { _Bedroom2Level = value; }
        }

        public string Bedroom2Width
        {
            get { return _Bedroom2Width; }
            set { _Bedroom2Width = value; }
        }

        public string Bedroom3Features
        {
            get { return _Bedroom3Features; }
            set { _Bedroom3Features = value; }
        }

        public string Bedroom3Length
        {
            get { return _Bedroom3Length; }
            set { _Bedroom3Length = value; }
        }

        public string Bedroom3Level
        {
            get { return _Bedroom3Level; }
            set { _Bedroom3Level = value; }
        }

        public string Bedroom3Width
        {
            get { return _Bedroom3Width; }
            set { _Bedroom3Width = value; }
        }

        public string Bedroom4Features
        {
            get { return _Bedroom4Features; }
            set { _Bedroom4Features = value; }
        }

        public string Bedroom4Length
        {
            get { return _Bedroom4Length; }
            set { _Bedroom4Length = value; }
        }

        public string Bedroom4Level
        {
            get { return _Bedroom4Level; }
            set { _Bedroom4Level = value; }
        }

        public string Bedroom4Width
        {
            get { return _Bedroom4Width; }
            set { _Bedroom4Width = value; }
        }

        public int Bedrooms
        {
            get { return _Bedrooms; }
            set { _Bedrooms = value; }
        }

        public double Betterment
        {
            get { return _Betterment; }
            set { _Betterment = value; }
        }

        public string BuildingConstruction
        {
            get { return _BuildingConstruction; }
            set { _BuildingConstruction = value; }
        }

        public string BuildingName
        {
            get { return _BuildingName; }
            set { _BuildingName = value; }
        }

        public string BusinessIncludes
        {
            get { return _BusinessIncludes; }
            set { _BusinessIncludes = value; }
        }

        public string BusinessIncludesComments
        {
            get { return _BusinessIncludesComments; }
            set { _BusinessIncludesComments = value; }
        }

        public string BusinessName
        {
            get { return _BusinessName; }
            set { _BusinessName = value; }
        }

        public string BusinessOperatingYN
        {
            get { return _BusinessOperatingYN; }
            set { _BusinessOperatingYN = value; }
        }

        public string BusinessWithRealEstateYN
        {
            get { return _BusinessWithRealEstateYN; }
            set { _BusinessWithRealEstateYN = value; }
        }

        public string CommUnit1Description
        {
            get { return _CommUnit1Description; }
            set { _CommUnit1Description = value; }
        }

        public string Complex
        {
            get { return _Complex; }
            set { _Complex = value; }
        }

        public string ComplexCompleted
        {
            get { return _ComplexCompleted; }
            set { _ComplexCompleted = value; }
        }

        public string CondoAttachedOrDetached
        {
            get { return _CondoAttachedOrDetached; }
            set { _CondoAttachedOrDetached = value; }
        }

        public string CondoManagement
        {
            get { return _CondoManagement; }
            set { _CondoManagement = value; }
        }

        public string CondoOwnership
        {
            get { return _CondoOwnership; }
            set { _CondoOwnership = value; }
        }

        public string CondoType
        {
            get { return _CondoType; }
            set { _CondoType = value; }
        }

        public string ConformingUseYN
        {
            get { return _ConformingUseYN; }
            set { _ConformingUseYN = value; }
        }

        public string ConvenientTo
        {
            get { return _ConvenientTo; }
            set { _ConvenientTo = value; }
        }

        public string Cooling
        {
            get { return _Cooling; }
            set { _Cooling = value; }
        }

        public string County
        {
            get { return _County; }
            set { _County = value; }
        }

        public string CumulativeDOM
        {
            get { return _CumulativeDOM; }
            set { _CumulativeDOM = value; }
        }

        public string DenFeatures
        {
            get { return _DenFeatures; }
            set { _DenFeatures = value; }
        }

        public string DenLength
        {
            get { return _DenLength; }
            set { _DenLength = value; }
        }

        public string DenLevel
        {
            get { return _DenLevel; }
            set { _DenLevel = value; }
        }

        public string DenWidth
        {
            get { return _DenWidth; }
            set { _DenWidth = value; }
        }

        public string DiningRoomFeatures
        {
            get { return _DiningRoomFeatures; }
            set { _DiningRoomFeatures = value; }
        }

        public string DiningRoomLength
        {
            get { return _DiningRoomLength; }
            set { _DiningRoomLength = value; }
        }

        public string DiningRoomLevel
        {
            get { return _DiningRoomLevel; }
            set { _DiningRoomLevel = value; }
        }

        public string DiningRoomWidth
        {
            get { return _DiningRoomWidth; }
            set { _DiningRoomWidth = value; }
        }

        public string Directional
        {
            get { return _Directional; }
            set { _Directional = value; }
        }

        public string Directions
        {
            get { return _Directions; }
            set { _Directions = value; }
        }

        public string DockDescription
        {
            get { return _DockDescription; }
            set { _DockDescription = value; }
        }

        public string DockYN
        {
            get { return _DockYN; }
            set { _DockYN = value; }
        }

        public string DOM
        {
            //get { return _DOM; }
            get
            {
               // double __domTemp = Math.Round((DateTime.Today - _InsertDate.Date).TotalDays, 0);
                _DOM = (DateTime.Today.Date - _InsertDate.Date).TotalDays.ToString();

                return _DOM;
            }


            set { _DOM = value; }
        }

        public string elementary
        {
            get { return _elementary; }
            set { _elementary = value; }
        }

        public string elementary1
        {
            get { return _elementary1; }
            set { _elementary1 = value; }
        }

        public string ElevationCert
        {
            get { return _ElevationCert; }
            set { _ElevationCert = value; }
        }

        public string EntertainmentFeatures
        {
            get { return _EntertainmentFeatures; }
            set { _EntertainmentFeatures = value; }
        }

        public string ExerciseRoomFeatures
        {
            get { return _ExerciseRoomFeatures; }
            set { _ExerciseRoomFeatures = value; }
        }

        public string ExerciseRoomLength
        {
            get { return _ExerciseRoomLength; }
            set { _ExerciseRoomLength = value; }
        }

        public string ExerciseRoomLevel
        {
            get { return _ExerciseRoomLevel; }
            set { _ExerciseRoomLevel = value; }
        }

        public string ExerciseRoomWidth
        {
            get { return _ExerciseRoomWidth; }
            set { _ExerciseRoomWidth = value; }
        }

        public string ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; }
        }

        public string ExteriorFeatures
        {
            get { return _ExteriorFeatures; }
            set { _ExteriorFeatures = value; }
        }

        public string FamilyRoomFeatures
        {
            get { return _FamilyRoomFeatures; }
            set { _FamilyRoomFeatures = value; }
        }

        public string FamilyRoomLength
        {
            get { return _FamilyRoomLength; }
            set { _FamilyRoomLength = value; }
        }

        public string FamilyRoomLevel
        {
            get { return _FamilyRoomLevel; }
            set { _FamilyRoomLevel = value; }
        }

        public string FamilyRoomWidth
        {
            get { return _FamilyRoomWidth; }
            set { _FamilyRoomWidth = value; }
        }

        public string FinancingAvailable
        {
            get { return _FinancingAvailable; }
            set { _FinancingAvailable = value; }
        }

        public double FinishedBasementSqFt
        {
            get { return _FinishedBasementSqFt; }
            set { _FinishedBasementSqFt = value; }
        }

        public string Fireplace
        {
            get { return _Fireplace; }
            set { _Fireplace = value; }
        }

        public int FireplaceNumber
        {
            get { return _FireplaceNumber; }
            set { _FireplaceNumber = value; }
        }

        public string FirstMonthRequired
        {
            get { return _FirstMonthRequired; }
            set { _FirstMonthRequired = value; }
        }

        public string FloodInsuranceRequired
        {
            get { return _FloodInsuranceRequired; }
            set { _FloodInsuranceRequired = value; }
        }

        public string Floors
        {
            get { return _Floors; }
            set { _Floors = value; }
        }

        public string ForSaleYN
        {
            get { return _ForSaleYN; }
            set { _ForSaleYN = value; }
        }

        public string Foundation
        {
            get { return _Foundation; }
            set { _Foundation = value; }
        }

        public double FoundationDepth
        {
            get { return _FoundationDepth; }
            set { _FoundationDepth = value; }
        }

        public double FoundationWidth
        {
            get { return _FoundationWidth; }
            set { _FoundationWidth = value; }
        }

        public double FoundationWingDepth
        {
            get { return _FoundationWingDepth; }
            set { _FoundationWingDepth = value; }
        }

        public double FoundationWingWidth
        {
            get { return _FoundationWingWidth; }
            set { _FoundationWingWidth = value; }
        }

        public string FoyerFeatures
        {
            get { return _FoyerFeatures; }
            set { _FoyerFeatures = value; }
        }

        public string FoyerLength
        {
            get { return _FoyerLength; }
            set { _FoyerLength = value; }
        }

        public string FoyerLevel
        {
            get { return _FoyerLevel; }
            set { _FoyerLevel = value; }
        }

        public string FoyerWidth
        {
            get { return _FoyerWidth; }
            set { _FoyerWidth = value; }
        }

        public string FuelType
        {
            get { return _FuelType; }
            set { _FuelType = value; }
        }

        public int FullBaths
        {
            get { return _FullBaths; }
            set { _FullBaths = value; }
        }

        public string GalleryFeatures
        {
            get { return _GalleryFeatures; }
            set { _GalleryFeatures = value; }
        }

        public string GameRoomFeatures
        {
            get { return _GameRoomFeatures; }
            set { _GameRoomFeatures = value; }
        }

        public string GarageDescription
        {
            get { return _GarageDescription; }
            set { _GarageDescription = value; }
        }

        public string GarageYN
        {
            get { return _GarageYN; }
            set { _GarageYN = value; }
        }

        public string GreatRoomFeatures
        {
            get { return _GreatRoomFeatures; }
            set { _GreatRoomFeatures = value; }
        }

        public int HalfBaths
        {
            get { return _HalfBaths; }
            set { _HalfBaths = value; }
        }

        public string Heating
        {
            get { return _Heating; }
            set { _Heating = value; }
        }

        public string HomeOfficeFeatures
        {
            get { return _HomeOfficeFeatures; }
            set { _HomeOfficeFeatures = value; }
        }

        public string HotWater
        {
            get { return _HotWater; }
            set { _HotWater = value; }
        }

        public string HotWaterSource
        {
            get { return _HotWaterSource; }
            set { _HotWaterSource = value; }
        }

        public double ImprovementAssessment
        {
            get { return _ImprovementAssessment; }
            set { _ImprovementAssessment = value; }
        }

        public string InLawAptFeatures
        {
            get { return _InLawAptFeatures; }
            set { _InLawAptFeatures = value; }
        }

        public DateTime InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
            // get
            //{
            //    if (CheckDate(_InsertDate.ToShortDateString()) == true)
            //    {
            //        return _InsertDate;
            //    }
            //    else

            //    {
            //        _InsertDate = _LastModifiedDate;
            //        return _InsertDate;
            //    }


            //}
        }

        public string InteriorFeatures
        {
            get { return _InteriorFeatures; }
            set { _InteriorFeatures = value; }
        }

        public string IrregularYN
        {
            get { return _IrregularYN; }
            set { _IrregularYN = value; }
        }

        public string KitchenDiningCombo
        {
            get { return _KitchenDiningCombo; }
            set { _KitchenDiningCombo = value; }
        }

        public string KitchenFeatures
        {
            get { return _KitchenFeatures; }
            set { _KitchenFeatures = value; }
        }

        public double LandAssessment
        {
            get { return _LandAssessment; }
            set { _LandAssessment = value; }
        }

        public string LandCourtCert
        {
            get { return _LandCourtCert; }
            set { _LandCourtCert = value; }
        }

        public string LandlordPays
        {
            get { return _LandlordPays; }
            set { _LandlordPays = value; }
        }

        public DateTime LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set { _LastModifiedDate = value; }
        }

        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        public string LaundryRoomFeatures
        {
            get { return _LaundryRoomFeatures; }
            set { _LaundryRoomFeatures = value; }
        }

        public string LaundryRoomLength
        {
            get { return _LaundryRoomLength; }
            set { _LaundryRoomLength = value; }
        }

        public string LaundryRoomLevel
        {
            get { return _LaundryRoomLevel; }
            set { _LaundryRoomLevel = value; }
        }

        public string LaundryRoomWidth
        {
            get { return _LaundryRoomWidth; }
            set { _LaundryRoomWidth = value; }
        }

        public string LeadPaint
        {
            get { return _LeadPaint; }
            set { _LeadPaint = value; }
        }

        public string LibraryFeatures
        {
            get { return _LibraryFeatures; }
            set { _LibraryFeatures = value; }
        }

        public string ListingAgentID
        {
            get { return _ListingAgentID; }
            set { _ListingAgentID = value; }
        }

        public string ListingAgentName
        {
            get { return _ListingAgentName; }
            set { _ListingAgentName = value; }
        }

        public string ListingNumber
        {
            get { return _ListingNumber; }
            set { _ListingNumber = value; }
        }

        public string ListingOfficeName
        {
            get { return _ListingOfficeName; }
            set { _ListingOfficeName = value; }
        }

        public double ListingPrice
        {
            get { return _ListingPrice; }
            set { _ListingPrice = value; }
        }

        public string ListingRid
        {
            get { return _ListingRid; }
            set { _ListingRid = value; }
        }

        public string ListingTable
        {
            get { return _ListingTable; }
            set { _ListingTable = value; }
        }

        public string ListOffUrl
        {
            get { return _ListOffUrl; }
            set { _ListOffUrl = value; }
        }

        public double ListPricePerSqFt
        {
            get { return _ListPricePerSqFt; }
            set { _ListPricePerSqFt = value; }
        }

        public string LivingDiningComboLevel
        {
            get { return _LivingDiningComboLevel; }
            set { _LivingDiningComboLevel = value; }
        }

        public string LivingDiningComboYN
        {
            get { return _LivingDiningComboYN; }
            set { _LivingDiningComboYN = value; }
        }

        public string LivingRoomFeatures
        {
            get { return _LivingRoomFeatures; }
            set { _LivingRoomFeatures = value; }
        }

        public string LivingRoomLength
        {
            get { return _LivingRoomLength; }
            set { _LivingRoomLength = value; }
        }

        public string LivingRoomWidth
        {
            get { return _LivingRoomWidth; }
            set { _LivingRoomWidth = value; }
        }

        public double LivingSpace
        {
            get { return _LivingSpace; }
            set { _LivingSpace = value; }
        }

        public string LocationDescription
        {
            get { return _LocationDescription; }
            set { _LocationDescription = value; }
        }

        public string LoftFeatures
        {
            get { return _LoftFeatures; }
            set { _LoftFeatures = value; }
        }

        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        public double LotDepth
        {
            get { return _LotDepth; }
            set { _LotDepth = value; }
        }

        public string LotDescription
        {
            get { return _LotDescription; }
            set { _LotDescription = value; }
        }

        public string LotNumber
        {
            get { return _LotNumber; }
            set { _LotNumber = value; }
        }

        public string LotSizeSource
        {
            get { return _LotSizeSource; }
            set { _LotSizeSource = value; }
        }

        public double LotSizeSqFt
        {
            get { return _LotSizeSqFt; }
            set { _LotSizeSqFt = value; }
        }

        public double LotWidth
        {
            get { return _LotWidth; }
            set { _LotWidth = value; }
        }

        public string MallParkName
        {
            get { return _MallParkName; }
            set { _MallParkName = value; }
        }

        public string MassUseCode
        {
            get { return _MassUseCode; }
            set { _MassUseCode = value; }
        }

        public string MasterBathFetaures
        {
            get { return _MasterBathFetaures; }
            set { _MasterBathFetaures = value; }
        }

        public string MasterBedroomFeatures
        {
            get { return _MasterBedroomFeatures; }
            set { _MasterBedroomFeatures = value; }
        }

        public string MaxNumberOfUnits
        {
            get { return _MaxNumberOfUnits; }
            set { _MaxNumberOfUnits = value; }
        }

        public string MediaRoomFeatures
        {
            get { return _MediaRoomFeatures; }
            set { _MediaRoomFeatures = value; }
        }

        public string MembershipRequired
        {
            get { return _MembershipRequired; }
            set { _MembershipRequired = value; }
        }

        public string MilesToBeach
        {
            get { return _MilesToBeach; }
            set { _MilesToBeach = value; }
        }

        public int MLNumber
        {
            get { return _MLNumber; }
            set { _MLNumber = value; }
        }

        public string MLSApproved
        {
            get { return _MLSApproved; }
            set { _MLSApproved = value; }
        }

        public string MLSID
        {
            get { return _MLSID; }
            set { _MLSID = value; }
        }

        public double MonthlyFeeAmount
        {
            get { return _MonthlyFeeAmount; }
            set { _MonthlyFeeAmount = value; }
        }

        public string MudRoomFeatures
        {
            get { return _MudRoomFeatures; }
            set { _MudRoomFeatures = value; }
        }

        public string NeighborhoodAmenities
        {
            get { return _NeighborhoodAmenities; }
            set { _NeighborhoodAmenities = value; }
        }

        public string NumberOfCars
        {
            get { return _NumberOfCars; }
            set { _NumberOfCars = value; }
        }

        public int NumRooms
        {
            get { return _NumRooms; }
            set { _NumRooms = value; }
        }

        public string OccupancyComments
        {
            get { return _OccupancyComments; }
            set { _OccupancyComments = value; }
        }

        public string ListingOfficeID
        {
            get { return _ListingOfficeID; }
            set { _ListingOfficeID = value; }
        }

        //
        public string OfficeID
        {
            get { return _OfficeID; }
            set { _OfficeID = value; }
        }

        public DateTime offmarkett
        {
            get { return _offmarkett; }
            set { _offmarkett = value; }
        }

        public DateTime onmarketti
        {
            get { return _onmarketti; }
            set { _onmarketti = value; }
        }

        public double OriginalListPrice
        {
            get { return _OriginalListPrice; }
            set { _OriginalListPrice = value; }
        }

        public double OtherAssessment
        {
            get { return _OtherAssessment; }
            set { _OtherAssessment = value; }
        }

        public string OtherRoom1Features
        {
            get { return _OtherRoom1Features; }
            set { _OtherRoom1Features = value; }
        }

        public string OtherRoom2Features
        {
            get { return _OtherRoom2Features; }
            set { _OtherRoom2Features = value; }
        }

        public string OtherRoom3Features
        {
            get { return _OtherRoom3Features; }
            set { _OtherRoom3Features = value; }
        }

        public string OwnerName
        {
            get { return _OwnerName; }
            set { _OwnerName = value; }
        }

        public string ParkingDescription
        {
            get { return _ParkingDescription; }
            set { _ParkingDescription = value; }
        }

        public string ParkingSpaces
        {
            get { return _ParkingSpaces; }
            set { _ParkingSpaces = value; }
        }

        public string PetsAllowed
        {
            get { return _PetsAllowed; }
            set { _PetsAllowed = value; }
        }

        public DateTime PicTimestamp
        {
            get { return _PicTimestamp; }
            set { _PicTimestamp = value; }
        }

        public int PictureCount
        {
            get { return _PictureCount; }
            set { _PictureCount = value; }
        }

        public string Plan
        {
            get { return _Plan; }
            set { _Plan = value; }
        }

        public string PlayRoomFeatures
        {
            get { return _PlayRoomFeatures; }
            set { _PlayRoomFeatures = value; }
        }

        public string Pool
        {
            get { return _Pool; }
            set { _Pool = value; }
        }

        public string PoolDescription
        {
            get { return _PoolDescription; }
            set { _PoolDescription = value; }
        }

        public string PresentUse
        {
            get { return _PresentUse; }
            set { _PresentUse = value; }
        }

        public double previousli
        {
            get { return _previousli; }
            set { _previousli = value; }
        }

        public DateTime pricechange
        {
            get { return _pricechange; }
            set { _pricechange = value; }
        }

        public string PropertySubType1
        {
            get { return _PropertySubType1; }
            set { _PropertySubType1 = value; }
        }

        public string PublicRemarks
        {
            get { return _PublicRemarks; }
            set { _PublicRemarks = value; }
        }

        public string PublishToInternet
        {
            get { return _PublishToInternet; }
            set { _PublishToInternet = value; }
        }

        public string Renovated
        {
            get { return _Renovated; }
            set { _Renovated = value; }
        }

        public string RentalType
        {
            get { return _RentalType; }
            set { _RentalType = value; }
        }

        public string Restrictions
        {
            get { return _Restrictions; }
            set { _Restrictions = value; }
        }

        public string RoadFrontage
        {
            get { return _RoadFrontage; }
            set { _RoadFrontage = value; }
        }

        public string Roofing
        {
            get { return _Roofing; }
            set { _Roofing = value; }
        }

        public string SchoolDistrict
        {
            get { return _SchoolDistrict; }
            set { _SchoolDistrict = value; }
        }

        public string SecondKitchen
        {
            get { return _SecondKitchen; }
            set { _SecondKitchen = value; }
        }

        public string Security
        {
            get { return _Security; }
            set { _Security = value; }
        }

        public string SeparateLivingQtrsDescription
        {
            get { return _SeparateLivingQtrsDescription; }
            set { _SeparateLivingQtrsDescription = value; }
        }

        public string SeparateLivingQtrsYN
        {
            get { return _SeparateLivingQtrsYN; }
            set { _SeparateLivingQtrsYN = value; }
        }

        public string SepticTankGrade
        {
            get { return _SepticTankGrade; }
            set { _SepticTankGrade = value; }
        }

        public string Sewer
        {
            get { return _Sewer; }
            set { _Sewer = value; }
        }

        public string ShowingInstructions
        {
            get { return _ShowingInstructions; }
            set { _ShowingInstructions = value; }
        }

        public string SidingDescription
        {
            get { return _SidingDescription; }
            set { _SidingDescription = value; }
        }

        public string SittingRoomFeatures
        {
            get { return _SittingRoomFeatures; }
            set { _SittingRoomFeatures = value; }
        }

        public string SolarFeatures
        {
            get { return _SolarFeatures; }
            set { _SolarFeatures = value; }
        }

        public DateTime SoldDate
        {
            get { return _SoldDate; }
            set { _SoldDate = value; }
        }

        public double SoldPrice
        {
            get { return _SoldPrice; }
            set { _SoldPrice = value; }
        }

        public double SoldPricePerSqFt
        {
            get { return _SoldPricePerSqFt; }
            set { _SoldPricePerSqFt = value; }
        }

        public string SpecialListingCond
        {
            get { return _SpecialListingCond; }
            set { _SpecialListingCond = value; }
        }

        public string SqFtSource
        {
            get { return _SqFtSource; }
            set { _SqFtSource = value; }
        }

        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        public DateTime StatusChangeDate
        {
            get { return _StatusChangeDate; }
            set { _StatusChangeDate = value; }
        }

        public string StatusCode
        {
            get { return _StatusCode; }
            set { _StatusCode = value; }
        }

        public string Stories
        {
            get { return _Stories; }
            set { _Stories = value; }
        }

        public string StreetDescription
        {
            get { return _StreetDescription; }
            set { _StreetDescription = value; }
        }

        public string StreetMod
        {
            get { return _StreetMod; }
            set { _StreetMod = value; }
        }

        public string StreetName
        {
            get { return _StreetName; }
            set { _StreetName = value; }
        }

        public string StreetNumber
        {
            get { return _StreetNumber; }
            set { _StreetNumber = value; }
        }

        public string StreetType
        {
            get { return _StreetType; }
            set { _StreetType = value; }
        }

        public string StudioFeatures
        {
            get { return _StudioFeatures; }
            set { _StudioFeatures = value; }
        }

        public string Style
        {
            get { return _Style; }
            set { _Style = value; }
        }

        public string Subdivision
        {
            get { return _Subdivision; }
            set { _Subdivision = value; }
        }

        public string SunRoomFeatures
        {
            get { return _SunRoomFeatures; }
            set { _SunRoomFeatures = value; }
        }

        public double Taxes
        {
            get { return _Taxes; }
            set { _Taxes = value; }
        }

        public string TaxID
        {
            get { return _TaxID; }
            set { _TaxID = value; }
        }

        public int TaxYear
        {
            get { return _TaxYear; }
            set { _TaxYear = value; }
        }

        public string TitleReferenceBook
        {
            get { return _TitleReferenceBook; }
            set { _TitleReferenceBook = value; }
        }

        public string TitleReferencePage
        {
            get { return _TitleReferencePage; }
            set { _TitleReferencePage = value; }
        }

        public double TotalAssessments
        {
            get { return _TotalAssessments; }
            set { _TotalAssessments = value; }
        }

        public double TotalBaths
        {
            get { return _TotalBaths; }
            set { _TotalBaths = value; }
        }

        public string Town
        {
            get { return _Town; }
            set { _Town = value; }
        }

        public DateTime UnderContractDate
        {
            get { return _UnderContractDate; }
            set { _UnderContractDate = value; }
        }

        public string UndergroundFuelTank
        {
            get { return _UndergroundFuelTank; }
            set { _UndergroundFuelTank = value; }
        }

        public int Unit1Bedrooms
        {
            get { return _Unit1Bedrooms; }
            set { _Unit1Bedrooms = value; }
        }

        public string Unit1Features
        {
            get { return _Unit1Features; }
            set { _Unit1Features = value; }
        }

        public int Unit1FloorLevel
        {
            get { return _Unit1FloorLevel; }
            set { _Unit1FloorLevel = value; }
        }

        public string Unit1FloorLvlDes
        {
            get { return _Unit1FloorLvlDes; }
            set { _Unit1FloorLvlDes = value; }
        }

        public int Unit1FullBaths
        {
            get { return _Unit1FullBaths; }
            set { _Unit1FullBaths = value; }
        }

        public int Unit1HalfBaths
        {
            get { return _Unit1HalfBaths; }
            set { _Unit1HalfBaths = value; }
        }

        public string Unit1Leased
        {
            get { return _Unit1Leased; }
            set { _Unit1Leased = value; }
        }

        public string Unit1LeaseExpires
        {
            get { return _Unit1LeaseExpires; }
            set { _Unit1LeaseExpires = value; }
        }

        public double Unit1MonthlyRent
        {
            get { return _Unit1MonthlyRent; }
            set { _Unit1MonthlyRent = value; }
        }

        public int Unit1Rooms
        {
            get { return _Unit1Rooms; }
            set { _Unit1Rooms = value; }
        }

        public int Unit2Bedrooms
        {
            get { return _Unit2Bedrooms; }
            set { _Unit2Bedrooms = value; }
        }

        public string Unit2Features
        {
            get { return _Unit2Features; }
            set { _Unit2Features = value; }
        }

        public int Unit2FloorLevel
        {
            get { return _Unit2FloorLevel; }
            set { _Unit2FloorLevel = value; }
        }

        public string Unit2FloorLvlDes
        {
            get { return _Unit2FloorLvlDes; }
            set { _Unit2FloorLvlDes = value; }
        }

        public int Unit2FullBaths
        {
            get { return _Unit2FullBaths; }
            set { _Unit2FullBaths = value; }
        }

        public int Unit2HalfBaths
        {
            get { return _Unit2HalfBaths; }
            set { _Unit2HalfBaths = value; }
        }

        public string Unit2Leased
        {
            get { return _Unit2Leased; }
            set { _Unit2Leased = value; }
        }

        public string Unit2LeaseExpires
        {
            get { return _Unit2LeaseExpires; }
            set { _Unit2LeaseExpires = value; }
        }

        public double Unit2MonthlyRent
        {
            get { return _Unit2MonthlyRent; }
            set { _Unit2MonthlyRent = value; }
        }

        public int Unit2Rooms
        {
            get { return _Unit2Rooms; }
            set { _Unit2Rooms = value; }
        }

        public int Unit3Bedrooms
        {
            get { return _Unit3Bedrooms; }
            set { _Unit3Bedrooms = value; }
        }

        public string Unit3Features
        {
            get { return _Unit3Features; }
            set { _Unit3Features = value; }
        }

        public int Unit3FloorLevel
        {
            get { return _Unit3FloorLevel; }
            set { _Unit3FloorLevel = value; }
        }

        public string Unit3FloorLvlDes
        {
            get { return _Unit3FloorLvlDes; }
            set { _Unit3FloorLvlDes = value; }
        }

        public int Unit3FullBaths
        {
            get { return _Unit3FullBaths; }
            set { _Unit3FullBaths = value; }
        }

        public int Unit3HalfBaths
        {
            get { return _Unit3HalfBaths; }
            set { _Unit3HalfBaths = value; }
        }

        public string Unit3Leased
        {
            get { return _Unit3Leased; }
            set { _Unit3Leased = value; }
        }

        public string Unit3LeaseExpires
        {
            get { return _Unit3LeaseExpires; }
            set { _Unit3LeaseExpires = value; }
        }

        public double Unit3MonthlyRent
        {
            get { return _Unit3MonthlyRent; }
            set { _Unit3MonthlyRent = value; }
        }

        public int Unit3Rooms
        {
            get { return _Unit3Rooms; }
            set { _Unit3Rooms = value; }
        }

        public int Unit4Bedrooms
        {
            get { return _Unit4Bedrooms; }
            set { _Unit4Bedrooms = value; }
        }

        public string Unit4Features
        {
            get { return _Unit4Features; }
            set { _Unit4Features = value; }
        }

        public int Unit4FloorLevel
        {
            get { return _Unit4FloorLevel; }
            set { _Unit4FloorLevel = value; }
        }

        public string Unit4FloorLvlDes
        {
            get { return _Unit4FloorLvlDes; }
            set { _Unit4FloorLvlDes = value; }
        }

        public int Unit4FullBaths
        {
            get { return _Unit4FullBaths; }
            set { _Unit4FullBaths = value; }
        }

        public int Unit4HalfBaths
        {
            get { return _Unit4HalfBaths; }
            set { _Unit4HalfBaths = value; }
        }

        public string Unit4Leased
        {
            get { return _Unit4Leased; }
            set { _Unit4Leased = value; }
        }

        public string Unit4LeaseExpires
        {
            get { return _Unit4LeaseExpires; }
            set { _Unit4LeaseExpires = value; }
        }

        public double Unit4MonthlyRent
        {
            get { return _Unit4MonthlyRent; }
            set { _Unit4MonthlyRent = value; }
        }

        public int Unit4Rooms
        {
            get { return _Unit4Rooms; }
            set { _Unit4Rooms = value; }
        }

        public string UnitNumber
        {
            get { return _UnitNumber; }
            set { _UnitNumber = value; }
        }

        public string UnitPlacement
        {
            get { return _UnitPlacement; }
            set { _UnitPlacement = value; }
        }

        public string UtilityRoomFeatures
        {
            get { return _UtilityRoomFeatures; }
            set { _UtilityRoomFeatures = value; }
        }

        public string Village
        {
            get { return _Village; }
            set { _Village = value; }
        }

        public string VirtualTourLink
        {
            get { return _VirtualTourLink; }
            set { _VirtualTourLink = value; }
        }

        public string WarrantyAvailable
        {
            get { return _WarrantyAvailable; }
            set { _WarrantyAvailable = value; }
        }

        public string WaterAccess
        {
            get { return _WaterAccess; }
            set { _WaterAccess = value; }
        }

        public string WaterFrontDesc
        {
            get { return _WaterFrontDesc; }
            set { _WaterFrontDesc = value; }
        }

        public string WaterfrontYN
        {
            get { return _WaterfrontYN; }
            set { _WaterfrontYN = value; }
        }

        public string WaterSource
        {
            get { return _WaterSource; }
            set { _WaterSource = value; }
        }

        public string WaterViewDesc
        {
            get { return _WaterViewDesc; }
            set { _WaterViewDesc = value; }
        }

        public string WaterViewYN
        {
            get { return _WaterViewYN; }
            set { _WaterViewYN = value; }
        }

        public DateTime WithdrawlDate
        {
            get { return _WithdrawlDate; }
            set { _WithdrawlDate = value; }
        }

        public string WorkShopFeatures
        {
            get { return _WorkShopFeatures; }
            set { _WorkShopFeatures = value; }
        }

        public int YearBuilt
        {
            get { return _YearBuilt; }
            set { _YearBuilt = value; }
        }

        public string YearBuiltDesc
        {
            get { return _YearBuiltDesc; }
            set { _YearBuiltDesc = value; }
        }

        public string YearRoundYN
        {
            get { return _YearRoundYN; }
            set { _YearRoundYN = value; }
        }

        public string YearsEstablished
        {
            get { return _YearsEstablished; }
            set { _YearsEstablished = value; }
        }

        public string ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        public string Zoning
        {
            get { return _Zoning; }
            set { _Zoning = value; }
        }







        #endregion
    }
}
