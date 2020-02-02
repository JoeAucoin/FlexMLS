using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace GIBS.Modules.FlexMLS.Components
{
    public class FlexMLSController :  IPortable
    {

        #region public method


        //public abstract IDataReader FlexMLS_Search(string city, string propertyType);

        public List<FlexMLSInfo> FlexMLS_Search(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search(propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM));
        }

        public List<FlexMLSInfo> FlexMLS_Search_YearBuilt(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string yearBuilt)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search_YearBuilt(propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM, yearBuilt));
        }

        public List<FlexMLSInfo> FlexMLS_Search_Condo(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string complex)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search_Condo(propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM, complex));
        }

        public List<FlexMLSInfo> FlexMLS_Search_MLS_Numbers(string mlsNumbers)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search_MLS_Numbers(mlsNumbers));
        }

        public List<FlexMLSInfo> FlexMLS_Lookup_Town()
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Lookup_Town());
        }

        public List<FlexMLSInfo> FlexMLS_Lookup_Village(string town)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Lookup_Village(town));
        }

        public List<FlexMLSInfo> FlexMLS_Search_LastModified(string propertyType, string town, string village, string bedRooms, string bathRooms, string waterFront, string waterView, string priceLow, string priceHigh, string listingOfficeMLSID, string dOM, string lastModified, string complex)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search_LastModified(propertyType, town, village, bedRooms, bathRooms, waterFront, waterView, priceLow, priceHigh, listingOfficeMLSID, dOM, lastModified, complex));
        }

        public FlexMLSInfo FlexMLS_Get_Listing(int mlNumber)
        {
          //  return (FlexMLSInfo)CBO.FillObject(DataProvider.Instance().FlexMLS_Get_Listing(mlNumber), typeof(FlexMLSInfo));
            return CBO.FillObject<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Get_Listing(mlNumber));
        }


        public void FlexMLS_Favorites_Add(FlexMLSInfo info)
        {
            //check we have some content to store
            if (info.Favorite != string.Empty)
            {
                DataProvider.Instance().FlexMLS_Favorites_Add(info.ModuleId, info.Favorite, info.FavoriteType, info.UserID, info.EmailSearch);
            }
        }

        public List<FlexMLSInfo> FlexMLS_Favorites_GetEmailSearches()
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Favorites_GetEmailSearches());
        }


        public List<FlexMLSInfo> TaxRates_GetList(string town, string year)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().TaxRates_GetList(town, year));
        }

        // ListingViews
        public void FlexMLS_ListingViews_Add(FlexMLSInfo info)
        {
            //check we have some content to store
            if (info.ListingNumber.Length > 0)
            {
                DataProvider.Instance().FlexMLS_ListingViews_Add(info.ModuleId, info.Content, info.ListingNumber, info.ListingPrice, info.CreatedByUserID);
            }
        }

        public List<FlexMLSInfo> FlexMLS_ListingViews_Get(int numberOfRecords)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_ListingViews_Get(numberOfRecords));
        }

        /// <summary>
        /// Gets all the FlexMLSInfo objects for items matching the this moduleId
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<FlexMLSInfo> GetFlexMLSs(int moduleId)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().GetFlexMLSs(moduleId));
        }

        /// <summary>
        /// Get an info object from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public FlexMLSInfo GetFlexMLS(int moduleId, int itemId)
        {
          //  return (FlexMLSInfo)CBO.FillObject(DataProvider.Instance().GetFlexMLS(moduleId, itemId), typeof(FlexMLSInfo));
            return CBO.FillObject<FlexMLSInfo>(DataProvider.Instance().GetFlexMLS(moduleId, itemId));
        }


        public List<FlexMLSInfo> FlexMLS_Get_CondoComplex()
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Get_CondoComplex());
        }

        public FlexMLSInfo FlexMLS_Get_CondoComplexName(string complexCode)
        {
          //  return (FlexMLSInfo)CBO.FillObject(DataProvider.Instance().FlexMLS_Get_CondoComplexName(complexCode), typeof(FlexMLSInfo));
            return CBO.FillObject<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Get_CondoComplexName(complexCode));
        }



        // IMAGES NEEDED
        public void FlexMLS_ImagesNeeded_Insert(FlexMLSInfo info)
        {
            //check we have some content to store
            if (info.ListingNumber != string.Empty)
            {
                DataProvider.Instance().FlexMLS_ImagesNeeded_Insert(info.ListingNumber);
            }
        }

        /// <summary>
        /// Adds a new FlexMLSInfo object into the database
        /// </summary>
        /// <param name="info"></param>


        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>
        public void UpdateFlexMLS(FlexMLSInfo info)
        {
            //check we have some content to update
            if (info.Content != string.Empty)
            {
                DataProvider.Instance().UpdateFlexMLS(info.ModuleId, info.ItemId, info.Content, info.CreatedByUser);
            }
        }


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteFlexMLS(int moduleId, int itemId)
        {
            DataProvider.Instance().DeleteFlexMLS(moduleId, itemId);
        }

        // CONDO MODULE
        public List<FlexMLSInfo> FlexMLS_Search_Condo_By_Town(string town)
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search_Condo_By_Town(town));
        }
        public List<FlexMLSInfo> FlexMLS_Search_Condo_TownList()
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Search_Condo_TownList());
        }

        //FlexMLS_Get_Offices
        public List<FlexMLSInfo> FlexMLS_Get_Offices()
        {
            return CBO.FillCollection<FlexMLSInfo>(DataProvider.Instance().FlexMLS_Get_Offices());
        }




        #endregion

        #region ISearchable Members

        /// <summary>
        /// Implements the search interface required to allow DNN to index/search the content of your
        /// module
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo modInfo)
        //{
        //    SearchItemInfoCollection searchItems = new SearchItemInfoCollection();

        //    List<FlexMLSInfo> infos = GetFlexMLSs(modInfo.ModuleID);

        //    foreach (FlexMLSInfo info in infos)
        //    {
        //        SearchItemInfo searchInfo = new SearchItemInfo(modInfo.ModuleTitle, info.Content, info.CreatedByUser, info.CreatedDate,
        //                                            modInfo.ModuleID, info.ItemId.ToString(), info.Content, "Item=" + info.ItemId.ToString());
        //        searchItems.Add(searchInfo);
        //    }

        //    return searchItems;
        //}

        #endregion

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public string ExportModule(int moduleID)
        {
            StringBuilder sb = new StringBuilder();

            List<FlexMLSInfo> infos = GetFlexMLSs(moduleID);

            if (infos.Count > 0)
            {
                sb.Append("<FlexMLSs>");
                foreach (FlexMLSInfo info in infos)
                {
                    sb.Append("<FlexMLS>");
                    sb.Append("<content>");
                    sb.Append(XmlUtils.XMLEncode(info.Content));
                    sb.Append("</content>");
                    sb.Append("</FlexMLS>");
                }
                sb.Append("</FlexMLSs>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "FlexMLSs");

            foreach (XmlNode info in infos.SelectNodes("FlexMLS"))
            {
                FlexMLSInfo FlexMLSInfo = new FlexMLSInfo();
                FlexMLSInfo.ModuleId = ModuleID;
                FlexMLSInfo.Content = info.SelectSingleNode("content").InnerText;
                FlexMLSInfo.CreatedByUser = UserID;

                //    AddFlexMLS(FlexMLSInfo);
            }
        }

        #endregion
    }
}
