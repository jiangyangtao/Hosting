using NPOI.HPSF;
using NPOI.HSSF.UserModel;
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

        public static IFont GetHeadRequiredFont(this IWorkbook workbook)
        {
            var font = workbook.CreateFont();
            font.IsBold = true;
            font.Color = IndexedColors.Red.Index;

            return font;
        }

        public static ICellStyle CreateDrawBorderStyle(this IWorkbook workbook, BorderStyle border = BorderStyle.Thin)
        {
            var cellStyle = workbook.CreateCellStyle();
            cellStyle.BorderTop = border;
            cellStyle.BorderRight = border;
            cellStyle.BorderBottom = border;
            cellStyle.BorderLeft = border;
            return cellStyle;
        }

        public static ICellStyle CreateCellStyle(this IWorkbook workbook, Action<ICellStyle> action)
        {
            var cellStyle = workbook.CreateCellStyle();
            action(cellStyle);
            return cellStyle;
        }

        public static void SetAuthor(this IWorkbook workbook, string author)
        {
            if (workbook is XSSFWorkbook)
            {
                var xssfWorkbook = workbook as XSSFWorkbook;
                var xmlProps = xssfWorkbook.GetProperties();
                var coreProps = xmlProps.CoreProperties;
                coreProps.Creator = author;
                return;
            }

            if (workbook is HSSFWorkbook)
            {
                var hssfWorkbook = workbook as HSSFWorkbook;
                var summaryInfo = hssfWorkbook.SummaryInformation;

                if (summaryInfo != null)
                {
                    summaryInfo.Author = author;
                    return;
                }

                var newDocInfo = PropertySetFactory.CreateDocumentSummaryInformation();

                var newInfo = PropertySetFactory.CreateSummaryInformation();
                newInfo.Author = author;

                hssfWorkbook.DocumentSummaryInformation = newDocInfo;
                hssfWorkbook.SummaryInformation = newInfo;

            }
        }
    }
}
