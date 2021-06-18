using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace Snowdrop.Infrastructure.Dto.Users
{
    public record UserResponse(string UserName, IEnumerable<Claim> Claims);
}