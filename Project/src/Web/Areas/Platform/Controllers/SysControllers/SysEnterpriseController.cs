using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Platform.Helpers;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Wjw1.Libarary.ModuleBaseLibrary.Extentions;
using Wjw1.Libarary.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Platform")]
    public class SysEnterpriseController : Controller
    {
        private readonly IRepository<SysEnterprise> _SysEnterpriseService;
        private readonly IUserInfo _iUserInfo;

        public SysEnterpriseController(IRepository<SysEnterprise> SysEnterpriseService
            , IUserInfo iUserInfo)
        {
            _SysEnterpriseService = SysEnterpriseService;
            _iUserInfo = iUserInfo;
        }



        //
        // GET: /Platform/SysEnterprise/

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
            var model = _SysEnterpriseService.GetAll(DeletedDataType.All)
                                     .Select(a =>new
                                         {
                                             a.EnterpriseName,
                                             a.Id
                                         })
                                    .OrderBy(a=>a.Id);

            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysEnterprise/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _SysEnterpriseService.GetById(id);
            return View(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/SysEnterprise/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysEnterprise();
            if (!string.IsNullOrEmpty(id))
            {
                item = _SysEnterpriseService.GetById(id);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysEnterprise/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, SysEnterprise collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }
            //只能创建下级企业，ID自动分配
            if (string.IsNullOrEmpty(id))
            {
                var curEntId = _iUserInfo.EnterpriseId;
                collection.Id =curEntId+ (_SysEnterpriseService.GetAll(DeletedDataType.All).Count(a => a.Id.StartsWith(curEntId) && a.Id.Length.Equals(curEntId.Length + 3))+1).ToString().PadLeft(3,'0');
            }
            _SysEnterpriseService.Save(id, collection);

            await _SysEnterpriseService.CommitAsync();

            return new EditSuccessResult(id);
        }

        //
        // POST: /Platform/SysEnterprise/Delete/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            _SysEnterpriseService.Delete(id);

            await _SysEnterpriseService.CommitAsync();
           
            return new DeleteSuccessResult();
        }
    }
}