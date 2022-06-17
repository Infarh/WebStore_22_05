using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersApiController : ControllerBase
{
    private readonly IOrderService _OrderService;
    private readonly ILogger<OrdersApiController> _Logger;

    public OrdersApiController(IOrderService OrderService, ILogger<OrdersApiController> Logger)
    {
        _OrderService = OrderService;
        _Logger = Logger;
    }
}
