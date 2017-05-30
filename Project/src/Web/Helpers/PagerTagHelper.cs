using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Wjw1.Libarary.Web;

namespace Web
{
    /// <summary>
    /// 分页标签
    /// </summary>
    public class PagerTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        
        public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public IEnumerable PagerOption { get; set; }
        /// <summary>
        /// Ajax
        /// </summary>
        public bool PagerAjax { get; set; } = false;
        /// <summary>
        /// Target id to be updated 
        /// </summary>
        public string PagerAjaxUpdate { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            return base.ProcessAsync(context, output);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            if (PagerAjax && string.IsNullOrEmpty(PagerAjaxUpdate))
            {
                PagerAjaxUpdate = "#Main";
            }

            output.TagName = "div";

            var model = PagerOption as IPagedList;

            if (model == null)
            {
                base.Process(context, output);
            }

            var totalPage = model.TotalPage;
            var start = model.PageIndex - 5 >= 1 ? model.PageIndex - 5 : 1;
            var end = totalPage - start >= 10 ? start + 10 : totalPage;

            if (totalPage - model.PageIndex < 5)
            {
                start = totalPage - 10;
                if (start < 1)
                {
                    start = 1;
                }
            }

            //构造分页样式
            var sbPage = new StringBuilder(string.Empty);

            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var vs = ViewContext.RouteData.Values;


            var queryString = ViewContext.HttpContext.Request.Query;

            foreach (var key in queryString)
                vs[key.Key] = key.Value;

            //var formString = ViewContext.HttpContext.Request.Form;

            //foreach (var key in formString)
            //    vs[key.Key] = formString[key.Value];

            vs.Remove("X-Requested-With");
            vs.Remove("X-HTTP-Method-Override");

            #region 默认样式

            sbPage.Append("<nav>");
            sbPage.Append("<ul class=\"pagination\">");

            if (model.PageIndex > 1)
            {
                vs["pageIndex"] = 1;

                sbPage.AppendFormat("<li><a href=\"{0}\" aria-label=\"Previous\" " + (PagerAjax ? "data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"" + PagerAjaxUpdate : " ") + "\"><span aria-hidden=\"true\">|<</span></a></li>", urlHelper.Action("",  vs));

                vs["pageIndex"] = model.PageIndex - 1 <= 0 ? 1 : model.PageIndex - 1;

                sbPage.AppendFormat("<li><a href=\"{0}\" aria-label=\"Previous\"  " + (PagerAjax ? "data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"" + PagerAjaxUpdate : " ") + "\"><span aria-hidden=\"true\"><</span></a></li>", urlHelper.Action("", vs));
            }

            for (var i = start; i <= end; i++)
            {
                vs["pageIndex"] = i;

                sbPage.AppendFormat("<li {1}><a  href=\"{2}\"   " + (PagerAjax ? "data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"" + PagerAjaxUpdate : " ") + "\">{0}</a></li>",
                    i,
                    i == model.PageIndex ? "class=\"active\"" : "",
                    urlHelper.Action("", vs));
            }

            if (model.PageIndex < totalPage)
            {
                vs["pageIndex"] = model.PageIndex + 1 > model.TotalCount ? model.PageIndex : model.PageIndex + 1;

                sbPage.AppendFormat("<li><a href=\"{0}\" aria-label=\"Next\" " + (PagerAjax ? "data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"" + PagerAjaxUpdate : " ") + "\"><span aria-hidden=\"true\">></span></a></li>", urlHelper.Action("", vs));

                vs["pageIndex"] = totalPage;

                sbPage.AppendFormat("<li><a href=\"{0}\" aria-label=\"Next\"   " + (PagerAjax ? "data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"" + PagerAjaxUpdate : " ") + "\"><span aria-hidden=\"true\">>|</span></a></li>", urlHelper.Action("",vs));
            }

            sbPage.Append("<li>");

            sbPage.Append("<span>");

            vs["pageIndex"] = 1;

            sbPage.Append("<form action=\"" + urlHelper.Action("", vs) +
                           "\"  " + (PagerAjax ? "data-ajax=\"true\" data-ajax-mode=\"replace\" data-ajax-update=\"" + PagerAjaxUpdate : " ") + "\" id=\"form1\" method=\"post\" >");

            sbPage.Append("每页" + model.PageSize + "条/共" + model.TotalCount + "条 第");

            sbPage.Append("<input type=\"text\" id=\"pageIndex\" name=\"pageIndex\" size=4  style=\"border:0;border-bottom: 1px solid white;text-align: center;padding:0;  background-color:transparent;\" value=" + model.PageIndex + " />");

            sbPage.Append("页/共" + totalPage + "页");
            sbPage.Append("</form>");

            sbPage.Append("</span>");

            sbPage.Append("</li>");

            sbPage.Append("</ul>");

            sbPage.Append("</nav>");

            #endregion

            output.Content.SetHtmlContent(sbPage.ToString());
            //output.TagMode = TagMode.SelfClosing;
            base.Process(context, output);
        }

    }


}