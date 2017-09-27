namespace Retailcrm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public class AbstractClient
    {
        private string _siteCode;

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
        protected static void CheckIdParameter(string by)
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
        protected Dictionary<string, object> FillSite(string site, Dictionary<string, object> param)
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
