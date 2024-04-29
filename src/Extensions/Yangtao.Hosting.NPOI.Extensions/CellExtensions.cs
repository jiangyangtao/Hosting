using NPOI.SS.UserModel;
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
    }
}
