using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class RowExtensions
    {
        public static ICell Cell(this IRow row, int columnIndex) => row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
    }
}
