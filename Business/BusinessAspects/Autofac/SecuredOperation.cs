namespace Business.BusinessAspects.Autofac
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using Business.Constants;
    using Castle.DynamicProxy;
    using Core.Utilities.Interceptors;
    using Core.Utilities.IoC;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',').Select(r => r.Trim()).ToArray();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity == null)
            {
                throw new Exception("CRITICAL_AUTH_ERROR: HttpContext.User veya Identity NULL!");
            }

            var allClaims = httpContext.User.Claims.ToList();
            var extractedUserRoles = new List<string>();

            foreach (var claim in allClaims)
            {
                var val = claim.Value?.Trim();
                if (string.IsNullOrEmpty(val)) continue;

                if (val.StartsWith("[") && val.EndsWith("]"))
                {
                    try
                    {
                        var parsed = JsonSerializer.Deserialize<List<string>>(val);
                        if (parsed != null) extractedUserRoles.AddRange(parsed.Select(r => r.Trim()));
                    }
                    catch
                    {
                        extractedUserRoles.Add(val);
                    }
                }
                else
                {
                    extractedUserRoles.Add(val);
                }
            }

            foreach (var role in _roles)
            {
                if (extractedUserRoles.Any(r => r.Equals(role, StringComparison.OrdinalIgnoreCase)))
                {
                    return; // Yetki tamam
                }
            }

            var reqRoles = string.Join(", ", _roles);
            var foundRoles = string.Join(", ", extractedUserRoles.Distinct());
            var isAuth = httpContext.User.Identity.IsAuthenticated;
            var claimsCount = allClaims.Count;

            throw new Exception($"YETKİ BUM! IsAuth: {isAuth} | ClaimCount: {claimsCount} | Aranan: [{reqRoles}] | Okunan Roller: [{foundRoles}]");
        }
    }
}