using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.WebAPI.Controllers.Identity;

[ApiController]
[Route(WebAPIAddresses.V1.Identity.Users)]
public class UsersApiController : ControllerBase
{
    private readonly UserStore<User, Role, WebStoreDB> _UserStore;
    private readonly ILogger<UsersApiController> _Logger;

    public UsersApiController(WebStoreDB db, ILogger<UsersApiController> Logger)
    {
        _Logger = Logger;
        _UserStore = new(db);
    }

    [HttpGet("all")]
    public async Task<IEnumerable<User>> GetAll() => await _UserStore.Users.ToArrayAsync();
}
