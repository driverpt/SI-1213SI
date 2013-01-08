using System;
using System.IdentityModel.Tokens;

namespace CustomToken
{
    public class CustomTokenKeyIdentifierClause : SecurityKeyIdentifierClause
    {
        const string localId="CustomToken";
        private string _audience;
                
        public CustomTokenKeyIdentifierClause(string audience )
            :base (localId)
        {
            if (audience == null)
            {
                throw new ArgumentNullException("audience");
            }
            _audience = audience;
        }

        public string Audience
        {
            get
            {
                return _audience;
            }
        }

        public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
        {
            if (keyIdentifierClause is CustomTokenKeyIdentifierClause)
            {
                return true;
            }

            return false;
        }
    }
}
