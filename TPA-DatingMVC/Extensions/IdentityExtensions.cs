﻿using System.Security.Claims;
using System.Security.Principal;

namespace TPA_DatingMVC.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetDisplayName(this IIdentity identity) {
            Claim claim = ((ClaimsIdentity)identity).FindFirst("DisplayName");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}