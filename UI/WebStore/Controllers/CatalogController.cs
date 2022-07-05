using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _ProductData;
    private readonly IMapper _Mapper;
    private readonly IConfiguration _Configuration;

    public CatalogController(IProductData ProductData, IMapper Mapper, IConfiguration Configuration)
    {
        _ProductData = ProductData;
        _Mapper = Mapper;
        _Configuration = Configuration;
    }

    public IActionResult Index([Bind("SectionId,BrandId,PageNumber,PageSize")] ProductFilter filter)
    {
        filter.PageSize ??= int.TryParse(_Configuration["CatalogPageSize"], out var page_size) ? page_size : null;

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
