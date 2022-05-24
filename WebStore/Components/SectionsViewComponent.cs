using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components;

//[ViewComponent(Name = "qwe")]
public class SectionsViewComponent : ViewComponent
{
    private readonly IProductData _ProductData;

    public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;

    //public async Task<IViewComponentResult> InvokeAsync() => View();
    public IViewComponentResult Invoke()
    {
        var sections = _ProductData.GetSections();

        var parent_sections = sections.Where(s => s.ParentId is null).OrderBy(s => s.Order);

        var parent_sections_views = parent_sections
           .Select(s => new SectionViewModel
            {
                Id = s.Id,
                Name = s.Name,
            })
           .ToArray();

        foreach (var parent_section in parent_sections_views)
        {
            var childs = sections.Where(s => s.ParentId == parent_section.Id);
            foreach (var child_section in childs.OrderBy(s => s.Order))
                parent_section.ChildSections.Add(new()
                {
                    Id = child_section.Id,
                    Name = child_section.Name,
                });
        }


        return View(parent_sections_views);
    }
}
