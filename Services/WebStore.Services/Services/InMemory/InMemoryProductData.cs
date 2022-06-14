using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InMemory;

[Obsolete("Используйте SqlProductData")]
public class InMemoryProductData : IProductData
{
    public IEnumerable<Section> GetSections() => TestData.Sections;
    public Section? GetSectionById(int Id) { throw new NotImplementedException(); }

    public IEnumerable<Brand> GetBrands() => TestData.Brands;
    public Brand? GetBrandById(int Id) { throw new NotImplementedException(); }

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        IEnumerable<Product> query = TestData.Products;

        //if(Filter != null && Filter.SectionId != null)
        //    query = query.Where(x => x.SectionId == Filter.SectionId);

        if (Filter is { SectionId: { } section_id })
            query = query.Where(x => x.SectionId == section_id);

        if (Filter is { BrandId: { } brand_id })
            query = query.Where(x => x.BrandId == brand_id);

        return query;
    }

    public Product? GetProductById(int Id) { throw new NotImplementedException(); }
}
