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
using System.Web.Services;

using System.Collections.Specialized;
using System.Data;
using System.ComponentModel;

namespace GIBS.Modules.FlexMLS
{
    public partial class Search : PortalModuleBase
    {
        public string _SearchResultPage = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                ModuleConfiguration.ModuleTitle = "Search the Cape Cod MLS";

                if (DotNetNuke.Framework.AJAX.IsInstalled())
                {
                    DotNetNuke.Framework.AJAX.RegisterScriptManager();

                    ScriptManager objScriptManager = ScriptManager.GetCurrent(this.Page);
                    // Add a reference to the web service
                    ServiceReference objServiceReference = new ServiceReference();
                    ScriptReference objScriptReference = new ScriptReference();
                    objScriptReference.Path = @"https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js";
                    objScriptManager.Scripts.Add(objScriptReference);

                    objScriptReference.Path = @"https://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.min.js";
                    objScriptManager.Scripts.Add(objScriptReference);



                }






                if (!IsPostBack)
                {
                    GetTowns();

                }

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void GetTowns()
        {

            try
            {

                List<FlexMLSInfo> items;

                FlexMLSController controller = new FlexMLSController();
                items = controller.FlexMLS_Lookup_Town();

               
                ddl_Town.DataSource = items;
                ddl_Town.DataTextField = "Town";
                ddl_Town.DataValueField = "Town";
                ddl_Town.DataBind();

                ddl_Town.Items.Insert(0, new ListItem("--Select--", ""));

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            StringBuilder queryString = new StringBuilder(64);
            queryString.Append("pg").Append("/").Append("List");

            if (ddlPropertyType.SelectedValue.ToString().Length > 0)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("Type").Append("/").Append(ddlPropertyType.SelectedValue.ToString());
            }

            if (ddl_Town.SelectedValue.ToString().Length > 0)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("Town").Append("/").Append(ddl_Town.SelectedValue.ToString());
            }



            if (ddl_Village.SelectedValue.ToString().Length > 0)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("Village").Append("/").Append(ddl_Village.SelectedValue.ToString());
            }
            if (ddlBedRooms.SelectedValue.ToString() != "0")
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("Beds").Append("/").Append(ddlBedRooms.SelectedValue.ToString());
            }
            if (ddlBathRooms.SelectedValue.ToString() != "0")
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("Baths").Append("/").Append(ddlBathRooms.SelectedValue.ToString());
            }
            if (cbxWaterFront.Checked)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("WaterFront").Append("/").Append("True");
            }
            if (cbxWaterView.Checked)
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("WaterView").Append("/").Append("True");
            }

            if (ddlPriceLow.SelectedValue.ToString() != "0")
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("Low").Append("/").Append(ddlPriceLow.SelectedValue.ToString());
            }

            if (ddlPriceHigh.SelectedValue.ToString() != "0")
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("High").Append("/").Append(ddlPriceHigh.SelectedValue.ToString());
            }

            if (ddlDOM.SelectedValue.ToString() != "0")
            {
                if (queryString.Length > 0)
                {
                    queryString.Append("/");
                }
                queryString.Append("DOM").Append("/").Append(ddlDOM.SelectedValue.ToString());
            }


            string vLink = "";


            //if (_SearchResultPage.ToString().Length > 0)
            //{
            //    vLink = "/TabID/" + _SearchResultPage.ToString() + "/" + queryString.ToString() + "/Default.aspx";
            //}

            //else
            //{
            //    vLink = Globals.NavigateURL(this.TabId, "List", "mid", this.ModuleId.ToString(), queryString.ToString());
            //}

            //Response.Redirect(NavigateURL(TabId, "", CType(objParams.ToArray(GetType(String)), String())), True)

            //     Response.Redirect(NavigateURL(TabId, "", Convert.ToString(objParams.ToArray(typeof(string)))), true);


            vLink = Globals.NavigateURL(TabId, "View", queryString.ToString());
            vLink = vLink.ToString().Replace("ctl/View/", "");

            //// FOR DEBUGGING
            lblFormMessage.Visible = true;
            lblFormMessage.Text = queryString.ToString() + " <br />" + vLink.ToString() + " <br />tabID=" + TabId.ToString();

            Response.Redirect(vLink.ToString());

        }

        protected void ddl_Town_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<FlexMLSInfo> items;
            FlexMLSController controller = new FlexMLSController();

            items = controller.FlexMLS_Lookup_Village(ddl_Town.SelectedValue.ToString());

            ddl_Village.DataSource = items;
            ddl_Village.DataTextField = "Village";
            ddl_Village.DataValueField = "Village";
            ddl_Village.DataBind();

            ddl_Village.Items.Insert(0, new ListItem("--Optionally Select--", ""));


        }

    }
}