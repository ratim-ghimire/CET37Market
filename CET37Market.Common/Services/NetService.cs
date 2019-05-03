namespace CET37Market.Common.Services
{
    using CET37Market.Common.Models;
    using Xamarin.Essentials;
    //install Xamarin Essestials for validation of internet
    public class NetService
    {
        public Response CheckConnection()
        {
            var connection = Connectivity.NetworkAccess;

            if (connection == NetworkAccess.Internet)
            {
                return new Response
                {
                    Success = true,
                    Message = "Ok"
                };
            }
            else
            {
                return new Response
                {
                    Success = false,
                    Message = "Internet Connection unavailable, Please check the Internet connection",
                };
            }
        }
    }
}
