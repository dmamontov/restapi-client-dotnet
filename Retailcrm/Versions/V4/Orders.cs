namespace Retailcrm.Versions.V4
{
    using System.Collections.Generic;

    public partial class Client
    {
        /// <summary>
        /// Get orders history
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Response OrdersHistory(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/orders/history", Request.MethodGet, parameters);
        }
    }
}
