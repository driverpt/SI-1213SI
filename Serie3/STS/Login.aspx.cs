using RBAC.Core;
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
namespace STS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
        {

        }

        protected void UserloginWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Wizard wizard = (Wizard)sender;

            if (String.IsNullOrEmpty(STSLogin.UserName))
            {

            }

            RBAC.Core.Entities.User user = Factory.User(STSLogin.UserName);

            if (user != null)
            {
                Session.Add("User_Roles_" + user.Name, GetSelectedRoles((CheckBoxList)wizard.ActiveStep.FindControl("RolesList")));

                FormsAuthentication.RedirectFromLoginPage(STSLogin.UserName, false);
            }
            else
            {
                
            }
        }

        private List<RBAC.Core.Entities.Role> GetSelectedRoles(CheckBoxList checkBoxList)
        {
            List<RBAC.Core.Entities.Role> roles = new List<RBAC.Core.Entities.Role>();
            foreach (ListItem listItem in checkBoxList.Items)
            {
                if (listItem.Selected)
                {
                    roles.Add(Factory.Role(listItem.Text));
                }
            }
            return roles;
        }

        protected void UserloginWizard_ActiveStepChanged(object sender, EventArgs e)
        {
            Wizard wizard = (Wizard)sender;

            if (wizard != null && wizard.ActiveStepIndex == 1)
            {
                RBACHelper helper = new RBACHelper();

                List<RBAC.Core.Entities.Role> roles = helper.GetUserRolesHierarchy(Factory.User(STSLogin.UserName).Id);

                CheckBoxList rolesList = (CheckBoxList)wizard.ActiveStep.FindControl("RolesList");

                foreach (RBAC.Core.Entities.Role role in roles)
                {
                    rolesList.Items.Add(new ListItem(role.Name));
                }
            }

        }
    }
}