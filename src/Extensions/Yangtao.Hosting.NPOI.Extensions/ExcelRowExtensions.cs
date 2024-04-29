using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class ExcelRowExtensions
    {
        public static void AddCells(this IRow row, int cellCount)
        {
            for (int i = 0; i < cellCount; i++)
            {
                row.CreateCell(row.Cells.Count);
            }
        }

        public static void AddCells(this IRow row, int cellCount, IWorkbook workbook)
        {
            for (int i = 0; i < cellCount; i++)
            {
                row.CreateStyleCell(row.Cells.Count, workbook);
            }
        }

        public static bool IsAllBlank(this IRow cells) => cells.All(a => a.CellType == CellType.Blank);
    }
}
