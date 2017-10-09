namespace Retailcrm.Versions.V5
{
    using System;
    using System.Collections.Generic;

    public partial class Client
    {
        public new Response DeliverySettingGet(string code)
        {
            throw new ArgumentException("This method is unavailable in API V5", code);
        }

        public new Response DeliverySettingsEdit(Dictionary<string, object> configuration)
        {
            throw new ArgumentException("This method is unavailable in API V5");
        }
    }
}
