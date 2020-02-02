<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="GIBS.Modules.FlexMLS.Contact" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<script src="https://www.google.com/recaptcha/api.js" async defer></script>

<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FlexMLS/JavaScript/jquery.timepicker.css" />
<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/cupertino/jquery-ui.css" />



<script type="text/javascript">

    function recaptchaCallback() {
        $('#<%= btnSubmit.ClientID %>').removeAttr('disabled');
    };

    $(function () {
        $("#txtShowDate").datepicker({
            numberOfMonths: 1,
            minDate : 1,
            showButtonPanel: false,
            showCurrentAtPos: 0
        });
        $('#txtShowTime').timepicker({
            'minTime': '9:00am',
            'maxTime': '4:30pm',
            'timeFormat': 'h:i A',
            'showDuration': false
        });


    });






    function UseData() {
        $.Watermark.HideAll();   //Do Stuff   $.Watermark.ShowAll();
    }

    
    jQuery(function ($) {
        
        $("#<%= txtPhone.ClientID %>").Watermark("(000) 000-0000");
        $("#<%= txtCell.ClientID %>").Watermark("(000) 000-0000");
        $("#<%= txtPhone.ClientID %>").mask("(999) 999-9999? x99999");
        $("#<%= txtCell.ClientID %>").mask("(999) 999-9999?");

    });
</script>
<style type="text/css">



</style>

<h5><asp:Label ID="lblMlsInterest" runat="server" Text="Label"></asp:Label></h5>



<div class="dnnForm" id="tabs-client">

<fieldset>
<asp:Panel ID="PanelShowDateTime" runat="server" Visible="False">
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblShowingDateTime" ControlName="radShowDate" ResourceKey="lblShowingDateTime" Suffix=":" CssClass="dnnFormLabel"   />
            <asp:TextBox ID="txtShowDate" runat="server" ClientIDMode="Static" />
        </div>
        <div class="dnnFormItem">
        <dnn:Label runat="server" ID="lblShowingTime" ControlName="radShowDate" ResourceKey="lblShowingTime" Suffix=":" CssClass="dnnFormLabel" />
        <asp:TextBox ID="txtShowTime" runat="server" ClientIDMode="Static" />   
        </div>
</asp:Panel>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblFirstName" ControlName="txtFirstName" ResourceKey="lblFirstName" Suffix=":" />
            <asp:TextBox runat="server" ID="txtFirstName" /><asp:RequiredFieldValidator ID="rfvtxtFirstName" runat="server" ControlToValidate="txtFirstName" 
                    CssClass="dnnDragdropTip" display="Dynamic" ResourceKey="txtFirstName.Required" />		
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblLastName" ControlName="txtLastName" ResourceKey="lblLastName" Suffix=":" />
            <asp:TextBox runat="server" ID="txtLastName" /><asp:RequiredFieldValidator ID="rfvtxtLastName" runat="server" ControlToValidate="txtLastName" 
                        CssClass="dnnDragdropTip" display="Dynamic" ResourceKey="txtLastName.Required" />		
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblEmail" ControlName="txtEmail" ResourceKey="lblEmail" Suffix=":" />
            <asp:TextBox runat="server" ID="txtEmail" /><asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmail" 
                            CssClass="dnnDragdropTip" display="Dynamic" ResourceKey="REQtxtEmail" />            
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblAddress" ControlName="txtAddress" ResourceKey="lblAddress" Suffix=":" />   
            <asp:TextBox runat="server" ID="txtAddress" />      
        </div>		
	
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCityStateZip" ControlName="txtCity" ResourceKey="lblCityStateZip" Suffix=":" />
            <asp:TextBox runat="server" ID="txtCity" Width="128px" /> <asp:DropDownList ID="ddlState" runat="server" Width="120px"></asp:DropDownList> 
            <asp:TextBox runat="server" ID="txtZip" Width="50px" />          
        </div>	
        <div class="dnnFormItem">
		    <dnn:Label runat="server" ID="lblPhone" ControlName="txtPhone" ResourceKey="lblPhone" Suffix=":" />
		    <asp:TextBox runat="server" ID="txtPhone" />		
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblCell" ControlName="txtCell" ResourceKey="lblCell" Suffix=":" />
            <asp:TextBox runat="server" ID="txtCell" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblPreviousAgent" ControlName="ddlPreviousAgent" ResourceKey="lblPreviousAgent" Suffix=":" />
            <asp:DropDownList ID="ddlPreviousAgent" runat="server"></asp:DropDownList>
        </div>		
	
        <div class="dnnFormItem">
  	        <dnn:Label runat="server" ID="lblQuestions" ControlName="txtQuestions" ResourceKey="lblQuestions" Suffix=":" />
            <asp:TextBox runat="server" ID="txtQuestions" TextMode="MultiLine" Rows="5" />        
        </div>	
     <div class="dnnFormItem dnnFormInput">
    <div class="g-recaptcha" data-callback="recaptchaCallback" data-sitekey='<% = _Recaptcha %>'></div><span class="msg-error error"></span>
    </div>

</fieldset>
    <div style="text-align:left"><asp:Button ID="btnSubmit" resourcekey="btnSubmit" OnClientClick="UseData();" 
            CausesValidation="True" runat="server" CssClass="dnnPrimaryAction" 
                Text="Submit" onclick="btnSubmit_Click" Enabled="false"  />
               <asp:Button ID="btnCancel" resourcekey="btnCancel" 
            CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" 
                Text="Cancel" onclick="btnCancel_Click"  /> 
                
    </div>


    <asp:HiddenField ID="txtMlsNumber" runat="server" />

</div>