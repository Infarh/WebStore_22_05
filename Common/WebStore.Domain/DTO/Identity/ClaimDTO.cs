using System.Security.Claims;

namespace WebStore.Domain.DTO.Identity;

public class ClaimDTO : UserDTO
{
    public IEnumerable<Claim> Claims { get; init; } = null!;
}

public class ReplaceClaimDTO : UserDTO
{
    public Claim Claim { get; init; } = null!;

    public Claim NewClaim { get; init; } = null!;
}