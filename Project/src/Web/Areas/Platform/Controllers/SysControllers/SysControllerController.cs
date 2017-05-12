using System.Linq;
using IServices.Infrastructure;
using IServices.ISysServices;
using Models.SysModels;
using Web.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Platform.Helpers;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Platform")]
    public class SysControllerController : Controller
    {
        private readonly ISysActionService _sysActionService;
        private readonly ISysAreaService _sysAreaService;
        private readonly ISysControllerService _sysControllerService;
        private readonly ISysControllerSysActionService _sysControllerSysActionService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysActionService"></param>
        /// <param name="sysAreaService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="sysControllerService"></param>
        /// <param name="sysControllerSysActionService"></param>
        public SysControllerController(ISysActionService sysActionService, ISysAreaService sysAreaService,
                                       IUnitOfWork unitOfWork, ISysControllerService sysControllerService,
                                       ISysControllerSysActionService sysControllerSysActionService)
        {
            _sysActionService = sysActionService;
            _sysAreaService = sysAreaService;
            _unitOfWork = unitOfWork;
            _sysControllerService = sysControllerService;
            _sysControllerSysActionService = sysControllerSysActionService;
        }

        //
        // GET: /Platform/SysController/

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
                _sysControllerService.GetAll()
                                     .Select(
                                         a =>
                                         new
                                         {
                                             SysArea = a.SysArea.Name,
                                             a.Name,
                                             a.ControllerName,
                                             a.ActionName,
                                             a.Parameter,
                                             a.SystemId,
                                             a.Display,
                                             a.Ico,
                                             a.Enable,
                                             a.TargetBlank,
                                             a.Id
                                         });

            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }

        //
        // GET: /Platform/SysController/Details/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _sysControllerService.GetAll(a => a.Id == id).Include(a => a.SysArea).FirstOrDefault();
            ViewBag.SysAreaId = item.SysArea.ToString();
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
        // GET: /Platform/SysController/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = new SysController();
            if (!string.IsNullOrEmpty(id))
            {
                item = await _sysControllerService.GetAll(a => a.Id == id).Include(a => a.SysControllerSysActions).SingleOrDefaultAsync();
            }
            ViewBag.SysAreaId = new SelectList(_sysAreaService.GetAll(), "Id", "Name", item.SysAreaId);
            ViewBag.SysControllerSysActions = new MultiSelectList(_sysActionService.GetAll(), "Id", "Name",
                                                       item.SysControllerSysActions?.Select(a => a.SysActionId));
            return View(item);
        }

        //
        // POST: /Platform/SysController/Edit/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <param name="sysControllerSysActions"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(string id, SysController collection, string[] sysControllerSysActions)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                ViewBag.SysAreaId = new SelectList(_sysAreaService.GetAll(), "Id", "Name", collection.SysAreaId);
                ViewBag.SysControllerSysActions = new MultiSelectList(_sysActionService.GetAll(), "Id", "Name", sysControllerSysActions);
                return View(collection);
            }

            if (!string.IsNullOrEmpty(id))
            {
                //清除原有数据
                _sysControllerSysActionService.Delete(a => a.SysControllerId.Equals(id) && !sysControllerSysActions.Contains(a.SysActionId));
            }

            if (sysControllerSysActions != null)
            {
                foreach (
                    var actionid in
                    sysControllerSysActions.Where(
                        actionid =>
                            !_sysControllerSysActionService.GetAll()
                                .Where(b => b.SysControllerId.Equals(id))
                                .Select(b => b.SysActionId)
                                .Contains(actionid)))
                {
                    _sysControllerSysActionService.Save(null, new SysControllerSysAction
                    {
                        SysControllerId = collection.Id,
                        SysActionId = actionid
                    });
                }
            }

            _sysControllerService.Save(id, collection);

            await _unitOfWork.CommitAsync();

            return new EditSuccessResult(id);
        }

        //
        // POST: /Platform/SysController/Delete/5

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            _sysControllerService.Delete(id);

            await _unitOfWork.CommitAsync();

            return new DeleteSuccessResult();
        }
    }
}