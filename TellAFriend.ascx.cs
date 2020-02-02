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
using DotNetNuke.Common;
using DotNetNuke.Services.Localization;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Net.Mail;


namespace GIBS.Modules.FlexMLS
{
    public partial class TellAFriend : PortalModuleBase
    {

        static int _MLS; 
        static string _ReturnLink = "";
        public string _pageName = "";

        protected void Page_Load(object sender, EventArgs e)
        {


            JavaScript.RequestRegistration(CommonJs.jQuery);
            JavaScript.RequestRegistration(CommonJs.jQueryUI);

            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "InputMasks", (this.TemplateSourceDirectory + "/JavaScript/jquery.maskedinput-1.3.js"));
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "Watermark", (this.TemplateSourceDirectory + "/JavaScript/jquery.watermarkinput.js"));

            ModuleConfiguration.ModuleTitle = "Tell A Friend";

            if (!IsPostBack)
            {

                if (Request.QueryString["MLS"] != null)
                {
                    _MLS = Convert.ToInt32(Request.QueryString["MLS"].ToString());
                 //   lblMlsInterest.Text = _MLS.ToString();
                    GetListing(_MLS);
                    txtMlsNumber.Value = _MLS.ToString();

                }


                
                //string vLink = ""; // Globals.NavigateURL(this.TabId, "ViewListing", "mid", this.ModuleId.ToString(), "MLS", DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString());
                //vLink = "/TabID/" + _ViewListingPage.ToString() + "/MLS/" + _ListingNumber.ToString() + "/Default.aspx";



                if (this.UserId > -1)
                {
                    GetUserInfo();
                }

                //if (Request.UrlReferrer == null)
                //{
                //    _ReturnLink = this.PortalSettings.PortalAlias.ToString();
                //}
                //else
                //{
                //    _ReturnLink = Request.UrlReferrer.ToString();

                //}
                
                
                
            }
        }

        public void GetUserInfo()
        {

            try
            {
                txtEmail.Text = this.UserInfo.Email.ToString();
                txtFirstName.Text = this.UserInfo.FirstName.ToString();
                txtLastName.Text = this.UserInfo.LastName.ToString();



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
                    if (item.FullBaths > 1)
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

                    txtSubject.Text = "Information about a property: MLS# " + item.ListingNumber.ToString();
                    
                    lblMlsInterest.Text = "MLS #: " + item.ListingNumber.ToString() + " | " + String.Format("{0:C0}", item.ListingPrice) + "<br />"
                        + item.Address.ToString().Trim() + ", " + item.Village.ToString() + "<br />"
                        + item.PropertyType.ToString() + " | " + _BedsAndBaths.ToString();



                    _pageName = item.Address.ToString().Trim().Replace(" ", "_").ToString().Replace("&", "").ToString() + "_" + item.Village.ToString().Replace(" ", "_").ToString().Replace("&", "").ToString() + ".aspx";

                    string vLink = Globals.NavigateURL("View", "pg", "v", "MLS", _MLS.ToString());
                    vLink = vLink.ToString().Replace("ctl/View/", "");
                    vLink = vLink.ToString().Replace("Default.aspx", _pageName.ToString());
                    _ReturnLink = vLink.ToString();


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
            string EmailContent = Localization.GetString("emailMessage", this.LocalResourceFile) + "<p>" + lblMlsInterest.Text.ToString() + "</p><p><a href='" + _ReturnLink.ToString() +"'>" + _ReturnLink.ToString() + "</a></p>" + GetFormValues();


            DotNetNuke.Services.Mail.Mail.SendMail(txtEmail.Text.ToString(), txtFriendEmail.Text.ToString(), 
                "", txtSubject.Text.ToString(), EmailContent.ToString(), "", "HTML", "", "", "", "");

            string SMTPUserName = DotNetNuke.Entities.Controllers.HostController.Instance.GetString("SMTPUsername");
            //  string[] emptyStringArray = new string[0];
            List<Attachment> attchmnts = new List<Attachment>();

            DotNetNuke.Services.Mail.Mail.SendMail(SMTPUserName.ToString(), SMTPUserName.ToString(), txtFriendEmail.Text.ToString(), "", "",
                               txtEmail.Text.ToString(), DotNetNuke.Services.Mail.MailPriority.Normal, txtSubject.Text.ToString(),
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


                MyFormResults.Append("<p>" + txtFirstName.Text.ToString() + " " + txtLastName.Text.ToString() + "<br />");
             //   MyFormResults.Append(txtAddress.Text.ToString() + "<br />");
             //   MyFormResults.Append(txtCity.Text.ToString() + ", " + ddlState.SelectedValue.ToString() + " " + txtZip.Text.ToString() + "</p>");
                MyFormResults.AppendLine();
                MyFormResults.Append("<p>" + txtEmail.Text.ToString() + "</p>");
            //    MyFormResults.Append("<p>Daytime Phone: " + txtPhone.Text.ToString() + "<br />");
            //    MyFormResults.Append("Cell Phone: " + txtCell.Text.ToString() + "</p>");
                MyFormResults.Append("<p>Message:<br />" + txtQuestions.Text.ToString() + "</p>");



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

                //uUser.Profile.Street = txtAddress.Text.ToString();
                //uUser.Profile.City = txtCity.Text.ToString();

                //uUser.Profile.Telephone = txtPhone.Text.ToString();
                //uUser.Profile.Cell = txtCell.Text.ToString();
                
                //uUser.Profile.PostalCode = txtZip.Text.ToString();
                //uUser.Profile.Region = ddlState.SelectedValue.ToString();


                UserController.UpdateUser(PortalSettings.PortalId, uUser);

             //   lblStatus.Text = "Record Successully Updated";

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


    }
}