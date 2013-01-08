using CustomToken;
using System.IdentityModel.Configuration;
using System.Web;

namespace STS
{

    public class CustomSecurityTokenServiceConfiguration : SecurityTokenServiceConfiguration
    {
        static readonly object syncRoot = new object();
        static string CustomSecurityTokenServiceConfigurationKey = "CustomSecurityTokenServiceConfigurationKey";
        static string Base64SymmetricKey = "wAVkldQiFypTQ+kdNdGWCYCHRcee8XmXxOvgmak8vSY=";

        public CustomSecurityTokenServiceConfiguration()
            : base("STS")
        {
            this.SecurityTokenService = typeof(STS.CustomSecurityTokenService);
            CustomTokenHandler tokenHandler = new CustomTokenHandler();
            this.SecurityTokenHandlers.Add(tokenHandler);

            CustomIssuerTokenResolver customTokenResolver = new CustomToken.CustomIssuerTokenResolver();
            customTokenResolver.AddAudienceKeyPair("http://localhost:19851/", Base64SymmetricKey);
            this.IssuerTokenResolver = customTokenResolver;

            this.DefaultTokenType = CustomTokenHandler.CustomTokenTypeUri;
        }

        public static CustomSecurityTokenServiceConfiguration Current
        {
            get
            {
                HttpApplicationState httpAppState = HttpContext.Current.Application;

                CustomSecurityTokenServiceConfiguration myConfiguration = httpAppState.Get( CustomSecurityTokenServiceConfigurationKey ) as CustomSecurityTokenServiceConfiguration;

                if ( myConfiguration != null )
                {
                    return myConfiguration;
                }

                lock ( syncRoot )
                {
                    myConfiguration = httpAppState.Get( CustomSecurityTokenServiceConfigurationKey ) as CustomSecurityTokenServiceConfiguration;

                    if ( myConfiguration == null )
                    {
                        myConfiguration = new CustomSecurityTokenServiceConfiguration();
                        httpAppState.Add( CustomSecurityTokenServiceConfigurationKey, myConfiguration );
                    }

                    return myConfiguration;
                }
            }
        }

    }
}
