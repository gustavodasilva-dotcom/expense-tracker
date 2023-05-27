using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Services.Account.Interfaces;

namespace MVCExpenseTracker.Controllers;

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
        _logger.LogDebug($"Login request: {email}");

        try
        {
            var user = new UserModel
            {
                Email = email,
                Password = password
            };

            var userAuthenticated = await _accountService.SignUpAsync(user);

            HttpContext.Session.SetString("UserEmail", userAuthenticated.Email);

            return RedirectPermanent("~/Expenses/Overview");
        }
        catch (Exception e)
        {
            _logger.LogDebug($"Login error: {e.Message}");
            
            ViewBag.message = e.Message;
        }

        return View();
    }

    public IActionResult _forgotMyPassword()
    {
        return PartialView();
    }
}