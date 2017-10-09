namespace RetailcrmUnitTest.V5
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Retailcrm;
    using Retailcrm.Versions.V5;

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
        public void CostGroups()
        {
            Response response = _client.CostGroups();
            Assert.IsTrue(response.IsSuccessfull());
            Assert.IsTrue(response.GetStatusCode() == 200);
            Assert.IsInstanceOfType(response, typeof(Response));
            Assert.IsTrue(response.GetResponse().ContainsKey("costGroups"));
        }
    }
}
