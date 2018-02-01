using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;
using Wjw1.Libarary.Web;
using Wjw1.Module.Task.Models;
using Wjw1.Module.Task.ViewModels;

namespace Web.Areas.API.Controllers
{
    /// <summary>
    /// 任务中心
    /// </summary>
    [Area("API")]
    [Route("api/TaskCenter")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TaskCenterAPIController : Controller
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
        public TaskCenterAPIController(IRepository<TaskCenter> iTaskCenterService, IRepository<SysUser> iSysUserService, IUserInfo iUserInfo)
        {
            _iTaskCenterService = iTaskCenterService;
            _iSysUserService = iSysUserService;
            _iUserInfo = iUserInfo;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="ordering">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>
        /// </returns>
        [HttpGet]
        public async Task<APIPagedList<TaskCenterListModel>> Index(string keyword, string ordering, int pageIndex = 1)
        {
            var model = _iTaskCenterService.GetAll(a => a.UserCreatedBy.UserName == User.Identity.Name || a.TaskExecutorId == _iUserInfo.UserId).Select(a => new TaskCenterListModel { TaskType = a.TaskType.ToString(), Title = a.Title, Content = a.Content, Files = a.Files, TaskExecutor = a.TaskExecutor.UserName, UserName = a.UserCreatedBy.UserName, ScheduleEndTime = a.ScheduleEndTime, Id = a.Id, ActualEndTime = a.ActualEndTime, CreatedBy = a.CreatedBy, TaskExecutorId = a.TaskExecutorId, Duration = a.Duration, CreatedDate = a.CreatedDate });

            model = !string.IsNullOrEmpty(ordering) ? model.OrderBy(ordering, null) : model.OrderBy(a => a.ActualEndTime).ThenBy(a => a.ScheduleEndTime);
            
            return model.ToAPIPagedList(pageIndex);
        }
    }
        
}