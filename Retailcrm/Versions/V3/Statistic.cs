namespace Retailcrm.Versions.V3
{
    public partial class Client
    {
        public Response StatisticUpdate()
        {
            return Request.MakeRequest(
                "/statistic/update",
                Request.MethodGet
            );
        }
    }
}
