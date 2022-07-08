using Microsoft.AspNetCore.Mvc;

using WebStore.ViewModels;

namespace WebStore.Controllers;

public class AjaxTestController : Controller
{
    private readonly ILogger<AjaxTestController> _Logger;

    public AjaxTestController(ILogger<AjaxTestController> Logger) => _Logger = Logger;

    public IActionResult Index() => View();

    public async Task<IActionResult> GetJSON(int? id, string? msg, int Delay = 2000)
    {
        _Logger.LogInformation("Получен запрос к GetJSON - id:{0}, msg:{1}, Delay:{2}", id, msg, Delay);

        await Task.Delay(Delay);

        _Logger.LogInformation("Ответ на запрос к GetJSON - id:{0}, msg:{1}, Delay:{2}", id, msg, Delay);

        return Json(new
        {
            Message = $"Response (id:{id ?? -1}: {msg ?? "--null--"})",
            ServerTime = DateTime.Now,
        });
    }

    public async Task<IActionResult> GetHTML(int? id, string? msg, int Delay = 2000)
    {
        _Logger.LogInformation("Получен запрос к GetHTML - id:{0}, msg:{1}, Delay:{2}", id, msg, Delay);

        await Task.Delay(Delay);

        _Logger.LogInformation("Ответ на запрос к GetHTML - id:{0}, msg:{1}, Delay:{2}", id, msg, Delay);

        return PartialView("Partial/_DataView", new AjaxTestDataViewModel
        {
            Id = id ?? -1,
            Message = msg,
        });
    }

    public IActionResult Chat() => View();
}
