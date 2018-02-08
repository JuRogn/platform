using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Wjw1.Infrastructure;
using Wjw1.Infrastructure.Models;

namespace AngularApp.Helpers
{

    public class LogFilter : ActionFilterAttribute
    {

        private readonly IRepository<SysControllerSysAction>  _iSysControllerSysActionService;
        private readonly IRepository<SysUserLog> _iSysUserLogService;
        //private readonly IUnitOfWork _iUnitOfWork;

        public LogFilter(IRepository<SysControllerSysAction> iSysControllerSysActionService
            , IRepository<SysUserLog> iSysUserLogService
            //, IUnitOfWork iUnitOfWork
            )
        {
            _iSysControllerSysActionService = iSysControllerSysActionService;
            _iSysUserLogService = iSysUserLogService;
            //_iUnitOfWork = iUnitOfWork;
        }

        private DateTime _actiondatetimenow;

        private DateTime _viewdatetimenow;

        private double _actionDuration;

        private double _viewDuration;

        #region Action时间监控
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actiondatetimenow = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            _actionDuration = Math.Round((DateTime.Now - _actiondatetimenow).TotalSeconds, 3);

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _viewdatetimenow = DateTime.Now;
            base.OnResultExecuting(filterContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

            _viewDuration = Math.Round((DateTime.Now - _viewdatetimenow).TotalSeconds, 3);

            //记录用户访问记录
            var area = (string)filterContext.RouteData.Values["area"];
            var action = (string)filterContext.RouteData.Values["action"];
            var controller = (string)filterContext.RouteData.Values["controller"];
            var recordId = (string)filterContext.RouteData.Values["id"];
         
            var sysControllerSysAction =
                _iSysControllerSysActionService.GetAll(a => a.SysController.ControllerName.Equals(controller) &&
                            a.SysController.SysArea.AreaName.Equals(area) && a.SysAction.ActionName.Equals(action)).OrderBy(a => a.SysController.SystemId).Select(a => new { a.Id, SysAreaName = a.SysController.SysArea.Name, SysControllerName = a.SysController.Name, SysActionName = a.SysAction.Name }).FirstOrDefault();

            var sysuserlog = new SysUserLog
            {
                Ip = filterContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                SysControllerSysActionId = sysControllerSysAction?.Id,
                RecordId = recordId,
                Url = filterContext.HttpContext.Request.Path,
                SysArea = sysControllerSysAction?.SysAreaName ?? area,
                SysController = sysControllerSysAction?.SysControllerName ?? controller,
                SysAction = sysControllerSysAction?.SysActionName ?? action,
                ViewDuration = _viewDuration,
                ActionDuration = _actionDuration,
                Duration = Math.Round((DateTime.Now - _actiondatetimenow).TotalSeconds, 3),
                RequestType = filterContext.HttpContext.Request.Method
            };

            _iSysUserLogService.Save(null, sysuserlog);

            _iSysUserLogService.CommitAsync().Wait();

        }
    }


   
}
