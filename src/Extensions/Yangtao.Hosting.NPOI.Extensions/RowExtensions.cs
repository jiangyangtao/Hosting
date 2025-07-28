using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class RowExtensions
    {
        public static ICell Cell(this IRow row, int columnIndex) => row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

        public static IRow CopyTo(this IRow row, int targetRowIndex, bool isCopyCellValue = true)
        {
            return row.Sheet.DeepCopyRow(row.RowNum, targetRowIndex, isCopyCellValue);
        }

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
