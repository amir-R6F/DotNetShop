using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application;
using Shop.Infrastructure;

namespace ServiceHost
{
    public class SecurityPageFilter : IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var RequiredPermission = 
                (NeedsPermissionAttribute) context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute));

            var UserPermissions = _authHelper.GetPermissions();

            if (RequiredPermission == null)
                return;
            
            if (UserPermissions.All(permission=> permission != RequiredPermission.Permission))
                context.HttpContext.Response.Redirect("/Account");

        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            
        }
    }
}