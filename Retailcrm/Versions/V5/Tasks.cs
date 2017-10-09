namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response TasksCreate(Dictionary<string, object> task, string site = "")
        {
            if (task.Count < 1)
            {
                throw new ArgumentException("Parameter `task` must contains a data");
            }

            return Request.MakeRequest(
                "/tasks/create",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "task", new JavaScriptSerializer().Serialize(task) }
                    }
                )
            );
        }

        public Response TasksUpdate(Dictionary<string, object> task, string site = "")
        {
            if (task.Count < 1)
            {
                throw new ArgumentException("Parameter `task` must contains a data");
            }

            if (!task.ContainsKey("id"))
            {
                throw new ArgumentException("Parameter `task` must contains an id");
            }

            return Request.MakeRequest(
                $"/tasks/{task["id"].ToString()}/edit",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "task", new JavaScriptSerializer().Serialize(task) }
                    }
                )
            );
        }

        public Response TasksGet(string id)
        {
            return Request.MakeRequest(
                $"/tasks/{id}",
                Request.MethodGet
            );
        }

        public Response TasksList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/tasks", Request.MethodGet, parameters);
        }
    }
}
