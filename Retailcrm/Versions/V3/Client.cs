namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Script.Serialization;

    public class Client
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

        public Response OrdersCreate(Dictionary<string, object> order, string site = "")
        {
            if (order.Count < 1)
            {
                throw new ArgumentException("Parameter `order` must contains a data");
            }

            return _request.MakeRequest(
                "/orders/create",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "order", new JavaScriptSerializer().Serialize(order) }
                    }
                )
            );
        }

        public Response OrdersUpdate(Dictionary<string, object> order, string by = "externalId", string site = "")
        {
            if (order.Count < 1)
            {
                throw new ArgumentException("Parameter `order` must contains a data");
            }

            if (!order.ContainsKey("id") && !order.ContainsKey("externalId"))
            {
                throw new ArgumentException("Parameter `order` must contains an id or externalId");
            }

            CheckIdParameter(by);

            string uid = by == "externalId" ? order["externalId"].ToString() : order["id"].ToString();

            return _request.MakeRequest(
                $"/orders/{uid}/edit",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "by", by },
                        { "order", new JavaScriptSerializer().Serialize(order) }
                    }
                )
            );
        }

        public Response OrdersGet(string id, string by = "externalId", string site = "")
        {
            CheckIdParameter(by);

            return _request.MakeRequest(
                $"/orders/{id}",
                Request.MethodGet,
                FillSite(
                    site,
                    new Dictionary<string, object>() {
                        { "by", by }
                    }
                )
            );
        }

        public Response OrdersList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (filter != null && filter.Count > 0)
            {
                parameters.Add("filter", filter);
            }
            if (page > 0)
            {
                parameters.Add("page", page);
            }
            if (limit > 0)
            {
                parameters.Add("limit", limit);
            }

            return _request.MakeRequest("/orders", Request.MethodGet, parameters);
        }

        public Response OrdersFixExternalIds(Dictionary<string, object>[] ids)
        {
            return _request.MakeRequest(
                "/orders/fix-external-ids",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "orders", new JavaScriptSerializer().Serialize(ids) }
                }
            );
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
