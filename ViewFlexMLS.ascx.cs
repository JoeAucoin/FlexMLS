using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common.Controls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.ClientCapability;
using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Common;
using System.Text;

namespace GIBS.Modules.FlexMLS
{
    public partial class ViewFlexMLS : PortalModuleBase, IActionable
    {


        public string mControlToLoad = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                ////    // Determine if AJAX is installed
                ////    if (DotNetNuke.Framework.AJAX.IsInstalled())
                ////    {
                //////        DotNetNuke.Framework.AJAX.RegisterScriptManager();
                ////        // Create a reference to the Script Manager
                ////        ScriptManager objScriptManager = ScriptManager.GetCurrent(this.Page);
                ////        // Add a reference to the web service
                ////        ServiceReference objServiceReference = new ServiceReference();

                ////        ////   DesktopModules/GIBS/FlexMLS/CascadingDropdown1.asmx
                ////  //      objServiceReference.Path = @"~/DesktopModules/GIBS/FlexMLS/CascadingDropdown1.asmx";
                ////  //      objScriptManager.Services.Add(objServiceReference);

                ////        ScriptReference objScriptReference = new ScriptReference();
                ////        objScriptReference.Path = @"~/DesktopModules/GIBS/FlexMLS/js/CallWebServiceMethods.js";
                ////        objScriptManager.Scripts.Add(objScriptReference);

                ////    }

                if (!IsPostBack)
                {






                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void ReadQueryString()
        {

            try
            {


                if (Request.QueryString["pg"] != null)
                {

                    switch (Request.QueryString["pg"].ToString().ToLower())
                    {

                        case "list":
                            mControlToLoad = "List.ascx";
                            break;

                        case "v":
                            mControlToLoad = "ViewListing.ascx";
                            break;

                        case "contact":

                            mControlToLoad = "Contact.ascx";
                            break;

                        //TellAFriend
                        case "tellafriend":

                            mControlToLoad = "TellAFriend.ascx";
                            break;

                        //case "default":

                        //    mControlToLoad = "yourfrontpage.ascx";
                        //    break;

                    }

                }
                else
                {

                    mControlToLoad = "Search.ascx";
                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        public void LoadControlType()
        {

            try
            {

                PortalModuleBase pmb = (PortalModuleBase)this.LoadControl(mControlToLoad);

                if (pmb != null)
                {

                    pmb.ModuleConfiguration = this.ModuleConfiguration;
                    pmb.ID = System.IO.Path.GetFileNameWithoutExtension(mControlToLoad);
                    PlaceHolder1.Controls.Add(pmb);

                }



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }




        protected void Page_Init(object sender, EventArgs e)
        {

            ReadQueryString();

            LoadControlType();


        }





        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                ////actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                ////    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                ////     true, false);
                //actions.Add(GetNextActionID(), "GetMissingMLSImages",
                //    ModuleActionType.AddContent, "", "", EditUrl("GetMissingMLSImages"), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                //     true, false);
                return actions;
            }
        }

        #endregion




        public void LoadSettings()
        {

            try
            {


                //FlexMLSSettings settingsData = new FlexMLSSettings(this.TabModuleId);

                //if (settingsData.ListPage != null)
                //{
                //    //_SearchResultPage = settingsData.ListPage.ToString();
                //    ////lblFromInstructions.Text = settingsData.FromInstructions;
                //}


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }



        /// <summary>
        /// Handles the items being bound to the datalist control. In this method we merge the data with the
        /// template defined for this control to produce the result to display to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


    }
}