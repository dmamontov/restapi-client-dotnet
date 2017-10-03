using System.Globalization;

namespace RetailcrmUnitTest.V3
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Retailcrm;
    using Retailcrm.Versions.V3;

    [TestClass]
    public class PacksTest
    {
        private readonly Client _client;
        private readonly NameValueCollection _appSettings;

        public PacksTest()
        {
            _appSettings = ConfigurationManager.AppSettings;
            _client = new Client(_appSettings["apiUrl"], _appSettings["apiKey"], _appSettings["site"]);
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PacksCreateUpdateReadDelete()
        {
            string uid = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 12);

            Dictionary<string, object> order = new Dictionary<string, object>
            {
                { "number", $"packs-test-{uid}" },
                { "firstName", $"John_{uid}" },
                { "lastName", $"Doe_{uid}"},
                { "email", $"{uid}@example.com"},
                {
                    "items", new List<object>
                    {
                        new Dictionary<string, object>
                        {
                            { "initialPrice", 500 },
                            { "quantity", 2},
                            { "productId", "_jAjMfjjgs6ukFxOiTE433"},
                            { "productName", "Test"}
                        }
                    }
                }
            };

            Response orderCreateResponse = _client.OrdersCreate(order);

            Assert.IsTrue(orderCreateResponse.IsSuccessfull());
            Assert.IsTrue(orderCreateResponse.GetStatusCode() == 201);
            Assert.IsInstanceOfType(orderCreateResponse, typeof(Response));
            Assert.IsTrue(orderCreateResponse.GetResponse().ContainsKey("id"));

            Response orderGetResponse = _client.OrdersGet(orderCreateResponse.GetResponse()["id"].ToString(), "id");

            Assert.IsTrue(orderGetResponse.IsSuccessfull());
            Assert.IsTrue(orderGetResponse.GetStatusCode() == 200);
            Assert.IsInstanceOfType(orderGetResponse, typeof(Response));
            Assert.IsTrue(orderGetResponse.GetResponse().ContainsKey("order"));

            Dictionary<string, object> orderFromResponse =
                (Dictionary<string, object>) orderGetResponse.GetResponse()["order"];

            object[] arr = (object[])orderFromResponse["items"];
            int[] id = new int[1];
            

            foreach (Dictionary<string, object> s in arr.OfType<Dictionary<string, object>>())
            {
                int itemId;
                int.TryParse(s["id"].ToString(), NumberStyles.Any, null, out itemId);
                id[0] = itemId;
            }

            Dictionary<string, object> pack = new Dictionary<string, object>
            {
                { "purchasePrice", 100 },
                { "quantity", 1},
                { "store", _appSettings["store"]},
                { "itemId", id[0]}
            };

            Response packsCreateResponse = _client.PacksCreate(pack);

            Assert.IsTrue(packsCreateResponse.IsSuccessfull());
            Assert.IsTrue(packsCreateResponse.GetStatusCode() == 201);
            Assert.IsInstanceOfType(packsCreateResponse, typeof(Response));
            Assert.IsTrue(packsCreateResponse.GetResponse().ContainsKey("id"));

            string packId = packsCreateResponse.GetResponse()["id"].ToString();

            Dictionary<string, object> packEdit = new Dictionary<string, object>
            {
                { "id", packId },
                { "quantity", 2}
            };

            Response packsUpdateResponse = _client.PacksUpdate(packEdit);

            Assert.IsTrue(packsUpdateResponse.IsSuccessfull());
            Assert.IsTrue(packsUpdateResponse.GetStatusCode() == 200);
            Assert.IsInstanceOfType(packsUpdateResponse, typeof(Response));
            Assert.IsTrue(packsUpdateResponse.GetResponse().ContainsKey("id"));

            Response packsGetResponse = _client.PacksGet(packId);

            Assert.IsTrue(packsGetResponse.IsSuccessfull());
            Assert.IsTrue(packsGetResponse.GetStatusCode() == 200);
            Assert.IsInstanceOfType(packsGetResponse, typeof(Response));
            Assert.IsTrue(packsGetResponse.GetResponse().ContainsKey("pack"));

            Response packsDeleteResponse = _client.PacksGet(packId);

            Assert.IsTrue(packsDeleteResponse.IsSuccessfull());
            Assert.IsTrue(packsDeleteResponse.GetStatusCode() == 200);
            Assert.IsInstanceOfType(packsDeleteResponse, typeof(Response));

        }

        [TestMethod]
        public void PacksList()
        {
            Dictionary<string, object> filter = new Dictionary<string, object>
            {
                { "store", _appSettings["store"]}
            };

            Response response = _client.PacksList(filter, 1, 100);

            Assert.IsTrue(response.IsSuccessfull());
            Assert.IsTrue(response.GetStatusCode() == 200);
            Assert.IsInstanceOfType(response, typeof(Response));
            Assert.IsTrue(response.GetResponse().ContainsKey("packs"));
        }

        [TestMethod]
        public void PacksHistory()
        {
            Dictionary<string, object> filter = new Dictionary<string, object>
            {
                { "endDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
            };

            Response response = _client.PacksHistory(filter, 1, 100);

            Assert.IsTrue(response.IsSuccessfull());
            Assert.IsTrue(response.GetStatusCode() == 200);
            Assert.IsInstanceOfType(response, typeof(Response));
            Assert.IsTrue(response.GetResponse().ContainsKey("history"));
        }
    }
}
