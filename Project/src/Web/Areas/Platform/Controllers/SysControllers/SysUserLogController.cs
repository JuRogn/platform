using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Wjw1.Libarary.ModuleBaseLibrary.Extentions;
using Wjw1.Libarary.Web;

namespace Web.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class SysUserLogController : Controller
    {
        private readonly IRepository<SysUserLog> _sysUserLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysUserLogService"></param>
        public SysUserLogController(IRepository<SysUserLog> sysUserLogService)
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