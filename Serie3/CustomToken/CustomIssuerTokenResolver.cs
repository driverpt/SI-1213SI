using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.Xml;

namespace CustomToken
{
    public class CustomIssuerTokenResolver : IssuerTokenResolver
    {
        private Dictionary<string, string> _keys;

        public CustomIssuerTokenResolver()
        {
            _keys = new Dictionary<string, string>();            
        }

        public void AddAudienceKeyPair(string audience, string symmetricKey)
        {
            _keys.Add(audience, symmetricKey);
        }

        public override void LoadCustomConfiguration(System.Xml.XmlNodeList nodelist)
        {
            foreach (XmlNode node in nodelist)
            {
                XmlDictionaryReader rdr = XmlDictionaryReader.CreateDictionaryReader(new XmlTextReader(new StringReader(node.OuterXml)));
                rdr.MoveToContent();

                string symmetricKey = rdr.GetAttribute("symmetricKey");
                string audience = rdr.GetAttribute("audience");
                
                this.AddAudienceKeyPair(audience, symmetricKey);
            }
        }

        protected override bool TryResolveSecurityKeyCore(SecurityKeyIdentifierClause keyIdentifierClause, out SecurityKey key)
        {
            key = null;
            CustomTokenKeyIdentifierClause keyClause = keyIdentifierClause as CustomTokenKeyIdentifierClause;
            if (keyClause != null)
            {
                string base64Key = null;
                _keys.TryGetValue(keyClause.Audience, out base64Key);
                if (!string.IsNullOrEmpty(base64Key))
                {
                    key = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(base64Key));
                    return true;
                }
            }

            return false;
        }

    }
}
