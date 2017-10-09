namespace Retailcrm.Versions.V4
{
    using System.Collections.Generic;

    public partial class Client
    {
        public Response Users(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/users", Request.MethodGet, parameters);
        }

        public Response UsersGroups(int page = 0, int limit = 0)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (page > 0)
            {
                parameters.Add("page", page);
            }

            if (limit > 0)
            {
                parameters.Add("limit", limit);
            }

            return Request.MakeRequest("/user-groups", Request.MethodGet, parameters);
        }

        public Response User(int id)
        {
            return Request.MakeRequest($"/users/{id}", Request.MethodGet);
        }
    }
}
