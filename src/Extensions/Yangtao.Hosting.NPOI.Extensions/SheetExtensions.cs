using NPOI.SS.UserModel;
using Yangtao.Hosting.NPOI.Extensions.Enums;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class SheetExtensions
    {
        /// <summary>
        /// 插入单元格
        /// </summary>
        /// <param name="sheet">工作薄</param>
        /// <param name="sourceRowIndex">起始行索引</param>
        /// <param name="columnStartIndex">起始列索引</param>
        /// <param name="columnEndIndex">结束列索引</param>
        /// <param name="insertCount">要插入的数量</param>
        /// <param name="direction">活动单元格移动方向，默认下向</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static ISheet InsertCell(this ISheet sheet, int sourceRowIndex, int columnStartIndex, int columnEndIndex, int insertCount, OffsetDirection direction = OffsetDirection.Down)
        {
            if (columnStartIndex > columnEndIndex) throw new ArgumentOutOfRangeException(nameof(columnEndIndex), "");

            if (columnStartIndex < 0) throw new ArgumentOutOfRangeException(nameof(columnStartIndex), "The column start index can not less than 0");
            if (columnStartIndex > columnEndIndex) throw new ArgumentException("The column end index can not less than column start index", nameof(columnEndIndex));


            var endRowIndex = sheet.LastRowNum;
            for (int rowIndex = endRowIndex; rowIndex > sourceRowIndex; rowIndex--)
            {
                var row = sheet.Row(rowIndex);
                for (int columnIndex = columnStartIndex; columnIndex <= columnEndIndex; columnIndex++)
                {
                    var cell = row.Cell(columnIndex);
                    var targetRowIndex = rowIndex + insertCount;
                    cell.CopyTo(targetRowIndex);
                    cell.ClearValue().ClearMerge();
                }
            }


            var insertRowStartIndex = sourceRowIndex + 1;
            var midEndRowEndIndex = sourceRowIndex + insertCount;
            var sourceRow = sheet.Row(sourceRowIndex);
            for (int rowIndex = insertRowStartIndex; rowIndex <= midEndRowEndIndex; rowIndex++)
            {
                var row = sheet.Row(rowIndex);
                row.Height = sourceRow.Height;

                for (int columnIndex = columnStartIndex; columnIndex <= columnEndIndex; columnIndex++)
                {
                    var sourceRowCell = sourceRow.Cell(columnIndex);
                    if (sourceRowCell.CellStyle == null) continue;

                    var cell = row.Cell(columnIndex);
                    cell.CellStyle = sourceRowCell.CellStyle;
                }
            }


            return sheet;
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static IRow Row(this ISheet sheet, int rowIndex) => sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);

        /// <summary>
        /// 获取所有行
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static IEnumerable<IRow> GetRows(this ISheet sheet)
        {
            var rows = new List<IRow>();
            for (int index = 0; index < sheet.LastRowNum; index++)
            {
                var row = sheet.GetRow(index);
                if (row == null) continue;

                rows.Add(row);
            }

            return rows;
        }

        /// <summary>
        /// 获取所有单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static IEnumerable<ICell> GetCells(this ISheet sheet)
        {
            var rows = sheet.GetRows();
            var cells = new List<ICell>();

            foreach (var row in rows)
            {
                cells.AddRange(row);
            }

            return cells;
        }

        /// <summary>
        /// 查找单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static ICell? FindCell(this ISheet sheet, Func<ICell, bool> predicate)
        {
            var cells = sheet.GetCells();
            return cells.FirstOrDefault(predicate);
        }

        /// <summary>
        /// 查询单元格
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<ICell> QueryCell(this ISheet sheet, Func<ICell, bool> predicate)
        {
            var cells = sheet.GetCells();
            return cells.Where(predicate);
        }
    }
}
