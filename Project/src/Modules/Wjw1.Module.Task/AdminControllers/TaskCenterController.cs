using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Wjw1.Libarary.ModuleBaseLibrary;
using Wjw1.Libarary.ModuleBaseLibrary.Extentions;
using Wjw1.Libarary.Web;
using Wjw1.Libarary.Web.ActionResults;
using Wjw1.Module.Task.Models;
using Wjw1.Module.Task.ViewModels;

namespace Web.Areas.Platform.Controllers
{
    /// <summary>
    /// 任务中心
    /// </summary>
    [Area("Platform")]
    public class TaskCenterController : Controller
    {
        private readonly IRepository<TaskCenter> _iTaskCenterService;
        private readonly IRepository<SysUser> _iSysUserService;
        private readonly IUserInfo _iUserInfo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iTaskCenterService"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="iSysUserService"></param>
        /// <param name="iUserInfo"></param>
        public TaskCenterController(IRepository<TaskCenter> iTaskCenterService, IRepository<SysUser> iSysUserService, IUserInfo iUserInfo)
        {
            _iTaskCenterService = iTaskCenterService;
            _iSysUserService = iSysUserService;
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
            var model = _iTaskCenterService.GetAll(a => a.CreatedBy == _iUserInfo.UserId || a.TaskExecutorId == _iUserInfo.UserId).Select(a => new TaskCenterListModel { TaskType = a.TaskType.ToString(), Title = a.Title, Content = a.Content, Files = a.Files, TaskExecutor = a.TaskExecutor.UserName, UserName = a.UserCreatedBy.UserName, ScheduleEndTime = a.ScheduleEndTime, Id = a.Id, ActualEndTime = a.ActualEndTime, CreatedBy = a.CreatedBy, TaskExecutorId = a.TaskExecutorId, Duration = a.Duration, CreatedDate = a.CreatedDate });

            model = !string.IsNullOrEmpty(ordering) ? model.OrderBy(ordering, null) : model.OrderBy(a => a.ActualEndTime).ThenBy(a => a.ScheduleEndTime);


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
        public async Task<IActionResult> Details(object id)
        {
            var item = _iTaskCenterService.GetById(id);
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
        // GET: /Platform/TaskCenter/Edit/5

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <param name="finished"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id, bool finished = false)
        {
            var item = _iTaskCenterService.GetById(id);

            if (finished)
            {
                if (item.TaskExecutorId != _iUserInfo.UserId)
                {
                    throw new Exception();
                }

                item.ActualEndTime = DateTime.Now.ToDateTimeString();

                await _iTaskCenterService.CommitAsync();

                return new EditSuccessResult(id);
            }

            var model = new TaskCenterEditModel();

            if (!string.IsNullOrEmpty(id))
            {
                if (item.CreatedBy != _iUserInfo.UserId)
                {
                    throw new Exception();
                }

                Mapper.Initialize(a => a.CreateMap<TaskCenter, TaskCenterEditModel>());
                Mapper.Map(item, model);
            }

            ViewBag.TaskExecutorId = new SelectList(_iSysUserService.GetAll().Select(a => new { a.Id, a.UserName }), "Id", "UserName", model.TaskExecutorId);

            return View(model);
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, TaskCenterEditModel collection)
        {
            if (!ModelState.IsValid)
            {
                await Edit(id);
                return View(collection);
            }

            var item = new TaskCenter();

            if (!string.IsNullOrEmpty(id))
            {
                item = _iTaskCenterService.GetById(id);
            }

            Mapper.Initialize(a => a.CreateMap<TaskCenterEditModel, TaskCenter>());
            Mapper.Map(collection, item);

            //await TryUpdateModelAsync(item);

            _iTaskCenterService.Save(id, item);

            await _iTaskCenterService.CommitAsync();

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
            var item = _iTaskCenterService.GetById(id);

            if (item.CreatedBy != _iUserInfo.UserId)
            {
                throw new Exception();
            }

            _iTaskCenterService.Delete(id);

            await _iTaskCenterService.CommitAsync();

            return new DeleteSuccessResult();
        }
    }
}