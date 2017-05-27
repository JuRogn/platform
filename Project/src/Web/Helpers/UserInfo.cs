using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Wjw1.Infrastructure.Models;
using Wjw1.Infrastructure;

namespace Web.Helpers
{
    public class UserInfo : IUserInfo
    {
        private readonly UserManager<SysUser> _userManager;
        private readonly RoleManager<SysRole> _roleManager;
        private readonly  IHttpContextAccessor _httpContextAccessor;

        public UserInfo(UserManager<SysUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string EnterpriseId => GetCurrentUserAsync().Result?.CurrentEnterpriseId;

        public string UserId =>  GetCurrentUserAsync().Result?.Id;

        public string UserName => GetCurrentUserAsync().Result?.UserName;

        public bool? IsInRole(string roleName)
        {
            return _userManager.GetRolesAsync(GetCurrentUserAsync().Result).Result?.Any(r => r.Equals(roleName));
        }

        private Task<SysUser> GetCurrentUserAsync()
        {
            return  _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
