﻿using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositries.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
