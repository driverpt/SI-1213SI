//----------------------------------------------------------------------------------------------
//    Copyright 2012 Microsoft Corporation
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//----------------------------------------------------------------------------------------------

using System;
using System.IdentityModel.Services;
using System.Security.Claims;

namespace STS
{
    public partial class _Default : System.Web.UI.Page
    {
        /// <summary>
        /// We perform the WS-Federation Passive Protocol processing in this method. 
        /// </summary>
        protected void Page_PreRender( object sender, EventArgs e ) 
        {
            FederatedPassiveSecurityTokenServiceOperations.ProcessRequest( Request, User as ClaimsPrincipal, CustomSecurityTokenServiceConfiguration.Current.CreateSecurityTokenService(), Response );
        }
    }
}
