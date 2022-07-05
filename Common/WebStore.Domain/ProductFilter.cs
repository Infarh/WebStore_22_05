namespace WebStore.Domain;

public class ProductFilter
{
    public int? SectionId { get; set; }

    public int? BrandId { get; set; }

    public int[]? Ids { get; set; }

    public int PageNumber { get; set; }

    public int? PageSize { get; set; }
}
