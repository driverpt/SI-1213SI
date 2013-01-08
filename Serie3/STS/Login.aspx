<%@ Page Title="Passive STS Log In" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="STS.Login" %>

<%@ OutputCache Location="None" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%:Page.Title %>.</h1>
    </hgroup>
    
    <asp:Wizard ID="UserloginWizard" runat="server" 
                            ActiveStepIndex="0" 
                            OnFinishButtonClick="UserloginWizard_FinishButtonClick" 
                            OnActiveStepChanged="UserloginWizard_ActiveStepChanged" 
                            FinishCompleteButtonText="Login">
        <WizardSteps>

            <asp:WizardStep ID="Login_Step" runat="server">

                <h2>Enter your user name and password below.</h2>

                <asp:Login ID="STSLogin" runat="server" DisplayRememberMe="False" OnAuthenticate="Login_Authenticate"
                    TitleText="Enter any user name and password to sign-in">
                    <LayoutTemplate>
                        <p class="validation-summary-errors">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>

                        <ol>
                            <li>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="Enter any non-empty string as username, the user name field is required." />
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                                                CssClass="field-validation-error" ErrorMessage="The password field is not required." />--%>
                            </li>
                        </ol>

                    </LayoutTemplate>
                </asp:Login>

            </asp:WizardStep>

            <asp:WizardStep ID="Roles_Step" runat="server">

                <h2>Choose the session Roles for the current user.</h2>

                <asp:CheckBoxList ID="RolesList" runat="server">
                    
                </asp:CheckBoxList>

            </asp:WizardStep>

        </WizardSteps>
    </asp:Wizard>


</asp:Content>
