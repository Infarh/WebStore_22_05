using System.Net;
using System.Net.Http.Json;

using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Products;

public class ProductsClient : BaseClient, IProductData
{
    public ProductsClient(HttpClient Client) : base(Client, WebAPIAddresses.V1.Products) { }

    public IEnumerable<Section> GetSections()
    {
        var result = Get<IEnumerable<SectionDTO>>($"{Address}/sections");
        return result.FromDTO();
    }

    public Section? GetSectionById(int Id)
    {
        var result = Get<SectionDTO>($"{Address}/sections/{Id}");
        return result.FromDTO();
    }

    public IEnumerable<Brand> GetBrands()
    {
        var result = Get<IEnumerable<BrandDTO>>($"{Address}/brands");
        return result.FromDTO();
    }

    public Brand? GetBrandById(int Id)
    {
        var result = Get<BrandDTO>($"{Address}/brands/{Id}");
        return result.FromDTO();
    }

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        var response = Post(Address, Filter ?? new());

        if (response.StatusCode == HttpStatusCode.NoContent)
            return Enumerable.Empty<Product>();

        var result = response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<IEnumerable<ProductDTO>>()
           .Result;

        return result.FromDTO();
    }

    public Product? GetProductById(int Id)
    {
        var result = Get<ProductDTO>($"{Address}/{Id}");
        return result.FromDTO();
    }
}
