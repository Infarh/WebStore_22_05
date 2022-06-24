namespace WebStore.WebAPI.Infrastructure.Middleware;

public class ExceptionHandler
{
    private readonly RequestDelegate _Next;
    private readonly ILogger<ExceptionHandler> _Logger;

    public ExceptionHandler(RequestDelegate Next, ILogger<ExceptionHandler> Logger)
    {
        _Next = Next;
        _Logger = Logger;
    }

    public async Task Invoke(HttpContext Context)
    {
        try
        {
            await _Next(Context);
        }
        catch (Exception error)
        {
            HandleException(Context, error);
            throw;
        }
    }

    private void HandleException(HttpContext Context, Exception Error)
    {
        _Logger.LogError(Error, "Ошибка в процессе обработки запроса к {0}", Context.Request.Path);
    }
}
