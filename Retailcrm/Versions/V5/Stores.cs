namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;

    public partial class Client
    {
        public new Response StoreSettingGet(string code)
        {
            throw new ArgumentException("This method is unavailable in API V5", code);
        }

        public new Response StoreSettingsEdit(Dictionary<string, object> configuration)
        {
            throw new ArgumentException("This method is unavailable in API V5");
        }

        public Response StoreProductsGroups(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/store/products-groups", Request.MethodGet, parameters);
        }

        public Response StoreProductsProperties(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/store/products/properties", Request.MethodGet, parameters);
        }
    }
}
