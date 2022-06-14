using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _ProductData;
    private readonly IMapper _Mapper;

    public CatalogController(IProductData ProductData, IMapper Mapper)
    {
        _ProductData = ProductData;
        _Mapper = Mapper;
    }

    public IActionResult Index([Bind("SectionId,BrandId")] ProductFilter filter)
    {
        var products = _ProductData.GetProducts(filter);

        return View(new CatalogViewModel
        {
            BrandId = filter.BrandId,
            SectionId = filter.SectionId,
            Products = products.OrderBy(p => p.Order).Select(p => _Mapper.Map<ProductViewModel>(p))
            //Products = products.OrderBy(p => p.Order).ToView()!,
        });
    }

    public IActionResult Details(int Id)
    {
        var product = _ProductData.GetProductById(Id);
        if (product is null)
            return NotFound();

        return View(product.ToView());
    }
}
