using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _Logger;

    public HomeController(ILogger<HomeController> Logger) => _Logger = Logger;

    public IActionResult Index([FromServices] IProductData ProductData)
    {
        var products = ProductData.GetProducts().OrderBy(p => p.Order).Take(6).ToView();

        ViewBag.Products = products;

        return View();
    }

    public IActionResult Greetings(string? id)
    {
        return Content($"Hello from first controller - {id}");
    }

    public IActionResult Contacts() => View();

    public IActionResult Error404() => View();

    public async Task<IActionResult> Test(int? Id, CancellationToken Cancel)
    {
        try
        {
            _Logger.LogInformation("Операция Test запущена");
            await Task.Delay(10000, Cancel);

            _Logger.LogInformation("Операция Test завершена");
            return RedirectToAction(nameof(Index));
        }
        catch (OperationCanceledException e) when (e.CancellationToken == Cancel)
        {
            _Logger.LogInformation("Операция Test отменена");
            throw;
        }
    }
}
