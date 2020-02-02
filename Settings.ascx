<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FlexMLS.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="URL" Src="~/controls/URLControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>


<asp:Label ID="lblDebug" runat="server" />
 
<div class="dnnForm" id="form-settings">


    <fieldset>


    <dnn:sectionhead id="sectGeneralSettings" cssclass="Head" runat="server" text="General Settings" section="GeneralSettingSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="GeneralSettingSection" runat="server">
        <div class="dnnFormItem">
            <dnn:label id="lblAgentRole" runat="server" controlname="ddlAgentRole" suffix=":" />
            <asp:DropDownList ID="ddlAgentRole" runat="server">
            </asp:DropDownList>		
        </div>
        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblThumbSize" ControlName="txtThumbSize" ResourceKey="lblThumbSize" Suffix=":" />
            <asp:Textbox ID="txtThumbSize" runat="server" Width="80px" />
        </div>	       
	    <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblThumbPlacement" ControlName="rblThumbPlacement" ResourceKey="lblThumbPlacement" Suffix=":" />
                <asp:RadioButtonList ID="rblThumbPlacement" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Left" Value="Left" />
                <asp:ListItem Text="Right" Value="Right" />
                </asp:RadioButtonList>
        </div>

        <div class="dnnFormItem">
            <dnn:Label runat="server" ID="lblMaxImageSize" ControlName="txtMaxImageSize" ResourceKey="lblMaxImageSize" Suffix=":" />
            <asp:Textbox ID="txtMaxImageSize" runat="server" Width="80px" />
        </div>

    
    <div class="dnnFormItem">
    <dnn:label id="lblGoogleMapAPIKey" runat="server" controlname="txtGoogleMapAPIKey" suffix=":" />
            <asp:textbox id="txtGoogleMapAPIKey" cssclass="NormalTextBox" runat="server" />		
        </div>
        
        <div class="dnnFormItem">
    <dnn:label id="lblMLSImagesUrl" runat="server" controlname="txtMLSImagesUrl" suffix=":" />
            <asp:textbox id="txtMLSImagesUrl" cssclass="NormalTextBox" runat="server" />		
        </div>

    
</div>



    <dnn:sectionhead id="sectZillowSettings" cssclass="Head" runat="server" text="Zillow Settings" section="ZillowSettingSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="ZillowSettingSection" runat="server">
        <div class="dnnFormItem">
    <dnn:label id="lblZillowWebServiceId" runat="server" controlname="txtZillowWebServiceId" suffix=":" />
            <asp:textbox id="txtZillowWebServiceId" cssclass="NormalTextBox" runat="server" />		
        </div>
        <div class="dnnFormItem">
    <dnn:label id="lblZillowUserId" runat="server" controlname="txtZillowUserId" suffix=":" />
            <asp:textbox id="txtZillowUserId" cssclass="NormalTextBox" runat="server" />            
        </div>	

        <div class="dnnFormItem">
            <dnn:label id="lblAutoRunZillow" runat="server" controlname="cbxAutoRunZillow" suffix=":" />
            <asp:CheckBox ID="cbxAutoRunZillow" runat="server" />
        </div>
    
    </div>



<dnn:sectionhead id="sectContactFormSection" cssclass="Head" runat="server" text="Contact Form Settings" section="ContactFormSection"
	includerule="False" isexpanded="True"></dnn:sectionhead>

<div id="ContactFormSection" runat="server">
        <div class="dnnFormItem">
        <dnn:label id="lblEmailToDefault" runat="server" controlname="txtEmailToDefault"  suffix=":" />
       <asp:TextBox ID="txtEmailToDefault" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>			
        </div>
        <div class="dnnFormItem">
        <dnn:label id="lblEmailFrom" runat="server" controlname="txtEmailFrom" suffix=":" />
       <asp:TextBox ID="txtEmailFrom" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>            
        </div>
        <div class="dnnFormItem">
        <dnn:label id="lblEmailBCC" runat="server" controlname="txtEmailBCC" suffix=":" />
       <asp:TextBox ID="txtEmailBCC" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>            
        </div>		
	
        <div class="dnnFormItem">
        <dnn:label id="lblEmailSubject" runat="server" controlname="txtEmailSubject" suffix=":" />
       <asp:TextBox ID="txtEmailSubject" width="320" cssclass="NormalTextBox" runat="server"></asp:TextBox>           
        </div>	

       <div class="dnnFormItem">
            <dnn:label id="lblRecaptcha"  runat="server" text="Recaptcha Site Key" controlname="txtRecaptcha" suffix=":" />
            <asp:textbox id="txtRecaptcha" cssclass="NormalTextBox" runat="server" />	
        </div>

