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
    public class SysDepartmentController : Controller
    {
        private readonly IRepository<SysDepartment> _SysDepartmentService;

        public SysDepartmentController(IRepository<SysDepartment> SysDepartmentService)
        {
            _SysDepartmentService = SysDepartmentService;
        }



        //
        // GET: /Platform/SysDepartment/

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
            var model = _SysDepartmentService.GetAll()
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
        // GET: /Platform/SysDepartment/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _SysDepartmentService.GetAll(a => a.Id == id).Include(a=>a.UserCreatedBy).Include(a=>a.UserUpdatedBy).FirstOrDefault();
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
        // GET: /Platform/SysDepartment/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysDepartment();
            if (!string.IsNullOrEmpty(id))
            {
                item = _SysDepartmentService.GetById(id);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysDepartment/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, SysDepartment collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            _SysDepartmentService.Save(id, collection);

            await _SysDepartmentService.CommitAsync();

            return new EditSuccessResult(id);
        }

        //
        // POST: /Platform/SysDepartment/Delete/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            _SysDepartmentService.Delete(id);

            await _SysDepartmentService.CommitAsync();
           
            return new DeleteSuccessResult();
        }
    }
}