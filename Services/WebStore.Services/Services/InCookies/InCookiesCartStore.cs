using System.Text.Json;
using Microsoft.AspNetCore.Http;

using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InCookies;

public class InCookiesCartStore : ICartStore
{
    private readonly IHttpContextAccessor _HttpContextAccessor;
    private readonly string _CartName;

    public Cart Cart
    {
        get
        {
            var context = _HttpContextAccessor.HttpContext!;
            var cookies = context.Response.Cookies;

            var cart_cookie = context.Request.Cookies[_CartName];
            if (cart_cookie is null)
            {
                var cart = new Cart();
                cookies.Append(_CartName, JsonSerializer.Serialize(cart));
                return cart;
            }

            ReplaceCart(cookies, cart_cookie);
            return JsonSerializer.Deserialize<Cart>(cart_cookie)!;
        }
        set => ReplaceCart(_HttpContextAccessor.HttpContext!.Response.Cookies, JsonSerializer.Serialize(value));
    }

    private void ReplaceCart(IResponseCookies cookies, string cart)
    {
        cookies.Delete(_CartName);
        cookies.Append(_CartName, cart);
    }

    public InCookiesCartStore(IHttpContextAccessor HttpContextAccessor)
    {
        _HttpContextAccessor = HttpContextAccessor;

        var user = HttpContextAccessor.HttpContext!.User;
        var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

        _CartName = $"WebStore.GB.Cart{user_name}";
    }
}
