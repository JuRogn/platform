// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Wjw1.Infrastructure.Models;
using System.Linq;

namespace Api.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class IdentityController : ControllerBase
    {
        public IdentityController(SignInManager<SysUser> signInManager)
        {
            _signInManager = signInManager;
        }

        private readonly SignInManager<SysUser> _signInManager;

        public object AuthenticationTypes { get; private set; }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value,c.Issuer,c.Subject.Name });
            //return new JsonResult(User.Identity.Name);
        }
    }
}