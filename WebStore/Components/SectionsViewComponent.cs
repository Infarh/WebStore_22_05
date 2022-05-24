using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components;

//[ViewComponent(Name = "qwe")]
public class SectionsViewComponent : ViewComponent
{
    //public async Task<IViewComponentResult> InvokeAsync() => View();
    public IViewComponentResult Invoke() => View();
}
