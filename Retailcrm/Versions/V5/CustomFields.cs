namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response CustomFieldsList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/custom-fields", Request.MethodGet, parameters);
        }

        public Response CustomFieldsCreate(Dictionary<string, object> customField, string entity = "")
        {
            List<string> types = new List<string>
            {
                "boolean", "date", "dictionary", "email", "integer", "numeric", "string", "text"
            };

            if (customField.Count < 1)
            {
                throw new ArgumentException("Parameter `customField` must contains a data");
            }

            if (!customField.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `customField` should contain `code`");
            }

            if (!customField.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `customField` should contain `name`");
            }

            if (!customField.ContainsKey("type"))
            {
                throw new ArgumentException("Parameter `customField` should contain `type`");
            }

            if (!types.Contains(customField["type"].ToString()))
            {
                throw new ArgumentException(
                    "Parameter `customField` should contain `type` & value of type should be on of `boolean|date|dictionary|email|integer|numeric|string|text`"
                );
            }

            return Request.MakeRequest(
                $"/custom-fields/{entity}/create",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "customField", new JavaScriptSerializer().Serialize(customField) }
                }
            );
        }

        public Response CustomFieldsGet(string code, string entity)
        {
            return Request.MakeRequest(
                $"/custom-fields/{entity}/{code}",
                Request.MethodGet
            );
        }

        public Response CustomFieldsUpdate(Dictionary<string, object> customField, string entity = "")
        {
            if (customField.Count < 1)
            {
                throw new ArgumentException("Parameter `customField` must contains a data");
            }

            if (!customField.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `customField` should contain `code`");
            }

            if (!customField.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `customField` should contain `name`");
            }

            return Request.MakeRequest(
                $"/custom-fields/{entity}/{customField["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "customField", new JavaScriptSerializer().Serialize(customField) }
                }
            );
        }

        public Response CustomDictionaryList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/custom-fields/dictionaries", Request.MethodGet, parameters);
        }

        public Response CustomDictionaryCreate(Dictionary<string, object> customDictionary)
        {
            if (customDictionary.Count < 1)
            {
                throw new ArgumentException("Parameter `customDictionary` must contains a data");
            }

            if (!customDictionary.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `customDictionary` should contain `code`");
            }

            if (!customDictionary.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `customDictionary` should contain `name`");
            }

            return Request.MakeRequest(
                "/custom-fields/dictionaries/create",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "customDictionary", new JavaScriptSerializer().Serialize(customDictionary) }
                }
            );
        }

        public Response CustomDictionaryGet(string code)
        {
            return Request.MakeRequest(
                $"/custom-fields/dictionaries/{code}",
                Request.MethodGet
            );
        }

        public Response CustomDictionaryUpdate(Dictionary<string, object> customDictionary)
        {
            if (customDictionary.Count < 1)
            {
                throw new ArgumentException("Parameter `customDictionary` must contains a data");
            }

            if (!customDictionary.ContainsKey("code"))
            {
                throw new ArgumentException("Parameter `customDictionary` should contain `code`");
            }

            if (!customDictionary.ContainsKey("name"))
            {
                throw new ArgumentException("Parameter `customDictionary` should contain `name`");
            }

            return Request.MakeRequest(
                $"/custom-fields/dictionaries/{customDictionary["code"].ToString()}/edit",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "customDictionary", new JavaScriptSerializer().Serialize(customDictionary) }
                }
            );
        }
    }
}
