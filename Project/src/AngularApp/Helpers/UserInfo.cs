using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Wjw1.Infrastructure.Models;
using Wjw1.Infrastructure;

namespace AngularApp.Helpers
{
    public class UserInfo : IUserInfo
    {
        private readonly UserManager<SysUser> _userManager;
        private readonly  IHttpContextAccessor _httpContextAccessor;

        public UserInfo(UserManager<SysUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string EnterpriseId => GetCurrentUserAsync()?.CurrentEnterpriseId;

        public string UserId =>  GetCurrentUserAsync()?.Id;

        public string UserName => GetCurrentUserAsync()?.UserName;

        public bool? IsInRole(string roleName)
        {
            return _userManager.GetRolesAsync(GetCurrentUserAsync()).Result?.Any(r => r.Equals(roleName));
        }

        private SysUser GetCurrentUserAsync()
        {
            return _userManager.Users.FirstOrDefault(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name);//.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
