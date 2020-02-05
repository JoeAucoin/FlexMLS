using System;
using System.Collections.Generic;
using DotNetNuke.Services.Scheduling;
using GIBS.Modules.FlexMLS.Components;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using DotNetNuke.Services.Social.Notifications;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Log.EventLog;

namespace GIBS.Modules.FlexMLS.Components
{
    public class EmailScheduledTask : SchedulerClient
    {
        public EmailScheduledTask(ScheduleHistoryItem oItem)
            : base()
        {
            this.ScheduleHistoryItem = oItem;
        }

        string _village = "";
        string _town = "";
        string _beds = "";
        string _baths = "";
        string _waterfrontYN = "";
        string _waterviewYN = "";
        string _type = "";
        int _pricelow = 0;
        int _pricehigh = 50000000;
        string _listingOfficeMLSID = "";
        string _dom = "";
        string _complex = "";


        public override void DoWork()
        {
            try
            {
                //Perform required items for logging
                this.Progressing();

                //To log note
                this.ScheduleHistoryItem.AddLogNote("<br />Run notes<br />");

                List<FlexMLSInfo> items;
                FlexMLSController controller = new FlexMLSController();

                items = controller.FlexMLS_Favorites_GetEmailSearches();
                int TotalRecords = 0;
                int RecordsEmailed = 0;
                for (int i = 0; i <= items.Count - 1; i++)
                {

                    string mySearchCriteria = SearchCriteria(items[i].Favorite.ToString());

                    string myEmailContent = BuildEmailContent(items[i].Favorite.ToString(), mySearchCriteria.ToString());

                    int clientID = Int32.Parse(items[i].ClientID.ToString());
                    int agentID = Int32.Parse(items[i].AgentID.ToString());
                    int clientPortalID = Int32.Parse(items[i].ClientPortalID.ToString());    

                    int myRecordCount = 0;
                    myRecordCount=SearchNumRecords();
                    if (myRecordCount > 0)
                    {

                        string subject = myRecordCount.ToString() + " Newly Matched MLS Listings";
                        string body = myEmailContent.ToString();
                        // NEED THE PORTALID HERE INSTEAD OF A ZERO
                        UserInfo _currentUser = DotNetNuke.Entities.Users.UserController.GetUserById(clientPortalID, clientID);
                        var notificationType = NotificationsController.Instance.GetNotificationType("HtmlNotification");
                        // NEED THE PORTALID HERE AND AGENTID
                        var sender = UserController.GetUserById(clientPortalID, agentID);
                        var notification = new Notification { NotificationTypeID = notificationType.NotificationTypeId, Subject = subject, Body = body, IncludeDismissAction = true, SenderUserID = sender.UserID };
                        NotificationsController.Instance.SendNotification(notification, 0, null, new List<UserInfo> { _currentUser });
                        
                    //    FOR DEBUG LOGGING UNCOMMENT NEXT LINE
                    //    this.ScheduleHistoryItem.AddLogNote(_currentUser.DisplayName.ToString() + " - " + mySearchCriteria.ToString() + "<br />");
                        RecordsEmailed += 1;
                    }
                    else
                    {
                       
                        this.ScheduleHistoryItem.AddLogNote("No search records to send.<br />");
                    }

                    TotalRecords += 1;

                }

                

                string subject1 = "Newly Matched MLS Listings Report - " + RecordsEmailed.ToString() + " Clients";
                string body1 = RecordsEmailed.ToString() + " clients emailed of " + TotalRecords.ToString();

                UserInfo _currentUser1 = DotNetNuke.Entities.Users.UserController.GetUserById(0, 1);
                var notificationType1 = NotificationsController.Instance.GetNotificationType("HtmlNotification");

                var sender1 = UserController.GetUserById(0, 1);
                var notification1 = new Notification { NotificationTypeID = notificationType1.NotificationTypeId, Subject = subject1, Body = body1, IncludeDismissAction = true, SenderUserID = sender1.UserID };
                NotificationsController.Instance.SendNotification(notification1, 0, null, new List<UserInfo> { _currentUser1 });



                var objEventLog = new EventLogController();
                objEventLog.AddLog("FlexMLS Scheduler", "Results: " + RecordsEmailed.ToString() + " searches emailed of " + TotalRecords.ToString(), EventLogController.EventLogType.ADMIN_ALERT);

          
                this.ScheduleHistoryItem.AddLogNote("FlexEmailScheduler Results: " + RecordsEmailed.ToString() + " client searches emailed of " + TotalRecords.ToString());
                //Show success
                this.ScheduleHistoryItem.Succeeded = true;
            }
            catch (Exception ex)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                this.ScheduleHistoryItem.AddLogNote("Exception= " + ex.ToString());
                this.Errored(ref ex);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }


