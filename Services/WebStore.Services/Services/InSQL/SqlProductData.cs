using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL;

public class SqlProductData : IProductData
{
    private readonly WebStoreDB _db;

    public SqlProductData(WebStoreDB db) => _db = db;

    public IEnumerable<Section> GetSections() => _db.Sections.Include(s => s.Products)/*.AsEnumerable()*/;

    public Section? GetSectionById(int Id) => _db.Sections
       .Include(s => s.Products)
       .FirstOrDefault(s => s.Id == Id);

    public IEnumerable<Brand> GetBrands() => _db.Brands.Include(b => b.Products)/*.AsEnumerable()*/;

    public Brand? GetBrandById(int Id) => _db.Brands
       .Include(b => b.Products)
       .FirstOrDefault(b => b.Id == Id);

    public IEnumerable<Product> GetProducts(ProductFilter? Filter = null)
    {
        IQueryable<Product> query = _db.Products
           .Include(p => p.Section)
           .Include(p => p.Brand);

        if (Filter is { Ids: { Length: > 0 } ids })
            query = query.Where(p => ids.Contains(p.Id));
        else
        {
            if (Filter is { SectionId: { } section_id })
                query = query.Where(x => x.SectionId == section_id);

            if (Filter is { BrandId: { } brand_id })
                query = query.Where(x => x.BrandId == brand_id);
        }

        return query;
    }

    public Product? GetProductById(int Id) => _db.Products
       .Include(p => p.Section)
       .Include(p => p.Brand)
       .FirstOrDefault(p => p.Id == Id);
}