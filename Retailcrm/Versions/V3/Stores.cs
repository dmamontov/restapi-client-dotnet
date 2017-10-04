namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response StoreInventoriesGet(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/store/inventories", Request.MethodGet, parameters);
        }

        public Response StoreInventoriesUpload(List<object> offers, string site = "")
        {
            if (offers.Count< 1)
            {
                throw new ArgumentException("Parameter `offers` must contains a data");
            }

            if (offers.Count > 250)
            {
                throw new ArgumentException("Parameter `offers` must contain 250 or less records");
            }

            return Request.MakeRequest(
                "/store/inventories/upload",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "offers", new JavaScriptSerializer().Serialize(offers) }
                    }
                )
            );
        }
    }
}