        public string BuildEmailContent(string SearchURL, string SearchCriteria)
        {
            try
            {


                StringBuilder EmailContentHTML = new StringBuilder();
                EmailContentHTML.Capacity = 5000;

                EmailContentHTML.Append("<style type=\"text/css\">" + Environment.NewLine);
                EmailContentHTML.Append(".Section{font-weight: bold; font-family: Verdana, Tahoma;font-size: 14px;}" + Environment.NewLine);
                EmailContentHTML.Append(".Value{font-weight: normal; font-family: Verdana, Tahoma;font-size: 12px;}" + Environment.NewLine);
                EmailContentHTML.Append(".Footer{font-weight: normal; font-family: Verdana, Tahoma;font-size: 10px;line-height:150%;}" + Environment.NewLine);
                EmailContentHTML.Append("</style>" + Environment.NewLine + Environment.NewLine);

                EmailContentHTML.Append(Environment.NewLine);
                EmailContentHTML.Append("<p class=\"Section\">New listings match your search criteria.</p>" + Environment.NewLine);
                EmailContentHTML.Append("<p class=\"Value\">" + SearchCriteria.ToString() + "</p>" + Environment.NewLine);
                //EmailContentHTML.Append("<p class=\"Section\">Follow the following link to view the listings:</p>" + Environment.NewLine);

                EmailContentHTML.Append("<p class=\"Value\"><a href=\"" + SearchURL.ToString() + "&e=" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + "\">CLICK HERE TO VIEW THE LISTINGS</a></p>" + Environment.NewLine);

                EmailContentHTML.Append("<p class=\"Footer\">Some e-mail clients do not support links, cut 'n paste the URL below into a web browser.<br />" + SearchURL.ToString() + "&e=" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + "</p>" + Environment.NewLine);

                return EmailContentHTML.ToString();
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return "ERROR: " + ex.ToString();
            }
        }

        public int SearchNumRecords()
        {
            try
            {
                
                List<FlexMLSInfo> items;
                FlexMLSController controller = new FlexMLSController();

                items = controller.FlexMLS_Search_LastModified(_type.ToString(),
                    _town.ToString(),
                    _village.ToString(),
                    _beds.ToString(),
                    _baths.ToString(),
                    _waterfrontYN.ToString(),
                    _waterviewYN.ToString(),
                    _pricelow.ToString(),
                    _pricehigh.ToString(), _listingOfficeMLSID.ToString(), 
                    _dom.ToString(), DateTime.Today.AddDays(-1).ToString("yyyyMMdd"), _complex.ToString().Replace("'","''"));

                if (items.Count > 0)
                {
                    return items.Count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return 0;
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
                        myRetValue = "Single Family";
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
                        myRetValue = PropType;
                        break;
                }
                return myRetValue.ToString();


            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return "ERROR: " + ex.ToString();
            }

        }


