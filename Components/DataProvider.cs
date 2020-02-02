using System;
using System.Data;
using DotNetNuke;
using DotNetNuke.Framework;

namespace GIBS.Modules.FlexMLS.Components
{
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "GIBS.Modules.FlexMLS.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion


        #region Abstract methods

        /* implement the methods that the dataprovider should */
        // FlexMLS_Search
        public abstract IDataReader FlexMLS_Search(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM);
        public abstract IDataReader FlexMLS_Search_YearBuilt(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string yearBuilt);
        public abstract IDataReader FlexMLS_Search_LastModified(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string lastModified, string complex);
        public abstract IDataReader FlexMLS_Search_Condo(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string complex);
        public abstract IDataReader FlexMLS_Search_MLS_Numbers(string mlsNumbers);

        public abstract IDataReader FlexMLS_Lookup_Town();
        public abstract IDataReader FlexMLS_Lookup_Village(string town);

        public abstract IDataReader FlexMLS_Get_Listing(int mlNumber);


        public abstract void FlexMLS_Favorites_Add(int moduleId, string favorite, string favoriteType, int userID, bool emailSearch);
        public abstract IDataReader FlexMLS_Favorites_GetEmailSearches();

        public abstract IDataReader TaxRates_GetList(string town, string year);


        public abstract IDataReader GetFlexMLSs(int moduleId);
        public abstract IDataReader GetFlexMLS(int moduleId, int itemId);

        public abstract void UpdateFlexMLS(int moduleId, int itemId, string content, int userId);
        public abstract void DeleteFlexMLS(int moduleId, int itemId);

        //GIBS_FlexMLS_ListingViews
        public abstract void FlexMLS_ListingViews_Add(int moduleId, string content, string listingNumber, double listingPrice, int createdByUserID);
        public abstract IDataReader FlexMLS_ListingViews_Get(int numberOfRecords);

        //FlexMLS_List
        public abstract IDataReader FlexMLS_Get_CondoComplex();

        //FlexMLS_Get_Offices
        public abstract IDataReader FlexMLS_Get_Offices();

        // CONDO MODULE
        public abstract IDataReader FlexMLS_Search_Condo_By_Town(string town);
        public abstract IDataReader FlexMLS_Search_Condo_TownList();
        public abstract IDataReader FlexMLS_Get_CondoComplexName(string complexCode);

        // IMAGES NEEDED
        public abstract void FlexMLS_ImagesNeeded_Insert(string listingNumber);

        #endregion

    }



}
