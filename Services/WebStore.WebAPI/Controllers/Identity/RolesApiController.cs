using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.WebAPI.Controllers.Identity;

[ApiController]
[Route("api/roles")]
public class RolesApiController : ControllerBase
{
    private readonly RoleStore<Role> _RoleStore;
    private readonly ILogger<RolesApiController> _Logger;

    public RolesApiController(WebStoreDB db, ILogger<RolesApiController> Logger)
    {
        _Logger = Logger;
        _RoleStore = new(db);
    }

    [HttpGet("all")]
    public async Task<IEnumerable<Role>> GetAll() => await _RoleStore.Roles.ToArrayAsync();
}
