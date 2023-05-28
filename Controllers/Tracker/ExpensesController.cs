using Microsoft.AspNetCore.Mvc;
using MVCExpenseTracker.Controllers.Core;
using MVCExpenseTracker.Services.Tracker.Interfaces;

namespace MVCExpenseTracker.Controllers.Tracker;

public class ExpensesController : BaseController
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService)
    {
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
}