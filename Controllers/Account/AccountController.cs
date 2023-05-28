using System.Net;
using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Models.Core;
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
        _logger.LogInformation($"Login request: {email}");

        try
        {
            var user = new UserModel
            {
                email = email,
                password = password
            };

            var userAuthenticated = await _accountService.SignUpAsync(user);

            HttpContext.Session.SetString("UserId", userAuthenticated.id);
            HttpContext.Session.SetString("UserEmail", userAuthenticated.email);

            return RedirectPermanent("~/Expenses/Overview");
        }
        catch (Exception e)
        {
            _logger.LogError($"Login error: {e.Message}");

            ViewBag.message = e.Message;
        }

        return View();
    }

    [HttpGet]
    public async Task<JsonResult> Get()
    {
        _logger.LogInformation("Get session user");

        var retorno = new JsonResponseModel();

        try
        {
            var userId = HttpContext.Session.GetString("UserId") ?? throw new Exception("No session found");
            retorno.data = await _accountService.GetAsync(userId);

            _logger.LogInformation($"User {userId} found");
        }
        catch (Exception e)
        {
            _logger.LogError($"Get error: {e.Message}");

            retorno.status_code = HttpStatusCode.InternalServerError;
            retorno.status_message = e.Message;
        }

        return Json(retorno);
    }

    [HttpPut]
    public async Task<JsonResult> Update(UserModel model)
    {
        _logger.LogInformation("Update user");

        var retorno = new JsonResponseModel();

        try
        {
            retorno.data = await _accountService.UpdateAsync(model);

            _logger.LogInformation("Update successful");
        }
        catch (Exception e)
        {
            _logger.LogError($"Update error: {e.Message}");

            retorno.status_code = HttpStatusCode.InternalServerError;
            retorno.status_message = e.Message;
        }

        return Json(retorno);
    }

    public IActionResult _forgotMyPassword()
    {
        return PartialView();
    }
}