namespace Retailcrm
{
    using System;
    using System.Collections.Generic;

    public class Response
    {
        private readonly int _statusCode;
        private readonly string _rawResponse;
        private readonly Dictionary<string, object> _responseData;

        public Response(int statusCode, string responseBody = null)
        {
            _statusCode = statusCode;

            if (string.IsNullOrEmpty(responseBody))
            {
                throw new ArgumentException("Response body is empty");
            }

            _rawResponse = responseBody;

            var jsSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            _responseData = (Dictionary<string, object>)jsSerializer.DeserializeObject(responseBody);
        }

        public int GetStatusCode()
        {
            return _statusCode;
        }

        public Dictionary<string, object> GetResponse()
        {
            return _responseData;
        }

        public string GetRawResponse()
        {
            return _rawResponse;
        }

        public bool IsSuccessfull()
        {
            return _statusCode < 400;
        }

    }
}