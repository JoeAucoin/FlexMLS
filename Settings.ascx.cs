using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;

using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Entities.Tabs;
using System.Collections;
using System.Web.UI.WebControls;

namespace GIBS.Modules.FlexMLS
{
    public partial class Settings : FlexMLSSettings
    {

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                if (!IsPostBack)
                {
                    GetRoles();
                 //   lblDebug.Text = TabModuleId.ToString() + " AgentRole: " + AgentRole.ToString();
                       
                    ddlAgentRole.SelectedValue = AgentRole.ToString(); 
                    
                    if (Settings.Contains("AgentRole"))
                    {
                             // Settings["AgentRole"].ToString();
                    }

                    if (Settings.Contains("Recaptcha"))
                    {
                        txtRecaptcha.Text = Settings["Recaptcha"].ToString();
                    }

                    if (Settings.Contains("MlsDataBase"))
                    {
                        txtDataBase.Text = Settings["MlsDataBase"].ToString();
                    }

                    if (Settings.Contains("MlsLogin"))
                    {

                        txtLogin.Text = Settings["MlsLogin"].ToString();
                    }

                    if (Settings.Contains("MlsPassword"))
                    {
                        txtPassword.Text = Settings["MlsPassword"].ToString();
                    }

                    if (Settings.Contains("MlsServer"))
                    {
                        txtServer.Text = Settings["MlsServer"].ToString();
                    }

                    if (Settings.Contains("MaxThumbSize"))
                    {
                        txtThumbSize.Text = Settings["MaxThumbSize"].ToString();
                    }

                    if (Settings.Contains("ThumbImageAlign"))
                    {
                        rblThumbPlacement.SelectedValue = Settings["ThumbImageAlign"].ToString();
                    }

                    if (Settings.Contains("MaxImageSize"))
                    {
                        txtMaxImageSize.Text = Settings["MaxImageSize"].ToString();
                    }

                    if (Settings.Contains("ZillowWebServiceId"))
                    {
                        txtZillowWebServiceId.Text = Settings["ZillowWebServiceId"].ToString();
                    }
                    if (Settings.Contains("ZillowUserId"))
                    {
                        txtZillowUserId.Text = Settings["ZillowUserId"].ToString();
                    }

                    if (Settings.Contains("ZillowAutoRunData"))
                    {
                        cbxAutoRunZillow.Checked = Convert.ToBoolean(Settings["ZillowAutoRunData"].ToString());
                    }

                    if (Settings.Contains("EmailBCC"))
                    {
                        txtEmailBCC.Text = Settings["EmailBCC"].ToString();
                    }

                    if (Settings.Contains("EmailFrom"))
                    {
                        txtEmailFrom.Text = Settings["EmailFrom"].ToString();
                    }

                    if (Settings.Contains("EmailSubject"))
                    {
                        txtEmailSubject.Text = Settings["EmailSubject"].ToString();
                    }

                    if (Settings.Contains("EmailToDefault"))
                    {
                        txtEmailToDefault.Text = Settings["EmailToDefault"].ToString();
                    }

                    if (Settings.Contains("GoogleMapAPIKey"))
                    {
                        txtGoogleMapAPIKey.Text = Settings["GoogleMapAPIKey"].ToString();
                    }

                    if (Settings.Contains("MLSImagesUrl"))
                    {
                        txtMLSImagesUrl.Text = Settings["MLSImagesUrl"].ToString();
                    }



                    if (Settings.Contains("RetsVersion"))
                    {
                        txtRetsVersion.Text = Settings["RetsVersion"].ToString();
                    }

                    if (Settings.Contains("RetsUserAgent"))
                    {
                        txtRetsUserAgent.Text = Settings["RetsUserAgent"].ToString();
                    }

                    if (Settings.Contains("RetsUserAgentAuthType"))
                    {
                        txtRetsUserAgentAuthType.Text = Settings["RetsUserAgentAuthType"].ToString();
                    }

                    if (Settings.Contains("RetsUserAgentPassword"))
                    {
                        txtRetsUserAgentPassword.Text = Settings["RetsUserAgentPassword"].ToString();
                    }

                    if (Settings.Contains("RetsUserName"))
                    {
                        txtRetsUserName.Text = Settings["RetsUserName"].ToString();
                    }

                    if (Settings.Contains("RetsPassword"))
                    {
                        txtRetsPassword.Text = Settings["RetsPassword"].ToString();
                    }

                    if (Settings.Contains("RetsImageDirectory"))
                    {
                        txtRetsImageDirectory.Text = Settings["RetsImageDirectory"].ToString();
                    }

                    if (Settings.Contains("RetsImageDownLoadTestMode"))
                    {
                        cbxImageDownLoadTestMode.Checked = Convert.ToBoolean(Settings["RetsImageDownLoadTestMode"].ToString());
                    }		               

                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
        public void GetMyTabs()
        {

            try
            {


                //ddlContactFormPage.DataSource = TabController.GetPortalTabs(this.PortalId, this.TabId, true, false);
                //ddlContactFormPage.DataBind();

             //   ddlViewListing.DataSource = TabController.GetPortalTabs(this.PortalId, this.TabId, true, false);
             //   ddlViewListing.DataBind();

                //        ddlList.DataSource = TabController.GetPortalTabs(this.PortalId, this.TabId, true, false);
                //        ddlList.DataBind();






            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetRoles()
        {
          //  ArrayList myRoles = new ArrayList();

            DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();

            var myRoles = rc.GetRoles(this.PortalId);
            ddlAgentRole.DataTextField = "RoleName";
            ddlAgentRole.DataValueField = "RoleName";
            ddlAgentRole.DataSource = myRoles;
            ddlAgentRole.DataBind();

            // ADD FIRST (NULL) ITEM
            ListItem item = new ListItem();
            item.Text = "-- Select Role for Agents --";
            item.Value = "";
            ddlAgentRole.Items.Insert(0, item);
            // REMOVE DEFAULT ROLES
            ddlAgentRole.Items.Remove("Administrators");
            ddlAgentRole.Items.Remove("Registered Users");
            ddlAgentRole.Items.Remove("Subscribers");

        }

        public override void UpdateSettings()
        {
            try
            {

                AgentRole = ddlAgentRole.SelectedValue.ToString();

                var modules = new ModuleController();

                modules.UpdateTabModuleSetting(TabModuleId, "MlsDataBase", txtDataBase.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "MlsLogin", txtLogin.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "MlsPassword", txtPassword.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "MlsServer", txtServer.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "MaxThumbSize", txtThumbSize.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ThumbImageAlign", rblThumbPlacement.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "MaxImageSize", txtMaxImageSize.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "ZillowWebServiceId", txtZillowWebServiceId.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "ZillowUserId", txtZillowUserId.Text.ToString());
           //     modules.UpdateTabModuleSetting(TabModuleId, "AgentRole", ddlAgentRole.SelectedValue.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "EmailBCC", txtEmailBCC.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "EmailFrom", txtEmailFrom.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "EmailSubject", txtEmailSubject.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "EmailToDefault", txtEmailToDefault.Text);
                modules.UpdateTabModuleSetting(TabModuleId, "ZillowAutoRunData", cbxAutoRunZillow.Checked.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "GoogleMapAPIKey", txtGoogleMapAPIKey.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "MLSImagesUrl", txtMLSImagesUrl.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "Recaptcha", txtRecaptcha.Text.ToString());
               
                modules.UpdateTabModuleSetting(TabModuleId, "RetsVersion", txtRetsVersion.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "RetsUserAgent", txtRetsUserAgent.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "RetsUserAgentPassword", txtRetsUserAgentPassword.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "RetsUserName", txtRetsUserName.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "RetsPassword", txtRetsPassword.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "RetsImageDirectory", txtRetsImageDirectory.Text.ToString());
                modules.UpdateTabModuleSetting(TabModuleId, "RetsImageDownLoadTestMode", cbxImageDownLoadTestMode.Checked.ToString());
             
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}