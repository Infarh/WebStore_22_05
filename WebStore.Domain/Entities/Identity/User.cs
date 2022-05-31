using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity;

public class User : IdentityUser
{
    public override string ToString() => UserName;
}