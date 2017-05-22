using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Wjw1.Infrastructure;
using Wjw1.Libarary.ModuleBaseLibrary.Extentions;
using Wjw1.Libarary.Web;
using Wjw1.Libarary.Web.ActionResults;
using Wjw1.Module.Localization.Models;

using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 文化语言设置
    /// </summary>
    [Area("Platform")]
    public class CultureController : Controller
    {
        private readonly IRepository<Culture> _iCultureService;
        private readonly IUserInfo _iUserInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="iCultureService"></param>
        /// <param name="iUserInfo"></param>
        public CultureController(IRepository<Resource> iResourceService, IRepository<Culture> iCultureService, IUserInfo iUserInfo)
        {
            _iCultureService = iCultureService;
            _iUserInfo = iUserInfo;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="ordering"></param>
        /// <param name="pageIndex"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string keyword, string ordering, int pageIndex = 1, bool report = false)
        {
            var model = _iCultureService.GetAll();
            model = !string.IsNullOrEmpty(ordering) ? model.OrderBy(ordering, null) : model.OrderBy(a => a.Id);


            if (report)
            {
                return model.ToExcelFile();
            }

            return View(model.ToPagedList(pageIndex));
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            var item = _iCultureService.GetById(id);
            return View(item);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // GET: /Platform/Resource/Edit/5

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="finished"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = _iCultureService.GetById(id);
            if (item == null)
                item = new Culture();
            return View(item);
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Culture collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }


            _iCultureService.Save(id, collection);

            await _iCultureService.CommitAsync();

            return new EditSuccessResult(id);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(string id)
        {
            var item = _iCultureService.GetById(id);

            _iCultureService.Delete(id);

            await _iCultureService.CommitAsync();

            return new DeleteSuccessResult();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Remove(string id)
        {
            var item = _iCultureService.GetById(id);
            
            _iCultureService.Delete(id);

            await _iCultureService.CommitAsync();

            return new DeleteSuccessResult();
        }
    }
}