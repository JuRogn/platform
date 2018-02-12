using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Wjw1.Infrastructure;
using Wjw1.Libarary.ModuleBaseLibrary.Extentions;
using Wjw1.Libarary.Web;
using Wjw1.Libarary.Web.ActionResults;
using Wjw1.Module.Localization.Models;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 语言包设置
    /// </summary>
    [Area("Platform")]
    public class ResourceController : Controller
    {
        private readonly IRepository<Resource> _iResourceService;
        private readonly IRepository<Culture> _iCultureService;
        private readonly IUserInfo _iUserInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iResourceService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="iCultureService"></param>
        /// <param name="iUserInfo"></param>
        public ResourceController(IRepository<Resource> iResourceService, IRepository<Culture> iCultureService, IUserInfo iUserInfo)
        {
            _iResourceService = iResourceService;
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
            var model = _iResourceService.GetAll().Select(a=>new {
                Resource_Key =  a.Key,
                Resource_Value = a.Value,
                Resource_Culture = a.Culture.DisplayName,
                a.Id
            });
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
            var item = _iResourceService.GetById(id);
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
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var item = _iResourceService.GetById(id);
            if (item == null)
                item = new Resource();
            ViewBag.CultureId = new SelectList(_iCultureService.GetAll().Select(a => new { a.Id, a.DisplayName }), "Id", "DisplayName", item.CultureId);

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
        public async Task<IActionResult> Edit(string id,Resource collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }
            
            _iResourceService.Save(id, collection);

            _iResourceService.CommitAsync().Wait();

            return new EditSuccessResult(id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(object id)
        {
            var item = _iResourceService.GetById(id);
            
            _iResourceService.Delete(id);

            await _iResourceService.CommitAsync();

            return new DeleteSuccessResult();
        }
    }
}