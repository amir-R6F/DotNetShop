using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Domain;

namespace Shop.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;


        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAuthenticated()
        {
            // var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            // return claims.Count > 0;
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated; 
        }

        public string CurrentAccountRole()
        {
            if (IsAuthenticated())
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            
            return null;
        }

        public AuthViewModel CurrentAccountInfo()
        {
            var res = new AuthViewModel();
            if (!IsAuthenticated())
                return res;

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();
            res.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            res.Username = claims.FirstOrDefault(x => x.Type == "Username").Value;
            res.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            res.FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            res.Role = Rules.GetRuleBy(res.Id);
            return res;
        }

        public List<int> GetPermissions()
        {
            if (!IsAuthenticated())
                return new List<int>();
            
            var permissions = _contextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == "Permissions")?.Value;
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        public void SingOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void SignIn(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);
            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("Username", account.Username),
                new Claim("Permissions", permissions),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
            };

            _contextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

        }


    }
}