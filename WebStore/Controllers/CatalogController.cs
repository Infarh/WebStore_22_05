using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _ProductData;

    public CatalogController(IProductData ProductData) => _ProductData = ProductData;

    public IActionResult Index([Bind("SectionId,BrandId")] ProductFilter filter)
    {
        var products = _ProductData.GetProducts(filter);

        return View(new CatalogViewModel
        {
            BrandId = filter.BrandId,
            SectionId = filter.SectionId,
            Products = products
               .OrderBy(p => p.Order)
               .Select(p => new ProductViewModel
                {
                   Id = p.Id,
                   Name = p.Name,
                   Price = p.Price,
                   ImageUrl = p.ImageUrl,
                }),
        });
    }
}
