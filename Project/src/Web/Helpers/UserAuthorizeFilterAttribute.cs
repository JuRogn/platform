using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;

namespace Web.Helpers
{
    /// <summary>
    /// 用户身份验证
    /// </summary>
    public class UserAuthorizeAttribute : AuthorizeFilter
    {

        public UserAuthorizeAttribute(AuthorizationPolicy policy) : base(policy)
        {
        }

        public UserAuthorizeAttribute(IEnumerable<IAuthorizeData> authorizeData) : base(authorizeData)
        {
        }

        public UserAuthorizeAttribute(string policy) : base(policy)
        {
        }

        public UserAuthorizeAttribute(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData) : base(policyProvider, authorizeData)
        {
        }

        public override Task OnAuthorizationAsync(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
        {
            var iSysAreaService = context.HttpContext.RequestServices.GetService(typeof(IRepository<SysArea>)) as Repository<SysArea>;

            var area = (string)context.RouteData.Values["area"];

            if (string.IsNullOrEmpty(area) || iSysAreaService == null || !iSysAreaService.GetAll(a => a.AreaName == area).Any()) return Task.FromResult(0);

            //从Cookie中读出，如果用户已经不存在需要重新登录
            if (!context.HttpContext.User.Identity.IsAuthenticated) return base.OnAuthorizationAsync(context);

            //判断当前用户权限
            var controller = (string)context.RouteData.Values["controller"];
            var action = (string)context.RouteData.Values["action"];

            var sysRoleService = context.HttpContext.RequestServices.GetService(typeof(IRepository<SysRole>)) as Repository<SysRole>;
            var userInfo = context.HttpContext.RequestServices.GetService(typeof(IUserInfo)) as IUserInfo;
            if(string.IsNullOrEmpty(userInfo.UserId))
            {
                //？需要注销当前用户？
                return base.OnAuthorizationAsync(context);
            }
            if (userInfo != null && sysRoleService != null && sysRoleService.GetAll(a => a.Users.Any(b => b.UserId.Equals(userInfo.UserId)) &&
                                       a.SysRoleSysControllerSysActions.Any(b => b.SysControllerSysAction.SysController.SysArea.AreaName.Equals(area) &&
                                               b.SysControllerSysAction.SysController.ControllerName.Equals(controller) &&
                                               b.SysControllerSysAction.SysAction.ActionName.Equals(action))).Any())
                return Task.FromResult(0);

            // 用户无权限
            context.Result = new BadRequestObjectResult("用户：" + userInfo.UserName + "(" + userInfo.UserId + ") 没有权限访问 " + area + " > " + controller + " > " + action + " ！请联系系统管理员进行权限分配！");

            return Task.FromResult(0);

        }

    }

}