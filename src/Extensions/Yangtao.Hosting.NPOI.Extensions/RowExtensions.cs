using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class RowExtensions
    {
        /// <summary>
        /// Get or Create cell, If cell not exist then create
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static ICell GetOrCreateCell(this IRow row, int columnIndex) => row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

        public static IRow CopyTo(this IRow row, int targetRowIndex, bool isCopyCellValue = true)
        {
            return row.Sheet.DeepCopyRow(row.RowNum, targetRowIndex, isCopyCellValue);
        }

        public static ICell GetOrCreateCell(this IRow row, int index, ICellStyle cellStyle)
        {
            var cell = row.GetOrCreateCell(index);
            cell.CellStyle = cellStyle;

            return cell;
        }

        public static void AddCells(this IRow row, int cellCount)
        {
            for (int i = 0; i < cellCount; i++)
            {
                row.GetOrCreateCell(row.Cells.Count);
            }
        }

        public static void AddCells(this IRow row, int cellCount, ICellStyle cellStyle)
        {
            for (int i = 0; i < cellCount; i++)
            {
                row.GetOrCreateCell(row.Cells.Count, cellStyle);
            }
        }

        public static bool IsAllBlank(this IRow cells) => cells.All(a => a.CellType == CellType.Blank);
    }
}
