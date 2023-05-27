using System.Net;

namespace MVCExpenseTracker.Models.Core;

public class JsonResponseModel
{
    public HttpStatusCode status_code { get; set; } = HttpStatusCode.OK;
    public string status_message { get; set; } = "OK";
    public dynamic data { get; set; }
}