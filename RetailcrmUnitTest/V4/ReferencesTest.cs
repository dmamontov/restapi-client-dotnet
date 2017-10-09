namespace RetailcrmUnitTest.V4
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Retailcrm;
    using Retailcrm.Versions.V4;

    [TestClass]
    public class ReferencesTest
    {
        private readonly Client _client;

        public ReferencesTest()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            _client = new Client(appSettings["apiUrl"], appSettings["apiKey"]);
        }

        [TestMethod]
        public void PriceTypes()
        {
            Response response = _client.PriceTypes();

            Assert.IsTrue(response.IsSuccessfull());
            Assert.IsTrue(response.GetStatusCode() == 200);
            Assert.IsInstanceOfType(response, typeof(Response));
            Assert.IsTrue(response.GetResponse().ContainsKey("priceTypes"));
        }
    }
}
