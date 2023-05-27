using System.Net;
using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Models.Core;
using MVCExpenseTracker.Services.Account.Interfaces;

namespace MVCExpenseTracker.Controllers.Account;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger,
                           IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            var user = new UserModel
            {
                Email = email,
                Password = password
            };

            await _accountService.SignUpAsync(user);
            return RedirectPermanent("~/Home/Index");
        }
        catch (Exception e)
        {
            ViewBag.message = e.Message;
        }

        return View();
    }

    public IActionResult _forgotMyPassword()
    {
        return PartialView();
    }
}