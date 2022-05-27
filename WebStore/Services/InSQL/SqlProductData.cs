using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL;

public class SqlProductData : IProductData
{
    private readonly WebStoreDB _db;

    public SqlProductData(WebStoreDB db) => _db = db;

    public IEnumerable<Section> GetSections() => _db.Sections/*.AsEnumerable()*/;

    public IEnumerable<Brand> GetBrands() => _db.Brands/*.AsEnumerable()*/;

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        IQueryable<Product> query = _db.Products;

        if (Filter is { SectionId: { } section_id })
            query = query.Where(x => x.SectionId == section_id);

        if (Filter is { BrandId: { } brand_id })
            query = query.Where(x => x.BrandId == brand_id);

        return query;
    }
}