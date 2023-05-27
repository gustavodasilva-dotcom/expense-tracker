using Microsoft.AspNetCore.Mvc;

namespace MVCExpenseTracker.Controllers.Core;

public class BaseController : Controller
{
    protected IActionResult Authenticated()
    {
        //if (HttpContext.Session.Keys.Any())
        //{
            return View();
        //}
        //return Redirect("~/Home/Index");
    }
}