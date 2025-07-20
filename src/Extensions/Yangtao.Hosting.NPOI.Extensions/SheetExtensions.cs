using NPOI.SS.UserModel;
using NPOI.SS.Util;
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


        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="sourceRowIndex">要复制的行索引</param>
        /// <param name="targetRowIndex">目标行索引</param>
        /// <param name="isCopyCellValue">是否复制单元格的值</param>
        public static IRow CopyRowTo(this ISheet sheet, int sourceRowIndex, int targetRowIndex, bool isCopyCellValue = true)
        {
            var sourceRow = sheet.GetRow(sourceRowIndex);
            var targetRow = sheet.GetRow(targetRowIndex);
            targetRow ??= sheet.CreateRow(targetRowIndex);

            targetRow.Height = sourceRow.Height;
            targetRow.RowStyle = sourceRow.RowStyle;

            for (int cellIndex = 0; cellIndex < sourceRow.LastCellNum; cellIndex++)
            {
                var sourceCell = sourceRow.GetCell(cellIndex);
                if (sourceCell != null)
                {
                    var targetCell = targetRow.CreateCell(cellIndex, sourceCell.CellType);
                    targetCell.SetCellType(sourceCell.CellType);
                    targetCell.ClearValue();
                    targetCell.CellStyle = sourceCell.CellStyle;

                    if (sourceCell.CellComment != null) targetCell.CellComment = sourceCell.CellComment;
                    if (sourceCell.Hyperlink != null) targetCell.Hyperlink = sourceCell.Hyperlink;
                    if (isCopyCellValue) targetCell.SetCellValue(sourceCell.StringCellValue);
                }
            }

            for (int mergedRegionIndex = 0; mergedRegionIndex < sheet.NumMergedRegions; mergedRegionIndex++)
            {
                var mergedRegion = sheet.GetMergedRegion(mergedRegionIndex);
                if (mergedRegion.FirstRow >= targetRowIndex && mergedRegion.LastRow <= targetRowIndex)
                {
                    sheet.RemoveMergedRegion(mergedRegionIndex);
                    mergedRegionIndex--;
                }
            }

            for (int mergedRegionIndex = 0; mergedRegionIndex < sourceRow.Sheet.NumMergedRegions; mergedRegionIndex++)
            {
                var mergedRegion = sourceRow.Sheet.GetMergedRegion(mergedRegionIndex);
                if (mergedRegion.FirstRow == sourceRow.RowNum)
                {
                    var newMergedRegion = new CellRangeAddress(
                        targetRow.RowNum,
                        targetRow.RowNum + mergedRegion.LastRow - mergedRegion.FirstRow,
                        mergedRegion.FirstColumn,
                        mergedRegion.LastColumn
                    );

                    sheet.AddMergedRegion(newMergedRegion);
                }
            }

            return targetRow;
        }


        public static string[] GetColumns(this ISheet sheet, int headRowIndex = 0)
        {
            var headerRow = sheet.GetRow(headRowIndex);
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
