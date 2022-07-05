namespace WebStore.Domain.ViewModels;

public class CatalogViewModel
{
    public int? SectionId { get; init; }

    public int? BrandId { get; init; }

    public IEnumerable<ProductViewModel> Products { get; init; } = Enumerable.Empty<ProductViewModel>();

    public PageViewModel PageModel { get; init; } = null!;
}