</div>


<dnn:sectionhead id="sectServerSettings" cssclass="Head" runat="server" text="MLS Server Settings" section="ServerSettingSection"
	includerule="False" isexpanded="False"></dnn:sectionhead>

<div id="ServerSettingSection" runat="server">
        <div class="dnnFormItem">
        <dnn:label id="lblServer" runat="server" controlname="txtServer" suffix=":" />
            <asp:textbox id="txtServer" cssclass="NormalTextBox" runat="server" />	
        </div>
        <div class="dnnFormItem">
       <dnn:label id="lblDataBase" runat="server" controlname="txtDataBase" suffix=":" />
            <asp:textbox id="txtDataBase" cssclass="NormalTextBox" runat="server" />            
        </div>
        <div class="dnnFormItem">
        <dnn:label id="lblLogin" runat="server" controlname="txtLogin" suffix=":" />

            <asp:textbox id="txtLogin" cssclass="NormalTextBox" runat="server" />          
        </div>		
	
        <div class="dnnFormItem">
        <dnn:label id="lblPassword" runat="server" controlname="txtPassword" suffix=":" />
            <asp:textbox id="txtPassword" cssclass="NormalTextBox" runat="server" />	          
        </div>	
    
    </div>

<dnn:sectionhead id="sectRetsServerSettings" cssclass="Head" runat="server" text="RETS Server Settings" section="RetsServerSettings"
	includerule="False" isexpanded="True"></dnn:sectionhead>
       

        
        
        <div class="dnnFormItem">
            <dnn:label id="lblRetsVersion" text="Rets Version " runat="server" controlname="lblRetsVersion" suffix=":" />
            <asp:textbox id="txtRetsVersion" cssclass="NormalTextBox" runat="server" />            
        </div>
        
        <div class="dnnFormItem">
            <dnn:label id="lblRetsUserAgent" text="Rets User Agent " runat="server" controlname="lblRetsUserAgent" suffix=":" />
            <asp:textbox id="txtRetsUserAgent" cssclass="NormalTextBox" runat="server" />          
        </div>		
	
        <div class="dnnFormItem">
            <dnn:label id="lblRetsUserAgentAuthType" text= "Rets User Agent Auth Type " runat="server" controlname="lblRetsUserAgentAuthType" suffix=":" />
            <asp:textbox id="txtRetsUserAgentAuthType" cssclass="NormalTextBox" runat="server" />	          
        </div>	
        <div class="dnnFormItem">
            <dnn:label id="lblRetsUserAgentPassword" text="Rets User Agent Password " runat="server" controlname="lblRetsUserAgentPassword" suffix=":" />
            <asp:textbox id="txtRetsUserAgentPassword" cssclass="NormalTextBox" runat="server" />	          
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblRetsUserName" text="Rets User Name " runat="server" controlname="lblRetsUserName" suffix=":" />
            <asp:textbox id="txtRetsUserName" cssclass="NormalTextBox" runat="server" />	          
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblRetsPassword" text="Rets Password " runat="server" controlname="lblRetsPassword" suffix=":" />
            <asp:textbox id="txtRetsPassword" cssclass="NormalTextBox" runat="server" />	          
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblRetsImageDirectory" Text="Destinantion Image Directory " runat="server" controlname="lblRetsImageDirectory" suffix=":" />
            <asp:textbox id="txtRetsImageDirectory" cssclass="NormalTextBox" runat="server" />	          
        </div>
         <div class="dnnFormItem">
            <dnn:label id="lblImageDownLoadTestMode" text="Test Mode " runat="server" controlname="lblImageDownLoadTestMode" suffix=":" />
            <asp:CheckBox id="cbxImageDownLoadTestMode" cssclass="NormalTextBox" runat="server" />	          
        </div>
      
    
    



    </fieldset>

</div>