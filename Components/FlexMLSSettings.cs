using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;
using DotNetNuke.Common;

namespace GIBS.Modules.FlexMLS.Components
{
    /// <summary>
    /// Provides strong typed access to settings used by module
    /// </summary>
    public class FlexMLSSettings : ModuleSettingsBase
    {


        #region public properties




        public string Recaptcha
        {
            get
            {
                if (Settings.Contains("Recaptcha"))
                    return Settings["Recaptcha"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();

                mc.UpdateModuleSetting(ModuleId, "Recaptcha", value.ToString());
            }
        }


        //RetsVersion
        public string RetsVersion
        {
            get
            {
                if (Settings.Contains("RetsVersion"))
                    return Settings["RetsVersion"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsVersion", value.ToString());
            }
        }


        public string RetsUserAgent
        {
            get
            {
                if (Settings.Contains("RetsUserAgent"))
                    return Settings["RetsUserAgent"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsUserAgent", value.ToString());
            }
        }

        public string RetsUserAgentAuthType
        {
            get
            {
                if (Settings.Contains("RetsUserAgentAuthType"))
                    return Settings["RetsUserAgentAuthType"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsUserAgentAuthType", value.ToString());
            }
        }

        public string RetsUserAgentPassword
        {
            get
            {
                if (Settings.Contains("RetsUserAgentPassword"))
                    return Settings["RetsUserAgentPassword"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsUserAgentPassword", value.ToString());
            }
        }

        public string RetsUserName
        {
            get
            {
                if (Settings.Contains("RetsUserName"))
                    return Settings["RetsUserName"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsUserName", value.ToString());
            }
        }

        public string RetsPassword
        {
            get
            {
                if (Settings.Contains("RetsPassword"))
                    return Settings["RetsPassword"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsPassword", value.ToString());
            }
        }

        public string RetsImageDirectory
        {
            get
            {
                if (Settings.Contains("RetsImageDirectory"))
                    return Settings["RetsImageDirectory"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsImageDirectory", value.ToString());
            }
        }

        public string RetsImageDownLoadTestMode
        {
            get
            {
                if (Settings.Contains("RetsImageDownLoadTestMode"))
                    return Settings["RetsImageDownLoadTestMode"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "RetsImageDownLoadTestMode", value.ToString());
            }
        }

        public string MlsServer
        {
            get
            {
                if (Settings.Contains("MlsServer"))
                    return Settings["MlsServer"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MlsServer", value.ToString());
            }
        }

        public string MlsDataBase
        {
            get
            {
                if (Settings.Contains("MlsDataBase"))
                    return Settings["MlsDataBase"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MlsDataBase", value.ToString());
            }
        }


        public string MlsLogin
        {
            get
            {
                if (Settings.Contains("MlsLogin"))
                    return Settings["MlsLogin"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MlsLogin", value.ToString());
            }
        }

        public string MlsPassword
        {
            get
            {
                if (Settings.Contains("MlsPassword"))
                    return Settings["MlsPassword"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MlsPassword", value.ToString());
            }
        }

        public string ThumbImageAlign
        {
            get
            {
                if (Settings.Contains("ThumbImageAlign"))
                    return Settings["ThumbImageAlign"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "ThumbImageAlign", value.ToString());
            }
        }


        public string MaxThumbSize
        {
            get
            {
                if (Settings.Contains("MaxThumbSize"))
                    return Settings["MaxThumbSize"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MaxThumbSize", value.ToString());
            }
        }

        public string MaxImageSize
        {
            get
            {
                if (Settings.Contains("MaxImageSize"))
                    return Settings["MaxImageSize"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MaxImageSize", value.ToString());
            }
        }

        
        public string ZillowWebServiceId
        {
            get
            {
                if (Settings.Contains("ZillowWebServiceId"))
                    return Settings["ZillowWebServiceId"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "ZillowWebServiceId", value.ToString());
            }
        }

        
        public string ZillowUserId
        {
            get
            {
                if (Settings.Contains("ZillowUserId"))
                    return Settings["ZillowUserId"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "ZillowUserId", value.ToString());
            }
        }

        public string ZillowAutoRunData
        {
            get
            {
                if (Settings.Contains("ZillowAutoRunData"))
                    return Settings["ZillowAutoRunData"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "ZillowAutoRunData", value.ToString());
            }
        }

        public string AgentRole
        {
            get
            {
                if (Settings.Contains("AgentRole"))
                    return Settings["AgentRole"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "AgentRole", value.ToString());
            }
        }

        public string EmailToDefault
        {
            get
            {
                if (Settings.Contains("EmailToDefault"))
                    return Settings["EmailToDefault"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "EmailToDefault", value.ToString());
            }
        }

        public string EmailFrom
        {
            get
            {
                if (Settings.Contains("EmailFrom"))
                    return Settings["EmailFrom"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "EmailFrom", value.ToString());
            }
        }


        public string EmailBCC
        {
            get
            {
                if (Settings.Contains("EmailBCC"))
                    return Settings["EmailBCC"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "EmailBCC", value.ToString());
            }
        }


        public string EmailSubject
        {
            get
            {
                if (Settings.Contains("EmailSubject"))
                    return Settings["EmailSubject"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "EmailSubject", value.ToString());
            }
        }


        public string GoogleMapAPIKey
        {
            get
            {
                if (Settings.Contains("GoogleMapAPIKey"))
                    return Settings["GoogleMapAPIKey"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "GoogleMapAPIKey", value.ToString());
            }
        }

        public string MLSImagesUrl
        {
            get
            {
                if (Settings.Contains("MLSImagesUrl"))
                    return Settings["MLSImagesUrl"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateModuleSetting(ModuleId, "MLSImagesUrl", value.ToString());
                
            }
        }	
        
        



        #endregion
    }
}
