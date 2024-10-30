using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Text;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class CellExtensions
    {
        public static ICell CreateStyleCell(this IRow row, int index, IWorkbook workBook)
        {
            var cell = row.CreateCell(index);
            cell.CellStyle = workBook.CreateCellStyle();

            return cell;
        }

        public static ICell DrawBorder(this ICell cell, BorderStyle border = BorderStyle.Thin)
        {
            cell.CellStyle.BorderTop = border;
            cell.CellStyle.BorderRight = border;
            cell.CellStyle.BorderBottom = border;
            cell.CellStyle.BorderLeft = border;

            return cell;
        }

        public static ICell SetFont(this ICell cell, IFont font)
        {
            cell.CellStyle.SetFont(font);
            return cell;
        }

        public static ICell SetBlodFont(this ICell cell, IWorkbook wrokbook)
        {
            var font = wrokbook.CreateFont();
            font.IsBold = true;
            cell.SetFont(font);

            return cell;
        }

        public static ISheet AutoSizeColumn(this ISheet sheet)
        {
            var first = sheet.GetRow(0);
            if (first == null) return sheet;

            for (int col = 0; col < first.Cells.Count; col++)
            {
                sheet.AutoSizeColumn(col);
                int columnWidth = sheet.GetColumnWidth(col) / 256;
                for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    var row = sheet.GetRow(rowIndex);
                    var cell = row.GetCell(col);

                    var content = cell != null ? cell.ToString() : string.Empty;
                    int contextLength = Encoding.UTF8.GetBytes(content).Length;
                    columnWidth = columnWidth < contextLength ? contextLength : columnWidth;

                    row.Height = 360;
                }
                sheet.SetColumnWidth(col, columnWidth * 256);
            }

            return sheet;
        }




        /// <summary>
        /// 复制到指定的单元格
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="targetRowIndex"></param>
        /// <param name="targetColumnIndex"></param>
        /// <param name="copyMerge"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICell CopyTo(this ICell? cell, int targetRowIndex, int? targetColumnIndex = null, bool copyMerge = true)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));

            var targetRow = cell.Row.Sheet.Row(targetRowIndex);
            var targetCell = targetRow.Cell(targetColumnIndex ?? cell.ColumnIndex);

            CopyCellFormat(cell, targetCell, copyMerge);
            return CopyCellValue(cell, targetCell);
        }


        /// <summary>
        /// 复制格式到指定的单元格
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="targetRowIndex"></param>
        /// <param name="targetColumnIndex"></param>
        /// <param name="copyMerge"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICell CopyFormatTo(this ICell? cell, int targetRowIndex, int? targetColumnIndex = null, bool copyMerge = true)
        {
            if (cell == null) throw new ArgumentNullException(nameof(cell));

            var targetRow = cell.Row.Sheet.Row(targetRowIndex);
            var targetCell = targetRow.Cell(targetColumnIndex ?? cell.ColumnIndex);

            return CopyCellFormat(cell, targetCell, copyMerge);
        }


        /// <summary>
        /// 复制单元格的值
        /// </summary>
        /// <param name="oldCell"></param>
        /// <param name="newCell"></param>
        /// <returns></returns>
        public static ICell CopyCellValue(this ICell oldCell, ICell newCell)
        {
            // Set the cell data value
            switch (oldCell.CellType)
            {
                case CellType.Blank:
                    newCell.SetCellValue(oldCell.StringCellValue);
                    break;
                case CellType.Boolean:
                    newCell.SetCellValue(oldCell.BooleanCellValue);
                    break;
                case CellType.Error:
                    newCell.SetCellErrorValue(oldCell.ErrorCellValue);
                    break;
                case CellType.Formula:
                    newCell.SetCellFormula(oldCell.CellFormula);
                    break;
                case CellType.Numeric:
                    newCell.SetCellValue(oldCell.NumericCellValue);
                    break;
                case CellType.String:
                    newCell.SetCellValue(oldCell.RichStringCellValue);
                    break;
            }
            return newCell;
        }


        /// <summary>
        /// 复制单元格格式
        /// </summary>
        /// <param name="oldCell"></param>
        /// <param name="newCell"></param>
        /// <param name="copyMerge"></param>
        /// <returns></returns>
        public static ICell CopyCellFormat(this ICell oldCell, ICell newCell, bool copyMerge = true)
        {
            newCell.Row.Height = oldCell.Row.Height;
            //oldCell.Row.Sheet.me

            // Copy style from old cell and apply to new cell
            if (oldCell.CellStyle != null)
            {
                newCell.CellStyle = oldCell.CellStyle;
            }
            // If there is a cell comment, copy
            if (oldCell.CellComment != null)
            {
                newCell.CellComment = oldCell.CellComment;
            }

            // If there is a cell hyperlink, copy
            if (oldCell.Hyperlink != null)
            {
                newCell.Hyperlink = oldCell.Hyperlink;
            }

            // Set the cell data type
            newCell.SetCellType(oldCell.CellType);

            if (newCell.IsMergedCell == false && oldCell.IsMergedCell && copyMerge)
            {
                for (int i = 0; i < oldCell.Sheet.NumMergedRegions; i++)
                {
                    var cellRangeAddress = oldCell.Sheet.GetMergedRegion(i);
                    if (cellRangeAddress == null) continue;
                    if (cellRangeAddress.FirstRow != oldCell.RowIndex) continue;

                    var exist = cellRangeAddress.ContainsColumn(newCell.ColumnIndex);
                    if (exist == false) continue;

                    var index = newCell.RowIndex + (cellRangeAddress.LastRow - cellRangeAddress.FirstRow);
                    var newCellRangeAddress = new CellRangeAddress(newCell.RowIndex, index, cellRangeAddress.FirstColumn, cellRangeAddress.LastColumn);
                    newCell.Sheet.AddMergedRegion(newCellRangeAddress);

                    for (int j = newCellRangeAddress.FirstColumn; j <= newCellRangeAddress.LastColumn; j++)
                    {
                        var cell = newCell.Row.CreateCell(j);
                        cell.CellStyle = oldCell.CellStyle;
                    }
                }
            }

            return newCell;
        }


        /// <summary>
        /// 清除合并
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static ICell ClearMerge(this ICell cell)
        {
            if (cell.IsMergedCell)
            {
                for (int i = 0; i < cell.Sheet.NumMergedRegions; i++)
                {
                    var cellRangeAddress = cell.Sheet.GetMergedRegion(i);
                    if (cellRangeAddress == null) continue;
                    if (cellRangeAddress.FirstRow != cell.RowIndex) continue;

                    var exist = cellRangeAddress.ContainsColumn(cell.ColumnIndex);
                    if (exist == false) continue;

                    cell.Sheet.RemoveMergedRegion(i);
                }
            }

            return cell;
        }

        /// <summary>
        /// 清除值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static ICell ClearValue(this ICell cell)
        {
            cell.SetBlank();
            return cell;
        }
    }
}
