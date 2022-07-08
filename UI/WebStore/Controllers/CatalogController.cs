using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private const string __CatalogPageSize = "CatalogPageSize";

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
        filter.PageSize ??= int.TryParse(_Configuration[__CatalogPageSize], out var page_size) ? page_size : null;

        var products = _ProductData.GetProducts(filter);

        return View(new CatalogViewModel
        {
            BrandId = filter.BrandId,
            SectionId = filter.SectionId,
            Products = products
               .Items
               .OrderBy(p => p.Order)
               .Select(p => _Mapper.Map<ProductViewModel>(p)),
            //Products = products.OrderBy(p => p.Order).ToView()!,
            PageModel = new()
            {
                Page = filter.PageNumber,
                PageSize = filter.PageSize ?? 0,
                TotalPages = products.PageCount,
            }
        });
    }

    public IActionResult Details(int Id)
    {
        var product = _ProductData.GetProductById(Id);
        if (product is null)
            return NotFound();

        return View(product.ToView());
    }

    public IActionResult GetProductsAPI([Bind("SectionId,BrandId,PageNumber,PageSize")] ProductFilter filter)
    {
        filter.PageSize ??= _Configuration.GetValue(__CatalogPageSize, 6);

        var products = _ProductData.GetProducts(filter);
        return PartialView("Partial/_Products", products.Items.Select(p => _Mapper.Map<ProductViewModel>(p)));
    }
}
