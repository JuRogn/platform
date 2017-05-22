using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Platform.Helpers;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Wjw1.Libarary.ModuleBaseLibrary.Extentions;
using Wjw1.Libarary.Web;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Platform")]
    public class SysActionController : Controller
    {
        private readonly IRepository<SysAction> _sysActionService;

        public SysActionController(IRepository<SysAction> sysActionService)
        {
            _sysActionService = sysActionService;
        }



        //
        // GET: /Platform/SysAction/

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
            var model = _sysActionService.GetAll()
                                     .Select(
                                         a =>
                                         new
                                         {
                                             a.Name,
                                             a.ActionName,
                                             a.SystemId,
                                             a.Enable,
                                             a.System,
                                             a.Id
                                         });

            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysAction/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _sysActionService.GetById(id);
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
        // GET: /Platform/SysAction/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysAction();
            if (!string.IsNullOrEmpty(id))
            {
                item = _sysActionService.GetById(id);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysAction/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, SysAction collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            _sysActionService.Save(id, collection);

            await _sysActionService.CommitAsync();

            return new EditSuccessResult(id);
        }

        //
        // POST: /Platform/SysAction/Delete/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {

            var item = _sysActionService.GetById(id);

            if (item.System)
            {
                throw new Exception("系统内置类型，不可删除！");
            }

            _sysActionService.Delete(item);

            await _sysActionService.CommitAsync();


            //return RedirectToAction("Index");
            return new DeleteSuccessResult();
        }
    }
}