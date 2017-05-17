using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wjw1.Infrastructure;

namespace Wjw1.Libarary.ModuleBaseLibrary.Extentions
{
    public static class LinqExtensions
    {
        public static FileContentResult ToExcelFile<T>(this IQueryable<T> model)
        {
            //转换列名语言
            
            var excel = new ExcelPackage();

            var wsData = excel.Workbook.Worksheets.Add(DateTime.Now.ToDateTimeString());

            wsData.Cells.LoadFromCollection(model, true);

            return new FileContentResult(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
