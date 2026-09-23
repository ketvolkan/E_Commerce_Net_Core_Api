namespace Core.Utilities.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Core.Utilities.IoC;
    using Microsoft.Extensions.DependencyInjection;

    public static class CurrentUser
    {
        private static IHttpContextAccessor HttpContextAccessor => ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        public static int GetUserId()
        {
            var idClaim = HttpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(idClaim, out var id)) return id;
            return 0;
        }

        public static List<string> GetRoles()
        {
            var roles = HttpContextAccessor?.HttpContext?.User?.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            return roles ?? new List<string>();
        }

        public static bool IsAdmin()
        {
            var roles = GetRoles();
            return roles.Contains("claim.assign-to-user");
        }
    }
}
