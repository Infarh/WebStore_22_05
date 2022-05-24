using Microsoft.AspNetCore.Mvc;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index([FromServices] IProductData ProductData)
    {
        var products = ProductData.GetProducts()
           .OrderBy(p => p.Order)
           .Take(6)
           .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
            });

        ViewBag.Products = products;

        return View();
    }

    public IActionResult Greetings(string? id)
    {
        return Content($"Hello from first controller - {id}");
    }

    public IActionResult Contacts() => View();

    public IActionResult Error404() => View();
}
