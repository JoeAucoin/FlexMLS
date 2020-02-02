using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Lists;
using DotNetNuke.Services.Exceptions;
using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Entities.Users;
using System.Text;
using System.Collections;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Common;
using System.Net.Mail;

namespace GIBS.Modules.FlexMLS
{
    public partial class Contact : PortalModuleBase
    {

        public int _MLS = 0;
        static string _ReturnLink = "";
        static string _AgentRole = "";
        static string _EmailToDefault = "";
        static string _EmailSubject = "";
  //      static string _EmailFrom = "";
        static string _EmailBCC = "";
        public string _Recaptcha = "";

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.3.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Watermark", (this.TemplateSourceDirectory + "/JavaScript/jquery.watermarkinput.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "TimePicker", (this.TemplateSourceDirectory + "/JavaScript/jquery.timepicker.min.js"));

            //<script src="https://www.google.com/recaptcha/api.js" async defer></script
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //Recaptcha
            if (Settings.Contains("Recaptcha"))
            {
                _Recaptcha = Settings["Recaptcha"].ToString();
            }

            ModuleConfiguration.ModuleTitle = "Request More Information";

            if (!IsPostBack)
            {

                LoadSettings();
                GetPreviousAgents(_AgentRole.ToString());

                if (Request.QueryString["MLS"] != null)
                {
                    _MLS = Convert.ToInt32(Request.QueryString["MLS"].ToString());
                    //   lblMlsInterest.Text = _MLS.ToString();
                    GetListing(_MLS);
                    txtMlsNumber.Value = _MLS.ToString();

                }

                // SCHEDULE A SHOWING
                if (Request.QueryString["Schedule"] != null)
                {

                    PanelShowDateTime.Visible = true;
                    ModuleConfiguration.ModuleTitle = "Schedule a Showing";
                    
                    //ShowDate.MinDate = DateTime.Today.AddDays(1);

                }

                GetStates();

                if (this.UserId > -1)
                {
                    GetUserInfo();
                }

                if (Request.UrlReferrer == null)
                {
                    string vLink = Globals.NavigateURL("View", "pg", "v", "MLS", _MLS.ToString());
                    vLink = vLink.ToString().Replace("ctl/View/", "");
               //     vLink = vLink.ToString().Replace("Default.aspx", _pageName.ToString());

                    _ReturnLink = vLink.ToString();
                }
                else
                {
                    _ReturnLink = Request.UrlReferrer.ToString();

                }



            }
            else
            { 
            //DO NOTHING
            }
        }

        public void GetUserInfo()
        {

            try
            {
                txtEmail.Text = this.UserInfo.Email.ToString();
                txtFirstName.Text = this.UserInfo.FirstName.ToString();
                txtLastName.Text = this.UserInfo.LastName.ToString();
                txtAddress.Text = this.UserInfo.Profile.GetPropertyValue("Street");
                txtCity.Text = this.UserInfo.Profile.GetPropertyValue("City");
                txtPhone.Text = this.UserInfo.Profile.GetPropertyValue("Telephone");
                txtCell.Text = this.UserInfo.Profile.GetPropertyValue("Cell");
                txtZip.Text = this.UserInfo.Profile.GetPropertyValue("PostalCode");
                ddlState.SelectedValue = this.UserInfo.Profile.GetPropertyValue("Region");


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetStates()
        {

            try
            {

                var ctlList = new ListController();
                var leic = ctlList.GetListEntryInfoItems("Region", "Country.US");

                ddlState.DataTextField = "Text";
                ddlState.DataValueField = "Value";
                ddlState.DataSource = leic;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                ddlState.SelectedValue = "MA";

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void GetListing(int mlsNumber)
        {
            try
            {

                FlexMLSController controller = new FlexMLSController();
                FlexMLSInfo item = controller.FlexMLS_Get_Listing(mlsNumber);

                if (item != null)
                {





                    string _S_baths = "";
                    string _S_halfbaths = "";
                    string _S_beds = "";

                    string _BedsAndBaths = "";
                    if (item.Bedrooms > 1)
                    {
                        _S_beds = "s";
                    }
                    if (item.TotalBaths > 1)
                    {
                        _S_baths = "s";

                    }
                    if (item.HalfBaths > 1)
                    {
                        _S_halfbaths = "s";
                    }

                    if (item.Bedrooms > 0)
                    {
                        _BedsAndBaths = item.Bedrooms.ToString() + " Bedroom" + _S_beds.ToString();
                    }
                    if (item.FullBaths > 0)
                    {
                        _BedsAndBaths = _BedsAndBaths.ToString() + " - " + item.FullBaths.ToString() + " Full Bath" + _S_baths.ToString();
                    }
                    if (item.HalfBaths > 0)
                    {
                        _BedsAndBaths = _BedsAndBaths.ToString() + " & " + item.HalfBaths.ToString() + " Half Bath" + _S_halfbaths.ToString();
                    }


                    
                    lblMlsInterest.Text = "MLS #: " + item.ListingNumber.ToString() + " | " + String.Format("{0:C0}", item.ListingPrice) + "<br />"
                        + item.Address.ToString().Trim() + ", " + item.Village.ToString() + "<br />"
                        + item.PropertyType.ToString() + " | " + _BedsAndBaths.ToString();



                    



                    //if (item.PictureCount > 0)
                    //{
                    //    imgListingImage.ImageUrl = "http://mls.gibs.com/images/" + item.MLNumber.ToString() + ".jpg";
                    //    imgListingImage.Width = 400;

                    //}
                    //else
                    //{
                    //    imgListingImage.Visible = false;
                    //}

                    //if (HttpContext.Current.User.Identity.IsAuthenticated)
                    //{
                    //    lblSummary.Text = lblSummary.Text.ToString() + " | Days on Market: " + item.DOM.ToString();
                    //}

                    //lblSummary.Text




                }
                else
                {
                    //Response.Redirect(Globals.NavigateURL(), true);
                    lblMlsInterest.Text = "Error! Listing cannot be found.";
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string EmailContent = "<p>" + lblMlsInterest.Text.ToString() + "</p>" + GetFormValues();
            string _emailTo = ddlPreviousAgent.SelectedValue.ToString();

            //DotNetNuke.Services.Mail.Mail.SendMail(txtEmail.Text.ToString(),
            //    _emailTo.ToString(),
            //    _EmailBCC.ToString(),
            //    _EmailSubject.ToString() + "MLS Inquiry - " + txtMlsNumber.Value.ToString(), EmailContent.ToString(), "", "HTML", "", "", "", "");

            string SMTPUserName = DotNetNuke.Entities.Controllers.HostController.Instance.GetString("SMTPUsername");
          //  string[] emptyStringArray = new string[0];
            List<Attachment> attchmnts = new List<Attachment>();

            DotNetNuke.Services.Mail.Mail.SendMail(SMTPUserName.ToString(),SMTPUserName.ToString(), _emailTo.ToString(), "", "",
                               txtEmail.Text.ToString(), DotNetNuke.Services.Mail.MailPriority.Normal, "New Contact - " +  _EmailSubject.ToString(), 
                                DotNetNuke.Services.Mail.MailFormat.Html, System.Text.Encoding.ASCII,
                                EmailContent.ToString(), attchmnts, "", "", "", "", true);
            //MailFrom,  MailSender,    MailTo,       Cc, Bcc,     
            //ReplyTo,       Priority,     Subject,           
            //BodyFormat, BodyEncoding,          
            //Body, Attachments, SMTPServer, SMTPAuthentication, SMTPUsername, SMTPPassword, SMTPEnableSSL

            
            if (this.UserId > -1)
            {
                UpdateRecord(this.UserId);
            
            }
            
            
         //   Page.ClientScript.RegisterStartupScript(this.GetType(),"Alert", "<script language='javascript'>alert('Success');</script>");

            Response.Redirect(_ReturnLink.ToString());

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(_ReturnLink.ToString());
        }	

        public string GetFormValues()
        {
            try
            {
                StringBuilder MyFormResults = new StringBuilder();
                // SCHEDULE A SHOWING
                if (Request.QueryString["Schedule"] != null)
                {
                    
                    MyFormResults.Append("<p>Schedule a Showing: " + txtShowDate.Text.ToString() + " " + txtShowTime.Text.ToString() + "</p>");

                }
                MyFormResults.Append("<p>" + txtFirstName.Text.ToString() + " " + txtLastName.Text.ToString() + "<br />");
                MyFormResults.Append(txtAddress.Text.ToString() + "<br />");
                MyFormResults.Append(txtCity.Text.ToString() + ", " + ddlState.SelectedValue.ToString() + " " + txtZip.Text.ToString() + "</p>");
                MyFormResults.AppendLine();
                MyFormResults.Append("<p>" + txtEmail.Text.ToString() + "</p>");
                MyFormResults.Append("<p>Daytime Phone: " + txtPhone.Text.ToString() + "<br />");
                MyFormResults.Append("Cell Phone: " + txtCell.Text.ToString() + "</p>");
                MyFormResults.Append("<p>Previous Agent: " + ddlPreviousAgent.SelectedItem.ToString() + "</p>");
                MyFormResults.Append("<p>Questions:<br />" + txtQuestions.Text.ToString() + "</p>");



                return MyFormResults.ToString();
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
                return ex.ToString();
            }
        }

        public void UpdateRecord(int RecordID)
        {
            try
            {
   
                UserController objUserController = new UserController();
                UserInfo uUser = objUserController.GetUser(this.PortalId, RecordID);

                uUser.FirstName = txtFirstName.Text.ToString();
                uUser.LastName = txtLastName.Text.ToString();
                uUser.Email = txtEmail.Text.ToString();

                uUser.Profile.Street = txtAddress.Text.ToString();
                uUser.Profile.City = txtCity.Text.ToString();

                uUser.Profile.Telephone = txtPhone.Text.ToString();
                uUser.Profile.Cell = txtCell.Text.ToString();
                
                uUser.Profile.PostalCode = txtZip.Text.ToString();
                uUser.Profile.Region = ddlState.SelectedValue.ToString();


                UserController.UpdateUser(PortalSettings.PortalId, uUser);

             //   lblStatus.Text = "Record Successully Updated";

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetPreviousAgents(string _AgentRole)
        {
            try
            {

                DotNetNuke.Security.Roles.RoleController rc = new DotNetNuke.Security.Roles.RoleController();

                var objUserList = rc.GetUsersByRole(this.PortalId, _AgentRole.ToString());

              //  DotNetNuke.Security.Roles.RoleController objRoleController = new DotNetNuke.Security.Roles.RoleController();
              //  ArrayList objUserList = objRoleController.GetUsersByRoleName(DotNetNuke.Entities.Portals.PortalSettings.Current.PortalId, _AgentRole.ToString());

                ddlPreviousAgent.Items.Insert(0, new ListItem("-- Please Select --", _EmailToDefault.ToString()));
                int i = 1;

                foreach (DotNetNuke.Entities.Users.UserInfo objUserInfo in objUserList)
                {
                    ddlPreviousAgent.Items.Insert(i, new ListItem(objUserInfo.DisplayName.ToString(),objUserInfo.Email.ToString()));
                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void LoadSettings()
        {

            try
            {
                if (Settings.Contains("AgentRole"))
                {
                    _AgentRole = Settings["AgentRole"].ToString();
                }
                if (Settings.Contains("EmailToDefault"))
                {
                    _EmailToDefault = Settings["EmailToDefault"].ToString();
                }
                else
                {
                    _EmailToDefault = "josephmaucoin@gmail.com";
                }
                if (Settings.Contains("EmailSubject"))
                {
                    _EmailSubject = Settings["EmailSubject"].ToString();
                }
                if (Settings.Contains("EmailBCC"))
                {
                    _EmailBCC = Settings["EmailBCC"].ToString();
                }



                //          lblPopupContent.Text = Globals.NavigateURL(PortalSettings.LoginTabId) + " - " + Globals.NavigateURL(PortalSettings.RegisterTabId);


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


    }
}