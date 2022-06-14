using System.Text.Json;
using Microsoft.AspNetCore.Http;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InCookies;

public class InCookiesCartService : ICartService
{
    private readonly IHttpContextAccessor _HttpContextAccessor;
    private readonly IProductData _ProductData;
    private readonly string _CartName;

    private Cart Cart
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

    public InCookiesCartService(IHttpContextAccessor HttpContextAccessor, IProductData ProductData)
    {
        _HttpContextAccessor = HttpContextAccessor;
        _ProductData = ProductData;

        var user = HttpContextAccessor.HttpContext!.User;
        var user_name = user.Identity!.IsAuthenticated ? $"-{user.Identity.Name}" : null;

        _CartName = $"WebStore.GB.Cart{user_name}";
    }

    public void Add(int Id)
    {
        var cart = Cart;
        cart.Add(Id);
        Cart = cart;
    }

    public void Decrement(int Id)
    {
        var cart = Cart;
        cart.Decrement(Id);
        Cart = cart;
    }

    public void Remove(int Id)
    {
        var cart = Cart;
        cart.Remove(Id);
        Cart = cart;
    }

    public void Clear()
    {
        var cart = Cart;
        cart.Clear();
        Cart = cart;
    }

    public CartViewModel GetViewModel()
    {
        var cart = Cart;

        var products = _ProductData.GetProducts(new()
        {
            Ids = cart.Items.Select(item => item.ProductId).ToArray(),
        });

        var products_views = products.ToView().ToDictionary(p => p!.Id);

        return new()
        {
            Items = cart.Items
               .Where(item => products_views.ContainsKey(item.ProductId))
               .Select(item => (products_views[item.ProductId], item.Quantity))!,
        };
    }
}
