using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Wjw1.Infrastructure.Models;

namespace AngularApp
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly SignInManager<SysUser> _signInManager;
        private readonly RoleManager<SysRole> _roleManager;
        public ResourceOwnerPasswordValidator(SignInManager<SysUser> signInManager, RoleManager<SysRole> roleManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            //根据context.UserName和context.Password与数据库的数据做校验，判断是否合法
            var result = await _signInManager.PasswordSignInAsync(context.UserName, context.Password, true, true);
            //var result = await _signInManager.UserManager.FindByNameAsync(context.UserName);
            if (result.Succeeded)
            {
                 context.Result = new GrantValidationResult(
                 subject: context.UserName,
                 authenticationMethod: "custom",
                 authTime:DateTime.Today.AddDays(140),
                 claims: await GetUserClaims(context));
            }
            else
            {
                 //验证失败
                 context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "invalid custom credential");
            }
        }
        //可以根据需要设置相应的Claim
        private async Task<Claim[]> GetUserClaims(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _signInManager.UserManager.FindByNameAsync(context.UserName);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            var claims =  new Claim[]
                {
                new Claim(JwtClaimTypes.Id, user.Id),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtClaimTypes.Name,user.UserName),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Expiration,DateTime.UtcNow.AddDays(14).Second.ToString()),
                new Claim(JwtClaimTypes.Role,String.Join(',',roles))
            };
            return claims;
        }
    }
    
}
