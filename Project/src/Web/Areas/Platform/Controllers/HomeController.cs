using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace Web.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class HomeController : Controller
    {
        private readonly ISysUserLogService _iSysUserLogService;
        private readonly ISysControllerService _iSysControllerService;
        private readonly IUserInfo _userInfo;
        private readonly IDistributedCache _distributedCache;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ISysUserLogService iSysUserLogService, ISysControllerService iSysControllerService, IUserInfo userInfo, IDistributedCache distributedCache, IStringLocalizer<HomeController> localizer)
        {
            _iSysUserLogService = iSysUserLogService;
            _iSysControllerService = iSysControllerService;
            _userInfo = userInfo;
            _distributedCache = distributedCache;
            _localizer = localizer;
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


            ViewBag.lang = _localizer["aa"].Value;

            return View();
        }
    }
}