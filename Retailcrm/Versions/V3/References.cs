namespace Retailcrm.Versions.V3
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response Countries()
        {
            return Request.MakeRequest(
                "/reference/countries",
                Request.MethodGet
            );
        }

        public Response DeliveryServices()
        {
            return Request.MakeRequest(
                "/reference/delivery-services",
                Request.MethodGet
            );
        }

        public Response DeliveryTypes()
        {
            return Request.MakeRequest(
                "/reference/delivery-types",
                Request.MethodGet
            );
        }

        public Response OrderMethods()
        {
            return Request.MakeRequest(
                "/reference/order-methods",
                Request.MethodGet
            );
        }

        public Response OrderTypes()
        {
            return Request.MakeRequest(
                "/reference/order-types",
                Request.MethodGet
            );
        }

        public Response PaymentStatuses()
        {
            return Request.MakeRequest(
                "/reference/payment-statuses",
                Request.MethodGet
            );
        }

        public Response PaymentTypes()
        {
            return Request.MakeRequest(
                "/reference/payment-types",
                Request.MethodGet
            );
        }

        public Response ProductStatuses()
        {
            return Request.MakeRequest(
                "/reference/product-statuses",
                Request.MethodGet
            );
        }

        public Response Sites()
        {
            return Request.MakeRequest(
                "/reference/sites",
                Request.MethodGet
            );
        }

        public Response StatusGroups()
        {
            return Request.MakeRequest(
                "/reference/status-groups",
                Request.MethodGet
            );
        }

        public Response Statuses()
        {
            return Request.MakeRequest(
                "/reference/statuses",
                Request.MethodGet
            );
        }

        public Response Stores()
        {
            return Request.MakeRequest(
                "/reference/stores",
                Request.MethodGet
            );
        }

        public Response DeliveryServicesEdit(Dictionary<string, object> service)
        {
            if (!service.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!service.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            return Request.MakeRequest(
                $"/reference/delivery-services/{service["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "deliveryService", new JavaScriptSerializer().Serialize(service) }
                }
            );
        }

        public Response DeliveryTypesEdit(Dictionary<string, object> type)
        {
            if (!type.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!type.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            if (!type.ContainsKey("defaultCost"))
            {
                throw new ArgumentException("Parameter `defaultCost` is missing");
            }

            if (!type.ContainsKey("defaultNetCost"))
            {
                throw new ArgumentException("Parameter `defaultCost` is missing");
            }

            return Request.MakeRequest(
                $"/reference/delivery-types/{type["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "deliveryType", new JavaScriptSerializer().Serialize(type) }
                }
            );
        }

        public Response OrderMethodsEdit(Dictionary<string, object> method)
        {
            if (!method.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!method.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            return Request.MakeRequest(
                $"/reference/order-methods/{method["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "orderMethod", new JavaScriptSerializer().Serialize(method) }
                }
            );
        }

        public Response OrderTypesEdit(Dictionary<string, object> type)
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
                $"/reference/order-types/{type["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "orderType", new JavaScriptSerializer().Serialize(type) }
                }
            );
        }

        public Response PaymentStatusesEdit(Dictionary<string, object> status)
        {
            if (!status.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!status.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            return Request.MakeRequest(
                $"/reference/payment-statuses/{status["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "paymentStatus", new JavaScriptSerializer().Serialize(status) }
                }
            );
        }

        public Response PaymentTypesEdit(Dictionary<string, object> type)
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
                $"/reference/payment-types/{type["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "paymentType", new JavaScriptSerializer().Serialize(type) }
                }
            );
        }

        public Response ProductStatusesEdit(Dictionary<string, object> status)
        {
            if (!status.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!status.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            return Request.MakeRequest(
                $"/reference/product-statuses/{status["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "productStatus", new JavaScriptSerializer().Serialize(status) }
                }
            );
        }

        public Response SitesEdit(Dictionary<string, object> site)
        {
            if (!site.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!site.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            if (!site.ContainsKey("url"))
            {
                throw new ArgumentException("Parameter `url` is missing");
            }

            return Request.MakeRequest(
                $"/reference/sites/{site["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "site", new JavaScriptSerializer().Serialize(site) }
                }
            );
        }

        public Response StatusesEdit(Dictionary<string, object> status)
        {
            if (!status.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!status.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }
            
            if (!status.ContainsKey("ordering"))
            {
                throw new ArgumentException("Parameter `ordering` is missing");
            }

            if (!status.ContainsKey("group"))
            {
                throw new ArgumentException("Parameter `group` is missing");
            }

            return Request.MakeRequest(
                $"/reference/statuses/{status["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "status", new JavaScriptSerializer().Serialize(status) }
                }
            );
        }

        public Response StoresEdit(Dictionary<string, object> store)
        {
            if (!store.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `code` is missing");
            }

            if (!store.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `name` is missing");
            }

            List<string> types = new List<string>
            {
                "store-type-online",
                "store-type-retail",
                "store-type-supplier",
                "store-type-warehouse"
            };

            if (store.ContainsKey("type") && !types.Contains(store["type"].ToString()))
            {
                throw new ArgumentException("Parameter `type` should be equal to one of `store-type-online|store-type-retail|store-type-supplier|store-type-warehouse`");
            }

            return Request.MakeRequest(
                $"/reference/stores/{store["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "store", new JavaScriptSerializer().Serialize(store) }
                }
            );
        }
    }
}
