using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using Wjw1.Infrastructure;

namespace AngularApp.Helpers
{
    public static class LinqExtensions
    {
        public static FileContentResult ToExcelFile<T>(this IQueryable<T> model)
        {
            //转换列名语言

      




            var excel = new ExcelPackage();

            var wsData = excel.Workbook.Worksheets.Add(DateTime.Now.ToDateString());

            wsData.Cells.LoadFromCollection(model, true);

            return new FileContentResult(excel.GetAsByteArray(),  "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
