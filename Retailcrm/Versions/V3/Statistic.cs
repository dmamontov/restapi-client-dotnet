namespace Retailcrm.Versions.V3
{
    public partial class Client
    {
        public Response StatisticUpdate()
        {
            return _request.MakeRequest(
                "/statistic/update",
                Request.MethodGet
            );
        }
    }
}
