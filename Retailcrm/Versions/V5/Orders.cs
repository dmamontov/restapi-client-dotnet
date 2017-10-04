namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response OrdersCombine(Dictionary<string, object> order, Dictionary<string, object> resultOrder, string technique = "ours")
        {
            if (order.Count <= 0)
            {
                throw new ArgumentException("Parameter `order` must contains a data");
            }

            if (!order.ContainsKey("id"))
            {
                throw new ArgumentException("Parameter `order` must contains `id` key");
            }

            if (resultOrder.Count <= 0)
            {
                throw new ArgumentException("Parameter `resultOrder` must contains a data");
            }

            if (!resultOrder.ContainsKey("id"))
            {
                throw new ArgumentException("Parameter `resultOrder` must contains `id` key");
            }

            return Request.MakeRequest(
                "/orders/combine",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "technique", technique },
                    { "order", new JavaScriptSerializer().Serialize(order) },
                    { "resultOrder", new JavaScriptSerializer().Serialize(resultOrder) }
                }
            );
        }
    }
}
