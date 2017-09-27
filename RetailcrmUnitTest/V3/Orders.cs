namespace RetailcrmUnitTest.V3
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Retailcrm;
    using Retailcrm.Versions.V3;

    [TestClass]
    public class Orders
    {
        private readonly Client _client;

        public Orders()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            _client = new Client(appSettings["apiUrl"], appSettings["apiKey"]);
        }

        [TestMethod]
        public void OrdersCreateReadUpdate()
        {
            Dictionary<string, object> order = new Dictionary<string, object>();

            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
            
            order.Add("number", unixTime);
            order.Add("createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            order.Add("lastName", "Doe");
            order.Add("firstName", "John");
            order.Add("email", "john@example.com");
            order.Add("phone", "+79999999999");

            Debug.WriteLine("Running order create, get & update");

            Response createResponse = _client.OrdersCreate(order);
            Assert.IsTrue(createResponse.IsSuccessfull());
            Assert.IsInstanceOfType(createResponse, typeof(Response));
            Assert.IsTrue(createResponse.GetResponse().ContainsKey("id"));

            string id = createResponse.GetResponse()["id"].ToString();

            Response getResponse = _client.OrdersGet(id, "id");
            Assert.IsTrue(getResponse.IsSuccessfull());
            Assert.IsInstanceOfType(getResponse, typeof(Response));
            Assert.IsTrue(getResponse.GetResponse().ContainsKey("order"));

            Dictionary<string, object> update = new Dictionary<string, object>
            {
                {"id", id},
                {"status", "cancel-other"}
            };

            Response updateResponse = _client.OrdersUpdate(update, "id");
            Assert.IsTrue(updateResponse.IsSuccessfull());
            Assert.IsInstanceOfType(updateResponse, typeof(Response));
            Assert.IsTrue(updateResponse.GetStatusCode() == 200);
        }

        [TestMethod]
        public void OrdersFixExternalId()
        {
            Dictionary<string, object> order = new Dictionary<string, object>();

            long epochTicks = new DateTime(1970, 1, 1).Ticks;
            long unixTime = ((DateTime.UtcNow.Ticks - epochTicks) / TimeSpan.TicksPerSecond);

            order.Add("number", unixTime);
            order.Add("createdAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            order.Add("lastName", "Doe");
            order.Add("firstName", "John");
            order.Add("email", "john@example.com");
            order.Add("phone", "+79999999999");

            Debug.WriteLine("Running orders fix external ids");
            Response createResponse = _client.OrdersCreate(order);

            string id = createResponse.GetResponse()["id"].ToString();
            string externalId = $"{unixTime}ID";

            Dictionary<string, object>[] fix =
            {
                new Dictionary<string, object>
                {
                    { "id", id },
                    { "externalId", externalId }
                }
            };

            Assert.IsTrue(createResponse.IsSuccessfull());
            Assert.IsInstanceOfType(createResponse, typeof(Response));
            Assert.IsTrue(createResponse.GetResponse().ContainsKey("id"));

            Response fixResponse = _client.OrdersFixExternalIds(fix);
            Assert.IsTrue(fixResponse.IsSuccessfull());
            Assert.IsInstanceOfType(fixResponse, typeof(Response));
        }

        [TestMethod]
        public void OrdersList()
        {
            Debug.WriteLine("Running orders list");
            Response response = _client.OrdersList();
            
            Assert.IsTrue(response.IsSuccessfull());
            Assert.IsTrue(response.GetStatusCode() == 200 || response.GetStatusCode() == 201);
            Assert.IsInstanceOfType(response, typeof(Response));
            Assert.IsTrue(response.GetResponse().ContainsKey("orders"));
        }
    }
}