        public string SearchCriteria(string SearchURL)
        {
            try
            {
                 StringBuilder _SearchCriteria = new StringBuilder();
                _SearchCriteria.Capacity = 500;


                string _criteria = "";
                
                // DEBUG
                //Label1.Text += hl.Text.ToString() + "<br />";

                NameValueCollection query = HttpUtility.ParseQueryString(SearchURL.ToString());


                if (query["type"] != null && query["type"] != "")
                {
                    _SearchCriteria.Append("Type: <b>" + GetPropertyTypeDesc(query["Type"].ToString()) + "</b> &nbsp;");
                    _type = query["Type"].ToString();

                }
                else
                {
                    _type = "";
                }

                if (query["Town"] != null && query["Town"] != "")
                {
                    _SearchCriteria.Append(" Town: <b>" + query["Town"].ToString() + "</b> &nbsp;");
                    _town = query["Town"].ToString();
                }
                else
                {
                    _town = "";
                }

                if (query["Village"] != null && query["Village"] != "")
                {
                    _SearchCriteria.Append(" Village: <b>" + query["Village"].ToString() + "</b> &nbsp;");
                    _village = query["Village"].ToString();
                }
                else
                {
                    _village = "";
                }

                if (query["Beds"] != null && query["Beds"] != "")
                {
                    _SearchCriteria.Append(" Bedrooms: <b>" + query["Beds"].ToString() + "</b> &nbsp;");
                    _beds = query["Beds"].ToString();
                }
                else
                {
                    _beds = "";
                }

                if (query["Baths"] != null && query["Baths"] != "")
                {
                    _SearchCriteria.Append(" Bathrooms: <b>" + query["Baths"].ToString() + "</b> &nbsp;");
                    _baths = query["Baths"].ToString();
                }
                else
                {
                    _baths = "";
                }

                if (query["WaterFront"] != null && query["WaterFront"] != "")
                {
                    _SearchCriteria.Append(" Waterfront: <b>" + query["WaterFront"].ToString() + "</b> &nbsp;");
                    _waterfrontYN = query["WaterFront"].ToString();
                }
                else
                {
                    _waterfrontYN = "";
                }

                if (query["WaterView"] != null && query["WaterView"] != "")
                {
                    _SearchCriteria.Append(" Waterview: <b>" + query["WaterView"].ToString() + "</b> &nbsp;");
                    _waterviewYN = query["WaterView"].ToString();
                }
                else
                {
                    _waterviewYN = "";
                }

                if (query["Low"] != null && query["Low"] != "")
                {
                    _SearchCriteria.Append(" Min. Price: <b>" + query["Low"].ToString() + "</b> &nbsp;");
                    _pricelow = Int32.Parse(query["Low"].ToString());
                }
                else
                {
                    _pricelow = 0;
                }

                if (query["High"] != null && query["High"] != "")
                {
                    _SearchCriteria.Append(" Max Price: <b>" + query["High"].ToString() + "</b> &nbsp;");
                    _pricehigh = Int32.Parse(query["High"].ToString());
                }
                else
                {
                    _pricehigh = 50000000;
                }

                if (query["LOID"] != null && query["LOID"] != "")
                {
                    _SearchCriteria.Append(" Office: <b>" + query["LOID"].ToString() + "</b> &nbsp;");
                    _listingOfficeMLSID = query["LOID"].ToString();
                }
                else
                {
                    _listingOfficeMLSID = "";
                }

                if (query["DOM"] != null && query["DOM"] != "")
                {
                    _SearchCriteria.Append(" Days on Market: <b>" + query["DOM"].ToString() + "</b> &nbsp;");
                    _dom = query["DOM"].ToString();
                }
                else
                {
                    _dom = "";
                }

                if (query["Complex"] != null && query["Complex"] != "")
                {
                    _complex = query["Complex"].ToString();
                    _complex = _complex.ToString().Replace("_", " ").ToString().Replace("~", "/").ToString().Replace("^", "'").ToString();
                    _SearchCriteria.Append(" Complex: <b>" + _complex.ToString() + "</b> &nbsp;");
                }
                else
                {
                    _complex = "";
                }

                _criteria = _SearchCriteria.ToString().Remove(_SearchCriteria.Length - 7);

                return _criteria.ToString();
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return "ERROR: " + ex.ToString();
            }
        }

        public int SearchRecords()
        {
            try
            {

                return 1;
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
                return 0;
            }
        }	

        public void EmailNotificationHTML(string content, string subject, string emailto)
        {

            try
            {        
                //GET THE FROM ADDRESS
                    string EmailFrom = DotNetNuke.Entities.Controllers.HostController.Instance.GetString("SMTPUsername").ToString(); 

                    this.ScheduleHistoryItem.AddLogNote("<br />EmailFrom: " + EmailFrom.ToString() + "<br />");
                    this.ScheduleHistoryItem.AddLogNote("<br />emailto: " + emailto.ToString() + "<br />");

                    // NEW
                    string SMTPUserName = DotNetNuke.Entities.Controllers.HostController.Instance.GetString("SMTPUsername");
                
                    string[] emailAttachemnts1 = null;
                    DotNetNuke.Services.Mail.Mail.SendMail(SMTPUserName.ToString(), 
                        EmailFrom.ToString().Trim(), "", "", 
                        EmailFrom.ToString(), 
                        DotNetNuke.Services.Mail.MailPriority.Normal, 
                        "My Subject", 
                        DotNetNuke.Services.Mail.MailFormat.Html, 
                        System.Text.ASCIIEncoding.ASCII, content.ToString(), emailAttachemnts1, string.Empty, string.Empty, string.Empty, string.Empty, true);



         //       }
            }
            catch (Exception ex)
            {
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex); 
            }

        }

    }
}