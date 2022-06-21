using System.Net;
using System.Net.Http.Json;

namespace WebStore.WebAPI.Clients.Base;

public abstract class BaseClient : IDisposable
{
    protected HttpClient Http { get; }
    protected string Address { get; }

    protected BaseClient(HttpClient Client, string Address)
    {
        Http = Client;
        this.Address = Address;
    }

    protected T? Get<T>(string url) => GetAsync<T>(url).Result;

    protected async Task<T?> GetAsync<T>(string url, CancellationToken Cancel = default)
    {
        var response = await Http.GetAsync(url, Cancel).ConfigureAwait(false);

        //if (response.StatusCode == HttpStatusCode.NoContent)
        //    return default;

        //if (response.StatusCode == HttpStatusCode.NotFound)
        //    return default;

        //var result = await response
        //   .EnsureSuccessStatusCode()
        //   .Content
        //   .ReadFromJsonAsync<T>();

        //return result;

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
            case HttpStatusCode.NotFound:
                return default;
            default:
                var result = await response
                   .EnsureSuccessStatusCode()
                   .Content
                   .ReadFromJsonAsync<T>(cancellationToken: Cancel);
                return result;
        }
    }

    protected HttpResponseMessage Post<T>(string url, T value) => PostAsync(url, value).Result;

    protected async Task<HttpResponseMessage> PostAsync<T>(string url, T value, CancellationToken Cancel = default)
    {
        var response = await Http.PostAsJsonAsync(url, value, Cancel).ConfigureAwait(false);
        return response.EnsureSuccessStatusCode();
    }

    protected HttpResponseMessage Put<T>(string url, T value) => PutAsync(url, value).Result;

    protected async Task<HttpResponseMessage> PutAsync<T>(string url, T value, CancellationToken Cancel = default)
    {
        var response = await Http.PutAsJsonAsync(url, value, Cancel).ConfigureAwait(false);
        return response.EnsureSuccessStatusCode();
    }

    protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

    protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default)
    {
        var response = await Http.DeleteAsync(url, Cancel).ConfigureAwait(false);
        return response;
    }

    //~BaseClient() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // в случае наличия финализатора (~BaseClient() { ... }) - удалить данный объект из очереди на финализацию если был вызван данный метод
    }

    private bool _Disposed;
    protected virtual void Dispose(bool Disposing)
    {
        if (Disposing)
        {
            // здесь должны очистить все управляемые ресурсы
            //Http.Dispose(); // у этого объекта вызвать метод Dispose() мы права не имеем - не мы его создавали тут!
        }

        // здесь надо освободить неуправляемые ресурсы
    }
}
