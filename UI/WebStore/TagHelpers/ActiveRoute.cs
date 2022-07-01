using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers;

[HtmlTargetElement(Attributes = AttributeName)]
public class ActiveRoute : TagHelper
{
    private const string AttributeName = "is-active-route";

    [HtmlAttributeName("asp-controller")]
    public string Controller { get; set; } = null!;

    [HtmlAttributeName("asp-action")]
    public string Action { get; set; } = null!;

    [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
    public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    [ViewContext, HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll(AttributeName);
    }
}
