using System.Net;
using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Models.Core;
using MVCExpenseTracker.Services.Account.Interfaces;

namespace MVCExpenseTracker.Controllers.Account;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IAccountService _accountService;

    public LoginController(ILogger<LoginController> logger,
                           IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult _forgotMyPassword()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<JsonResult> SignUp(UserModel data)
    {
        var result = new JsonResponseModel();

        try
        {
            await _accountService.SignUpAsync(data);
        }
        catch (Exception e)
        {
            result.status_code = HttpStatusCode.InternalServerError;
            result.status_message = e.Message;
        }

        return Json(result);
    }
}