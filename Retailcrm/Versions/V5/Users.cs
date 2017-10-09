namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;

    public partial class Client
    {
        public Response UsersStatus(int id, string status)
        {
            List<string> statuses = new List<string> { "free", "busy", "dinner", "break"};

            if (!statuses.Contains(status))
            {
                throw new ArgumentException("Parameter `status` must be equal one of these values: `free|busy|dinner|break`");
            }

            return Request.MakeRequest(
                $"/users/{id}/status",
                Request.MethodPost,
                new Dictionary<string, object>
                {
                    { "status", status }
                }
            );
        }
    }
}
