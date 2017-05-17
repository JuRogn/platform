using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wjw1.Infrastructure.Models;

namespace Wjw1.Libarary.Web.ActionResults
{
    /// <summary>
    /// 扩展删除后的返回结果，提示和跳转
    /// </summary>
    public class DeleteSuccessResult : ActionResult
    {
        private RouteValueDictionary RouteValues { get; set; }
        private string Index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="routeValues"></param>
        public DeleteSuccessResult(string index = "Index", RouteValueDictionary routeValues = null)
        {
            Index = index;
            RouteValues = routeValues ?? new RouteValueDictionary();
        }

        protected DeleteSuccessResult()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            foreach (var key in context.HttpContext.Request.Query)
            {
                RouteValues.Add(key.Key, key.Value);
            }

            RouteValues["action"] = Index;

            var result = new RedirectToRouteResult(RouteValues);

            var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

            var tempdata = new Dictionary<string, object> { { AlertType.Alerts.Success, "删除成功" } };//Lang.DeleteSuccess

            factory.SaveTempData(context.HttpContext, tempdata);

            result.ExecuteResult(context);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            foreach (var key in context.HttpContext.Request.Query)
            {
                RouteValues.Add(key.Key, key.Value);
            }

            RouteValues["action"] = Index;

            var result = new RedirectToRouteResult(RouteValues);

            var factory = context.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

            var tempdata = new Dictionary<string, object> { { AlertType.Alerts.Success,  "删除成功"} };//Lang.DeleteSuccess

            factory.SaveTempData(context.HttpContext, tempdata);

            return result.ExecuteResultAsync(context);
        }

    }
}
