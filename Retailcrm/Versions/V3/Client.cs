namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Client
    {
        private readonly Request _request;
        private string _siteCode;

        public Client(string url, string key, string site = "")
        {
            if ("/" != url.Substring(url.Length - 1, 1))
            {
                url += "/";
            }

            url += "api/v3";

            _request = new Request(url, new Dictionary<string, object> { { "apiKey", key } });
            _siteCode = site;
        }

        /// <summary>
        /// Return current site
        /// </summary>
        /// <returns>string</returns>
        public string GetSite()
        {
            return _siteCode;
        }

        /// <summary>
        /// Return current site
        /// </summary>
        public void SetSite(string site)
        {
            _siteCode = site;
        }

        /// <summary>
        /// Check ID parameter
        /// </summary>
        /// <param name="by"></param>
        private static void CheckIdParameter(string by)
        {
            string[] allowedForBy = { "externalId", "id" };
            if (allowedForBy.Contains(by) == false)
            {
                throw new ArgumentException($"Value {by} for parameter `by` is not valid. Allowed values are {string.Join(", ", allowedForBy)}");
            }
        }

        /// <summary>
        /// Fill params by site value
        /// </summary>
        /// <param name="site"></param>
        /// <param name="param"></param>
        /// <returns>Dictionary</returns>
        private Dictionary<string, object> FillSite(string site, Dictionary<string, object> param)
        {
            if (site.Length > 1)
            {
                param.Add("site", site);
            }
            else if (_siteCode.Length > 1)
            {
                param.Add("site", _siteCode);
            }

            return param;
        }
    }
}
