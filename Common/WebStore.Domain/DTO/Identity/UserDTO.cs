using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.DTO.Identity;

public abstract class UserDTO
{
    public User User { get; init; } = null!;
}

public class AddLoginDTO : UserDTO
{
    public UserLoginInfo UserLoginInfo { get; init; } = null!;
}

public class PasswordHashDTO : UserDTO
{
    public string Hash { get; init; } = null!;
}

public class SetLockoutDTO : UserDTO
{
    public DateTimeOffset? LockoutEnd { get; init; }
}