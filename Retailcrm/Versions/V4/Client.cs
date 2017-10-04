namespace Retailcrm.Versions.V4
{
    using System.Collections.Generic;
    using ParentClass = V3.Client;

    public partial class Client : ParentClass
    {
        public Client(string url, string key, string site = "") : base(url, key, site)
        {
            if ("/" != url.Substring(url.Length - 1, 1))
            {
                url += "/";
            }

            url += "api/v4";

            Request = new Request(url, new Dictionary<string, object> { { "apiKey", key } });
            SiteCode = site;
        }
    }
}
