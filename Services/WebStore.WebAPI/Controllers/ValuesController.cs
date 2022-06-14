using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebAPI.Controllers;

[ApiController]
//[Route("api/[controller]")]
[Route("api/values")]
public class ValuesController : ControllerBase
{
    private static readonly Dictionary<int, string> __Values = Enumerable.Range(1, 10)
       .Select(i => (Id: i, Value: $"Value-{i}"))
       .ToDictionary(v => v.Id, v => v.Value);
}
