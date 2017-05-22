using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class SysHelpClassController : Controller
    {
        private readonly IRepository<SysHelpClass> _SysHelpClassService;

        public SysHelpClassController(IRepository<SysHelpClass> SysHelpClassService)
        {
            _SysHelpClassService = SysHelpClassService;
        }



        //
        // GET: /Platform/SysHelpClass/

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
            var model = _SysHelpClassService.GetAll()
                                     .Select(
                                         a =>
                                         new
                                         {
                                             a.Name,
                                             a.SystemId,
                                             a.Enable,
                                             a.CreateDateTime,
                                             a.Id
                                         });

            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysHelpClass/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _SysHelpClassService.GetAll(a => a.Id == id).Include(a => a.UserCreatedBy).Include(a => a.UserUpdatedBy).FirstOrDefault();
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
        // GET: /Platform/SysHelpClass/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysHelpClass();
            if (!string.IsNullOrEmpty(id))
            {
                item = _SysHelpClassService.GetById(id);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysHelpClass/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, SysHelpClass collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            _SysHelpClassService.Save(id, collection);

            await _SysHelpClassService.CommitAsync();

            return new EditSuccessResult(id);
        }

        //
        // POST: /Platform/SysHelpClass/Delete/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            _SysHelpClassService.Delete(id);

            await _SysHelpClassService.CommitAsync();
            
            return new DeleteSuccessResult();
        }
    }
}