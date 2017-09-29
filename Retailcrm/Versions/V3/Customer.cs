﻿namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response CustomersCreate(Dictionary<string, object> customer, string site = "")
        {
            if (customer.Count < 1)
            {
                throw new ArgumentException("Parameter `customer` must contains a data");
            }

            return _request.MakeRequest(
                "/customers/create",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "customer", new JavaScriptSerializer().Serialize(customer) }
                    }
                )
            );
        }

        public Response CustomersUpdate(Dictionary<string, object> customer, string by = "externalId", string site = "")
        {
            if (customer.Count < 1)
            {
                throw new ArgumentException("Parameter `customer` must contains a data");
            }

            if (!customer.ContainsKey("id") && !customer.ContainsKey("externalId"))
            {
                throw new ArgumentException("Parameter `customer` must contains an id or externalId");
            }

            CheckIdParameter(by);

            string uid = by == "externalId" ? customer["externalId"].ToString() : customer["id"].ToString();

            return _request.MakeRequest(
                $"/customers/{uid}/edit",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "by", by },
                        { "customer", new JavaScriptSerializer().Serialize(customer) }
                    }
                )
            );
        }

        public Response CustomersGet(string id, string by = "externalId", string site = "")
        {
            CheckIdParameter(by);

            return _request.MakeRequest(
                $"/customers/{id}",
                Request.MethodGet,
                FillSite(
                    site,
                    new Dictionary<string, object>() {
                        { "by", by }
                    }
                )
            );
        }

        public Response CustomersList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return _request.MakeRequest("/customers", Request.MethodGet, parameters);
        }

        public Response CustomersFixExternalIds(Dictionary<string, object>[] ids)
        {
            return _request.MakeRequest(
                "/customers/fix-external-ids",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "customers", new JavaScriptSerializer().Serialize(ids) }
                }
            );
        }

        public Response CustomersUpload(List<object> customers, string site = "")
        {
            if (customers.Count < 1)
            {
                throw new ArgumentException("Parameter `customers` must contains a data");
            }

            if (customers.Count > 50)
            {
                throw new ArgumentException("Parameter `customers` must contain less or 50 records");
            }

            return _request.MakeRequest(
                "/customers/upload",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "customers", new JavaScriptSerializer().Serialize(customers) }
                    }
                )
            );
        }
    }
}
