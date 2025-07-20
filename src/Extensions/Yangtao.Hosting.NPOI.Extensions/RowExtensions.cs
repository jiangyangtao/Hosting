using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class RowExtensions
    {
        public static ICell Cell(this IRow row, int columnIndex) => row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

        public static IRow CopyTo(this IRow row, int targetRowIndex, bool isCopyCellValue = true)
        {
            return row.Sheet.CopyRowTo(row.RowNum, targetRowIndex, isCopyCellValue);
        }

        public static bool IsAllBlank(this IRow cells) => cells.All(a => a.CellType == CellType.Blank);
    }
}
