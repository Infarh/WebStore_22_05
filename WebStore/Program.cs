var builder = WebApplication.CreateBuilder(args);

// Конфигурирование перечня составных частей приложения

var app = builder.Build();

var greetings = app.Configuration["ServerGreetings"];
app.MapGet("/", () => greetings);

app.Run();
