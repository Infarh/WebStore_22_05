using System.Diagnostics.CodeAnalysis;
using WebStore.Domain.Entities;

namespace WebStore.Domain.DTO;

public class ProductDTO
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public int Order { get; init; }

    public decimal Price { get; init; }

    public string ImageUrl { get; init; } = null!;

    public SectionDTO Section { get; init; } = null!;

    public BrandDTO? Brand { get; init; }
}

public class SectionDTO
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public int Order { get; init; }

    public int? ParentId { get; init; }

    public IEnumerable<int> ProductIds { get; init; } = null!;
}

public class BrandDTO
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public int Order { get; init; }

    public IEnumerable<int> ProductIds { get; init; } = null!;
}

public static class BrandDTOMapper
{
    [return: NotNullIfNotNull("brand")]
    public static BrandDTO? ToDTO(this Brand? brand) => brand is null
        ? null
        : new()
        {
            Id = brand.Id,
            Name = brand.Name,
            Order = brand.Order,
            ProductIds = brand.Products.Select(p => p.Id),
        };

    [return: NotNullIfNotNull("brand")]
    public static Brand? FromDTO(this BrandDTO? brand) => brand is null
        ? null
        : new()
        {
            Id = brand.Id,
            Name = brand.Name,
            Order = brand.Order,
            Products = brand.ProductIds.Select(id => new Product { Id = id }).ToArray(),
        };

    public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand>? brands) => brands?.Select(ToDTO)!;

    public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO>? brands) => brands?.Select(FromDTO)!;
}

public static class SectionDTOMapper
{
    [return: NotNullIfNotNull("section")]
    public static SectionDTO? ToDTO(this Section? section) => section is null
        ? null
        : new()
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
            ProductIds = section.Products.Select(p => p.Id),
        };

    [return: NotNullIfNotNull("section")]
    public static Section? FromDTO(this SectionDTO? section) => section is null
        ? null
        : new()
        {
            Id = section.Id,
            Name = section.Name,
            Order = section.Order,
            ParentId = section.ParentId,
            Products = section.ProductIds.Select(id => new Product { Id = id }).ToArray(),
        };

    public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section>? sections) => sections?.Select(ToDTO)!;

    public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO>? sections) => sections?.Select(FromDTO)!;
}

public static class ProductDTOMapper
{
    [return: NotNullIfNotNull("product")]
    public static ProductDTO? ToDTO(this Product? product) => product is null
        ? null
        : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.ToDTO(),
            Section = product.Section.ToDTO(),
        };

    [return: NotNullIfNotNull("product")]
    public static Product? FromDTO(this ProductDTO? product) => product is null
        ? null
        : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.FromDTO(),
            Section = product.Section.FromDTO(),
        };

    public static IEnumerable<ProductDTO> ToDTO(this IEnumerable<Product>? products) => products?.Select(ToDTO)!;

    public static IEnumerable<Product> FromDTO(this IEnumerable<ProductDTO>? products) => products?.Select(FromDTO)!;

    [return: NotNullIfNotNull("page")]
    public static Page<ProductDTO>? ToDTO(this Page<Product>? page) => page is null
        ? null
        : new(page.Items.ToDTO(), page.PageNumber, page.PageSize, page.TotalCount);

    [return: NotNullIfNotNull("page")]
    public static Page<Product>? FromDTO(this Page<ProductDTO>? page) => page is null
        ? null
        : new(page.Items.FromDTO(), page.PageNumber, page.PageSize, page.TotalCount);
}