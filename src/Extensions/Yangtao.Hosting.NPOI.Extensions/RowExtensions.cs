using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class RowExtensions
    {
        public static ICell GetCell(this IRow row, int columnIndex)
        {
            var cell = row.GetCell(columnIndex);
            if (cell == null) row.CreateCell(columnIndex);

            return cell;
        }
    }
}
