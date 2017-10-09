namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response PaymentsCreate(Dictionary<string, object> payment, string site = "")
        {
            if (payment.Count < 1)
            {
                throw new ArgumentException("Parameter `payment` must contains a data");
            }

            return Request.MakeRequest(
                "/orders/payments/create",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "payment", new JavaScriptSerializer().Serialize(payment) }
                    }
                )
            );
        }

        public Response PaymentsUpdate(Dictionary<string, object> payment, string by = "id", string site = "")
        {
            if (payment.Count < 1)
            {
                throw new ArgumentException("Parameter `payment` must contains a data");
            }

            if (!payment.ContainsKey("id") && !payment.ContainsKey("externalId"))
            {
                throw new ArgumentException("Parameter `payment` must contains an id or externalId");
            }

            CheckIdParameter(by);

            string uid = by == "externalId" ? payment["externalId"].ToString() : payment["id"].ToString();

            return Request.MakeRequest(
                $"/orders/payments/{uid}/edit",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "by", by },
                        { "payment", new JavaScriptSerializer().Serialize(payment) }
                    }
                )
            );
        }

        public Response PaymentsDelete(string id)
        {
            return Request.MakeRequest(
                $"/orders/payments/{id}/delete",
                Request.MethodPost
            );
        }
    }
}
