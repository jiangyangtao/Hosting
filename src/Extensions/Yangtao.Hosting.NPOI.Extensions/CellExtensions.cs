using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI.Extensions
{
    public static class CellExtensions
    {
        public static ICell DrawBorder(this ICell cell)
        {
            cell.CellStyle.BorderTop = BorderStyle.Thin;
            cell.CellStyle.BorderRight = BorderStyle.Thin;
            cell.CellStyle.BorderBottom = BorderStyle.Thin;
            cell.CellStyle.BorderLeft = BorderStyle.Thin;

            return cell;
        }
    }
}
