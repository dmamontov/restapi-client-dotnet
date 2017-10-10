﻿namespace Retailcrm.Versions.V4
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response StoreProducts(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/store/products", Request.MethodGet, parameters);
        }

        public Response StorePricesUpload(List<object> prices)
        {
            if (prices.Count< 1)
            {
                throw new ArgumentException("Parameter `prices` must contains a data");
            }

            if (prices.Count > 250)
            {
                throw new ArgumentException("Parameter `prices` must contain 250 or less records");
            }

            return Request.MakeRequest(
                "/store/prices/upload",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "prices", new JavaScriptSerializer().Serialize(prices) }
                }
            );
        }

        public Response StoreSettingGet(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Parameter `code` is mandatory");
            }

            return Request.MakeRequest(
                $"/store/setting/{code}",
                Request.MethodGet
            );
        }

        public Response StoreSettingsEdit(Dictionary<string, object> configuration)
        {
            if (configuration.Count < 1)
            {
                throw new ArgumentException("Parameter `configuration` must contain data");
            }

            if (!configuration.ContainsKey("clientId"))
            {
                throw new ArgumentException("Parameter `configuration` should contain `clientId`");
            }

            if (!configuration.ContainsKey("baseUrl"))
            {
                throw new ArgumentException("Parameter `configuration` should contain `baseUrl`");
            }

            if (!configuration.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `configuration` should contain `code`");
            }

            if (!configuration.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `configuration` should contain `name`");
            }

            return Request.MakeRequest(
                $"/store/setting/{configuration["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "configuration", new JavaScriptSerializer().Serialize(configuration) }
                }
            );
        }
    }
}