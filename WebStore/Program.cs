using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
builder.Services.AddScoped<IEmployeesData, InMemoryEmployeesData>();        // самый универсальный
//builder.Services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

builder.Services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<TestMiddleware>();

app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

app.UseWelcomePage("/welcome");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
