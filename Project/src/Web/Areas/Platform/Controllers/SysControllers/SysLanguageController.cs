using System;
using System.Linq;
using IServices.Infrastructure;
using IServices.ISysServices;
using Models.SysModels;
using Web.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Platform.Helpers;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Platform")]
    public class SysLanguageController : Controller
    {
        private readonly ISysLanguagePackService _SysLanguageService;
        private readonly IUnitOfWork _unitOfWork;

        public SysLanguageController(IUnitOfWork unitOfWork, ISysLanguagePackService SysLanguageService)
        {
            _unitOfWork = unitOfWork;
            _SysLanguageService = SysLanguageService;
        }

        //
        // GET: /Platform/SysLanguage/

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
            var model = _SysLanguageService.GetAll()
                                     .Select(
                                         a =>
                                         new
                                         {
                                             a.Language,
                                             a.Name,
                                             a.Value,
                                             a.Id
                                         });

            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysLanguage/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _SysLanguageService.GetById(id);
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
        // GET: /Platform/SysLanguage/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysLanguagePack();
            if (!string.IsNullOrEmpty(id))
            {
                item = _SysLanguageService.GetById(id);
            }
            return View(item);
        }

        //
        // POST: /Platform/SysLanguage/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, SysLanguagePack collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            _SysLanguageService.Save(id, collection);

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        //
        // POST: /Platform/SysLanguage/Delete/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            _SysLanguageService.Delete(id);

            await _unitOfWork.CommitAsync();

            return new DeleteSuccessResult();
        }
    }
}