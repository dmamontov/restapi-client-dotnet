namespace Retailcrm.Versions.V4
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response PriceTypes()
        {
            return Request.MakeRequest(
                "/reference/price-types",
                Request.MethodGet
            );
        }

        public Response PriceTypesEdit(Dictionary<string, object> type)
        {
            if (!type.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!type.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            return Request.MakeRequest(
                $"/reference/price-types/{type["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "priceType", new JavaScriptSerializer().Serialize(type) }
                }
            );
        }
    }
}
