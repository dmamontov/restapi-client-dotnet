namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public partial class Client
    {
        public Response NotesCreate(Dictionary<string, object> note, string site = "")
        {
            if (note.Count < 1)
            {
                throw new ArgumentException("Parameter `note` must contains a data");
            }

            return Request.MakeRequest(
                "/customers/notes/create",
                Request.MethodPost,
                FillSite(
                    site,
                    new Dictionary<string, object>
                    {
                        { "note", new JavaScriptSerializer().Serialize(note) }
                    }
                )
            );
        }

        public Response NotesDelete(string id)
        {
            return Request.MakeRequest(
                $"/customers/notes/{id}/delete",
                Request.MethodPost
            );
        }

        public Response NotesList(Dictionary<string, object> filter = null, int page = 0, int limit = 0)
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

            return Request.MakeRequest("/customers/notes", Request.MethodGet, parameters);
        }
    }
}
