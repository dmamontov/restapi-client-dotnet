namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response OrdersCreate(Dictionary<string, object> order, string site = "")
        {
            if (order.Count < 1)
            {
                throw new ArgumentException("Parameter `order` must contains a data");
            }

            return Request.MakeRequest(
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

            return Request.MakeRequest(
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

            return Request.MakeRequest(
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

            return Request.MakeRequest("/orders", Request.MethodGet, parameters);
        }

        public Response OrdersFixExternalIds(Dictionary<string, object>[] ids)
        {
            return Request.MakeRequest(
                "/orders/fix-external-ids",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "orders", new JavaScriptSerializer().Serialize(ids) }
                }
            );
        }

        public Response OrdersHistory(DateTime? startDate = null, DateTime? endDate = null, int limit = 200, int offset = 0, bool skipMyChanges = true)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (startDate != null)
            {
                parameters.Add("startDate", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (endDate != null)
            {
                parameters.Add("endDate", endDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (limit > 0)
            {
                parameters.Add("limit", limit);
            }

            if (offset > 0)
            {
                parameters.Add("offset", offset);
            }

            parameters.Add("skipMyChanges", skipMyChanges);
            
            return Request.MakeRequest(
                "/orders/history",
                Request.MethodGet,
                parameters
            );
        }

        public Response OrdersStatuses(List<string> ids, List<string> externalIds = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (ids == null && externalIds == null)
            {
                throw new ArgumentException("You must set the array of `ids` or `externalIds`.");
            }

            if (
                ids != null && externalIds != null && ids.Count + externalIds.Count > 500 ||
                ids == null && externalIds != null && externalIds.Count > 500 ||
                ids != null && externalIds == null && ids.Count > 500
            )
            {
                throw new ArgumentException("Too many ids or externalIds. Maximum number of elements is 500");
            }

            if (ids != null && ids.Count > 0)
            {
                parameters.Add("ids", ids);
            }

            if (externalIds != null && externalIds.Count > 0)
            {
                parameters.Add("externalIds", externalIds);
            }

            return Request.MakeRequest(
                "/orders/statuses",
                Request.MethodGet,
                parameters
            );
        }

        public Response OrdersUpload(List<object> orders, string site = "")
        {
            if (orders.Count < 1)
            {
                throw new ArgumentException("Parameter `orders` must contains a data");
            }

            if (orders.Count > 50)
            {
                throw new ArgumentException("Parameter `orders` must contain 50 or less records");
            }

            return Request.MakeRequest(
                "/orders/upload",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "orders", new JavaScriptSerializer().Serialize(orders) }
                    }
                )
            );
        }
    }
}
