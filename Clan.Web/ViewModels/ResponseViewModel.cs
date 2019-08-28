using System.Net;

namespace Clan.Web.ViewModels
{
    public class ResponseViewModel : BaseViewModel
    {
        public ResponseViewModel(HttpStatusCode statusCode)
        {
            Status = (int)statusCode;

            if (Status >= 200 && Status < 299)
            {
                Success = true;
            }
        }

        public bool Success { get; set; }
        public int Status { get; private set; }
        public string Redirect { get; set; }
    }
}