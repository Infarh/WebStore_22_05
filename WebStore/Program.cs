var builder = WebApplication.CreateBuilder(args);

// Конфигурирование перечня составных частей приложения
builder.Services.AddControllersWithViews();

var app = builder.Build();

//var greetings = app.Configuration["ServerGreetings"];
//app.MapGet("/", () => greetings);
app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

//app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
