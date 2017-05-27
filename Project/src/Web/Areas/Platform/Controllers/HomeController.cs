using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;

namespace Web.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class HomeController : Controller
    {
        private readonly IRepository<SysUserLog> _iSysUserLogService;
        private readonly IRepository<SysController> _iSysControllerService;
        private readonly IUserInfo _userInfo;
        private readonly IDistributedCache _distributedCache;

        public HomeController(IRepository<SysUserLog> iSysUserLogService, IRepository<SysController> iSysControllerService, IUserInfo userInfo, IDistributedCache distributedCache)
        {
            _iSysUserLogService = iSysUserLogService;
            _iSysControllerService = iSysControllerService;
            _userInfo = userInfo;
            _distributedCache = distributedCache;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.sysUserLogCount = await _iSysUserLogService.GetAll().CountAsync();

            var area = (string)RouteData.Values["area"];

            ViewBag.UserId = _userInfo.UserId;

            ViewBag.EnterpriseId = _userInfo.EnterpriseId;


            ViewBag.menu = await _iSysControllerService.GetAll(a =>
                                   a.Display && a.Enable &&
                                   a.SysArea.AreaName.Equals(area)).ToListAsync();


            var aa = _distributedCache.GetString("now");

            if (string.IsNullOrEmpty(aa))
            {
                _distributedCache.Set("now", System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
            }
            
            return View();
        }
    }
}