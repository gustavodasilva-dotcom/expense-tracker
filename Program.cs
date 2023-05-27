using MVCExpenseTracker.Database;
using MVCExpenseTracker.Database.Interfaces;
using MVCExpenseTracker.Database.Seeders;
using MVCExpenseTracker.Services.Account;
using MVCExpenseTracker.Services.Tracker;
using MVCExpenseTracker.Services.Account.Interfaces;
using MVCExpenseTracker.Services.Tracker.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IMongoDbConnection, MongoDbConnection>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IExpensesService, ExpensesService>(); 

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("MongoDb");
var database = builder.Configuration["MongoDB:Database"];
Seeders.Run(connectionString, database);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
