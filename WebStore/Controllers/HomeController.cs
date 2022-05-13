using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Content("Index action");
        //return new ViewResult();
    }

    public IActionResult Greetings(string? id)
    {
        return Content($"Hello from first controller - {id}");
    }
}
