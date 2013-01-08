using RBAC.Core;
using RBAC.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace STS
{

    public class CustomSecurityTokenService : SecurityTokenService
    {
        // Certificate Constants
        private const string SIGNING_CERTIFICATE_NAME = "CN=localhost";
        private const string ENCRYPTING_CERTIFICATE_NAME = "CN=localhost";

        private SigningCredentials _signingCreds;
        private EncryptingCredentials _encryptingCreds;
        // Used for validating applies to address, set to URI used in RP app of application, could also have been done via config
        private string _addressExpected = "http://localhost:19851/";

        private string[] userPermissions;

        public CustomSecurityTokenService(SecurityTokenServiceConfiguration configuration)
            : base(configuration)
        {
            _signingCreds = new X509SigningCredentials(CertificateUtil.GetCertificate(StoreName.My, StoreLocation.LocalMachine, SIGNING_CERTIFICATE_NAME));
            _encryptingCreds = new X509EncryptingCredentials(CertificateUtil.GetCertificate(StoreName.My, StoreLocation.LocalMachine, ENCRYPTING_CERTIFICATE_NAME));
        }


        protected override Scope GetScope(ClaimsPrincipal principal, RequestSecurityToken request)
        {
            // Validate the AppliesTo address
            ValidateAppliesTo( request.AppliesTo );

            // Create the scope using the request AppliesTo address and the RP identity
            Scope scope = new Scope( request.AppliesTo.Uri.AbsoluteUri, _signingCreds );

            if (Uri.IsWellFormedUriString(request.ReplyTo, UriKind.Absolute))
            {
                if (request.AppliesTo.Uri.Host != new Uri(request.ReplyTo).Host)
                    scope.ReplyToAddress = request.AppliesTo.Uri.AbsoluteUri;
                else
                    scope.ReplyToAddress = request.ReplyTo;
            }
            else
            {
                Uri resultUri = null;
                if (Uri.TryCreate(request.AppliesTo.Uri, request.ReplyTo, out resultUri))
                    scope.ReplyToAddress = resultUri.AbsoluteUri;
                else
                    scope.ReplyToAddress = request.AppliesTo.Uri.ToString() ;
            }

            // Note: In this sample app only a single RP identity is shown, which is localhost, and the certificate of that RP is 
            // populated as _encryptingCreds
            // If you have multiple RPs for the STS you would select the certificate that is specific to 
            // the RP that requests the token and then use that for _encryptingCreds
            scope.EncryptingCredentials = _encryptingCreds;

            return scope;
        }

        protected override ClaimsIdentity GetOutputClaimsIdentity( ClaimsPrincipal principal, RequestSecurityToken request, Scope scope )
        {
            ClaimsIdentity outgoingIdentity = new ClaimsIdentity();
            outgoingIdentity.AddClaims(principal.Claims);

            List<Role> roles = (List<Role>)HttpContext.Current.Session["User_Roles_" + principal.Identity.Name];

            RBACHelper rbacHelper = new RBACHelper();
            Permission[] permissions = rbacHelper.GetUserPermissions(Factory.Session(Factory.User(principal.Identity.Name), roles.ToArray()));

            outgoingIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/permissions", Serialize(permissions)));

            return outgoingIdentity;
        }

        private String Serialize(Permission[] permissions)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Permission p in permissions)
            {
                if (sb.Length != 0)
                {
                    sb.Append(",");
                }
                sb.Append(p.Description);
            }

            return sb.ToString();
        }

        void ValidateAppliesTo(EndpointReference appliesTo)
        {
            if (appliesTo == null)
            {
                throw new InvalidRequestException("The appliesTo is null.");
            }

            if (!appliesTo.Uri.Equals(new Uri(_addressExpected)))
            {
                throw new InvalidRequestException(String.Format("The relying party address is not valid. Expected value is {0}, the actual value is {1}.", _addressExpected, appliesTo.Uri.AbsoluteUri));
            }
        }
       
    }
}
