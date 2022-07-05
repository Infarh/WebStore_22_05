using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers.Api;

[ApiController]
[Route("/sitemap")]
public class SiteMapApiController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromServices] IProductData ProductData)
    {
        var nodes = new List<SitemapNode>
        {
            new(Url.Action("Index", "Home")),
            new(Url.Action("Greetings", "Home")),
            new(Url.Action("Contacts", "Home")),
            new(Url.Action("Test", "Home")),
            new(Url.Action("Index", "WebAPI")),
            new(Url.Action("Index", "Catalog")),
        };

        nodes.AddRange(ProductData.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = s.Id }))));

        foreach (var brand in ProductData.GetBrands())
            nodes.Add(new(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));

        foreach (var product in ProductData.GetProducts().Items)
            nodes.Add(new(Url.Action("Details", "Catalog", new { product.Id })));

        return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
    }
}
