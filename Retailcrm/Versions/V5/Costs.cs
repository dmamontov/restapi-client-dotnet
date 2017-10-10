﻿namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response CostsList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/costs", Request.MethodGet, parameters);
        }

        public Response CostsCreate(Dictionary<string, object> cost, string site = "")
        {
            if (cost.Count < 1)
            {
                throw new ArgumentException("Parameter `cost` must contains a data");
            }

            return Request.MakeRequest(
                "/costs/create",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "cost", new JavaScriptSerializer().Serialize(cost) }
                    }
                )
            );
        }

        public Response CostsDelete(List<string> ids)
        {
            if (ids.Count < 1)
            {
                throw new ArgumentException("Parameter `ids` must contains a data");
            }

            return Request.MakeRequest(
                "/costs/delete",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "ids", new JavaScriptSerializer().Serialize(ids) }
                }
            );
        }

        public Response CostsUpload(List<object> costs)
        {
            if (costs.Count < 1)
            {
                throw new ArgumentException("Parameter `costs` must contains a data");
            }

            return Request.MakeRequest(
                "/costs/upload",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "costs", new JavaScriptSerializer().Serialize(costs) }
                }
            );
        }

        public Response CostsGet(int id)
        {
            return Request.MakeRequest($"/costs/{id}", Request.MethodGet);
        }

        public Response CostsDelete(string id)
        {
            return Request.MakeRequest(
                $"/costs/{id}/delete",
                Request.MethodPost
            );
        }

        public Response CostsUpdate(Dictionary<string, object> cost, string site = "")
        {
            if (cost.Count < 1)
            {
                throw new ArgumentException("Parameter `cost` must contains a data");
            }

            if (!cost.ContainsKey("id"))
            {
                throw new ArgumentException("Parameter `cost` must contains an id");
            }

            return Request.MakeRequest(
                $"/costs/{cost["id"].ToString()}/edit",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "cost", new JavaScriptSerializer().Serialize(cost) }
                    }
                )
            );
        }
    }
}