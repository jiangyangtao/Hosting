using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class SheetExtensions
    {
        public static string[] GetColumns(this ISheet sheet)
        {
            var headerRow = sheet.GetRow(0);
            var cellCount = headerRow.LastCellNum;
            var excelcolumnNames = new List<string>();
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                var cell = headerRow.GetCell(i);
                excelcolumnNames.Add(cell.StringCellValue);
            }

            return excelcolumnNames.ToArray();
        }
    }
}
