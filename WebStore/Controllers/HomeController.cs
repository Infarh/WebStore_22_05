using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    public IActionResult Greetings(string? id)
    {
        return Content($"Hello from first controller - {id}");
    }

    public IActionResult Contacts() => View();

    public IActionResult Error404() => View();
}
