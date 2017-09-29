namespace Retailcrm
{
    using System.Collections.Generic;

    public class Connection
    {
        private readonly Request _request;
        
        public Connection(string url, string key)
        {
            if ("/" != url.Substring(url.Length - 1, 1))
            {
                url += "/";
            }

            url += "api/";

            _request = new Request(url, new Dictionary<string, object> { { "apiKey", key } });
        }

        public Response Versions()
        {
            return _request.MakeRequest(
                "api-versions",
                Request.MethodGet
            );
        }

        public Response Credentials()
        {
            return _request.MakeRequest(
                "credentials",
                Request.MethodGet
            );
        }
    }
}
