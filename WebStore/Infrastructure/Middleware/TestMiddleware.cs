namespace WebStore.Infrastructure.Middleware;

public class TestMiddleware
{
    private readonly RequestDelegate _Next;

    public TestMiddleware(RequestDelegate Next) => _Next = Next;

    public async Task Invoke(HttpContext Context)
    {
        // обработка данных внутри Context
        //Context.Request
        //Context.Response.

        await _Next(Context);

        // постобработка данных из Context.Response
    }
}
