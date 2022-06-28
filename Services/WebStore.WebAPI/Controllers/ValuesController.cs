using Microsoft.AspNetCore.Mvc;

using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers;

[ApiController]
//[Route("api/[controller]")]
[Route(WebAPIAddresses.V1.Values)]
public class ValuesController : ControllerBase
{
    private const int __ValuesCount = 10;

    private static readonly Dictionary<int, string> __Values = Enumerable.Range(1, __ValuesCount)
       .Select(i => (Id: i, Value: $"Value-{i}"))
       .ToDictionary(v => v.Id, v => v.Value);

    private static int __LastFreeId = __ValuesCount + 1;

    private readonly ILogger<ValuesController> _Logger;

    public ValuesController(ILogger<ValuesController> Logger) => _Logger = Logger;

    [HttpGet]
    public IActionResult GetAll()
    {
        if (__Values.Count == 0)
            return NoContent();

        var values = __Values.Values;
        return Ok(values);
    }

    [HttpGet("{Id:int}")] // GET -> api/values/5
    public IActionResult GetById(int Id)
    {
        if(__Values.TryGetValue(Id, out var value))
            return Ok(value);

        return NotFound(new { Id });
    }

    [HttpPost] // POST -> api/values || Body:Value=qwe ;; POST -> api/values?Value=qwe
    [HttpPost("{Value}")] // POST -> api/values/qwe
    public IActionResult Add(/*[FromBody]*/string Value)
    {
        var id = __LastFreeId;
        __Values[id] = Value;

        _Logger.LogInformation("Значение {0} добавлено под id:{1}", Value, id);
        __LastFreeId++;

        return CreatedAtAction(nameof(GetById), new { Id = id }, Value);
    }

    [HttpPut("{Id:int}")]
    public IActionResult Edit(int Id, [FromBody] string Value)
    {
        if (!__Values.ContainsKey(Id))
        {
            _Logger.LogWarning("При попытке редактирования записи с id:{0} - запись не найдена", Id);
            return NotFound(new { Id });
        }

        var old_value = __Values[Id];
        __Values[Id] = Value;

        _Logger.LogInformation("Редактирвоание записи id:{0} (старое значение {1} - новое значение {2})",
            Id, old_value, Value);

        return Ok(new { Id, OldValue = old_value, NewValue = Value });
    }

    [HttpDelete("{Id:int}")]
    public IActionResult Delete(int Id)
    {
        if (!__Values.ContainsKey(Id))
        {
            _Logger.LogWarning("При попытке удаления записи с id:{0} - запись не найдена", Id);
            return NotFound(new { Id });
        }

        var value = __Values[Id];
        __Values.Remove(Id);

        _Logger.LogInformation("Удаление записи id:{0} (значение {1})", Id, value);

        return Ok(new { Id, Value = value });
    }
}
