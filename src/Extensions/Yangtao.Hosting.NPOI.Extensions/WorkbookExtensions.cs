﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class WorkbookExtensions
    {
        public static string GetContentType(this IWorkbook workBook)
        {
            if (workBook is HSSFWorkbook hssf) return hssf.DocumentSummaryInformation.ContentType;

            if (workBook is XSSFWorkbook xssf) return xssf.WorkbookType.ContentType;

            return string.Empty;
        }
    }
}
