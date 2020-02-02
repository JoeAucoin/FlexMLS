<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TellAFriend.ascx.cs" Inherits="GIBS.Modules.FlexMLS.TellAFriend" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>


<script type="text/javascript" language="javascript" >


</script>

<h5><asp:Label ID="lblMlsInterest" runat="server" Text="Label"></asp:Label></h5>

 

<div class="dnnForm" id="form-tellafriend">
 <fieldset>
 
	<div class="dnnFormItem"><dnn:Label runat="server" ID="lblFirstName" ControlName="txtFirstName" ResourceKey="lblFirstName" Suffix=":" />
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
    <dnn:Label runat="server" ID="lblFriendEmail" ControlName="txtFriendEmail" ResourceKey="lblFriendEmail" Suffix=":" />
        <asp:TextBox runat="server" ID="txtFriendEmail" />
	</div>

	<div class="dnnFormItem"><dnn:Label runat="server" ID="lblSubject" ControlName="txtSubject" ResourceKey="lblSubject" Suffix=":" />
        <asp:TextBox runat="server" ID="txtSubject" />
	</div>

	<div class="dnnFormItem">
    <dnn:Label runat="server" ID="lblQuestions" ControlName="txtQuestions" ResourceKey="lblQuestions" Suffix=":" />
        <asp:TextBox runat="server" ID="txtQuestions" TextMode="MultiLine" Rows="5" />
	</div>
	
 </fieldset>
</div>


    <div style="text-align:left"><asp:Button ID="btnSubmit" resourcekey="btnSubmit" OnClientClick="UseData();" 
            CausesValidation="True" runat="server" CssClass="dnnPrimaryAction" 
                Text="Submit" onclick="btnSubmit_Click"  />
               <asp:Button ID="btnCancel" resourcekey="btnCancel" 
            CausesValidation="False" runat="server" CssClass="dnnSecondaryAction" 
                Text="Cancel" onclick="btnCancel_Click"  /> 
                
                </div>


    <asp:HiddenField ID="txtMlsNumber" runat="server" />

