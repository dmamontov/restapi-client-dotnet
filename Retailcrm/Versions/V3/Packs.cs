namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response PacksList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return _request.MakeRequest("/orders/packs", Request.MethodGet, parameters);
        }

        public Response PacksCreate(Dictionary<string, object> pack)
        {
            if (pack.Count < 1)
            {
                throw new ArgumentException("Parameter `pack` must contains a data");
            }

            return _request.MakeRequest(
                "/orders/packs/create",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "pack", new JavaScriptSerializer().Serialize(pack) }
                }
            );
        }

        public Response PacksUpdate(Dictionary<string, object> pack)
        {
            if (pack.Count < 1)
            {
                throw new ArgumentException("Parameter `pack` must contains a data");
            }

            if (!pack.ContainsKey("id"))
            {
                throw new ArgumentException("Parameter `pack` must contains an id");
            }

            return _request.MakeRequest(
                $"/orders/packs/{pack["id"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "pack", new JavaScriptSerializer().Serialize(pack) }
                }
            );
        }

        public Response PacksDelete(string id)
        {
            if (id.Length < 1)
            {
                throw new ArgumentException("Parameter `id` must contains a data");
            }

            return _request.MakeRequest(
                $"/orders/packs/{id}/delete",
                Request.MethodPost
            );
        }

        public Response PacksGet(string id)
        {
            return _request.MakeRequest(
                $"/orders/packs/{id}",
                Request.MethodGet
            );
        }

        public Response PacksHistory(Dictionary<string, object> filter = null, int page = 1, int limit = 20)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (filter != null && filter.Count > 0)
            {
                parameters.Add("filter", filter);
            }

            if (page > 1)
            {
                parameters.Add("page", page);
            }

            if (limit > 0)
            {
                parameters.Add("limit", limit);
            }

            return _request.MakeRequest("/orders/packs/history", Request.MethodGet, parameters);
        }
    }
}
