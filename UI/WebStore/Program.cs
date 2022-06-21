using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Services.Identity;
using WebStore.Interfaces.TestAPI;
using WebStore.Services.Data;
using WebStore.Services.Services.InCookies;
using WebStore.WebAPI.Clients.Employees;
using WebStore.WebAPI.Clients.Identity;
using WebStore.WebAPI.Clients.Orders;
using WebStore.WebAPI.Clients.Products;
using WebStore.WebAPI.Clients.Values;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;

services.AddIdentity<User, Role>()
   .AddDefaultTokenProviders();

services.AddHttpClient("WebStoreAPIIdentity", client => client.BaseAddress = new(config["WebAPI"]))
   .AddTypedClient<IUsersClient, UsersClient>()
   .AddTypedClient<IUserStore<User>, UsersClient>()
   .AddTypedClient<IUserRoleStore<User>, UsersClient>()
   .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
   .AddTypedClient<IUserEmailStore<User>, UsersClient>()
   .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
   .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
   .AddTypedClient<IUserClaimStore<User>, UsersClient>()
   .AddTypedClient<IUserLoginStore<User>, UsersClient>()
   .AddTypedClient<IRolesClient, RolesClient>()
   .AddTypedClient<IRoleStore<Role>, RolesClient>();

services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "GB.WebStore";
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromDays(10);

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

services.AddHttpClient("WebStoreApi", client => client.BaseAddress = new(config["WebAPI"]))
   .AddTypedClient<IValuesService, ValuesClient>()
   .AddTypedClient<IEmployeesData, EmployeesClient>()
   .AddTypedClient<IProductData, ProductsClient>()
   .AddTypedClient<IOrderService, OrdersClient>();

services.AddScoped<ICartService, InCookiesCartService>();

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
    opt.Conventions.Add(new AddAreaToControllerConversation());
});

services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TestMiddleware>();

app.MapGet("/greetings", () => app.Configuration["ServerGreetings"]);

app.UseWelcomePage("/welcome");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
