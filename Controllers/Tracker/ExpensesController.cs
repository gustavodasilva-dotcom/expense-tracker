using System.Net;
using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Controllers.Core;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Models.Core;
using MVCExpenseTracker.Services.Tracker.Interfaces;

namespace MVCExpenseTracker.Controllers.Tracker;

public class ExpensesController : BaseController
{
    private readonly ILogger<ExpensesController> _logger;
    private readonly IExpensesService _expensesService;

    public ExpensesController(ILogger<ExpensesController> logger,
                              IExpensesService expensesService)
    {
        _logger = logger;
        _expensesService = expensesService;
    }

    public IActionResult Overview()
    {
        return Authenticated();
    }

    public IActionResult Tracker()
    {
        return Authenticated();
    }

    public IActionResult _monthlyIncome()
    {
        return PartialView();
    }

    public IActionResult _addNewExpense()
    {
        return PartialView();
    }

    [HttpGet]
    public async Task<JsonResult> GetExpenseTypes()
    {
        _logger.LogInformation("Get expense types");

        var result = new JsonResponseModel();

        try
        {
            result.data = await _expensesService.GetExpenseTypesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Get error: {e.Message}");

            result.status_code = HttpStatusCode.InternalServerError;
            result.status_message = e.Message;
        }

        return Json(result);
    }

    [HttpGet]
    public async Task<JsonResult> GetExpenses()
    {
        _logger.LogInformation("Get expenses");

        var result = new JsonResponseModel();

        try
        {
            var userId = HttpContext.Session.GetString("UserId") ?? throw new Exception("No session found");
            result.data = await _expensesService.GetExpensesAsync(userId);
        }
        catch (Exception e)
        {
            _logger.LogError($"Get error: {e.Message}");

            result.status_code = HttpStatusCode.InternalServerError;
            result.status_message = e.Message;
        }

        return Json(result);
    }

    [HttpPost]
    public async Task<JsonResult> AddExpense(ExpenseModel data)
    {
        _logger.LogInformation("Add expense");

        var result = new JsonResponseModel();

        try
        {
            result.data = await _expensesService.AddExpenseAsync(data);
        }
        catch (Exception e)
        {
            _logger.LogError($"Get error: {e.Message}");

            result.status_code = HttpStatusCode.InternalServerError;
            result.status_message = e.Message;
        }

        return Json(result);
    }

    [HttpDelete]
    public async Task<JsonResult> DeleteExpense(string id)
    {
        _logger.LogInformation("Add expense");

        var result = new JsonResponseModel();

        try
        {
            await _expensesService.DeleteAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError($"Get error: {e.Message}");

            result.status_code = HttpStatusCode.InternalServerError;
            result.status_message = e.Message;
        }

        return Json(result);
    }
}