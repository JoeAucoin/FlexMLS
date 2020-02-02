using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Localization;

namespace GIBS.Modules.FlexMLS.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "GIBS_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string FlexMLSConnectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {



            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            FlexMLSConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FlexMLS"].ConnectionString;  

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion



        //public void LoadSettings()
        //{
        //    try
        //    {

        //        FlexMLSSettings settingsData = new FlexMLSSettings(this.mo);

        //        if (string.IsNullOrEmpty(settingsData.MlsDataBase))
        //        {
        //        //    vStartDate = DateTime.Now;
        //        }
        //        else
        //        {
        //          //  vStartDate = Convert.ToDateTime(settingsData.StartDate);
        //        }





        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }
        //}


        /// <summary>
        /// connectionString="Data Source=(local);Initial Catalog=DNN70;User ID=sa;Password=SugarCone"
        /// </summary>



        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region override methods

        public override IDataReader FlexMLS_Search(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search"), propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM);
        }

        public override IDataReader FlexMLS_Search_YearBuilt(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string yearBuilt)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search_YearBuilt"), propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM, yearBuilt);
        }

        public override IDataReader FlexMLS_Search_LastModified(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string lastModified, string complex)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search_LastModified"), propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM, lastModified, complex);
        }

        public override IDataReader FlexMLS_Search_Condo(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string complex)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search_Condo"), propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM, complex);
        }

        public override IDataReader FlexMLS_Search_MLS_Numbers(string mlsNumbers)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search_MLS_Numbers"), mlsNumbers);
        }

        public override IDataReader FlexMLS_Lookup_Town()
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Lookup_Town"));
        }

        public override IDataReader FlexMLS_Lookup_Village(string town)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Lookup_Village"), town);
        }

        public override IDataReader FlexMLS_Get_Listing(int mlNumber)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Get_Listing"), mlNumber);
        }

        public override void FlexMLS_Favorites_Add(int moduleId, string favorite, string favoriteType, int userID, bool emailSearch)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FlexMLS_Favorites_Add"), moduleId, favorite, favoriteType, userID, emailSearch);
        }

        public override IDataReader FlexMLS_Favorites_GetEmailSearches()
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FlexMLS_Favorites_GetEmailSearches"));
        }

        public override IDataReader TaxRates_GetList(string town, string year)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("TaxRates_GetList"), town, year);
        }


        public override void FlexMLS_ListingViews_Add(int moduleId, string content, string listingNumber, double listingPrice, int createdByUserID)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("FlexMLS_ListingViews_Add"), moduleId, content, listingNumber, listingPrice, createdByUserID);
        }

        public override IDataReader FlexMLS_ListingViews_Get(int numberOfRecords)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("FlexMLS_ListingViews_Get"), numberOfRecords);
        }

        // UN USED
        public override IDataReader GetFlexMLSs(int moduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetFlexMLSs"), moduleId);
        }

        public override IDataReader GetFlexMLS(int moduleId, int itemId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetFlexMLS"), moduleId, itemId);
        }

        public override IDataReader FlexMLS_Get_CondoComplex()
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Get_CondoComplex"));
        }


        public override void UpdateFlexMLS(int moduleId, int itemId, string content, int userId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("UpdateFlexMLS"), moduleId, itemId, content, userId);
        }

        public override void DeleteFlexMLS(int moduleId, int itemId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteFlexMLS"), moduleId, itemId);
        }

        // CONDO MODULE
        public override IDataReader FlexMLS_Search_Condo_By_Town(string town)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search_Condo_By_Town"), town);
        }

        public override IDataReader FlexMLS_Get_CondoComplexName(string complexCode)
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Lookup_CondoComplexName"), complexCode);
        }

        public override IDataReader FlexMLS_Search_Condo_TownList()
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Search_Condo_TownList"));
        }

        // FlexMLS_Get_Offices

        public override IDataReader FlexMLS_Get_Offices()
        {
            return (IDataReader)SqlHelper.ExecuteReader(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_Get_Offices"));
        }

        // IMAGES NEEDED
        public override void FlexMLS_ImagesNeeded_Insert(string listingNumber)
        {
            SqlHelper.ExecuteNonQuery(FlexMLSConnectionString, GetFullyQualifiedName("FlexMLS_ImagesNeeded_Insert"), listingNumber);
        }

        #endregion
    }
}
