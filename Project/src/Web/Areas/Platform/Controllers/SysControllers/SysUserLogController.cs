using IServices.ISysServices;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using Web.Helpers;

namespace Web.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class SysUserLogController : Controller
    {
        private readonly ISysUserLogService _sysUserLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysUserLogService"></param>
        public SysUserLogController(ISysUserLogService sysUserLogService)
        {
            _sysUserLogService = sysUserLogService;
        }

        //
        // GET: /Platform/SysUserLog/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ordering"></param>
        /// <param name="pageIndex"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, bool report = false)
        {
            var model =
                _sysUserLogService.GetAll()
                                  .Select(
                                      a =>
                                      new
                                      {
                                          a.UserCreatedBy.UserName,
                                          a.SysArea,
                                          a.SysController,
                                          a.SysAction,
                                          a.RecordId,
                                          a.ActionDuration,
                                          a.ViewDuration,
                                          a.Duration,
                                          a.RequestType,
                                          a.Ip,
                                          a.CreateDateTime
                                      });


            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }


    }
}